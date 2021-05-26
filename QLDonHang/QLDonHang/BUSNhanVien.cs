using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace QLDonHang
{
    class BUSNhanVien
    {
        DAONhanVien daNV;

        public BUSNhanVien()
        {
            daNV = new DAONhanVien();
        }

        public void LayDSNhanVien(DataGridView dg)
        {
            dg.DataSource = daNV.LayDSNhanVien();
        }

        public void ThemNhanVien(Employee nv)
        {
            if (daNV.ThemNhanVien(nv)) 
            {
                MessageBox.Show("Thêm thành công");
            }
            else 
            {
                MessageBox.Show("Thêm thất bại");
            }
        }

        public void SuaNhanVien(Employee nv)
        {
            if (daNV.SuaNhanVien(nv))
            {
                MessageBox.Show("Sửa thành công");
            }
            else
            {
                MessageBox.Show("Sửa thất bại hoặc không tìm thấy thông tin nhân viên");
            }
        }

        public void XoaNhanVien(int maNV)
        {
            if (daNV.XoaNhanVien(maNV))
            {
                MessageBox.Show("Xóa thành công");
            }
            else
            {
                MessageBox.Show("Xóa thất bại, dữ liệu nhân viên đã được tham chiếu");
            }
        }
    }
}
