using UnityEngine;

namespace Nazio_LT.Tools.Core
{
    [AddComponentMenu("Nazio_LT/Core/NCurveBehaviour")]
    public class NCurveBehaviour : MonoBehaviour
    {
        //For Editor
        [SerializeField] private Mesh meshToDeform;
        [SerializeField] public bool editing;
        [SerializeField] private CurveMesh.CurveMeshType meshType;

        [SerializeField] public NCurve curve;
        [SerializeField] private MeshFilter meshFilter;

        public void Gen()
        {
            CurveMesh _mesh = CurveMesh.Factory(meshType, curve);
            meshFilter.sharedMesh = _mesh.GenerateMesh();
        }

        [ContextMenu("Mesh Gen")]
        public void GenByMesh()
        {

        }
    }
}