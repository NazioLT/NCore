using UnityEngine;

namespace Nazio_LT.Tools.Core
{
    [System.Serializable]
    public struct CurveMeshDeformerPointSettings
    {
        [SerializeField] private float m_distance;
        [SerializeField] private bool m_genLast;

        public float Distance => m_distance > 0 ? m_distance : NMath.EPSILON;
        public bool GenLast => m_genLast;
    }

    public class CurveMeshDeformerPoint : CurveMeshDeformer
    {
        public CurveMeshDeformerPoint(CurveMeshDeformerMainSettings mainSettings, CurveMeshDeformerPointSettings placementSettings) : base(mainSettings)
        {
            m_placementSettings = placementSettings;
        }

        protected CurveMeshDeformerPointSettings m_placementSettings;

        public override void Generate()
        {
            m_curve.Update();

            int objToPlace = (int)(m_settings.Curve.CurveLength / m_placementSettings.Distance) + 1;
            float factor = 1f / (float)(objToPlace - 1);

            for (var i = 0; i < objToPlace; i++)
            {
                PlaceMesh(i * m_placementSettings.Distance);
            }

            if (m_placementSettings.GenLast) PlaceMesh(m_settings.Curve.CurveLength);
        }

        private void PlaceMesh(float dst)
        {
            GameObject sub = CreateSubMesh(m_transform, out MeshRenderer mr, out MeshFilter mf);

            mf.mesh = m_settings.MeshToDeform;

            sub.transform.position = m_curve.ComputePointDistance(dst);
            mr.material = m_material;
        }
    }
}