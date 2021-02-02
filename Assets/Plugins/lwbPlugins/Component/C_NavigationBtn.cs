/*Name:		 				DC_NavigationBtn	
 *Description: 				
 *Author:       			lwb
 *Date:         			2019-06-
 *Copyright(C) 2019 by 		company@zhiwyl.com*/
using UnityEngine;
using UnityEngine.UI;

namespace W
{
    public class C_NavigationBtn : MonoBehaviour
    {
        public C_NavigationObj obj;
        public C_Navigation manager;

        public void AddClickListener()
        {
            Debug.Log("加入点击监听");
            GetComponent<Button>().onClick.AddListener(OnClick);
        }

        void OnClick()
        {
            Debug.Log("onClick：" + GetComponent<Button>().gameObject.name);
            manager.Show(obj);
        }
    }
}