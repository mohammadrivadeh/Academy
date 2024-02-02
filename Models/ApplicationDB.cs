using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademyEFCore_NET7.Models
{
    public class ApplicationDB:DbContext
    {
        public static ApplicationDB Current { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=AcademyEFCore;Integrated Security=True");
            base.OnConfiguring(optionsBuilder);
        }
        public DbSet<Student> Students { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Register> Registers { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Presentation> Presentations { get; set; }
        public DbSet<Address> Addresses { get; set; }
    }
}
