/*Name:		 				E_ShortcutKey	
 *Description: 				
 *Author:       			李文博 
 *Date:         			2019-11-
 *Copyright(C) 2018 by 		北京兆泰源信息技术有限公司*/
using UnityEngine;
using UnityEditor;
using System.Collections;


public class ShortcutKey_ToRoot : ScriptableObject
{

    public const string ToRootHierarchy="W_/放到根目录下 %w";
    //%对应Ctrl,#对应Shift &对应Alt f对应F


    //根据当前有没有选中物体来判断可否用快捷键
    [MenuItem(ToRootHierarchy, true)]
    static bool ValidateSelectEnableDisable()
    {
        GameObject[] go = GetSelectedGameObjects() as GameObject[];

        if (go == null || go.Length == 0)
            return false;
        return true;
    }

    [MenuItem(ToRootHierarchy)]
    static void ToRoot()
    {
        GameObject[] gos = GetSelectedGameObjects() as GameObject[];
        foreach (GameObject go in gos)
        {
            Undo.SetTransformParent(go.transform,null, "toRoot" + go.transform.name);
            go.transform.SetParent(null);
        }
    }
    //获得选中的物体
    static GameObject[] GetSelectedGameObjects()
    {
        return Selection.gameObjects;
    }

}