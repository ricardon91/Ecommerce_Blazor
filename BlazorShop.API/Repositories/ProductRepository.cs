using BlazorShop.API.Context;
using BlazorShop.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace BlazorShop.API.Repositories
{
    public class ProdutoRepository : IProdutoRepository
    {
        private readonly AppDbContext dbContext;

        public ProdutoRepository(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }        

        public async Task<Produto> GetItem(int id)
        {
            var produto = await dbContext.Produtos.Include(c => c.Categoria).SingleOrDefaultAsync(c => c.Id == id);
            return produto;
        }

        public async Task<IEnumerable<Produto>> GetItens()
        {
            var produtos = await dbContext.Produtos.Include(c => c.Categoria).ToListAsync();
            return produtos;
        }

        public async Task<IEnumerable<Produto>> GetItensPorCategoria(int id)
        {
            var produtos = await dbContext.Produtos
                .Include(c => c.Categoria)
                .Where(c => c.CategoriaId == id)
                .ToListAsync();
            return produtos;
        }

        public async Task<IEnumerable<Categoria>> GetCategorias()
        {
            var categorias = await dbContext.Categorias.ToListAsync();
            return categorias;
        }
    }
}
