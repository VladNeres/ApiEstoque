using Aplication.Interface;
using Aplication.ViewModels;
using Domain.Mensagem;
using Microsoft.AspNetCore.Mvc;
namespace ApiEstoque.Controllers;


[Route("[controller]")]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
public sealed class EstoqueController : Controller
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
{
    private readonly IEstoqueService _estoqueService;
    private EstoqueController(IEstoqueService estoque)
    {
        _estoqueService = estoque;
    }


    /// <summary>
    /// Busca Produtos
    /// </summary>
    /// <param name="paginaAtual"></param>
    /// <param name="quantidadePorPagina"></param>
    /// <returns></returns>
    [HttpGet]
    [Route("")]
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
    public async Task<IActionResult> BuscarProduto(string codigoDoPedido)
    {
        var response = await _estoqueService.BuscarProdutoPorCodigo(codigoDoPedido);
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
        if (response != null) return BadRequest();
        return Ok();

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
            var response = await _estoqueService.InserirProdutosPorCodigo(caminho);
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
    /// <param name="codigoDoProduto"></param>
    /// <returns></returns>
    [HttpPut]
    [Route("/Atualizar/{codigoDoProduto}")]
    [ProducesResponseType(StatusCodes.Status204NoContent, Type = typeof(MensagemBase<ProdutoViewModel>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(MensagemBase<ProdutoViewModel>))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(MensagemBase<ProdutoViewModel>))]
    public async Task<IActionResult> AtualizarProduto(string codigoDoProduto)
    {
        var response = await _estoqueService.AtualizarProduto(codigoDoProduto);
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
#pragma warning disable CS8604 // Possible null reference argument.
        var response = await _estoqueService.AtualizarProdutoNome(produto.CodigoDoProduto, produto.Nome);
#pragma warning restore CS8604 // Possible null reference argument.
        if (response == null) return BadRequest();
        return Ok(response);
    }
}
