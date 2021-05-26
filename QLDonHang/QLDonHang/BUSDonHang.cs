using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace QLDonHang
{
    class BUSDonHang
    {
        DAODonHang da;
        DAOSanPham ds;

        public BUSDonHang()
        {
            da = new DAODonHang();
            ds = new DAOSanPham();
        }

        public void LayDSDonHang(DataGridView dg)
        {
            dg.DataSource = da.LayDSDonHang();
        }

        public void LayDSKhachHang(ComboBox cb)
        {
            cb.DataSource = da.LayDSKhachHang();
            cb.DisplayMember = "CompanyName";
            cb.ValueMember = "CustomerID";
        }

        public void LayDSNhanVien(ComboBox cb)
        {
            cb.DataSource = da.LayDSNhanVien();
            cb.DisplayMember = "FirstName";
            cb.ValueMember = "EmployeeID";
        }

        public void ThemDonHang(Order donHang)
        {
            try
            {
                da.ThemDonHang(donHang);
            }
            catch (Exception)
            {
                MessageBox.Show("Error!");
            }
        }

        public void SuaDonHang(Order donHang)
        {
            if (da.SuaDonHang(donHang))
            {
                MessageBox.Show("Sửa thành công");
            }
            else
            {
                MessageBox.Show("Không có mã đơn hàng");
            }
        }

        public void XoaDonHang(Order donHang)
        {

            if (da.XoaDonHang(donHang))
            {
                MessageBox.Show("Xóa thành công");
            }
            else
            {
                MessageBox.Show("Xóa thất bại");
            }
        }

        public void LayDSCTDH(DataGridView dg, int maDH) 
        {
            try
            {
                dg.DataSource = da.LayDSCTHD(maDH);
            }
            catch (Exception)
            {

                MessageBox.Show("Error!");
            }
        }

        public Product HienThiThongTinSP(int maSP)
        {
            Product p = ds.LayThongTinSanPham(maSP);

            return p;
        }

        public void DatHang(List<Order_Detail> dsSanPham)
        {
            if (da.ThemCTDH(dsSanPham))
            {
                MessageBox.Show("Đặt hàng thành công");
            }
            else
            {
                MessageBox.Show("Đặt hàng thất bại");
            }
        }


    }
}
