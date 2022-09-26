using Interface.DTO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class SurroundDbContext : DbContext
    {
        public DbSet<UserDTO> Users { get; set; } = null!;


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=mssqlstud.fhict.local;Database=dbi485050;User Id=dbi485050;Password=Vredeoord123;");
        }
    }
}
