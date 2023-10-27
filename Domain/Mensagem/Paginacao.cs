namespace Domain.Mensagem
{
    public class Paginacao<T>
    {
        public int ContadorTotal { get; set; }
        public int? QuantidadePorPagina { get; set; }
        public int? PaginaAtual { get; set; }
        public int MaximoDePaginas { get; set; }
        public bool TemProximaPagina { get; set; }
        public bool TemPaginaAnterior { get; set; }
        public T Objeto { get; set; }

        public Paginacao(int quantidadeTotal, T objeto, int? paginaAtual, int? quantidadePorPagina)
        {
            ContadorTotal = quantidadeTotal;
            Objeto = objeto;
            PaginaAtual = paginaAtual;
            QuantidadePorPagina = quantidadePorPagina;

            if (ContadorTotal <= 0 && QuantidadePorPagina <= 0) 
                MaximoDePaginas = 0;
            else
             MaximoDePaginas = (int)Math.Ceiling((double)ContadorTotal / (double)QuantidadePorPagina);
            TemPaginaAnterior = PaginaAtual > 1;
            TemProximaPagina = PaginaAtual < MaximoDePaginas;
        }
    }
}
