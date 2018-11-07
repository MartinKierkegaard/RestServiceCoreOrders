using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestServiceCoreOrders.Model
{
    public class OrderLine
    {
        public int ProductID { get; set; }

        public int OrderQty { get; set; }

        public int UnitPrice { get; set; }

        public int UnitPriceDiscount { get; set; }
    }
}
