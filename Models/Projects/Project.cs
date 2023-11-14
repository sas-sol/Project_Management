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

        public Project()
        {

        }
        public Project(Project model, int userid)
        {
            Random rand = new Random();
            int randomNumber = rand.Next(100, 1000);

            Project_Id = "pro" + randomNumber.ToString();
            Project_name = model.Project_name;
            Description = model.Description;
            Start_Date = model.Start_Date;
            User_Id = userid;
        }
    }
    
   
}
