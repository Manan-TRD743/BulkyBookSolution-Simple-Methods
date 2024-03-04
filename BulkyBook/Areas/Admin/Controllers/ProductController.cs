using BulkyBookDataAccess.Repository.IRepository;
using BulkyBookModel;
using BulkyBookModel.ViewModel;
using BulkyBookUtility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BulkyBook.Areas.Admin.Controllers
{
    [Area("Admin")]
    //only Admin is Authrorize
    [Authorize(Roles = StaticData.RoleUserAdmin)]
    public class ProductController : Controller
    {
        private readonly IUnitOfWork ProductUnitOfWork;
        private readonly IWebHostEnvironment ProductWebHostEnvironment;

        public ProductController(IUnitOfWork UnitOfWorkProduct, IWebHostEnvironment webHostEnvironment)
        {
            ProductUnitOfWork = UnitOfWorkProduct;
            ProductWebHostEnvironment = webHostEnvironment;
        }
        #region Get Product
        //Get All Product and return view
        public IActionResult Index()
        {
            //Get Data of Product also include the category 
            List<ProductModel> objProductList = ProductUnitOfWork.Product.GetAll(includeProperties: "Category").ToList();
                return View(objProductList);
        }
        #endregion

        #region Create and Update Product
        //Create and Update Product
        public IActionResult UpsertProduct(int? id)
        {
            // Fetch all categories from the database and project them into SelectListItem objects,
            // where Text is set to the category name and Value is set to the category ID converted to a string
            IEnumerable<SelectListItem> categorylist = ProductUnitOfWork.Category.GetAll().
                Select(u => new SelectListItem
                {
                    Text = u.CategoryName,
                    Value = u.CategoryID.ToString()
                });
            //Pass Data From Controller to View
            ViewBag.CategoryList = categorylist;

            ProductViewModel productViewModel = new()
            {
                CategoryList = categorylist,
                Product = new ProductModel()
            };
            //check the Product id is null or not
            if(id==null || id == 0)
            {
                
                return View(productViewModel);
            }
            else
            {
                //Get Product Details From Product Id
                productViewModel.Product = ProductUnitOfWork.Product.Get(u=>u.ProductID.Equals(id));
                return View(productViewModel);
            }
          
        }

        #region Add and Update Image of product
        [HttpPost]
        public IActionResult UpsertProduct(ProductViewModel ProductVM, IFormFile? file)
        {
            // Check if ProductViewModel is not null
            if (ProductVM != null)
            {
                // Check if ModelState is valid
                if (ModelState.IsValid)
                {
                    string WwwRootPath = ProductWebHostEnvironment.WebRootPath;

                    if (file != null)
                    {
                        string filename = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                        string productpath = Path.Combine(WwwRootPath, @"Images\Product");

                        //Delete the old image if exist
                        if (!string.IsNullOrEmpty(ProductVM.Product.ProductImgUrl))
                        {
                            //Delete Old Image
                            var OldImgPath = Path.Combine(WwwRootPath, ProductVM.Product.ProductImgUrl.TrimStart('\\'));
                            if (System.IO.File.Exists(OldImgPath))
                            {
                                System.IO.File.Delete(OldImgPath);
                            }
                        }
                        // Copy new image file to the server
                        using (var fileStream = new FileStream(Path.Combine(productpath, filename), FileMode.Create))
                        {
                            file.CopyTo(fileStream);
                        }
                        // Update ProductImgUrl with the new filename
                        ProductVM.Product.ProductImgUrl = @"\Images\Product\" + filename;
                    }

                    if (ProductVM.Product.ProductID == 0)
                    {
                        //Add Product
                        ProductUnitOfWork.Product.Add(ProductVM.Product);
                        ProductUnitOfWork.Save();
                        TempData["Success"] = "New Product Created Successfully";
                    }
                    else
                    {
                        //Update Product
                        ProductUnitOfWork.Product.Update(ProductVM.Product);
                        ProductUnitOfWork.Save();
                        TempData["Success"] = "Product Updated Successfully";
                    }

                    // Redirect to Index action
                    return RedirectToAction("Index");
                }
                else
                {
                    // Populate CategoryList for the view
                    ProductVM.CategoryList = ProductUnitOfWork.Category.GetAll().
                         Select(u => new SelectListItem
                         {
                             Text = u.CategoryName,
                             Value = u.CategoryID.ToString()
                         });
                    // Return the view with validation errors
                    return View(ProductVM);
                }

            }
            else
            {
                return NotFound();
            }

        }

        #endregion

        #endregion

        #region GetAll Product for Datatable 
        [HttpGet]
        public IActionResult GetAll()
        {
            List<ProductModel> productlist = ProductUnitOfWork.Product.GetAll(includeProperties: "Category").ToList();
            return Json(new {Data = productlist});
        }
        #endregion

        #region Delete Product
        //Delete Product
        [HttpDelete]
        public IActionResult DeleteProduct(int id)
        {
            //Get Product From id for delete
            ProductModel ProductToBeDelelted = ProductUnitOfWork.Product.Get(u => u.ProductID == id);
            if(ProductToBeDelelted == null)
            {
                return Json( new  { sucess = false,message="Error While Deleting" } );
            }

                 #region Delete Product Image
            // If a product image doesn't exist, delete it as well
            if (ProductToBeDelelted.ProductImgUrl == null)
            {
                ProductUnitOfWork.Product.Remove(ProductToBeDelelted);
                ProductUnitOfWork.Save();
                return Json(new { sucess = true, message = "Delete Sucessfully" });
            }
            else
            {
                var oldImgPath = Path.Combine(ProductWebHostEnvironment.WebRootPath, ProductToBeDelelted.ProductImgUrl!.TrimStart('\\'));
                //Check Img is there or not
                if (System.IO.File.Exists(oldImgPath))
                {
                    //Delete Img of Product
                    System.IO.File.Delete(oldImgPath);
                }
                #endregion

                //Remove Product
                ProductUnitOfWork.Product.Remove(ProductToBeDelelted);
                ProductUnitOfWork.Save();
                return Json(new { sucess = true, message = "Delete Sucessfully" });
            }
                
            }
           
        }
        #endregion
    }

