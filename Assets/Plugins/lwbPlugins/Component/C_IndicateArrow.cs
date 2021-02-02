/*Name:		 				C_IndicateArrow	
 *Description: 				
 *Author:       			李文博 
 *Date:         			2019-03-
 *Copyright(C) 2018 by 		北京兆泰源信息技术有限公司*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace W
{
    public class C_IndicateArrow : MonoBehaviour
    {

        #region
        [Header("箭头跟随的目标")]
        public Transform followTran;
        [Header("观察箭头者")]
        public Transform player;

        float height = 2;
        public float minScale = 0.03f;
        private float maxScale = 0.5f;
        [Header("远大近小的缩放系数")]
        public int scaleFactor = 3;

        [Header("旋转速度")]
        [Range(0.2f, 2)]
        public float rotateSpeed = 1;
        [Header("下落速度")]
        [Range(0.2f, 2)]
        public float dropSpeed = 1;

        float distance = 1;

        public Material mat;


        protected virtual void Start()
        {
            SetMat(mat);
        }
        public virtual void SetMat(Material mat)
        {
            if (mat == null)
                return;
            Transform tra = transform.GetChild(0);
            Renderer[] renderers = transform.GetChild(0).GetComponentsInChildren<Renderer>();
            foreach (Renderer renderer in renderers)
            {
                renderer.material = mat;
            }
        }

        public float Distance
        {
            get
            {
                distance = (followTran.position - player.position).magnitude;
                return distance;
            }
        }

        #endregion

        protected virtual void Update()
        {
            Rotate();
            FollowTarget(height);
            FarAndNear_Scale();
            MoveAnimation_Y();
        }

        protected virtual void Rotate()
        {
            transform.Rotate(Vector3.up, 2.5f * rotateSpeed);
        }
        protected virtual float MoveAnimation_Y()
        {
            float yOffset = Mathf.PingPong(Time.time * Distance * 0.1f * dropSpeed, height * Distance * 0.06f);
            return height + yOffset;

        }
        protected virtual void FollowTarget(float height)
        {
            transform.position = followTran.position + MoveAnimation_Y() * Vector3.up;
        }

        protected virtual void FarAndNear_Scale()
        {
            float scale = Distance * 0.002f * scaleFactor;
            if (Distance < 3)
                scale = minScale;
            else if (Distance > 100)
            {
                scale = maxScale;
            }
            transform.localScale = Vector3.one * scale;

        }



    }
}