Shader "Custom/WaterShader"
{
    Properties
    {
        _Color ("Color", Color) = (1, 1, 1, 1)
        _WaveScale ("Wave scale", Range(0.01, 0.2)) = 0.1
        _Speed ("Speed", Range(0.0, 3.0)) = 1.0
        _TextureSize ("Texture Size", Range(128, 512)) = 256
    }
    
    SubShader
    {
        Tags { "Queue"="Transparent" "RenderType"="Transparent" }
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            float4 _Color;
            float _WaveScale;
            float _Speed;
            float _TextureSize;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                fixed2 offset = _WaveScale * sin(_Time.y * _Speed + i.uv * 2.0 * 3.141592);
                fixed2 pos = i.uv * _TextureSize;
                fixed3 normalColor = fixed3(frac(pos.x) - 0.5, frac(pos.y) - 0.5, 0);
                normalColor = normalize(normalColor);
                fixed3 lightDir = normalize(fixed3(0.3, 0.7, 0.5));
                fixed3 normal = normalize((normalColor - 0.5) * 2.0);
                fixed3 reflection = reflect(-lightDir, normal);
                fixed fresnel = dot(normal, lightDir);
                fresnel = pow(1.0 - fresnel, 2.0);
                fresnel *= fresnel;
                fixed3 finalColor = fresnel * _Color.rgb * normalColor;
                return fixed4(finalColor, _Color.a);
            }
            ENDCG
        }
    }
}