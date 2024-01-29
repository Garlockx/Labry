using UnityEngine;

public class CustomShaderManager : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private Color Color;

    public void setDefaultShader(GameObject gameObject, SpriteRenderer _spriteRenderer)
    {
        spriteRenderer = _spriteRenderer;
        Color = _spriteRenderer.color;
        Material defaultMat = new Material(Shader.Find("Sprites/Default"));
        gameObject.GetComponent<SpriteRenderer>().material = defaultMat;
    }

    public void setCircleMat(GameObject gameObject, SpriteRenderer _spriteRenderer)
    {
        spriteRenderer = _spriteRenderer;
        Color = _spriteRenderer.color;
        Material circleMaterial = (Material)Resources.Load("Material/CircleMat", typeof(Material));
        gameObject.GetComponent<SpriteRenderer>().material = circleMaterial;
        setTillingOffSet();
        setColor();
    }

    public void setTriangleMat(GameObject gameObject, SpriteRenderer _spriteRenderer, Vector3 direction)
    {
        spriteRenderer = _spriteRenderer;
        Color = _spriteRenderer.color;
        Material circleMaterial = (Material)Resources.Load("Material/TriangleMat", typeof(Material));
        gameObject.GetComponent<SpriteRenderer>().material = circleMaterial;
        setTillingOffSet();
        setTriangleOrientation(direction);
        setColor();
    }

    private void setColor()
    {
        spriteRenderer.material.SetColor("_Color", Color);
    }

    private void setTriangleOrientation(Vector3 direction)
    {
        Vector4 triangleVertexA;
        Vector4 triangleVertexB;
        Vector4 triangleVertexC;

        if (direction == new Vector3(-1,0,0))
        {
            triangleVertexA = new Vector4(1.0f, 0.0f, 0.0f, 0.0f);
            triangleVertexB = new Vector4(1.0f, 1.0f, 0.0f, 0.0f);
            triangleVertexC = new Vector4(0.0f, 0.5f, 0.0f, 0.0f);
        } else if (direction == new Vector3(1, 0, 0))
        {
            triangleVertexA = new Vector4(0.0f, 0.0f, 0.0f, 0.0f);
            triangleVertexB = new Vector4(0.0f, 1.0f, 0.0f, 0.0f);
            triangleVertexC = new Vector4(1.0f, 0.5f, 0.0f, 0.0f);

        } else if (direction == new Vector3(0, 1, 0))
        {
            triangleVertexA = new Vector4(0.5f, 1.0f, 0.0f, 0.0f);
            triangleVertexB = new Vector4(0.0f, 0.0f, 0.0f, 0.0f);
            triangleVertexC = new Vector4(1.0f, 0.0f, 0.0f, 0.0f);

        } else
        {
            triangleVertexA = new Vector4(0.5f, 0.0f, 0.0f, 0.0f);
            triangleVertexB = new Vector4(1.0f, 1.0f, 0.0f, 0.0f);
            triangleVertexC = new Vector4(0.0f, 1.0f, 0.0f, 0.0f);
        }

        spriteRenderer.material.SetVector("_VertexA", triangleVertexA);
        spriteRenderer.material.SetVector("_VertexB", triangleVertexC);
        spriteRenderer.material.SetVector("_VertexC", triangleVertexB);
    }

    private void setTillingOffSet()
    {
        Rect textureRect = spriteRenderer.sprite.textureRect;
        Texture texture = spriteRenderer.sprite.texture;
        Vector4 v4 = new Vector4(textureRect.xMin / texture.width, textureRect.yMin / texture.height, textureRect.xMax / texture.width, textureRect.yMax / texture.height);
        spriteRenderer.material.SetVector("_TilingOffSet", v4);
    }
}
