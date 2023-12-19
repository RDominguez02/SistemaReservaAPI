using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using SistemaReservaAPI.Models;

namespace SistemaReservaAPI.Server.Models;

public partial class SistemaReservaCitaContext : DbContext
{
    public SistemaReservaCitaContext()
    {
    }

    public SistemaReservaCitaContext(DbContextOptions<SistemaReservaCitaContext> options)
        : base(options)
    {
    }

    public virtual DbSet<LavadorServicio> Lavador_Servicio { get; set; }

    public virtual DbSet<EstacionServicio> Estacion_Servicio { get; set; }

    public virtual DbSet<Citum> Cita { get; set; }

    public virtual DbSet<DetalleCitum> DetalleCita { get; set; }

    public virtual DbSet<DetalleFactorRiesgo> DetalleFactorRiesgos { get; set; }

    public virtual DbSet<Estacion> Estacions { get; set; }

    public virtual DbSet<EstacionStatus> EstacionStatuses { get; set; }

    public virtual DbSet<FactorRiesgo> FactorRiesgos { get; set; }

    public virtual DbSet<Horario> Horarios { get; set; }

    public virtual DbSet<Lavador> Lavadors { get; set; }

    public virtual DbSet<LavadorStatus> LavadorStatuses { get; set; }

    public virtual DbSet<Rol> Rols { get; set; }

