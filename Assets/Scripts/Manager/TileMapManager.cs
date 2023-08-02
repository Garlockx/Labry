using UnityEngine;
using UnityEngine.Tilemaps;
using System;
using System.Collections.Generic;

public class TileMapManager : MonoBehaviour
{
    public Action onWinning;

    private Tilemap tilemap;
    private GameObject player;
    private BoundsInt.PositionEnumerator allTilesPos;
    private BaseRules baseRules;

    private Color tileUnderColorPlayer;

    /*
     POSSIBLE CELLS COLORS     
     */
    private Color Green = new Color(0.47f, 0.84f, 0.06f);
    private Color Orange = new Color(0.96f, 0.64f, 0.10f);
    private Color Red = new Color(0.83f, 0.15f, 0.15f);


    void Start()
    {
        baseRules = GameObject.FindGameObjectWithTag("Rules").GetComponent<BaseRules>();
        player = GameObject.FindGameObjectWithTag("Player");
        tilemap = gameObject.GetComponent<Tilemap>();
        allTilesPos = tilemap.cellBounds.allPositionsWithin;
        checkColor();
        player.GetComponentInChildren<PlayerMovement>().onPlayerMove += checkColor;
    }

    public Dictionary<string, Color> getGameColors()
    {
        Dictionary<string, Color> gameColor = new Dictionary<string, Color>() { 
            {"Green", Green },
            {"Orange", Green },
            {"Red", Red }
        };

        return gameColor;
    }

    public List<Color> getAllTilesColor()
    {
        List<Color> tilesColors = new List<Color>();

        foreach (Vector3Int tilePos in allTilesPos)
        {
            if (tilemap.HasTile(tilePos))
            {
                tilesColors.Add(tilemap.GetColor(tilePos));
            }
        }

        return tilesColors;
    }
    
    public void invokeWinning()
    {
        player.GetComponent<PlayerManager>().playerCannotDie();
        player.GetComponent<PlayerManager>().playerCannotMove();
        onWinning?.Invoke();
    }

    private void isWinning()
    {
        if(!baseRules.winningRule)
        {
            return;
        }
        foreach(Vector3Int tilePos in allTilesPos)
        {
            if (tilemap.HasTile(tilePos))
            {
                if (tilemap.GetColor(tilePos) == new Color (1.0f, 1.0f, 1.0f, 1.0f))
                {
                    return;
                }
            }
        }
        invokeWinning();
    }

    private void checkColor()
    {
        if (!baseRules.colorRule)
        {
            return;
        }
        Vector3Int cellPosition = tilemap.WorldToCell(player.transform.GetChild(0).transform.position);
        updateColor(cellPosition);
    }

    private void updateColor(Vector3Int cellPosition) 
    {
        tilemap.SetTileFlags(cellPosition, TileFlags.None);
        Color tileColor = tilemap.GetColor(cellPosition);
        if (tileColor == Color.white)
        {
            tilemap.SetColor(cellPosition, Green);
            isWinning();
            return;
        }
        if (tileColor == Green)
        {
            tilemap.SetColor(cellPosition, Orange);
            return;
        }
        if (tileColor == Orange)
        {
            tilemap.SetColor(cellPosition, Red);
            return;
        }
        playerIsDead();
        return;
    }

    private void playerIsDead()
    {
        player.GetComponent<PlayerManager>().playerDeath();
    }
}
