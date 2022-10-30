using UnityEngine;

namespace Nazio_LT.Tools.Core
{
    [AddComponentMenu("Nazio_LT/Core/NCurveBehaviour")]
    public class NCurveBehaviour : MonoBehaviour
    {
        //For Editor
        [SerializeField] private Mesh meshToDeform;
        [SerializeField] private Material material;
        [SerializeField] public bool editing;
        [SerializeField] private CurveMeshType meshType;
        [SerializeField] private CurveMeshDeformerPointSettings meshPlacementSettings;

        [SerializeField] private NCurve curve;

        public void Gen()
        {
            DeleteMeshes();

            if (curve.handles.Count == 0) throw new System.Exception("Cannot Generate the mesh. No handles.");

            if (meshToDeform == null) throw new System.Exception("Cannot Generate the mesh. No mesh to deform.");


            CurveMeshDeformerMainSettings _mainSettings = new CurveMeshDeformerMainSettings(meshToDeform, transform, curve, material);
            CurveMeshDeformer _deformer = CurveMeshDeformer.Factory(meshType, _mainSettings, meshPlacementSettings);
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

        public NCurve Curve => curve;
    }
}