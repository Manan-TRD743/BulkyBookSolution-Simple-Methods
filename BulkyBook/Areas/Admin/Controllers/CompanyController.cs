using BulkyBookDataAccess.Repository.IRepository;
using BulkyBookModel;
using BulkyBookUtility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BulkyBook.Areas.Admin.Controllers
{
    [Area("Admin")]
    // Only Admin is Authorize Person
    [Authorize(Roles = StaticData.RoleUserAdmin)]
    public class CompanyController : Controller
    {
        private readonly IUnitOfWork CompanyUnitOfWork;

        public CompanyController(IUnitOfWork UnitOfWorkCompany)
        {
            CompanyUnitOfWork = UnitOfWorkCompany;
        }
        #region Get Company List
        public IActionResult Index()
        {
            List<CompanyModel> objCompanyList = CompanyUnitOfWork.Company.GetAll().ToList();
                return View(objCompanyList);
        }
        #endregion

        #region Create and Update Company Details

        public IActionResult UpsertCompany(int? id)
        {
            //Check Company id is null or not
            if(id==null || id == 0)
            {
                return View(new CompanyModel());
            }
            else
            {
                //GetCompany Details From ID for Update Detalis
                CompanyModel Company = CompanyUnitOfWork.Company.Get(u=>u.CompanyID.Equals(id));
                return View(Company);
            }
          
        }

        [HttpPost]
        public IActionResult UpsertCompany(CompanyModel Companyobj)
        {
            if (Companyobj != null)
            {
                if (ModelState.IsValid)
                { 
                    if(Companyobj.CompanyID == 0)
                    {
                        //Add Company
                        CompanyUnitOfWork.Company.Add(Companyobj);
                        CompanyUnitOfWork.Save();
                        TempData["Success"] = "New Company Created Successfully";
                    }
                    else
                    {
                        //Update Company Details
                        CompanyUnitOfWork.Company.UpdateCompany(Companyobj);
                        CompanyUnitOfWork.Save();
                        TempData["Success"] = "Company Details Updated Successfully";
                    }
                  
                    
                    return RedirectToAction("Index");
                }
                else
                {
                    return View(Companyobj);
                }
               
            }
            else
            {
                return NotFound();
            }

        }
        #endregion

        #region GetALl Data for Datatable
        [HttpGet]
        public IActionResult GetAllCompany()
        {
            // Get Company List and return it in json Format
            List<CompanyModel> Companylist = CompanyUnitOfWork.Company.GetAll().ToList();
            return Json(new {Data = Companylist});
        }
        #endregion

        #region Delete Company
        [HttpDelete]
        public IActionResult DeleteCompany(int id)
        {
            //Get Company Details From Copmany ID 
            CompanyModel CompanyToBeDelelted = CompanyUnitOfWork.Company.Get(u => u.CompanyID.Equals(id));
            if(CompanyToBeDelelted == null)
            {
                return Json( new  { sucess = false,message="Error While Deleting" } );
            }
                //Delete Company
                CompanyUnitOfWork.Company.Remove(CompanyToBeDelelted);
                CompanyUnitOfWork.Save();
                return Json(new { sucess = true, message = "Delete Sucessfully" });
                
            }
        }
        #endregion
    }

