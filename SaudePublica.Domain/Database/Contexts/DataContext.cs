using SaudePublica.Domain.Database.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;
using SaudePublica.Domain.Database.Mapping;

namespace SaudePublica.Domain.Database
{
    public class DataContext : DbContext
    {
        #region Constructors

        public DataContext()
        //: base("Data Source=JEIHCIO-NOTE\\SQLEXPRESS;Initial Catalog=tcc;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False")
            :base("Data Source=den1.mssql7.gear.host;Initial Catalog=saudepublica;User ID=saudepublica;Password=Xd2f4vP-~O89;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False")
        {
            Configuration.LazyLoadingEnabled = false;
        }

        public static DataContext Create()
        {
            return new DataContext();
        }

        #endregion

        #region Properties

        public virtual DbSet<Profissional> Profissionais { get; set; }
        public virtual DbSet<Especialidade> Especialidades { get; set; }
        public virtual DbSet<Consultorio> Consultorios { get; set; }

        #endregion

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            #region Maps

            modelBuilder.Configurations.Add(new ConsultorioMap());
            modelBuilder.Configurations.Add(new EspecialidadeMap());
            modelBuilder.Configurations.Add(new ProfissionalMap());

            #endregion

            base.OnModelCreating(modelBuilder);
        }

    }
}