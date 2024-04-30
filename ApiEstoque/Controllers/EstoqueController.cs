using Aplication.Interface;
using Aplication.ViewModels;
using Domain.Mensagem;
using Microsoft.AspNetCore.Mvc;
namespace ApiEstoque.Controllers;


[Route("[controller]")]
public sealed class EstoqueController : Controller
{
    private readonly IEstoqueService _estoqueService;

    public EstoqueController(IEstoqueService estoqueService)
    {
        _estoqueService = estoqueService;
    }


    /// <summary>
    /// Busca Produtos
    /// </summary>
    /// <param name="paginaAtual"></param>
    /// <param name="quantidadePorPagina"></param>
    /// <returns></returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(MensagemBase<ProdutoViewModel>))]
    [ProducesResponseType(StatusCodes.Status204NoContent, Type = typeof(MensagemBase<ProdutoViewModel>))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(MensagemBase<ProdutoViewModel>))]
    public async Task<IActionResult> BuscarProdutos(int paginaAtual, int quantidadePorPagina)
    {
        var response = await _estoqueService.BuscarProdutos(paginaAtual, quantidadePorPagina);
        if (response == null) return NoContent();
        return Ok(response);
    }

    /// <summary>
    /// Busca produto
    /// </summary>
    /// <param name="codigoDoPedido"></param>
    /// <returns></returns>
    [HttpGet]
    [Route("/GetPorCodigo/{codigoDoPedido}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(MensagemBase<ProdutoViewModel>))]
    [ProducesResponseType(StatusCodes.Status204NoContent, Type = typeof(MensagemBase<ProdutoViewModel>))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(MensagemBase<ProdutoViewModel>))]
    public async Task<IActionResult> BuscarProduto(Guid codigoDoPedido)
    {
        var response = await _estoqueService.BuscarProduto(codigoDoPedido);
        if (response == null) return NoContent();
        return Ok(response);
    }

    /// <summary>
    /// Reabastecer produtos
    /// </summary>
    /// <param name="produto"></param>
    /// <returns></returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(MensagemBase<ProdutoViewModel>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(MensagemBase<ProdutoViewModel>))]
    public async Task<IActionResult> ReabastecerProduto(ProdutoRequestViewModel produto)
    {
        var response = await _estoqueService.ReabastecerProduto(produto);
        if (response == null) return BadRequest();
        return Ok(response.Mensagem);

    }

    /// <summary>
    /// Reabastecer em lote
    /// </summary>
    /// <param name="caminho"></param>
    /// <returns></returns>
    [HttpPost]
    [Route("PorLista")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(MensagemBase<ProdutoViewModel>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(MensagemBase<ProdutoViewModel>))]
    public async Task<IActionResult> ReabastecerProdutoPorLista(string caminho)
    {
        try
        {
            var response = await _estoqueService.InserirProdutosPorLista(caminho);
            if (response != null) BadRequest();
            return Ok(response);

        }
        catch (ArgumentNullException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {

            return Problem(ex.Message);
        }

    }

    /// <summary>
    /// Atualiza produto
    /// </summary>
    /// <param name="produto"></param>
    /// <returns></returns>
    [HttpPut]
    [Route("/Atualizar")]
    [ProducesResponseType(StatusCodes.Status204NoContent, Type = typeof(MensagemBase<ProdutoRequestViewModel>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(MensagemBase<ProdutoRequestViewModel>))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(MensagemBase<ProdutoRequestViewModel>))]
    public async Task<IActionResult> AtualizarProduto([FromBody] ProdutoRequestViewModel produto)
    {
        var response = await _estoqueService.AtualizarProduto(produto);
        if(response == null) return BadRequest();
        return Ok(response);

    }

    /// <summary>
    ///  Atualiza parcial
    /// </summary>
    /// <param name="produto"></param>
    /// <returns></returns>
    [HttpPatch]
    [Route("AtualizarParcial")]
    [ProducesResponseType(StatusCodes.Status204NoContent, Type = typeof(MensagemBase<ProdutoViewModel>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(MensagemBase<ProdutoViewModel>))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(MensagemBase<ProdutoViewModel>))]
    public async Task<IActionResult> AtualizarParcial([FromBody] ProdutoRequestViewModel produto)
    {
        var response = await _estoqueService.AtualizarProdutoParcial(produto.CodigoProduto, produto.Nome);
        if (response == null) return BadRequest();
        return Ok(response);
    }
}
