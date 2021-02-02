/*Name:		 				U_3DMath
 *Description: 				
 *Author:       			lwb
 *Date:         			2019-10-
 *Copyright(C) 2019 by 		yoodao*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace W
{
    public class U_3DMath
    {
        public static void ConstantDistance(Transform moveObj, Transform staticObj, float distance)
        {
            Vector3 dir = (moveObj.position - staticObj.position).normalized;
            dir *= distance;
            moveObj.position = staticObj.position + dir;
        }
        public static void ConstantDistance_WithOutY(Transform moveObj, Transform staticObj, float distance)
        {
            //Vector3 dir = (moveObj.position - staticObj.position).normalized;
            Vector3 dir = (moveObj.position - staticObj.position).normalized;
            float oriY = moveObj.position.y - staticObj.position.y;
            dir *= distance;
            dir = new Vector3(dir.x, oriY, dir.z);
            moveObj.position = staticObj.position + dir;
            //Debug.Log("移动向量：" + dir);


            //float xOffset = moveObj.position.x - staticObj.position.x;
            //float zOffset = moveObj.position.z - staticObj.position.z;
            //moveObj.position = staticObj.position + new Vector3(xOffset,0,zOffset); 




            //float xOffset = moveObj.position.x - staticObj.position.x;
            //float zOffset = moveObj.position.z - staticObj.position.z;
            //float yOffset = moveObj.position.y - staticObj.position.y;
            //moveObj.position = staticObj.position + new Vector3(-1, yOffset, -1);
        }
    }
}