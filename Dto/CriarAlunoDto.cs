namespace ProjetoPortifolio.Api.Dto
{
    public class CriarAlunoDto
    {
        public string Nome { get; set; }
        public string Sobre { get; set; }
        public string AreaDeAtuacao { get; set; }
        public string Experiencia { get; set; }
        public string Tecnologia { get; set; }
        public string HabilidadesCertificados { get; set; }
        public string Ferramentas { get; set; }
        public int? TurmaId { get; set; }
    }
}
