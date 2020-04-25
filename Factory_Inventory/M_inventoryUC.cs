using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Factory_Inventory.Factory_Classes;

namespace Factory_Inventory
{
    public partial class M_inventoryUC : UserControl
    {
        private DbConnect c;
        public M_inventoryUC()
        {
            InitializeComponent();
            this.c = new DbConnect();
        }
    }
}
