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
    public partial class FSanPham : Form
    {
        BUSSanPham bus_SP;
        public FSanPham()
        {
            InitializeComponent();
            bus_SP = new BUSSanPham();
        }

        private void FSanPham_Load(object sender, EventArgs e)
        {
            LayDSLoaiSanPham();
            LayDSNhaCungCap();
            CapNhatDSSanPham();
        }

        private void LayDSLoaiSanPham()
        {
            bus_SP.LayDSLoaiSanPham(cbLoaiSP);
        }

        private void LayDSNhaCungCap()
        {
            bus_SP.LayDSNhaCungCap(cbNCC);
        }

        private void CapNhatDSSanPham()
        {
            dGSP.Columns.Clear();
            bus_SP.LayDSSanPham(dGSP);
            dGSP.Columns[0].Width = (int)(0.15 * dGSP.Width);
            dGSP.Columns[1].Width = (int)(0.15 * dGSP.Width);
            dGSP.Columns[2].Width = (int)(0.15 * dGSP.Width);
            dGSP.Columns[3].Width = (int)(0.15 * dGSP.Width);
            dGSP.Columns[4].Width = (int)(0.15 * dGSP.Width);
            dGSP.Columns[5].Width = (int)(0.15 * dGSP.Width);
        }

        private void dGSP_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.RowIndex >= 0 && e.RowIndex < dGSP.Rows.Count )
            {
                txtTenSP.Text = dGSP.CurrentRow.Cells["ProductName"].Value.ToString();
                txtSoLuong.Text = dGSP.CurrentRow.Cells["UnitsInStock"].Value.ToString();
                txtDonGia.Text = dGSP.CurrentRow.Cells["UnitPrice"].Value.ToString();
                cbLoaiSP.SelectedIndex = cbLoaiSP.FindStringExact(dGSP.CurrentRow.Cells["CategoryName"].Value.ToString());
                cbNCC.SelectedIndex = cbNCC.FindStringExact(dGSP.CurrentRow.Cells["CompanyName"].Value.ToString());
            }
        }

        private void btThem_Click(object sender, EventArgs e)
        {
            Product sanPham = new Product();
            sanPham.ProductName = txtTenSP.Text;
            sanPham.UnitPrice = (decimal?)double.Parse(txtDonGia.Text);
            sanPham.UnitsInStock = (short?)int.Parse(txtSoLuong.Text);
            sanPham.SupplierID = int.Parse(cbNCC.SelectedValue.ToString());
            sanPham.CategoryID = int.Parse(cbLoaiSP.SelectedValue.ToString());

            bus_SP.ThemSanPham(sanPham);
            CapNhatDSSanPham();
        }

        private void btSua_Click(object sender, EventArgs e)
        {
            Product sanPham = new Product();
            sanPham.ProductID = int.Parse(dGSP.CurrentRow.Cells["ProductID"].Value.ToString());
            sanPham.ProductName = txtTenSP.Text;
            sanPham.UnitPrice = (decimal?)double.Parse(txtDonGia.Text);
            sanPham.UnitsInStock = (short?)int.Parse(txtSoLuong.Text);
            sanPham.SupplierID = int.Parse(cbNCC.SelectedValue.ToString());
            sanPham.CategoryID = int.Parse(cbLoaiSP.SelectedValue.ToString());

            bus_SP.SuaSanPham(sanPham);
            CapNhatDSSanPham();
        }

        private void btThoat_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void quảnLýĐơnHàngToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form1 f = new Form1();
            this.Hide();
            f.ShowDialog();
        }

        private void QLSPToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FNhanVien f = new FNhanVien();
            this.Hide();
            f.ShowDialog();
        }

        private void btXoa_Click(object sender, EventArgs e)
        {
            int maSP = int.Parse(dGSP.CurrentRow.Cells["ProductID"].Value.ToString());

            bus_SP.XoaSanPham(maSP);
            CapNhatDSSanPham();
        }
    }
}
