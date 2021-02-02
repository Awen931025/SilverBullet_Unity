/*Name:		 				C_Material	
 *Description: 				
 *Author:       			lwb
 *Date:         			2019-06-
 *Copyright(C) 2019 by 		company@zhiwyl.com*/
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace W
{
    public class C_Material : MonoBehaviour
    {

        public Material tarMat;

        [InspectorButton("ChangeUIMatToChuanTou")]
        public string 所有子物体UI改成上面的材质;
        public void ChangeUIMatToChuanTou()
        {
            RawImage[] rawImageS = transform.GetComponentsInChildren<RawImage>(true);
            foreach (RawImage item in rawImageS)
            {
                if (item.material.name.Contains("Default UI Material"))
                {
                    item.material = tarMat;
                }
            }

            Image[] imageS = transform.GetComponentsInChildren<Image>(true);
            foreach (Image item in imageS)
            {
                if (item.material.name.Contains("Default UI Material"))
                {
                    item.material = tarMat;
                }
            }

            Text[] textS = transform.GetComponentsInChildren<Text>(true);
            foreach (Text item in textS)
            {
                if (item.material.name.Contains("Default UI Material"))
                {
                    item.material = tarMat;
                }
            }
            Debug.Log("UI材质替换完成");
        }








        [Separator("改贴图颜色")]
        public Color dise = new Color32(224, 224, 206, 255);
        [InspectorButton("ChangTexture")]
        public string 有贴图的底色改成上边颜色 = "";
        public void ChangTexture()
        {
            List<Transform> listTran = transform.GetChildren();
            for (int i = 0; i < listTran.Count; i++)
            {
                if (listTran[i].transform.GetComponent<Renderer>() == null)
                    continue;
                Material[] metS = listTran[i].transform.GetComponent<Renderer>().sharedMaterials;

                foreach (var met in metS)
                {
                    if (met.mainTexture != null)
                    {
                        met.SetColor("_Color", dise);
                    }
                }

            }
        }

    }
}