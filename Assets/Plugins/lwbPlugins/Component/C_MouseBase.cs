/*Name:		 				C_BaseMono	
 *Description: 				需要Button什么的是有导航的，配套那个Fix_But
 *Author:       			李文博 
 *Date:         			2019-05-14
 *Copyright(C) 2019 by 		智网易联*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace W
{
    public class C_MouseBase : MonoBehaviour
    {

        #region File
        [Header("悬停监视的画布,目前这种写法，将UI和OBJ集合到一起，UI是拖进来一个管一个，物体始终管")]
        public GraphicRaycaster graphicRaycaster;


        [HideInInspector]
        public GameObject hoverUI;
        [HideInInspector]
        public GameObject lastHoveredUI;

        [HideInInspector]
        public GameObject hoverObj;

        [HideInInspector]
        public GameObject lastHoveredObj;
        [Header("点击在")]
        [HideInInspector]
        public GameObject clicked;
        [HideInInspector]
        public GameObject lastClicked;

        PointerEventData pointerEventData;
        RaycastHit objHit;
        Ray objRay;
        [HideInInspector]
        public bool onUI;

        #endregion

        #region Cycle
        protected virtual void Awake()
        {
            pointerEventData = new PointerEventData(EventSystem.current);
        }

        protected virtual void Update()
        {
            InputKeyboard();
            Ray();
            Hover();
            Click();
        }
        #endregion


        #region public
        public void SetCanvas(GraphicRaycaster raycaster)
        {
            graphicRaycaster = raycaster;
        }
        #endregion

        public enum CursorMode { 鼠标, 屏幕中心点 }
        public CursorMode cursorMode = CursorMode.鼠标;
        #region Main Private




        void Ray()
        {
            if (cursorMode == CursorMode.鼠标)
            {
                pointerEventData.position = Input.mousePosition;
                objRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            }
            else if (cursorMode == CursorMode.屏幕中心点)
            {
                Vector2 v = new Vector2(Screen.width / 2, Screen.height / 2);  //屏幕中心点
                objRay= Camera.main.ScreenPointToRay(v);
            }
        }

        void Hover()
        {
            List<RaycastResult> uiRaycastResults = new List<RaycastResult>();
            graphicRaycaster.Raycast(pointerEventData, uiRaycastResults);
            if (uiRaycastResults.Count != 0)
            {
                if (lastHoveredObj != null)
                {
                    hoverObj = null;
                    ExitObj(lastHoveredObj);
                    lastHoveredObj = hoverObj;
                }
                hoverUI = uiRaycastResults[0].gameObject;
                Hover_UI(hoverUI);
            }
            else
            {
                hoverUI = null;
                if (lastHoveredUI != hoverUI)
                {
                    ExitUI(lastHoveredUI);
                    lastHoveredUI = hoverUI;
                }
            }

            //UI进模型没事，模型进UI不行
            if (uiRaycastResults.Count == 0)
            {
                if (Physics.Raycast(objRay, out objHit, 100f))
                {
                    hoverObj = objHit.collider.gameObject;
                    Hover_OBJ(objHit);
                }
                else
                {
                    hoverObj = null;
                    if (lastHoveredObj != hoverObj)
                    {
                        ExitObj(lastHoveredObj);
                        lastHoveredObj = hoverObj;
                    }
                }
            }
        }


        void Click()
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (EventSystem.current.IsPointerOverGameObject())
                {
                    if (EventSystem.current.currentSelectedGameObject != null)
                    {
                        clicked = EventSystem.current.currentSelectedGameObject;
                        Click_UI(clicked);
                        onUI = true;
                    }
                }
                else
                {
                    if (Physics.Raycast(objRay, out objHit, 10f))
                    {
                        clicked = objHit.collider.gameObject;
                        Click_OBJ(objHit);



                     
                    }
                    else
                    {
                        clicked = null;
                    }
                }
                lastClicked = clicked;
            }
            if (Input.GetMouseButtonUp(0))
            {
                onUI = false;
            }
            if (Input.GetMouseButton(1))
            {

            }
        }
        #endregion


        #region virtual
        protected virtual void InputKeyboard()
        {

        }

        protected virtual void EnterUI(GameObject go)
        {
            //Debug.Log("进入" + go.name);
        }
        protected virtual void ExitUI(GameObject go)
        {
            //Debug.Log("离开" + go.name);
        }


        protected virtual void EnterObj(GameObject go)
        {
            //Debug.Log("进入" + go.name);
        }
        protected virtual void ExitObj(GameObject go)
        {
            //Debug.Log("离开" + go.name);
        }



        protected virtual void Hover_UI(GameObject go)
        {
            if (hoverUI != null && lastHoveredUI != hoverUI && hoverUI.GetComponent<RectTransform>() != null)
            {
                EnterUI(hoverUI);
                if (lastHoveredUI != null)
                {
                    ExitUI(lastHoveredUI);
                }
            }
            lastHoveredUI = hoverUI;
        }
        protected virtual void Hover_OBJ(RaycastHit hit)
        {
            hoverObj = hit.collider.gameObject;
            if (hoverObj != null && lastHoveredObj != hoverObj && hoverObj.GetComponent<RectTransform>() == null)
            {
                if (lastHoveredObj != null)
                {
                    ExitObj(lastHoveredObj);
                }

                EnterObj(hoverObj);
            }
            lastHoveredObj = hoverObj;

            Hover_OBJ(hoverObj);
        }
        protected virtual void Hover_OBJ(GameObject go)
        {
           
        }



        #region Click
        protected virtual void Click_OBJ(RaycastHit hit)
        {
            GameObject go = hit.collider.gameObject;
            Click_OBJ(go);
        }
        protected virtual void Click_OBJ(GameObject go) { }
        protected virtual void Click_UI(GameObject go) { }
        #endregion

        #endregion
    }
}