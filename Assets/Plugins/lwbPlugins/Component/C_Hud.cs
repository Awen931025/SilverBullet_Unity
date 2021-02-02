/*Name:		 				C_Hud	
 *Description: 				
 *Author:       			lwb
 *Date:         			2019-06-
 *Copyright(C) 2019 by 		company@zhiwyl.com*/
using UnityEngine;
using UnityEngine.UI;
using W_Enum;

namespace W
{
    public class C_Hud : MonoBehaviour
    {


        public bool is连接在一个新生成的物体上 = true;
        public Transform target;
        C_Line cLine;
        public RectTransform cornerTran;
        public bool drawLine = false;
        public Color color = Color.blue;

        public float yAngles = -90;
        [InspectorButton("CreateContectObj")]
        public string 生成连接点;

        [InspectorButton("Fun命名为父物体的名称")]
        public string 命名为父物体的名称;
        void Fun命名为父物体的名称()
        {
            if (transform.Find(transform.name + "——连接物") != null)
            {
                createdObjName = transform.name.Replace(" hud", "");
                transform.Find(transform.name + "——连接物").name = createdObjName;
            }
        }
        string createdObjName;
        void CreateContectObj()
        {

            if (is连接在一个新生成的物体上)
            {
                if (transform.Find(transform.name + "——连接物") != null)
                {
                    DestroyImmediate(transform.Find(transform.name + "——连接物").gameObject);
                }
                createdObjName = transform.name.Replace(" hud", "");
                if (transform.Find(createdObjName) != null)
                {
                    DestroyImmediate(transform.Find(createdObjName).gameObject);
                }
                Transform temp = GameObject.CreatePrimitive(PrimitiveType.Sphere).transform;
                target = temp;
                DestroyImmediate(temp.GetComponent<Collider>());
                temp.SetParent(transform);
                temp.name = transform.name + "——连接物";
                temp.localScale = Vector3.one * 0.01f;
                temp.localPosition = new Vector3(0, 0.1f, 0);
            }
            else
            {


            }
        }


        private void Awake()
        {
            cLine = transform.GetComponentInChildren<C_Line>(true);
            cLine.startTran = cornerTran;
            SetLine(true, Corner.左中);
            offset = transform.position - target.position;
            C_Line cline = GetComponentInChildren<C_Line>();
            if (cline != null)
            {
                cline.endTran = target;
            }
            if (is连接在一个新生成的物体上)
            {
                transform.GetChild(2).gameObject.SetActive(false);
            }
        }
        public string textContent;



        private void OnEnable()
        {
            if (string.IsNullOrEmpty(textContent))
            {
                textContent = target.name;
            }
            transform.GetComponentInChildren<Text>(true).text = textContent;
            if (drawLine)
            {
                UpdatePosAndLine();
                HudFollow();
            }
            else
            {
                cLine.drawLine = false;
            }
        }
        [Header("竖直方向提升一个等级需要的offset差，需要比他的中点高多少")]
        public float heightChangeStep = 0.2f;
        public float widthChangeStep = 0.1f;

        Vector3 offset;
        public LookAtMode lookAtMode = LookAtMode.无;
        public Transform player;
        public void HudFollow()
        {
            transform.position = target.position + offset;
            if (player != null)
            {
                if (lookAtMode == LookAtMode.看向)
                {
                    transform.LookAt(player);
                }
                else if (lookAtMode == LookAtMode.Y轴跟着旋转)
                {
                    transform.eulerAngles = new Vector3(0, player.localEulerAngles.y + yAngles, 0);
                }
            }
        }
        public void SetActive(bool active)
        {
            gameObject.SetActive(active);
        }
        public void SetEnable(bool enable)
        {
            drawLine = enable;
        }

        public void SetC_Line()
        {
            if (cLine == null)
                cLine = transform.GetComponentInChildren<C_Line>(true);
            cLine.startTran = cornerTran;
            cLine.endTran = target;
            cLine.color = color;
        }
        /// 根据相对位置设置corner
        public void UpdatePosAndLine()
        {
            if (cLine == null)
            {
                cLine = transform.GetComponentInChildren<C_Line>();
            }
            SetLineCorner(Corner.右中);
            float rightDistance = (cornerTran.position - target.position).magnitude;
            SetLineCorner(Corner.左中);
            float leftDistance = (cornerTran.position - target.position).magnitude;

            Vector3 tranPos = transform.position;
            Vector3 targetPos = target.position;
            if (leftDistance - rightDistance > widthChangeStep)
            {
                if (tranPos.y - targetPos.y < -heightChangeStep)
                {
                    SetLine(true, Corner.右上);
                }
                else
                {
                    SetLine(true, Corner.右下);
                }
            }
            else if (rightDistance - leftDistance > widthChangeStep)
            {
                if (tranPos.y - targetPos.y < -heightChangeStep)
                {
                    SetLine(true, Corner.左上);
                }
                else
                {
                    SetLine(true, Corner.左下);
                }
            }
            else
            {
                if (tranPos.y - targetPos.y < -heightChangeStep)
                {
                    SetLine(true, Corner.上);
                }
                else
                {
                    SetLine(true, Corner.下);
                }
            }

        }


        public void SetLine(bool active, Corner corner)
        {
            SetLineCorner(corner);
            cLine.SetEnable(active);
        }

        public void SetLineCorner(Corner corner)
        {
            U_RectTransform.SetPos(cornerTran, corner);
        }


    }
}