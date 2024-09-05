using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_Brickstore
{
    class Items
    {
        int itemID;
        string itemName;
        string categoryName;
        string colorName;
        int qty;

        public Items(int itemID, string itemName, string categoryName, string colorName, int qty)
        {
            this.itemID = itemID;
            this.itemName = itemName;
            this.categoryName = categoryName;
            this.colorName = colorName;
            this.qty = qty;
        }

        public int ItemID { get => itemID; set => itemID = value; }
        public string ItemName { get => itemName; set => itemName = value; }
        public string CategoryName { get => categoryName; set => categoryName = value; }
        public string ColorName { get => colorName; set => colorName = value; }
        public int Qty { get => qty; set => qty = value; }
    }
}
