using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GHWarehouseApp.Views
{
    public class ProductScan
    {
        public string ProductName { get; set; }
        public int ScannedQuantity { get; set; }
        public int RequiredQuantity { get; set; }
    }
}
