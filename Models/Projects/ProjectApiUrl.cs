using System.ComponentModel.DataAnnotations;

namespace Project_Management.Models.Projects
{
    public class ProjectApiUrl
    {
        public int id { get; set; }
        public string Project_Id { get; set; } 
        public string Api_Url { get; set; }
        public int Project_Id_MIS { get; set;} // used to store the projects unique id/ primary key
        
    }
}
