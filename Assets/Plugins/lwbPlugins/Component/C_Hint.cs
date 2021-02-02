/*Name:					W_Hint		
 *Description: 			对象激活后，过timer秒后自动隐藏
 *Author:       		阿文
 *Date:         		2017-11-21
 *Copyright(C) 2017 by 	北京兆泰源信息技术有限公司*/

using UnityEngine;
namespace W
{
    public class C_Hint : MonoBehaviour
    {
        public float timerInterval = 2f;
        float timer;
        public bool activeByMethord = false;
        public bool showOnAwake = false;
        private void Awake()
        {

            //timer = timerInterval;
        }
        private void OnEnable()
        {
            if (!activeByMethord)
            {
                timer = timerInterval;
                if (!showOnAwake)
                {
                    SetActive(false);
                }
            }
        }
        private void OnDisable()
        {
            activeByMethord = false;
        }
        public void FixedUpdate()
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                gameObject.SetActive(false);
            }
        }


        public void SetActive(bool active)
        {
            if (active)
            {
                activeByMethord = true;
                timer = timerInterval;
                gameObject.SetActive(true);
            }
            else
            {
                timer = 0;
                activeByMethord = false;
                gameObject.SetActive(false);
            }
        }
        public void SetActive(bool active, float hideTimer)
        {
            if (active)
            {
                activeByMethord = true;
                timer = hideTimer;
                gameObject.SetActive(true);
            }
            else
            {
                activeByMethord = false;
                gameObject.SetActive(false);
            }
        }
    }
}