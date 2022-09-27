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

    public class CurveMeshDeformer
    {
        public CurveMeshDeformer(CurveMeshDeformerMainSettings _mainSettings)
        {
            settings = _mainSettings;
        }

        protected CurveMeshDeformerMainSettings settings;

        public void Generate()
        {
            //Analyse le Mesh
            Bounds _meshBounds = settings.meshToDeform.bounds;
            Vector3 _meshOrigin = settings.transform.TransformPoint(new Vector3(_meshBounds.center.x, _meshBounds.min.y, _meshBounds.min.z));
            Vector3 _meshEnd = settings.transform.TransformPoint(new Vector3(_meshBounds.center.x, _meshBounds.min.y, _meshBounds.max.z));

            float _zDst = Mathf.Abs(_meshEnd.z - _meshOrigin.z);
            int _objNumber = (int)(settings.curve.curveLength / _zDst);

            float _tPart = 1f / (float)_objNumber;
            float _startT = 0f;

            //Crée les meshs
            for (var j = 0; j < _objNumber; j++)
            {
                if (!CreateChildMesh(_meshOrigin, _meshEnd, _zDst, _startT, _tPart)) break;
                _startT += _tPart;
            }
        }

        private bool CreateChildMesh(Vector3 _origin, Vector3 _end, float _zDst, float _startT, float _tPart)
        {
            GameObject _sub = new GameObject("Spline Sub Mesh");
            _sub.transform.parent = settings.transform;
            MeshRenderer _mr = _sub.AddComponent<MeshRenderer>();
            MeshFilter _mf = _sub.AddComponent<MeshFilter>();

            Vector3[] _vertices = settings.meshToDeform.vertices;
            Mesh objMesh = new Mesh();
            for (var i = 0; i < _vertices.Length; i++)
            {
                _vertices[i] = DeformVertex(_vertices[i], _origin, _end, _zDst, _startT, _tPart);
            }

            objMesh.vertices = _vertices;
            objMesh.uv = settings.meshToDeform.uv;
            objMesh.triangles = settings.meshToDeform.triangles;
            objMesh.RecalculateNormals();
            objMesh.RecalculateBounds();

            if (objMesh.bounds.size.magnitude < _zDst / 1.5f)//Vrmt petit puisque si deformé normalement la taille augmente
            {
                GameObject.DestroyImmediate(_sub);
                return false;
            }

            _mr.material = settings.material;
            _mf.mesh = objMesh;

            return true;
        }

        private Vector3 DeformVertex(Vector3 _vertex, Vector3 _start, Vector3 _end, float _zDst, float _startT, float _tPart)
        {
            float _vDst = Mathf.Abs(_vertex.z - _start.z);
            float _localT = _vDst / _zDst;
            float _curveT = Mathf.Clamp(_startT + (_localT * _tPart), 0f, 1f);

            Vector3 _refPoint = Vector3.Lerp(_start, _end, _localT);
            Vector3 _deformationDelta = _vertex - _refPoint;

            settings.curve.DirectionUniform(_curveT, out Vector3 _forward, out Vector3 _up, out Vector3 _right);

            Vector3 _orientedDelta = Vector3.zero;
            _orientedDelta -= _forward * _deformationDelta.z;
            _orientedDelta += _up * _deformationDelta.y;
            _orientedDelta -= _right * _deformationDelta.x;

            return _orientedDelta + settings.curve.ComputePointUniform(_curveT, false);
        }
    }
}