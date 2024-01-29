using UnityEngine;
using System.Collections;
using System;

public class PlayerMovement : MonoBehaviour
{
    private float speed = 10.0f;
    // Flag to know if the player is moving
    private bool isMoving = false;
    // Trigger an action when player is moving
    public event Action onPlayerMove;

    public Coroutine moveToCoroutine;
    private PlayerSpriteManager playerSpriteManager;
    private PlayerManager playerManager;
    private Vector3 playerPositionBeforeMoving;

    private void Start()
    {
        playerSpriteManager = FindObjectOfType<PlayerSpriteManager>();
        playerManager = FindObjectOfType<PlayerManager>();
    }

    private void Update()
    {
        if (isMoving || !playerManager.getIsPlayerCanMove())
        {
            return;
        }
        playerPositionBeforeMoving = transform.position;
        if(Input.GetButtonDown("Fire1"))
        {
            Vector3 direction = calculateMoveDirection();
            playerSpriteManager.circleSprite();
            playerManager.playerCanDie();
            moveToCoroutine = StartCoroutine(MoveTo(transform.position + direction));
        } else if(Input.GetButtonDown("Fire2"))
        {
            playerManager.playerCannotDie();
            Vector3 direction = calculateMoveDirection();
            playerSpriteManager.triangleSprite(direction);
            moveToCoroutine = StartCoroutine(MoveTo(transform.position + direction * 2));
        }
    }

    public void moveToOtherPosition(Vector3 newPosition)
    {
        StopCoroutine(moveToCoroutine);
        playerSpriteManager.resetSprite();
        isMoving = false;
        playerManager.playerCanDie();
        transform.position = newPosition;
        transform.rotation = new Quaternion();
    }

    /*
     * Get the direction to where move player
     */
    private Vector3 calculateMoveDirection()
    {
        float angle = calculeAngle();
        Vector3 direction = new Vector3();
        if (angle >= 45 && angle <= 135)
        {
            direction.y += 1;
        } else if (angle <= -45 && angle >= -135)
        {
            direction.y -= 1;
        }
        else if (angle <= 45 && angle >= -45)
        {
            direction.x += 1;
        } else
        {
            direction.x -= 1;
        }

        return direction;
    }


    /*
     * Calcule angle between Player and Mouse
     */
    private float calculeAngle() {
        Vector3 mouse = Input.mousePosition;
        Vector3 screenPoint = Camera.main.WorldToScreenPoint(transform.position);
        Vector2 offset = new Vector2(mouse.x - screenPoint.x, mouse.y - screenPoint.y);
        float angle = Mathf.Atan2(offset.y, offset.x) * Mathf.Rad2Deg;
        return angle;
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Collider")
        {
            StopCoroutine(moveToCoroutine);
            StartCoroutine(BackToOldPosition());
        }
    }

    /* 
     *  Take the player to his old position if he hit a wall
     */
    IEnumerator BackToOldPosition()
    {
        isMoving = true;
        while ((transform.position - playerPositionBeforeMoving).sqrMagnitude != 0)
        {
            transform.position = Vector3.MoveTowards(transform.position, playerPositionBeforeMoving, speed * Time.deltaTime);
            yield return null;
        }
        isMoving = false;
        playerManager.playerCanDie();
        playerSpriteManager.resetSprite();
    }


    /*
     * Move The player to coordinates
     */
    IEnumerator MoveTo(Vector3 target)
    {
        isMoving = true;
        while ((transform.position - target).sqrMagnitude != 0)
        {
            transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
            yield return null;
        }
        isMoving = false;
        playerManager.playerCanDie();
        onPlayerMove?.Invoke();
        playerSpriteManager.resetSprite();
    }
}
