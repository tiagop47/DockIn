# Diagrama de Classes — DockIn

Modelo conceptual alinhado com a Proposta de Sistema e a Declaração de Âmbito (versão 2), os casos de uso, o modelo de dados e os modelos de comportamento. As operações representam responsabilidades do domínio, sem impor uma tecnologia.

```plantuml
@startuml
skinparam classAttributeIconSize 0
hide empty members

package "Acesso" {
  class Utilizador {
    +id : int
    +nome : string
    +email : string
    +passwordHash : string
    +perfil : Perfil
    +ativo : bool
    +podeAutenticar() : bool
  }
  enum Perfil {
    Administrador
    GestorStock
    Operador
    Licitador
  }
  Utilizador --> Perfil
}

package "Armazéns e localizações" {
  class Armazem {
    +id : int
    +nome : string
    +descricao : string
    +estado : EstadoArmazem
    +podeReceberOperacoes() : bool
  }
  enum EstadoArmazem {
    Ativo
    Inativo
  }
  class Localizacao {
    +id : int
    +codigo : string
    +tipo : string
    +ativa : bool
    +podeReceberStock() : bool
    +obterAdjacentes() : Localizacao[]
  }
  class LigacaoLocalizacao {
    +validarMesmoArmazem() : bool
  }
  Armazem "1" *-- "0..*" Localizacao
  Armazem --> EstadoArmazem
  LigacaoLocalizacao "0..*" --> "1" Localizacao : extremo A
  LigacaoLocalizacao "0..*" --> "1" Localizacao : extremo B
}

package "Artigos e stock" {
  class Artigo {
    +id : int
    +nome : string
    +descricao : string
    +unidadeMedida : UnidadeMedida
    +nivelMinimoStock : decimal
    +ativo : bool
    +obterQuantidadeTotal() : decimal
    +temStockBaixo() : bool
  }
  enum UnidadeMedida {
    Unidade
    Caixa
    Quilograma
    Litro
  }
  class Lote {
    +id : int
    +referencia : string
    +validade : date?
    +estaProximoDaValidade() : bool
  }
  class StockLocalizacao {
    +id : int
    +quantidade : decimal
    +registarEntrada(qtd : decimal) : void
    +registarSaida(qtd : decimal) : void
    +registarAjuste(novaQtd : decimal) : void
  }
  class OperacaoStock {
    +id : int
    +tipo : TipoOperacao
    +quantidadeAnterior : decimal
    +quantidadeNova : decimal
    +justificacao : string?
    +data : datetime
  }
  enum TipoOperacao {
    Entrada
    Saida
    Ajuste
  }
  Artigo "1" *-- "0..*" Lote
  Artigo --> UnidadeMedida
  Lote "1" -- "0..*" StockLocalizacao
  Localizacao "1" -- "0..*" StockLocalizacao
  StockLocalizacao "1" -- "0..*" OperacaoStock : histórico
  OperacaoStock --> TipoOperacao
  Utilizador "1" -- "0..*" OperacaoStock : responsável
}

package "Transferências" {
  class Transferencia {
    +id : int
    +quantidade : decimal
    +estado : EstadoTransferencia
    +dataPlaneamento : datetime
    +dataConfirmacao : datetime?
    +planear() : void
    +confirmar() : void
  }
  enum EstadoTransferencia {
    Pendente
    Confirmada
  }
  Transferencia --> EstadoTransferencia
  Transferencia "0..*" --> "1" Lote
  Transferencia "0..*" --> "1" Localizacao : origem
  Transferencia "0..*" --> "1" Localizacao : destino
  Transferencia "0..*" --> "1" Utilizador : planeada por
  Transferencia "0..*" --> "0..1" Utilizador : confirmada por
}

package "Licitações" {
  class Licitacao {
    +id : int
    +estado : EstadoLicitacao
    +prazoSubmissao : datetime
    +dataEncerramento : datetime?
    +justificacaoEncerramento : string?
    +aceitaPropostas(agora : datetime) : bool
    +encerrar(propostaSelecionada : Proposta?) : void
  }
  enum EstadoLicitacao {
    Aberto
    Encerrado
  }
  class ArtigoLicitacao {
    +quantidadePretendida : decimal
  }
  class Proposta {
    +id : int
    +valorTotal : decimal
    +prazoEntregaDias : int
    +dataSubmissao : datetime
    +selecionada : bool
  }
  Licitacao --> EstadoLicitacao
  Armazem "1" -- "0..*" Licitacao
  Licitacao "1" *-- "1..*" ArtigoLicitacao
  Artigo "1" -- "0..*" ArtigoLicitacao
  Licitacao "1" *-- "0..*" Proposta
  Licitacao "0..*" --> "1" Utilizador : criada por
  Licitacao "0..*" --> "0..1" Utilizador : encerrada por
  Proposta "0..*" --> "1" Utilizador : submetida por
}

note right of Localizacao
  A adequação física do espaço é avaliada
  pelo utilizador. Não há cálculo automático
  de capacidade neste modelo.
end note

note bottom of Transferencia
  O Gestor de Stock planeia; o Operador confirma
  após o movimento físico. A confirmação atualiza
  origem e destino sem alterar o total do armazém.
end note

note bottom of Licitacao
  Recolhe propostas para eventual fornecimento de artigos.
  A seleção é opcional e só ocorre no encerramento.
  Não executa compra, contrato nem faturação.
end note
@enduml
```

## Restrições do modelo

- O perfil define permissões: Administrador gere utilizadores, armazéns, localizações e artigos; Gestor de Stock gere alocações, transferências planeadas e licitações; Operador regista movimentos e confirma transferências; Licitador consulta licitações abertas e submete as suas propostas. Um utilizador inativo não pode autenticar.
- Uma ligação une duas localizações distintas do mesmo armazém, sem duplicados; a adjacência é bidirecional.
- Cada registo de stock corresponde a um lote numa localização. Quantidades não podem ser negativas; saídas e confirmações de transferência exigem quantidade disponível. Quantidades fracionárias só são aceites em quilogramas e litros.
- A transferência usa localizações ativas do mesmo armazém, tem quantidade positiva e passa de Pendente a Confirmada. O planeamento e a confirmação registam utilizadores e datas distintos.
- Uma licitação tem armazém, prazo e pelo menos um artigo com quantidade pretendida positiva. Recebe propostas apenas enquanto está aberta e dentro do prazo. Cada proposta regista licitador, valor total, prazo de entrega e data de submissão. Ao encerrar, pode selecionar no máximo uma das suas propostas; guarda responsável, data e justificação da decisão. O encerramento não altera stock.
