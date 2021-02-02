/*Name:		 				C_ObjInMap	
 *Description: 				
 *Author:       			李文博 
 *Date:         			2019-03-
 *Copyright(C) 2018 by 		北京兆泰源信息技术有限公司*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace W
{
    public class C_ObjInMap : MonoBehaviour
    {

        [Header("本脚本显示目标物体在地图中的相对位置")]
        [Header("用此脚本需要保证光圈的锚点等都是在左下角")]
        public Transform target;
        [Header("地图四条边的特征值")]
        float top = 2027f;
        float bottom = -61f;
        float left = -26f;
        float right = 2005f;

        [Header("在什么上边显示，默认是父物体")]
        RectTransform bgPanel;

        RectTransform rectTran;
        float bgWidth;
        float bgHeight;
        float xDistance;
        float yDistance;
        float xRate;
        float yRate;
        private void Awake()
        {
            rectTran = transform.GetComponent<RectTransform>();
            bgPanel = transform.parent.GetComponent<RectTransform>();
            bgWidth = bgPanel.rect.width;
            bgHeight = bgPanel.rect.height;
        }

        void GetDistanceVector2()
        {
            xDistance = target.position.x - left;
            yDistance = target.position.z - bottom;
            xRate = xDistance / (right - left);
            yRate = yDistance / (top - bottom);
            rectTran.anchoredPosition = new Vector3(xRate * bgWidth, yRate * bgHeight);
        }
        private void Update()
        {
            GetDistanceVector2();
        }

    }
}