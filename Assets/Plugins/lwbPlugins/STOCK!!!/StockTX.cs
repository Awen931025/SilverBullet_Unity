using System.Collections.Generic;
using UnityEngine;
namespace W.Stock
{
    public class StockTx
    {
        public string name;             //~1
        public string id;               //~2
        public string nowPri;           //~3
        public string lastDayPri;       //~4
        public string liang;            //~6
        public string neipan;           //~7
        public string waipan;           //~8
        public string shiying;          //~39
        public string time;             //~30
        public float increase;          //~31
        public float increPer;          //~32
        public string maxPrice;         //~33
        public string minPrice;         //~34
        public string huanshou;         //~38
        public string zhenfu;           //~43
        public string shijing;          //~46

        public float sz;
        public float sumYK;
        public float allYingkuiRate;
        public float ratio;
        public float todayYingkui;
        public float cbPri;
        public StockTx(string content)
        {
            string[] dataS = content.Split('~');
            this.name = dataS[1];
            this.id = dataS[2];
            this.nowPri = dataS[3];
            this.lastDayPri = dataS[4];
            this.liang = dataS[6];
            this.neipan = dataS[7];
            this.waipan = dataS[8];
            this.shiying = dataS[39];
            this.time = dataS[30];
            this.increase = float.Parse(dataS[31]);
            this.increPer = float.Parse(dataS[32]);
            this.maxPrice = dataS[33];
            this.minPrice = dataS[34];
            this.huanshou = dataS[38];
            this.zhenfu = dataS[43];
            this.shijing = dataS[46];
        }

        public const string str分隔符 = "|  ";
        //public const string str分隔符 = "   ";
        public override string ToString()
        {
            string res = name + "\t";
            string danRi = str分隔符 + increPer.ToStringF2() + "%\t  " + todayYingkui.ToStringF1() + "\t" + str分隔符;
            string leiji = allYingkuiRate.AddPercent() + "\t  " + sumYK.ToStringF0() + "\t" + str分隔符;
            string cangwei = ratio.AddPercent() + "\t  " + sz.Switch1000byK_F1() + "\t" + str分隔符;
            string price = nowPri +"—"+ cbPri+ "\t" + str分隔符;
            res += danRi + leiji + cangwei + price;
            //
            return res;
        }
        public static void DebugList(List<StockTx> stocks)
        {
            foreach (StockTx item in stocks)
            {
                Debug.Log(item);
            }
        }
    }
}
