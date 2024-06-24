Shader "URP/BlowingAnimeGrass"
{
    Properties
    {
        _Color("Color", Color) = (1,1,1,1)
        _MainTex("Albedo (RGB)", 2D) = "white" {}
        _Glossiness("Smoothness", Range(0,1)) = 0.5
        _Metallic("Metallic", Range(0,1)) = 0.0
        _GrassTex("Grass Texture", 2D) = "white" {}
        _GrassSpeed("Grass Speed", Range(-10, 10.0)) = 1
        _GrassScale("Grass Scale", Range(0,1)) = 0.4
        _Grass2Scale("Second Layer Grass Scale", Range(0, 2)) = 1
        _GrassBlendSpeed("Grass Blend Speed", Range(0, 1)) = .5
        _WindTex("Distortion Texture", 2D) = "white" {}
        _WindSpeed("Wind Speed", Range(-10, 10.0)) = 1
        _WindScale("Wind Noise Scale", Range(0,1)) = 0.4
        _Wind2Scale("Second Layer Wind Scale", Range(0, 2)) = 1
        _WindBlendSpeed("Wind Blend Speed", Range(0, 1)) = .5
    }

    SubShader
    {
        Tags { "RenderType"="Transparent" "Queue"="Transparent" }

        Pass
        {
            Name "ForwardLit"
            Tags { "LightMode" = "UniversalForward" }
            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"

            struct Attributes
            {
                float4 positionOS : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct Varyings
            {
                float4 positionHCS : SV_POSITION;
                float2 uv : TEXCOORD0;
                float3 worldPos : TEXCOORD1;
            };

            TEXTURE2D(_MainTex);
            SAMPLER(sampler_MainTex);
            TEXTURE2D(_GrassTex);
            SAMPLER(sampler_GrassTex);
            TEXTURE2D(_WindTex);
            SAMPLER(sampler_WindTex);

            float _Glossiness;
            float _Metallic;
            float4 _Color;
            float _GrassScale;
            float _WindScale;
            float _GrassSpeed;
            float _Grass2Scale;
            float _GrassBlendSpeed;
            float _WindBlendSpeed;
            float _WindSpeed;
            float _Wind2Scale;

            Varyings vert(Attributes input)
            {
                Varyings output;
                output.positionHCS = TransformObjectToHClip(input.positionOS);
                output.uv = input.uv;
                output.worldPos = TransformObjectToWorld(input.positionOS);
                return output;
            }

            half4 frag(Varyings input) : SV_Target
            {
                // blend timings for grass
                float grasstiming = frac(_Time.y * _GrassBlendSpeed + 0.5f);
                float grasstiming2 = frac(_Time.y * _GrassBlendSpeed);
                float grasstimingLerp = abs((0.5f - grasstiming) / 0.5f);

                float grassScrollSpeed = _Time.x * _GrassSpeed;
                // move 2 Grass textures at different speeds
                half4 grassTex1 = SAMPLE_TEXTURE2D(_GrassTex, sampler_GrassTex, input.worldPos.xz * _GrassScale / 50 + grassScrollSpeed);
                // same noise, different scale
                half4 grassTex2 = SAMPLE_TEXTURE2D(_GrassTex, sampler_GrassTex, input.worldPos.xz * (_GrassScale / 50 * _Grass2Scale) + grassScrollSpeed * _Grass2Scale);

                // blend timings for wind
                float windtiming = frac(_Time.y * 0.5f + 0.5f);
                float windtiming2 = frac(_Time.y * 0.5f);
                float windtimingLerp = abs((0.5f - windtiming) / 0.5f);

                float windScrollSpeed = _Time.x * _WindSpeed;
                // move 2 Wind textures at different speeds
                half4 windTex1 = SAMPLE_TEXTURE2D(_WindTex, sampler_WindTex, input.worldPos.xz * _WindScale / 50 + windScrollSpeed);
                // same noise, different scale
                half4 windTex2 = SAMPLE_TEXTURE2D(_WindTex, sampler_WindTex, input.worldPos.xz * (_WindScale / 50 * _Wind2Scale) + windScrollSpeed * _Wind2Scale);

                //Wind motion
                half4 windNoiseTexture = lerp(windTex1, windTex2, windtimingLerp);

                //Grass motion
                half4 grassTexture = lerp(grassTex1, grassTex2, grasstimingLerp);

                //Combine wind and grass
                half4 grassWind = grassTexture * windNoiseTexture;

                // Albedo comes from a texture tinted by color
                half4 c = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, input.uv) * _Color;
                half3 albedo = grassWind.a + c.rgb;

                // Output color
                half4 color = half4(albedo, c.a);

                return color;
            }
            ENDHLSL
        }
    }
    FallBack "Diffuse"
}
