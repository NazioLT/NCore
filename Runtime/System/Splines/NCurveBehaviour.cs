using UnityEngine;

namespace Nazio_LT.Tools.Core
{
    [AddComponentMenu("Nazio_LT/Core/NCurveBehaviour")]
    public class NCurveBehaviour : MonoBehaviour
    {
        //For Editor
        public bool editing;

        [SerializeField] public NCurve curve;
        public MeshFilter meshFilter;

        [ContextMenu("Gen")]
        public void Gen()
        {
            CurveMesh _mesh = new CurveMesh(curve);
            meshFilter.sharedMesh = _mesh.GenerateMesh();
        }
    }

    public class CurveMesh
    {
        public CurveMesh(NCurve _curve)
        {
            mesh = new Mesh();
            curve = _curve;
        }

        private Mesh mesh;
        private NCurve curve;

        private const float PATERNS_PER_UNIT = 1f;

        public Mesh GenerateMesh()
        {
            int _patternCount = (int)(curve.curveLength * PATERNS_PER_UNIT);
            int _verticesCount = _patternCount * PointPerPattern;

            Vector3[] _vertices = new Vector3[_verticesCount];
            Vector2[] _uv = new Vector2[_verticesCount];
            int[] _triangles = new int[(_patternCount - 1) * TrianglesPerPattern * 3];

            // for (var i = 0; i < _verticesCount; i++)
            // {
            //     Debug.Log("i : " + i);
            //     int _index = i * PointPerPattern;
            //     float _t = (float)i / ((float)_verticesCount - 1);
            //     curve.Direction(_t, out Vector3 _forward, out Vector3 _up, out Vector3 _right);

            //     Vector3 _origin = curve.ComputePoint(_t);
            //     GetPointsPattern(_origin, _up, _right).CopyTo(mesh.vertices, _index);

            //     if (i + 1 >= _verticesCount) break;

            //     mesh.triangles = SetTrianglesPattern(mesh.triangles, _index);
            // }

            MakeGeometry(_patternCount, ref _vertices, ref _triangles, ref _uv);

            mesh = new Mesh();
            mesh.vertices = _vertices;
            mesh.triangles = _triangles;
            // mesh.uv = _uv;
            mesh.RecalculateNormals();
            return mesh;
        }

        private void MakeGeometry(int _patternCount, ref Vector3[] _vertices, ref int[] _triangles, ref Vector2[] _uvs)
        {
            float _loopDecrease = curve.loop ? 0 : -NMath.SQUARE_EPSILON;
            int _triIndex = 0;
            for (int i = 0; i < _patternCount; i++)
            {
                int _verticesIndex = i * PointPerPattern;
                float _t = (float)i / ((float)_patternCount - 1) - _loopDecrease;
                if(_t < 0) _t = 0;

                curve.Direction(_t, out Vector3 _forward, out Vector3 _up, out Vector3 _right);

                Vector3 _origin = curve.ComputePoint(_t);
                PointPattern(ref _vertices,_verticesIndex, _origin, _up, _right);
                // SetUV(ref _uvs, _verticesIndex);

                if (i == _patternCount - 1) break;

                TrianglesPattern(ref _triangles, _verticesIndex, ref _triIndex);
            }
        }

        protected virtual void PointPattern(ref Vector3[] _vertices, int _index, Vector3 _origin, Vector3 _up, Vector3 _right)
        {
            _vertices[_index] = _origin + _right * 0.5f;
            _vertices[_index + 1] = _origin - _right * 0.5f;
        }

        protected virtual void TrianglesPattern(ref int[] _triangles, int _verticesIndex, ref int _triIndex)
        {
            AddTriangle(ref _triangles, _verticesIndex, _verticesIndex + 2, _verticesIndex + 1, ref _triIndex);
            AddTriangle(ref _triangles, _verticesIndex + 1, _verticesIndex + 2, _verticesIndex + 3, ref _triIndex);
        }

        protected void AddTriangle(ref int[] _triArray, int _p1, int _p2, int _p3, ref int _triIndex)
        {
            _triArray[_triIndex] = _p1;
            _triArray[_triIndex + 1] = _p2;
            _triArray[_triIndex + 2] = _p3;

            _triIndex += 3;
        }


        // public static void AddTriangle(ref int[] _array, int _index, int _p1, int _p2, int _p3) => new int[] { _p1, _p2, _p3 }.CopyTo(_array, _index);

        protected virtual int PointPerPattern => 2;
        protected virtual int TrianglesPerPattern => 2;
    }
}