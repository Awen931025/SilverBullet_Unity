using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace W.Stock
{
    class Plate
    {
        public int id;
        public string name;
        public float yk;
        public float ykRate;
        public float cb;
        public float cbRate;
        public float sz;
        public float szRate;
        public string content;
        public override string ToString()
        {
            return name + "\t"
                + yk + "\t" + ykRate + "%\t"
                + cb + "\t" + cbRate + "%\t"
                + sz + "\t" + szRate + "%\t";
        }
    }
}
