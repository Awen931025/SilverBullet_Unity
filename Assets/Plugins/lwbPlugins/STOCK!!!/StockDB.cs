using UnityEngine;

namespace W.Stock
{
    [System.Serializable]
    public class StockDb
    {
        public string id;
        public string name;
        public float cbPrice;
        public int count;
        public float cb;
        public float cbK;
        public float ratio;
        public int plate;
        public override string ToString()
        {
            return name + "\t" + cbK + "k\t" + ratio + "%\t" + cbPrice + "\t";
        }
    }
}