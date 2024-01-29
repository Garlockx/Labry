using UnityEngine;
using TMPro;

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
        tutoText.GetComponent<TextMeshPro>().text = "When you move onto a green tile, it turns orange and then red. \n Try to move onto a red tile.";
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
        playerIsDead = true;
    }
}
