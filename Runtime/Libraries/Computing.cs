using UnityEngine;

namespace Nazio_LT.Tools.Core
{
    /// <summary>Library for compute shaders.</summary>
    public static class Computing
    {
        /// <summary>Automaticaly dispatch on the thread of the kernel in a compute shader.</summary>
        public static void AutoDispatch(ComputeShader computeShader, int iterationNumberX, int iterationNumberY = 1, int iterationNumberZ = 1, int kernelID = 0)
        {
            Vector3Int threadGroupSizes = GetThreadGroupSizes(computeShader, kernelID);
            Vector3Int numGroups = new Vector3Int(
                Mathf.CeilToInt(iterationNumberX / (float)threadGroupSizes.x),
                Mathf.CeilToInt(iterationNumberY / (float)threadGroupSizes.y),
                Mathf.CeilToInt(iterationNumberZ / (float)threadGroupSizes.y)
            );
            computeShader.Dispatch(kernelID, numGroups.x, numGroups.y, numGroups.z);
        }

        public static Vector3Int GetThreadGroupSizes(ComputeShader compute, int kernelIndex = 0)
        {
            compute.GetKernelThreadGroupSizes(kernelIndex, out uint x, out uint y, out uint z);
            return new Vector3Int((int)x, (int)y, (int)z);
        }
    }
}