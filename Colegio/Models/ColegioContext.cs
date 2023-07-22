using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Colegio.Models;

public partial class ColegioContext : DbContext
{
    private readonly IConfiguration _configuration;
    public ColegioContext(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public virtual DbSet<CursoTable> CursoTables { get; set; }

    public virtual DbSet<EstudianteTable> EstudianteTables { get; set; }

    public virtual DbSet<EstudianteVistum> EstudianteVista { get; set; }

    public virtual DbSet<ProfesorTable> ProfesorTables { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer(_configuration["ConnectionSQL"]);


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CursoTable>(entity =>
        {
            entity.HasKey(e => e.IdCurso);

            entity.ToTable("cursoTable");

            entity.Property(e => e.IdCurso).HasColumnName("idCurso");
            entity.Property(e => e.CreatedRowAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("CreatedRowAT");
            entity.Property(e => e.NombreCurso)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nombreCurso");
        });

        modelBuilder.Entity<EstudianteTable>(entity =>
        {
            entity.HasKey(e => e.IdEstudiante);

            entity.ToTable("estudianteTable");

            entity.Property(e => e.IdEstudiante).HasColumnName("idEstudiante");
            entity.Property(e => e.ApellidoEstudiante)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("apellidoEstudiante");
            entity.Property(e => e.EdadEstudiante)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("edadEstudiante");
            entity.Property(e => e.IdCurso).HasColumnName("idCurso");
            entity.Property(e => e.IdProfesor).HasColumnName("idProfesor");
            entity.Property(e => e.NombreEstudiante)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("nombreEstudiante");
            entity.Property(e => e.RowCreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.IdCursoNavigation).WithMany(p => p.EstudianteTables)
                .HasForeignKey(d => d.IdCurso)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_estudianteTable_cursoTable");

            entity.HasOne(d => d.IdProfesorNavigation).WithMany(p => p.EstudianteTables)
                .HasForeignKey(d => d.IdProfesor)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_estudianteTable_profesorTable");
        });

        modelBuilder.Entity<EstudianteVistum>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("estudianteVista");

            entity.Property(e => e.EdadEstudiante)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("edadEstudiante");
            entity.Property(e => e.EstudianteNombres)
                .HasMaxLength(201)
                .IsUnicode(false)
                .HasColumnName("estudianteNombres");
            entity.Property(e => e.IdEstudiante).HasColumnName("idEstudiante");
            entity.Property(e => e.NombreCurso)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nombreCurso");
            entity.Property(e => e.NombresProfesor)
                .HasMaxLength(201)
                .IsUnicode(false)
                .HasColumnName("nombresProfesor");
        });

        modelBuilder.Entity<ProfesorTable>(entity =>
        {
            entity.HasKey(e => e.IdProfesor);

            entity.ToTable("profesorTable");

            entity.Property(e => e.IdProfesor).HasColumnName("idProfesor");
            entity.Property(e => e.ApellidoProfesor)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("apellidoProfesor");
            entity.Property(e => e.EdadProfesor)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("edadProfesor");
            entity.Property(e => e.IdCurso).HasColumnName("idCurso");
            entity.Property(e => e.NombreProfesor)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("nombreProfesor");
            entity.Property(e => e.RowCreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.IdCursoNavigation).WithMany(p => p.ProfesorTables)
                .HasForeignKey(d => d.IdCurso)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_profesorTable_cursoTable");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
