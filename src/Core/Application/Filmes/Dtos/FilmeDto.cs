using Application.Common.Interfaces;

namespace Application.Filmes.Dtos
{
    public class FilmeDto : IDto
    {
        public Guid Id { get; set; }
        public string Titulo { get; set; } = default!;
        public int Duracao { get; set; }
        public string Diretor { get; set; } = default!;
        public string Genero { get; set; } = default!;
        public int ClassificacaoEtaria { get; set; }
    }
}