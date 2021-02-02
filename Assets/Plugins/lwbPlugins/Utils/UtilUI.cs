using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using W;

public class UtilUI : MonoBehaviour
{



    public static void LoadImageByIO(string url, Image image)
    {
        //这个地方出来的size可能是负值？
        //RectTransform rec = image.GetComponent<RectTransform>();
        int width = (int)image.rectTransform.rect.size.x;
        int height = (int)image.rectTransform.rect.size.y;
        //int width = (int)rec.sizeDelta.x;
        //int height = (int)rec.sizeDelta.y;
        LoadImageByIO(url, image, width, height);
    }
    /// 以IO方式进行加载
    public static void LoadImageByIO(string url, Image image, int width, int height)
    {
        Sprite sprite;
        try
        {
            //创建文件读取流
            FileStream fileStream = new FileStream(url, FileMode.Open, FileAccess.Read);
            //创建文件长度缓冲区
            byte[] bytes = new byte[fileStream.Length];
            //读取文件
            fileStream.Read(bytes, 0, (int)fileStream.Length);

            //释放文件读取流
            fileStream.Close();
            //释放本机屏幕资源
            fileStream.Dispose();
            fileStream = null;
            Texture2D texture = new Texture2D(width, height);
            texture.LoadImage(bytes);

            //创建Sprite
            sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
            image.sprite = sprite;
        }
        catch
        {
            Debug.LogError("没有找到对应的图片");
            image.sprite = null;
        }

    }















    public static string ConvertInt2Char(int i, bool isBig = false)
    {
        if (i > 26 || i < 1)
        {
            Debug.LogError("传入了一个非1-26的数字，无法转换字母");
        }
        char resChar;
        if (isBig)
            resChar = (char)((int)'A' + i - 1);
        else
            resChar = (char)((int)'a' + i - 1);
        return resChar.ToString();
    }


    public static List<string> SqlResultString2List(string strs)
    {
        List<string> res = new List<string>();

        string[] temp = strs.Split(';');
        foreach (var item in temp)
        {
            if (!string.IsNullOrEmpty(item))
            {
                res.Add(item);
            }
        }
        return res;
    }
    public static string GetTimeNote()
    {
        string res = "[" + DateTime.Now.Hour.ToString("d2") + ":" + DateTime.Now.Minute.ToString("d2") + "]";
        return res;
    }


    ///grid自带的刚生成grid取pos有坑，第二针才行
    ///仅做了左上角轴心锚点的情况，且width只能往小了调整。
    ///返回左下角
    public static Vector2 FitterGrid(GridLayoutGroup grid)
    {
        return FitterGrid(grid, grid.transform.childCount);
    }
    public static Vector2 FitterGrid(GridLayoutGroup grid, int count)
    {

        return FitterGrid(grid, count, grid.GetComponent<RectTransform>().sizeDelta.x);
    }
    public static Vector2 FitterGrid(GridLayoutGroup grid, int count, float oriWidth)
    {
        RectTransform gridTrans = grid.GetRect();
        gridTrans.sizeDelta = new Vector2(oriWidth, 0);
        int row = 0;
        int maxCountOneRow = (int)((gridTrans.sizeDelta.x - grid.padding.left - grid.padding.right) / (grid.cellSize.x + grid.spacing.x));
        maxCountOneRow = maxCountOneRow > 0 ? maxCountOneRow : 1;
        if (count < maxCountOneRow)
        {
            float widthNew = grid.padding.left + grid.padding.right + count * (grid.spacing.x + grid.cellSize.x);
            gridTrans.sizeDelta = new Vector2(widthNew, gridTrans.sizeDelta.y);
            row = 1;
        }
        else
        {
            row = (count / maxCountOneRow) + 1;
        }
        float height = grid.padding.top + grid.padding.bottom + row * (grid.spacing.y + grid.cellSize.y);
        gridTrans.sizeDelta = new Vector2(gridTrans.sizeDelta.x, height);
        return GetLeftBottom(gridTrans);
    }

    /// <summary>
    /// 同上，也只针对左上角锚点轴点布局使用
    /// </summary>
    public static Vector2 GetLeftBottom(RectTransform rectTrans)
    {
        Vector2 leftBottom = rectTrans.anchoredPosition - new Vector2(0, rectTrans.sizeDelta.y);
        return leftBottom;
    }

    internal static void ScrollrectYFitGrid(ScrollRect scroll, GridLayoutGroup grid, float maxScrollRectY, bool fitScrollbar = true)
    {
        float gridSizeY = grid.transform.childCount * (grid.cellSize.y + grid.spacing.y) + grid.padding.top + grid.padding.bottom;
        float resHeight = Mathf.Min(maxScrollRectY, gridSizeY);
        scroll.GetRect().sizeDelta = new Vector2(scroll.GetRect().sizeDelta.x, resHeight);
        if (fitScrollbar)
        {
            scroll.vertical = !(maxScrollRectY > gridSizeY);
        }
    }
}
