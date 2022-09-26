using UnityEngine;

namespace Nazio_LT.Tools.Core
{
    [RequireComponent(typeof(MeshRenderer), typeof(MeshFilter))]
    public class SplineDeformedGPU : MonoBehaviour
    {
        [SerializeField] private Texture2D splineVariations;
        [SerializeField] private NCurveBehaviour spline;
        private MeshRenderer meshRenderer;
        private MeshFilter meshFilter;

        private const int PRECISION = 100;

        private void References()
        {
            meshRenderer = GetComponent<MeshRenderer>();
            meshFilter = GetComponent<MeshFilter>();
        }

        [ContextMenu("Deform")]
        public void Deform()
        {
            References();

            if(!spline)
            {
                Debug.LogError("No spline wasn't defined.");
                return;
            }

            Mesh _meshToDeform = meshFilter.mesh;
            splineVariations = new Texture2D(PRECISION, 1);

            //Get spline deltas
            Vector3 _origin = spline.curve.ComputePointUniform(0f, true);
            Vector3[] _delta = new Vector3[PRECISION];
            Bounds _splineBoundingBox = new Bounds();

            float _factor = 1f / ((float)PRECISION - 1);
            for (var i = 1; i < PRECISION; i++)
            {
                float _t = _factor * i;

                Vector3 _point = spline.curve.ComputePointUniform(_t, true);
                _delta[i] = _point - _origin;

                _splineBoundingBox.Encapsulate(_delta[i]);
            }

            //Draw Texture
            Color[] _colours = new Color[PRECISION];
            for (var i = 0; i < PRECISION; i++)
            {
                _colours[i] = new Color(
                    Mathf.InverseLerp(_splineBoundingBox.min.x, _splineBoundingBox.max.x, _delta[i].x),
                    Mathf.InverseLerp(_splineBoundingBox.min.y, _splineBoundingBox.max.y, _delta[i].y),
                    Mathf.InverseLerp(_splineBoundingBox.min.z, _splineBoundingBox.max.z, _delta[i].z));
            }

            splineVariations.SetPixels(_colours);
            splineVariations.Apply();

            meshRenderer.material.SetTexture("_DeformationTexture", splineVariations);
            meshRenderer.material.SetFloat("_TextureWidth", splineVariations.width);
            meshRenderer.material.SetFloat("_minZ", _splineBoundingBox.min.z);
            meshRenderer.material.SetFloat("_maxZ", _splineBoundingBox.max.z);
        }
    }
}