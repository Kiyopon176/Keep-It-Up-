Shader"Custom/BlurShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _BlurSize ("Blur Size", Range(0.0, 10.0)) = 1.0
    }

    SubShader
    {
        Tags { "RenderType"="Opaque" }
        Pass
        {
            CGPROGRAM
            #pragma vertex vert_img
            #pragma fragment frag
#include "UnityCG.cginc"

uniform sampler2D _MainTex;
uniform float _BlurSize;

struct v2f
{
    float4 pos : SV_POSITION;
    float2 uv : TEXCOORD0;
};

v2f vert_img(appdata_full v)
{
    v2f o;
    o.pos = UnityObjectToClipPos(v.vertex);
    o.uv = v.texcoord;
    return o;
}

float4 frag(v2f i) : SV_TARGET
{
    float4 color = float4(0, 0, 0, 0);
    float blurSize = _BlurSize;

                // Sample the texture multiple times and accumulate the color
    for (int x = -4; x <= 4; x++)
    {
        for (int y = -4; y <= 4; y++)
        {
            float2 offset = float2(x, y) * blurSize;
            color += tex2D(_MainTex, i.uv + offset);
        }
    }

                // Normalize the color
    color /= 81.0;

    return color;
}
            ENDCG
        }
    }
}
