using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField]
    Vector3 playerStartPosition;

    private void Start()
    {
        loadPlayer();
        loadMapManager();
    }

    private void loadPlayer()
    {
        GameObject player = Instantiate((GameObject)Resources.Load("Prefabs/PlayerManager", typeof(GameObject)), playerStartPosition, Quaternion.identity);
        player.GetComponent<PlayerManager>().onPlayerDeath += loadDeathCanvas;
    }

    private void loadMapManager()
    {
        TileMapManager tileMapManager = GameObject.FindGameObjectWithTag("Path").AddComponent<TileMapManager>();
        tileMapManager.onWinning += loadWinningCanvas;
    }

    private void loadDeathCanvas()
    {
        Instantiate(Resources.Load("Prefabs/DeathCanvas"), new Vector3(), Quaternion.identity);
    }

    private void loadWinningCanvas()
    {
        Instantiate(Resources.Load("Prefabs/WinningCanvas"), new Vector3(), Quaternion.identity);
    }
}
