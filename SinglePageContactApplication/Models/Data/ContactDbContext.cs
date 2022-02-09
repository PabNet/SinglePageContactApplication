using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using SinglePageContactApplication.Models.Entities;

namespace SinglePageContactApplication.Models.Data
{
    public class ContactDbContext : DbContext
    {
        public DbSet<Employee> Employees { get; set; } = null!;
        public DbSet<JobTitle> JobTitles { get; set; } = null!;
        
        public ContactDbContext(DbContextOptions<ContactDbContext> options)
            : base(options)
        {
            base.Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            JobTitle developer = new JobTitle() {Id = 1, Position = "Developer"},
                designer = new JobTitle() {Id = 2, Position = "Designer"},
                businessAnalyst = new JobTitle() { Id = 3, Position = "Business Analyst"},
                recruiter = new JobTitle() {Id = 4, Position = "Recruiter"},
                projectManager = new JobTitle() { Id = 5, Position = "Project Manager"},
                marketer = new JobTitle() { Id = 6, Position = "Marketer"},
                tester = new JobTitle() {Id = 7, Position = "Tester"};
                
            
            modelBuilder.Entity<JobTitle>().HasData(
                new List<JobTitle>()
                {
                    developer,designer,businessAnalyst,recruiter,projectManager,marketer,tester
                }
            );

            modelBuilder.Entity<Employee>().HasData(
                new List<Employee>()
                {
                    new()
                    {
                        Id = 1,
                        Name = "Ekaterina Maninets",
                        PhoneNumber = "292857295",
                        BirthDate = new DateTime(2001, 5, 10),
                        JobTitleId = designer.Id
                    },
                    new()
                    {
                        Id = 2,
                        Name = "Petr Fedorchuk",
                        PhoneNumber = "253856791",
                        BirthDate = new DateTime(1992, 10, 2),
                        JobTitleId = developer.Id
                    },
                    new()
                    {
                        Id = 3,
                        Name = "Margarita Bashalova",
                        PhoneNumber = "331290237",
                        BirthDate = new DateTime(1999, 1, 7),
                        JobTitleId = recruiter.Id
                    },
                    new()
                    {
                        Id = 4,
                        Name = "Nikolai Gorbets",
                        PhoneNumber = "297781293",
                        BirthDate = new DateTime(1996, 11, 11),
                        JobTitleId = tester.Id
                    },
                }
            );
            
        }

        private void DeleteDataBase()
        {
            base.Database.EnsureDeleted();
        }
        
    }
}