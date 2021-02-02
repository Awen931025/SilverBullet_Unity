/*Name:		 				U_Transform	
 *Description: 				Transfrom组件的工具类
 *Author:       			李文博 
 *Date:         			2018-08-16
 *Copyright(C) 2018 by 		北京兆泰源信息技术有限公司*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using W_Enum;
using System.Text;
using System.Text.RegularExpressions;

namespace W
{
    public static class U_Transform
    {
        public static float GetAngelWithoutY(Transform self, Transform tar)
        {
            Vector2 selfPos = new Vector2(self.position.x, self.position.z);
            Vector2 tarPos = new Vector2(tar.position.x, tar.position.z);
            Vector2 dir = selfPos - tarPos;
            Vector2 selfForward = new Vector2(self.forward.x, self.forward.z);
            float res = Vector2.Angle(selfForward, dir);
            res = Mathf.Abs(res - 180);
            //Debug.Log("夹角为：" + res);
            return res;
        }
        public static float GetAngel(Transform tran01, Transform tran02)
        {
            float res = 0;
            float dot = Vector3.Dot(tran01.right, tran02.up);
            //弧度转角度
            float angle = Mathf.Acos(dot) * Mathf.Rad2Deg;
            //叉乘求法线
            Vector3 dir = Vector3.Cross(tran01.right, tran02.up);
            angle = (Vector3.Dot(tran02.forward, dir) < 0 ? -1 : 1) * angle;
            Debug.Log("夹角为：" + angle);
            return res;
        }
        public static int Get括号内的Index(Transform tran)
        {
            string str = tran.name;
            str = str.Remove(str.Length - 1, 1);
            string[] strS = str.Split('(');
            str = strS[strS.Length - 1];
            int index = 0;
            if (U_String.Is数字(str))
            {
                index = int.Parse(str);
            }
            else
            {
                Debug.LogError("不符合末尾英文括号+数字的命名规则");
            }
            return index;
        }
        public static bool IsSon(Transform tran, Transform parent)
        {
            List<Transform> sons = parent.GetSonS(true);
            if (sons.Contains(tran))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static bool IsChild(Transform tran, Transform parent)
        {
            List<Transform> children = parent.GetChildren(true);
            if (children.Contains(tran))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 把兄弟级全关了，自己激活
        /// </summary>
        public static void InactiveBrother(Transform tran, bool activeSelf = true)
        {
            Transform parent = tran.parent;
            if (parent == null)
            {
                Debug.LogError("w没有父物体，用此方法必须有父物体");
                return;
            }
            foreach (Transform son in parent)
            {
                if (son == tran)
                {
                    if (activeSelf)
                        son.gameObject.SetActive(true);
                }
                else
                    son.gameObject.SetActive(false);
            }
        }

        /// <summary>
        /// 在他的直系父物体中找到目标tag的父物体，还没写
        /// </summary>
        /// <returns></returns>
        public static Transform GetParentWithTag(Transform tran, string tag)
        {
            return tran;
            //tran.getpa
        }

        /// 获取物体在面板上的名字
        public static string GetHierarchyPathName(Transform tran, string pathMark = @"\", string sameNameMark = "_", bool withScene = true)
        {
            string result = "";
            List<Transform> tranS = new List<Transform>();
            Transform root = tran.root;
            Transform temp = tran;
            for (int i = 0; i < 100; i++)
            {
                int sameNameIndex = 0;
                if (temp.parent != null)
                {
                    sameNameIndex = temp.GetSameNameBrotherS(true, true).IndexOf(temp);
                    //Debug.Log(tran.GetSameNameBrotherS(true, true).IndexOf(tran));
                }

                if (temp != root)
                {
                    tranS.Add(temp);
                    result = temp.name + sameNameMark + sameNameIndex + pathMark + result;
                    temp = temp.parent;
                }
                else
                {
                    result = root.name + sameNameMark + sameNameIndex + pathMark + result;
                    tranS.Add(root);
                    break;
                }
            }
            result = result.Substring(0, result.Length - 1);
            return result;
        }

        public static List<Transform> GetAllParent(Transform tran)
        {
            List<Transform> tranS = new List<Transform>();
            Transform root = tran.root;
            Transform temp = tran;
            for (int i = 0; i < 100; i++)
            {
                if (temp != root)
                {
                    tranS.Add(temp);
                    temp = temp.parent;
                }
                else
                {
                    tranS.Add(root);
                    break;
                }
            }

            return tranS;
        }

        public static bool IsContainChildren(List<Transform> tranS, Transform tran)
        {
            foreach (Transform t in tranS)
            {
                if (t == tran)
                    return true;
            }
            return false;
        }



        ///获取一个物体的子孙物体，无论是否激活,
        public static List<GameObject> GetChildren_Recursion(Transform parent)//参数：根节点物体的transform
        {
            List<GameObject> objList = new List<GameObject>();
            for (int i = 0; i < parent.childCount; i++) //childCount的数量包括不显示的物体
            {
                Transform temp = parent.GetChild(i);

                objList.Add(temp.gameObject);

                if (temp.childCount > 0)
                {
                    GetChildren_Recursion(temp);
                }
            }
            return objList;
        }
        /// <summary>
        /// 检测是两个列表里，是否有前半部分名字一样，但加了后缀的物体。最后一个参数是对下标需要控制时所用。
        /// </summary>
        /// <param name="trans01"></param>
        /// <param name="trans02"></param>
        /// <param name="mark"></param>
        /// <param name="isFirst"></param>
        /// <param name="lastTwoNumber">下标</param>
        /// <returns></returns>
        public static List<Transform> GetAddedStrTrans
            (List<Transform> trans01, List<Transform> trans02, string mark, int lastDigit, bool isFirst, int lastTwoNumber = 999)
        {
            List<Transform> trans = new List<Transform>();
            List<string> listStr01 = GetTranssName(trans01);
            List<string> listStr02 = GetTranssName(trans02);
            //这么写是为了少执行一些if
            if (lastTwoNumber == 999)
            {
                for (int i = 0; i < listStr01.Count; i++)
                {
                    if (string.Compare(listStr01[i], listStr02[i]) < 0 && U_String.JudeLastContain(listStr02[i], mark, lastDigit))
                    {
                        if (isFirst)
                            trans.Add(trans01[i]);
                        else
                            trans.Add(trans02[i]);
                    }
                }
            }
            else
            {

                for (int i = 0; i < listStr01.Count; i++)
                {
                    if (string.Compare(listStr01[i], listStr02[i]) < 0
                        && listStr02[i].Contains(mark + U_String.IntToString(lastTwoNumber)))
                    {
                        if (isFirst)
                            trans.Add(trans01[i]);
                        else
                        {
                            trans.Add(trans02[i]);
                        }
                    }
                }
            }
            return trans;
        }
        //和上边的区别就是这个的参数是单个transform
        public static List<Transform> GetAddedStrTrans
            (Transform tran01, Transform tran02, string mark, int lastDigit, bool isFirst, int lastTwoNumber = 999)
        {
            List<Transform> trans01 = GetChildren(tran01);
            List<Transform> trans02 = GetChildren(tran02);
            List<Transform> trans = GetAddedStrTrans(trans01, trans02, mark, lastDigit, isFirst, lastTwoNumber);
            return trans;
        }


        //比较两个TransList中的名称，并返回名称不一样的物体。
        public static List<Transform> CompareStrsReturnStrs(List<Transform> trans1, List<Transform> trans2, bool byFirst = true)
        {
            List<Transform> trans = new List<Transform>();
            List<string> str1 = GetTranssName(trans1);
            List<string> str2 = GetTranssName(trans2);
            if (byFirst)
            {
                for (int i = 0; i < trans1.Count; i++)
                {
                    if (string.Compare(str1[i], str2[i]) != 0)
                        trans.Add(trans1[i]);
                }
            }
            else
            {
                for (int i = 0; i < trans2.Count; i++)
                {
                    if (string.Compare(str1[i], str2[i]) != 0)
                        trans.Add(trans2[i]);
                }
            }
            return trans;
        }

        //输入两个父物体，对比他们的孩子有哪个名称改变了
        public static List<Transform> CompareChildrenNameReturnTrans(Transform trans1, Transform trans2, bool byFirst = true)
        {
            List<Transform> transs1 = GetChildren(trans1);
            List<Transform> transs2 = GetChildren(trans2);
            return CompareStrsReturnStrs(transs1, transs2, byFirst);
        }
        public static void DestroyImmediateAllSon(Transform trans)
        {
            List<Transform> sons = trans.GetSonS();
            foreach (Transform son in sons)
            {
                UnityEngine.Object.DestroyImmediate(son.gameObject);
            }
        }
        public static void DestroyAllSon(Transform trans)
        {
            List<Transform> sons = trans.GetSonS();
            foreach (Transform son in sons)
            {
                UnityEngine.Object.Destroy(son.gameObject);
            }
        }
        public static void InActiveSon(Transform trans)
        {
            List<Transform> sons = trans.GetSonS();
            foreach (Transform son in sons)
            {
                son.gameObject.SetActive(false);
            }
        }
        public static void InActiveSon(Transform trans, Transform except)
        {
            List<Transform> sons = trans.GetSonS();
            foreach (Transform son in sons)
            {
                if (son != except)
                {
                    son.gameObject.SetActive(false);
                }
                else
                {
                    son.gameObject.SetActive(true);
                }
            }
        }
        public static void DestoryAllSon(Transform trans)
        {
            List<Transform> sons = trans.GetSonS();
            foreach (Transform son in sons)
            {
                UnityEngine.Object.Destroy(son.gameObject);
            }
        }
        public static void DestorySon(Transform trans, IncludeOrExcept wInOrEx, string str)
        {
            List<Transform> sons = trans.GetSonS();
            foreach (Transform son in sons)
            {
                if (wInOrEx == IncludeOrExcept.except && !son.name.Contains(str))
                    UnityEngine.Object.Destroy(son.gameObject);
                else if (wInOrEx == IncludeOrExcept.include && son.name.Contains(str))
                {
                    UnityEngine.Object.Destroy(son.gameObject);
                }
            }
        }
        public static void DestorySunzi(Transform trans, IncludeOrExcept wInOrEx, string str)
        {
            List<Transform> sons = trans.GetSonS();
            foreach (Transform son in sons)
            {
                if (son.childCount == 0)
                    continue;
                foreach (Transform sunzi in son.GetSonS())
                {
                    if (wInOrEx == IncludeOrExcept.except && !sunzi.name.Contains(str))
                        UnityEngine.Object.Destroy(sunzi.gameObject);
                    else if (wInOrEx == IncludeOrExcept.include && sunzi.name.Contains(str))
                    {
                        UnityEngine.Object.Destroy(sunzi.gameObject);
                    }
                }
            }
        }


        // 解除父子关系 让其所有子物体跟其并列或指定为一个物体的子物体
        public static void RemoveChildren(Transform transform, Transform tarTrans = null)
        {
            List<Transform> sons = GetSonS(transform);
            foreach (Transform son in sons)
            {
                if (tarTrans != null)
                    son.parent = tarTrans.parent;
                else
                    son.parent = transform.parent;
            }
        }

        // 建立父子关系，让当前物体称为某物体的子物体
        public static void SetParent(Transform tran, Transform parent)
        {
            tran.parent = parent;
        }

        public static List<Transform> GetSonSContainMark(Transform trans, string sonContainName, bool containSelf = false)
        {
            List<Transform> sonS = GetSonS(trans);
            List<Transform> result = new List<Transform>();
            foreach (Transform son in sonS)
            {
                if (son.name.Contains(sonContainName))
                {
                    result.Add(son);
                }
            }
            return result;
        }

        // 获得他们的直接子物体
        public static List<Transform> GetSonS(Transform trans, bool containSelf = false)
        {
            List<Transform> sonList = new List<Transform>();
            for (int i = 0; i < trans.childCount; i++)
            {
                sonList.Add(trans.GetChild(i));
            }
            if (containSelf)
            {
                sonList.Insert(0, trans);
            }
            return sonList;
        }
        public static Transform GetSon(Transform tran, string sonContainName, bool justContain = true)
        {
            if (justContain)
            {
                for (int i = 0; i < tran.childCount; i++)
                {
                    if (tran.GetChild(i).name.Contains(sonContainName))
                    {
                        return tran.GetChild(i);
                    }
                }
            }
            else
            {
                for (int i = 0; i < tran.childCount; i++)
                {
                    if (tran.GetChild(i).name == sonContainName)
                    {
                        return tran.GetChild(i);
                    }
                }
            }
            return null;
        }
        // 获取子物体
        public static List<Transform> GetChildren(Transform tran, bool containSelf = false)
        {
            if (tran == null)
            {
                Debug.LogError("传入了空物体");
            }
            Transform[] children = tran.GetComponentsInChildren<Transform>(true);
            List<Transform> tList = new List<Transform>(children);
            if (!containSelf)
                tList.Remove(tran);
            return tList;
        }
        //[Obsolete("不知道为啥会报错但是不影响使用")]
        public static Transform GetChild(Transform tran, string childrenContainName, bool justContain = true)
        {
            Transform[] children = tran.GetComponentsInChildren<Transform>();

            if (justContain)
            {
                foreach (Transform child in children)
                {
                    if (child.name.Contains(childrenContainName))
                        return child;
                }
            }
            else
            {
                foreach (Transform child in children)
                {
                    if (child.name == childrenContainName)
                        return child;
                }
            }
            return null;
        }
        //通过父子级来查兄弟
        public static List<Transform> GetBrotherS(Transform tran, bool containSelf = false)
        {
            Transform parent = tran.parent;
            List<Transform> brothers = new List<Transform>();
            for (int i = 0; i < parent.childCount; i++)
            {
                brothers.Add(parent.GetChild(i));
            }
            if (!containSelf)
                brothers.Remove(tran);
            return brothers;
        }
        //查找同名的兄弟
        public static List<Transform> GetSameNameBrotherS(Transform tran, bool sameName = true, bool containSelf = true)
        {
            Transform parent = tran.parent;
            List<Transform> brothers = new List<Transform>();
            for (int i = 0; i < parent.childCount; i++)
            {
                if (sameName)
                {
                    if (parent.GetChild(i).name == tran.name)
                        brothers.Add(parent.GetChild(i));
                }
                else
                {
                    brothers.Add(parent.GetChild(i));
                }
            }
            if (!containSelf)
                brothers.Remove(tran);
            return brothers;
        }



        public static Transform GetBrother(Transform tran, string brotherContainName, bool justContain = true)
        {
            Transform parent = tran.parent;
            Transform brother;
            if (justContain)
            {
                for (int i = 0; i < parent.childCount; i++)
                {
                    if (parent.GetChild(i).name.Contains(brotherContainName))
                    {
                        brother = parent.GetChild(i);
                        return brother;
                    }
                }
            }
            else
            {
                for (int i = 0; i < parent.childCount; i++)
                {
                    if (parent.GetChild(i).name == brotherContainName)
                    {
                        brother = parent.GetChild(i);
                        return brother;
                    }
                }

            }

            return null;
        }

        // 获取子物体的名字
        public static List<string> GetChildrenName(Transform tran, bool containSelf = false)
        {
            List<Transform> children = GetChildren(tran, containSelf);
            List<string> childrenName = new List<string>();
            foreach (Transform t in children)
            {
                childrenName.Add(t.name);
            }
            return childrenName;
        }
        public static List<string> GetTranssName(List<Transform> trans)
        {
            List<string> names = new List<string>();
            foreach (Transform t in trans)
            {
                names.Add(t.name);
            }
            return names;
        }

        // 查看子物体中谁带了某个字符串
        public static List<Transform> GetChildrenWithString(Transform tran, string content, int length)
        {
            List<Transform> children = GetChildren(tran);
            List<string> childrenName = new List<string>();
            List<Transform> withStrList = new List<Transform>();

            for (int i = 0; i < childrenName.Count; i++)
            {
                childrenName.Add(children[i].name);
            }
            for (int i = 0; i < childrenName.Count; i++)
            {
                if (U_String.JudeLastContain(childrenName[i], content, length))
                {
                    withStrList.Add(children[i]);
                }
            }
            return withStrList;
        }

        //获取一个物体Ctrl+D之后的物体
        public static Transform GetCtrlD(Transform tran)
        {
            Transform ctrlD;
            if (null != tran.parent)
            {
                ctrlD = tran.parent.Find(tran.name + " (1)");
            }
            else
            {
                ctrlD = GameObject.Find(tran.name + " (1)").transform;
            }
            return ctrlD;
        }



        // 获取sons的position
        public static List<Vector3> GetSonPostion(Transform trans, bool containSelf = false)
        {
            List<Vector3> position = new List<Vector3>();
            List<Transform> sons = GetSonS(trans, containSelf);
            foreach (Transform son in sons)
            {
                position.Add(son.position);
            }
            return position;
        }

        // 获取sons的localPosition
        public static List<Vector3> GetSonLocalPostion(Transform trans, bool containSelf = false)
        {
            List<Vector3> localPositionList = new List<Vector3>();
            List<Transform> sons = GetSonS(trans, containSelf);
            foreach (Transform son in sons)
            {
                localPositionList.Add(son.localPosition);
            }
            return localPositionList;
        }
        // 获取sons的euler
        public static List<Vector3> GetSonEuler(Transform trans, bool containSelf = false)
        {
            List<Vector3> localEuler = new List<Vector3>();
            List<Transform> sons = GetSonS(trans, containSelf);
            foreach (Transform son in sons)
            {
                localEuler.Add(son.eulerAngles);
            }
            return localEuler;
        }
        // 获取sons的localEuler
        public static List<Vector3> GetSonLocalEuler(Transform trans, bool containSelf = false)
        {
            List<Vector3> GetSonLocalEuler = new List<Vector3>();
            List<Transform> sons = GetSonS(trans, containSelf);
            foreach (Transform son in sons)
            {
                GetSonLocalEuler.Add(son.localEulerAngles);
            }
            return GetSonLocalEuler;
        }

        // 获得children的postion
        public static List<Vector3> GetChildrenPostion(Transform trans, bool containSelf = false)
        {
            List<Transform> children = GetChildren(trans, containSelf);
            List<Vector3> childrenPosition = new List<Vector3>();
            foreach (Transform t in children)
            {
                childrenPosition.Add(t.position);
            }
            return childrenPosition;
        }
        // 获得children的localPostion
        public static List<Vector3> GetChildrenLocalPostion(Transform trans, bool containSelf = false)
        {
            List<Transform> children = GetChildren(trans, containSelf);
            List<Vector3> childrenLocalPosition = new List<Vector3>();
            foreach (Transform t in children)
            {
                childrenLocalPosition.Add(t.localPosition);
            }
            return childrenLocalPosition;
        }
        //获取物体的实际尺寸
        public static Vector3 GetTrueSize(Transform tran, W_SizeMode sizeMode = W_SizeMode.根据collider)
        {
            Vector3 size = new Vector3(0, 0, 0);
            if (sizeMode == W_SizeMode.根据collider)
            {
                bool haveBoxcollider = true;
                if (null == tran.GetComponent<BoxCollider>())
                {
                    haveBoxcollider = false;
                    tran.gameObject.AddComponent<BoxCollider>();
                }
                BoxCollider box = tran.GetComponent<BoxCollider>();
                size = box.bounds.size;
                if (!haveBoxcollider)
                {
                    UnityEngine.Object.DestroyImmediate(box);
                }
            }
            else
            {
                MeshFilter mesh = tran.GetComponent<MeshFilter>();
                size = mesh.mesh.bounds.size;
            }
            return size;
        }
        //根据物体的大小创建一个cube等
        public static GameObject SetupGameobjectBySize(Transform tran, string addName, PrimitiveType meshType = PrimitiveType.Cube, W_SizeMode sizeMode = W_SizeMode.根据collider)
        {
            Vector3 size = GetTrueSize(tran, sizeMode);
            GameObject go = GameObject.CreatePrimitive(meshType);
            if (meshType == PrimitiveType.Cube)
                go.transform.localScale = size;
            else if (meshType == PrimitiveType.Sphere || meshType == PrimitiveType.Capsule)
            {
                float maxAxis = Mathf.Max(size.x, size.y, size.z);
                go.transform.localScale = new Vector3(maxAxis, maxAxis, maxAxis);
            }
            go.transform.SetParent(tran);
            go.transform.localPosition = new Vector3(0, 0, 0);
            go.name = tran.name + addName;
            return go;
        }
        public enum W_SizeMode { 根据collider, 根据mesh }

        //是否有包含某字符的孩子
        public static bool IsChildrenNameContain(Transform tran, string signStr)
        {
            bool isChildrenNameContain = false;
            List<Transform> trans = GetChildren(tran);
            foreach (Transform transfrom in trans)
            {
                if (transfrom.name.Contains(signStr))
                {
                    isChildrenNameContain = true;
                }
            }
            return isChildrenNameContain;
        }

        // 获得children的eulerAngles
        public static List<Vector3> GetChildrenEuler(Transform trans, bool containSelf = false)
        {
            List<Transform> children = GetChildren(trans, containSelf);
            List<Vector3> childrenEuler = new List<Vector3>();
            foreach (Transform t in children)
            {
                childrenEuler.Add(trans.eulerAngles);
            }
            return childrenEuler;
        }
        // 获得children的localEulerAngles
        public static List<Vector3> GetChildrenLocalEuler(Transform trans, bool containSelf = false)
        {
            List<Transform> children = GetChildren(trans, containSelf);
            List<Vector3> childrenLocalEuler = new List<Vector3>();
            foreach (Transform t in children)
            {
                childrenLocalEuler.Add(trans.localEulerAngles);
            }
            return childrenLocalEuler;
        }

        //获取面板上的所有物体，包括激活非激活，Linq
        //public static List<GameObject> GetAllObjs_Linq()
        //{
        //    var allTransforms = Resources.FindObjectsOfTypeAll(typeof(Transform));
        //    var previousSelection = Selection.objects;
        //    Selection.objects = allTransforms.Cast<Transform>()
        //        .Where(x => x != null)
        //        .Select(x => x.gameObject)
        //        //如果你只想获取所有在Hierarchy中被禁用的物体，反注释下面代码
        //        //.Where(x => x != null && !x.activeInHierarchy)
        //        .Cast<UnityEngine.Object>().ToArray();

        //    var selectedTransforms = Selection.GetTransforms(SelectionMode.Editable | SelectionMode.ExcludePrefab);
        //    Selection.objects = previousSelection;
        //    return selectedTransforms.Select(tr => tr.gameObject).ToList();
        //}


        //找到面板上的所有激活的trans
        public static List<Transform> FindAllActiveTran()
        {
            List<Transform> trans = new List<Transform>(UnityEngine.Object.FindObjectsOfType(typeof(Transform)) as Transform[]);
            foreach (Transform tran in trans)
            {
                Debug.Log(tran.name);
            }
            return trans;
        }


        //找到面板上的所有激活的gameobject
        public static List<GameObject> FindAllActiveGO()
        {
            List<GameObject> gos = new List<GameObject>(UnityEngine.Object.FindObjectsOfType(typeof(GameObject)) as GameObject[]);

            foreach (GameObject tran in gos)
            {
                Debug.Log(tran.name);
            }
            return gos;
        }


        public static List<GameObject> TranS_To_GameObjectS(List<Transform> tranS)
        {
            List<GameObject> goS = new List<GameObject>();
            foreach (Transform tran in tranS)
            {
                goS.Add(tran.gameObject);
            }
            return goS;
        }
    }
    public static class U_TranformExtern
    {

        public static List<Transform> GetSonS(this Transform transform, bool isContainSelf = false)
        {
            return U_Transform.GetSonS(transform, isContainSelf);
        }
        public static List<Transform> GetChildren(this Transform transform, bool isContainSelf = false)
        {
            return U_Transform.GetChildren(transform, isContainSelf);
        }

        public static List<Transform> GetBrotherS(this Transform transform, bool isContainSelf = false)
        {
            return U_Transform.GetBrotherS(transform, isContainSelf);
        }
        public static List<Transform> GetSameNameBrotherS(this Transform transform, bool sameName = true, bool isContainSelf = true)
        {
            return U_Transform.GetSameNameBrotherS(transform, sameName, isContainSelf);
        }
        public static Transform GetSon(this Transform transform, string sonName, bool justContain = true)
        {
            return U_Transform.GetSon(transform, sonName, justContain);
        }
        public static Transform GetChild(this Transform transform, string childName, bool justContain = true)
        {
            return U_Transform.GetChild(transform, childName, justContain);
        }
        public static Transform GetBrother(this Transform transform, string brotherName, bool justContain = true)
        {
            return U_Transform.GetBrother(transform, brotherName, justContain);
        }
        public static void SetParentAndInitZero(this Transform transform, Transform parent)
        {
            if (transform == null)
            {
                Debug.LogError("传入了空的");
                return;
            }
            transform.SetParent(parent);
            transform.localPosition = Vector3.zero;
            transform.localRotation = Quaternion.identity;
            transform.localScale = Vector3.one;
            transform.gameObject.SetActive(true);
        }

    }
}