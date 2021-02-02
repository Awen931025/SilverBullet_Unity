/*Name:		 				C_3dObjDrag	
 *Description: 				
 *Author:       			李文博 
 *Date:         			2019-08-
 *Copyright(C) 2019 by 		智联友道*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace W
{
    public class C_3dObjDrag : MonoBehaviour
    {
        bool isDragging = false;
        Vector3 screenSpace;
        Vector3 resPos;
        RaycastHit objHit;
        Ray objRay;
        public bool allowChildrenDetect = true;

        List<Transform> children;
        protected virtual void Start()
        {
            if (allowChildrenDetect)
                children = transform.GetChildren();

        }
        void Update()
        {
            objRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Input.GetMouseButtonDown(0))
            {
                if (Physics.Raycast(objRay, out objHit, 100f))
                {
                    if (objHit.collider.gameObject == gameObject
                        ||
                        allowChildrenDetect && children.Contains(objHit.collider.transform))
                    {
                        OnStartDrag();
                        isDragging = true;
                        screenSpace = Camera.main.WorldToScreenPoint(transform.position);
                    }
                }
            }
            if (isDragging)
            {
                var curScreenSpace = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenSpace.z);
                resPos = Camera.main.ScreenToWorldPoint(curScreenSpace);
                transform.position = resPos;
            }
            if (Input.GetMouseButtonUp(0))
            {
                if (isDragging)
                {
                    OnEndDrag();
                    isDragging = false;
                }
            }
        }

        public Vector3 oriPos;
        protected virtual void Awake()
        {
            oriPos = transform.position;
        }

        public void SetEnable(bool enable)
        {
            enabled = enable;
        }

        public void ToOriPos()
        {
            transform.position = oriPos;
        }
        protected virtual void OnStartDrag()
        {

        }
        protected virtual void OnEndDrag()
        {

        }



    }
}