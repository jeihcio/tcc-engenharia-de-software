using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;
using SaudePublica.Domain.Database.Entities;

namespace SaudePublica.Domain.Database.Mapping
{
    public class ConsultorioMap : EntityTypeConfiguration<Consultorio>
    {
        public ConsultorioMap()
        {
            HasKey(x=> x.id)
                .Map(x => x.ToTable("Consultorio"));
        }
    }
}