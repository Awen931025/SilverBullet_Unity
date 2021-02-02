/*Name:		 				C_Follow	
 *Description: 				
 *Author:       			lwb 
 *Date:         			2019-08-27
 *Copyright(C) 2018 by 		yoodao*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace W
{
    public class C_Follow : MonoBehaviour
    {
        public Transform target;
        public bool isFollow = true;
        //[Header("是否成为目标的子物体")]
        //public bool setParent = false;

        Vector3 relativeDistance;
        protected virtual void Awake()
        {
            relativeDistance = transform.position - target.position;
        }
        private void Update()
        {
            if (!isFollow)
                return;
            transform.position = target.position + relativeDistance;
        }
    }
}