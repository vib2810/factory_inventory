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
    public partial class O_SettingsForm : Form
    {
        //protected override void OnPaint(PaintEventArgs e)
        //{
        //    base.OnPaint(e);
        //    ControlPaint.DrawBorder(e.Graphics, ClientRectangle,
        //                                 Color.Black, 10, ButtonBorderStyle.Inset,
        //                                 Color.Black, 10, ButtonBorderStyle.Inset,
        //                                 Color.Black, 10, ButtonBorderStyle.Inset,
        //                                 Color.Black, 10, ButtonBorderStyle.Inset);
        //}
        public O_SettingsForm()
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
