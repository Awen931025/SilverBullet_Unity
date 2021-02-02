using UnityEngine;
namespace W
{
    public class C_PathWaypoint : MonoBehaviour
    {
        public bool needStop;

        public C_Path path;

        [InspectorButton("PathUpdate")]
        public string 更新 = "";

        [InspectorButton("CreateOnePoint")]
        public string 生成一个点 = "";
        [InspectorButton("Delete")]
        public string 删除 = "";



        //public 
        //[Header("目前就是目标点，也是看向的物体")]
        public Transform target;

        public void PathUpdate()
        {
            path.UpdatePointsByChildren();
        }


        public void CreateOnePoint()
        {
            path.CreateOnePoint();
        }
        public void Delete()
        {
            path.pointS.Remove(this);
            path.UpdateStopList();
            DestroyImmediate(gameObject);

        }
    }
}