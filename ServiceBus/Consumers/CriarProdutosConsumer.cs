using Aplication.Mappers;
using Aplication.ViewModels;
using Domain.Model;
using Org.BouncyCastle.Asn1.X509.Qualified;
using ServiceBus.Interfaces;
using SqlDataAccess.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ServiceBus.Consumers
{
    public class CriarProdutosConsumer : IConsumer<string>
    {
        private readonly IEstoqueRepository _estoqueRepository;
        private const string CLASS_NAME = nameof(CriarProdutosConsumer);

        public CriarProdutosConsumer(IEstoqueRepository estoqueRepository)
        {
            _estoqueRepository = estoqueRepository;
        }

        public async Task ProcessMessage(string message)
        {
            // Lógica de processamento da mensagem para ConsumerA


            if (string.IsNullOrWhiteSpace(message))
            {

                Console.WriteLine($"{CLASS_NAME} - Error - a mensagem não pode ser nula.");
                return;
            }

            try
            {
                var produto = JsonSerializer.Deserialize<ProdutoViewModel>(message);
                if (produto == null)
                {
                    Console.WriteLine($"Erro ao tentar desrializar a mensagem");
                    return;
                }

                var produtoExiste = await _estoqueRepository.VerificaSeExiste(produto.Nome, produto.CodigoProduto);
                if (produtoExiste != null)
                {
                    Console.WriteLine($"Atenção o produto ja esta cadastrado!.");
                    return ;
                }


                await _estoqueRepository.CadastrarProdutoNoEstoque(produto.ParaProduto());
                Console.WriteLine($"Produto cadastrado.");
                return;

            }
            catch (Exception)
            {

                throw;  
            }
        }
    }
}
