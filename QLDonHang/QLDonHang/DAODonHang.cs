using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QLDonHang
{
    class DAODonHang
    {
        NWDataContext db;

        public DAODonHang()
        {
            db = new NWDataContext();
        }

        public IEnumerable<Order> LayDSDonHang2()
        {
            var dsDonHang = from i in db.Orders select i;

            return dsDonHang;
        }

        public dynamic LayDSDonHang()
        {
            dynamic dsDonHang = db.Orders.Select(s => 
                new {s.OrderID,s.OrderDate,s.Customer.CompanyName,s.Employee.FirstName}
                ).ToList();

            return dsDonHang;
        }

        public dynamic LayDSKhachHang()
        {
            dynamic dsKhachHang = db.Customers.Select(s => new { s.CustomerID, s.CompanyName });

            return dsKhachHang;
        }

        public dynamic LayDSNhanVien()
        {
            dynamic dsNhanVien = db.Employees.Select(s => new { s.EmployeeID, s.FirstName});

            return dsNhanVien;
        }

        public void ThemDonHang(Order donHang)
        {
            db.Orders.InsertOnSubmit(donHang);
            db.SubmitChanges();
        }

        public bool SuaDonHang(Order donHang)
        {
            try
            {
                Order d = db.Orders.First(f => f.OrderID == donHang.OrderID);
                d.EmployeeID = donHang.EmployeeID;
                d.CustomerID = donHang.CustomerID;
                d.OrderDate = donHang.OrderDate;

                db.SubmitChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool XoaDonHang(Order donHang)
        {
            try
            {
                List<Order_Detail> ds_CTDH = db.Order_Details.Where(f => f.OrderID == donHang.OrderID).ToList();
                Order d = db.Orders.Where(f => f.OrderID == donHang.OrderID).Single();

                db.Order_Details.DeleteAllOnSubmit(ds_CTDH);
                db.Orders.DeleteOnSubmit(d);

                db.SubmitChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool ThemCTDH(List<Order_Detail> dsSanPham)
        {
            try
            {
                foreach(Order_Detail d in dsSanPham)
                {
                    db.Order_Details.InsertOnSubmit(d);
                    db.SubmitChanges();
                }

                return true;
            }
            catch
            {
                return false;
            }
        }

        public dynamic LayDSCTHD(int maDH)
        {
            dynamic ds = db.Order_Details.Where(f => f.OrderID == maDH)
                                         .Select(f => new
                                         {
                                             f.OrderID,
                                             f.ProductID,
                                             f.UnitPrice,
                                             f.Quantity
                                         });
            return ds;
        }

    }
}
