using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProductsCrud.DAL;
using ProductsCrud.Models;

namespace ProductsCrud.Controllers
{
    public class ProductController : Controller
    {
        Product_DAL _productDAL = new Product_DAL();
        // GET: Product
        public ActionResult Index()
        {
            var productlist = _productDAL.GetAllProducts();
            if(productlist.Count == 0)
            {
                TempData["InfoMessage"] = "No Data In Database!!";
            }
            return View(productlist);
        }

        // GET: Product/Details/5
        public ActionResult Details(int id)
        {
            var products = _productDAL.GetProductById(id).FirstOrDefault();
            if(products == null )
            {
                TempData["InfoMessage"] = "Product Not available with ID : " + id.ToString();
                return RedirectToAction("Index");
            }
            return View(products);
        }

        // GET: Product/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Product/Create
        [HttpPost]
        public ActionResult Create(Product product)
        {
            bool isInserted = false;
            try
            {
                // TODO: Add insert logic here
                if(ModelState.IsValid)
                {
                   isInserted = _productDAL.InsertProducts(product);

                    if(isInserted)
                    {
                        TempData["SuccessMessage"] = "Product added Sucessfully!!!";
                    }
                    else
                    {
                        TempData["ErrorMEssage"] = "Product Already in Table";
                    }
                }

                return RedirectToAction("Index");
            }
            catch(Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return View();
            }
        }

        // GET: Product/Edit/5
        public ActionResult Edit(int id)
        {
            var products = _productDAL.GetProductById(id).FirstOrDefault();

            if(products == null)
            {
                TempData["InfoMessage"] = "Product Not Found!!!";
                return RedirectToAction("Index");
            }
            return View(products);
        }

        // POST: Product/Edit/5
        [HttpPost,ActionName("Edit")]
        public ActionResult UpdateProduct(Product product)
        {
            try
            {
                // TODO: Add update logic here

                if(ModelState.IsValid)
                {
                    bool IsUpdated = _productDAL.UpdateProducts(product);
                    if (IsUpdated)
                    {
                        TempData["SuccessMessage"] = "Product Added Successfully";
                    }
                    else
                    {
                        TempData["ErrorMEssage"] = "Product Already in Table";
                    }

                }



                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Product/Delete/5
        public ActionResult Delete(int id)


        {
            var products = _productDAL.GetProductById(id).FirstOrDefault();

            if(products == null)
            {
                TempData["InfoMessage"] = "Product not available with ID : " + id.ToString();
                return RedirectToAction("Index");
            }
            return View(products);
        }

        // POST: Product/Delete/5
        [HttpPost,ActionName("Delete")]
        public ActionResult DeleteProduct(int id )
        {
            try
            {
                // TODO: Add delete logic here

                if (ModelState.IsValid)
                {
                    bool IsDeleted = _productDAL.DeleteProductById(id);

                    if(IsDeleted)
                    {
                        TempData["InfoMEssage"] = "Product Deleted Successfully ID :" + id.ToString();
                    }
                    else
                    {
                        TempData["InfoMessage"] = " Product Cannot Found with ID  :" + id.ToString();
                    }
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
