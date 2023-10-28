using Aplication.Interface;
using Aplication.ViewModels;
using Domain.Mensagem;
using Domain.Model;
using Microsoft.AspNetCore.Http;
using SqlDataAccess.Repositories.Interface;

using OfficeOpenXml;


namespace Aplication.Service
{
    public class EstoqueService : IEstoqueService
    {
        private readonly IEstoqueRepository _estoqueRepository;

        public EstoqueService(IEstoqueRepository estoqueRepository)
        {
            _estoqueRepository = estoqueRepository;
        }

        public async Task<MensagemBase<ProdutoViewModel>> BuscarProdutoPorCodigo(string codigo)
        {
           Produto produto = await _estoqueRepository.BuscarProduto(codigo);
            if(produto == null) return new MensagemBase<ProdutoViewModel>() { Mensagem = "Ops algo deu errado, produto não encontrado", StatusCodes = StatusCodes.Status422UnprocessableEntity };
           
            return new MensagemBase<ProdutoViewModel>()
            {
                Mensagem = "Busca realizada com sucesso",
                Object = produto,
                StatusCodes = StatusCodes.Status200OK
            };
        }

        public async Task<MensagemBase<Paginacao<List<ProdutoViewModel>>>> BuscarProdutos(int paginaAtual, int quantidadePorPagina)
        {
           var produtos = await _estoqueRepository.BuscarProdutos(paginaAtual, quantidadePorPagina);
            if (produtos == null)
                return new MensagemBase<Paginacao<List<ProdutoViewModel>>>() { Mensagem = "Ops algo deu errado, nao foram encontrados pedidos", StatusCodes = StatusCodes.Status422UnprocessableEntity };

               return new MensagemBase<Paginacao<List<ProdutoViewModel>>>() 
               { Mensagem = "Busca realizada com sucesso", 
                   Object = produtos, 
                   StatusCodes = StatusCodes.Status200OK 
               };

        }

        public async Task<MensagemBase<ProdutoRequestViewModel>> ReabastecerProduto(ProdutoRequestViewModel produto)
        {
            var produtoExiste =  await  _estoqueRepository.VerificaSeExiste(produto.Nome, produto.CodigoDoProduto);
            if (produtoExiste == null)
            {
                return new MensagemBase<ProdutoRequestViewModel>()
                {
                    Mensagem = "Ops algo deu errado produto não econtrado",
                    StatusCodes = StatusCodes.Status400BadRequest
                };
            }

           var estoqueAtualizado = await _estoqueRepository.AtualizarEstoque(produto.CodigoDoProduto,produto.Nome, produto.QuantidadeEmEstoque);
            return new MensagemBase<ProdutoRequestViewModel>()
            {
                Mensagem = "Estoque reabastecido!",
                Object = estoqueAtualizado,
                StatusCodes = StatusCodes.Status200OK
            };

        }

        public async Task<List<ProdutoRequestViewModel>> InserirProdutosPorCodigo(string caminho)
        {
            var produtos = ReadXLs(caminho);
           
            List<ProdutoRequestViewModel> listExist = new List<ProdutoRequestViewModel>(); 
            List<ProdutoRequestViewModel> listNaoExist = new List<ProdutoRequestViewModel>(); 
            foreach(var p in produtos)
            {
              var existe = await _estoqueRepository.VerificaSeExiste(p.Nome,p.CodigoDoProduto);
                if(existe != null)
                    listExist.Add(p);
                else
                    listNaoExist.Add(p);
                
            }

            listExist.ForEach(x => _estoqueRepository.AtualizarEstoque(x.CodigoDoProduto,x.Nome,x.QuantidadeEmEstoque));
            return listExist;

        }

        public async Task<MensagemBase<ProdutoRequestViewModel>>AtualizarProdutoNome( string codigoDoPedido, string nome)
        {
            var produtoExiste = await _estoqueRepository.VerificaSeExiste(nome, codigoDoPedido);
            
            if(produtoExiste == null)
                return new MensagemBase<ProdutoRequestViewModel>() { Mensagem = "Ops esse produto não foi encontrado", StatusCodes = StatusCodes.Status400BadRequest };

            if (produtoExiste.Nome.ToLower() == nome.ToLower())
                return new MensagemBase<ProdutoRequestViewModel>() { Mensagem = "Ops esse nome de produto já é existente em nossa base",  StatusCodes = StatusCodes.Status422UnprocessableEntity};
            

           var response = await _estoqueRepository.AtualizarProdutoNome(codigoDoPedido, nome);
            return new MensagemBase<ProdutoRequestViewModel>()
            {
                Mensagem = "Nome do produto atualizado com sucesso!",
                Object = response,
                StatusCodes = StatusCodes.Status200OK
            };

        }

        private List<ProdutoRequestViewModel> ReadXLs(string caminho)
        {
            if (!caminho.Contains(".xlsx"))
                 throw new ArgumentNullException("Documento inserido, contem um formato invalido");
            var response = new List<ProdutoRequestViewModel>();
            
            FileInfo exitingFile = new FileInfo(fileName: caminho);

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using (ExcelPackage packge = new ExcelPackage(exitingFile))
            {
                ExcelWorksheet worksheet= packge.Workbook.Worksheets[PositionID:0];
                int colunaCount = worksheet.Dimension.End.Column;
                int rowCount = worksheet.Dimension.End.Row;

                for(int row = 2; row<=rowCount; row++)
                {
                    var produto = new ProdutoRequestViewModel();
                    produto.CodigoDoProduto = worksheet.Cells[row, Col:1].Value?.ToString();
                    produto.Nome = worksheet.Cells?[row, Col: 2].Value?.ToString();
                    produto.QuantidadeEmEstoque = Convert.ToInt32(worksheet.Cells[row, Col: 3].Value);

                    response.Add(produto);
                }
            }
            return response;
        }
    }
}
