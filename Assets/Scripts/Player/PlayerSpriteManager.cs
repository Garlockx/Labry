using UnityEngine;

public class PlayerSpriteManager : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private CustomShaderManager customShaderManager;

    private void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        customShaderManager = FindObjectOfType<CustomShaderManager>();
        changePlayerColor(new Color(0.41f, 0.41f, 0.41f));
    }

    public void resetSprite()
    {
        customShaderManager.setDefaultShader(gameObject, spriteRenderer);
    }

    public void circleSprite()
    {
        customShaderManager.setCircleMat(gameObject, spriteRenderer);
    }

    public void triangleSprite(Vector3 direction)
    {
        customShaderManager.setTriangleMat(gameObject, spriteRenderer, direction);
    }

    public void changePlayerColor(Color color)
    {
        spriteRenderer.color = color;
    }

}
