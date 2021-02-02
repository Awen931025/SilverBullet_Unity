/*Name:		 				U_Excel	
 *Description: 				
 *Author:       			lwb
 *Date:         			2019-06-
 *Copyright(C) 2019 by 		company@zhiwyl.com*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace W
{
    public class U_Excel : MonoBehaviour
    {

        Excel rayConf;
        public List<string> tagS = new List<string>();
        public List<Color> colorS = new List<Color>();
        public List<int> cursorSizeS = new List<int>();

        private void Start()
        {
            rayConf = ExcelHelper.LoadExcel(Application.streamingAssetsPath + @"\Conf\" + "vr射线颜色配置.xlsx");
            ExcelTable tb = rayConf.Tables[0];
            int rowCount = tb.NumberOfRows;
            for (int i = 2; i <= rowCount; i++)
            {
                Color color;
                tagS.Add(tb.GetValue(i, 1));
                ColorUtility.TryParseHtmlString("#" + tb.GetValue(i, 2), out color);
                colorS.Add(color);
                cursorSizeS.Add(int.Parse(tb.GetValue(i, 3)));

                //Debug.Log(tb.GetValue(i,1)+"    "+ tb.GetValue(i, 2) + "    " + tb.GetValue(i, 3) );
            }
        }

        public static ExcelTable GetTable(string pathName, int tableIndex)
        {
            Excel excel = ExcelHelper.LoadExcel(Application.streamingAssetsPath + pathName);
            ExcelTable table = excel.Tables[tableIndex];
            return table;
        }
    }
}