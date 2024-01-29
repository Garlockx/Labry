using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.SceneManagement;
using TMPro;

public class LevelGenerationManager : MonoBehaviour
{
    [SerializeField]
    Tilemap tilemap;
    [SerializeField]
    int maxLevelNumber;

    private int levelNumberUnlock;
    private Object[] worldSprite;
    private Vector3Int defaultPosition = new Vector3Int(0, -1, 0);
    private SaveManager saveManager;

    private void Start()
    {
        setUnlockLevel();
        addLeveldName();
        worldCreation();
        SelectionManager selectionManager = gameObject.AddComponent<SelectionManager>();
        selectionManager.tilemap = tilemap;
        setCamera();
    }

    public int getLevelNumber()
    {
        return levelNumberUnlock;
    }

    // Start is called before the first frame update
    public void worldCreation()
    {
        worldSprite = Resources.LoadAll("WorldSprite");
        setTilemapSize();
        createLevelSelectionTile();
        createLevelBetweenTile();
    }

    private void setCamera()
    {
        CameraManager cameraManager = Camera.main.gameObject.AddComponent<CameraManager>();
        Vector3 cameraPos = Camera.main.transform.position;
        if (SceneManager.GetActiveScene().name != "W0")
        {
            cameraPos.y = (levelNumberUnlock - 1) * 3.0f + 0.5f;
        }
        cameraManager.setCameraNewPos(cameraPos);
    }

    private void setUnlockLevel()
    {
        if (SceneManager.GetActiveScene().name == "W0")
        {
            levelNumberUnlock = maxLevelNumber;
            return;
        }
        saveManager = FindObjectOfType<SaveManager>();
        string sceneName = SceneManager.GetActiveScene().name;
        int worldId = int.Parse(sceneName.Substring(1));
        int unlockLevelNumber = saveManager.getUnlockLevelNumberInWorld(worldId);
        if (unlockLevelNumber == 0)
        {
            levelNumberUnlock = maxLevelNumber;
        }
        else
        {
            levelNumberUnlock = unlockLevelNumber;
        }
    }

    private void setTilemapSize()
    {
        Vector3Int mapSize = new Vector3Int(1, 2 + levelNumberUnlock * 3, 1);
        tilemap.size = mapSize;
    }

    private void createLevelSelectionTile()
    {
        Vector3Int tilePos = defaultPosition;
        tilePos.y += 1;
        Tile tile = ScriptableObject.CreateInstance<Tile>();
        tile.sprite = (Sprite)worldSprite[5];
        for (int i = 0; i < levelNumberUnlock; i++)
        {
            tile.color = getTileColor(i);
            tilemap.SetTile(tilePos, tile);            
            tilePos.y += 3;
        }
    }

    private Color getTileColor(int levelId)
    {
        if (SceneManager.GetActiveScene().name == "W0")
        {
            return Color.white;
        }
        GameColorsManager gameColorManager = FindObjectOfType<GameColorsManager>();
        string[] levelsCompleted = saveManager.getLevelsCompleted(SceneManager.GetActiveScene().name).Split(';');
        Color tileColor = gameColorManager.getColor("ThirdColor");
        if (levelId == levelNumberUnlock - 1 && levelNumberUnlock != 0)
        {
            return tileColor = Color.white;
        }
        foreach (string level in levelsCompleted)
        {
            if (int.TryParse(level, out int result))
            {
                if (int.Parse(level) == levelId)
                {
                    tileColor = gameColorManager.getColor("FirstColor");
                }
            }
        }
        return tileColor;
    }

    private void createLevelBetweenTile()
    {
        Vector3Int tilePos = defaultPosition;
        tilePos.y += 2;
        Tile tile = ScriptableObject.CreateInstance<Tile>();
        tile.sprite = (Sprite)worldSprite[1];
        for (int i = 1; i < levelNumberUnlock; i++)
        {
            tilemap.SetTile(tilePos, tile);
            tilePos.y += 1;
            tilemap.SetTile(tilePos, tile);
            tilePos.y += 2;
        }
    }

    private void addLeveldName()
    {
        if (SceneManager.GetActiveScene().name == "W0")
        {
            return;
        }
        Vector3 nextNamePos = new Vector3(-1.2f, 1.4f, 0.0f);

        for (int i = 0; i < levelNumberUnlock; i++)
        {
            GameObject worldNameText = Instantiate((GameObject)Resources.Load("Prefabs/TextWithArrow", typeof(GameObject)), nextNamePos, Quaternion.identity);
            worldNameText.transform.GetChild(1).GetComponent<TextMeshPro>().text = "Level " + (i + 1);
            nextNamePos.y += 3.0f;
        }
    }
}
