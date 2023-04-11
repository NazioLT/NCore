Shader "Nazio_LT/NLit"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Color ("Albedo", Color) = (1,1,1,1)
        _SpecularStrength ("Specular Strength", Float) = 0.5
    }
    SubShader
    {
        Tags { "RenderType"="Opaque"}
        LOD 100
        
        ZTest On
        ZTest LEqual
        
        HLSLINCLUDE

        #include "../Library/CustomLightning.hlsl"
        #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Common.hlsl"
        #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

        float4 _Color;
        float _SpecularStrength;

        TEXTURE2D(_MainTex);
        SAMPLER(sampler_MainTex);
        float4 _MainTex_ST;
        
        ENDHLSL

        Pass
        {
            HLSLPROGRAM
            
            #pragma vertex vert
            #pragma fragment frag

            #if UNITY_VERSION >= 202120

            #pragma multi_compile _ _MAIN_LIGHT_SHADOWS _MAIN_LIGHT_SHADOWS_CASCADE       
            
            #else
            
            #pragma multi_compile _ _MAIN_LIGHT_SHADOWS            
            #pragma multi_compile _ _MAIN_LIGHT_SHADOWS_CASCADE
            
            #endif

            #pragma multi_compile_fragment _ _SHADOWS_SOFT

            struct VertexInputs
            {
                float4 vertexOS : POSITION;
                float2 uv : TEXCOORD0;
                float3 normalOS : NORMAL;
            };

            struct VertexOutput
            {
                float2 uv : TEXCOORD0;
                float4 vertexCS : SV_POSITION;
                float3 normalWS : TEXCOORD1;
                float3 viewDirWS : TEXCOORD2;
                float4 shadowCoord : TEXCOORD3;
            };

            VertexOutput vert (VertexInputs IN)
            {
                float3 vertexWS = TransformObjectToWorld(IN.vertexOS);   
                
                VertexOutput OUT;
                OUT.vertexCS = TransformObjectToHClip(IN.vertexOS);
                OUT.uv = TRANSFORM_TEX(IN.uv, _MainTex);
                OUT.normalWS = SafeNormalize(TransformObjectToWorldNormal(IN.normalOS));
                OUT.viewDirWS = normalize(GetCameraPositionWS() - vertexWS);

                VertexPositionInputs positionInputs = GetVertexPositionInputs(IN.vertexOS.xyz);
                OUT.shadowCoord = GetShadowCoord(positionInputs);
                return OUT;
            }

            half3 frag (VertexOutput IN) : SV_Target
            {
                float shadow;
                
                Light mainLight = GetMainLight(IN.shadowCoord);

                float3 unlitColor = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, IN.uv) * _Color;
                float3 diffuse = GetDiffuse_float3(IN.normalWS, mainLight, shadow);
                float3 specular = GetSpecular_float3(mainLight, IN.viewDirWS, IN.normalWS, shadow, _SpecularStrength);
                
                return (diffuse + SKYBOX_COLOR + specular) * unlitColor;
            }
            
            ENDHLSL
        }
        
        UsePass "Universal Render Pipeline/Lit/ShadowCaster"
        UsePass "Universal Render Pipeline/Lit/DepthOnly"
    }
}