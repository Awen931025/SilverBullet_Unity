/*Name:		 				DC_Navigation	
 *Description: 				根据NavigationObj确定位置
 *Author:       			lwb
 *Date:         			2019-06-
 *Copyright(C) 2019 by 		company@zhiwyl.com*/
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace W
{
    public class C_Navigation : MonoBehaviour
    {
        public C_NavigationObj tempObj;

        [Header("对应excel表")]
        public string xlsxPathname = "";
        public Transform menu;
        [Header("导航按钮prefab")]
        public Button buttonPrefab;
        C_NavigationObj curObj;


        public Button returnBtn;
        /// 设备本身，这个通过Editor加入
        public List<C_NavigationObj> objS = new List<C_NavigationObj>();

        /// 设备对应的按钮,想想怎么跟设备对应上——（按钮是生成的，根据设备生成的，设备是获取的或者拖拽的）
        public List<Button> btnS = new List<Button>();
        ///设备对应的位置,这个给生成一个Cube，命名为 pos，在一个物体之下，命名为导航Pos，运行的时候关掉
        public List<Transform> posTrans = new List<Transform>();
        ///头的实际角度，这个可以考虑用算法取代，过去之后，就让他根据他与眼睛的距离，给值
        public List<float> angleS = new List<float>();

        [ContextMenu("GetLayer")]
        public void AddBtnS()
        {
            foreach (C_NavigationObj obj in objS)
            {
                obj.GetLayer();
                obj.GetSons(objS);
            }
        }
        private void Awake()
        {
            panelS = menu.GetSonS(false);
            returnBtn.onClick.AddListener(Return);
            foreach (C_NavigationObj obj in objS)
            {
                if (obj.layer == 1)
                {
                    firstlayer.Add(obj);
                }
            }
            Show(tempObj);
        }

        /// 点这个物体，出他的子菜单
        public void CreateButton(C_NavigationObj obj)
        {
            int layer = obj.layer;

            Transform parent = panelS[layer];
            foreach (C_NavigationObj son in obj.sonS)
            {
                Button btn = Instantiate(buttonPrefab, parent);
                btn.transform.name = son.name;
                btn.GetComponentInChildren<Text>().text = son.name;

                C_NavigationBtn objBtn = btn.GetComponent<C_NavigationBtn>();
                objBtn.manager = this;
                objBtn.obj = son;
                objBtn.AddClickListener();
                //btn.onClick.AddListener(()=>Show(obj));
            }
        }

        List<C_NavigationObj> firstlayer = new List<C_NavigationObj>();

        public void CreateFirstButton()
        {
            foreach (C_NavigationObj son in firstlayer)
            {
                Button btn = Instantiate(buttonPrefab, menu.GetChild(0));
                btn.transform.name = son.name;
                btn.GetComponentInChildren<Text>().text = son.name;


                C_NavigationBtn objBtn = btn.GetComponent<C_NavigationBtn>();
                objBtn.manager = this;
                objBtn.obj = son;
                objBtn.AddClickListener();
            }
        }

        public void DestroyButton(Transform tran)
        {
            List<Transform> sonS = tran.GetSonS();
            foreach (Transform son in sonS)
            {
                Destroy(son.gameObject);
            }
        }
        public void ShowFirst()
        {
            OpenPanel(0);
            CreateFirstButton();
        }
        public void Show(C_NavigationObj obj)
        {
            if (obj == null)
            {
                Debug.Log("传入了空");
                return;
            }
            if (obj.sonS != null && obj.sonS.Count == 0)
            {
                Debug.Log("点的是最下层的，不给反应");
                return;
            }
            curObj = obj;
            OpenPanel(curObj);
            CreateButton(curObj);
        }

        public void InActiveAllPanel()
        {
            foreach (Transform son in panelS)
            {
                son.gameObject.SetActive(false);
            }
        }
        public List<Transform> panelS = new List<Transform>();

        /// 0是第一级，1是第二级
        public void OpenPanel(int layer)
        {
            InActiveAllPanel();
            if (panelS.Count > layer)
            {
                panelS[layer].gameObject.SetActive(true);
                DestroyButton(panelS[layer]);
            }
            else
            {
                Debug.Log("没有这一层!");
            }
        }
        public void OpenPanel(C_NavigationObj obj)
        {
            OpenPanel(obj.layer);
        }


        public void Return()
        {
            if (curObj == null)
                return;
            int curPanel = GetCurrentShowLayer();
            if (curPanel == 1)
            {
                Debug.Log("关闭");
            }
            else if (curPanel == 2)
            {
                ShowFirst();
            }
            else
            {
                Show(curObj.parent);
            }
        }

        /// 0代表没显示，1代表第一级，2代表第二级
        public int GetCurrentShowLayer()
        {
            //获取当前正在激活的是第几级
            for (int i = 0; i < panelS.Count; i++)
            {
                if (panelS[i].gameObject.activeInHierarchy)
                {
                    return i + 1;
                }
            }
            return 0;
        }
    }
}