/*Name:		 				U_ExcelToJson	
 *Description: 				
 *Author:       			李文博 
 *Date:         			2019-08-
 *Copyright(C) 2019 by 		智联友道*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace W
{
    public class C_Excel : MonoBehaviour
    {
        [InspectorButton("DebugExcel")]
        public string 打印表所有内容;
        ExcelTable GetTable(string pathName, int tableIndex)
        {
            Excel excel = ExcelHelper.LoadExcel(Application.streamingAssetsPath + pathName);
            ExcelTable table = excel.Tables[tableIndex];
            return table;
        }
        public string pathName = @"\圆形明亮型.xlsx";


        void DebugExcel()
        {
            ExcelTable table = GetTable(pathName, 0);

            int rowCount = table.NumberOfRows;
            int columnsCount = table.NumberOfColumns;
            for (int r = 1; r <= rowCount; r++)
            {
                string stRow = "";
                for (int c = 1; c < columnsCount; c++)
                {
                    stRow += table.GetValue(1, c) + "：" + table.GetValue(r, c) + "  ";
                }
                Debug.Log(stRow);
            }
        }
    }
}