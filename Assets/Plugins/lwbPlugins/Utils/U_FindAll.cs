/*Name:		 				U_FindAll
 *Description: 				
 *Author:       			lwb
 *Date:         			2019-09-
 *Copyright(C) 2019 by 		yoodao*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using System;
using System.Reflection;
namespace W
{
#if UNITY_EDITOR
    using UnityEditor;
#endif
    public class U_FindAll
    {
        public static void LoadPointLightByName<T>(object obj)
        {
            List<C_PointLight> lightS = GetAllT<C_PointLight>();
            Type type = typeof(T);
            FieldInfo[] fields = type.GetFields();
            foreach (var item in fields)
            {
                if (item.Attributes != FieldAttributes.Public)
                    return;
                //Debug.Log("外" + item.Name);
                if (item.FieldType == typeof(C_PointLight))
                {
                    foreach (var light in lightS)
                    {
                        if (light.name == item.Name)
                        {
                            item.SetValue(obj, light);
                        }
                    }
                }
            }
            Debug.Log("相应的C_PointLight的已经拖拽完毕");
        }

        public static void LoadUIByName<T>(object obj)
        {
            List<Button> btnS = GetAllT<Button>();
            List<Toggle> togS = GetAllT<Toggle>();
            List<RectTransform> panelS = GetAllT<RectTransform>();
            List<Slider> sliderS = GetAllT<Slider>();
            Type type = typeof(T);
            FieldInfo[] fields = type.GetFields();
            foreach (var item in fields)
            {
                if (item.Attributes != FieldAttributes.Public)
                    return;
                //Debug.Log("外" + item.Name);
                if (item.FieldType == typeof(Button))
                {
                    foreach (var btn in btnS)
                    {
                        if (btn.name == item.Name)
                        {
                            item.SetValue(obj, btn);
                        }
                    }
                }
                else if (item.FieldType == typeof(Toggle))
                {
                    foreach (var tog in togS)
                    {
                        if (tog.name == item.Name)
                        {
                            item.SetValue(obj, tog);
                        }
                    }
                }
                else if (item.FieldType == typeof(RectTransform))
                {
                    foreach (var panel in panelS)
                    {
                        if (panel.name == item.Name)
                        {
                            item.SetValue(obj, panel);
                        }
                    }
                }
                else if (item.FieldType == typeof(Slider))
                {
                    foreach (var slider in sliderS)
                    {
                        if (slider.name == item.Name)
                        {
                            item.SetValue(obj, slider);
                        }
                    }
                }
            }
            Debug.Log("符合条件button、toggle、slider、panel的已经拖拽完毕");
        }

        public static List<T> GetAllT<T>()
        {
            List<T> tS = new List<T>();
            List<GameObject> goS = new List<GameObject>();
#if UNITY_EDITOR
            goS = GetAllSceneObj();
#endif
            foreach (GameObject go in goS)
            {
                T t = go.GetComponent<T>();
                if (t != null)
                {
                    //不知道为啥，RectTransform为null时也会添加进去一个null
                    string tStr = t.ToString();
                    if (tStr != "null")
                    {
                        tS.Add(t);
                    }
                }
            }
            return tS;
        }
#if UNITY_EDITOR
        static List<GameObject> GetAllSceneObj()
        {
            var allTransforms = Resources.FindObjectsOfTypeAll(typeof(Transform));
            var previousSelection = Selection.objects;
            Selection.objects = allTransforms.Cast<Transform>()
                .Where(x => x != null)
                .Select(x => x.gameObject)
                //如果你只想获取所有在Hierarchy中被禁用的物体，反注释下面代码
                //.Where(x => x != null && !x.activeInHierarchy)
                .Cast<UnityEngine.Object>().ToArray();
            var selectedTransforms = Selection.GetTransforms(SelectionMode.Editable | SelectionMode.ExcludePrefab);
            Selection.objects = previousSelection;
            return selectedTransforms.Select(tr => tr.gameObject).ToList();
        }
#endif
    }
}