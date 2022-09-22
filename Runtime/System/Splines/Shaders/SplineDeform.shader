Shader "Nazio_LT/SplineDeform"
{
    Properties
    {
        [MainColor] _Color("Base Color", Color) = (1,1,1,1)
        _DeformationTexture("Texture", 2D) = "white" {}
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        HLSLINCLUDE
        #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

        CBUFFER_START(unityPerMaterial)
            float4 _Color;
        CBUFFER_END

        TEXTURE2D(_DeformationTexture);
        SAMPLER(sampler_DeformationTexture);

        struct VertInput
        {
            float4 position : POSITION;
        };

        struct VertOutput
        {
            float4 position : SV_POSITION;
        };

        ENDHLSL

        Pass 
        {
            HLSLPROGRAM

            #pragma vertex vert
            #pragma fragment fragment

            VertOutput vert(VertInput input)
            {
                VertOutput output;

                float4 variation = SAMPLE_TEXTURE2D(_DeformationTexture, sampler_DeformationTexture, (input.position.z,0));

                float4 newPos = input.position + variation;

                output.position = TransformObjectToHClip(newPos.xyz);
            }

            float4 frag(VertOutput input) : SV_TARGET
            {
                return _Color;
            }

            ENDHLSL
        }
    }
}
