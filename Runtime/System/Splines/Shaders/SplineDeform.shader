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
            float2 uv : TEXCOORD0;
        };

        struct VertOutput
        {
            float4 position : SV_POSITION;
            float2 uv : TEXCOORD0;
        };

        ENDHLSL

        Pass 
        {
            HLSLPROGRAM

            #pragma vertex vert
            #pragma fragment frag
            #pragma target 4.5

            float4 GetColor(float2 uv)
            {
                // float scale = exp2(2);
                return tex2Dgrad(sampler_DeformationTexture, uv, 1, 1);
            }

            VertOutput vert(VertInput input)
            {
                VertOutput output;

                float2 deformUV = input.position.zy  * 10;

                output.uv = input.position.zy  * 10;

                float4 pos = input.position;
                float4 deform = SAMPLE_TEXTURE2D(_DeformationTexture, sampler_DeformationTexture, deformUV);

                output.position = TransformObjectToHClip(pos.xyz + deform.xyz);

                return output;
            }

            float4 frag(VertOutput input) : SV_TARGET
            {
                return SAMPLE_TEXTURE2D(_DeformationTexture, sampler_DeformationTexture, input.uv);
            }

            ENDHLSL
        }
    }
}
