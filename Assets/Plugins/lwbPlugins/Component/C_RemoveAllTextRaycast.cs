/*Name:		 				C_RemoveAllTextRaycast	
 *Description: 				反勾选所有Text的Raycast
 *Author:       			李文博 
 *Date:         			2019-08-
 *Copyright(C) 2019 by 		智联友道*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

namespace W
{
    public class C_RemoveAllTextRaycast : MonoBehaviour
    {
        public List<Text> textS = new List<Text>();
        [InspectorButton("ChangeAllFont")]
        public string 反勾选所有Text的Raycast = "";
        public void ChangeAllFont()
        {
            textS.Clear();
            textS = U_FindAll.GetAllT<Text>();
            int i = 0;
            foreach (var text in textS)
            {
                if (text.raycastTarget)
                {
                    i++;
                    text.raycastTarget = false;
                }
            }
            Debug.Log("有 " + i + " 个Text反勾选了raycastTarget");
        }


    }
}