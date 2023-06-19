using BlazorShop.Models.DTOs;

namespace RazorShop.Web.Services.Interfaces
{
	public interface IGerenciaProdutosLocalStorageService
	{
		Task<IEnumerable<ProdutoDto>> GetCollection();
		Task RemoveCollection();
	}
}
