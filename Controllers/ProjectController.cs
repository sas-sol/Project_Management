using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project_Management.Data;
using Project_Management.Models.Projects;
using System.Diagnostics.Metrics;
using System.Security.Claims;

namespace Project_Management.Controllers
{
    [Authorize]
    public class ProjectController : Controller
    {
        private readonly ProjectContext _context;

        public ProjectController(ProjectContext context)
        {
            _context = context;
        }
        //Return view for Add License
        [HttpGet]
        public IActionResult AddPorject()
        {
            return View("AddProject");
        }
        //Add license to database
        [HttpPost]
        public IActionResult AddPorject(Project model)
        {
            try
            {
                //get the logged in user id and convert it into integer
                string userId = null;
                if (User.Identity.IsAuthenticated)
                {

                    userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                }
                int userId1;
                int.TryParse(userId, out userId1);
                // call the random function and store the random value in variable
                Random rand = new Random();
                int randomNumber = rand.Next(100, 1000); // Generates a random number between 100 and 999
                // add new record of project
                var data = new Project
                {
                    Project_Id = "pro" + randomNumber.ToString(),
                    Project_name = model.Project_name,
                    Description = model.Description,
                    Start_Date = model.Start_Date,
                    User_Id = userId1,
                };
                _context.Add(data);
                _context.SaveChanges();
                return RedirectToAction("GetPorject");
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = "An error occurred while adding project record.";
                return View(TempData);
            }
        }
        //Delete license from database
        [HttpPost]
        public IActionResult DeletePorject(int id)
        {
            try
            {
                var record = _context.Project.Where( r => r.Id == id ).FirstOrDefault();
                if (record != null)
                {
                    _context.Remove(record);
                    _context.SaveChanges();
                    return RedirectToAction("GetPorject");
                }
                return NotFound();
            }
           catch (Exception ex)
            {
                TempData["errorMessage"] = "An error occurred while deleting project record.";
                return View(TempData);
            }
        }
        //Return view for Update license 
        [HttpGet]
        public IActionResult UpdatePorject(int id)
        {
            try
            {
                var project = _context.Project.Where(p => p.Id == id).FirstOrDefault();
                if (project != null)
                {
                    return View(project);
                }
                return NotFound();
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = "An error occurred while retrieving project record.";
                return View(TempData);
            }
        }
        //Update license in database
        [HttpPost]
        public IActionResult UpdatePorject(Project model)
        {
            try
            {
            var project = _context.Project.Where(p => p.Id == model.Id).FirstOrDefault();
                project.Project_name = model.Project_name;
                project.Description = model.Description;
                project.Start_Date = model.Start_Date;
                _context.SaveChanges();
                return RedirectToAction("GetPorject");
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = "An error occurred while updating project record.";
                return View(TempData);
            }
            
        }
        //Get the all license records from database
        [HttpGet]
        public async Task<IActionResult> GetPorject()
        {
            try
            {
                string userId = null;
                if (User.Identity.IsAuthenticated)
                {

                    userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                }
                int userId1;
                int.TryParse(userId, out userId1);
                var projects = await _context.Project.Where(tm => tm.User_Id == userId1).ToListAsync();
               
                return View(projects);
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = "An error occurred while retrieving projects.";
                return View(TempData);
            }
        }
    }
}
