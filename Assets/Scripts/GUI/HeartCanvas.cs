using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;


public class HeartCanvas : MonoBehaviour
{
    private List<GameObject> hearts;
    private EnemyManager enemyManager;

    // Start is called before the first frame update
    void Start()
    {
        enemyManager = FindObjectOfType<EnemyManager>();
        hearts = new List<GameObject>();
        addHeart();
        enemyManager.onEnemyHit += enemyLoseHeart;
        enemyManager.onEnemyDie += deleteCanvas;
    }

    public void addText(string text)
    {
        gameObject.GetComponentInChildren<Text>().text = text;
    }

    private void deleteCanvas()
    {
        Destroy(gameObject.gameObject);
    }

    private void addHeart()
    {
        int health = enemyManager.getMaxLife();
        Vector3 firstHeartPos = new Vector3(0.0f, 325.0f, 0.0f);

        if (health % 2 == 0)
        {
            firstHeartPos.x = -32 + (((health / 2) - 1) * - 64);
        } else
        {
            firstHeartPos.x = (health - 1) / 2 * -64;
        }

        for(int i = 0; i < health; i++)
        {
            GameObject heart = Instantiate((GameObject)Resources.Load("Prefabs/Heart", typeof(GameObject)));
            heart.GetComponent<RectTransform>().SetParent(gameObject.transform);
            heart.transform.localPosition = firstHeartPos;
            hearts.Add(heart);
            firstHeartPos.x += 64;
        }
    }

    private void enemyLoseHeart()
    {
        Sprite[] sprites;
        sprites = Resources.LoadAll<Sprite>("UISprite/hearts");
        hearts[enemyManager.getCurrentLife()].GetComponent<Image>().sprite = sprites[1];
    }
}
