using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;
using SaudePublica.Domain.Database.Entities;

namespace SaudePublica.Domain.Database.Mapping
{
    public class EspecialidadeMap : EntityTypeConfiguration<Especialidade>
    {
        public EspecialidadeMap()
        {
            HasKey(x => x.id)
                .Map(x => x.ToTable("Especialidade"));

            Property(x => x.descricao)
                .HasMaxLength(100)
                .IsRequired();
        }
    }
}