    public virtual DbSet<Servicio> Servicios { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=SQL5112.site4now.net;Initial Catalog=db_aa1a9f_sistemareservacita;User Id=db_aa1a9f_sistemareservacita_admin;Password=fmdm03997161");
    //=> optionsBuilder.UseSqlServer("Data Source=localhost\\SQLEXPRESS01;Initial Catalog=SistemaReservaCita;Integrated Security=True;MultipleActiveResultSets=True");
    //STORED PROCEDURES
    public async Task<string> CrearCitaRapidaAsync(int idParametroNumerico, string parametroNvarchar)
    {
        var idParam = new SqlParameter("@IdParametroNumerico", idParametroNumerico);
        var nvarcharParam = new SqlParameter("@ParametroNvarchar", parametroNvarchar);
        var mensajeParam = new SqlParameter
        {
            ParameterName = "@Mensaje",
            SqlDbType = SqlDbType.NVarChar,
            Size = 4000, // Ajusta el tamaño según sea necesario
            Direction = ParameterDirection.Output
        };

        await Database.ExecuteSqlRawAsync("EXEC CrearCitaRapida @IdParametroNumerico, @ParametroNvarchar, @Mensaje OUTPUT",
            idParam, nvarcharParam, mensajeParam);

        return mensajeParam.Value as string;
    }

    public async Task CrearAgendatuDiaAsync(int idParametroNumerico, string parametroNvarchar, DateTime parametroDatetime)
    {
        var idParam = new SqlParameter("@IdParametroNumerico", idParametroNumerico);
        var nvarcharParam = new SqlParameter("@ParametroNvarchar", parametroNvarchar);
        var datetimeParam = new SqlParameter("@ParametroDatetime", parametroDatetime);

        await Database.ExecuteSqlRawAsync("EXEC Agendatudia @IdParametroNumerico, @ParametroNvarchar, @ParametroDatetime", idParam, nvarcharParam, datetimeParam);
    }

    public async Task EliminarCitaYDetalleAsync(int idCita)
    {
        var IdCita = new SqlParameter("@IdCita", idCita);

        await Database.ExecuteSqlRawAsync("EXEC EliminarCitaYDetalles @IdCita", IdCita);
    }

    public async Task IniciarSesion(string UserP, string PassP)
    {
        var User = new SqlParameter("@User", UserP);
        var Pass = new SqlParameter("@Pass", PassP);

        await Database.ExecuteSqlRawAsync("EXEC IniciarSesion @User, @Pass", User, Pass);
    }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Citum>(entity =>
        {
            entity.HasKey(e => e.IdCitaCit).HasName("PK__cita__E4953C2BA4464F81");

            entity.ToTable("cita");

            entity.Property(e => e.IdCitaCit).HasColumnName("idCita_cit");
            entity.Property(e => e.DuracionCit).HasColumnName("Duracion_cit");
            entity.Property(e => e.EstadoCit).HasColumnName("Estado_cit");
            entity.Property(e => e.FechaCit)
                .HasColumnType("datetime")
                .HasColumnName("Fecha_cit");
            entity.Property(e => e.FechaFinCit)
                .HasColumnType("datetime")
                .HasColumnName("FechaFin_cit");
            entity.Property(e => e.IdEstacionCit).HasColumnName("idEstacion_cit");
            entity.Property(e => e.IdLavadorCit).HasColumnName("idLavador_cit");
            entity.Property(e => e.IdUsuarioCit).HasColumnName("idUsuario_cit");

            entity.HasOne(d => d.IdEstacionCitNavigation).WithMany(p => p.Cita)
                .HasForeignKey(d => d.IdEstacionCit)
                .HasConstraintName("FK__cita__idEstacion__02084FDA");

            entity.HasOne(d => d.IdLavadorCitNavigation).WithMany(p => p.Cita)
                .HasForeignKey(d => d.IdLavadorCit)
                .HasConstraintName("FK_cita_lavador");

            entity.HasOne(d => d.IdUsuarioCitNavigation).WithMany(p => p.Cita)
                .HasForeignKey(d => d.IdUsuarioCit)
                .HasConstraintName("FK_cita_usuario");
        });

        modelBuilder.Entity<DetalleCitum>(entity =>
        {
            entity.HasKey(e => new { e.IdDetalleCitaDct, e.IdCitaDct }).HasName("PK__detalleC__D539A0CA522B3508");

            entity.ToTable("detalleCita");

            entity.Property(e => e.IdDetalleCitaDct)
                .ValueGeneratedOnAdd()
                .HasColumnName("idDetalleCita_dct");
            entity.Property(e => e.IdCitaDct).HasColumnName("idCita_dct");
            entity.Property(e => e.DuracionDct).HasColumnName("Duracion_dct");
            entity.Property(e => e.IdServicioDct).HasColumnName("idServicio_dct");

            entity.HasOne(d => d.IdCitaDctNavigation).WithMany(p => p.DetalleCita)
                .HasForeignKey(d => d.IdCitaDct)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__detalleCi__idCit__02FC7413");

            entity.HasOne(d => d.IdServicioDctNavigation).WithMany(p => p.DetalleCita)
                .HasForeignKey(d => d.IdServicioDct)
                .HasConstraintName("FK__detalleCi__idSer__03F0984C");
        });

        modelBuilder.Entity<DetalleFactorRiesgo>(entity =>
        {
            entity.HasKey(e => e.IdDetalleFactorRiesgoDetfrg).HasName("PK__detalleF__D1BCB19060C83239");

            entity.ToTable("detalleFactorRiesgo");

            entity.Property(e => e.IdDetalleFactorRiesgoDetfrg).HasColumnName("idDetalleFactorRiesgo_detfrg");
            entity.Property(e => e.DescripcionDetfrg)
                .HasMaxLength(255)
                .HasColumnName("Descripcion_detfrg");
            entity.Property(e => e.DuracionDetfrg).HasColumnName("Duracion_detfrg");
            entity.Property(e => e.IdFactorRiesgoDetfrg).HasColumnName("idFactorRiesgo_detfrg");

            entity.HasOne(d => d.IdFactorRiesgoDetfrgNavigation).WithMany(p => p.DetalleFactorRiesgos)
                .HasForeignKey(d => d.IdFactorRiesgoDetfrg)
                .HasConstraintName("FK_detalleFactorRiesgo_factorRiesgo");
        });

        modelBuilder.Entity<Estacion>(entity =>
        {
            entity.HasKey(e => e.IdEstacionEst).HasName("PK__estacion__0FE3925F3726937F");

            entity.ToTable("estacion");

            entity.Property(e => e.IdEstacionEst).HasColumnName("idEstacion_est");
            entity.Property(e => e.DescripcionEst)
                .HasMaxLength(255)
                .HasColumnName("Descripcion_est");
            entity.Property(e => e.EsActivo).HasColumnName("esActivo");

            entity.HasMany(d => d.IdServicioEstsers).WithMany(p => p.IdEstacionEstsers)
                .UsingEntity<Dictionary<string, object>>(
                    "EstacionServicio",
                    r => r.HasOne<Servicio>().WithMany()
                        .HasForeignKey("IdServicioEstser")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__estacion___idSer__7E37BEF6"),
                    l => l.HasOne<Estacion>().WithMany()
                        .HasForeignKey("IdEstacionEstser")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__estacion___idEst__7D439ABD"),
                    j =>
                    {
                        j.HasKey("IdEstacionEstser", "IdServicioEstser").HasName("PK__estacion__C1A10CDF97207C7D");
                        j.ToTable("estacion_servicio");
                        j.IndexerProperty<int>("IdEstacionEstser").HasColumnName("idEstacion_estser");
                        j.IndexerProperty<int>("IdServicioEstser").HasColumnName("idServicio_estser");
                    });
        });

        modelBuilder.Entity<EstacionStatus>(entity =>
        {
            entity.HasKey(e => e.IdEstacionStatusEststa);

            entity.ToTable("estacion_status");

            entity.Property(e => e.IdEstacionStatusEststa).HasColumnName("idEstacionStatus_eststa");
            entity.Property(e => e.FechaFinEststa)
                .HasColumnType("datetime")
                .HasColumnName("FechaFin_eststa");
            entity.Property(e => e.FechaInicioEststa)
                .HasColumnType("datetime")
                .HasColumnName("FechaInicio_eststa");
            entity.Property(e => e.IdEsacionEststa).HasColumnName("idEsacion_eststa");
            entity.Property(e => e.StatusEststa).HasColumnName("Status_eststa");

            entity.HasOne(d => d.IdEsacionEststaNavigation).WithMany(p => p.EstacionStatuses)
                .HasForeignKey(d => d.IdEsacionEststa)
                .HasConstraintName("FK_estacion_status_estacion");
        });

        modelBuilder.Entity<FactorRiesgo>(entity =>
        {
            entity.HasKey(e => e.IdFactorRiesgoFrg).HasName("PK__factorRi__DAA8201009AF56EC");

            entity.ToTable("factorRiesgo");

            entity.Property(e => e.IdFactorRiesgoFrg).HasColumnName("idFactorRiesgo_frg");
            entity.Property(e => e.DescripcionFrg)
                .HasMaxLength(250)
                .HasColumnName("Descripcion_frg");
            entity.Property(e => e.DuracionTotalFrg).HasColumnName("duracionTotal_frg");
        });

        modelBuilder.Entity<Horario>(entity =>
        {
            entity.HasKey(e => e.IdHorarioHor);

            entity.ToTable("Horario");

            entity.Property(e => e.IdHorarioHor).HasColumnName("idHorario_hor");
            entity.Property(e => e.EsActivo).HasColumnName("esActivo");
            entity.Property(e => e.HoraFinalHor).HasColumnName("horaFinal_hor");
            entity.Property(e => e.HoraInicioHor).HasColumnName("horaInicio_hor");
            entity.Property(e => e.IdLavadorHor).HasColumnName("idLavador_hor");

            entity.HasOne(d => d.IdLavadorHorNavigation).WithMany(p => p.Horarios)
                .HasForeignKey(d => d.IdLavadorHor)
                .HasConstraintName("FK_Horario_lavador");
        });

        modelBuilder.Entity<Lavador>(entity =>
        {
            entity.HasKey(e => e.IdLavadorLav).HasName("PK__lavador__CEF33DD9D676398D");

            entity.ToTable("lavador");

            entity.Property(e => e.IdLavadorLav).HasColumnName("idLavador_lav");
            entity.Property(e => e.EsActivo).HasColumnName("esActivo");
            entity.Property(e => e.NombreLav)
                .HasMaxLength(150)
                .HasColumnName("Nombre_lav");

            entity.HasMany(d => d.IdServicioLavsers).WithMany(p => p.IdLavadorLavsers)
                .UsingEntity<Dictionary<string, object>>(
                    "LavadorServicio",
                    r => r.HasOne<Servicio>().WithMany()
                        .HasForeignKey("IdServicioLavser")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__lavador_s__idSer__7A672E12"),
                    l => l.HasOne<Lavador>().WithMany()
                        .HasForeignKey("IdLavadorLavser")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__lavador_s__idLav__797309D9"),
                    j =>
                    {
                        j.HasKey("IdLavadorLavser", "IdServicioLavser").HasName("PK__lavador___0A324A09856637C9");
                        j.ToTable("lavador_servicio");
                        j.IndexerProperty<int>("IdLavadorLavser").HasColumnName("idLavador_lavser");
                        j.IndexerProperty<int>("IdServicioLavser").HasColumnName("idServicio_lavser");
                    });
        });

        modelBuilder.Entity<LavadorServicio>()
        .HasKey(ls => new { ls.IdLavadorLavser, ls.IdServicioLavser });

        modelBuilder.Entity<LavadorServicio>()
       .Property(ls => ls.IdLavadorLavser)
       .HasColumnName("idLavador_lavser");

        modelBuilder.Entity<LavadorServicio>()
        .Property(ls => ls.IdServicioLavser)
        .HasColumnName("idServicio_lavser");



        modelBuilder.Entity<EstacionServicio>()
        .HasKey(es => new { es.IdEstacionEstser, es.IdServicioEstser });

        modelBuilder.Entity<EstacionServicio>()
       .Property(es => es.IdEstacionEstser)
       .HasColumnName("idEstacion_estser");

        modelBuilder.Entity<EstacionServicio>()
            .Property(es => es.IdServicioEstser)
            .HasColumnName("idServicio_estser");

        modelBuilder.Entity<LavadorStatus>(entity =>
        {
            entity.HasKey(e => e.IdLavadorStatusLavsta);

            entity.ToTable("lavador_status");

            entity.Property(e => e.IdLavadorStatusLavsta).HasColumnName("idLavadorStatus_lavsta");
            entity.Property(e => e.FechaFinLavsta)
                .HasColumnType("datetime")
                .HasColumnName("FechaFin_lavsta");
            entity.Property(e => e.FechaInicioLavsta)
                .HasColumnType("datetime")
                .HasColumnName("FechaInicio_lavsta");
            entity.Property(e => e.IdLavadorLavsta).HasColumnName("idLavador_lavsta");
            entity.Property(e => e.StatusLavsta).HasColumnName("Status_lavsta");

            entity.HasOne(d => d.IdLavadorLavstaNavigation).WithMany(p => p.LavadorStatuses)
                .HasForeignKey(d => d.IdLavadorLavsta)
                .HasConstraintName("FK_lavador_status_lavador");
        });

        modelBuilder.Entity<Rol>(entity =>
        {
            entity.HasKey(e => e.IdRolRol).HasName("PK__rol__0CBC8A3F0971306A");

            entity.ToTable("rol");

            entity.Property(e => e.IdRolRol).HasColumnName("idRol_rol");
            entity.Property(e => e.DescripcionRol)
                .HasMaxLength(255)
                .HasColumnName("Descripcion_rol");
        });

        modelBuilder.Entity<Servicio>(entity =>
        {
            entity.HasKey(e => e.IdServicioSer).HasName("PK__servicio__418613E234088108");

            entity.ToTable("servicio");

            entity.Property(e => e.IdServicioSer).HasColumnName("idServicio_ser");
            entity.Property(e => e.CostoSer)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("Costo_ser");
            entity.Property(e => e.DescripcionSer)
                .HasMaxLength(255)
                .HasColumnName("Descripcion_ser");
            entity.Property(e => e.DuracionSer).HasColumnName("Duracion_ser");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.IdUsuarioUsu).HasName("PK__usuario__158C939C9D504DF4");

            entity.ToTable("usuario");

            entity.Property(e => e.IdUsuarioUsu).HasColumnName("idUsuario_usu");
            entity.Property(e => e.ClaveUsu)
                .HasMaxLength(255)
                .HasColumnName("Clave_usu");
            entity.Property(e => e.IdRolUsu).HasColumnName("idRol_usu");
            entity.Property(e => e.NombreUsu)
                .HasMaxLength(150)
                .HasColumnName("Nombre_usu");
            entity.Property(e => e.NombreUsuarioUsu)
                .HasMaxLength(255)
                .HasColumnName("nombreUsuario_usu");

            entity.HasOne(d => d.IdRolUsuNavigation).WithMany(p => p.Usuarios)
                .HasForeignKey(d => d.IdRolUsu)
                .HasConstraintName("FK__usuario__idRol_u__75A278F5");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
