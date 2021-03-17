using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SaudePublica.Domain.Database.Entities
{
    public class Consultorio
    {
        public Consultorio()
        {
            profissionals = new List<Profissional>();
        }

        public int id { get; set; }

        [Required]
        [MaxLength(100)]
        [DisplayName("Nome")]
        public string nome { get; set; }

        [DisplayName("Número da Sala")]
        public int numeroSala { get; set; }

        [DisplayName("Andar")]
        public int andar { get; set; }

        [Required]
        [DisplayName("Setor")]
        [MaxLength(100)]
        public string setor { get; set; }

        [Required]
        [DisplayName("Latitude")]
        public string latitude { get; set; }

        [Required]
        [DisplayName("Longitude")]
        public string longitude { get; set; }

        [NotMapped]
        public bool isChecked { get; set; }

        public virtual ICollection<Profissional> profissionals { get; set; }
    }
}