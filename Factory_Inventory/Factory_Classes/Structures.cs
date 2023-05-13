using Microsoft.SqlServer.Management.HadrModel;
using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms.DataVisualization.Charting;

namespace Factory_Inventory.Factory_Classes
{
    public class Structures
    {
        public struct Tray_Params
        {
            public float g_rd, n_rd;
            public float g_g,  n_g;
            public float gross_wt, tray_tare, net_wt, spring_wt;
            public int no_of_springs, s_rd, s_g;
            public float rd_percentage;
            public Tray_Params(int mode)
            {
                if(mode==0)
                {
                    this.g_rd = 0F; this.n_rd = 0F;
                    this.g_g = 0F; this.n_g = 0F;
                    this.gross_wt = 0F; this.tray_tare = 0F; this.net_wt = 0F; this.spring_wt = 0F;
                    this.no_of_springs = 0; this.s_rd = 0; this.s_g = 0;
                    this.rd_percentage = 0F;
                }
                else
                {
                    this.g_rd = 0F; this.n_rd = 0F;
                    this.g_g = 0F; this.n_g = 0F;
                    this.gross_wt = 0F; this.tray_tare = 0F; this.net_wt = 0F; this.spring_wt = 0F;
                    this.no_of_springs = 0; this.s_rd = 0; this.s_g = 0;
                    this.rd_percentage = 0F;
                }
            }
        };

        public struct ErrorTable
        {
            public DataTable dt;
            public SqlException e;
            public ErrorTable(DataTable dti, SqlException ei)
            {
                this.dt = dti;
                this.e = ei;
            }
        };
    }
}
