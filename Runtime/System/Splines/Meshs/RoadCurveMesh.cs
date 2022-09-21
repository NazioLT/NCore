using UnityEngine;

namespace Nazio_LT.Tools.Core
{
    public class RoadCurveMesh : CurveMesh
    {
        public RoadCurveMesh(NCurve _curve) : base(_curve) { }

        private float roadWitdh = 1.5f;
        private float sideWalkHeight = 0.2f;
        private float sideWalkWidth = 0.3f;


        protected override void PointPattern(ref Vector3[] _vertices, int _index, Vector3 _origin, Vector3 _up, Vector3 _right)
        {
            Vector3 _rightLenght = _right * (TotalWidth / 2);
            Vector3 _rightRoad = _right * (roadWitdh / 2);

            Vector3 _verticalRoadCross = _up * 0.5f;
            Vector3 _verticalSidewalkCross = _up * (sideWalkHeight + 0.5f);

            _vertices[_index] = _origin + _right * 0.5f;
            _vertices[_index + 1] = _origin - _right * 0.5f;

            _vertices[_index] = _origin + _verticalRoadCross;
            _vertices[_index + 1] = _origin - _rightLenght;
            _vertices[_index + 2] = _origin + _rightLenght;
            _vertices[_index + 3] = _origin - _rightLenght + _verticalSidewalkCross;
            _vertices[_index + 4] = _origin - _rightRoad + _verticalSidewalkCross;
            _vertices[_index + 5] = _origin - _rightRoad + _verticalRoadCross;
            _vertices[_index + 6] = _origin + _rightLenght + _verticalSidewalkCross;
            _vertices[_index + 7] = _origin + _rightRoad + _verticalSidewalkCross;
            _vertices[_index + 8] = _origin + _rightRoad + _verticalRoadCross;
        }

        protected override void TrianglesPattern(ref int[] _triangles, int _verticesIndex, ref int _triIndex)
        {
            int formVertices = 9;
            AddSquad(ref _triangles, _verticesIndex + 1, _verticesIndex + 2, _verticesIndex + formVertices + 2, _verticesIndex + formVertices + 1, ref _triIndex);//Dessous

            AddSquad(ref _triangles, _verticesIndex + 1, _verticesIndex + formVertices + 1, _verticesIndex + formVertices + 3, _verticesIndex + 3, ref _triIndex);//Ext gauche
            AddSquad(ref _triangles, _verticesIndex + 3, _verticesIndex + formVertices + 3, _verticesIndex + formVertices + 4, _verticesIndex + 4, ref _triIndex);//Trottoir gauche
            AddSquad(ref _triangles, _verticesIndex + 4, _verticesIndex + formVertices + 4, _verticesIndex + formVertices + 5, _verticesIndex + 5, ref _triIndex);//Bout Trottoir gauche
            AddSquad(ref _triangles, _verticesIndex + 5, _verticesIndex + formVertices + 5, _verticesIndex + formVertices + 0, _verticesIndex + 0, ref _triIndex);//Route gauche

            AddSquad(ref _triangles, _verticesIndex + 0, _verticesIndex + formVertices + 0, _verticesIndex + formVertices + 8, _verticesIndex + 8, ref _triIndex);//Route droite
            AddSquad(ref _triangles, _verticesIndex + 8, _verticesIndex + formVertices + 8, _verticesIndex + formVertices + 7, _verticesIndex + 7, ref _triIndex);//Bout Trottoir droit
            AddSquad(ref _triangles, _verticesIndex + 7, _verticesIndex + formVertices + 7, _verticesIndex + formVertices + 6, _verticesIndex + 6, ref _triIndex);//Trottoir droit
            AddSquad(ref _triangles, _verticesIndex + 6, _verticesIndex + formVertices + 6, _verticesIndex + formVertices + 2, _verticesIndex + 2, ref _triIndex);//Ext droite
        }

        private float TotalWidth => sideWalkWidth + roadWitdh;
        protected override int PointPerPattern => 9;
        protected override int TrianglesPerPattern => 18;
    }
}