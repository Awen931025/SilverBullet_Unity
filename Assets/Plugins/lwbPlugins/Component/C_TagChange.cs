/*Name:		 				C_TagChange	
 *Description: 				用于改变子物体tag
 *Author:       			李文博 
 *Date:         			2018-07-
 *Copyright(C) 2018 by 		北京兆泰源信息技术有限公司*/
using UnityEngine;

namespace W
{
    public class C_TagChange : MonoBehaviour
    {
        [Header("用于改变子物体tag")]
        public string tarTag = "equip";
        [InspectorButton("ChangeChildrenTag")]
        public string 修改子物体和自己的tag = "";
        [Separator("常用")]
        [InspectorButton("ToEquip", 6)]
        public string 改为equip = "";
        [InspectorButton("ToPlane", 6)]
        public string 改为plane = "";
        [InspectorButton("ToUntagged", 6)]
        public string 改为Untagged = "";
        [InspectorButton("TovrtkUI", 6)]
        public string 改为vrtkUI = "";
        public static void SetAllChildrenTag(Transform tran, string tag)
        {
            Transform[] tranS = tran.GetComponentsInChildren<Transform>(true);
            foreach (Transform item in tranS)
            {
                item.tag = tag;
            }
        }
        public void ChangeChildrenTag()
        {
            //foreach(Transform tran in U_Transform.GetChildren(transform, true))
            foreach (Transform tran in U_Transform.GetChildren(transform, true))
            {
                tran.tag = tarTag;
            }
        }
        public void TovrtkUI()
        {
            int i = 0;
            foreach (Transform tran in U_Transform.GetChildren(transform, true))
            {
                i++;
                tran.tag = "vrtkUI";
            }
            Debug.Log(i + "   个物体改tag为【equip】");
        }
        public void ToEquip()
        {
            int i = 0;
            foreach (Transform tran in U_Transform.GetChildren(transform, true))
            {
                i++;
                tran.tag = "equip";
            }
            Debug.Log(i + "   个物体改tag为【equip】");
        }
        public void ToPlane()
        {
            //foreach(Transform tran in U_Transform.GetChildren(transform, true))
            foreach (Transform tran in U_Transform.GetChildren(transform, true))
            {
                tran.tag = "plane";
            }
        }
        public void ToUntagged()
        {
            //foreach(Transform tran in U_Transform.GetChildren(transform, true))
            foreach (Transform tran in U_Transform.GetChildren(transform, true))
            {
                tran.tag = "Untagged";
            }
        }
    }
}