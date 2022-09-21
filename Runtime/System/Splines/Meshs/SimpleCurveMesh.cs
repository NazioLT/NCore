using UnityEngine;

namespace Nazio_LT.Tools.Core
{
    public class SimpleCurveMesh : CurveMesh
    {
        public SimpleCurveMesh(NCurve _curve) : base(_curve) { }

        protected override void PointPattern(ref Vector3[] _vertices, int _index, Vector3 _origin, Vector3 _up, Vector3 _right)
        {
            _vertices[_index] = _origin + _right * 0.5f;
            _vertices[_index + 1] = _origin - _right * 0.5f;
        }

        protected override void TrianglesPattern(ref int[] _triangles, int _verticesIndex, ref int _triIndex)
        {
            AddSquad(ref _triangles, _verticesIndex + 1, _verticesIndex + 3, _verticesIndex + 2, _verticesIndex, ref _triIndex);
        }

        protected override void UVPattern(ref Vector2[] _uvs, int _uvIndex, int _patternCount)
        {
            Vector2 _uv = Vector2.Lerp(Vector2.zero, Vector2.one, (float)_uvIndex / _patternCount);
            _uvs[_uvIndex] = _uv;
            _uvs[_uvIndex + 1] = _uv;
            _uvs[_uvIndex + 2] = _uv;
        }

        protected override int PointPerPattern => 2;
        protected override int TrianglesPerPattern => 2;
    }
}