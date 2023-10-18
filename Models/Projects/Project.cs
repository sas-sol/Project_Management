namespace Project_Management.Models.Projects
{
    public class Project
    {
        public int Id { get; set; }
        public string Project_Id { get; set; }
        public string Project_name { get; set; }
        public DateTime Start_Date { get; set; }
        public string Description { get; set; }
        public int User_Id { get; set; }
    }
}
