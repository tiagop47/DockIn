public record RegraClassificacaoABC
{
    public decimal LimiteClasseA { get; init; } // ex: 80%
    public decimal LimiteClasseB { get; init; } // ex: 95% (80 + 15)

    public RegraClassificacaoABC(decimal percentagemA = 80m, decimal percentagemB = 15m)
    {
        if (percentagemA <= 0 || percentagemB <= 0 || (percentagemA + percentagemB) >= 100)
        {
            throw new ArgumentException("As percentagens da Curva ABC são inválidas.");
        }

        LimiteClasseA = percentagemA;
        LimiteClasseB = percentagemA + percentagemB;
    }

    // Padrão de indústria pré-definido (80/15/5)
    public static RegraClassificacaoABC Padrao => new(80m, 15m);
}
