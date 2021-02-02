using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class U_Float : MonoBehaviour
{


}
public static class UFloatExtren
{
    public static string ToString(this float v, int weishu)
    {
        string str = "F" + weishu.ToString();
        return v.ToString(str);
    }
    public static string ToStringF0(this float v)
    {
        return v.ToString("F0");
    }
    public static string ToStringF1(this float v)
    {
        return v.ToString("F1");
    }
    public static string ToStringF2(this float v)
    {
        return v.ToString("F2");
    }
    public static string ToStringF3(this float v)
    {
        return v.ToString("F3");
    }
    public static string ToPercentF2(this float v)
    {
        return (v * 100).ToString("F2") + "%";
    }
    public static string AddPercent(this float v)
    {
        return (v).ToString("F2") + "%";
    }
    public static string Switch1000byK_F1(this float v)
    {
        return (v / 1000).ToString("F1") + "k";
    }
    public static string AddK(this float v)
    {
        return v.ToString("F1") + "k";
    }
}