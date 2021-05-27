Shader "LightBeam"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _MainTex ("Texture", 2D) = "white" {}
        _GlowSpeed("Glow speed", Float) = 1.0
        _GlowStrength("Glow strength", Float) = 1.0
        _GlowBrightness("Glow brightness", Float) = 1.0
        _PannerSpeed("Panner speed", Float) = 0.0
    }
    SubShader
    {
        Tags { "Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent" }
        ZWrite Off
        Blend SrcAlpha One

        Pass{
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            sampler2D _MainTex;
            float4 _MainTex_ST;
            fixed4 _Color;
            float _GlowSpeed;
            float _GlowStrength;
            float _GlowBrightness;
            float _PannerSpeed;

            struct v2f
            {
                float4 pos : SV_POSITION;
                float4 uv : TEXCOORD0;
            };

            v2f vert(appdata_img v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.uv.xy = TRANSFORM_TEX(v.texcoord, _MainTex);
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                float2 uv = i.uv.xy;
                uv.y += _Time * _MainTex_ST.z * _PannerSpeed;
                fixed brightness_mask = (tex2D(_MainTex, i.uv.xy).a) * _Color.a * (_GlowStrength*sin(_GlowSpeed * _Time) + _GlowStrength + _GlowBrightness);
                return fixed4(_Color.rgb, brightness_mask);
            }
            ENDCG
        }
    }
}
