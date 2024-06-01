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

        public async Task<MensagemBase<ProdutoViewModel>> BuscarProduto(Guid codigo)
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
            var produtoExiste =  await  _estoqueRepository.VerificaSeExiste(produto.Nome, produto.CodigoProduto);
            if (produtoExiste == null)
            {
                return new MensagemBase<ProdutoRequestViewModel>()
                {
                    Mensagem = "Ops algo deu errado produto não econtrado",
                    StatusCodes = StatusCodes.Status400BadRequest
                };
            }

           var estoqueAtualizado = await _estoqueRepository.AtualizarEstoque(produto.CodigoProduto,produto.Nome, produto.Quantidade);
            return new MensagemBase<ProdutoRequestViewModel>()
            {
                Mensagem = "Estoque reabastecido!",
                Object = estoqueAtualizado,
                StatusCodes = StatusCodes.Status200OK
            };

        }

        public async Task<MensagemBase<List<ProdutoRequestViewModel>>> InserirProdutosPorLista(string caminho)
        {
            var produtos = ReadXLs(caminho);

            if(produtos == null || !produtos.Any())
                return  new MensagemBase<List<ProdutoRequestViewModel>>() { Mensagem = $"Lista invalida", StatusCodes = StatusCodes.Status400BadRequest };

            List<ProdutoRequestViewModel> listExist = new List<ProdutoRequestViewModel>(); 
            List<ProdutoRequestViewModel> listNaoExist = new List<ProdutoRequestViewModel>(); 
            foreach(var p in produtos)
            {
              var existe = await _estoqueRepository.VerificaSeExiste(p.Nome,p.CodigoProduto);
                if(existe != null)
                    listExist.Add(p);
                else
                    listNaoExist.Add(p);
            }

            if(listNaoExist == null || !listNaoExist.Any())
                return new MensagemBase<List<ProdutoRequestViewModel>>() { Mensagem = $"Um ou mais Codigos invalidos {string.Join(",",listNaoExist)}", StatusCodes = StatusCodes.Status400BadRequest };

            listExist.ForEach(x => _estoqueRepository.AtualizarEstoque(x.CodigoProduto,x.Nome,x.Quantidade));
            return new MensagemBase<List<ProdutoRequestViewModel>>(){
                Mensagem = "Lista Atualizada com sucesso",
                Object = listExist,
                StatusCodes = StatusCodes.Status200OK};

        }

        public async Task<MensagemBase<ProdutoRequestViewModel>>AtualizarProdutoParcial( Guid codigoDoPedido, string nome)
        {
            var produtoExiste = await _estoqueRepository.VerificaSeExiste(nome, codigoDoPedido);
            
            if(produtoExiste == null)
                return new MensagemBase<ProdutoRequestViewModel>() { Mensagem = "Ops esse produto não foi encontrado", StatusCodes = StatusCodes.Status400BadRequest };

            if (produtoExiste.ProdutoNome.ToLower() == nome.ToLower())
                return new MensagemBase<ProdutoRequestViewModel>() { Mensagem = "Ops esse nome de produto já é existente em nossa base",  StatusCodes = StatusCodes.Status422UnprocessableEntity};
            

           var response = await _estoqueRepository.AtualizarProdutoParcial(codigoDoPedido, nome);
            return new MensagemBase<ProdutoRequestViewModel>()
            {
                Mensagem = "Nome do produto atualizado com sucesso!",
                Object = response,
                StatusCodes = StatusCodes.Status200OK
            };

        }


        public async Task<MensagemBase<ProdutoRequestViewModel>> AtualizarProduto(ProdutoRequestViewModel produto)
        {
            if(produto ==null) return new MensagemBase<ProdutoRequestViewModel>(){ Mensagem = "Produto invalido", StatusCodes = StatusCodes.Status400BadRequest};

            var produtoExiste = await _estoqueRepository.VerificaSeExiste(produto.Nome, produto.CodigoProduto);

            if (produtoExiste == null)
                return new MensagemBase<ProdutoRequestViewModel>() { Mensagem = "Ops esse produto não foi encontrado", StatusCodes = StatusCodes.Status400BadRequest };

            if (produtoExiste.ProdutoNome.ToLower() == produto.Nome.ToLower())
                return new MensagemBase<ProdutoRequestViewModel>() { Mensagem = "Ops esse nome de produto já é existente em nossa base", StatusCodes = StatusCodes.Status422UnprocessableEntity };

            var result =  await _estoqueRepository.AtualizarProduto(produto.CodigoProduto, produto.Nome, produto.Quantidade);

            return new MensagemBase<ProdutoRequestViewModel>()
            {
                Mensagem = "Nome do produto atualizado com sucesso!",
                Object = result,
                StatusCodes = StatusCodes.Status200OK
            }; 

        }
        private List<ProdutoRequestViewModel> ReadXLs(string caminho)
        {
            try
            {
                if (!caminho.Contains(".xlsx"))
                     throw new ArgumentNullException("Documento inserido, contem um formato invalido");
                var response = new List<ProdutoRequestViewModel>();
            
                FileInfo exitingFile = new FileInfo(fileName: caminho);

                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

                using (ExcelPackage packge = new ExcelPackage(exitingFile))
                {
                    ExcelWorksheet worksheet = packge.Workbook.Worksheets[PositionID:0];
                    int colunaCount = worksheet.Dimension.End.Column;
                    int rowCount = worksheet.Dimension.End.Row;

                    for(int row = 2; row<=rowCount; row++)
                    {
                        var produto = new ProdutoRequestViewModel();
                        produto.CodigoProduto = (Guid)worksheet.Cells[row, Col:1].Value;
                        produto.Nome = worksheet.Cells[row, Col: 2].Value?.ToString();
                        produto.Quantidade = Convert.ToInt32(worksheet.Cells[row, Col: 3].Value);

                        response.Add(produto);
                    }
                }
                return response;

            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
