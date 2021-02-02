/*Name:		 				C_SceneSetting
 *Description: 				
 *Author:       			lwb
 *Date:         			2019-09-
 *Copyright(C) 2019 by 		yoodao*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace W
{
    public class C_SceneSetting : MonoBehaviour
    {




        [Separator("只留亲儿子")]
        [InspectorButton("OnlySaveSon", 1)]
        public string 只留亲儿子;
        public void OnlySaveSon()
        {
            foreach (Transform son in transform)
            {
                foreach (Transform sunzi in son)
                {
                    DestroyImmediate(sunzi.gameObject);
                }
            }
        }





        [Separator("Font")]
        public Font targetFont;
        [InspectorButton("ChangeAllFont", 1)]
        public string 替换所有Font = "";
        public void ChangeAllFont()
        {
            List<Text> textS = U_FindAll.GetAllT<Text>();
            int i = 0;
            foreach (var text in textS)
            {
                if (text.font != targetFont)
                {
                    i++;
                    text.font = targetFont;
                }
            }
            Debug.Log("替换了 " + i + " 个Text的字体");
        }

        [InspectorButton("TextNoRaycast", 1)]
        public string Text都不进行射线截获 = "";
        public void TextNoRaycast()
        {
            List<Text> textS = U_FindAll.GetAllT<Text>();
            int i = 0;
            foreach (var text in textS)
            {
                text.raycastTarget = false;
                i++;
            }
            Debug.Log("关闭了 " + i + " 个Text的射线截获");
        }


        [Separator("BtnColor")]
        public Color normalColor = Color.white;
        public Color hoverColor = new Color32(188, 188, 188, 255);
        public Color pressedColor = new Color32(137, 137, 137, 255);
        [InspectorButton("BtnColors", 1)]
        public string Btn的颜色加重 = "";
        public void BtnColors()
        {
            List<Button> tS = U_FindAll.GetAllT<Button>();
            int i = 0;
            ColorBlock tarBlock = new ColorBlock();
            tarBlock.normalColor = normalColor;
            tarBlock.highlightedColor = hoverColor;
            tarBlock.pressedColor = pressedColor;
            tarBlock.colorMultiplier = 1;
            tarBlock.fadeDuration = 0.1f;

            foreach (var item in tS)
            {
                item.colors = tarBlock;
                i++;
            }
            Debug.Log(i + " 个Button改成了默认加重button");
        }




        [Separator("空物体")]

        [InspectorButton("ExcuteChildren", 1)]
        public string 删掉该物体的所有空物体;




        [InspectorButton("ExcuteAll", 8)]
        public string 删掉场景内所有空物体;
        public void ExcuteChildren()
        {
            Transform[] tranS = transform.GetComponentsInChildren<Transform>();
            int i = 0;
            foreach (var tran in tranS)
            {
                if (tran.GetComponentsInChildren<MeshFilter>(true).Length == 0)
                {
                    DestroyImmediate(tran.gameObject);
                    i++;
                }
            }
            Debug.Log("删除了 " + i + " 个空物体");
        }
        public void ExcuteAll()
        {
            List<Transform> tranS = U_FindAll.GetAllT<Transform>();
            int i = 0;
            foreach (var tran in tranS)
            {
                if (tran.GetComponentsInChildren<MeshFilter>(true).Length == 0)
                {

                    Debug.Log("没有任何");
                    DestroyImmediate(tran.gameObject);
                    i++;

                }
            }
            Debug.Log("删除了 " + i + " 个空物体");
        }
    }
}