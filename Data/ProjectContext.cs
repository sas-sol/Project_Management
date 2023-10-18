using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Project_Management.Models.Account;
using Project_Management.Models.Projects;
namespace Project_Management.Data
{
    public class ProjectContext : DbContext
    {
        public ProjectContext(DbContextOptions<ProjectContext> options)
          : base(options)
        {
        }
        public DbSet<Project_Management.Models.Account.Users> Users { get; set; } = default!;
        public DbSet<Project_Management.Models.Projects.Project> Project { get; set; } = default!;
        public DbSet<Project_Management.Models.Projects.License> License { get; set; } = default!;
    }
}

