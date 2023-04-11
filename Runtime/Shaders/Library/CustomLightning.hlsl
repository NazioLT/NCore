#ifndef NCORE_LIGHTNING
#define NCORE_LIGHTNING

#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"

#define SKYBOX_COLOR (0.25,0.25,0.25,0.25)

float GetShadow_float(float3 normalWS, Light light)
{
    float3 lightDir = SafeNormalize(light.direction);
    
    float NdotL = dot(normalWS, lightDir);
    float NdotLClamped = max(0, NdotL);
    float shadow = NdotLClamped * light.shadowAttenuation * light.distanceAttenuation;

    return shadow;
}

float3 GetDiffuse_float3(float3 normalWS, Light light, out float shadow)
{
    shadow = GetShadow_float(normalWS, light);
    return shadow * light.color;
}

float3 GetSpecular_float3(Light light, float3 viewDirWS, float3 normalWS, float shadow, float specStrengh)
{
    float3 reflectDir = reflect(-light.direction, normalWS);
    float spec = pow(max(dot(viewDirWS, reflectDir), 0), 32);
    float3 specular = shadow * specStrengh * spec * light.color;

    return  specular;
}

#endif