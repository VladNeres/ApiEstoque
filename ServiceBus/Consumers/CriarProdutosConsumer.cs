using ServiceBus.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceBus.Consumers
{
    public class CriarProdutosConsumer : IConsumer<string>
    {
        public void ProcessMessage(string message)
        {
            // Lógica de processamento da mensagem para ConsumerA
            Console.WriteLine($"[Consumer A] Mensagem recebida: {message}");
        }
    }
}
