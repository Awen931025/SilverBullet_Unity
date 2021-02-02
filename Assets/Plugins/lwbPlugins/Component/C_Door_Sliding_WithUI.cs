/*Name:		 				C_Door_Sliding_WithUI
 *Description: 				
 *Author:       			lwb
 *Date:         			2019-08-
 *Copyright(C) 2019 by 		yoodao*/

using UnityEngine;
using UnityEngine.UI;

namespace W
{
    public class C_Door_Sliding_WithUI : C_Door_Sliding
    {
        [Separator("C_Door_Sliding_WithUI")]
        public Button btnOpen;
        public Button btnClose;
        public Button btnPause;
        [HideInInspector]
        public Transform canvas;


        [InspectorButton("ShowCanvas", 6)]
        public string 显示canvas;
        [InspectorButton("HideCanvas", 6)]
        public string 隐藏canvas;

        public override void Start()
        {
            if (doorL == null)
            {
                doorL = transform.Find("左扇");
            }
            if (doorR == null)
            {

                doorR = transform.Find("右扇");
            }
            if (doorR == null)
            {

                doorR = transform.Find("右扇(十字花钥匙)");
            }
            YYObjInit();

            doorLOriPos = doorL.localPosition;
            doorROriPos = doorR.localPosition;
        }
        public void ShowCanvas()
        {
            if (canvas == null)
            {
                canvas = transform.Find("单独开关门UI");
            }
            canvas.gameObject.SetActive(true);
        }
        public void HideCanvas()
        {
            if (canvas == null)
            {
                canvas = transform.Find("单独开关门UI");
            }
            canvas.gameObject.SetActive(false);
        }

        /// <summary>
        /// 这个方法纯粹是为了少拖拽，名字先弄成死的
        /// </summary>
        void YYObjInit()
        {
            canvas = transform.Find("单独开关门UI");
            //canvas = transform.GetComponentInChildren<Canvas>(true).transform;
            if (btnOpen == null)
            {
                btnOpen = canvas.Find("btn开启").GetComponent<Button>();
            }
            if (btnClose == null)
            {
                btnClose = canvas.Find("btn关闭").GetComponent<Button>();
            }
            if (btnPause == null)
            {
                btnPause = canvas.Find("btn暂停").GetComponent<Button>();
            }
            btnOpen.onClick.AddListener(() => OpenDoor());
            btnClose.onClick.AddListener(() => CloseDoor());
            btnPause.onClick.AddListener(PauseOpeningDoor);
        }


    }
}