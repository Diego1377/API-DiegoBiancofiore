using Flixdi.Entities;
using Flixdi.Entities.MicrosoftIdentity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flixdi.DataAccess
{
    public class DbDataAccess : IdentityDbContext<User, Role, Guid, UserClaim, UserRole, UserLogin, RoleClaim, UserToken>
    {
        public virtual DbSet<Pelicula> Peliculas { get; set; }
        public virtual DbSet<Actor> Actores { get; set; }
        public virtual DbSet<EstudioCinematografico> EstudioCinematografico { get; set; }
        public virtual DbSet<Genero> Generos { get; set; }
        public virtual DbSet<Director> Directores { get; set; }
        public virtual DbSet<Pais> Paises { get; set; }
        public virtual DbSet<PeliculaPorActor> PeliculasPorActores { get; set; }
        public virtual DbSet<PeliculaPorDirector> PeliculasPorDirectores { get; set; }
        public virtual DbSet<PeliculaPorGenero> PeliculasPorGeneros { get; set; }
        public DbDataAccess(DbContextOptions<DbDataAccess> options) : base(options) { }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) => optionsBuilder.LogTo(Console.WriteLine).EnableDetailedErrors();
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
        }
    }
}
