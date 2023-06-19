using BlazorShop.Models.DTOs;

namespace RazorShop.Web.Services.Interfaces
{
	public interface IGerenciaCarrinhoItensLocalStorageService
	{
		Task<List<CarrinhoItemDto>> GetCollection();
		Task SaveCollection(List<CarrinhoItemDto> carrinhoItensDto);
		Task RemoveCollection();
	}
}
