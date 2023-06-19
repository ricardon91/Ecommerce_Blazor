using BlazorShop.API.Mappings;
using BlazorShop.API.Repositories;
using BlazorShop.Models.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace BlazorShop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProdutosController : ControllerBase
    {
        private readonly IProdutoRepository _produtoRepository;

        public ProdutosController(IProdutoRepository produtoRepository)
        {
            _produtoRepository = produtoRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProdutoDto>>> GetItems()
        {
            try
            {
                var produtos = await _produtoRepository.GetItens();

                if(produtos == null)
                {
                    return NotFound();
                }
                else
                {
                    var produtosDtos = produtos.ConverterProdutosParaDto();
                    return Ok(produtosDtos);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
                throw new Exception(ex.Message, ex);                
            }
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<ProdutoDto>> GetItem(int id)
        {
            try
            {
                var produto = await _produtoRepository.GetItem(id);

                if (produto == null)
                {
                    return NotFound("Erro ao localizar o produto.");
                }
                else
                {
                    var produtoDtos = produto.ConverterProdutoParaDto();
                    return Ok(produtoDtos);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
                throw new Exception(ex.Message, ex);
            }
        }

        [HttpGet]
        [Route("{categoriaId}/GetItensPorCategoria")]
        public async Task<ActionResult<IEnumerable<ProdutoDto>>> GetItensPorCategoria(int categoriaId)
        {
            try
            {
                var produtos = await _produtoRepository.GetItensPorCategoria(categoriaId);                
                var produtosDtos = produtos.ConverterProdutosParaDto();
                return Ok(produtosDtos);
                
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
                throw new Exception(ex.Message, ex);
            }
        }

        [HttpGet]
        [Route("GetCategorias")]
        public async Task<ActionResult<IEnumerable<CategoriaDto>>> GetCategorias()
        {
            try
            {
                var categorias = await _produtoRepository.GetCategorias();
                var categoriaDto = categorias.ConverterCategoriasParaDto();
                return Ok(categoriaDto);

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
                throw new Exception(ex.Message, ex);
            }
        }
    }
}
