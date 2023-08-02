using UnityEngine;
using UnityEngine.Tilemaps;

public class LevelGenerationManager : MonoBehaviour
{
    [SerializeField]
    Tilemap tilemap;
    [SerializeField]
    int levelNumber;

    private Object[] worldSprite;
    private Vector3Int defaultPosition = new Vector3Int(0, -1, 0);

    private void Start()
    {
        worldCreation();
        SelectionManager selectionManager = gameObject.AddComponent<SelectionManager>();
        selectionManager.tilemap = tilemap;
        Camera.main.gameObject.AddComponent<CameraManager>();
    }

    // Start is called before the first frame update
    public void worldCreation()
    {
        worldSprite = Resources.LoadAll("WorldSprite");
        setTilemapSize();
        createLevelSelectionTile();
        createLevelBetweenTile();
    }

    private void setTilemapSize()
    {
        Vector3Int mapSize = new Vector3Int(1, 2 + levelNumber * 3, 1);
        tilemap.size = mapSize;
    }

    private void createLevelSelectionTile()
    {
        Vector3Int tilePos = defaultPosition;
        tilePos.y += 1;
        Tile tile = ScriptableObject.CreateInstance<Tile>();
        tile.sprite = (Sprite)worldSprite[5];
        for (int i = 0; i < levelNumber; i++)
        {
            tilemap.SetTile(tilePos, tile);
            tilePos.y += 3;
        }
    }

    private void createLevelBetweenTile()
    {
        Vector3Int tilePos = defaultPosition;
        tilePos.y += 2;
        Tile tile = ScriptableObject.CreateInstance<Tile>();
        tile.sprite = (Sprite)worldSprite[1];
        for (int i = 1; i < levelNumber; i++)
        {
            tilemap.SetTile(tilePos, tile);
            tilePos.y += 1;
            tilemap.SetTile(tilePos, tile);
            tilePos.y += 2;
        }
    }
}
