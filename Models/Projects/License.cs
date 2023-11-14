using System.ComponentModel.DataAnnotations;

namespace Project_Management.Models.Projects
{
    public class License
    {
       public int Id { get; set; }
        [Display(Name = "License Name")]
        public string Name { get; set; }
        [Display(Name = "Project Id")]
        public string Project_id { get; set; }
        [Display(Name = "Expiry Date")]
        public DateTime Expiry_Date { get; set; }
        
        public License()
        {}
        public License(License model, string projetId)
        {
            Project_id = projetId;
            Name = model.Name;
            Expiry_Date = model.Expiry_Date;
        }
    }
}
