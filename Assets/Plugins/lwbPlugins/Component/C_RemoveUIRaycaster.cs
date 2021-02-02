/*Name:		 				RemoveUIRaycaster	
 *Description: 				
 *Author:       			lwb
 *Date:         			2019-06-
 *Copyright(C) 2019 by 		company@zhiwyl.com*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace W
{
    public class C_RemoveUIRaycaster : MonoBehaviour
    {
        [InspectorButton("RemoveRaycaster")]
        public string 删掉画布上的GraphicRaycaster = "";
        public void RemoveRaycaster()
        {
            GraphicRaycaster[] rayS = transform.GetComponentsInChildren<GraphicRaycaster>(true);
            foreach (var item in rayS)
            {
                DestroyImmediate(item);
            }
        }

    }
}