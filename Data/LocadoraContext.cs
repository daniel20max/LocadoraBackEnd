using LocadoraDeFilmes.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LocadoraDeFilmes.Data
{
    public class LocadoraContext : IdentityDbContext
    {
        public LocadoraContext(DbContextOptions<LocadoraContext> options) : base(options) { }
        public DbSet<Filme> filmes { get; set; }
        public DbSet<Locacao> locacao { get; set; }
    }
}
