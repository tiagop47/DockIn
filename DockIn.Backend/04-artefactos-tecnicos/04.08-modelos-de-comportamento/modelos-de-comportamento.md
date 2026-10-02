# Modelos de Comportamento — DockIn

---

## 1. Ciclo de Vida de uma Transferência Interna

```mermaid
stateDiagram-v2
    [*] --> Pendente : Gestor de Stock cria transferência\n(artigo, lote, qtd, origem, destino)

    Pendente --> Confirmada : Operador confirma após\nmovimento físico (mobile)

    Confirmada --> [*] : Existências atualizadas\n(−origem / +destino)

    note right of Pendente
        A quantidade total no armazém
        não se altera durante este estado.
        A movimentação física é da
        responsabilidade do Operador.
    end note

    note right of Confirmada
        Conserva: artigo, lote,
        quantidade, responsável e
        data da operação.
    end note
```

---

## 2. Ciclo de Vida de uma Licitação

```mermaid
stateDiagram-v2
    [*] --> Aberto : Gestor de Stock cria licitação\n(armazém, artigos, qtds, prazo)

    Aberto --> Aberto : Licitador submete proposta\n[dentro do prazo]

    Aberto --> Encerrado_ComSelecao : Gestor de Stock seleciona\numa proposta e encerra

    Aberto --> Encerrado_SemSelecao : Gestor de Stock encerra\nsem selecionar proposta

    Encerrado_ComSelecao --> [*] : Resultado conservado\n(proposta, justificação,\nresponsável, data)

    Encerrado_SemSelecao --> [*] : Resultado conservado\n(responsável e data)

    note right of Aberto
        Não são aceites propostas
        após o prazo definido.
    end note

    note right of Encerrado_ComSelecao
        A seleção NÃO altera
        as existências de stock.
        Compras/contratos estão
        fora do âmbito.
    end note
```

---

## 3. Ciclo de Vida de uma Proposta

```mermaid
stateDiagram-v2
    [*] --> Submetida : Licitador submete proposta\n[licitação Aberta e dentro do prazo]

    Submetida --> Selecionada : Gestor de Stock seleciona\nesta proposta ao encerrar

    Submetida --> NaoSelecionada : Gestor de Stock encerra\nsem selecionar esta proposta

    Selecionada --> [*]
    NaoSelecionada --> [*]

    note right of Submetida
        Conserva: valor total proposto,
        prazo de entrega,
        data de apresentação.
    end note
```

---

## 4. Fluxo de Operações de Stock (Operador)

```mermaid
stateDiagram-v2
    [*] --> ConsultaArtigo : Operador pesquisa artigo

    ConsultaArtigo --> RegistaEntrada : Recebe mercadoria
    ConsultaArtigo --> RegistaSaida : Retira do armazém
    ConsultaArtigo --> RegistaAjuste : Verifica discrepância física

    RegistaEntrada --> ValidaEntrada : Sistema valida
    ValidaEntrada --> ExistenciasAtualizadas : [qtd > 0 e info obrigatória presente]
    ValidaEntrada --> Erro : [qtd = 0 ou negativa]

    RegistaSaida --> ValidaSaida : Sistema valida
    ValidaSaida --> ExistenciasAtualizadas : [qtd ≤ disponível]
    ValidaSaida --> Erro : [qtd > disponível]

    RegistaAjuste --> ValidaAjuste : Sistema valida
    ValidaAjuste --> ExistenciasAtualizadas : [nova qtd ≥ 0]
    ValidaAjuste --> Erro : [nova qtd negativa]

    ExistenciasAtualizadas --> [*] : Operação conservada\n(responsável + data)
    Erro --> [*] : Operação recusada
```

---

## 5. Ciclo de Vida de um Alerta

```mermaid
stateDiagram-v2
    [*] --> Inativo : Condição não verificada

    Inativo --> Ativo_StockBaixo : Sistema deteta\nqtd total < nível mínimo do artigo

    Inativo --> Ativo_ValidadeProxima : Sistema deteta\nvalidade dentro do período de antecedência

    Ativo_StockBaixo --> Inativo : Quantidade total\nsobe acima do mínimo

    Ativo_ValidadeProxima --> [*] : Lote expirado ou\nstock esgotado

    note right of Ativo_StockBaixo
        Consultável pelo Gestor de Stock (web)
        e parcialmente pelo Operador (mobile).
    end note

    note right of Ativo_ValidadeProxima
        O período de antecedência é
        predefinido no sistema.
        Consultável pelo Gestor de Stock (web)
        e pelo Operador (mobile).
    end note
```

---

## 6. Diagrama de Atividade — Processo Completo de Licitação

```mermaid
flowchart TD
    A([Início]) --> B["Gestor de Stock cria licitação\n(armazém, artigos, qtds, prazo)"]
    B --> C{Tem pelo menos\num artigo e prazo válido?}
    C -- Não --> B
    C -- Sim --> D["Licitação criada\nEstado: Aberto"]
    D --> E["Licitador consulta\nlicitações abertas"]
    E --> F{Dentro do prazo?}
    F -- Não --> G([Fim — sem proposta])
    F -- Sim --> H["Licitador submete proposta\n(valor total + prazo entrega)"]
    H --> I["Proposta registada"]
    I --> J{Gestor decide\nencerrar?}
    J -- Não --> E
    J -- Sim --> K{Existem\npropostas?}
    K -- Sim --> L["Gestor seleciona proposta\nou encerra sem seleção"]
    K -- Não --> M["Gestor encerra\nsem seleção"]
    L --> N["Licitação encerrada\nEstado: Encerrado"]
    M --> N
    N --> O([Fim])
```

---

## 7. Diagrama de Atividade — Transferência Interna

```mermaid
flowchart TD
    A([Início]) --> B["Gestor de Stock identifica\nnecessidade de mover stock"]
    B --> C["Cria transferência:\nartigo, lote, qtd, origem, destino"]
    C --> D{Origem e destino\nativos e no mesmo armazém?}
    D -- Não --> E([Erro — transferência recusada])
    D -- Sim --> F["Transferência registada\nEstado: Pendente"]
    F --> G["Operador realiza\nmovimento físico no armazém"]
    G --> H["Operador confirma na\napp mobile"]
    H --> I["Sistema atualiza existências:\n−origem / +destino"]
    I --> J["Transferência registada\nEstado: Confirmada"]
    J --> K([Fim])
```
