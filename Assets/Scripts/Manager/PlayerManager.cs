using UnityEngine;
using System;

public class PlayerManager : MonoBehaviour
{
    public Action onPlayerDeath;
    private bool isPlayerCanMove = true;
    private bool playerDieState = true;

    public void playerCannotMove()
    {
        isPlayerCanMove = false;
    }

    public bool getIsPlayerCanMove()
    {
        return isPlayerCanMove;
    }

    public void playerCanDie()
    {
        playerDieState = true;
    }

    public void playerCannotDie()
    {
        playerDieState = false;
    }

    public void playerDeath()
    {
        BaseRules baseRules = GameObject.FindGameObjectWithTag("Rules").GetComponent<BaseRules>();
        if (!baseRules.deathRule)
        {
            return;
        }
        if (!playerDieState)
        {
            return;
        }
        playerDeathAnimation();
        Destroy(gameObject.transform.GetChild(0).gameObject);
        onPlayerDeath?.Invoke();
    }

    private void playerDeathAnimation()
    {
        GameObject deathSprite = Instantiate((GameObject)Resources.Load("Prefabs/DeathSprite", typeof(GameObject)), gameObject.transform.GetChild(0).position, Quaternion.identity);
        Color color = gameObject.transform.GetChild(0).transform.GetChild(0).transform.GetChild(0).GetComponent<SpriteRenderer>().color;
        for (int i = 0; i < deathSprite.transform.childCount; i++)
        {
            deathSprite.transform.GetChild(i).GetComponent<SpriteRenderer>().color = color;
        }
    }
}
