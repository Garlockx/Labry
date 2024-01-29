using UnityEngine;
using System.Collections;
using System;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class TrapTwo : MonoBehaviour
{
    //define the length in tile of the trap
    public int trapLength = 1;
    // trapSpeed must be greater than fadeInDelay  + fadeTime
    public float trapSpeed = 5.0f;
    // fadeTime can't be < 1.0f
    public float fadeTime = 1.0f;
    // fadeInDelay can't be < 1.0f
    public float fadeInDelay = 2.0f;

    // Position of trap at the start of the level
    public Vector3 startPosition;
    // Define the position where the trap goes first
    public Vector3 firstPosition;
    // Define the position where the trap go after hit the first position
    public Vector3 secondPosition;

    private float inGameTime;
    private bool goToFirstPos;
    private bool trapHasToMove;
    private bool isTrapActive;

    void Start()
    {
        inGameTime = 0.0f;
        goToFirstPos = true;
        trapHasToMove = false;
        isTrapActive = true;

        changeSizeCollider();
        setTrapLength();
        setTrapPosition();
        setArrowDirection();
    }

    private void Update()
    {
        checkTrapHasToMove();
        checkFade();
    }

    private void checkTrapHasToMove()
    {
        if(!trapHasToMove)
        {
            return;
        }
        Vector3 direction = directionToGo();
        moveTrap(direction);
        fadeTrap(1.0f);
        trapHasToMove = false;
    }

    private void checkFade()
    {
        inGameTime += Time.deltaTime;
        if (inGameTime < trapSpeed)
        {
            return;
        }
        fadeTrap(0.0f);
        inGameTime = 0.0f;
    }

    private void fadeTrap(float alphaColor)
    {
        foreach (SpriteRenderer sprite in gameObject.transform.GetComponentsInChildren<SpriteRenderer>())
        {
            StartCoroutine(ChangeAlphaColor(alphaColor, sprite));
        }
    }

    private Vector3 directionToGo()
    {
        Vector3 checkdirection = startPosition - firstPosition;
        if (checkdirection.x != 0.0f)
        {
            if (startPosition.x < firstPosition.x)
            {
                return new Vector3(1.0f, 0.0f, 0.0f);
            }
            else if (startPosition.x > firstPosition.x)
            {
                return new Vector3(-1.0f, 0.0f, 0.0f);
            }
        }
        else if (checkdirection.y != 0.0f)
        {
            if (startPosition.y < firstPosition.y)
            {
                return new Vector3(0.0f, 1.0f, 0.0f);
            }
            else if (startPosition.y > firstPosition.y)
            {
                return new Vector3(0.0f, -1.0f, 0.0f);
            }
        }
        return new Vector3();
    }

    private void moveTrap(Vector3 moveAxis)
    {
        
        if (goToFirstPos)
        {
            gameObject.transform.position += moveAxis;
            if (gameObject.transform.position == firstPosition)
            {
                changeArrowDirection();
                goToFirstPos = false;
            }
        }
        else
        {
            gameObject.transform.position -= moveAxis;
            if (gameObject.transform.position == secondPosition)
            {
                changeArrowDirection();
                goToFirstPos = true;
            }
        }
    }

    private void setTrapPosition()
    {
        gameObject.transform.position = startPosition;
    }

    private void setTrapLength()
    {
        UnityEngine.Object[] allTrapSprites = Resources.LoadAll("TrapSprites");
        if (trapLength == 1)
        {
            addSprite(new Vector3(2, 0, 0), (Sprite)allTrapSprites[7]);
            return;
        }
        for (int i = 0; i < trapLength - 1; i++)
        {
            addSprite(new Vector3(i + 2, 0, 0), (Sprite)allTrapSprites[6]);
        }
        addSprite(new Vector3(trapLength + 1, 0, 0), (Sprite)allTrapSprites[7]);
    }

    private void setArrowDirection()
    {
        Vector3 trapDirection = directionToGo();
        if (trapDirection.x == -1.0f || trapDirection.y == -1.0f)
        {
            changeArrowDirection();
        }
    }

    private void changeArrowDirection()
    {
        gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().flipY = !gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().flipY;
        Vector3 pos = gameObject.transform.GetChild(0).transform.localPosition;
        pos.y = -pos.y;
        gameObject.transform.GetChild(0).transform.localPosition = pos;
    }

    private void changeSizeCollider()
    {
        Vector2 colliderSize = gameObject.GetComponent<BoxCollider2D>().size;
        colliderSize.x = trapLength;
        gameObject.GetComponent<BoxCollider2D>().size = colliderSize;

        Vector2 colliderOffset = gameObject.GetComponent<BoxCollider2D>().offset;
        colliderOffset.x = trapLength / 2 + 1.0f;
        gameObject.GetComponent<BoxCollider2D>().offset = colliderOffset;
    }

    private GameObject addSprite(Vector3 position, Sprite sprite)
    {
        GameObject newSprite = Instantiate(new GameObject(), gameObject.transform);
        newSprite.transform.localPosition = position;
        SpriteRenderer newSpriteRenderer = newSprite.AddComponent<SpriteRenderer>();
        newSpriteRenderer.sprite = sprite;
        newSpriteRenderer.sortingOrder = 3;
        return newSprite;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "PlayerCollider")
        {
            gameObject.GetComponent<Rigidbody2D>().WakeUp();
        }
        if (!isTrapActive)
        {
            return;
        }
        if (collision.gameObject.tag == "PlayerCollider")
        {
            collision.gameObject.transform.parent.gameObject.GetComponent<PlayerManager>().playerDeath();
        }
    }


    IEnumerator ChangeAlphaColor(float alphaColor, SpriteRenderer sprite)
    {
        float alphaAtStart = sprite.color.a;
        if (alphaColor == 1.0f)
        {
            yield return new WaitForSeconds(fadeInDelay);
        }
        for (float i = 0; i < fadeTime; i += Time.deltaTime)
        {
            sprite.color = new Color(1, 1, 1, Mathf.Lerp(alphaAtStart, alphaColor, i));
            if(alphaColor == 1.0f && sprite.color.a > 0.2f)
            {
                isTrapActive = true;
            }
            if (alphaColor == 0.0f && sprite.color.a < 0.2f)
            {
                isTrapActive = false;
            }
            yield return null;
        }
        if (alphaColor != 1.0f)
        {
            trapHasToMove = true;
        }
    }
}