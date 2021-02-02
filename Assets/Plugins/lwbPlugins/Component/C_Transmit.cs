/*Name:		 				DC_Transmit	
 *Description: 				
 *Author:       			lwb
 *Date:         			2019-06-
 *Copyright(C) 2019 by 		company@zhiwyl.com*/
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace W
{
    public class C_Transmit : MonoBehaviour
    {

        public List<C_TransmitObj> objS = new List<C_TransmitObj>();

        public C_TransmitBtn transmitBtnPrefab;
        public Transform posModelPrefab;
        public Transform posModelParent;

        public List<GridLayoutGroup> gridLayoutS = new List<GridLayoutGroup>();
        private void Awake()
        {
            posModelParent.gameObject.SetActive(false);
        }

        public void Remove()
        {
            foreach (GridLayoutGroup grid in gridLayoutS)
            {
                U_Transform.DestroyImmediateAllSon(grid.transform);
            }
            U_Transform.DestroyImmediateAllSon(posModelParent);
            objS.Clear();
        }

        [InspectorButton("CreateButton")]
        public string 生成btn和Pos = "";
        [InspectorButton("Remove")]
        public string 清空 = "";
        public void CreateButton()
        {
            Remove();
            foreach (C_TransmitObj item in objS)
            {
                C_TransmitBtn btn = Instantiate(transmitBtnPrefab);

                Transform posModel = Instantiate(posModelPrefab);
                posModel.SetParent(posModelParent);

                posModel.name = item.transform.name + "   modelPos";
                posModel.position = item.transform.position;


                btn.transmit = item;
                btn.manager = this;
                btn.posModel = posModel;

                btn.Init();

            }
        }



    }
}