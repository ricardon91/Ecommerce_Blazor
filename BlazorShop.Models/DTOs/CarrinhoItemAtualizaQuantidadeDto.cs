using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorShop.Models.DTOs
{
    public class CarrinhoItemAtualizaQuantidadeDto
    {
        public int CarrinhoItemId { get; set; }
        public int Quantidade { get; set; }
    }
}
