using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TempusDigitalMVC.Models
{
    public class ContextoCadastro : DbContext
    {
        public ContextoCadastro(DbContextOptions<ContextoCadastro> options) : base(options) { }

        public DbSet<CadastroCliente> CadastroCliente { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CadastroCliente>(entity =>
            {
                entity.Property(x => x.Nome)
                    .IsRequired()
                    .HasMaxLength(150)
                    .IsUnicode(false);
                entity.Property(x => x.CPF)
                    .IsRequired()
                    .HasMaxLength(11)
                    .IsUnicode(false);
                entity.Property(x => x.DataNascimento)
                    .IsRequired();
                entity.Property(x => x.DataCadastro)
                    .HasColumnType("date")
                    .HasDefaultValueSql("getdate()");
                entity.Property(x => x.RendaFamiliar);
                    
            });
        }
    }
}
