using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

public class U_Console
{
    public static void ClearDenbug(bool isDebugResult=false)
    {
        //xg这个地方影响发布
#if UNITY_EDITOR
        Assembly assembly = Assembly.GetAssembly(typeof(SceneView));
        Type logEntries = assembly.GetType("UnityEditor.LogEntries");
        MethodInfo clearConsoleMethod = logEntries.GetMethod("Clear");
        clearConsoleMethod.Invoke(new object(), null);

        if (isDebugResult)
        {
            Debug.Log("clear");
        }
#endif
    }
}
