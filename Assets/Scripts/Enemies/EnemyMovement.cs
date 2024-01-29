using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyMovement : MonoBehaviour
{
    public float speed;
    public float timeBetweenMove;
    public float minDistanceWithPlayer;
    public float maxDistanceWithPlayer;

    public Coroutine moveToCoroutine;

    private float timeSinceMove;
    private bool isMoving;
    private Vector3 lastVisitedTile;

    private EnemySprite enemySprite;
    private EnemyManager enemyManager;
    private TileMapManager tileMapManager;

    void Start()
    {
        timeSinceMove = 0.0f;
        isMoving = false;
        lastVisitedTile = new Vector3();
        tileMapManager = GameObject.FindGameObjectWithTag("Path").GetComponent<TileMapManager>();
        tileMapManager.setTileColor(transform.position, Color.white);
        enemySprite = FindObjectOfType<EnemySprite>();
        enemyManager = FindObjectOfType<EnemyManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!checkEnemyhasToMove())
        {
            return;
        }
        isMoving = true;
        enemySprite.circleSprite();
        moveToCoroutine = StartCoroutine(MoveTo(getFurthestPlayerTile()));
    }

    private bool checkEnemyhasToMove()
    {
        timeSinceMove += Time.deltaTime;
        if (GameObject.FindGameObjectWithTag("Player").transform.childCount == 0)
        {
            return false;
        }
        if (isMoving || !checkMaxDistance() || timeSinceMove < timeBetweenMove)
        {
            return false;
        } else
        {
            return true;
        }
    }

    private bool checkMaxDistance()
    {
        if (tileMapManager.getDistanceWithPlayer(transform.position) >= maxDistanceWithPlayer)
        {
            return false;
        }
        return true;
    }

    private Vector3 getFurthestPlayerTile()
    {
        List<TileMapManager.NearTile> nearestTiles = tileMapManager.getNearestTilesPositions(transform.position);
        Vector3 playerPos = GameObject.FindGameObjectWithTag("PlayerCollider").transform.position;
        List<TileMapManager.NearTile> possibleTiles = new List<TileMapManager.NearTile>();

        if (nearestTiles.Count == 1)
        {
            return nearestTiles[0].position;
        }

        for (int i = 0; i < nearestTiles.Count; i++)
        {
            if (nearestTiles[i].color != Color.white && nearestTiles[i].position != lastVisitedTile && nearestTiles[i].position != playerPos && nearestTiles[i].distanceWithPlayer > minDistanceWithPlayer)
            {
                possibleTiles.Add(nearestTiles[i]);
            }
        }


        if (possibleTiles.Count == 0)
        {
            nearestTiles.Sort((x, y) => y.distanceWithPlayer.CompareTo(x.distanceWithPlayer));
            if (nearestTiles.Count != 2)
            {
                if (nearestTiles[0].position != playerPos && nearestTiles[0].position != lastVisitedTile)
                {
                    possibleTiles.Add(nearestTiles[0]);
                }
                else
                {
                    possibleTiles.Add(nearestTiles[1]);
                }
            } else
            {
                possibleTiles.Add(nearestTiles[0]);
            }
            
        }

        possibleTiles.Sort((x, y) => y.distanceWithPlayer.CompareTo(x.distanceWithPlayer));

        return possibleTiles[0].position;
    }

    public void moveToOtherPosition(Vector3 newPosition)
    {
        StopCoroutine(moveToCoroutine);
        enemySprite.resetSprite();
        isMoving = false;
        transform.position = newPosition;
        transform.rotation = new Quaternion();
        tileMapManager.setTileColor(transform.position, Color.white);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "PlayerCollider")
        {
            enemyManager.enemyHit();
        }
    }

    IEnumerator MoveTo(Vector3 target)
    {
        lastVisitedTile = transform.position;
        while ((transform.position - target).sqrMagnitude != 0)
        {
            transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
            yield return null;
        }
        tileMapManager.setTileColor(target, Color.white);
        timeSinceMove = 0.0f;
        isMoving = false;
        enemySprite.resetSprite();
    }

}
