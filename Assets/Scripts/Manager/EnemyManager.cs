using UnityEngine;
using System;

public class EnemyManager : MonoBehaviour
{
    public struct EnemyParameter
    {
        public float speed;
        public float timeBetweenMove;
        public float minDistanceWithPlayer;
        public float maxDistanceWithPlayer;
        public float animationDeathTime;
        public int life;

        public EnemyParameter(float speed, float timeBetweenMove, float minDistanceWithPlayer, float maxDistanceWithPlayer, float animationDeathTime, int life)
        {
            this.speed = speed;
            this.timeBetweenMove = timeBetweenMove;
            this.minDistanceWithPlayer = minDistanceWithPlayer;
            this.maxDistanceWithPlayer = maxDistanceWithPlayer;
            this.animationDeathTime = animationDeathTime;
            this.life = life;
        }
    }

    public float animationDeathTime = 2.0f;

    public event Action onEnemyHit;
    public event Action onEnemyDie;

    private int maxLife;
    private int currentLife;
    private bool isHit = false;
    private float timeSinceDead;
    private GameObject deathSprite;

    private void Update()
    {
        if (timeSinceDead >= animationDeathTime)
        {
            Destroy(deathSprite.gameObject);
            timeSinceDead = 0.0f;
            isHit = false;
            return;
        }
        if (isHit)
        {
            timeSinceDead += Time.deltaTime;
        }
    }

    public int getMaxLife()
    {
        return maxLife;
    }

    public int getCurrentLife()
    {
        return currentLife;
    }

    public void setParameters(EnemyParameter enemyParameter)
    {
        GetComponentInChildren<EnemyMovement>().speed = enemyParameter.speed;
        GetComponentInChildren<EnemyMovement>().timeBetweenMove = enemyParameter.timeBetweenMove;
        GetComponentInChildren<EnemyMovement>().minDistanceWithPlayer = enemyParameter.minDistanceWithPlayer;
        GetComponentInChildren<EnemyMovement>().maxDistanceWithPlayer = enemyParameter.maxDistanceWithPlayer;
        maxLife = enemyParameter.life;
        currentLife = enemyParameter.life;
        animationDeathTime = enemyParameter.animationDeathTime;
    }

    public void setSpeed(float speed)
    {
        GetComponentInChildren<EnemyMovement>().speed = speed;
    }

    public void enemyHit()
    {
        currentLife -= 1;
        enemyDeathAnimation();
        if (currentLife != 0)
        {
            onEnemyHit?.Invoke();
        } else
        {
            onEnemyDie?.Invoke();
            Destroy(gameObject.gameObject);
        }
    }

    private void enemyDeathAnimation()
    {
        deathSprite = Instantiate((GameObject)Resources.Load("Prefabs/DeathSprite", typeof(GameObject)), gameObject.transform.GetChild(0).position, Quaternion.identity);
        Color color = gameObject.transform.GetChild(0).transform.GetChild(0).transform.GetChild(0).GetComponent<SpriteRenderer>().color;
        for (int i = 0; i < deathSprite.transform.childCount; i++)
        {
            deathSprite.transform.GetChild(i).GetComponent<SpriteRenderer>().color = color;
        }
        isHit = true;
    }
}
