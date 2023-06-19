using BlazorShop.API.Mappings;
using BlazorShop.API.Repositories;
using BlazorShop.Models.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace BlazorShop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarrinhoCompraController : ControllerBase
    {
        private readonly ICarrinhoCompraRepository _carrinhoCompraRepository;
        private readonly IProdutoRepository _produtoRepository;
        private ILogger<CarrinhoCompraController> _logger;

        public CarrinhoCompraController(ICarrinhoCompraRepository carrinhoCompraRepository, IProdutoRepository produtoRepository, ILogger<CarrinhoCompraController> logger)
        {
            _carrinhoCompraRepository = carrinhoCompraRepository;
            _produtoRepository = produtoRepository;
            _logger = logger;
        }

        [HttpGet]
        [Route("{usuarioId}/GetItens")]
        public async Task<ActionResult<IEnumerable<CarrinhoItemDto>>> GetItens(string usuarioId)
        {
            try
            {
                var carrinhoItens = await _carrinhoCompraRepository.GetItens(usuarioId);
                if(carrinhoItens == null)
                {
                    return NoContent();
                }

                var produtos = await _produtoRepository.GetItens();
                if(produtos == null)
                {
                    throw new Exception("Não existem produtos...");
                }

                var carrinhoItensDto = carrinhoItens.ConverterCarrinhoItensParaDto(produtos);
                return Ok(carrinhoItensDto);
            }catch (Exception ex)
            {
                _logger.LogError("## Erro ao obter itens do carrinho");
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CarrinhoItemDto>> GetItem(int id)
        {
            try
            {
                var carrinhoItem = await _carrinhoCompraRepository.GetItem(id);
                if (carrinhoItem == null)
                {
                    return NotFound($"Item não encontrado.");
                }

                var produto = await _produtoRepository.GetItem(carrinhoItem.ProdutoId);
                if (produto == null)
                {
                    return NotFound($"Item não existe na fonte de dados."); 
                }

                var carrinhoItemDto = carrinhoItem.ConverterCarrinhoItemParaDto(produto);
                return Ok(carrinhoItemDto);
            }
            catch (Exception ex)
            {
                _logger.LogError($"## Erro ao obter item = {id} do carrinho");
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<CarrinhoItemDto>> PostItem([FromBody] CarrinhoItemAdicionaDto carrinhoItemAdicionaDto)
        {
            try
            {
                var novoCarrinhoItem = await _carrinhoCompraRepository.AdicionaItem(carrinhoItemAdicionaDto);

                if(novoCarrinhoItem == null) { return NoContent(); }

                var produto = await _produtoRepository.GetItem(novoCarrinhoItem.ProdutoId);

                if(produto == null) 
                {
                    throw new Exception($"Produto não localizado (Id:({carrinhoItemAdicionaDto.ProdutoId})");
                }

                var novoCarrinhoItemDto = novoCarrinhoItem.ConverterCarrinhoItemParaDto(produto);

                return CreatedAtAction(nameof(GetItem), new { id = novoCarrinhoItem.Id }, novoCarrinhoItemDto);
            }
            catch(Exception ex)
            {
                _logger.LogError("## Erro ao criar umnovo item no carrinho");
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<CarrinhoItemDto>> DeleteItem(int id)
        {
            try
            {
                var carrinhoItem = await _carrinhoCompraRepository.DeletaItem(id);

                if(carrinhoItem == null) 
                {
                    return NotFound();
                }

                var produto = await _produtoRepository.GetItem(carrinhoItem.ProdutoId);

                if(produto == null)
                    return NotFound();

                var carrinhoItemDto = carrinhoItem.ConverterCarrinhoItemParaDto(produto);
                return Ok(carrinhoItemDto);
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPatch("{id:int}")]
        public async Task<ActionResult<CarrinhoItemDto>> AtualizaQuantidade(int id, CarrinhoItemAtualizaQuantidadeDto carrinhoItemAtualizaQuantidadeDto)
        {
            try
            {
                var carrinhoItem = await _carrinhoCompraRepository.AtualizaQuantidade(id, carrinhoItemAtualizaQuantidadeDto);

                if(carrinhoItem == null)
                {
                    return NotFound();
                }

                var produto = await _produtoRepository.GetItem(carrinhoItem.ProdutoId);
                var carrinhoItemDto = carrinhoItem.ConverterCarrinhoItemParaDto(produto);
                return Ok(carrinhoItemDto);
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
