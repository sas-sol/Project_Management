namespace Project_Management.Models.Projects
{
    public class License
    {
       public int Id { get; set; }
        public string Name { get; set; }
        public string Project_id { get; set; }
        public DateTime Expiry_Date { get; set; }
    }
}
