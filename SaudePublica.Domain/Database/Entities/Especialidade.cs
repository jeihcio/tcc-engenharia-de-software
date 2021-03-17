using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SaudePublica.Domain.Database.Entities
{
    public class Especialidade
    {
        public int id { get; set; }

        [DisplayName("Descrição")]
        [MaxLength(100)]
        public string descricao { get; set; }
    }
}