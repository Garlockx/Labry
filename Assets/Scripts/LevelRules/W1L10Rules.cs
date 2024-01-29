using UnityEngine;
using System.Collections;

public class W1L10Rules : MonoBehaviour
{
    public float cameraSpeed = 10.0f;

    public Vector3[] enemiesPositions;
    public Vector3[] cameraPositions; 
    public Vector3[] playerPositions;

    private GameObject enemy;
    private EnemyManager enemyManager;
    private TileMapManager tileMapManager;

    // Start is called before the first frame update
    void Start()
    {
        cameraPositions = new Vector3[3] { new Vector3(35.5f, -0.5f, -10.0f), new Vector3(35.5f, 32.5f, -10.0f), new Vector3(64.5f, 32.5f, -10.0f)};
        playerPositions = new Vector3[3] { new Vector3(30.5f, -0.5f, 0.0f), new Vector3(36.5f, 32.5f, 0.0f), new Vector3(64.5f, 32.5f, 0.0f)};
        enemiesPositions = new Vector3[3] { new Vector3(40.5f, -0.5f, 0.0f), new Vector3(31.5f, 28.5f, 0.0f), new Vector3(69.5f, 32.5f, 0.0f)};
        tileMapManager = GameObject.FindGameObjectWithTag("Path").GetComponent<TileMapManager>();
        tileMapManager.setAllTilesColor(FindObjectOfType<GameColorsManager>().getColor("FirstColor"));
        addEnemy(new Vector3(3.0f, 1.5f, 0.0f));
        addHeartCanvas();
    }

    private void addEnemy(Vector3 position)
    {
        enemy = Instantiate((GameObject)Resources.Load("Prefabs/EnemyManager", typeof(GameObject)), position, Quaternion.identity);
        EnemyManager.EnemyParameter enemyParameter = new EnemyManager.EnemyParameter(10.0f, 0.1f, 2.0f, 5.0f, 3.0f, 4);
        enemyManager = enemy.GetComponent<EnemyManager>();
        enemyManager.setParameters(enemyParameter);
        enemyManager.onEnemyHit += moveEnemy;
        enemyManager.onEnemyHit += moveCamera;
        enemyManager.onEnemyHit += movePlayer;
        enemyManager.onEnemyDie += tileMapManager.invokeWinning;
    }

    private void addHeartCanvas()
    {
        GameObject canvas = Instantiate((GameObject)Resources.Load("Prefabs/HeartCanvas", typeof(GameObject)));
        canvas.GetComponent<HeartCanvas>().addText("Catch Him !");
    }

    private void moveEnemy()
    {
        enemy.GetComponentInChildren<EnemyMovement>().moveToOtherPosition(enemiesPositions[enemyManager.getMaxLife() - enemyManager.getCurrentLife() - 1]);
        if (enemyManager.getCurrentLife() == 1)
        {
            enemyManager.setSpeed(5.0f);
        }
    }

    private void moveCamera()
    {
        StartCoroutine(MoveCameraTo(cameraPositions[enemyManager.getMaxLife() - enemyManager.getCurrentLife() - 1]));
    }

    private void addCameraFollowPlayer()
    {
        Camera.main.gameObject.AddComponent<CameraFollowPlayer>();
        StartCoroutine(ChangeCameraSize(4.0f));
    }

    private void movePlayer()
    {
        GameObject player = GameObject.FindGameObjectWithTag("PlayerCollider");
        player.GetComponent<PlayerMovement>().moveToOtherPosition(playerPositions[enemyManager.getMaxLife() - enemyManager.getCurrentLife() - 1]);
    }

    IEnumerator MoveCameraTo(Vector3 target)
    {
        while ((Camera.main.transform.position - target).sqrMagnitude != 0)
        {
            Camera.main.transform.position = Vector3.MoveTowards(Camera.main.transform.position, target, cameraSpeed * Time.deltaTime);
            yield return null;
        }
        if (enemyManager.getCurrentLife() == 1)
        {
            addCameraFollowPlayer();
        }
    }

    IEnumerator ChangeCameraSize(float size)
    {
        while (Camera.main.orthographicSize - size != 0)
        {
            Camera.main.orthographicSize = Mathf.Lerp(Camera.main.orthographicSize, 4.0f,  cameraSpeed * Time.deltaTime);
            yield return null;
        }
    }
}
