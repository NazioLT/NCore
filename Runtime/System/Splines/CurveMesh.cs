using UnityEngine;

namespace Nazio_LT.Tools.Core
{
    public abstract class CurveMesh
    {
        public enum CurveMeshType { Simple, Road,}

        public static CurveMesh Factory(CurveMeshType _type, NCurve _curve)
        {
            switch(_type)
            {
                case CurveMeshType.Road:
                return new RoadCurveMesh(_curve);
            }

            return new SimpleCurveMesh(_curve);
        }

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

            MakeGeometry(_patternCount, ref _vertices, ref _triangles, ref _uv);

            mesh = new Mesh();
            mesh.vertices = _vertices;
            mesh.triangles = _triangles;
            mesh.uv = _uv;
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
                if (_t < 0) _t = 0;

                curve.Direction(_t, out Vector3 _forward, out Vector3 _up, out Vector3 _right);

                Vector3 _origin = curve.ComputePoint(_t);
                PointPattern(ref _vertices, _verticesIndex, _origin, _up, _right);
                // SetUV(ref _uvs, _verticesIndex);

                if (i == _patternCount - 1) break;

                TrianglesPattern(ref _triangles, _verticesIndex, ref _triIndex);
            }
        }

        protected abstract void PointPattern(ref Vector3[] _vertices, int _index, Vector3 _origin, Vector3 _up, Vector3 _right);
        protected abstract void TrianglesPattern(ref int[] _triangles, int _verticesIndex, ref int _triIndex);

        protected void AddTriangle(ref int[] _triArray, int _p1, int _p2, int _p3, ref int _triIndex)
        {
            _triArray[_triIndex] = _p1;
            _triArray[_triIndex + 1] = _p2;
            _triArray[_triIndex + 2] = _p3;

            _triIndex += 3;
        }

        /// <summary>
        /// Crée un carré composé de 2 triangles, les points doivent etre dans le sens des aiguilles d'une montre
        /// </summary>
        /// <param name="_triArray"></param>
        /// <param name="_p1"></param>
        /// <param name="_p2"></param>
        /// <param name="_p3"></param>
        /// <param name="_p4"></param>
        /// <param name="_triIndex"></param>
        protected void AddSquad(ref int[] _triArray, int _p1, int _p2, int _p3, int _p4, ref int _triIndex)
        {
            AddTriangle(ref _triArray, _p1, _p3, _p2, ref _triIndex);
            AddTriangle(ref _triArray, _p3, _p1, _p4, ref _triIndex);
        }

        protected abstract int PointPerPattern { get; }
        protected abstract int TrianglesPerPattern { get; }
    }
}