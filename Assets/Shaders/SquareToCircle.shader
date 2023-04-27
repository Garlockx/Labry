Shader "Custom/SquareToCircleShader" {
    Properties{
        _Color("Color", Color) = (0,0,0,0)
        _Transparency("Transparency", Range(0,1)) = 1
        _TilingOffSet("TilingOffSet", Vector) = (0,0,1,1)
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

                float4 _Color;
                float _Transparency;
                float4 _TilingOffSet;

                v2f vert(appdata v) {
                    v2f o;
                    o.vertex = UnityObjectToClipPos(v.vertex);
                    o.uv = v.uv;
                    return o;
                }

                fixed4 frag(v2f i) : SV_Target{
                    float2 uv = i.uv;
                    float2 min = _TilingOffSet.xy;
                    float2 max = _TilingOffSet.zw;
                    float2 size = max - min;
                    uv = (uv - min) / size;
                    uv = uv * 2 - 1;
                    if (uv.x * uv.x + uv.y * uv.y > 0.8) {
                        _Color.w = 0;
                    }
                    return _Color;
                }
                ENDCG
            }
        }

            FallBack "Diffuse"
}
