using BaiTapLon.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace BaiTapLon.Controllers
{
    public class ProductController : Controller
    {
        SaledbContext da = new SaledbContext();
        // GET: ProductController
        public ActionResult ListProduct()
        {
            List<Product> list = da.Products.Select(s => s).ToList();
            return View(list);
        }

        // GET: ProductController/Details/5
        public ActionResult Details(int id)
        {
            Product p = da.Products.Where(s => s.ProductId == id).FirstOrDefault();
            return View(p);
        }

        // GET: ProductController/Create
        public ActionResult Create()
        {
            ViewData["Brand"] = new SelectList(da.Brands, "BrandID", "BrandName");
            ViewData["Category"] = new SelectList(da.Categories, "CategoryID", "CategoryName");

            return View();
        }

        // POST: ProductController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Product product, IFormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here
                Product p = new Product();
                p = product;
                p.CategoryId = int.Parse(collection["Category"]);
                p.BrandId = int.Parse(collection["Brand"]);

                da.Products.Add(p);
                da.SaveChanges();

                return RedirectToAction("ListProducts");
            }
            catch
            {
                return View();
            }
        }

        // GET: ProductController/Edit/5
        public ActionResult Edit(int id)
        {
            Product p = da.Products.FirstOrDefault(s => s.ProductId == id);

            ViewData["Brand"] = new SelectList(da.Brands, "BrandID", "BrandName");
            ViewData["Category"] = new SelectList(da.Categories, "CategoryID", "CategoryName");

            return View(p);
        }

        // POST: ProductController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Product product, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here
                Product p = da.Products.First(s => s.ProductId == product.ProductId);
                p.ProductId = product.ProductId;
                p.ProductName = product.ProductName;
                p.Price = product.Price;
                p.UnitsInStock = product.UnitsInStock;
                p.Quantity = product.Quantity;

                

                p.CategoryId = int.Parse(collection["Category"]);
                p.BrandId = int.Parse(collection["Brand"]);

                da.SaveChanges();

                return RedirectToAction("ListProducts");
            }
            catch
            {
                return View();
            }
        }

        // GET: ProductController/Delete/5
        public ActionResult Delete(int id)
        {
            Product p = da.Products.FirstOrDefault(s => s.ProductId == id);
            return View(p);
        }

        // POST: Product/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, Product product)
        {
            try
            {
                // TODO: Add delete logic here
                Product p = da.Products.First(s => s.ProductId == id);

                da.Products.Remove(p);
                

                return RedirectToAction("ListProducts");
            }
            catch
            {
                return View();
            }
        }
    }
}
