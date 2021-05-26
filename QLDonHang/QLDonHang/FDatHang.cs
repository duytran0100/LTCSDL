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
    public partial class FDatHang : Form
    {
        BUSSanPham busSP;
        BUSDonHang busDH;

        public FDatHang()
        {
            InitializeComponent();
            busSP = new BUSSanPham();
            busDH = new BUSDonHang();
        }

        public int maDH;
        bool co = false;
        DataTable dtSanPham;

        private void FDatHang_Load(object sender, EventArgs e)
        {
            txtMaDH.Enabled = false;
            txtMaDH.Text = maDH.ToString();
            LayDSSanPham();
            co = true;

            dtSanPham = new DataTable();

            dtSanPham.Columns.Add("ProductID");
            dtSanPham.Columns.Add("UnitPrice");
            dtSanPham.Columns.Add("Quantity");
            dtSanPham.Columns.Add("Discount");

            dGSP.DataSource = dtSanPham;

            dGSP.Columns[0].Width = (int)(dGSP.Width * 0.22);
            dGSP.Columns[1].Width = (int)(dGSP.Width * 0.22);
            dGSP.Columns[2].Width = (int)(dGSP.Width * 0.22);
            dGSP.Columns[3].Width = (int)(dGSP.Width * 0.22);
        }

        private void LayDSSanPham()
        {
            busSP.LayDSSanPham(cbTenSP);
        }

        private void HienThiThongTinSanPham(String maSP)
        {
            int ma = int.Parse(maSP);
            Product p = busDH.HienThiThongTinSP(ma);

            txtNCC.Text = p.Supplier.CompanyName;
            txtDonGia.Text = p.UnitPrice.ToString();
            txtLoaiSP.Text = p.Category.CategoryName;
            txtGiamGia.Text = "0";
        }

        private void cbTenSP_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (co)
            {
                HienThiThongTinSanPham(cbTenSP.SelectedValue.ToString());
            }
        }

        private void btThem_Click(object sender, EventArgs e)
        {
            bool kiemtra = true;

            foreach (DataRow item in dtSanPham.Rows)
            {
                if(item[0].ToString() == cbTenSP.SelectedValue.ToString())
                {
                    kiemtra = false;
                    item[2] = int.Parse(item[2].ToString()) + int.Parse(numSoLuong.Value.ToString());
                    break;
                }
            }

            if (kiemtra)
            {
                DataRow r = dtSanPham.NewRow();

                r[0] = cbTenSP.SelectedValue.ToString();
                r[1] = txtDonGia.Text;
                r[2] = numSoLuong.Value.ToString();
                r[3] = txtGiamGia.Text;

                dtSanPham.Rows.Add(r);
            }
        }

        private void dGSP_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.RowIndex >= 0 && e.RowIndex < dGSP.Rows.Count - 1)
            {
                cbTenSP.SelectedValue = int.Parse(dGSP.Rows[e.RowIndex].Cells[0].Value.ToString());
            }
        }

        private void btSua_Click(object sender, EventArgs e)
        {
            bool kiemtra = false;

            foreach (DataRow item in dtSanPham.Rows)
            {
                if (item[0].ToString() == cbTenSP.SelectedValue.ToString())
                {
                    kiemtra = true;
                    item[2] = numSoLuong.Value.ToString();
                    item[3] = txtGiamGia.Text;
                    break;
                }
            }

            if (kiemtra)
            {
                MessageBox.Show("Sửa đơn hàng thành công");
            }
            else
            {
                MessageBox.Show("Không tìm thấy thông tin sản phẩm");
            }
        }

        private void btXoa_Click(object sender, EventArgs e)
        {
            try
            {
                dGSP.Rows.Remove(dGSP.CurrentRow);
            }
            catch
            {
                MessageBox.Show("Danh sách rỗng, không thể xóa");
            }
        }

        private void btDatHang_Click(object sender, EventArgs e)
        {
            List<Order_Detail> dsSanPham = new List<Order_Detail>();

            foreach(DataRow item in dtSanPham.Rows)
            {
                Order_Detail sanPham = new Order_Detail();

                sanPham.OrderID = int.Parse(txtMaDH.Text);
                sanPham.ProductID = int.Parse(cbTenSP.SelectedValue.ToString());
                sanPham.UnitPrice = decimal.Parse(txtDonGia.Text);
                sanPham.Quantity = short.Parse(numSoLuong.Value.ToString());
                sanPham.Discount = int.Parse(txtGiamGia.Text);

                dsSanPham.Add(sanPham);
            }

            if(dsSanPham.Count > 0)
            {
                busDH.DatHang(dsSanPham);
            }
            else
            {
                MessageBox.Show("Danh sách đặt hàng rỗng");
            }
        }
    }
}
