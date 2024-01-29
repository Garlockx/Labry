using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField]
    Vector3 playerStartPosition;
    
    [SerializeField]
    //Turn it to true if it's the last level of the current world (needed to save)
    bool isLastLevel = false;

    private GameObject saveManager;
    private TileMapManager tileMapManager;

    private void Start()
    {
        loadCanvas();
        loadCustomShaderManager();
        loadPlayer();
        loadMapManager();
        loadRulesLevel();
    }

    private void loadCanvas()
    {
        Instantiate(Resources.Load("Prefabs/UILevel"));
    }

    private void loadCustomShaderManager()
    {
        GameObject customShaderManager = new GameObject("CustomShaderManager");
        customShaderManager.AddComponent<CustomShaderManager>();
    }


    private void loadRulesLevel()
    {
        string ComponentToLoad = SceneManager.GetActiveScene().name + "Rules";
        if (Type.GetType(ComponentToLoad) != null)
        {
            GameObject.FindGameObjectWithTag("Rules").AddComponent(Type.GetType(ComponentToLoad));
        }
    }

    private void loadPlayer()
    {
        GameObject player = Instantiate((GameObject)Resources.Load("Prefabs/PlayerManager", typeof(GameObject)), playerStartPosition, Quaternion.identity);
        player.GetComponent<PlayerManager>().onPlayerDeath += loadDeathCanvas;
    }

    private void loadMapManager()
    {
        tileMapManager = GameObject.FindGameObjectWithTag("Path").AddComponent<TileMapManager>();
        tileMapManager.onWinning += loadWinningCanvas;
        tileMapManager.onWinning += loadSaveManager; 
        tileMapManager.onWinning += saveProgression;
    }

    private void loadDeathCanvas()
    {
        Instantiate(Resources.Load("Prefabs/DeathCanvas"), new Vector3(), Quaternion.identity);
    }

    private void loadWinningCanvas()
    {
        Instantiate(Resources.Load("Prefabs/WinningCanvas"), new Vector3(), Quaternion.identity);
    }

    private void loadSaveManager()
    {
        saveManager = Instantiate((GameObject)Resources.Load("Prefabs/SaveManager", typeof(GameObject)),new Vector3(), Quaternion.identity);
    }

    private void saveProgression()
    {
        string sceneName = SceneManager.GetActiveScene().name;
        int worldId = int.Parse(sceneName.Substring(1).Substring(0, sceneName.IndexOf("L") - 1));
        int levelId = int.Parse(sceneName.Substring(sceneName.IndexOf("L") + 1));

        if (tileMapManager.getIsLevelComplete())
        {
            saveManager.GetComponent<SaveManager>().levelCompleted(worldId, levelId);
        }

        if (isLastLevel)
        {
            saveManager.GetComponent<SaveManager>().worldFinished();
            return;
        }
        saveManager.GetComponent<SaveManager>().newLevelUnlock(worldId, levelId);
    }
}
