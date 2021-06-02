using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace QLDonHang
{
    class BUSSanPham
    {
        DAOSanPham da;
        public BUSSanPham()
        {
            da = new DAOSanPham();
        }

        public void LayDSLoaiSanPham(ComboBox cb)
        {
            cb.DataSource = da.LayDSLoaiSanPham();

            cb.DisplayMember = "CategoryName";
            cb.ValueMember = "CategoryID";
        }

        public void LayDSNhaCungCap(ComboBox cb)
        {
            cb.DataSource = da.LayDSNhaCungCap();

            cb.DisplayMember = "CompanyName";
            cb.ValueMember = "SupplierID";
        }

        public void LayDSSanPham(DataGridView dg)
        {
            dg.DataSource = da.LayDSSanPham();
        }

        public void LayDSSanPham(ComboBox cb)
        {
            cb.DataSource = da.LayDSSanPham2();

            cb.DisplayMember = "ProductName";
            cb.ValueMember = "ProductID";
        }

        public void ThemSanPham(Product sanPham)
        {
            if (da.ThemSanPham(sanPham))
            {
                MessageBox.Show("Thêm thành công");
            }
            else
            {
                MessageBox.Show("Thêm thất bại");
            }
        }

        public void SuaSanPham(Product sanPham)
        {
            if (da.SuaSanPham(sanPham))
            {
                MessageBox.Show("Sửa thành công");
            }
            else
            {
                MessageBox.Show("Sửa thất bại");
            }
        }

        public void XoaSanPham(int maSP)
        {
            if (da.XoaSanPham(maSP))
            {
                MessageBox.Show("Xóa thành công");
            }
            else
            {
                MessageBox.Show("Sản phẩm đang được tham chiếu, không thể xóa !!!");
            }
        }
    }
}
