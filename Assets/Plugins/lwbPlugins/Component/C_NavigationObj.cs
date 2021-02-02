/*Name:		 				DC_NavigationObj	
 *Description: 				需要写进追踪的物体
 *Author:       			lwb
 *Date:         			2019-06-
 *Copyright(C) 2019 by 		company@zhiwyl.com*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace W
{
    public class C_NavigationObj : MonoBehaviour
    {
        [Header("从属于")]
        public C_NavigationObj parent;
        public List<C_NavigationObj> sonS = new List<C_NavigationObj>();

        public int layer = 1;




        public Button btn;

        public void GetLayer()
        {
            if (parent != null)
            {
                layer = parent.layer + 1;
            }
            else
            {
                layer = 1;
            }
        }

        public void GetSons(List<C_NavigationObj> allObjS)
        {
            sonS.Clear();
            foreach (C_NavigationObj obj in allObjS)
            {
                if (obj.parent == this)
                {
                    if (!sonS.Contains(obj))
                        sonS.Add(obj);
                }
            }
        }




        public void GetBtn()
        {

        }
    }
}