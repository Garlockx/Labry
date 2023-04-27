using UnityEngine;
using System.Collections;
using System;

public class PlayerMovement : MonoBehaviour
{
    private float speed = 10.0f;
    /* Flag to know if the player is moving */
    private bool isMoving = false;
    /* Trigger an action when player is moving */
    public event Action onPlayerMove;

    private Coroutine moveToCoroutine;
    private PlayerSpriteManager playerSpriteManager;
    private Vector3 playerPositionBeforeMoving;

    private void Start()
    {
        playerSpriteManager = FindObjectOfType<PlayerSpriteManager>();
    }

    void Update()
    {
        if (isMoving)
        {
            return;
        }
        playerPositionBeforeMoving = transform.position;
        if(Input.GetButtonDown("Fire1"))
        {
            Vector3 direction = calculateMoveDirection();
            playerSpriteManager.circleSprite();
            moveToCoroutine = StartCoroutine(MoveTo(transform.position + direction));
        }
        if(Input.GetButtonDown("Fire2"))
        {
            Vector3 direction = calculateMoveDirection();
            playerSpriteManager.triangleSprite(direction);
            moveToCoroutine = StartCoroutine(MoveTo(transform.position + direction * 2));
        }
    }

    private Vector3 calculateMoveDirection()
    {
        Vector3 mouse = Input.mousePosition;
        Vector3 screenPoint = Camera.main.WorldToScreenPoint(transform.position);
        Vector2 offset = new Vector2(mouse.x - screenPoint.x, mouse.y - screenPoint.y);
        float angle = Mathf.Atan2(offset.y, offset.x) * Mathf.Rad2Deg;
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        StopCoroutine(moveToCoroutine);
        StartCoroutine(BackToOldPosition());
    }

    IEnumerator BackToOldPosition()
    {
        isMoving = true;
        while ((transform.position - playerPositionBeforeMoving).sqrMagnitude != 0)
        {
            transform.position = Vector3.MoveTowards(transform.position, playerPositionBeforeMoving, speed * Time.deltaTime);
            yield return null;
        }
        isMoving = false;
        playerSpriteManager.resetSprite();
    }

    IEnumerator MoveTo(Vector3 target)
    {
        isMoving = true;
        while ((transform.position - target).sqrMagnitude != 0)
        {
            transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
            yield return null;
        }
        isMoving = false;
        onPlayerMove?.Invoke();
        playerSpriteManager.resetSprite();
    }
}
