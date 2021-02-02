/************************************************************
  FileName: DeleteMissingScripts.cs
  Author:末零       Version :1.0          Date: 2018-7-13
  Description:删除所有Miss的脚本
************************************************************/

using UnityEngine;
using UnityEditor;
using TMPro;

public class E_DeleteMissingScripts : MonoBehaviour
{

    [MenuItem("W_/Delete Missing Scripts")]
    static void CleanupMissingScript()
    {
        GameObject[] pAllObjects = (GameObject[])Resources.FindObjectsOfTypeAll(typeof(GameObject));
        int r;
        int j;
        int count = 0;
        for (int i = 0; i < pAllObjects.Length; i++)
        {
            if (pAllObjects[i].hideFlags == HideFlags.None)//HideFlags.None 获取Hierarchy面板所有Object
            {
                var components = pAllObjects[i].GetComponents<Component>();
                var serializedObject = new SerializedObject(pAllObjects[i]);
                var prop = serializedObject.FindProperty("m_Component");
                r = 0;
                for (j = 0; j < components.Length; j++)
                {
                    if (components[j] == null)
                    {
                        prop.DeleteArrayElementAtIndex(j - r);
                        r++;
                        count = r;
                    }
                }
                serializedObject.ApplyModifiedProperties();
            }
        }
        Debug.Log("清除了"+ count + "个空脚本");
    } 
}
