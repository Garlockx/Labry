using UnityEngine;

public class PlayerSpriteManager : MonoBehaviour
{
    private CustomShaderManager customShaderManager;

    private void Start()
    {
        customShaderManager = FindObjectOfType<CustomShaderManager>();
        changePlayerColor(new Color(0.41f, 0.41f, 0.41f));
    }

    public void resetSprite()
    {
        customShaderManager.setDefaultShader(gameObject);
    }

    public void circleSprite()
    {
        customShaderManager.setCircleMat(gameObject);
    }

    public void triangleSprite(Vector3 direction)
    {
        customShaderManager.setTriangleMat(gameObject, direction);
    }

    public void changePlayerColor(Color color)
    {
        gameObject.GetComponent<SpriteRenderer>().color = color;
    }

}
