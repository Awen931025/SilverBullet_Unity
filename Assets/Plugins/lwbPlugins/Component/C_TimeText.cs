/*Name:					W_NowTime		
 *Description: 		    当前时间，这个跟Text耦合在一起了，有时间拆开，放到一个工具类里。
 *Author:       		李文博
 *Date:         		2018-06-21
 *Copyright(C) 2018 by 	北京兆泰源信息技术有限公司*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

namespace W
{
    public class C_TimeText : MonoBehaviour
    {


        public enum 显示什么内容 { 日期, 时间, 星期_英文, 星期_简写, 星期_中文, }
        public enum 时间样式 { 小时和分钟闪烁的冒号, 小时分钟秒不闪烁 }
        public 显示什么内容 mode = 显示什么内容.时间;
        public 时间样式 style = 时间样式.小时分钟秒不闪烁;
        Text timeText;
        void Awake()
        {
            timeText = GetComponent<Text>();
        }


        private void OnEnable()
        {
            switch (mode)
            {
                case 显示什么内容.日期:
                    ShowDate();
                    break;
                case 显示什么内容.星期_英文:
                    ShowWeekday_英文();
                    break;
                case 显示什么内容.星期_简写:
                    ShowWeekday_英文简写();
                    break;
                case 显示什么内容.星期_中文:
                    ShowWeekday_中文();
                    break;
                default:
                    break;
            }

        }
        float timer = 1;
        bool showMaohao = true;
        int int_hour;
        int int_mintue;
        string str_hour;
        string str_mintue;

        string hourStr;
        string minuteStr;
        string secondStr;

        void GetCurTime()
        {
            HourAdd0();
            MinuteAdd0();
            SecondAdd0();
        }
        void HourAdd0()
        {
            int hour = DateTime.Now.Hour;
            if (hour < 10)
            {
                hourStr = "0" + hour.ToString();
            }
            else
            {
                hourStr = hour.ToString();
            }
        }
        void MinuteAdd0()
        {
            int minute = DateTime.Now.Minute;
            if (minute < 10)
            {
                minuteStr = "0" + minute.ToString();
            }
            else
            {
                minuteStr = minute.ToString();
            }
        }
        void SecondAdd0()
        {
            int second = DateTime.Now.Second;
            if (second < 10)
            {
                secondStr = "0" + second.ToString();
            }
            else
            {
                secondStr = second.ToString();
            }
        }


        void ShowWeekday_数字()
        {
            timeText.text = ((int)DateTime.Now.DayOfWeek).ToString();
        }
        void ShowWeekday_英文()
        {
            Debug.Log("显示英");
            timeText.text = DateTime.Now.DayOfWeek.ToString();
        }
        void ShowWeekday_英文简写()
        {
            string result = "";
            switch ((int)DateTime.Now.DayOfWeek)
            {
                case 0: result = "Sun"; break;
                case 1: result = "Mon"; break;
                case 2: result = "Tues"; break;
                case 3: result = "Wed"; break;
                case 4: result = "Thur"; break;
                case 5: result = "Fri"; break;
                case 6: result = "Sat"; break;
            }
            timeText.text = result;
        }
        void ShowWeekday_中文()
        {
            string result = "";
            switch ((int)DateTime.Now.DayOfWeek)
            {
                case 0: result = "星期日"; break;
                case 1: result = "星期一"; break;
                case 2: result = "星期二"; break;
                case 3: result = "星期三"; break;
                case 4: result = "星期四"; break;
                case 5: result = "星期五"; break;
                case 6: result = "星期六"; break;
            }
            timeText.text = result;
        }

        void ShowDate()
        {
            timeText.text = U_DateTime.GetDate(".");
        }
        void FixedUpdate()
        {
            if (mode != 显示什么内容.时间)
                return;
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                GetCurTime();
                if (style == 时间样式.小时和分钟闪烁的冒号)
                {
                    timer = 1;
                    if (showMaohao)
                    {
                        timeText.text = hourStr + ":" + minuteStr;
                        showMaohao = false;
                    }
                    else
                    {
                        timeText.text = hourStr + " " + minuteStr;
                        showMaohao = true;
                    }
                }
                else if (style == 时间样式.小时分钟秒不闪烁)
                {
                    timer = 1;
                    timeText.text = hourStr + ":" + minuteStr + ":" + secondStr;
                }
            }
        }
    }
}