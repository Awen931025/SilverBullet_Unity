using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace W
{
    public class C_FindSameNameButton : MonoBehaviour
    {
        public List<Button> list = new List<Button>();
        public List<string> btnNameS = new List<string>();
        [InspectorButton("DebugSameNameButton")]
        public string 打印所有重名的Button = "";
        public void DebugSameNameButton()
        {
            list.Clear();
            btnNameS.Clear();
            list = U_FindAll.GetAllT<Button>();
            int i = 0;
            foreach (var item in list)
            {
                if (!btnNameS.Contains(item.name))
                {
                    btnNameS.Add(item.name);
                }
                else
                {
                    i++;
                    Debug.Log(item.name + "   " + U_Transform.GetHierarchyPathName(item.transform));
                }
            }
            Debug.Log("共有 " + i + " 个重名的Button");
        }
    }
}