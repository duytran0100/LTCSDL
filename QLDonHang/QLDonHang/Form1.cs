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
    public partial class Form1 : Form
    {
        BUSDonHang bus;
        public Form1()
        {
            InitializeComponent();  
            bus = new BUSDonHang();
        }

        private void Form1_Load_1(object sender, EventArgs e)
        {
            bus.LayDSKhachHang(cbCustomer);
            bus.LayDSNhanVien(cbEmployee);
            CapNhatDSDH();
        }

        private void gVDH_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtMaDH.Enabled = false;
            if (e.RowIndex >= 0 && e.RowIndex <= gVDH.Rows.Count - 1)
            {
                txtMaDH.Text = gVDH.Rows[e.RowIndex].Cells["OrderID"].Value.ToString();
                dtpNgayDH.Text = gVDH.Rows[e.RowIndex].Cells["OrderDate"].Value.ToString();
                cbCustomer.SelectedIndex = cbCustomer.FindStringExact(gVDH.Rows[e.RowIndex].Cells["CompanyName"].Value.ToString());
                cbEmployee.SelectedIndex = cbEmployee.FindString(gVDH.Rows[e.RowIndex].Cells["FirstName"].Value.ToString());
            }
        }

        private void btThem_Click(object sender, EventArgs e)
        {
            Order donHang = new Order();

            donHang.OrderDate = dtpNgayDH.Value;
            donHang.CustomerID = cbCustomer.SelectedValue.ToString();
            donHang.EmployeeID = int.Parse(cbEmployee.SelectedValue.ToString());

            bus.ThemDonHang(donHang);

            CapNhatDSDH();
        }

        void CapNhatDSDH()
        {
            gVDH.Columns.Clear();
            bus.LayDSDonHang(gVDH);
            gVDH.Columns[0].Width = (int)(0.2 * gVDH.Width);
            gVDH.Columns[1].Width = (int)(0.2 * gVDH.Width);
            gVDH.Columns[2].Width = (int)(0.3 * gVDH.Width);
            gVDH.Columns[3].Width = (int)(0.2 * gVDH.Width);
        }

        private void btXoa_Click(object sender, EventArgs e)
        {
            return;
        }

        private void btSua_Click(object sender, EventArgs e)
        {
            Order donHang = new Order();

            donHang.OrderID = int.Parse(txtMaDH.Text);
            donHang.OrderDate = dtpNgayDH.Value;
            donHang.CustomerID = cbCustomer.SelectedValue.ToString();
            donHang.EmployeeID = int.Parse(cbEmployee.SelectedValue.ToString());

            bus.SuaDonHang(donHang);

            CapNhatDSDH();
        }

        private void gVDH_DoubleClick(object sender, EventArgs e)
        {
            FCTDH c = new FCTDH();
            c.maDH = int.Parse(gVDH.CurrentRow.Cells["OrderID"].Value.ToString());
            c.ShowDialog();
        }
    }
}
