using System.ComponentModel.DataAnnotations;

namespace Project_Management.Models.Projects
{
    public class Project
    {
        public int Id { get; set; }
        [Display(Name = "Project Id")]
        public string Project_Id { get; set; }
        [Display(Name = "Project Name")]
        public string Project_name { get; set; }
        [Display(Name  = "Start Date")]
        public DateTime Start_Date { get; set; }
        public string Description { get; set; }
        public int User_Id { get; set; }
    }
}
