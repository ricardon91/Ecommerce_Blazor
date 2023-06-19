using BlazorShop.Models.DTOs;

namespace RazorShop.Web.Services.Interfaces
{
    public interface IProdutoService
    {
        Task<IEnumerable<ProdutoDto>> GetItems();
        Task<ProdutoDto> GetItem(int id);

        Task<IEnumerable<CategoriaDto>> GetCategorias();
        Task<IEnumerable<ProdutoDto>> GetItensPorCategoria(int categoriaId);
    }
}
