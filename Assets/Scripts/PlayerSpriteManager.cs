using UnityEngine;

public class PlayerSpriteManager : MonoBehaviour
{
    private CustomShaderManager customShaderManager;

    private void Start()
    {
        customShaderManager = FindObjectOfType<CustomShaderManager>();
    }

    public void resetSprite()
    {
        customShaderManager.setDefaultShader(gameObject);
    }

    public void circleSprite()
    {
        customShaderManager.setCircleMat(gameObject);
    }

    public void triangleSprite(Vector3 _direction)
    {
        customShaderManager.setTriangleMat(gameObject, _direction);
    }

}
