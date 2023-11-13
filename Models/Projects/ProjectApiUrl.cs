﻿namespace Project_Management.Models.Projects
{
    public class ProjectApiUrl
    {
        public int id { get; set; }
        public string Project_Id { get; set; } 
        public string Api_Url { get; set; }
        public int Unique { get; set;} // used to store the projects unique id/ primary key
    }
}
