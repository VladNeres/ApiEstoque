using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Mensagem
{
    public class MensagemBase<T>
    {
        public MensagemBase(int statusCodes, object @object)
        {
            StatusCodes = statusCodes;
            Object = @object;
        }

        public MensagemBase(int statusCodes, string mensagem, object @object)
        {
            StatusCodes = statusCodes;
            Mensagem = mensagem;
            Object = @object;
        }

        public MensagemBase()
        {

        }
        public string Mensagem { get; set; }
        public int StatusCodes { get; set; }
        public object Object { get; set; }

    }
}
