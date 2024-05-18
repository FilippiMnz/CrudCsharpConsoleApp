using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ConsoleAppCRUD.Models
{
    public class Produto
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Nome { get; set; }

        public int Quantidade { get; set; }

        [Column(TypeName = "decimal(10, 2)")]
        public decimal Preco { get; set; }
    }
}
