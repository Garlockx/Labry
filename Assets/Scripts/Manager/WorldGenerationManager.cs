using UnityEngine;
using UnityEngine.Tilemaps;
using TMPro;

public class WorldGenerationManager : MonoBehaviour
{
    [SerializeField]
    Tilemap tilemap;

    private int worldNumber;
    private Object[] worldSprite;
    private Vector3Int defaultPosition = new Vector3Int(0, -1, 0);

    private SaveManager saveManager;

    private void Start()
    {
        saveManager = FindObjectOfType<SaveManager>();
        saveManager.setFirstPlayerPrefs();
        worldNumber = saveManager.getUnlockWorld();
        worldCreation();
        SelectionManager selectionManager = gameObject.AddComponent<SelectionManager>();
        selectionManager.tilemap = tilemap;
        Camera.main.gameObject.AddComponent<CameraManager>();
       
    }

    public int getWorldNumber()
    {
        return worldNumber;
    }

    // Start is called before the first frame update
    public void worldCreation()
    {
        worldSprite = Resources.LoadAll("WorldSprite");
        setTilemapSize();
        createTutorialTile();
        createWorldSelectionTile();
        createWorldBetweenTile();
        addWorldName();
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

    private void addWorldName()
    {
        Vector3 nextNamePos = new Vector3(1.15f, -1.0f, 0.0f);

        for (int i = 0; i < worldNumber; i++)
        {
            GameObject worldNameText = Instantiate((GameObject)Resources.Load("Prefabs/VTextWithArrow", typeof(GameObject)), nextNamePos, Quaternion.identity);
            worldNameText.transform.GetChild(1).GetComponent<TextMeshPro>().text += " " + (i + 1);
            nextNamePos.x += 3.0f;
        }
    }
}
