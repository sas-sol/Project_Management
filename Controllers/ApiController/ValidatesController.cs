using System;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project_Management.Data;
using Project_Management.Models.Projects;

namespace Project_Management.Controllers.ApiController
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValidatesController : ControllerBase
    {
        private readonly ProjectContext _context;
        public ValidatesController(ProjectContext context)
        {
            _context = context;
        }
        [HttpGet("validate/{id}")]
        public bool validateLicense(string id)
        {
            try
            {
                var license = _context.License.Where(l => l.Project_id == id).FirstOrDefault();
                if (license != null)
                {
                    if (license.Expiry_Date > DateTime.Now)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }

        }
        [HttpGet("GetUrl/{id}")]
        public IActionResult GetUrl(int id)
        {
           try
            {
                var data = _context.ProjectApiUrl.Where(p => p.id == id).FirstOrDefault();
                if (data != null)
                {
                    string url = data.Project_Id + "," + data.Api_Url;
                    return Content(url);
                }

                return Content("Record Not Found");
            }
            catch (Exception ex)
            {
                return Content(ex.Message);
            }
        }
    }
}
