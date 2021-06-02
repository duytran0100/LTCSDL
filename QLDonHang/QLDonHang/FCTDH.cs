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
        private BUSSanPham busSP;

        public FCTDH()
        {
            InitializeComponent();
            bus = new BUSDonHang();
            busSP = new BUSSanPham();
        }

        bool co = false;

        private void FCTDH_Load(object sender, EventArgs e)
        {
            txtMaDH.Enabled = false;
            txtDonGia.Enabled = false;

            txtMaDH.Text = maDH.ToString();

            busSP.LayDSSanPham(cbSanPham);
            CapNhatDSDH();

            co = true;
        }

        void CapNhatDSDH()
        {
            bus.LayDSCTDH(gVCTDH, maDH);
            gVCTDH.Columns[0].Width = (int)(0.2 * gVCTDH.Width);
            gVCTDH.Columns[1].Width = (int)(0.2 * gVCTDH.Width);
            gVCTDH.Columns[2].Width = (int)(0.3 * gVCTDH.Width);
            gVCTDH.Columns[3].Width = (int)(0.2 * gVCTDH.Width);
        }

        private void gVCTDH_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.RowIndex >= 0 && e.RowIndex < gVCTDH.Rows.Count)
            {
                cbSanPham.SelectedValue = int.Parse(gVCTDH.Rows[e.RowIndex].Cells["ProductID"].Value.ToString());
                txtDonGia.Text = gVCTDH.Rows[e.RowIndex].Cells["UnitPrice"].Value.ToString();
                numSoLuong.Value = decimal.Parse(gVCTDH.Rows[e.RowIndex].Cells["Quantity"].Value.ToString());
            }
        }

        private void btThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void quảnLýSảnPhẩmToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FSanPham f = new FSanPham();
            this.Hide();
            f.ShowDialog();
        }

        private void HienThiThongTinSanPham(String maSP)
        {
            int ma = int.Parse(maSP);
            Product p = bus.HienThiThongTinSP(ma);

            txtDonGia.Text = p.UnitPrice.Value.ToString();
        }

        private void cbSanPham_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (co)
            {
                HienThiThongTinSanPham(cbSanPham.SelectedValue.ToString());
            }
        }

        private void btThem_Click(object sender, EventArgs e)
        {
            if(numSoLuong.Value <= 0)
            {
                MessageBox.Show("Số lượng sản phẩm phải lớn hơn không");
                return;
            }


            bool kiemtra = false;

            String maSP = cbSanPham.SelectedValue.ToString();
            int cur_Quantity = 0;

            foreach(DataGridViewRow r in gVCTDH.Rows)
            {
                if (r.Cells["ProductID"].Value.ToString() == maSP)
                {
                    kiemtra = true;
                    cur_Quantity = int.Parse(r.Cells["Quantity"].Value.ToString());
                    break;
                }
            }

            Order_Detail donHang = new Order_Detail();

            donHang.OrderID = maDH;
            donHang.ProductID = int.Parse(maSP);

            if (kiemtra)
            {
                int new_Quantity = cur_Quantity + int.Parse(numSoLuong.Value.ToString());

                donHang.Quantity = short.Parse(new_Quantity.ToString());

                bus.SuaCTDH(donHang);
            }
            else
            {

                 donHang.UnitPrice = decimal.Parse(txtDonGia.Text);
                 donHang.Quantity = short.Parse(numSoLuong.Value.ToString());
                 donHang.Discount = 0;

                 bus.ThemCTDH(donHang);
            }

            CapNhatDSDH();
        }

        private void btSua_Click(object sender, EventArgs e)
        {
            Order_Detail donHang = new Order_Detail();

            donHang.OrderID = maDH;
            donHang.ProductID = int.Parse(gVCTDH.CurrentRow.Cells["ProductID"].Value.ToString());
            donHang.Quantity = short.Parse(numSoLuong.Value.ToString());

            bus.SuaCTDH(donHang);

            CapNhatDSDH();
        }

        private void btXoa_Click(object sender, EventArgs e)
        {
            DialogResult dlr = MessageBox.Show("Bạn có chắc xóa đơn hàng này không ?", "Cảnh báo",
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (dlr == DialogResult.Yes)
            {
                Order_Detail donHang = new Order_Detail();
                donHang.OrderID = maDH;
                donHang.ProductID = int.Parse(gVCTDH.CurrentRow.Cells["ProductID"].Value.ToString());

                bus.XoaCTDH(donHang);

                CapNhatDSDH();
            }
        }
    }
}
