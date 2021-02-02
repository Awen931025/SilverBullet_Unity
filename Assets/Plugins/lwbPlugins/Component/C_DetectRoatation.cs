/*Name:		 				zhuanxuanTest
 *Description: 				
 *Author:       			lwb
 *Date:         			2019-10-
 *Copyright(C) 2019 by 		yoodao*/
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace W
{
    public class C_DetectRoatation : MonoBehaviour
    {
        protected virtual void Start()
        {
            initialRotation = transform.rotation;
            initialLocalRotation = transform.localRotation.eulerAngles;
        }
        private void FixedUpdate()
        {
            float oldValue = curValue;
            curValue = CalculateValue();
            if (oldValue != curValue)
            {
                //Debug.Log("oldValue："+ oldValue+ "   curValue：" + curValue);
                OnValueChanged();
            }
        }

        private void OnValueChanged()
        {
            if (curValue == 0)
            {
                Debug.Log("上");
            }
            else if (curValue == 25 && transform.localEulerAngles.z < 180)
            {
                Debug.Log("左");
            }
            else if (curValue == 25 && transform.localEulerAngles.z > 180)
            {
                Debug.Log("右");
            }
            else if (curValue == 50)
            {
                Debug.Log("下");

            }
        }

        public float curValue;
        bool subDirectionFound = false;
        Quaternion initialRotation;
        Vector3 initialLocalRotation;

        float CalculateValue()
        {
            //Debug.Log(transform.localRotation.eulerAngles.x);
            //Debug.Log(subDirectionFound+"       "+ initialRotation+"       "+ initialLocalRotation);

            if (!subDirectionFound)
            {
                float angleX = Mathf.Abs(transform.localRotation.eulerAngles.x - initialLocalRotation.x) % 90;
                angleX = Mathf.RoundToInt(angleX) >= 89 ? 0 : angleX;

                if (Mathf.RoundToInt(angleX) != 0)
                {
                    subDirectionFound = true;
                    //Debug.Log("标志位进来一次" + (transform.localRotation.eulerAngles.x - initialLocalRotation.x).ToString());
                }
            }

            float angle = transform.localRotation.eulerAngles.x - initialLocalRotation.x;
            angle = Mathf.Round(angle * 1000f) / 1000f;
            float calculatedValue = 0;
            if (angle > 0 && angle <= 180)
            {
                calculatedValue = 360 - Quaternion.Angle(initialRotation, transform.rotation);
                //calculatedValue = 360 - Quaternion.Angle(initialRotation, new Quaternion(0, 1, 0, 0.1f));
            }
            else
            {
                calculatedValue = Quaternion.Angle(initialRotation, transform.rotation);
                //下边保证输出的curValue跟之前一样了，但还是不行，那就是在别的地方。
                //calculatedValue = Quaternion.Angle(initialRotation, new Quaternion(0, 1, 0, 0.1f));
            }

            calculatedValue = Mathf.Round(0 + Mathf.Clamp01(calculatedValue / 360f) * (100 - 0));
            if (0 > 100 && angle != 0)
            {
                calculatedValue = 100 + 0 - calculatedValue;
            }
            //Debug.Log(initialRotation+ "     transform.rotation：" + transform.rotation + "       value"+calculatedValue);

            return calculatedValue;
        }
    }
}