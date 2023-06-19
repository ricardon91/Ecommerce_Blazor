using BlazorShop.Models.DTOs;
using RazorShop.Web.Services.Interfaces;
using System.Net;
using System.Net.Http.Json;

namespace BlazorShop.Web.Services
{
    public class ProdutoService : IProdutoService
    {
        public HttpClient _httpClient;
        public ILogger<ProdutoService> _logger;

        public ProdutoService(HttpClient httpClient, ILogger<ProdutoService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<IEnumerable<ProdutoDto>> GetItems()
        {
            try
            {
                var produtosDto = await _httpClient.GetFromJsonAsync<IEnumerable<ProdutoDto>>("api/produtos");
                return produtosDto;
            }
            catch (Exception ex) 
            {
                _logger.LogError("Erro ao acessar produtos: api/produtos");
                throw new Exception(ex.Message, ex);
            }
            
        }

		public async Task<ProdutoDto> GetItem(int id)
		{
			try
			{
				var response = await _httpClient.GetAsync($"api/produtos/{id}");

				if (response.IsSuccessStatusCode)
				{
					if (response.StatusCode == HttpStatusCode.NoContent)
					{
						return default(ProdutoDto);
					}
					return await response.Content.ReadFromJsonAsync<ProdutoDto>();
				}
				else
				{
					var message = await response.Content.ReadAsStringAsync();
					_logger.LogError($"Erro a obter produto pelo id= {id} - {message}");
					throw new Exception($"Status Code : {response.StatusCode} - {message}");
				}
			}
			catch (Exception ex)
			{
				_logger.LogError($"Erro a obter produto pelo id={id}");
				throw;
			}
		}

        public async Task<IEnumerable<CategoriaDto>> GetCategorias()
        {
            try
            {
                var response = await _httpClient.GetAsync("api/Produtos/GetCategorias");
                if (response.IsSuccessStatusCode)
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                    {
                        return Enumerable.Empty<CategoriaDto>().ToList();
                    }

                    return await response.Content.ReadFromJsonAsync<List<CategoriaDto>>();
                }
                else
                {
                    var message = await response.Content.ReadAsStringAsync();
                    throw new Exception($"Http Status Code: {response.StatusCode} Mensagem: {message}");
                }
            }
            catch (Exception)
            {
                _logger.LogError($"Erro a obter categorias");
                throw;
            }
            
        }

        public async Task<IEnumerable<ProdutoDto>> GetItensPorCategoria(int categoriaId)
        {
            try
            {
                var response = await _httpClient.GetAsync($"api/Produtos/{categoriaId}/GetItensPorCategoria");
                if (response.IsSuccessStatusCode)
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                    {
                        return Enumerable.Empty<ProdutoDto>();
                    }

                    return await response.Content.ReadFromJsonAsync<IEnumerable<ProdutoDto>>();
                }
                else
                {
                    var message = await response.Content.ReadAsStringAsync();
                    throw new Exception($"Http Status Code: {response.StatusCode} Mensagem: {message}");
                }
            }
            catch (Exception)
            {
                _logger.LogError($"Erro a obter categoria id - {categoriaId}");
                throw;
            }
        }
    }
}
