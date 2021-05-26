using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QLDonHang
{
    class DAOSanPham
    {
        NWDataContext db;
        public DAOSanPham()
        {
            db = new NWDataContext();
        }

        public dynamic LayDSLoaiSanPham()
        {
            dynamic dsLoaiSP = db.Categories
                                 .Select(f => new {f.CategoryID,f.CategoryName});
            return dsLoaiSP;
        }

        public dynamic LayDSNhaCungCap()
        {
            dynamic dsNCC = db.Suppliers
                              .Select(f => new { f.SupplierID, f.CompanyName });
            return dsNCC;
        }

        public dynamic LayDSSanPham()
        {
            dynamic dsSanPham = db.Products
                                  .Select(f => new {
                                      f.ProductID, f.ProductName, 
                                      f.UnitPrice, f.UnitsInStock,
                                      f.Category.CategoryName, f.Supplier.CompanyName });
            return dsSanPham;
        }

        public dynamic LayDSSanPham2()
        {
            dynamic dsSanPham = db.Products
                                  .Select(f => new {
                                      f.ProductID,
                                      f.ProductName,
                                  });
            return dsSanPham;
        }

        public Product LayThongTinSanPham(int maSP)
        {
            Product sp = db.Products.FirstOrDefault(f => f.ProductID == maSP);

            return sp;
        }

        public bool ThemSanPham(Product sanPham)
        {
            try
            {
                db.Products.InsertOnSubmit(sanPham);
                db.SubmitChanges();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool SuaSanPham(Product sanPham)
        {
            try
            {
                Product p = db.Products.First(f => f.ProductID == sanPham.ProductID);
                p.ProductName = sanPham.ProductName;
                p.CategoryID = sanPham.CategoryID;
                p.SupplierID = sanPham.SupplierID;
                p.UnitPrice = sanPham.UnitPrice;
                p.UnitsInStock = sanPham.UnitsInStock;

                db.SubmitChanges();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
