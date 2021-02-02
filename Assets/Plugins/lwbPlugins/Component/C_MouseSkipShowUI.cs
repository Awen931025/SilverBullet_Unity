/*Name:		 				C_MouseSkipShowUI	
 *Description: 				鼠标悬停
 *Author:       			李文博 
 *Date:         			2019-08-
 *Copyright(C) 2019 by 		智联友道*/
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace W
{
    public class C_MouseSkipShowUI : C_MouseBase
    {

        Text noticeUIText;
        public Image noticeUIImage;
        GameObject noticeCanvas;
        [Header("离开继续停留时间")]
        [Range(0, 1)]
        public float hideWaitTimer = 0.3f;
        protected override void Awake()
        {
            base.Awake();
            noticeUIText = noticeUIImage.GetComponentInChildren<Text>();
            HideNotice();
        }

        public void HideHint()
        {
            noticeUIImage.gameObject.SetActive(false);
        }
        protected override void EnterObj(GameObject go)
        {
            base.EnterObj(go);
            C_SkipUIObj obj = go.GetComponentInParent<C_SkipUIObj>();
            if (obj != null)
            {
                onObj = true;
                ShowNotice(obj.showConent);
            }
            else
            {
                onObj = false;
                StartCoroutine(IEHideUI());

            }
        }
        protected override void ExitObj(GameObject go)
        {
            base.ExitObj(go);
            C_SkipUIObj obj = go.GetComponentInParent<C_SkipUIObj>();
            if (obj != null)
            {
                //HideNotice();
                onObj = false;
                StartCoroutine(IEHideUI());
            }
        }
        public bool isFollowMouse = true;
        public void ShowNotice(string content)
        {
            //noticeCanvas.SetActive(true);
            noticeUIImage.gameObject.SetActive(true);
            noticeUIText.text = content;
            if (isFollowMouse)
            {
                noticeUIImage.GetComponent<RectTransform>().position = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);
            }
        }
        bool onObj = false;
        IEnumerator IEHideUI()
        {
            yield return new WaitForSeconds(hideWaitTimer);
            if (!onObj)
            {
                noticeUIImage.gameObject.SetActive(false);
            }
        }
        public void HideNotice()
        {
            noticeUIImage.gameObject.SetActive(false);
        }

    }
}