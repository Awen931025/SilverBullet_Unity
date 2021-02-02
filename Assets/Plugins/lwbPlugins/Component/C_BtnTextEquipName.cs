/*Name:		 				C_ButtonTextToButton	
 *Description: 				将按钮的显示Text改为按钮的名称
 *Author:       			lwb
 *Date:         			2019-06-
 *Copyright(C) 2019 by 		company@zhiwyl.com*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace W
{
    public class C_BtnTextEquipName : MonoBehaviour
    {

        [ContextMenu("按钮的显示=按钮的名称")]
        public void ButtonTextToButtonName()
        {
            Button[] btnS = GetComponentsInChildren<Button>();
            foreach (Button item in btnS)
            {
                if (item.GetComponentInChildren<Text>() != null)
                {
                    item.GetComponentInChildren<Text>().text = item.name;
                }
            }
        }
        /// 按钮的显示=按钮的名称
        public static void ChildrenBtnTextSameBtnName(Transform tran)
        {
            Button[] btnS = tran.GetComponentsInChildren<Button>();
            foreach (Button item in btnS)
            {
                if (item.GetComponentInChildren<Text>() != null)
                {
                    item.GetComponentInChildren<Text>().text = item.name;
                }
            }
        }
    }
}