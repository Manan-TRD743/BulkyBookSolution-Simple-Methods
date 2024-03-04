using BulkyBook_WebAPI.Model;
using BulkyBook_WebAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace BulkyBook_WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : Controller
    {
        private readonly ICompany CompanyObj;
        public CompanyController(ICompany Company)
        {
            CompanyObj = Company;
        }

        #region GetData
        // GET: api/Company
        [HttpGet]
        public async Task<IActionResult> GetCompany()
        {
            try
            {
                var GetCompany = await CompanyObj.GetAllCompanyAsync();
                if (GetCompany == null)
                {
                    // Handle the case where Company is null
                    return StatusCode(500, "Failed to retrieve Company from the database.");
                }
                else
                {
                    // Return success response with the list of Company
                    return Ok(new { StatusCode = 200, status = "Success", Companies = GetCompany });
                }

            }
            catch (Exception ex)
            {
                // Return error response with 500 status code
                return StatusCode(500, new { error = "Internal Server Error : " + ex.Message });
            }
        }

        // GET: api/Company/5
        [HttpPost("{id}")]
        public async Task<IActionResult> GetCompanyById(int id)
        {

            try
            {
                if (id < 1)
                {
                    // Bad request if id is invalid
                    return BadRequest(new { StatusCode = 400, Message = "Bad Request" });
                }
                var Company =await CompanyObj.GetCompanyAsync(id);
                if (Company == null)
                {
                    // Return NotFound if the Company is not found
                    return NotFound(new { StatusCode = 404, Message = "Not Found" });
                }
                // Return the Company if found
                return Ok(new { StatusCode = 200, status = "Success", Companies = Company });
            }
            catch (Exception ex)
            {
                // Return error response with 500 status code
                return StatusCode(500, new { error = "Internal Server Error : " + ex.Message });
            }

        }
        #endregion

        #region Add Data
        // POST: api/Companys
        [HttpPost]
        public async Task<IActionResult> PostCompany(Company Company)
        {
            try
            {
                if (Company == null)
                {
                    return NotFound(new { StatusCode = 404, Status = "Company not found" });
                }
                await CompanyObj.AddCompanyAsync(Company);
               await CompanyObj.SaveCompanyAsync();
                return Ok(new { StatusCode = 200, status = "Success", Companys = Company });
            }
            catch (Exception ex)
            {
                // Return error response with 500 status code
                return StatusCode(500, new { error = "Internal Server Error : " + ex.Message });
            }
        }
        #endregion

        #region Update Data
        // PUT: api/Companys
        [HttpPut]
        public async Task<IActionResult> UpdateCompany(Company CompanyData)
        {
            try
            {
                if (CompanyData == null || CompanyData.CompanyID == 0)
                {
                    // Bad request if CompanyData is null or has an invalid CompanyId
                    return BadRequest(new { StatusCode = 400, Message = "Bad Request" });
                }
                await CompanyObj.UpdateCompanyAsync(CompanyData);
                await CompanyObj.SaveCompanyAsync();
                return Ok(new { StatusCode = 200, status = "Success", Companys = CompanyData });

            }
            catch (Exception ex)
            {
                // Return error response with 500 status code
                return StatusCode(500, new { error = "Internal Server Error : " + ex.Message });
            }
        }
        #endregion

        #region Delete Data
        // DELETE: api/Companys/5
        [HttpDelete("{id}")]

        public async Task<IActionResult> DeleteProduct(int id)
        {
            try
            {
                if (id < 1)
                {
                    // Bad request if id is invalid
                    return BadRequest(new { StatusCode = 400, Message = "Bad Request" });
                }
                // Find the Company to delete by id
                Company DeleteCompany = await CompanyObj.GetCompanyAsync(id);

                if (DeleteCompany == null)
                {
                    // Return NotFound if the Company to delete is not found
                    return NotFound(new { StatusCode = 404, Message = "Not Found" });
                }
                else
                {
                   await CompanyObj.DeleteCompanyAsync(DeleteCompany);
                    await CompanyObj.SaveCompanyAsync();
                    return Ok(new { StatusCode = 200, status = "Success" });

                }

            }
            catch (Exception ex)
            {
                // Return error response with 500 status code
                return StatusCode(500, new { error = "Internal Server Error : " + ex.Message });
            }
        }
        #endregion
    }
        
    }

