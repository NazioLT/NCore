using UnityEngine;

namespace Nazio_LT.Tools.Core
{
    [System.Serializable]
    public struct CurveMeshDeformerPointSettings
    {
        [SerializeField] private float distance;

        public float Distance => distance > 0 ? distance : NMath.EPSILON;
    }

    public class CurveMeshDeformerPoint : CurveMeshDeformer
    {
        public CurveMeshDeformerPoint(CurveMeshDeformerMainSettings _mainSettings, CurveMeshDeformerPointSettings _placementSettings) : base(_mainSettings)
        {
            placementSettings = _placementSettings;
        }

        protected CurveMeshDeformerPointSettings placementSettings;

        public override void Generate()
        {
            int _objToPlace = (int)(settings.curve.curveLength / placementSettings.Distance);
            float _factor = 1f / (float)(_objToPlace - 1);

            for (var i = 0; i < _objToPlace; i++)
            {
                PlaceMesh(i * _factor);
            }
        }

        private void PlaceMesh(float _t)
        {
            GameObject _sub = CreateSubMesh(transform, out MeshRenderer _mr, out MeshFilter _mf);

            _mf.mesh = settings.meshToDeform;

            _sub.transform.position = Curve.ComputePointUniform(_t, false);
            _mr.material = Material;
        }
    }
}