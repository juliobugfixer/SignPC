using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace SignPC.Entities
{
    [Index(nameof(Email), IsUnique = true)]
    [Index(nameof(Senha), IsUnique = true)]
    [Index(nameof(Nome), IsUnique = true)]
    public class UsuarioConta
    {
        [Key]
        public int Id { get; set;}

        [Required(ErrorMessage="Digite o nome do Usuário.")]
        [MaxLength(200, ErrorMessage="Limite de (200) caracteres ultrapassado.")]
        public string Nome { get; set; } = " ";
        
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", ErrorMessage = "O e-mail não é válido.")]
        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage="Digite o e-mail.")]
        public string Email { get; set; } = " ";

        [DataType(DataType.Password)]
        [Required(ErrorMessage="Crie uma senha com (8) caracteres no mínimo.")]               
        [MaxLength(15, ErrorMessage="A senha deve conter de 8 a 15 caracteres."), MinLength(8, ErrorMessage="A senha deve conter de 8 a 15 caracteres.")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*[!@#$%^&*()_\-+=<>?.,]).{8,15}$", ErrorMessage = "A senha deve conter pelo menos (1) letra maiúscula, (1) letra minúscula e (1) caractere especial.")]
        public string Senha { get; set; } = " ";
    }
}
