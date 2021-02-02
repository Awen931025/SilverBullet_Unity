using UnityEngine;

namespace W
{
    public class U_Mesh
    {
        public static void GetVolume(Transform tran)
        {
            if (tran.GetComponent<MeshFilter>() == null)
            {
                return;
            }
            MeshFilter mf = tran.GetComponent<MeshFilter>();

            Vector3[] arrVertices = mf.mesh.vertices;
            int[] arrTriangles = mf.mesh.triangles;
            float sum = 0.0f;
            for (int i = 0; i < mf.mesh.subMeshCount; i++)
            {
                int[] arrIndices = mf.mesh.GetTriangles(i);
                for (int j = 0; j < arrIndices.Length; j += 3)
                    sum += CalculateVolume(tran, arrVertices[arrIndices[j]]
                                , arrVertices[arrIndices[j + 1]]
                                , arrVertices[arrIndices[j + 2]]);
            }
        }
        public static void GetSumArea(Transform tran)
        {
            if (tran.GetComponent<MeshFilter>() == null)
            {
                return;
            }
            MeshFilter mf = tran.GetComponent<MeshFilter>();
            Vector3[] arrVertices = mf.mesh.vertices;
            int[] arrTriangles = mf.mesh.triangles;
            float sum1 = 0.0f;
            for (int i = 0; i < mf.mesh.subMeshCount; i++)
            {
                int[] arrIndices = mf.mesh.GetTriangles(i);
                for (int j = 0; j < arrIndices.Length; j += 3)
                    sum1 += CalculateArea(tran, arrVertices[arrIndices[j]]
                                , arrVertices[arrIndices[j + 1]]
                                , arrVertices[arrIndices[j + 2]]);
            }
        }

        private static float CalculateVolume(Transform tran, Vector3 pt0, Vector3 pt1, Vector3 pt2)
        {
            Vector3 scale = tran.lossyScale;
            pt0 = new Vector3(pt0.x * scale.x, pt0.y * scale.y, pt0.z * scale.z);
            pt1 = new Vector3(pt1.x * scale.x, pt1.y * scale.y, pt1.z * scale.z);
            pt2 = new Vector3(pt2.x * scale.x, pt2.y * scale.y, pt2.z * scale.z);
            float v321 = pt2.x * pt1.y * pt0.z;
            float v231 = pt1.x * pt2.y * pt0.z;
            float v312 = pt2.x * pt0.y * pt1.z;
            float v132 = pt0.x * pt2.y * pt1.z;
            float v213 = pt1.x * pt0.y * pt2.z;
            float v123 = pt0.x * pt1.y * pt2.z;
            return 1.0f / 6.0f * (-v321 + v231 + v312 - v132 - v213 + v123);
        }

        private static float CalculateArea(Transform tran, Vector3 pt0, Vector3 pt1, Vector3 pt2)
        {
            Vector3 scale = tran.lossyScale;
            pt0 = new Vector3(pt0.x * scale.x, pt0.y * scale.y, pt0.z * scale.z);
            pt1 = new Vector3(pt1.x * scale.x, pt1.y * scale.y, pt1.z * scale.z);
            pt2 = new Vector3(pt2.x * scale.x, pt2.y * scale.y, pt2.z * scale.z);
            float a = (pt1 - pt0).magnitude;
            float b = (pt2 - pt1).magnitude;
            float c = (pt0 - pt2).magnitude;
            float p = (a + b + c) * 0.5f;
            return Mathf.Sqrt(p * (p - a) * (p - b) * (p - c));
        }
    }
}