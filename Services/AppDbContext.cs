using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SignPC.Models;
using SignPC.Entities;

namespace SignPC.Services
{
    public class AppDbContext:DbContext
    {
        public virtual DbSet<Membro> Membro { get;set;}
        public virtual DbSet<Equipa> Equipa { get;set;}
        public virtual DbSet<Equipamento> Equipamento { get;set;}
        public virtual DbSet<UsuarioConta> UsuarioContas { get;set;}
        
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }      
    }
}