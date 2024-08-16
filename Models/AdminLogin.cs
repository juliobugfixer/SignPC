using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using SignPC.Entities;

namespace SignPC.Models
{
    public class AdminLogin
    {
        [Key]
        public int Id { get; set;}

        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage="Digite o e-mail.")]
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", ErrorMessage = "O e-mail não é válido.")]
        public string Email { get; set; } = " ";

        [DataType(DataType.Password)]
        [Required(ErrorMessage="Digite a senha.")]        
        [MaxLength(15, ErrorMessage="A senha deve conter de 8 a 15 caracteres."), MinLength(8, ErrorMessage="A senha deve conter de 8 a 15 caracteres.")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*[!@#$%^&*()_\-+=<>?.,]).{8,15}$", ErrorMessage = "A senha deve conter pelo menos (1) letra maiúscula, (1) letra minúscula e (1) caractere especial.")]
        public string Senha { get; set; } = " ";
    }
}