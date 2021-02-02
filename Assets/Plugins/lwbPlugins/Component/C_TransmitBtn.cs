/*Name:		 				DC_Transmitbtn	
 *Description: 				挂在具体的导航按钮的预制件上
 *Author:       			lwb
 *Date:         			2019-06-
 *Copyright(C) 2019 by 		company@zhiwyl.com*/
using UnityEngine;
using UnityEngine.UI;

namespace W
{
    public class C_TransmitBtn : MonoBehaviour
    {
        Button btn;
        public C_TransmitObj transmit;
        public Transform posModel;
        public C_Transmit manager;
        Text text;

        private void Awake()
        {
            btn = GetComponent<Button>();
            btn.onClick.AddListener(OnClick);
        }
        public void Init()
        {
            btn = GetComponent<Button>();
            Create();
        }

        protected virtual void OnClick()
        {
            if (posModel == null)
            {
                Debug.LogError("没有引用posModel");
                return;
            }
            //原工程点击了，去相应的地方
            //WVR_Methord.SetEyeAngle(posModel.localEulerAngles.y);
            //WVR_Methord.SetEyePos(posModel.position);
            //DCS.Ins.transmitMenu.SetClose();
            //WVR_Methord.OpenConsPointer_R();
        }

        public void Create()
        {
            //原工程
            //text.text = transmit.transform.GetComponent<DC_EquipObj>().m_设备实体类.导航显示的名字;
            btn.transform.name = transmit.transform.name;
            text = btn.transform.GetComponentInChildren<Text>();
            switch (transmit.equipType)
            {
                case "汽机": transform.SetParent(manager.gridLayoutS[0].transform); break;
                case "锅炉": transform.SetParent(manager.gridLayoutS[1].transform); break;
                case "电除尘": transform.SetParent(manager.gridLayoutS[2].transform); break;
            }
            transform.localScale = Vector3.one;
        }
    }
}