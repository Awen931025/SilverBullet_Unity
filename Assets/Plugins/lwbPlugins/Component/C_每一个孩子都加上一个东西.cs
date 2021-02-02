using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace W
{
    public class C_每一个孩子都加上一个东西 : MonoBehaviour
    {
        public List<Transform> objS = new List<Transform>();

        [InspectorButton("Add")]
        public string 每一个孩子都加上一个东西;

        void Add()
        {
            foreach (Transform item in transform)
            {
                for (int i = 0; i < objS.Count; i++)
                {
                    Transform temp = Instantiate(objS[i], item);
                    temp.transform.localPosition = objS[i].transform.localPosition;
                    temp.transform.localRotation = objS[i].transform.localRotation;
                    temp.transform.localScale = objS[i].transform.localScale;
                    temp.transform.name = objS[i].transform.name;
                }
            }
        }

        [InspectorButton("ToSameTransfrom")]
        public string 同名的位置角度相同;
        public string 后缀 = "old";
        void ToSameTransfrom()
        {
            foreach (Transform item in transform)
            {
                foreach (Transform old in transform)
                {
                    if (old.name == item.name + 后缀)
                    {
                        item.position = old.position;
                        item.rotation = old.rotation;
                        item.localScale = old.localScale;
                    }
                }
            }
        }
    }
}