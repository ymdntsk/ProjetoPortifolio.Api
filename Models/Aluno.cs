using System.Text.Json.Serialization;

namespace ProjetoPortifolio.Api.Models
{
    public class Aluno
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Sobre { get; set; }
        public string AreaDeAtuacao { get; set; }
        public string Experiencia { get; set; }
        public string Tecnologia { get; set; }
        public string HabilidadesCertificados { get; set; }
        public string Ferramentas { get; set; }

        public int? TurmaId { get; set; }
        [JsonIgnore]
        public Turma? Turma { get; set; }
        [JsonIgnore]
        public ICollection<Projeto> Projetos { get; set; } = new List<Projeto>();


    }
}
