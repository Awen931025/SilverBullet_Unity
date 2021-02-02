/*Name:		 				U_Collider	
 *Description: 				
 *Author:       			lwb
 *Date:         			2019-06-
 *Copyright(C) 2019 by 		company@zhiwyl.com*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace W
{
    public class U_Collider : MonoBehaviour
    {

        /// <summary>
        /// 对于简单物体还行
        /// </summary>
        /// <param name="tran"></param>
        public static void CreatBoxByChildrenRender(Transform tran)
        {
            Vector3 postion = tran.position;
            Quaternion rotation = tran.rotation;
            Vector3 scale = tran.localScale;
            tran.position = Vector3.zero;
            tran.rotation = Quaternion.Euler(Vector3.zero);
            tran.localScale = Vector3.one;

            Collider[] colliders = tran.GetComponentsInChildren<Collider>();
            foreach (Collider child in colliders)
            {
                DestroyImmediate(child);
            }
            Vector3 center = Vector3.zero;
            Renderer[] renders = tran.GetComponentsInChildren<Renderer>();
            foreach (Renderer child in renders)
            {
                center += child.bounds.center;
            }
            center /= tran.GetComponentsInChildren<Transform>().Length;
            Bounds bounds = new Bounds(center, Vector3.zero);
            foreach (Renderer child in renders)
            {
                bounds.Encapsulate(child.bounds);
            }
            BoxCollider boxCollider = tran.gameObject.AddComponent<BoxCollider>();
            boxCollider.center = bounds.center - tran.position;
            boxCollider.size = bounds.size;

            tran.position = postion;
            tran.rotation = rotation;
            tran.localScale = scale;
        }



    }
}