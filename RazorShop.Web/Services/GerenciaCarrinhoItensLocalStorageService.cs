using Blazored.LocalStorage;
using BlazorShop.Models.DTOs;
using RazorShop.Web.Services.Interfaces;

namespace RazorShop.Web.Services
{
	public class GerenciaCarrinhoItensLocalStorageService : IGerenciaCarrinhoItensLocalStorageService
	{
		private const string key = "CArrinhoItemCollection";

		private readonly ILocalStorageService localStorageService;
		private readonly ICarrinhoCompraService carrinhoCompraService;

		public GerenciaCarrinhoItensLocalStorageService(ILocalStorageService localStorageService, ICarrinhoCompraService carrinhoCompraService)
		{
			this.localStorageService = localStorageService;
			this.carrinhoCompraService = carrinhoCompraService;
		}

		public Task<List<CarrinhoItemDto>> GetCollection()
		{
			throw new NotImplementedException();
		}

		public Task RemoveCollection()
		{
			throw new NotImplementedException();
		}

		public Task SaveCollection(List<CarrinhoItemDto> carrinhoItensDto)
		{
			throw new NotImplementedException();
		}
	}
}
