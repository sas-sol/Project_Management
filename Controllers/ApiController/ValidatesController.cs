using System;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Project_Management.Data;
using Project_Management.Models.Projects;
using Project = Project_Management.Models.Projects.Project;
using Project_Management.Models.ViewModels;

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
        [HttpGet("GetProjects")]
        public IActionResult GetProjects()
        {
            try
            {
                var projects = _context.Project.ToList();
                var jsonProjects = JsonConvert.SerializeObject(projects);
                return Content(jsonProjects, "application/json");
            }
            catch (Exception ex)
            {
                return Content(ex.Message);
            }
        }
        [HttpGet("GetLicenses")]
        public IActionResult GetLicenses()
        {
            try
            {
                var licenses = _context.License.ToList();
                var jsonLicenses = JsonConvert.SerializeObject(licenses);
                return Content(jsonLicenses, "application/json");
            }
            catch (Exception ex)
            {
                return Content(ex.Message);
            }
        }

        [HttpPost("AddProject")]
        public IActionResult AddProject([FromBody] ProjViewModel model)
        {
            try
            {
                Random rand = new Random();
                int randomNumber = rand.Next(100, 1000); // Generates a random number between 100 and 999
                // add new record of project
                var data = new Project
                {
                    Project_Id = "pro" + randomNumber.ToString(),
                    Project_name = model.Project_name,
                    Description = model.Description,
                    Start_Date = model.Start_Date,
                    User_Id = 1,
                };
                _context.Project.Add(data);
                _context.SaveChanges();
                return Ok();
            }
            catch (Exception ex)
            {
                return Content(ex.Message);
            }
        }

        [HttpDelete("DeleteProject/{id}")]
         public IActionResult DeleteProject(string id)
         {
             try
             {
                 var record = _context.Project.Where(r => r.Project_Id == id).FirstOrDefault();
                 if (record != null)
                 {
                     _context.Remove(record);
                     var license = _context.License.Where(l => l.Project_id == id).FirstOrDefault();
                     if (license != null)
                     {
                         _context.Remove(license);
                     }
                     _context.SaveChanges();
                     return Ok();
                 }
                 return NotFound();
             }
             catch (Exception ex)
             {
                 return Content(ex.Message);
             }
         }

        [HttpDelete("DeleteLicense/{id}")]
        public IActionResult DeleteLicense(string id)
        {
            try
            {
                var record = _context.License.Where(r => r.Project_id == id).FirstOrDefault();
                if (record != null)
                {                        
                    _context.Remove(record);
                    _context.SaveChanges();
                    return Ok();
                }
                return NotFound();
            }
            catch (Exception ex)
            {
                return Content(ex.Message);
            }
        }

    }
}
