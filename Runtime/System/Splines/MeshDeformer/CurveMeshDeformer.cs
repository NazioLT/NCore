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

    public enum CurveMeshType { Along, Point }

    public abstract class CurveMeshDeformer
    {
        public CurveMeshDeformer(CurveMeshDeformerMainSettings _mainSettings)
        {
            settings = _mainSettings;
        }

        protected CurveMeshDeformerMainSettings settings;

        public abstract void Generate();

        protected GameObject CreateSubMesh(Transform _parent, out MeshRenderer _mr, out MeshFilter _mf)
        {
            GameObject _sub = new GameObject("Spline Sub Mesh");
            _sub.transform.parent = _parent;
            _mr = _sub.AddComponent<MeshRenderer>();
            _mf = _sub.AddComponent<MeshFilter>();

            return _sub;
        }

        protected Mesh Mesh => settings.meshToDeform;
        protected Transform transform => settings.transform;
        protected NCurve Curve => settings.curve;
        protected Material Material => settings.material;

        public static CurveMeshDeformer Factory(CurveMeshType _type, CurveMeshDeformerMainSettings _mainSettings, CurveMeshDeformerPointSettings _placementSettings)
        {
            switch (_type)
            {
                case CurveMeshType.Along:
                    return new CurveMeshDeformerAlong(_mainSettings);
                case CurveMeshType.Point:
                    return new CurveMeshDeformerPoint(_mainSettings, _placementSettings);
            }

            return null;
        }
    }
}