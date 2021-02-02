/*Name:		 				E_Selection	
 *Description: 				
 *Author:       			lwb
 *Date:         			2019-06-
 *Copyright(C) 2019 by 		company@zhiwyl.com*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace W
{
    public class E_Selection : Editor
    {

        /// <summary>
        /// 返回当前选中的物体，只能在Editor下使用
        /// </summary>
        /// <returns></returns>
        public static List<Transform> GetTransforms()
        {
            return U_List.ArrayToList(Selection.transforms);
            //Application.
        }

        public static void AddOccludee(GameObject go)
        {
            GameObjectUtility.SetStaticEditorFlags(go, (StaticEditorFlags)18);
        }


    }
}