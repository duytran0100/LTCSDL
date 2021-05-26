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
    public partial class FNhanVien : Form
    {
        BUSNhanVien busNhanVien;

        public FNhanVien()
        {
            InitializeComponent();

            busNhanVien = new BUSNhanVien();
        }

        private void quảnLýSảnPhẩmToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FSanPham f = new FSanPham();
            this.Hide();
            f.ShowDialog();
        }

        private void quảnLýĐơnHàngToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form1 f = new Form1(); //Form Quản lý đơn hàng
            this.Hide();
            f.ShowDialog();
        }

        private void FNhanVien_Load(object sender, EventArgs e)
        {
            CapNhatDSNhanVien();
        }

        private void CapNhatDSNhanVien()
        {
            dGNhanVien.Columns.Clear();
            busNhanVien.LayDSNhanVien(dGNhanVien);

            dGNhanVien.Columns[0].Width = (int)(0.15 * dGNhanVien.Width);
            dGNhanVien.Columns[1].Width = (int)(0.15 * dGNhanVien.Width);
            dGNhanVien.Columns[2].Width = (int)(0.15 * dGNhanVien.Width);
            dGNhanVien.Columns[3].Width = (int)(0.15 * dGNhanVien.Width);
            dGNhanVien.Columns[4].Width = (int)(0.15 * dGNhanVien.Width);
            dGNhanVien.Columns[5].Width = (int)(0.2 * dGNhanVien.Width);
        }

        private void btThoat_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void dGNhanVien_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.RowIndex >= 0 && e.RowIndex < dGNhanVien.Rows.Count)
            {
                String firstName = dGNhanVien.Rows[e.RowIndex].Cells["FirstName"].Value.ToString();
                String lastName = dGNhanVien.Rows[e.RowIndex].Cells["LastName"].Value.ToString();

                txtHoten.Text = lastName + " " + firstName;
                dtpNgaySinh.Text = dGNhanVien.Rows[e.RowIndex].Cells["BirthDate"].Value.ToString();
                txtDiaChi.Text = dGNhanVien.Rows[e.RowIndex].Cells["Address"].Value.ToString();
                txtDienThoai.Text = dGNhanVien.Rows[e.RowIndex].Cells["HomePhone"].Value.ToString();
            }
        }

        private void btThem_Click(object sender, EventArgs e)
        {
           if(txtHoten.Text.Length == 0 || txtDienThoai.Text.Length == 0)
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin nhân viên");
            }
            else
            {
                String Name = txtHoten.Text.Trim();
                Employee nv = new Employee();
                if (Name.Contains(" "))
                {
                    String lastName = Name.Substring(0, Name.LastIndexOf(' '));
                    String firstName = Name.Substring(Name.LastIndexOf(' ') + 1);

                    nv.FirstName = firstName;
                    nv.LastName = lastName;
                    nv.HomePhone = txtDienThoai.Text;
                    nv.Address = txtDiaChi.Text;
                    nv.BirthDate = dtpNgaySinh.Value.Date;
                }
                else
                {
                    nv.LastName = "";
                    nv.FirstName = Name;
                    nv.HomePhone = txtDienThoai.Text;
                    nv.Address = txtDiaChi.Text;
                    nv.BirthDate = dtpNgaySinh.Value.Date;
                }

                busNhanVien.ThemNhanVien(nv);

                resetTextBox();
                CapNhatDSNhanVien();
            }
        }

        private void btSua_Click(object sender, EventArgs e)
        {
            if (txtHoten.Text.Length == 0)
            {
                MessageBox.Show("Vui lòng nhập họ tên nhân viên");
            }
            else
            {
                String Name = txtHoten.Text.Trim();
                Employee nv = new Employee();
                nv.EmployeeID = int.Parse(dGNhanVien.CurrentRow.Cells["EmployeeID"].Value.ToString());

                if (Name.Contains(" "))
                {
                    String lastName = Name.Substring(0, Name.LastIndexOf(' '));
                    String firstName = Name.Substring(Name.LastIndexOf(' ') + 1);

                    nv.FirstName = firstName;
                    nv.LastName = lastName;
                    nv.HomePhone = txtDienThoai.Text;
                    nv.Address = txtDiaChi.Text;
                    nv.BirthDate = dtpNgaySinh.Value.Date;
                }
                else
                {
                    nv.LastName = "";
                    nv.FirstName = Name;
                    nv.HomePhone = txtDienThoai.Text;
                    nv.Address = txtDiaChi.Text;
                    nv.BirthDate = dtpNgaySinh.Value.Date;
                }

                busNhanVien.SuaNhanVien(nv);

                resetTextBox();
                CapNhatDSNhanVien();
            }
        }

        private void resetTextBox()
        {
            txtHoten.Text = "";
            txtDienThoai.Text = "";
            txtDiaChi.Text = "";
            dtpNgaySinh.Value = DateTime.Now;
        }

        private void btXoa_Click(object sender, EventArgs e)
        {
            DialogResult dlr = MessageBox.Show("Bạn có chắc xóa nhân viên này không ?", "Cảnh báo", 
                MessageBoxButtons.YesNo,MessageBoxIcon.Warning);

            if(dlr == DialogResult.Yes)
            {
                int maNV = int.Parse(dGNhanVien.CurrentRow.Cells["EmployeeID"].Value.ToString());

                busNhanVien.XoaNhanVien(maNV);

                CapNhatDSNhanVien();
            }
        }
    }
}
