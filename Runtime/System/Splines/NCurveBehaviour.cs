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
        [SerializeField] private CurveMesh.CurveMeshType meshType;

        [SerializeField] private NCurve curve;
        [SerializeField] private MeshFilter meshFilter;

        public void Gen()
        {
            DeleteMeshes();

            if (meshType == CurveMesh.CurveMeshType.CustomMesh)
            {
                CurveMeshDeformer _deformer = new CurveMeshDeformerAlong(new CurveMeshDeformerMainSettings(meshToDeform, transform, curve, material));
                _deformer.Generate();
                return;
            }

            CurveMesh _mesh = CurveMesh.Factory(meshType, curve);
            meshFilter.sharedMesh = _mesh.GenerateMesh();
        }

        public void DeleteMeshes()
        {
            meshFilter.mesh = null;
            if (transform.childCount == 0) return;

            for (var i = transform.childCount - 1; i >= 0; i--)
            {
                DestroyImmediate(transform.GetChild(i).gameObject);
            }
        }

        public NCurve Curve => curve;
    }
}