using BlazorShop.API.Entities;

namespace BlazorShop.API.Repositories
{
    public interface IProdutoRepository
    {
        Task<IEnumerable<Produto>> GetItens();
        Task<Produto> GetItem(int id);

        Task<IEnumerable<Produto>> GetItensPorCategoria(int id);
        Task<IEnumerable<Categoria>> GetCategorias();
    }
}
