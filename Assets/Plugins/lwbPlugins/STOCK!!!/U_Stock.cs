
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.IO;
using UnityEngine;
using UnityEngine.AI;
using System.Data;
using System.Collections;
//using UnityEditor.Rendering;
using System;
using System.Linq;

namespace W.Stock
{
    public class U_Stock : MonoBehaviour
    {
        #region Files
        public static string sqllitePath = @"Data Source = " + Application.streamingAssetsPath + @"\Sqlite\stock.sqlite";
        public const string holdStocks = "holdStocks";
        public const string selledStocks = "selledStocks";

        public const string holdETFs = "holdETFs";
        public const string selledETFs = "selledETFs";

        public const string dbIp = "49.233.80.251";
        public const int mysqlPort = 33333;
        public const string mysqlUser = "lwb";
        public const string mysqlPassword = "fdsajkl123";
        public const string mysqlDbName = "stock";

        //public string txUE

        const string txURL = @"http://qt.gtimg.cn/q=";
        public static float sumSZ = 0;
        public static float sumYK = 0;
        public static float allLastSZ = 0;
        public static float todayYK = 0;
        public static float sumCB = 0;
        public static float toadyYKRate = 0;
        public static List<StockTx> txS = new List<StockTx>();
        public enum EnShowType { stock, etf, All }
        public EnShowType enShowType = EnShowType.stock;
        [Separator("setting")]
        public bool isDebugTitle = true;
        public bool isIndexAndTotalSameLine = true;
        public bool isOnlyShowChiCang = true;
        public bool isShowSelledYK = true;
        public bool isDebugStock = false;
        public int timeInveal = 3;
        public static bool is显示指数的数值 = true;
        public bool show指数数值 = true;

        #endregion

