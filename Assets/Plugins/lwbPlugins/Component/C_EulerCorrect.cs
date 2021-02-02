/*Name:		 				CVR_CamerigEulerCorrect	
 *Description: 				强制校正某方向旋转度的，比rigibody管用
 *Author:       			lwb
 *Date:         			2019-06-
 *Copyright(C) 2019 by 		company@zhiwyl.com*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace W
{
    public class C_EulerCorrect : MonoBehaviour
    {
        public bool active = true;
        [Separator("冻住Z轴的旋转")]
        //public bool x = true;
        //public bool y = true;
        public bool z = false;

        private void LateUpdate()
        {
            if (!active)
                return;

            if (z)
            {
                transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, 0);
            }


        }
    }
}