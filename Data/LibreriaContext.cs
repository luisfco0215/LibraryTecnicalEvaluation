using LibraryTecnicalEvaluation.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryTecnicalEvaluation.Data
{
    public class LibreriaContext(DbContextOptions<LibreriaContext> options) : DbContext(options)
    {
        #region DBSets
        public DbSet<Autores> Autores { get; set; }
        public DbSet<Libros> Libros { get; set; }
        public DbSet<Prestamos> Prestamos { get; set; }
        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Autores>(entity =>
            {
                entity.ToTable("Autores");
                entity.HasKey(a => a.Autor_Id);
                entity.Property(a => a.Nombre).IsRequired();
                entity.Property(a => a.Nacionalidad).IsRequired();
            });

            modelBuilder.Entity<Libros>(entity =>
            {
                entity.ToTable("Libros");
                entity.HasKey(l => l.Libro_Id);
                entity.Property(l => l.Titulo).IsRequired();
                entity.Property(l => l.Año_publicacion).IsRequired();

                entity.HasOne(l => l.Autores)
                      .WithMany(a => a.Libros)
                      .HasForeignKey(l => l.Autor_Id)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Prestamos>(entity =>
            {
                entity.ToTable("Prestamos");
                entity.HasKey(p => p.Prestamo_Id);
                entity.Property(p => p.Fecha_Prestamo).IsRequired();

                entity.HasOne(p => p.Libros)
                      .WithMany(l => l.Prestamos)
                      .HasForeignKey(p => p.Libro_Id)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            // Autores
            modelBuilder.Entity<Autores>().HasData(
                new Autores { Autor_Id = 1, Nombre = "Gabriel García Márquez", Nacionalidad = "Colombiano" },
                new Autores { Autor_Id = 2, Nombre = "Isabel Allende", Nacionalidad = "Chilena" },
                new Autores { Autor_Id = 3, Nombre = "J.K. Rowling", Nacionalidad = "Británica" }
            );

            // Libros
            modelBuilder.Entity<Libros>().HasData(
                new Libros { Libro_Id = 1, Titulo = "Cien Años de Soledad", Autor_Id = 1, Año_publicacion = 1967, Genero = "Realismo Mágico" },
                new Libros { Libro_Id = 2, Titulo = "La Casa de los Espíritus", Autor_Id = 2, Año_publicacion = 1982, Genero = "Novela" },
                new Libros { Libro_Id = 3, Titulo = "Harry Potter y la Piedra Filosofal", Autor_Id = 3, Año_publicacion = 1997, Genero = "Fantasía" }
            );

            // Prestamos
            modelBuilder.Entity<Prestamos>().HasData(
                new Prestamos { Prestamo_Id = 1, Libro_Id = 1, Fecha_Prestamo = new DateTime(2025, 10, 1), Fecha_Devolucion = null, UsuarioId = "user1" },
                new Prestamos { Prestamo_Id = 2, Libro_Id = 2, Fecha_Prestamo = new DateTime(2025, 10, 5), Fecha_Devolucion = new DateTime(2025, 10, 10), UsuarioId = "user2" }
            );
        }
    }

}