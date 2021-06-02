using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QLDonHang
{
    class DAONhanVien
    {
        NWDataContext db;

        public DAONhanVien()
        {
            db = new NWDataContext();
        }

        public dynamic LayDSNhanVien()
        {
            dynamic dsNhanVien = db.Employees
                                   .Select(f => new
                                   {
                                       f.EmployeeID, f.LastName, f.FirstName,
                                       f.BirthDate, f.HomePhone, f.Address
                                   });
            return dsNhanVien;
        }

        public bool ThemNhanVien(Employee nv)
        {
            try 
            {
                db.Employees.InsertOnSubmit(nv);
                db.SubmitChanges();

                return true;
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
                return false;
            }
        }

        public bool SuaNhanVien(Employee nv)
        {
            try
            {
                Employee emp = db.Employees.First(f => f.EmployeeID == nv.EmployeeID);

                emp.FirstName = nv.FirstName;
                emp.LastName = nv.LastName;
                emp.Address = nv.Address;
                emp.BirthDate = nv.BirthDate;
                emp.HomePhone = nv.HomePhone;

                db.SubmitChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool XoaNhanVien(int maNV)
        {
            try
            {
                Employee emp = db.Employees.First(f => f.EmployeeID == maNV);

                db.Employees.DeleteOnSubmit(emp);

                db.SubmitChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
