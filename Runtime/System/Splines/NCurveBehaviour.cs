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

        [SerializeField] public NCurve curve;
        [SerializeField] private MeshFilter meshFilter;

        public void Gen()
        {
            DestroyChilds();
            meshFilter.mesh = null;

            if (meshType == CurveMesh.CurveMeshType.CustomMesh)
            {
                GenByMesh();
                return;
            }

            CurveMesh _mesh = CurveMesh.Factory(meshType, curve);
            meshFilter.sharedMesh = _mesh.GenerateMesh();
        }

        private void DestroyChilds()
        {
            if (transform.childCount == 0) return;
            
            for (var i = transform.childCount - 1; i >= 0; i--)
            {
                DestroyImmediate(transform.GetChild(i).gameObject);
            }
        }

        private void GenByMesh()
        {
            Bounds _meshBounds = meshToDeform.bounds;
            Vector3 _meshOrigin = transform.TransformPoint(new Vector3(_meshBounds.center.x, _meshBounds.min.y, _meshBounds.min.z));
            Vector3 _meshEnd = transform.TransformPoint(new Vector3(_meshBounds.center.x, _meshBounds.min.y, _meshBounds.max.z));

            float _zDst = Mathf.Abs(_meshEnd.z - _meshOrigin.z);
            int _objNumber = (int)(curve.curveLength / _zDst);

            float _tPart = 1f / (float)_objNumber;
            float _startT = 0f;
            for (var j = 0; j < _objNumber; j++)
            {
                CreateChildMesh(_meshOrigin, _meshEnd, _zDst, _startT, _tPart);
                _startT += _tPart;
            }
        }

        private void CreateChildMesh(Vector3 _origin, Vector3 _end, float _zDst, float _startT, float _tPart)
        {
            GameObject _sub = new GameObject("Spline Sub Mesh");
            _sub.transform.parent = transform;
            MeshRenderer _mr = _sub.AddComponent<MeshRenderer>();
            MeshFilter _mf = _sub.AddComponent<MeshFilter>();

            Vector3[] _vertices = meshToDeform.vertices;
            Mesh objMesh = new Mesh();
            for (var i = 0; i < _vertices.Length; i++)
            {
                _vertices[i] = DeformVertex(_vertices[i], _origin, _end, _zDst, _startT, _tPart);
            }

            objMesh.vertices = _vertices;
            objMesh.uv = meshToDeform.uv;
            objMesh.triangles = meshToDeform.triangles;
            objMesh.RecalculateNormals();

            _mr.material = material;

            _mf.mesh = objMesh;
        }

        private Vector3 DeformVertex(Vector3 _vertex, Vector3 _start, Vector3 _end, float _zDst, float _startT, float _tPart)
        {
            float _vDst = Mathf.Abs(_vertex.z - _start.z);
            float _localT = _vDst / _zDst;
            float _curveT = Mathf.Clamp(_startT + (_localT * _tPart), 0f, 1f);

            Vector3 _refPoint = Vector3.Lerp(_start, _end, _localT);
            Vector3 _deformationDelta = _vertex - _refPoint;

            curve.DirectionUniform(_curveT, out Vector3 _forward, out Vector3 _up, out Vector3 _right);

            Vector3 _orientedDelta = Vector3.zero;
            _orientedDelta -= _forward * _deformationDelta.z;
            _orientedDelta += _up * _deformationDelta.y;
            _orientedDelta -= _right * _deformationDelta.x;

            return _orientedDelta + curve.ComputePointUniform(_curveT, false);
        }
    }
}