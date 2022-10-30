using UnityEngine;

namespace Nazio_LT.Tools.Core
{
    [System.Serializable]
    public struct CurveMeshDeformerPointSettings
    {
        [SerializeField] private float distance;
        [SerializeField] private bool genLast;

        public float Distance => distance > 0 ? distance : NMath.EPSILON;
        public bool GenLast => genLast;
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
            Curve.Update();

            int _objToPlace = (int)(settings.curve.curveLength / placementSettings.Distance) + 1;
            float _factor = 1f / (float)(_objToPlace - 1);

            for (var i = 0; i < _objToPlace; i++)
            {
                PlaceMesh(i * placementSettings.Distance);
            }

            if (placementSettings.GenLast) PlaceMesh(settings.curve.curveLength);
        }

        private void PlaceMesh(float _dst)
        {
            GameObject _sub = CreateSubMesh(transform, out MeshRenderer _mr, out MeshFilter _mf);

            _mf.mesh = settings.meshToDeform;

            _sub.transform.position = Curve.ComputePointDistance(_dst);
            _mr.material = Material;
        }
    }
}