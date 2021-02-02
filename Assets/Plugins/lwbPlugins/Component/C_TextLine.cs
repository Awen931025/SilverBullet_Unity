using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace W
{
    public class C_TextLine : MonoBehaviour
    {
        [Separator("这个经测试最好是华文宋体-STSONG")]
        public Font font;
        Text underline;
        int lineCount;
        float textWidth;
        void TextInit()
        {
            Destroy线();
            Text text = GetComponent<Text>();
            underline = Instantiate(text) as Text;
            underline.font = font;
            underline.name = "Underline";
            underline.transform.SetParent(text.transform);
            RectTransform rt = underline.rectTransform;
            rt.anchoredPosition3D = Vector3.zero;
            rt.offsetMax = Vector2.zero;
            rt.offsetMin = Vector2.zero;
            rt.anchorMax = Vector2.one;
            rt.anchorMin = Vector2.zero;
            underline.transform.localScale = Vector3.one;
            underline.transform.localRotation = Quaternion.identity;
            textWidth = text.preferredWidth;
        }

        public void Add删除线()
        {
            TextInit();
            underline.fontStyle = FontStyle.Bold;
            underline.text = "—";
            lineCount = (int)Mathf.Round(textWidth / underline.preferredWidth) + 1;
            for (int i = 1; i < lineCount; i++)
            {
                underline.text += "—";
            }
        }

        public void Add下划线()
        {
            TextInit();
            underline.text = "_";
            lineCount = (int)Mathf.Round(textWidth / underline.preferredWidth) + 2;
            for (int i = 1; i < lineCount; i++)
            {
                underline.text += "_";
            }
        }

        void Destroy线()
        {
            if (transform.Find("Underline") == null)
                return;
            Destroy(transform.Find("Underline").gameObject);
            underline = null;
            lineCount = 0;
            textWidth = 0;
        }


    }
}