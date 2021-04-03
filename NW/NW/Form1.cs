using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Configuration;

namespace NW
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        
        //
        SqlConnection conn;

        void Connection()
        {
            string cnstr = ConfigurationManager.ConnectionStrings["cnstr"].ConnectionString;
            conn = new SqlConnection(cnstr);
        }

        DataTable LayDSNhanVien()
        {
            string query = "select * from Employees";
            SqlDataAdapter da = new SqlDataAdapter(query, conn);
            DataSet ds = new DataSet();
            da.Fill(ds);
            return ds.Tables[0];
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Connection();
            dGNhanVien.DataSource = LayDSNhanVien();
        }

        private void btThem_Click(object sender, EventArgs e)
        {
            try
            {
                string query = string.Format("set dateformat dmy; insert into Employees(LastName,FirstName,BirthDate,Address,HomePhone) values(N'{0}',N'{1}',N'{2}',N'{3}',N'{4}')", txtHoten.Text, txtHoten.Text, dtpNgaySinh.Value.ToShortDateString(), txtDiaChi.Text, txtDienThoai.Text);
                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();
                cmd.CommandType = CommandType.Text;
                cmd.ExecuteNonQuery();
                dGNhanVien.Columns.Clear();
                dGNhanVien.DataSource = LayDSNhanVien();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message.ToString());
            }
            finally
            {
                conn.Close();
            }
        }

        private void dGNhanVien_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex <= (dGNhanVien.Rows.Count - 1))
            {
                txtHoten.Text = dGNhanVien.Rows[e.RowIndex].Cells["FirstName"].Value.ToString() + " " + dGNhanVien.Rows[e.RowIndex].Cells["LastName"].Value.ToString();
                dtpNgaySinh.Text = dGNhanVien.Rows[e.RowIndex].Cells["BirthDate"].Value.ToString();
                txtDiaChi.Text = dGNhanVien.Rows[e.RowIndex].Cells["Address"].Value.ToString();
                txtDienThoai.Text = dGNhanVien.Rows[e.RowIndex].Cells["HomePhone"].Value.ToString();
            }
        }

        private void btXoa_Click(object sender, EventArgs e)
        {
            string giatri;
            giatri = dGNhanVien.Rows[dGNhanVien.CurrentRow.Index].Cells[0].Value.ToString();
            try
            {
                string query = string.Format("delete from Employees where EmployeeID = {0}",giatri);
                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();
                cmd.CommandType = CommandType.Text;
                cmd.ExecuteNonQuery();
                dGNhanVien.Columns.Clear();
                dGNhanVien.DataSource = LayDSNhanVien();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message.ToString());
            }
            finally
            {
                conn.Close();
            }
        }

        
    }
}
