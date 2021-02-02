using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using UnityEngine;

namespace W.Stock
{
    public class StockMgr : MonoBehaviour
    {
        #region File
        public const string dbIp = "49.233.80.251";
        public const int mysqlPort = 33333;
        public const string mysqlUser = "lwb";
        public const string mysqlPassword = "fdsajkl123";

        public const string mysqlDbName = "stock";
        public const string holdStocks = "holdStocks";
        MysqlHelper sql;
        [Separator("set")]
        public string id;
        public int count;
        public float price;
        [Separator("data")]
        public float sumCB;
        public List<StockDb> stockS = new List<StockDb>();
        #endregion

        #region EditorButton
        [InspectorButton("UpdateDB")]
        public string showDb;
        public void UpdateDB()
        {
            ReadDbStockS();
            UpdateCB();
            UpdatePlate();
            foreach (var item in stockS)
            {
                Debug.Log(item);
            }
        }


        [InspectorButton("Submit", 6)]
        public string submit;
        public void Submit()
        {
            if (string.IsNullOrEmpty(id) || price * count == 0) { Debug.Log("输入不正确"); return; }

            ReadDbStockS();
            sumCB = UpdateSumCB();
            StockDb submitSotck = new StockDb();
            submitSotck.id = id;
            StockDb stock库里已存在的 = stockS.FirstOrDefault(s => s.id == submitSotck.id);
            if (stock库里已存在的 == null)
            {
                InsertData(submitSotck);
            }
            else
            {
                UpdateData();
            }
            id = "";
            price = 0;
            count = 0;
        }
        #endregion



        public float oldShizhi;
        public float oldPrice;
        public float buyPrice;
        //public float byCount;
        public float buyMoney;

        [InspectorButton("JisuanNewChengben", 6)]
        public string chengben;
        public void JisuanNewChengben()
        {
            int oldgushu = (int)(oldShizhi / oldPrice);
            float newShizhi = oldShizhi + buyMoney;
            newChengben = (oldShizhi + buyMoney) /(buyMoney/buyPrice+ oldShizhi/oldPrice);

            newChengbenYingkui = ((buyPrice - newChengben) / newChengben).ToPercentF2();
        }
        public float newChengben;
        public string newChengbenYingkui;













        void ReadDbStockS()
        {
            OpenSql();
            DataTable dt = sql.SelectAllTable(holdStocks);
            stockS = GetStockCCS(dt);
            UpdateSumCB();
            CloseSql();
        }

        void InsertData(StockDb stock)
        {
            StockTx stockWeb = U_Stock.GetStock(id.ToString());
            if (stockWeb == null)
            {
                Debug.Log("查不到该编号");
                return;
            }
            if (count < 0)
            {
                Debug.Log("并没有持有，无法为负数");
                return;
            }
            stock.cbPrice = price;
            stock.name = stockWeb.name;
            stock.count = count;
            stock.cb = count * price;
            stock.cbK = stock.cb / 1000f;
            sumCB += stock.cb;
            stock.ratio = stock.cb / sumCB * 100;
            stockS.Add(stock);
            OpenSql();
            sql.Insert(
                holdStocks,
                new string[] { "id", "name", "cbPrice", "count", "cb", "cbK", "ratio" },
                new string[] {
                stock.id.ToString(),
                stock.name,
                stock.cbPrice.ToStringF3(),
                stock.count.ToString(),
                stock.cb.ToStringF2(),
                stock.cbK.ToStringF2(),
                stock.ratio.ToStringF2() }
                );
            UpdateEveryRatio();
            CloseSql();
            Debug.Log("买入：" + stock.name + "\tprice" + price + "\tcount" + count);
        }


