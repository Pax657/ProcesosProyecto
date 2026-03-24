Shader "Custom/Daltonismo"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _R ("Red Mix", Vector) = (1,0,0,0)
        _G ("Green Mix", Vector) = (0,1,0,0)
        _B ("Blue Mix", Vector) = (0,0,1,0)
    }

    SubShader
    {
        Tags { "RenderPipeline"="UniversalRenderPipeline" }

        Pass
        {
            Name "ColorBlindnessPass"
            ZTest Always Cull Off ZWrite Off

            HLSLPROGRAM
            #pragma vertex Vert
            #pragma fragment Frag

            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

            TEXTURE2D(_MainTex);
            SAMPLER(sampler_MainTex);

            float4 _R, _G, _B;

            struct Attributes
            {
                float4 positionOS : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct Varyings
            {
                float4 positionHCS : SV_POSITION;
                float2 uv : TEXCOORD0;
            };

            Varyings Vert (Attributes v)
            {
                Varyings o;
                o.positionHCS = TransformObjectToHClip(v.positionOS);
                o.uv = v.uv;
                return o;
            }

            half4 Frag (Varyings i) : SV_Target
            {
                half4 c = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, i.uv);

                return half4(
                    dot(c.rgb, _R.rgb),
                    dot(c.rgb, _G.rgb),
                    dot(c.rgb, _B.rgb),
                    c.a
                );
            }
            ENDHLSL
        }
    }
}
