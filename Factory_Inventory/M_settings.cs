﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Factory_Inventory
{
    public partial class M_settings : Form
    {
        public M_settings()
        {
            InitializeComponent();
        }

        private void M_settings_Load(object sender, EventArgs e)
        {
            m_S_printUC.load_data();
            m_S_defaultUC.load_data("Default");
        }
    }
}
