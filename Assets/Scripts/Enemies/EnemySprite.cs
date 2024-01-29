using UnityEngine;

public class EnemySprite : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private CustomShaderManager customShaderManager;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        customShaderManager = FindObjectOfType<CustomShaderManager>();
        changeEnemyColor(new Color(0.83f, 0.15f, 0.15f));
    }

    public void resetSprite()
    {
        customShaderManager.setDefaultShader(gameObject, spriteRenderer);
    }

    public void circleSprite()
    {
        customShaderManager.setCircleMat(gameObject, spriteRenderer);
    }

    public void changeEnemyColor(Color color)
    {
        spriteRenderer.color = color;
    }
}
