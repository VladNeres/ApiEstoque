using Aplication.Interface;
using Aplication.ViewModels;
using Domain.Mensagem;
using Microsoft.AspNetCore.Mvc;
namespace ApiEstoque.Controllers;


[Route("[controller]")]
public class EstoqueController : Controller
{
    private readonly IEstoqueService _estoqueService;
    public EstoqueController(IEstoqueService estoque)
    {
        _estoqueService = estoque;
    }

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

    [HttpGet]
    [Route("/GetPorCodigo/{codigoDoPedido}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(MensagemBase<ProdutoViewModel>))]
    [ProducesResponseType(StatusCodes.Status204NoContent, Type = typeof(MensagemBase<ProdutoViewModel>))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(MensagemBase<ProdutoViewModel>))]
    public async Task<IActionResult> BuscarPorid(string codigoDoPedido)
    {
        var response = await _estoqueService.BuscarProdutoPorCodigo(codigoDoPedido);
        if (response == null) return NoContent();
        return Ok(response);
    }
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(MensagemBase<ProdutoViewModel>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(MensagemBase<ProdutoViewModel>))]
    public async Task<IActionResult> ReabastecerProduto(ProdutoRequestViewModel produto)
    {
        var response = await _estoqueService.ReabastecerProduto(produto);
            if(response!= null) return BadRequest();
            return Ok();
        
    }
    [HttpPost]
    [Route("PorLista")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(MensagemBase<ProdutoViewModel>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(MensagemBase<ProdutoViewModel>))]
    public async Task<IActionResult> ReabastecerProdutoPorLista(string caminho )
    {
        try
        {
            var response = await _estoqueService.InserirProdutosPorCodigo(caminho);
            if (response != null) BadRequest();
            return Ok(response);

        }
        catch(ArgumentNullException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {

            return Problem(ex.Message);
        }

    }

    [HttpPut]
    [Route("/Atualizar/{codigoDoProduto}")]
    [ProducesResponseType(StatusCodes.Status204NoContent, Type = typeof(MensagemBase<ProdutoViewModel>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(MensagemBase<ProdutoViewModel>))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(MensagemBase<ProdutoViewModel>))]
    public async Task<IActionResult> AtualizarProduto(string codigoDoProduto)
    {
        return BadRequest();
    }

    [HttpPatch]
    [Route("AtualizarParcial")]
    [ProducesResponseType(StatusCodes.Status204NoContent, Type = typeof(MensagemBase<ProdutoViewModel>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(MensagemBase<ProdutoViewModel>))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(MensagemBase<ProdutoViewModel>))]
    public async Task<IActionResult> AtualizarParcial(ProdutoRequestViewModel produto)
    {
        var response = await _estoqueService.AtualizarProdutoNome(produto.CodigoDoProduto, produto.Nome);
        if(response ==null) return BadRequest();
        return Ok(response);
    }
}
