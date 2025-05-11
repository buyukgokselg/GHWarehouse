using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GHWarehouseApp.Models
{
    public class Order
    {

        public string OrderNumber { get; set; }
        public string CustomerName { get; set; }
        public DateTime OrderDate { get; set; }
        public string Status { get; set; }

        public string StatusColor // Status'e göre renk döndüren property
        {
            get
            {
                switch (Status)
                {
                    case "Kargoda": return "Orange";
                    case "Tamamlandı": return "Green";
                    case "İade": return "Red";
                    case "Bekliyor": return "Gray";
                    case "Hazırlanıyor": return "Blue";
                    default: return "Black";
                }
            }
        }
    }
}
