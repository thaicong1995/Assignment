using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assignment.Model;
using Microsoft.EntityFrameworkCore;

namespace Assignment.DBcontext
{
    internal class DBContext : DbContext
    {
        public DbSet<Student> Students { get; set; }
        public DbSet<StudentSubject> StudentSubjects { get; set; }
        public DbSet<Subject> Subjects { get; set; }

        private const string cnString = @"
                Data Source=localhost,1433;
                Initial Catalog=assignment;
                 User ID=sa;
                 Password=123456;TrustServerCertificate=true;";

        public static string CnString => cnString;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer(CnString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<StudentSubject>()
                .HasKey(ss => new { ss.StudentId, ss.SubjectId });

            modelBuilder.Entity<StudentSubject>()
                .HasOne(ss => ss.Student)
                .WithMany(s => s.StudentSubjects)
                .HasForeignKey(ss => ss.StudentId);

            modelBuilder.Entity<StudentSubject>()
                .HasOne(ss => ss.Subject)
                .WithMany(s => s.StudentSubjects)
                .HasForeignKey(ss => ss.SubjectId);
        }

        public void createdb()
        {
            using var dbcontext = new DBContext();
            string dbname = dbcontext.Database.GetDbConnection().Database;

            bool isexisting = dbcontext.Database.ProviderName == "microsoft.entityframeworkcore.sqlserver" &&
                              dbcontext.Database.CanConnect();

            if (!isexisting)
            {
                var db = dbcontext.Database.EnsureCreated();
                if (db)
                {
                    Console.WriteLine($"create {dbname} success!!");
                }
                else
                {
                    Console.WriteLine($"can't create {dbname} ");
                }
            }
            else
            {
                Console.WriteLine($"database '{dbname}' already exists.");
            }
        }
    }
}
