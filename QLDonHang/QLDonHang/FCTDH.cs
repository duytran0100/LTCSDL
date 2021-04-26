using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace QLDonHang
{
    public partial class FCTDH : Form
    {
        public int maDH;
        private BUSDonHang bus;

        public FCTDH()
        {
            InitializeComponent();
            bus = new BUSDonHang();
        }

        private void FCTDH_Load(object sender, EventArgs e)
        {
            bus.LayDSCTDH(gVCTDH, maDH);
            CapNhatDSDH();
        }

        void CapNhatDSDH()
        {
            gVCTDH.Columns[0].Width = (int)(0.2 * gVCTDH.Width);
            gVCTDH.Columns[1].Width = (int)(0.2 * gVCTDH.Width);
            gVCTDH.Columns[2].Width = (int)(0.3 * gVCTDH.Width);
            gVCTDH.Columns[3].Width = (int)(0.2 * gVCTDH.Width);
        }

        private void gVCTDH_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.RowIndex >= 0 && e.RowIndex < gVCTDH.Rows.Count)
            {
                txtMaDH.Text = gVCTDH.Rows[e.RowIndex].Cells["OrderID"].Value.ToString();
                txtMaSP.Text = gVCTDH.Rows[e.RowIndex].Cells["ProductID"].Value.ToString();
                txtDonGia.Text = gVCTDH.Rows[e.RowIndex].Cells["UnitPrice"].Value.ToString();
                txtSoLuong.Text = gVCTDH.Rows[e.RowIndex].Cells["Quantity"].Value.ToString();
            }
        }
    }
}
