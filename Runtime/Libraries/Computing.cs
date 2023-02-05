using UnityEngine;

namespace Nazio_LT.Tools.Core
{
    /// <summary>Library for compute shaders.</summary>
    public static class Computing
    {
        /// <summary>Automaticaly dispatch on the thread of the kernel in a compute shader.</summary>
        public static void AutoDispatch(ComputeShader _computeShader, int _IterationNumberX, int _IterationNumberY = 1, int _IterationNumberZ = 1, int _kernelID = 0)
        {
            Vector3Int threadGroupSizes = GetThreadGroupSizes(_computeShader, _kernelID);
            Vector3Int _numGroups = new Vector3Int(
                Mathf.CeilToInt(_IterationNumberX / (float)threadGroupSizes.x),
                Mathf.CeilToInt(_IterationNumberY / (float)threadGroupSizes.y),
                Mathf.CeilToInt(_IterationNumberZ / (float)threadGroupSizes.y)
            );
            _computeShader.Dispatch(_kernelID, _numGroups.x, _numGroups.y, _numGroups.z);
        }

        public static Vector3Int GetThreadGroupSizes(ComputeShader _compute, int _kernelIndex = 0)
        {
            _compute.GetKernelThreadGroupSizes(_kernelIndex, out uint x, out uint y, out uint z);
            return new Vector3Int((int)x, (int)y, (int)z);
        }
    }
}