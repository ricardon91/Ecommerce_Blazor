using BlazorShop.Models.DTOs;

namespace RazorShop.Web.Services.Interfaces
{
    public interface ICarrinhoCompraService
    {
        Task<List<CarrinhoItemDto>> GetItens(string usuarioId);
        Task<CarrinhoItemDto> AdicionaItem(CarrinhoItemAdicionaDto carrinhoItemAdicionaDto);
        Task<CarrinhoItemDto> DeletaItem(int id);
        Task<CarrinhoItemDto> AtualizaQuantidade(CarrinhoItemAtualizaQuantidadeDto carrinhoItemAtualizaQuantidadeDto);
        void RaiseEventOnCarrinhoCompraChanged(int totalQuantidade);

        event Action<int> OnCarrinhoCompraChanged;
        
    }
}
