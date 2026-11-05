using System.Text.Json.Serialization;

namespace ProjetoPortifolio.Api.Models
{
    public class Projeto
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public string TecnologiasUtilizadas  { get; set; }
        public string LinkDeAcesso { get; set; }
        public string ImagemVideo { get; set; }

        public int? AlunoId { get; set; }
        [JsonIgnore]
        public Aluno? Aluno { get; set; }
    }
}
