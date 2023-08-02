using UnityEngine;

public class CustomShaderManager : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer spriteRenderer;
    private Color PlayerColor;

    private void Start()
    {
        PlayerColor = spriteRenderer.color;
    }

    public void setDefaultShader(GameObject _gameObject)
    {
        Material defaultMat = new Material(Shader.Find("Sprites/Default"));
        _gameObject.GetComponent<SpriteRenderer>().material = defaultMat;
    }

    public void setCircleMat(GameObject _gameObject)
    {
        Material circleMaterial = (Material)Resources.Load("Material/CircleMat", typeof(Material));
        _gameObject.GetComponent<SpriteRenderer>().material = circleMaterial;
        setTillingOffSet();
        setColor();
    }

    public void setTriangleMat(GameObject _gameObject, Vector3 _playerDirection)
    {
        Material circleMaterial = (Material)Resources.Load("Material/TriangleMat", typeof(Material));
        _gameObject.GetComponent<SpriteRenderer>().material = circleMaterial;
        setTillingOffSet();
        setTriangleOrientation(_playerDirection);
        setColor();
    }

    private void setColor()
    {
        spriteRenderer.material.SetColor("_Color", PlayerColor);
    }

    private void setTriangleOrientation(Vector3 _playerDirection)
    {
        Vector4 triangleVertexA;
        Vector4 triangleVertexB;
        Vector4 triangleVertexC;

        if (_playerDirection == new Vector3(-1,0,0))
        {
            triangleVertexA = new Vector4(1.0f, 0.0f, 0.0f, 0.0f);
            triangleVertexB = new Vector4(1.0f, 1.0f, 0.0f, 0.0f);
            triangleVertexC = new Vector4(0.0f, 0.5f, 0.0f, 0.0f);
        } else if (_playerDirection == new Vector3(1, 0, 0))
        {
            triangleVertexA = new Vector4(0.0f, 0.0f, 0.0f, 0.0f);
            triangleVertexB = new Vector4(0.0f, 1.0f, 0.0f, 0.0f);
            triangleVertexC = new Vector4(1.0f, 0.5f, 0.0f, 0.0f);

        } else if (_playerDirection == new Vector3(0, 1, 0))
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
