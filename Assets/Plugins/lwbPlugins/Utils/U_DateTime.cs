/*Name:		 				U_DateTime	
 *Description: 				
 *Author:       			李文博 
 *Date:         			2018-11-
 *Copyright(C) 2018 by 		北京兆泰源信息技术有限公司*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace W
{
    public class U_DateTime
    {

        public static string NowDateTime(string dataConnectorStr = "-", string timeConectorStr = ".")
        {
            string result = "";
            result
                += DateTime.Now.Year + dataConnectorStr
                + DateTime.Now.Month + dataConnectorStr
                + DateTime.Now.Day + " "
                + DateTime.Now.Hour + timeConectorStr
                + DateTime.Now.Minute + timeConectorStr
                + DateTime.Now.Second
                ;
            return result;
        }
        public static string GetWeek_Eng(string dataConnectorStr = "-", string timeConectorStr = ".")
        {
            string result = "";
            result
                += DateTime.Now.Year + dataConnectorStr
                + DateTime.Now.Month + dataConnectorStr
                + DateTime.Now.Day + " "
                + DateTime.Now.Hour + timeConectorStr
                + DateTime.Now.Minute + timeConectorStr
                + DateTime.Now.Second
                ;
            return result;
        }
        public static string GetDate(string connectorStr = ".")
        {
            string result = "";
            result
                += DateTime.Now.Year + connectorStr
                + DateTime.Now.Month + connectorStr
                + DateTime.Now.Day;
            return result;
        }
        public static string GetTime(string connectorStr = ":")
        {
            string result = "";
            result
                  += DateTime.Now.Hour + connectorStr
                + DateTime.Now.Minute + connectorStr
                + DateTime.Now.Second
                ;
            return result;
        }
    }
}