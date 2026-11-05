using System.Text.Json.Serialization;

namespace ProjetoPortifolio.Api.Models
{
    public class Turma
    {
        public int id { get; set; }
        public string Nome { get; set; }

        [JsonIgnore]
        public ICollection<Aluno> Alunos { get; set; } = new List<Aluno>();
    }
}
