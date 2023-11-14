using Microsoft.AspNetCore.Mvc;
using Project_Management.Data;
using Microsoft.EntityFrameworkCore;
using Project_Management.Models.Projects;
using System.Diagnostics.Metrics;
using System.Security.Claims;
using Project_Management.Models.Account;
using Microsoft.AspNetCore.Authorization;

namespace Project_Management.Controllers
{
    [Authorize]
    public class LicenseController : Controller
    {
        private readonly ProjectContext _context;
        public LicenseController (ProjectContext context)
        {
            _context = context;
        }
        //Return view for Add License
        [HttpGet]
        public IActionResult AddLicense(int id)
        {
            Response.Headers["Cache-Control"] = "no-cache, no-store, must-revalidate";
            Response.Headers["Pragma"] = "no-cache";
            Response.Headers["Expires"] = "-1";
            if (User.Identity.IsAuthenticated)
           {
                var project = _context.Project.Where(p => p.Id == id).FirstOrDefault();
                var license = _context.License.Where(l => l.Project_id == project.Project_Id).FirstOrDefault();
                if (license != null)
                {

                    return RedirectToAction("UpdateLicense", new { id = license.Id });
                }
                return View();
           }
            return NotFound();
        }
        //Add license to database
        [HttpPost]
        [Route("License/AddLicense")]
        [ValidateAntiForgeryToken]
        public IActionResult AddLicense(License model)
        {
            try
            {
                string projectIdString = model.Project_id;
                int projectId;
                int.TryParse(projectIdString, out projectId);
                var project = _context.Project.Where( p => p.Id == projectId).FirstOrDefault();
                //get the logged in user id and convert it into integer
                if (User.Identity.IsAuthenticated)
                {
                    // add new record of project
                    var data = new License(model, project.Project_Id);
                    _context.Add(data);
                    _context.SaveChanges();
                    return RedirectToAction("GetPorject","Project");
                }
                return View();
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = "An error occurred while adding project record.";
                return View(TempData);
            }
        }
        //Delete license from database
        [HttpPost]
        public IActionResult DeleteLicense(int id)
        {
            try
            {
                var record = _context.License.Where(r => r.Id == id).FirstOrDefault();
                if (record != null)
                {
                    _context.Remove(record);
                    _context.SaveChanges();
                    return RedirectToAction("GetPorject","Project");
                }
                return NotFound();
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = "An error occurred while deleting license record.";
                return View(TempData);
            }
        }
        //Return view for Update license 
        [HttpGet]
        public IActionResult UpdateLicense(int id) 
        {
            Response.Headers["Cache-Control"] = "no-cache, no-store, must-revalidate";
            Response.Headers["Pragma"] = "no-cache";
            Response.Headers["Expires"] = "-1";
            try
            {
                if (User.Identity.IsAuthenticated)
                {
                    var license = _context.License.Where(l => l.Id == id).FirstOrDefault();
                    if (license != null)
                    {
                    return View(license);
                    }
                return NotFound();
                }
                return NotFound();
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = "An error occurred while retrieving license record.";
                return View(TempData);
            }
        }
        //Update license in database
        [HttpPost]
        public IActionResult UpdateLicense(License model)
        {
            try
            {
                var license = _context.License.Where(l => l.Id == model.Id).FirstOrDefault();
                license.Name = model.Name;
                license.Expiry_Date = model.Expiry_Date;
                _context.SaveChanges();
                return RedirectToAction("GetPorject","Project");
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = "An error occurred while updating license record.";
                return View(TempData);
            }
        }
        //Get the all license records from database
        [HttpGet]
        public async Task<IActionResult> GetLicense()
        {
            Response.Headers["Cache-Control"] = "no-cache, no-store, must-revalidate";
            Response.Headers["Pragma"] = "no-cache";
            Response.Headers["Expires"] = "-1";
            try
            {
                if (User.Identity.IsAuthenticated)
                {
                    var license = await _context.License.ToListAsync();
                    return View(license);
                }
                return NotFound();
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = "An error occurred while retrieving projects.";
                return View(TempData);
            }
        }
    }
}
