using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorShop.Models.DTOs
{
    public class CategoriaDto
    {
        public int Id { get; set; }
        public string? Nome { get; set; }
        public string? IconCSS { get; set; }
    }
}
