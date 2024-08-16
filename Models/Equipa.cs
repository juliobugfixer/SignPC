using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using SignPC.Models;
using SignPC.Entities;

namespace SignPC.Models
{
    [Index(nameof(NomeEquipa), IsUnique = true)]

    public class Equipa
    {
        [Key]
        public int Id { get; set;}

        [Required(ErrorMessage="Digite o nome da equipa.")]
        [MaxLength(100, ErrorMessage="Limite de (100) caracteres ultrapassado.")]
        public string NomeEquipa { get; set; } = " ";
    }
}