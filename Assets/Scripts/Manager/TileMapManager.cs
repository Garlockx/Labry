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

    private bool isLevelComplete;
    

    public struct NearTile
    {
        public Vector3 position;
        public float distanceWithPlayer;
        public Color color;

        public NearTile(Vector3 position, Color color)
        {
            TileMapManager tileMapManager = FindObjectOfType<TileMapManager>();

            this.position = position;
            this.color = color;
            distanceWithPlayer = tileMapManager.getDistanceWithPlayer(position);
        }
    }

    void Start()
    {
        baseRules = GameObject.FindGameObjectWithTag("Rules").GetComponent<BaseRules>();
        player = GameObject.FindGameObjectWithTag("Player");
        tilemap = gameObject.GetComponent<Tilemap>();
        allTilesPos = tilemap.cellBounds.allPositionsWithin;
        checkColor();
        player.GetComponentInChildren<PlayerMovement>().onPlayerMove += checkColor;
    }

    public bool getIsLevelComplete()
    {
        return isLevelComplete;
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

    public Color getTileColor(Vector3 position)
    {
        Vector3Int cellPosition = tilemap.WorldToCell(position);
        return tilemap.GetColor(cellPosition);
    }

    public float getDistanceWithPlayer(Vector3 position)
    {
        Vector3Int cellPosition = tilemap.WorldToCell(position);
        Vector3Int playerCellPosition = tilemap.WorldToCell(player.transform.GetChild(0).transform.position);
        float distanceWithPlayer = Vector3Int.Distance(playerCellPosition, cellPosition);
        return distanceWithPlayer;
    }

    public float getDistanceWithPlayer(Vector3Int position)
    {
        Vector3Int playerCellPosition = tilemap.WorldToCell(player.transform.GetChild(0).transform.position);
        float distanceWithPlayer = Vector3Int.Distance(playerCellPosition, position);
        return distanceWithPlayer;
    }

    /**
     * Return a list of accessible tiles nearest a point 
     */
    public List<NearTile> getNearestTilesPositions(Vector3 position)
    {
        List<NearTile> nearestTilesPositions = new List<NearTile>();

        Vector3 topPostion = position;
        topPostion.y += 1;
        Vector3 bottomPostion = position;
        bottomPostion.y -= 1;
        Vector3 leftPostion = position;
        leftPostion.x -= 1;
        Vector3 rightPostion = position;
        rightPostion.x += 1;

        if (tilemap.GetTile(tilemap.WorldToCell(topPostion)) != null)
        {
            nearestTilesPositions.Add(new NearTile(topPostion, tilemap.GetColor(tilemap.WorldToCell(topPostion))));
        }
        if (tilemap.GetTile(tilemap.WorldToCell(bottomPostion)) != null)
        {
            nearestTilesPositions.Add(new NearTile(bottomPostion, tilemap.GetColor(tilemap.WorldToCell(bottomPostion))));
        }
        if (tilemap.GetTile(tilemap.WorldToCell(leftPostion)) != null)
        {
            nearestTilesPositions.Add(new NearTile(leftPostion, tilemap.GetColor(tilemap.WorldToCell(leftPostion))));
        }
        if (tilemap.GetTile(tilemap.WorldToCell(rightPostion)) != null)
        {
            nearestTilesPositions.Add(new NearTile(rightPostion, tilemap.GetColor(tilemap.WorldToCell(rightPostion))));
        }
        return nearestTilesPositions;
    }

    public void setTileColor(Vector3 position, Color color)
    {
        Vector3Int cellPosition = tilemap.WorldToCell(position);
        tilemap.SetColor(cellPosition, color);
    }

    public void setAllTilesColor(Color color)
    {
        foreach (Vector3Int tilePos in allTilesPos)
        {
            if (tilemap.HasTile(tilePos))
            {
                tilemap.SetColor(tilePos, color);
            }
        }
    }
    
    public void invokeWinning()
    {
        FindObjectOfType<BaseRules>().deathRule = false;
        player.GetComponent<PlayerManager>().playerCannotMove();
        onWinning?.Invoke();
    }

    private void isWinning()
    {
        bool levelComplete = true;
        GameColorsManager gameColorManager = FindObjectOfType<GameColorsManager>();
        if (!baseRules.winningRule)
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
                if (tilemap.GetColor(tilePos) != gameColorManager.getColor("FirstColor"))
                {
                    levelComplete = false;
                }
            }
        }
        isLevelComplete = levelComplete;
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
        GameColorsManager gameColorManager = FindObjectOfType<GameColorsManager>();
        if (tileColor == Color.white)
        {
            tilemap.SetColor(cellPosition, gameColorManager.getColor("FirstColor"));
            isWinning();
            return;
        }
        if (tileColor == gameColorManager.getColor("FirstColor"))
        {
            tilemap.SetColor(cellPosition, gameColorManager.getColor("SecondColor"));
            return;
        }
        if (tileColor == gameColorManager.getColor("SecondColor"))
        {
            tilemap.SetColor(cellPosition, gameColorManager.getColor("ThirdColor"));
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
