using UnityEngine;
using UnityEngine.Tilemaps;

public class WorldGenerationManager : MonoBehaviour
{
    [SerializeField]
    Tilemap tilemap;
    [SerializeField]
    int worldNumber;

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
        createTutorialTile();
        createWorldSelectionTile();
        createWorldBetweenTile();
    }

    private void setTilemapSize()
    {
        Vector3Int mapSize = new Vector3Int(2 + worldNumber * 3, 1, 1);
        tilemap.size = mapSize;
    }

    private void createTutorialTile()
    {
        Vector3Int tilePos = defaultPosition;
        Tile tile = ScriptableObject.CreateInstance<Tile>();
        tile.sprite = (Sprite)worldSprite[5];
        tilePos.x = -1;
        tilemap.SetTile(tilePos, tile);
        tile.sprite = (Sprite)worldSprite[1];
        tilemap.SetTile(defaultPosition, tile);
    }

    private void createWorldSelectionTile()
    {
        Vector3Int tilePos = defaultPosition;
        tilePos.x += 1;
        Tile tile = ScriptableObject.CreateInstance<Tile>();
        tile.sprite = (Sprite)worldSprite[5];
        for (int i = 0; i < worldNumber; i++)
        {
            tilemap.SetTile(tilePos, tile);
            tilePos.x += 3;
        }
    }

    private void createWorldBetweenTile()
    {
        Vector3Int tilePos = defaultPosition;
        tilePos.x += 2;
        Tile tile = ScriptableObject.CreateInstance<Tile>();
        tile.sprite = (Sprite)worldSprite[1];
        for (int i = 1; i < worldNumber; i++)
        {
            tilemap.SetTile(tilePos, tile);
            tilePos.x += 1;
            tilemap.SetTile(tilePos, tile);
            tilePos.x += 2;
        }
    }
}
