namespace ProjetoPortifolio.Api.Dto
{
    public class VerProjetoDto
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public string TecnologiasUtilizadas { get; set; }
        public string LinkDeAcesso { get; set; }
        public string ImagemVideo { get; set; }
        public int? AlunoId { get; set; }
    }
}
