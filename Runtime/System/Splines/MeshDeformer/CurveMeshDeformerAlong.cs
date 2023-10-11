using UnityEngine;

namespace Nazio_LT.Tools.Core
{
    public class CurveMeshDeformerAlong : CurveMeshDeformer
    {
        public CurveMeshDeformerAlong(CurveMeshDeformerMainSettings mainSettings) : base(mainSettings) { }

        public override void Generate()
        {
            //Analyse le Mesh
            Bounds meshBounds = m_mesh.bounds;
            Vector3 meshOrigin = m_transform.TransformPoint(new Vector3(meshBounds.center.x, meshBounds.min.y, meshBounds.min.z));
            Vector3 meshEnd = m_transform.TransformPoint(new Vector3(meshBounds.center.x, meshBounds.min.y, meshBounds.max.z));

            float zDst = Mathf.Abs(meshEnd.z - meshOrigin.z);
            int objNumber = (int)(m_curve.CurveLength / zDst);

            float tPart = 1f / (float)objNumber;
            float startT = 0f;

            //Crée les meshs
            for (var j = 0; j < objNumber; j++)
            {
                if (!CreateChildMesh(meshOrigin, meshEnd, zDst, startT, tPart)) break;
                startT += tPart;
            }
        }

        private bool CreateChildMesh(Vector3 origin, Vector3 end, float zDst, float startT, float tPart)
        {
            GameObject sub = CreateSubMesh(m_transform, out MeshRenderer mr, out MeshFilter mf);

            Vector3[] vertices = m_mesh.vertices;
            Mesh objMesh = new Mesh();
            for (var i = 0; i < vertices.Length; i++)
            {
                vertices[i] = DeformVertex(vertices[i], origin, end, zDst, startT, tPart);
            }

            objMesh.vertices = vertices;
            objMesh.uv = m_mesh.uv;
            objMesh.triangles = m_mesh.triangles;
            objMesh.RecalculateNormals();
            objMesh.RecalculateBounds();

            if (objMesh.bounds.size.magnitude < zDst / 1.5f)//Vrmt petit puisque si deformé normalement la taille augmente
            {
                GameObject.DestroyImmediate(sub);
                return false;
            }

            mr.material = m_material;
            mf.mesh = objMesh;

            return true;
        }

        private Vector3 DeformVertex(Vector3 vertex, Vector3 start, Vector3 end, float zDst, float startT, float tPart)
        {
            float vDst = Mathf.Abs(vertex.z - start.z);
            float localT = vDst / zDst;
            float curveT = Mathf.Clamp(startT + (localT * tPart), 0f, 1f);

            Vector3 refPoint = Vector3.Lerp(start, end, localT);
            Vector3 deformationDelta = vertex - refPoint;

            m_settings.Curve.DirectionUniform(curveT, out Vector3 forward, out Vector3 up, out Vector3 right);

            Vector3 orientedDelta = Vector3.zero;
            orientedDelta -= forward * deformationDelta.z;
            orientedDelta += up * deformationDelta.y;
            orientedDelta -= right * deformationDelta.x;

            return orientedDelta + m_curve.ComputePointUniform(curveT, false);
        }
    }
}