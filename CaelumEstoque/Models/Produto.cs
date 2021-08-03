using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CaelumEstoque.Models
{
    public class Produto
    {
        public int Id { get; set; }

        [Required, StringLength(20)]
        public String Nome { get; set; }

        [Range(0.1, 10000.1, ErrorMessage = "Preço do produto deve estar entre 0,00 e 10.000,00")]
        public float Preco { get; set; }

        public CategoriaDoProduto Categoria { get; set; }

        public int? CategoriaId { get; set; }

        [Required, StringLength(100, ErrorMessage = "")]
        public String Descricao { get; set; }
        
        [Range(1, Int32.MaxValue, ErrorMessage = "O campo Quantidade deve ser maior que zero.")]
        public int Quantidade { get; set; }
    }
}