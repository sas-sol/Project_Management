using System;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project_Management.Data;

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
        [HttpGet("{id}")]
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
                    return false;
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
    }
}
