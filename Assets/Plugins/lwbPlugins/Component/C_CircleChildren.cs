/*Name:		 				C_CircleChildren
 *Description: 				
 *Author:       			lwb
 *Date:         			2019-08-
 *Copyright(C) 2019 by 		yoodao*/
using System.Collections;

using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
namespace W
{
    public class C_CircleChildren : MonoBehaviour
    {
        private int sonCount; //子物体个数
        private float angle = 0;//角度
        public float r = .2f;
        public bool activeWithDotween = true;
        public float aniDuration = 1f;
        public float oneByOneTimeInterval = 0.2f;
        C_CircleObj[] circleChildren;
        private void Start()
        {
            circleChildren = transform.GetComponentsInChildren<C_CircleObj>(true);
            foreach (var item in circleChildren)
            {
                item.transform.localPosition = Vector3.zero;
                item.transform.localScale = Vector3.zero;
            }
        }
        void OnEnable()
        {
            circleChildren = transform.GetComponentsInChildren<C_CircleObj>(true);
            sonCount = circleChildren.Length;
            CirclePailie();
        }
        void OnDisable()
        {
            if (activeWithDotween)
            {
                aniCount = 0;
                foreach (var item in circleChildren)
                {
                    item.transform.localPosition = Vector3.zero;
                    item.transform.localScale = Vector3.zero;
                }
            }

        }
        int aniCount = 0;
        public void CirclePailie()
        {
            for (int i = 0; i < sonCount; i++)
            {
                float hudu = angle / 180 * Mathf.PI;
                float xx = r * Mathf.Cos(hudu);
                float zz = r * Mathf.Sin(hudu);

                Vector3 tarPos = new Vector3(zz, xx, 0);
                //Vector3 tarPos = new Vector3(xx, zz, 0);
                //Vector3 tarPos = new Vector3(xx, zz, 0);



                ToPos(circleChildren[i].transform, tarPos, aniDuration, i * oneByOneTimeInterval);
                angle += 360 / sonCount;
                aniCount++;
            }
        }

        void ToPos(Transform tran, Vector3 pos, float duration, float delay)
        {
            if (activeWithDotween)
            {
                tran.DOLocalMove(pos, duration).SetDelay(delay);
                tran.DOScale(Vector3.one, duration).SetDelay(delay);
            }
            else
            {
                tran.localPosition = pos;

            }
        }
    }
}