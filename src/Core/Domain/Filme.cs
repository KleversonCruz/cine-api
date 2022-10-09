using Domain.Common;

namespace Domain
{
    public class Filme : BaseEntity, IAggregateRoot
    {
        public string Titulo { get; private set; } = default!;
        public int Duracao { get; private set; }
        public string Diretor { get; private set; } = default!;
        public string Genero { get; private set; } = default!;
        public int ClassificacaoEtaria { get; private set; }
        public virtual List<Sessao> Sessoes { get; private set; } = default!;

        public Filme(string titulo, int duracao, string diretor, string genero, int classificacaoEtaria)
        {
            Titulo = titulo;
            Duracao = duracao;
            Diretor = diretor;
            Genero = genero;
            ClassificacaoEtaria = classificacaoEtaria;
        }

        public Filme Update(string? titulo, int? duracao, string? diretor, string? genero, int? classificacaoEtaria)
        {
            if (titulo is not null && Titulo?.Equals(titulo) is not true) Titulo = titulo;
            if (duracao.HasValue && Duracao != duracao) Duracao = duracao.Value;
            if (diretor is not null && Diretor?.Equals(diretor) is not true) Diretor = diretor;
            if (genero is not null && Genero?.Equals(genero) is not true) Genero = genero;
            if (classificacaoEtaria.HasValue && ClassificacaoEtaria != classificacaoEtaria) ClassificacaoEtaria = classificacaoEtaria.Value;
            return this;
        }
    }
}
