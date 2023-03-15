using DataAccessLayer.Interface;
using DomainLayer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Context
{
    public class ApplicationDbContext:DbContext,IApplicationDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext>options):base(options)
        {

        }

        public DbSet<Register> register { get; set; }
        public DbSet<Tasks> tasks { get; set; }
        public DbSet<AssignTasks> assignTasks { get; set; }
        public DbSet<UserSuccess> userSuccesses { get; set; }
    }
}
