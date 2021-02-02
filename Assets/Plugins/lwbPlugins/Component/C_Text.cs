/*Name:		 				C_Text	
 *Description: 				
 *Author:       			李文博 
 *Date:         			2018-08-
 *Copyright(C) 2018 by 		北京兆泰源信息技术有限公司*/
using UnityEngine;
using UnityEngine.UI;
namespace W
{
    public class C_Text : MonoBehaviour
    {

        [Header("需要显示的Text组件，不拖入就按本对象")]
        public Text text;
        protected virtual void Awake()
        {
            if (null == text)
            {
                text = gameObject.GetComponent<Text>();
            }
        }

        protected virtual void Update()
        {

        }

    }
}