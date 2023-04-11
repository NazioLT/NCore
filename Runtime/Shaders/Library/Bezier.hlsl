#ifndef NCORE_BEZIER
#define NCORE_BEZIER

float3 ComputeBezier(float3 p0, float3 p1, float3 p2, float3 p3, float t)
{
    float tSquare = t * t;
    float tCube = t * tSquare;

    float3 p0P = (-tCube + 3 * tSquare - 3 * t + 1) * p0;
    float3 p1P = (3 * tCube - 6 * tSquare + 3 * t) * p1;
    float3 p2P = (-3 * tCube + 3 * tSquare) * p2;
    float3 p3P = tCube * p3;

    return p0P + p1P + p2P + p3P;
}

float3 ComputeBezierDerivative(float3 p0, float3 p1, float3 p2, float3 p3, float t)
{
    float tSquare = t * t;

    float3 p0P = (-3 * tSquare + 6 * t - 3) * p0;
    float3 p1P = (9 * tSquare - 12 * t + 3) * p1;
    float3 p2P = (-9 * tSquare + 6 * t) * p2;
    float3 p3P = 3 * tSquare * p3;

    return p0P + p1P + p2P + p3P;
}
#endif