        void UpdateData()
        {
            OpenSql();
            StockDb ori = stockS.FirstOrDefault(s => s.id == id);
            if (count < 0 && count < -ori.count)
            {
                Debug.LogError("没有这么多持仓");
                return;
            }
            float byMoeny = count * price;
            sumCB += byMoeny;
            int newCount = ori.count + count;
            float newCb = ori.cb + byMoeny;
            float newCbPrice = newCb / newCount;
            float newCbK = newCb / 1000;
            float newRatio = newCb / sumCB;
            string debugStr = "";

            ori.count = newCount;
            ori.cb = newCb;
            ori.cbPrice = newCbPrice;
            ori.cbK = newCbK;

            if (newCount == 0)
            {
                sql.Delete(holdStocks, "id", id);
                //sql.Delete();
                stockS.Remove(ori);
                debugStr = "清仓";
            }
            else
            {
                //sqlite
                sql.Update(
                           holdStocks,
                  new string[] { "cbPrice", "count", "cb", "cbK", "ratio" },
                  new string[] { newCbPrice.ToString(), newCount.ToString(), newCb.ToString(), newCbK.ToString(), newRatio.ToString() },
                  "id", id.ToString()
                    );
                //mysql
                //sql.Update(
                //   holdStocks,
                //  new string[] { "cbPrice", "count", "cb", "cbK", "ratio" },
                //  new string[] { newCbPrice.ToString(), newCount.ToString(), newCb.ToString(), newCbK.ToString(), newRatio.ToString() },
                //  "id", id.ToString()
                //  );
            }
            if (count > 0)
            {
                debugStr = "买入";
            }
            else if (count < 0)
            {
                if (newCount != 0)
                {
                    debugStr = "卖出";
                }
            }
            Debug.Log(debugStr + ori.name + "\tprice" + price + "\tcount" + count);
            UpdateEveryRatio();
            CloseSql();
        }
        public float UpdateSumCB()
        {
            sumCB = 0;
            foreach (var item in stockS)
            {
                sumCB += item.cb;
            }
            return sumCB;
        }
        void UpdateEveryRatio()
        {
            foreach (var item in stockS)
            {
                float newRatio = item.cb / sumCB * 100;
                //Debug.Log("更新Ratio：" + item.name + newRatio.ToStringF2());
                sql.Update(holdStocks, new string[] { "ratio" }, new string[] { newRatio.ToStringF2() }, "id", item.id.ToString());
            }
        }

        void UpdatePlate()
        {
            OpenSql();
            foreach (var db in stockS)
            {
                //xg
                int plate = db.plate;
                //更新各个板块的数据；
                //下一步


                db.cb = db.cbPrice * db.count;
                db.cbK = db.cb / 1000;
                sql.Update(holdStocks, new string[] { "cb", "cbK" }, new string[] { db.cb.ToStringF2(), db.cbK.ToStringF2() }, "id", db.id.ToString());
            }
            CloseSql();
        }
        void UpdateCB()
        {
            OpenSql();

            foreach (var db in stockS)
            {
                db.cb = db.cbPrice * db.count;
                db.cbK = db.cb / 1000;

                sql.Update(holdStocks, new string[] { "cb", "cbK" }, new string[] { db.cb.ToStringF2(), db.cbK.ToStringF2() }, "id", db.id.ToString());
            }
            CloseSql();
        }

        public static List<StockDb> GetStockCCS(DataTable dt)
        {
            List<StockDb> res = new List<StockDb>();
            foreach (DataRow item in dt.Rows)
            {
                StockDb stock = SwitchToStockCC(item);
                res.Add(stock);
            }
            return res;
        }
        public static StockDb SwitchToStockCC(DataRow row)
        {
            StockDb res = new StockDb();
            res.id = row[0].ToString();
            res.name = row[1].ToString();
            res.cbPrice = float.Parse(row[2].ToString());
            res.count = int.Parse(row[3].ToString());
            res.cb = float.Parse(row[4].ToString());
            res.cbK = float.Parse(row[5].ToString());
            res.ratio = float.Parse(row[6].ToString());
            res.plate = int.Parse(row[7].ToString());
            return res;
        }

        #region common
        void OpenSql()
        {
            sql = new MysqlHelper(dbIp, mysqlPort, mysqlUser, mysqlPassword, mysqlDbName);

        }
        void CloseSql()
        {
            if (sql != null)
            {
                sql.Close();
            }
        }
        #endregion
    }
}
