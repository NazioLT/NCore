using UnityEngine;

namespace Nazio_LT.Tools.Core
{
    [AddComponentMenu("Nazio_LT/Core/NCurveBehaviour")]
    public class NCurveBehaviour : MonoBehaviour
    {
        //For Editor
        [SerializeField] private Mesh m_meshToDeform;
        [SerializeField] private Material m_material;
        [SerializeField] public bool Editing;
        [SerializeField] private CurveMeshType m_meshType;
        [SerializeField] private CurveMeshDeformerPointSettings m_meshPlacementSettings;

        [SerializeField] private NCurve m_curve;

        public void Gen()
        {
            DeleteMeshes();
            transform.position = Vector3.zero;

            if (m_curve.Handles.Count == 0) throw new System.Exception("Cannot Generate the mesh. No handles.");

            if (m_meshToDeform == null) throw new System.Exception("Cannot Generate the mesh. No mesh to deform.");

            CurveMeshDeformerMainSettings _mainSettings = new CurveMeshDeformerMainSettings(m_meshToDeform, transform, m_curve, m_material);
            CurveMeshDeformer _deformer = CurveMeshDeformer.Factory(m_meshType, _mainSettings, m_meshPlacementSettings);
            _deformer.Generate();
        }

        public void DeleteMeshes()
        {
            if (transform.childCount == 0) return;

            for (var i = transform.childCount - 1; i >= 0; i--)
            {
                DestroyImmediate(transform.GetChild(i).gameObject);
            }
        }

        private void Awake()
        {
            m_curve.Update();
        }

        public NCurve Curve => m_curve;
    }
}