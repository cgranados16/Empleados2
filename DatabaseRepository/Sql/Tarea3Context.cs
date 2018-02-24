using System;
using Empleados.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace DatabaseRepository.Sql
{
    public partial class Tarea3Context : DbContext
    {
        public Tarea3Context(DbContextOptions<Tarea3Context> options) : base(options)
        { }

        public DbSet<Correos> Correos { get; set; }
        public DbSet<Empleado> Empleado { get; set; }
        public DbSet<Familiares> Familiares { get; set; }
        public DbSet<HistorialVacaciones> HistorialVacaciones { get; set; }
        public DbSet<PagosRealizados> PagosRealizados { get; set; }
        public DbSet<Permisos> Permisos { get; set; }
        public DbSet<Persona> Persona { get; set; }
        public DbSet<Telefonos> Telefonos { get; set; }

        //public DbSet<View_Empleado> View_Empleado { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Correos>(entity =>
            {
                entity.HasKey(e => new { e.IdPersona, e.Correo });

                entity.Property(e => e.Correo)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdPersonaNavigation)
                    .WithMany(p => p.Correos)
                    .HasForeignKey(d => d.IdPersona)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Correos__IdPerso__66603565");
            });

            modelBuilder.Entity<Empleado>(entity =>
            {
                entity.HasKey(e => e.IdEmpleado);

                entity.Property(e => e.IdEmpleado).ValueGeneratedNever();

                entity.Property(e => e.FechaContratacion).HasColumnType("date");

                entity.Property(e => e.PuestoTrabajo)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Salario).HasColumnType("money");

                entity.HasOne(d => d.Persona)
                    .WithOne(p => p.Empleado)
                    .HasForeignKey<Empleado>(d => d.IdEmpleado)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Empleado__IdEmpl__693CA210");
            });

            modelBuilder.Entity<Familiares>(entity =>
            {
                entity.HasKey(e => new { e.IdEmpleado, e.IdFamiliar, e.Relacion });

                entity.Property(e => e.Relacion)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdEmpleadoNavigation)
                    .WithMany(p => p.Familiares)
                    .HasForeignKey(d => d.IdEmpleado)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Familiare__IdEmp__74AE54BC");

                entity.HasOne(d => d.IdFamiliarNavigation)
                    .WithMany(p => p.Familiares)
                    .HasForeignKey(d => d.IdFamiliar)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Familiare__IdFam__75A278F5");
            });

            modelBuilder.Entity<HistorialVacaciones>(entity =>
            {
                entity.HasKey(e => new { e.IdEmpleado, e.FechaInicio, e.FechaFinal });

                entity.Property(e => e.FechaInicio).HasColumnType("date");

                entity.Property(e => e.FechaFinal).HasColumnType("date");

                entity.HasOne(d => d.IdEmpleadoNavigation)
                    .WithMany(p => p.HistorialVacaciones)
                    .HasForeignKey(d => d.IdEmpleado)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Historial__IdEmp__6C190EBB");
            });

            modelBuilder.Entity<PagosRealizados>(entity =>
            {
                entity.HasKey(e => new { e.IdEmpleado, e.Monto, e.Fecha });

                entity.Property(e => e.Monto).HasColumnType("money");

                entity.Property(e => e.Fecha).HasColumnType("date");

                entity.HasOne(d => d.IdEmpleadoNavigation)
                    .WithMany(p => p.PagosRealizados)
                    .HasForeignKey(d => d.IdEmpleado)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__PagosReal__IdEmp__6EF57B66");
            });

            modelBuilder.Entity<Permisos>(entity =>
            {
                entity.HasKey(e => new { e.IdEmpleado, e.Permiso });

                entity.Property(e => e.Permiso)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdEmpleadoNavigation)
                    .WithMany(p => p.Permisos)
                    .HasForeignKey(d => d.IdEmpleado)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Permisos__IdEmpl__71D1E811");
            });

            modelBuilder.Entity<Persona>(entity =>
            {
                entity.HasKey(e => e.IdPersona);

                entity.Property(e => e.IdPersona).ValueGeneratedNever();

                entity.Property(e => e.Apellido1)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Apellido2)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Direccion)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.EstadoCivil)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.FechaNacimiento).HasColumnType("date");

                entity.Property(e => e.Genero)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Nacionalidad)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Nombre)
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Telefonos>(entity =>
            {
                entity.HasKey(e => new { e.IdPersona, e.Telefono });

                entity.Property(e => e.Telefono).HasColumnType("numeric(18, 0)");

                entity.HasOne(d => d.IdPersonaNavigation)
                    .WithMany(p => p.Telefonos)
                    .HasForeignKey(d => d.IdPersona)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Telefonos__IdPer__6383C8BA");
            });
        }

    }
}
