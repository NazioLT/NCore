using UnityEngine;

namespace Nazio_LT.Tools.Core
{
    public struct CurveMeshDeformerMainSettings
    {
        public CurveMeshDeformerMainSettings(Mesh _meshToDeform, Transform _parent, NCurve _curve, Material _material)
        {
            meshToDeform = _meshToDeform;
            transform = _parent;
            curve = _curve;
            material = _material;
        }

        public readonly Mesh meshToDeform;
        public readonly Transform transform;
        public readonly NCurve curve;
        public readonly Material material;
    }
    
    public abstract class CurveMeshDeformer
    {
        public CurveMeshDeformer(CurveMeshDeformerMainSettings _mainSettings)
        {
            settings = _mainSettings;
        }

        protected CurveMeshDeformerMainSettings settings;

        public abstract void Generate();
    }
}