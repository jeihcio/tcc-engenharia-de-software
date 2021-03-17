using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Data.Entity.ModelConfiguration.Configuration;
using System.Linq;
using System.Web;
using SaudePublica.Domain.Database.Entities;

namespace SaudePublica.Domain.Database.Mapping
{
    public class ProfissionalMap : EntityTypeConfiguration<Profissional>
    {
        public ProfissionalMap()
        {
            HasKey(x => x.id)
                .Map(x => x.ToTable("Profissional"));

            HasRequired(x => x.especialidade)
                .WithMany()
                .HasForeignKey(x => x.idEspecialidade);

            HasMany(s => s.consultorios)
                .WithMany(c => c.profissionals)
                .Map(cs =>
                {
                    cs.MapLeftKey("Profissional_id");
                    cs.MapRightKey("Consultorio_id");
                    cs.ToTable("ProfissionalConsultorio");
                });
        }
    }
}