

namespace CoreDemoSolution.Data
{
    using System;
    using CoreDemoSolution.Data.Classes;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;

    /// <summary>
    /// Database DBContext
    /// </summary>
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
           : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(GetConnectionString());
        }

        private static string GetConnectionString()
        {
            const string databaseName = "CoreAppDB";
            const string databaseUser = "sa";
            const string databasePass = "admin123!@#";
            const string server = @"c229\sqlexpress2014";

            var connectionString = @"Data Source=" + server + ";Initial Catalog=" + databaseName + ";Integrated Security=True ;user id=" + databaseUser + ";password=" + databasePass;
            return connectionString;
        }

    }
}
