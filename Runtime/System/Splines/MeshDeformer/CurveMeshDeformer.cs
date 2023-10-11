using UnityEngine;

namespace Nazio_LT.Tools.Core
{
    public struct CurveMeshDeformerMainSettings
    {
        public CurveMeshDeformerMainSettings(Mesh meshToDeform, Transform parent, NCurve curve, Material material)
        {
            MeshToDeform = meshToDeform;
            Transform = parent;
            Curve = curve;
            Material = material;
        }

        public readonly Mesh MeshToDeform;
        public readonly Transform Transform;
        public readonly NCurve Curve;
        public readonly Material Material;
    }

    public enum CurveMeshType { Along, Point }

    public abstract class CurveMeshDeformer
    {
        public CurveMeshDeformer(CurveMeshDeformerMainSettings mainSettings)
        {
            m_settings = mainSettings;
        }

        protected CurveMeshDeformerMainSettings m_settings;

        public abstract void Generate();

        protected GameObject CreateSubMesh(Transform parent, out MeshRenderer meshRenderer, out MeshFilter meshFilter)
        {
            GameObject sub = new GameObject("Spline Sub Mesh");
            sub.transform.parent = parent;
            meshRenderer = sub.AddComponent<MeshRenderer>();
            meshFilter = sub.AddComponent<MeshFilter>();

            return sub;
        }

        protected Mesh m_mesh => m_settings.MeshToDeform;
        protected Transform m_transform => m_settings.Transform;
        protected NCurve m_curve => m_settings.Curve;
        protected Material m_material => m_settings.Material;

        public static CurveMeshDeformer Factory(CurveMeshType type, CurveMeshDeformerMainSettings mainSettings, CurveMeshDeformerPointSettings placementSettings)
        {
            switch (type)
            {
                case CurveMeshType.Along:
                    return new CurveMeshDeformerAlong(mainSettings);
                case CurveMeshType.Point:
                    return new CurveMeshDeformerPoint(mainSettings, placementSettings);
            }

            return null;
        }
    }
}