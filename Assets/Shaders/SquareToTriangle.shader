Shader "Custom/SquareToTriangleShader" {
    Properties{
        _TilingOffSet("TilingOffSet", Vector) = (0,0,1,1)
        _Color("Color", Color) = (0,0,0,0)
        _VertexA("VertexA", Vector) = (0,0,0,0)
        _VertexB("VertexB", Vector) = (0,0,0,0)
        _VertexC("VertexC", Vector) = (0,0,0,0)
    }

        SubShader{
            Tags {"Queue" = "Transparent"}

            Pass {
                Blend SrcAlpha OneMinusSrcAlpha
                CGPROGRAM
                #pragma vertex vert
                #pragma fragment frag
                #include "UnityCG.cginc"

                struct appdata {
                    float4 vertex : POSITION;
                    float2 uv : TEXCOORD0;
                };

                struct v2f {
                    float2 uv : TEXCOORD0;
                    float4 vertex : SV_POSITION;
                };

                float4 _TilingOffSet;
                float4 _Color;
                float2 _VertexA;
                float2 _VertexB;
                float2 _VertexC;

                float2 vA;
                float2 vB;
                float2 vC;

                v2f vert(appdata v) {
                    v2f o;
                    o.vertex = UnityObjectToClipPos(v.vertex);
                    o.uv = v.uv;
                    return o;
                }

                fixed4 frag(v2f i) : SV_Target{
                    float2 uv = i.uv;
                    //resizing UV of my sprite
                    float2 min = _TilingOffSet.xy;
                    float2 max = _TilingOffSet.zw;
                    float2 size = max - min;
                    uv = (uv - min) / size;
                    
                    //Coordinates of first vertex of triangle
                    vA.x = _VertexA.x;
                    vA.y = _VertexA.y;
                    //Coordinates of second vertex of triangle
                    vB.x = _VertexB.x;
                    vB.y = _VertexB.y;
                    //Coordinates of third vertex of triangle
                    vC.x = _VertexC.x;
                    vC.y = _VertexC.y;

                    //Calculate barycentric coordinates
                    float w1 = ((vA.x * (vC.y - vA.y)) + ((uv.y - vA.y) * (vC.x - vA.x)) - (uv.x * (vC.y - vA.y))) / (((vB.y - vA.y) * (vC.x - vA.x)) - ((vB.x - vA.x) * (vC.y - vA.y)));
                    float w2 = (uv.y - vA.y - w1 * (vB.y - vA.y)) / (vC.y - vA.y);

                    // check if UV is in triangle
                    if (w1 < 0 || w2 < 0 || w1 + w2 > 1) {
                        _Color.w = 0;
                    }

                    return _Color;
                }
                ENDCG
            }
        }

            FallBack "Diffuse"
}
