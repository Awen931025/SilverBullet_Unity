using UnityEngine;
using UnityEditor;
using System.Collections;
using W;
using System.Reflection;
using System;

namespace W.Stock
{
    public class ShortcutKey_Stock : ScriptableObject
    {

        //%对应Ctrl,#对应Shift &对应Alt f对应F
        public const string BySZ = "W_/Stock/BySZ &q";
        public const string ByDayYK = "W_/Stock/ByDayYK &w";
        public const string BySumYK = "W_/Stock/BySumYK &e";
        public const string Clear = "W_/Stock/Clear &r";



        [MenuItem(BySZ)]
        public static void SZ()
        {
            U_Stock.Debug_SZ();
            //Debug.Log("市值");
        }
        [MenuItem(ByDayYK)]
        public static void DayYK()
        {
            U_Stock.Debug_DayYK();
            //Debug.Log("单日涨跌");
        }
        [MenuItem(BySumYK)]
        public static void SumYK()
        {
            U_Stock.Debug_SumYK();
        }


        [MenuItem(Clear)]
        public static void ClearDenbug()
        {
            U_Console.ClearDenbug();
        }

    }
}