using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class W0L1Rules : MonoBehaviour
{
    GameObject player;
    GameObject tutoText;
    BaseRules baseRules;
    TileMapManager tileMapManager;
    bool playerIsDead = false;
    bool checkFirstTuto = false;

    // Start is called before the first frame update
    void Start()
    {
        reloading();
        tutoText = GameObject.FindGameObjectWithTag("TutoText");
        baseRules = gameObject.GetComponent<BaseRules>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player == null || tileMapManager == null)
        {
            reloading();
        }
        if (playerIsDead == true)
        {
            return;
        }
        if (checkFirstTuto == false)
        {
            firstTuto();
            return;
        }
        afterDeath();
    }

    private void reloading()
    {
        player = GameObject.FindGameObjectWithTag("PlayerCollider");
        tileMapManager = FindObjectOfType<TileMapManager>();
    }

    private void firstTuto()
    {
        foreach (Color color in tileMapManager.getAllTilesColor())
        {
            if (color == Color.white)
            {
                return;
            }
        }
        tutoText.GetComponent<TextMeshPro>().text = "Si vous repassé encore une fois sur une même case, elle devient orange puis rouge. Essayez de passer sur une case rouge";
        baseRules.deathRule = true;
        checkFirstTuto = true;
    }
     
    private void afterDeath()
    {
        GameObject deathCanvas = GameObject.FindGameObjectWithTag("Canvas");
        if (deathCanvas == null)
        {
            return;
        }
        updateDeathCanvas(deathCanvas);
        playerIsDead = true;
    }

    private void updateDeathCanvas(GameObject deathCanvas)
    {
        tutoText.GetComponent<TextMeshPro>().text = "";
        DeathCanvasManager restartButton = deathCanvas.GetComponentInChildren<DeathCanvasManager>();
        restartButton.gameObject.transform.localPosition = new Vector3(200.0f, -26.0f, 0.0f);
        GameObject finalText = (GameObject)Instantiate(Resources.Load("Prefabs/TextTemplate"), deathCanvas.transform);
        GameObject buttonReturnToWorld = (GameObject)Instantiate(Resources.Load("Prefabs/ButtonTemplate"), deathCanvas.transform);
        buttonReturnToWorld.transform.localPosition = new Vector3(-200.0f, -26.0f, 0.0f);
        buttonReturnToWorld.GetComponent<Button>().onClick.AddListener(() =>
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name.Substring(0, 2), LoadSceneMode.Single);
        });
        buttonReturnToWorld.GetComponentInChildren<Text>().text = "Level Selection";
        finalText.GetComponent<TextMeshProUGUI>().text = "Vous avez été déchiré mais vous avez réussi cette partie du tutorial";
    }
}
