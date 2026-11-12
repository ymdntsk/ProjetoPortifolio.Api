namespace ProjetoPortifolio.Api.Dto
{
    public class AlunoPortifolioDto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string? NomeTurma { get; set; }
        public string Sobre { get; set; }
        public string AreaDeAtuacao { get; set; }
        public string Experiencia { get; set; }
        public string Tecnologia { get; set; }
        public string HabilidadesCertificados { get; set; }
        public string Ferramentas { get; set; }

        public bool PodePublicar { get; set; }

        public List<VerProjetoDto> Projetos { get; set; } = new List<VerProjetoDto>();
    }
}
