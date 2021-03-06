﻿/*Name:		 				U_RectTransform	
 *Description: 				UI尺寸工具类
 *Author:       			李文博 
 *Date:         			2018-08-
 *Copyright(C) 2018 by 		北京兆泰源信息技术有限公司*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using W_Enum;

namespace W
{
    public static class U_RectTransform
    {

        /// 这个的好处在于，直接激活UI顺带
        public static void SetUIActive()
        {

        }

        /// <summary>
        /// 获取当前物体所在的画布
        /// </summary>
        /// <param name="rect"></param>
        /// <returns></returns>
        public static Transform GetCanvas(Transform rect)
        {
            if (rect.root.GetComponent<Canvas>() != null)
                return rect.root;
            Canvas[] canvaS = rect.root.GetComponentsInChildren<Canvas>(true);
            if (canvaS.Length == 1)
                return canvaS[0].transform;
            else
            {
                foreach (Canvas can in canvaS)
                {
                    if (U_Transform.IsContainChildren(can.transform.GetChildren(), rect))
                    {
                        return can.transform;
                    }
                }
                return null;
            }
        }
        /// <summary>
        /// 和他父物体一样大
        /// </summary>
        public static void SetHefuqiYiyangda(RectTransform tran)
        {
            tran.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Left, 0, 0);
            tran.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Top, 0, 0);
            tran.anchorMin = Vector2.zero;
            tran.anchorMax = Vector2.one;
        }
        public static void SetMiddleMiddle(RectTransform tran)
        {
            tran.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Left, 0, 0);
            tran.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Top, 0, 0);
            tran.anchorMin = Vector2.zero;
            tran.anchorMax = Vector2.one;
        }

        public static void SetLeftBottom(RectTransform tran)
        {
            tran.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Left, 0, 0);
            tran.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Bottom, 0, 0);
            tran.anchorMin = Vector2.zero;
            tran.anchorMax = Vector2.one;
        }
        /// <summary>
        /// 需要锚点是中心点
        /// </summary>
        /// <param name="tran"></param>
        /// <param name="corner"></param>
        public static void SetPos(RectTransform tran, Corner corner)
        {
            Vector2 size = tran.parent.GetComponent<RectTransform>().sizeDelta;
            float with = size.x / 2;
            float height = size.y / 2;
            switch (corner)
            {
                case Corner.左上: tran.anchoredPosition = new Vector2(-with, height); break;

                case Corner.左中: tran.anchoredPosition = new Vector2(-with, 0); break;

                case Corner.左下: tran.anchoredPosition = new Vector2(-with, -height); break;

                case Corner.上: tran.anchoredPosition = new Vector2(0, height); break;

                case Corner.中: tran.anchoredPosition = new Vector2(0, 0); break;

                case Corner.下: tran.anchoredPosition = new Vector2(0, -height); break;

                case Corner.右上: tran.anchoredPosition = new Vector2(with, height); break;

                case Corner.右中: tran.anchoredPosition = new Vector2(with, 0); break;

                case Corner.右下: tran.anchoredPosition = new Vector2(with, -height); break;
            }
        }

        /// <summary>
        /// 返回某个物体的实际左下角位置
        /// </summary>
        /// <param name="rectTran"></param>
        /// <returns></returns>
        public static Vector2 GetReal_LeftDown(RectTransform rectTran)
        {
            Vector2 leftDown;
            Vector3[] corner4 = new Vector3[4];
            //左下、左上、右上、右下 
            rectTran.GetWorldCorners(corner4);
            float left = corner4[0].x;
            float down = corner4[0].y;
            leftDown = new Vector2(left, down);
            return leftDown;
        }


        /// <summary>
        /// 返回某个物体的实际尺寸，包含了像素和缩放。
        /// </summary>
        /// <param name="rectTran"></param>
        /// <returns></returns>
        public static Vector2 GetReal_Size(RectTransform rectTran)
        {
            Vector2 size;
            Vector3[] corner4 = new Vector3[4];
            //左下、左上、右上、右下 
            rectTran.GetWorldCorners(corner4);
            float width = corner4[2].x - corner4[0].x;
            float height = corner4[2].y - corner4[0].y;
            size = new Vector2(width, height);
            return size;
        }

        /// <summary>
        /// 获取一个空间在屏幕中的实际区域
        /// </summary>
        /// <param name="rectTran"></param>
        /// <returns></returns>
        public static Rect GetReal_Rect(RectTransform rectTran)
        {
            Rect rect;
            Vector3[] corner4 = new Vector3[4];
            rectTran.GetWorldCorners(corner4);
            rect = new Rect(corner4[0].x, corner4[0].y, corner4[2].x - corner4[0].x, corner4[2].y - corner4[0].y);
            return rect;
        }

    }
}