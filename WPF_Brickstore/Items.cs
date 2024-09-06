using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_Brickstore
{
    class Items
    {
        public string ItemId { get; set; }
        public string ItemName { get; set; }
        public string CategoryName { get; set; }
        public string ColorName { get; set; }
        public int Qty { get; set; }

        public Items(string itemId, string itemName, string categoryName, string colorName, int qty)
        {
            ItemId = itemId;
            ItemName = itemName;
            CategoryName = categoryName;
            ColorName = colorName;
            Qty = qty;
        }
    }
}
