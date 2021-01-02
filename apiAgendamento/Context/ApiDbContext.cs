using System;
using apiAgendamento.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace apiAgendamento.Context
{
    public partial class ApiDbContext : DbContext
    {
        public ApiDbContext()
        {
        }

        public ApiDbContext(DbContextOptions<ApiDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Agendamento> Agendamentos { get; set; }
        public virtual DbSet<Fornecedor> Fornecedores { get; set; }
        public virtual DbSet<SituacaoAgendamento> SituacaoAgendamentos { get; set; }
        public virtual DbSet<Veiculo> Veiculos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Agendamento>(entity =>
            {
                entity.HasKey(e => e.IdAgendamento)
                    .HasName("PK_AGENDAMENTOS");

                entity.ToTable("AGENDAMENTOS");

                entity.Property(e => e.DhAtualizacao).HasColumnType("datetime");

                entity.Property(e => e.DhCriacao)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.DhFinal).HasColumnType("datetime");

                entity.Property(e => e.DhInicial).HasColumnType("datetime");

                entity.Property(e => e.IdSituacaoAgendamento).HasDefaultValueSql("((0))");

                entity.Property(e => e.Obs).IsUnicode(false);

                entity.HasOne(d => d.IdSituacaoAgendamentoNavigation)
                    .WithMany(p => p.Agendamentos)
                    .HasForeignKey(d => d.IdSituacaoAgendamento)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AGENDAMENTOS01");

                entity.HasOne(d => d.IdVeiculoNavigation)
                    .WithMany(p => p.Agendamentos)
                    .HasForeignKey(d => d.IdVeiculo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AGENDAMENTOS02");
            });

            modelBuilder.Entity<Fornecedor>(entity =>
            {
                entity.HasKey(e => e.IdFornecedor)
                    .HasName("PK_FORNECEDOR");

                entity.ToTable("FORNECEDORES");

                entity.Property(e => e.NmFornecedor)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<SituacaoAgendamento>(entity =>
            {
                entity.HasKey(e => e.IdSituacaoAgendamento);

                entity.ToTable("SITUACAO_AGENDAMENTOS");

                entity.Property(e => e.DsSituacao)
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Veiculo>(entity =>
            {
                entity.HasKey(e => e.IdVeiculo);

                entity.ToTable("VEICULOS");

                entity.Property(e => e.DsVeiculo)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Placa)
                    .IsRequired()
                    .HasMaxLength(7)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdFornecedorNavigation)
                    .WithMany(p => p.Veiculos)
                    .HasForeignKey(d => d.IdFornecedor)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_VEICULOS01");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
