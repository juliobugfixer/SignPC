using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using SignPC.Models;
using SignPC.Entities;
using SignPC.Enums;

namespace SignPC.Models
{

    [Index(nameof(NumProcesso), IsUnique = true)]
    [Index(nameof(EmailMembro), IsUnique = true)]

    public class Membro
    {
        [Key]
        public int Id { get; set;}
 
        [Required(ErrorMessage="Digite o número de processo.")]
        [MaxLength(20, ErrorMessage="Limite de (20) caracteres ultrapassado.")]
        public string NumProcesso { get; set;} = " ";

        [Required(ErrorMessage="Digite o nome do membro.")]
        [MaxLength(200, ErrorMessage="Limite de (200) caracteres ultrapassado.")]
        public string NomeMembro { get; set; } = " ";

        [Required(ErrorMessage="Escolha o gênero.")]
        public GeneroEnum? Genero { get; set; }

        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage="Digite o e-mail do membro.")]
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", ErrorMessage = "O e-mail não é válido.")]
        public string EmailMembro { get; set; } = " ";

        [Required(ErrorMessage="Escolha o curso.")]
        public CursoEnum Curso { get; set; }

        [Required(ErrorMessage="Escolha o ano.")]
        public AnoEnum Ano { get; set; }
        
        [Required(ErrorMessage="Digite o contacto.")]
        [RegularExpression(@"^\+244 \d{3} \d{3} \d{3}$", ErrorMessage = "Contacto inválido! Digite corretamente.")]
        public string Telefone { get; set;} = " ";

        public string RefEquipa { get; set; } = "";
    }
}