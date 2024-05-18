using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ConsoleAppCRUD.Models
{
    public class Fornecedor
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string NomeFantasia { get; set; }

        [Required]
        [MaxLength(20)]
        public string CNPJ { get; set; }

        [MaxLength(15)]
        public string Telefone { get; set; }

        [MaxLength(100)]
        public string Email { get; set; }
    }
}
