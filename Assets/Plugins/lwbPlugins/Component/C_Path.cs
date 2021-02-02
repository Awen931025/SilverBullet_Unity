using System.Collections.Generic;
using UnityEngine;

namespace W
{
    public class C_Path : MonoBehaviour
    {
        [Separator("PathSetting")]
        public float pathCount = 10;
        public float radius = 3;
        float anglePre;
        public bool closePath;

        [Separator("DoPathSetting")]
        public float duration = 30;
        public float delay = 0;
        public int loop = -1;
        public float lookAhead = 0.2f;
        [Header("若不为空，就会看向这个参数")]
        public Transform lookFor;

        //[InspectorButton("CreatePath")]
        //public string 生成路径 = "";

        public List<C_PathWaypoint> pointS = new List<C_PathWaypoint>();
        [InspectorButton("CreateOnePoint")]
        public string 生成一个点 = "";

        [InspectorButton("UpdatePointsByChildren")]
        public string 更新 = "";

        [InspectorButton("UpdateStopList")]
        public string 更新暂停点 = "";

        [Header("用来判断这个点是否需要暂停")]
        public List<bool> stopList = new List<bool>();


        [InspectorButton("ClearAll")]
        public string 全部清除 = "";

        //原工程
        //public List<DC_EquipObj> equipS = new List<DC_EquipObj>();

        public bool awakeClosePoint = true;

        public void GetChildrenPoint()
        {
            C_PathWaypoint[] pS = transform.GetComponentsInChildren<C_PathWaypoint>(true);
            foreach (C_PathWaypoint point in pS)
            {
                pointS.Add(point);
            }
        }
        private void Awake()
        {
            if (awakeClosePoint && pointS != null || pointS.Count <= 0)
            {
                foreach (var item in pointS)
                {
                    item.gameObject.SetActive(false);
                }
            }
        }
        /// 生成圆形路径，用来做围绕比较好
        public void CreatCireclePointS()
        {
            anglePre = 360 / pathCount;
            int i = 0;
            for (float angle = 0; angle < 360; angle += anglePre)
            {
                float x = transform.position.x + radius * Mathf.Cos(angle * 3.14f / 180f);
                float y = transform.position.y + radius * Mathf.Sin(angle * 3.14f / 180f);
                GameObject obj = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                DestroyImmediate(obj.GetComponent<Collider>());
                obj.transform.SetParent(transform);
                obj.name = "path " + i.ToString();
                obj.transform.position = new Vector3(x, transform.position.z, y);
                C_PathWaypoint cPoint = obj.AddComponent<C_PathWaypoint>();
                cPoint.path = this;
                i++;
                pointS.Add(cPoint);
            }
            //draw = true;
        }


        [ContextMenu("生成Path")]
        public void CreatePath()
        {
            RemoveAll();
            CreatCireclePointS();
        }
        private void OnDrawGizmos()
        {
            DrawOnGizmos();

        }
        private void OnDrawGizmosSelected()
        {
            DrawOnGizmos();
        }
        //[EditorButton]
        [InspectorButton("Create")]
        public string 生成 = "";
        void Create()
        {
            CreateOne(gameObject, this);
        }


        public static C_PathWaypoint CreateOne(GameObject target, C_Path path, bool isPausePoint = true)
        {
            Debug.Log("生成漫游点");
            GameObject go = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            go.transform.SetParent(path.transform);
            go.transform.position = target.transform.position;
            go.transform.rotation = Quaternion.identity;
            go.name = target.name + " path";
            C_PathWaypoint p = go.AddComponent<C_PathWaypoint>();
            p.path = path;
            p.needStop = isPausePoint;
            return p;
        }
        public void CreateOnePoint()
        {
            GameObject obj = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            DestroyImmediate(obj.GetComponent<Collider>());
            obj.transform.SetParent(transform);
            obj.name = "path " + pointS.Count;
            if (pointS.Count > 0)
            {
                obj.transform.position = pointS[pointS.Count - 1].transform.position + new Vector3(1, 0, 1);
            }
            else
            {
                obj.transform.localPosition = Vector3.zero;
            }
            C_PathWaypoint cPoint = obj.AddComponent<C_PathWaypoint>();
            cPoint.path = this;
            pointS.Add(cPoint);
        }

        public void UpdatePointsByChildren()
        {
            pointS.Clear();
            C_PathWaypoint[] pS = transform.GetComponentsInChildren<C_PathWaypoint>();
            pointS = U_List.ArrayToList(pS);



            //原工程
            //equipS.Clear();
            foreach (C_PathWaypoint point in pS)
            {
                //原工程
                //if (point.target != null && point.target.GetComponent<DC_EquipObj>() != null)
                //{
                //    equipS.Add(point.target.GetComponent<DC_EquipObj>());
                //}
            }


            UpdateStopList();
        }

        public void UpdateStopList()
        {
            stopList.Clear();
            for (int i = 0; i < pointS.Count; i++)
            {
                stopList.Add(pointS[i].needStop);
            }
        }


        private void DrawOnGizmos()
        {
            //if (!draw)
            //return;
            Gizmos.color = Color.green;
            for (int i = 0; i < pointS.Count; i++)
            {
#if UNITY_EDITOR
                //U_IconManager.SetIcon(pointS[i].gameObject, U_IconManager.LabelIcon.Blue);
#endif
                if (i < pointS.Count - 1)
                {

                    Gizmos.DrawLine(pointS[i].transform.position, pointS[i + 1].transform.position);
                }
                else if (closePath)
                {
                    Gizmos.DrawLine(pointS[i].transform.position, pointS[0].transform.position);

                }
            }
        }


        public Vector3[] GetPath()
        {
            if (pointS.Count > 0)
            {
                List<Vector3> vector3S = new List<Vector3>();
                foreach (var item in pointS)
                {
                    vector3S.Add(item.transform.position);
                }
                Vector3[] posS = vector3S.ToArray();
                return posS;
            }
            else
            {
                Debug.LogError("传入了空路径！");
                return null;

            }
        }

        void RemoveAll()
        {
            pointS.Clear();
            List<Transform> sonS = transform.GetSonS();
            foreach (var item in sonS)
            {
                DestroyImmediate(item.gameObject);
            }
        }
        public void ClearAll()
        {
            stopList.Clear();
            pointS.Clear();
            U_Transform.DestroyImmediateAllSon(transform);
        }
    }
}