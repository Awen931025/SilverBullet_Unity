/*Name:		 				W_HighlightOneByOne	
 *Description: 				他的孩子，带那个高亮插件的，一个接着一个闪
 *Author:       			李文博 
 *Date:         			2018-08-
 *Copyright(C) 2018 by 		北京兆泰源信息技术有限公司*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace W
{
    public class C_OutlineOneByOne : MonoBehaviour
    {
        public OutlineMode outlineMode;
        public W_OutlineMethord OutlineChange;
        public List<OutlineObj> hightlightObjects;
        public float timerInterval = 1.5f;

        public bool startOneByOne;
        int objectsIndex = 0;
        void OutlineModeSelection()
        {
            if (outlineMode == OutlineMode.长亮)
                OutlineChange = OutlineConstant;
            else
                OutlineChange = OutlineFlicker;
        }
        void GetObjects()
        {
            hightlightObjects = U_Component.GetChildrenComponents<OutlineObj>(transform);
        }
        private void Awake()
        {
            OutlineModeSelection();
            GetObjects();
        }
        public float flickerTimer;
        private void FixedUpdate()
        {
            OutlineOneByOne(hightlightObjects, ref flickerTimer);
        }
        public void OutlineOneByOne(List<OutlineObj> objects, ref float timer)
        {
            if (startOneByOne)
            {
                timer -= Time.deltaTime;
                if (timer <= 0)
                {
                    timer = timerInterval;
                    OutlineChange(objects[objectsIndex], false);
                    objectsIndex += 1;
                    if (objectsIndex < objects.Count)
                    {
                        OutlineChange(objects[objectsIndex], true);
                    }
                    else
                    {
                        objectsIndex = 0;
                        startOneByOne = false;
                    }
                }
            }
        }

        void OutlineFlicker(OutlineObj outline, bool isTrue)
        {
            outline.flicker = isTrue;
        }
        void OutlineConstant(OutlineObj outline, bool isTrue)
        {
            outline.constantly = isTrue;
        }
        public enum OutlineMode { 长亮, 闪烁, }
        public delegate void W_OutlineMethord(OutlineObj outline, bool isTrue);
    }
}