using System.ComponentModel.DataAnnotations;

namespace Project_Management.Models.Projects
{
    public class ProjectApiUrl
    {
        public int id { get; set; }
        public string Project_Id { get; set; } 
        public string Api_Url { get; set; }
        public string Project_Id_MIS { get; set;} // used to store the projects unique id/ primary key
        
        public ProjectApiUrl(ProjectApiUrl model) {
            Project_Id = model.Project_Id;
            Api_Url = model.Api_Url;
            Project_Id_MIS = model.Project_Id_MIS;
        }
        public ProjectApiUrl()
        {

        }
    }
}
