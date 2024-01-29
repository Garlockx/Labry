using UnityEngine;
using System.Collections;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class TrapOne : MonoBehaviour
{
    //define the length in tile of the trap
    public int trapLength = 1;
    public float trapSpeed = 1.0f;

    // Set to true if the trap move in the level
    public bool trapIsMovable = false;

    // Position of trap at the start of the level
    [HideInInspector]
    public Vector3 startPosition;
    // Define the position where the trap goes first
    [HideInInspector]
    public Vector3 firstPosition;
    // Define the position where the trap go after hit the first position
    [HideInInspector]
    public Vector3 secondPosition;

    void Start()
    {
        changeSizeCollider();
        setTrapLength();
        if (trapIsMovable == true)
        {
            setTrapPosition();
            StartCoroutine(MoveTrap());
        }
    }

    private void setTrapPosition()
    {
        gameObject.transform.position = startPosition;
    }

    private void setTrapLength()
    {
        Object[] allTrapSprites = Resources.LoadAll("TrapSprites");
        if (trapLength == 1)
        {
            addSprite(new Vector3(2, 0, 0), (Sprite)allTrapSprites[3]);
            return;
        }
        for (int i = 0; i < trapLength - 1; i++)
        {
            addSprite(new Vector3(i + 2, 0, 0), (Sprite)allTrapSprites[2]);
        }
        addSprite(new Vector3(trapLength + 1, 0, 0), (Sprite)allTrapSprites[3]);
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

    private void addSprite(Vector3 position, Sprite sprite)
    {
        GameObject newSprite = Instantiate(new GameObject(), gameObject.transform);
        newSprite.transform.localPosition = position;
        SpriteRenderer newSpriteRenderer = newSprite.AddComponent<SpriteRenderer>();
        newSpriteRenderer.sprite = sprite;
        newSpriteRenderer.sortingOrder = 3;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "PlayerCollider")
        {
            collision.gameObject.transform.parent.gameObject.GetComponent<PlayerManager>().playerDeath();
        }
    }

    IEnumerator MoveTrap()
    {
        bool goTofirstPosition = true;
        while (trapIsMovable)
        {
            if (goTofirstPosition)
            {
                transform.position = Vector3.MoveTowards(transform.position, firstPosition, trapSpeed * Time.deltaTime);
                if ((transform.position - firstPosition).sqrMagnitude == 0)
                {
                    goTofirstPosition = false;
                }
            } else
            {
                transform.position = Vector3.MoveTowards(transform.position, secondPosition, trapSpeed * Time.deltaTime);
                if ((transform.position - secondPosition).sqrMagnitude == 0)
                {
                    goTofirstPosition = true;
                }
            }
            yield return null;
        }
    }
}



#if UNITY_EDITOR
[CustomEditor(typeof(TrapOne))]
class TrapOneEditor : Editor
{
    SerializedProperty trapIsMovable;
    SerializedProperty startPosition;
    SerializedProperty firstPosition;
    SerializedProperty secondPosition;

    public void OnEnable()
    {
        trapIsMovable = serializedObject.FindProperty(nameof(TrapOne.trapIsMovable));
        startPosition = serializedObject.FindProperty(nameof(TrapOne.startPosition));
        firstPosition = serializedObject.FindProperty(nameof(TrapOne.firstPosition));
        secondPosition = serializedObject.FindProperty(nameof(TrapOne.secondPosition));
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        DrawDefaultInspector();

        if (trapIsMovable.boolValue)
        {
            EditorGUILayout.PropertyField(startPosition); 
            EditorGUILayout.PropertyField(firstPosition);
            EditorGUILayout.PropertyField(secondPosition);
        }

        serializedObject.ApplyModifiedProperties();
    }
}
#endif