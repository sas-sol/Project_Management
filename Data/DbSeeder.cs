using Project_Management.Models.Account;

namespace Project_Management.Data
{
    public class DbSeeder
    {
        public static void SeedData(ProjectContext context)
        {
            if(context == null) {
                string hashedPasswords = BCrypt.Net.BCrypt.HashPassword("noman@321");
                var teamlead = new Users
                {
                    Username = "noman321",
                    Password = hashedPasswords,
                    Role = "Admin",
                    Email = "noman321@gmail.com",
                    // Set other user properties as needed
                };
                // Add the user to the Users table
                context.Users.Add(teamlead);
                context.SaveChanges();
            }

        }
    }
}