        private void OnEnable()
        {
            //

            if (isDebugStock)
            {
                StartUpdate();
            }
        }
        private void OnDisable()
        {
            if (isDebugStock)
            {
                //azongshizhi
                EndUpdate();
            }
        }
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.KeypadPlus))
            {
                StartUpdate();
            }
            if (Input.GetKeyDown(KeyCode.KeypadEnter))
            {
                EndUpdate();
            }
        }

        public enum SortType { sz, today, leiji }
        public SortType sortType = SortType.sz;
        public void StartUpdate()
        {
            isDebugStock = true;
            InvokeDebugSZ();
        }
        public void EndUpdate()
        {
            U_Console.ClearDenbug(true);
            isDebugStock = false;
        }
        void InvokeDebugSZ()
        {
            //
            if (isDebugStock)
            {
                StartCoroutine(RequestTx(sortType));
                Invoke("InvokeDebugSZ", timeInveal);
            }
        }


        public static List<StockDb> GetAllDbS()
        {
            MysqlHelper sql = new MysqlHelper(dbIp, mysqlPort, mysqlUser, mysqlPassword, mysqlDbName);
            DataTable dt = sql.SelectAllTable(holdStocks);
            sql.Close();

            List<StockDb> stockS = GetStockCCS(dt);
            return stockS;
        }
        public static List<StockDb> GetAllDbS(EnShowType en)
        {
            MysqlHelper sql = new MysqlHelper(dbIp, mysqlPort, mysqlUser, mysqlPassword, mysqlDbName);
            //DataTable dt = null;
            List<StockDb> stockS = new List<StockDb>();
            if (en == EnShowType.stock)
            {
                DataTable dt = sql.SelectAllTable(holdStocks);
                stockS = GetStockCCS(dt);
            }
            else if (en == EnShowType.etf)
            {
                DataTable dt = sql.SelectAllTable(holdETFs);
                stockS = GetStockCCS(dt);
            }
            else
            {
                DataTable dtStock = sql.SelectAllTable(holdStocks);
                stockS = GetStockCCS(dtStock);
                DataTable dtETF = sql.SelectAllTable(holdETFs);
                List<StockDb> ETFs = GetStockCCS(dtETF);
                stockS.AddRange(ETFs);
            }
            sql.Close();

            return stockS;
        }
        public static float GetSelledIncrease()
        {
            MysqlHelper sql = new MysqlHelper(dbIp, mysqlPort, mysqlUser, mysqlPassword, mysqlDbName);
            DataTable dt = sql.SelectAllTable(selledStocks);
            sql.Close();
            float res = 0;
            foreach (DataRow item in dt.Rows)
            {
                res += float.Parse(item[2].ToString());
            }
            return res;
        }

        public static float GetSelledIncrease(EnShowType en)
        {
            MysqlHelper sql = new MysqlHelper(dbIp, mysqlPort, mysqlUser, mysqlPassword, mysqlDbName);
            float res = 0;

            if (en == EnShowType.stock)
            {
                res = GetSelledStockYK();
            }
            else if (en == EnShowType.etf)
            {
                res = GetSelledEtfYK();
            }
            else if (en == EnShowType.All)
            {
                res = GetSelledStockYK();
                res += GetSelledEtfYK();
            }
            return res;
        }
        public static float GetSelledStockYK()
        {
            float res = 0;
            MysqlHelper sql = new MysqlHelper(dbIp, mysqlPort, mysqlUser, mysqlPassword, mysqlDbName);
            DataTable dt = sql.SelectAllTable(selledStocks);
            foreach (DataRow item in dt.Rows)
            {
                res += float.Parse(item[2].ToString());
            }
            sql.Close();
            return res;
        }
        public static float GetSelledEtfYK()
        {
            float res = 0;
            MysqlHelper sql = new MysqlHelper(dbIp, mysqlPort, mysqlUser, mysqlPassword, mysqlDbName);
            DataTable dt = sql.SelectAllTable(selledETFs);
            foreach (DataRow item in dt.Rows)
            {
                res += float.Parse(item[2].ToString());
            }
            sql.Close();
            return res;
        }


        public static List<StockDb> GetStockCCS(DataTable dt)
        {
            List<StockDb> res = new List<StockDb>();
            foreach (DataRow item in dt.Rows)
            {
                StockDb stock = StockMgr.SwitchToStockCC(item);
                res.Add(stock);
            }
            return res;
        }




        public static string 拼接Url(string stockId)
        {
            return txURL + Get加上前缀(stockId);
        }



        #region 快捷键
        public static void Debug_SZ()
        {
            U_Console.ClearDenbug();
            DebugTitle("q市值比重↓");
            RequestTxS();
            txS.Sort((a, b) => -a.sz.CompareTo(b.sz));
            StockTx.DebugList(txS);
            DebugSum(todayYK, toadyYKRate, sumYK, sumCB, sumSZ);
        }
        public static void Debug_DayYK()
        {
            U_Console.ClearDenbug();
            DebugTitle("w单日涨跌↓");
            RequestTxS();
            txS.Sort((a, b) => -a.increPer.CompareTo(b.increPer));
            StockTx.DebugList(txS);
            DebugSum(todayYK, toadyYKRate, sumYK, sumCB, sumSZ);
        }
        public static void Debug_SumYK()
        {
            U_Console.ClearDenbug();
            DebugTitle("e累计盈亏↓");
            RequestTxS();
            txS.Sort((a, b) => -a.sumYK.CompareTo(b.sumYK));
            StockTx.DebugList(txS);
            DebugSum(todayYK, toadyYKRate, sumYK, sumCB, sumSZ);
        }
        #endregion

        #region Debug通用
        static void DebugTitle(string tableTitle)
        {
            Debug.Log(tableTitle + "\t|\t单日\t|\t累计\t|\t仓位\t|  价格—成本\t|");
        }
        static void DebugSum(float todayYK, float todatYLRate, float allYK, float allCB, float allSZ)
        {
            string sumSZ = "总 " + allSZ.Switch1000byK_F1() + "\t";

            string danri = "|  " + todatYLRate.ToPercentF2() + "\t" + todayYK.ToStringF1() + "\t|  ";
            string leiji = ((allYK) / allCB).ToPercentF2() + "\t" + allYK.ToStringF2() + "\t";
            string lasStr = sumSZ + danri + leiji + GetStrZhishu();
            Debug.Log(lasStr);
        }
        static string DebugSum_WithoutZhishu(float todayYK, float todatYLRate, float allYK, float allCB, float allSZ)
        {
            string sumSZ = "总 " + allSZ.Switch1000byK_F1() + "\t";
            string danri = "|  " + todatYLRate.ToPercentF2() + "\t" + todayYK.ToStringF2() + "\t|  ";
            string leiji = ((allSZ - allCB) / allCB).ToPercentF2() + "\t" + allYK.ToStringF2() + "\t";
            string res = sumSZ + danri + leiji;
            return res;
        }

        #endregion


        #region WWW_Get
        IEnumerator RequestTx(SortType sortType)
        {
            List<StockTx> stockTxS = new List<StockTx>();
            float sumSZ = 0;
            float sumYK = 0;
            float allLastSZ = 0;
            float todayAllYK = 0;
            float sumCB = 0;

            float toadyAllYKRate = 0;
            List<StockDb> dbS = GetAllDbS(enShowType);
            foreach (StockDb db in dbS)
            {
                string url = 拼接Url(db.id);
                WWW www = new WWW(url);
                yield return www;
                StockTx tx = new StockTx(www.text);
                tx.sz = db.count * float.Parse(tx.nowPri);
                tx.todayYingkui = tx.increase * db.count;//这样计算，当天买卖，盈利是有问题的。
                tx.cbPri = db.cbPrice;
                tx.sumYK = db.count * float.Parse(tx.nowPri) - db.cb;
                tx.allYingkuiRate = tx.sumYK / db.cb * 100;
                tx.ratio = db.ratio;
                sumSZ += tx.sz;
                sumYK += tx.sumYK;
                sumCB += db.cb;
                stockTxS.Add(tx);
                allLastSZ += float.Parse(tx.lastDayPri) * db.count;
                todayAllYK += tx.todayYingkui;
            }
            float allSelledYK = GetSelledIncrease(enShowType);
            if (!isOnlyShowChiCang)
            {
                sumYK += allSelledYK;
            }
            string total = "";
            if (stockTxS.Count == dbS.Count)
            {
                toadyAllYKRate = todayAllYK / allLastSZ;
                total = DebugSum_WithoutZhishu(todayAllYK, toadyAllYKRate, sumYK, sumCB, sumSZ) + "|";
            }
            #region Zhishu
            List<string> zhishuS = new List<string>() { "000001", "399001", "399006" };
            List<StockTx> zhishuTx = new List<StockTx>();
            foreach (var item in zhishuS)
            {
                string url = 拼接Url(item);
                WWW www = new WWW(url);
                yield return www;
                if (www.error != null)
                {
                    Debug.LogError("获取地址错误");
                    yield break;
                }
                string res = www.text;
                StockTx tx = new StockTx(res);
                zhishuTx.Add(tx);
            }
            string strZhishuInfo;
            if (zhishuTx.Count == 3)
            {
                StockTx sh = zhishuTx.FirstOrDefault(s => s.id == "000001");
                StockTx sz = zhishuTx.FirstOrDefault(s => s.id == "399001");
                StockTx cy = zhishuTx.FirstOrDefault(s => s.id == "399006");
                if (show指数数值)
                {
                    strZhishuInfo = "上" + float.Parse(sh.nowPri).ToStringF0() + " " + sh.increPer.ToStringF2() + "%"
         + "  创" + float.Parse(cy.nowPri).ToStringF0() + " " + cy.increPer.ToStringF2() + "%"
         + "  深" + float.Parse(sz.nowPri).ToStringF0() + " " + sz.increPer.ToStringF2() + "%\t|";
                }
                else
                {
                    strZhishuInfo = "上" + cy.increPer.ToStringF2() + "%  创" + sz.increPer.ToStringF2() + "%  深" + sz.increPer.ToStringF2() + "%\t|";
                }
                if (isDebugStock)
                {
                    U_Console.ClearDenbug();
                    //最终是在此处打印的，是为了让他同一时间闪出来，控制台刷新感更小
                    DebugStockS(stockTxS, sortType);
                    if (isShowSelledYK)
                    {
                        if(allSelledYK>0)
                        {
                            Debug.Log("清仓累计：\t+" + allSelledYK);
                        }
                        else
                        {
                            Debug.Log("清仓累计：\t" + allSelledYK);

                        }
                    }
                    if (isIndexAndTotalSameLine)
                    {
                        Debug.Log(total + "  " + strZhishuInfo);
                    }
                    else
                    {
                        Debug.Log(total);
                        Debug.Log(strZhishuInfo);
                    }


                }
            }
            #endregion
        }


        public void DebugStockS(List<StockTx> txS, SortType sort)
        {
            string title = "";
            switch (sort)
            {
                case SortType.sz:
                    title = "q市值比重↓";
                    txS.Sort((a, b) => -a.sz.CompareTo(b.sz));
                    break;
                case SortType.today:
                    title = "w单日涨跌↓";
                    txS.Sort((a, b) => -a.todayYingkui.CompareTo(b.todayYingkui));
                    break;
                case SortType.leiji:
                    title = "e累计盈亏↓";
                    txS.Sort((a, b) => -a.sumYK.CompareTo(b.sumYK));
                    break;
            }
            if (isDebugTitle)
            {
                DebugTitle(title);
            }

            foreach (var item in txS)
            {
                //这个地方打印的
                Debug.Log(item);
            }
        }
        #endregion

        #region 同步Get
        public static StockTx GetStock(string number)
        {
            string stockCode = Get加上前缀(number);
            if (stockCode.Length != 8) return null;
            string url = txURL + stockCode;

            string content = RequestConentByUrl(url);
            return new StockTx(content);
        }


        public static string Get加上前缀(string number)
        {
            string res = "";
            if (number.Length != 6)
            {
                Debug.Log("请检查代码是否是六位");
                return res;
            }
            if (!U_Regex.IsNumeric(number))
            {
                Debug.Log("请检查代码是否是都是数字");
                return res;
            }

            int qiansanwei = int.Parse(number) / 1000;
            string prefix = "";
            switch (qiansanwei)
            {
                case 600: prefix = "sh"; break;        //沪A
                case 601: prefix = "sh"; break;     //沪A
                case 603: prefix = "sh"; break;     //沪A
                case 0: prefix = "sz"; break;     //深A
                case 2: prefix = "sz"; break;     //深中小
                case 300: prefix = "sz"; break;     //深创

                case 688: prefix = "sh"; break;     //沪 科创


                case 200: prefix = "sz"; break;     //深B
                case 900: prefix = "sh"; break;     //沪B
                case 730: prefix = "sh"; break;     //沪 新
                case 700: prefix = "sh"; break;     //沪配
                case 80: prefix = "sz"; break;     //深配
                case 580: prefix = "sh"; break;     //沪配权证
                case 31: prefix = "sz"; break;     //深配权证

                case 400: prefix = ""; break;         //三板

            }
            switch (int.Parse(number))
            {
                case 000001: prefix = "sh"; break;
                case 399006: prefix = "sz"; break;
                case 399001: prefix = "sz"; break;
            }
            //基金
            if (int.Parse(number) / 100000 == 5)
            {
                prefix = "sh";
            }
            else if (int.Parse(number) / 1000 == 159)
            {
                prefix = "sz";
            }
            res = prefix + number;
            return res;
        }

        static string GetStrZhishu()
        {
            StockTx sh = GetStock("000001");
            StockTx sz = GetStock("399001");
            StockTx cy = GetStock("399006");
            string res = "";
            if (is显示指数的数值)
            {
                res = "|  上" + float.Parse(sh.nowPri).ToStringF0() + " " + sh.increPer.ToStringF2() + "%"
                   + "  创" + float.Parse(cy.nowPri).ToStringF0() + " " + cy.increPer.ToStringF2() + "%"
                   + "  深" + float.Parse(sz.nowPri).ToStringF0() + " " + sz.increPer.ToStringF2() + "%\t|";

            }
            else
            {
                res = "|  上" + sh.increPer.ToStringF2() + "%  创" + cy.increPer.ToStringF2() + "%  深" + sz.increPer.ToStringF2() + "%\t|";

            }
            return res;
        }

        static void RequestTxS()
        {
            List<StockDb> dbS = GetAllDbS();
            txS.Clear();
            sumSZ = 0;
            sumYK = 0;
            allLastSZ = 0;
            todayYK = 0;
            sumCB = 0;
            foreach (var db in dbS)
            {
                StockTx tx = GetStock(db.id);
                tx.sz = db.count * float.Parse(tx.nowPri);
                tx.todayYingkui = tx.increase * db.count;//这样计算，当天买卖，盈利是有问题的。
                tx.cbPri = db.cbPrice;
                tx.sumYK = db.count * float.Parse(tx.nowPri) - db.cb;
                tx.allYingkuiRate = tx.sumYK / db.cb * 100;
                tx.ratio = db.ratio;

                sumSZ += tx.sz;
                sumYK += tx.sumYK;
                allLastSZ += float.Parse(tx.lastDayPri) * db.count;
                todayYK += tx.todayYingkui;
                sumCB += db.cb;

                //txS.Add
                txS.Add(tx);
            }
            sumYK += GetSelledIncrease();
            toadyYKRate = todayYK / allLastSZ;
        }

        static string RequestConentByUrl(string url)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";
            request.ReadWriteTimeout = 3000;
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream myResponseStream = response.GetResponseStream();
            StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.GetEncoding("GBK"));
            string res = myStreamReader.ReadToEnd();
            return res;
        }
        public List<StockTx> chicang = new List<StockTx>();






        #endregion
    }
}



