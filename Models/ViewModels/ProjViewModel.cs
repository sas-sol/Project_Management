using System.ComponentModel.DataAnnotations;

namespace Project_Management.Models.ViewModels
{
    public class ProjViewModel
    {
        public string Project_name { get; set; }
        public DateTime Start_Date { get; set; }
        public string Description { get; set; }
    }
}
