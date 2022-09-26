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

        private Mesh savedMesh;

        public void Gen()
        {
            CurveMesh _mesh = CurveMesh.Factory(meshType, curve);
            meshFilter.sharedMesh = _mesh.GenerateMesh();
        }

        [ContextMenu("Mesh Gen")]
        public void GenByMesh()
        {
            Bounds _meshBounds = meshToDeform.bounds;
            Vector3 _origin = transform.TransformPoint(new Vector3(_meshBounds.center.x, _meshBounds.min.y, _meshBounds.min.z));
            Vector3 _end = transform.TransformPoint(new Vector3(_meshBounds.center.x, _meshBounds.min.y, _meshBounds.max.z));

            Vector3[] _vertices = meshToDeform.vertices;
            float _zDst = Mathf.Abs(_end.z - _origin.z);
            for (var i = 0; i < _vertices.Length; i++)
            {
                _vertices[i] = DeformVertex(_vertices[i], _origin, _end, _zDst);
            }

            savedMesh = new Mesh();
            savedMesh.vertices = _vertices;
            savedMesh.uv = meshToDeform.uv;
            savedMesh.triangles = meshToDeform.triangles;
            savedMesh.RecalculateNormals();

            meshFilter.mesh = savedMesh;
        }

        private Vector3 DeformVertex(Vector3 _vertex, Vector3 _start, Vector3 _end, float _zDst)
        {
            float _vDst = Mathf.Abs(_vertex.z - _start.z);
            float _localT = _vDst / _zDst;
            Vector3 _refPoint = Vector3.Lerp(_start, _end, _localT);
            Vector3 _new = _vertex - _refPoint + curve.ComputePoint(_localT);
            print(_new);

            return _new;
        }
    }
}