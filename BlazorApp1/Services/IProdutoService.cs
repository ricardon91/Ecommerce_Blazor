using BlazorShop.Models.DTOs;

namespace BlazorShop.Web.Services
{
    public interface IProdutoService
    {
        Task<IEnumerable<ProdutoDto>> GetItems();
        //Task<ProdutoDto> GetItem(int id);

        //Task<IEnumerable<CategoriaDto>> GetCategorias();
        //Task<IEnumerable<ProdutoDto>> GetItemsPorCategoria(int categoriaId);
    }
}
