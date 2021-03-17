using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using SaudePublica.Domain.Database.Entities.Enum;

namespace SaudePublica.Domain.Database.Entities
{
    public class Profissional
    {
        public Profissional()
        {
            consultorios = new List<Consultorio>();
        }

        public static Profissional Create(Profissional profissional)
        {
            return new Profissional
            {
                id = profissional.id,
                nome = profissional.nome,
                funcao = profissional.funcao,
                registroProfissional = profissional.registroProfissional,
                idEspecialidade = profissional.idEspecialidade,
                especialidade = profissional.especialidade,
                cpf = profissional.cpf,
                dataNascimento = profissional.dataNascimento,
                sexo = profissional.sexo,
                cep = profissional.cep,
                logradouro = profissional.logradouro,
                numeroLogradouro = profissional.numeroLogradouro,
                complemento = profissional.complemento,
                bairro = profissional.bairro,
                telefone = profissional.telefone,
                celular = profissional.celular,
                email = profissional.email,
                statusProfissional = profissional.statusProfissional,
                consultorios = profissional.consultorios
            };
        }

        [Key]
        public int id { get; set; }

        [Required]
        [DisplayName("Nome")]
        [MaxLength(100)]
        public string nome { get; set; }

        [Required]
        [DisplayName("Função")]
        [MaxLength(100)]
        public string funcao { get; set; }

        [Required]
        [DisplayName("Registro Profissional")]
        [MaxLength(100)]
        public string registroProfissional { get; set; }

        [DisplayName("Especialidade")]
        public int idEspecialidade { get; set; }

        public virtual Especialidade especialidade { get; set; }

        [Required]
        [DisplayName("CPF")]
        [MaxLength(14)]
        public string cpf { get; set; }

        [Required]
        [DisplayName("Data de nascimento")]
        [DataType(DataType.DateTime, ErrorMessage = "Data inválida, preencha no formato dd/mm/aaaa")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy hh:mm}", ApplyFormatInEditMode = true)]
        public DateTime dataNascimento { get; set; }

        [Required]
        [DisplayName("Sexo")]
        public int sexo { get; set; }

        [DisplayName("CEP")]
        [MaxLength(9)]
        public string cep { get; set; }

        [DisplayName("Logradouro")]
        [MaxLength(500)]
        public string logradouro { get; set; }

        [DisplayName("Número do Logradouro")]
        public int numeroLogradouro { get; set; }

        [DisplayName("Complemento")]
        [MaxLength(500)]
        public string complemento { get; set; }

        [DisplayName("Bairro")]
        [MaxLength(200)]
        public string bairro { get; set; }

        [DisplayName("Telefone")]
        [MaxLength(100)]
        public string telefone { get; set; }

        [DisplayName("Celular")]
        [MaxLength(100)]
        public string celular { get; set; }

        [Required]
        [DisplayName("E-mail")]
        [MaxLength(256)]
        [EmailAddress(ErrorMessage = "E-mail inválido")]
        public string email { get; set; }

        [DisplayName("Status Profissional")]
        [MaxLength(100)]
        public string statusProfissional { get; set; }

        public virtual ICollection<Consultorio> consultorios { get; set; }
    }
}