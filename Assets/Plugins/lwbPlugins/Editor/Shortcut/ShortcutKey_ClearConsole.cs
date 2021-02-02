/*Name:		 				E_ShortcutKey	
 *Description: 				
 *Author:       			李文博 
 *Date:         			2019-11-
 *Copyright(C) 2018 by 		北京兆泰源信息技术有限公司*/
using UnityEngine;
//引入unity编辑器命名空间
using UnityEditor;
using System.Collections;
using System.Reflection;
using System;
namespace W
{
    public static class ShortcutKey_ClearConsole
    {
    //%对应Ctrl,#对应Shift &对应Alt f对应F
        [MenuItem("Tools/Clear Console %e")] // Ctrl + ALT + C 避免与唤出控制台的快捷方式冲突
        public static void ClearConsole()
        {
            Assembly assembly = Assembly.GetAssembly(typeof(SceneView));
            Type logEntries = assembly.GetType("UnityEditor.LogEntries");
            MethodInfo clearConsoleMethod = logEntries.GetMethod("Clear");
            clearConsoleMethod.Invoke(new object(), null);
        }
    }

}
