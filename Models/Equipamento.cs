using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SignPC.Models;
using System.ComponentModel.DataAnnotations;
using SignPC.Entities;
using SignPC.Enums;

namespace SignPC.Models

{
    public class Equipamento
    {
        [Key]
        public int Id { get; set;}
        
        [Required(ErrorMessage="Digite o nome do equipamento")]
        [MaxLength(10, ErrorMessage="Limite de (10) caracteres ultrapassado.")]
        public string NomeEquipamento { get; set; } = " ";

        [Required(ErrorMessage="Digite o tipo de equipamento.")]
        [MaxLength(100, ErrorMessage="Limite de (100) caracteres ultrapassado.")]
        public string Tipo { get; set; } = " ";


        [Required(ErrorMessage="Digite a descrição do equipamento.")]
        [MaxLength(200, ErrorMessage="Limite de (200) caracteres ultrapassado.")]
        public string  Descricao { get; set; } = " ";


        [Required(ErrorMessage="Escolha o estado do equipamento.")]
        public EstadoEnum? Estado { get; set; }
    }
}