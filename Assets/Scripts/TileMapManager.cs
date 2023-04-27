using UnityEngine;
using UnityEngine.Tilemaps;

public class TileMapManager : MonoBehaviour
{
    private Tilemap tilemap;
    private GameObject player;

    /*
     POSSIBLE CELLS COLORS     
     */
    private Color Green = new Color(0.47f, 0.84f, 0.06f);
    private Color Orange = new Color(0.96f, 0.64f, 0.10f);
    private Color Red = new Color(0.83f, 0.15f, 0.15f);


    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        tilemap = GameObject.FindGameObjectWithTag("Path").GetComponent<Tilemap>();
        checkColor();
        player.GetComponent<PlayerMovement>().onPlayerMove += checkColor;
    }

    private void checkColor()
    {
        Vector3Int cellPosition = tilemap.WorldToCell(player.transform.position);
        updateColor(cellPosition);
    }

    private void updateColor(Vector3Int _cellPosition) 
    {
        Color tileColor = tilemap.GetColor(_cellPosition);
        if (tileColor == Color.white)
        {
            tilemap.SetColor(_cellPosition, Green);
            return;
        }
        if (tileColor == Green)
        {
            tilemap.SetColor(_cellPosition, Orange);
            return;
        }
        if (tileColor == Orange)
        {
            tilemap.SetColor(_cellPosition, Red);
            return;
        }
        Debug.Log("You are Dead");
        //DEAD 
        return;
    }

}
