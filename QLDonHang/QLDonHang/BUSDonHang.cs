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

        public BUSDonHang()
        {
            da = new DAODonHang();
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
                MessageBox.Show("Sua thanh cong");
            }
            else
            {
                MessageBox.Show("Khong co ma don hang");
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

    }
}
