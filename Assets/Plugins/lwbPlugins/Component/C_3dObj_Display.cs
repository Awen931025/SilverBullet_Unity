/*Name:		 				NewZhuan	
 *Description: 				
 *Author:       			李文博 
 *Date:         			2019-08-
 *Copyright(C) 2019 by 		智联友道*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace W
{
    [DisallowMultipleComponent]
    public class C_3dObj_Display : MonoBehaviour
    {
        private float OffsetX = 0;
        private float OffsetY = 0;
        public float speed = 3;//旋转速度

        public enum ObjRotateMode { 点物体, 点对应UI, 点任意地方 };
        public ObjRotateMode 旋转方式 = ObjRotateMode.点物体;

        RaycastHit objHit;
        Ray objRay;
        private bool isRotating;

        public bool allowChildrenDetect = true;
        public List<Transform> children;
        private void Start()
        {
            if (allowChildrenDetect)
                children = transform.GetChildren();
        }
        public void UpdateChildrenList()
        {
            children = transform.GetChildren();
        }
        void Update()
        {
            objRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Input.GetMouseButtonDown(0))
            {
                if (Physics.Raycast(objRay, out objHit, 100f))
                {
                    if (objHit.collider.gameObject == gameObject ||
                        allowChildrenDetect && children.Contains(objHit.collider.transform)
                        )
                    {
                        OnStartRotate();
                        isRotating = true;
                    }
                }
            }
            if (Input.GetMouseButtonUp(0))
            {
                if (isRotating)
                {
                    OnEndRotate();
                    isRotating = false;
                }
            }

            if (isRotating)
            {
                OffsetX = Input.GetAxis("Mouse X");//获取鼠标x轴的偏移量
                OffsetY = Input.GetAxis("Mouse Y");//获取鼠标y轴的偏移量
                transform.Rotate(new Vector3(OffsetY, -OffsetX, 0) * speed, Space.World);
            }
        }

        protected virtual void OnEndRotate()
        {
        }

        protected virtual void OnStartRotate()
        {
        }

    }
}