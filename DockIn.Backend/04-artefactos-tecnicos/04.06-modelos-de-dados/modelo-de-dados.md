# Modelo de Dados — DockIn

---

## 1. Diagrama Entidade-Relação

```mermaid
erDiagram

    UTILIZADOR {
        int id PK
        string nome
        string email
        string password_hash
        enum perfil "Administrador | GestorStock | Operador | Licitador"
        bool ativo
    }

    ARMAZEM {
        int id PK
        string nome
        string descricao
        enum estado "Ativo | Inativo"
    }

    LOCALIZACAO {
        int id PK
        int armazem_id FK
        string codigo
        enum tipo
        enum estado "Ativa | Inativa"
    }

    LIGACAO_LOCALIZACAO {
        int localizacao_a_id FK
        int localizacao_b_id FK
    }

    ARTIGO {
        int id PK
        string nome
        string descricao
        enum unidade_medida "Unidade | Caixa | Quilograma | Litro"
        decimal nivel_minimo_stock
        bool ativo
    }

    LOTE {
        int id PK
        int artigo_id FK
        string referencia
        date validade
    }

    STOCK_LOCALIZACAO {
        int id PK
        int localizacao_id FK
        int lote_id FK
        decimal quantidade
    }

    OPERACAO_STOCK {
        int id PK
        int utilizador_id FK
        int stock_localizacao_id FK
        enum tipo "Entrada | Saida | Ajuste"
        decimal quantidade_anterior
        decimal quantidade_nova
        string justificacao
        datetime data
    }

    TRANSFERENCIA {
        int id PK
        int artigo_id FK
        int lote_id FK
        decimal quantidade
        int localizacao_origem_id FK
        int localizacao_destino_id FK
        enum estado "Pendente | Confirmada"
        int planeada_por_id FK
        int confirmada_por_id FK
        datetime data_planeamento
        datetime data_confirmacao
    }

    LICITACAO {
        int id PK
        int armazem_id FK
        int criada_por_id FK
        enum estado "Aberto | Encerrado"
        datetime prazo_submissao
        int encerrada_por_id FK
        datetime data_encerramento
        string justificacao_encerramento
    }

    ARTIGO_LICITACAO {
        int licitacao_id FK
        int artigo_id FK
        decimal quantidade_pretendida
    }

    PROPOSTA {
        int id PK
        int licitacao_id FK
        int licitador_id FK
        decimal valor_total
        int prazo_entrega_dias
        datetime data_submissao
        bool selecionada
    }

    %% Relações
    ARMAZEM         ||--o{ LOCALIZACAO          : "contém"
    LOCALIZACAO     ||--o{ LIGACAO_LOCALIZACAO  : "liga-se"
    LOCALIZACAO     ||--o{ STOCK_LOCALIZACAO    : "armazena"
    ARTIGO          ||--o{ LOTE                 : "tem lotes"
    LOTE            ||--o{ STOCK_LOCALIZACAO    : "encontra-se em"
    UTILIZADOR      ||--o{ OPERACAO_STOCK       : "realiza"
    STOCK_LOCALIZACAO ||--o{ OPERACAO_STOCK     : "é afetado por"
    ARTIGO          ||--o{ TRANSFERENCIA        : "move"
    LOTE            ||--o{ TRANSFERENCIA        : "identifica"
    LOCALIZACAO     ||--o{ TRANSFERENCIA        : "é origem de"
    UTILIZADOR      ||--o{ TRANSFERENCIA        : "planeia/confirma"
    ARMAZEM         ||--o{ LICITACAO            : "associada a"
    UTILIZADOR      ||--o{ LICITACAO            : "cria/encerra"
    LICITACAO       ||--o{ ARTIGO_LICITACAO     : "inclui"
    ARTIGO          ||--o{ ARTIGO_LICITACAO     : "listado em"
    LICITACAO       ||--o{ PROPOSTA             : "recebe"
    UTILIZADOR      ||--o{ PROPOSTA             : "submete"
```

---

## 2. Descrição das Entidades

### `UTILIZADOR`
Representa qualquer utilizador do sistema. O campo `perfil` determina o que pode fazer (RBAC). Um utilizador **inativo** não consegue autenticar.

### `ARMAZEM`
Entidade raiz da hierarquia física. Um armazém **inativo** mantém o histórico mas não aceita novas operações.

### `LOCALIZACAO`
Espaço físico dentro de um armazém (prateleira, zona, etc.). Uma localização **inativa** não pode receber alocações ou transferências.

### `LIGACAO_LOCALIZACAO`
Relação bidirecional entre duas localizações **do mesmo armazém**. Permite calcular adjacências (grafo não dirigido). Não podem existir ligações duplicadas nem entre armazéns diferentes.

### `ARTIGO`
Produto gerido pelo sistema. Tem uma única `unidade_medida` que define a precisão das quantidades. A desativação não apaga histórico.

### `LOTE`
Identifica um conjunto de unidades de um artigo com as mesmas características (referência, validade). A `validade` é opcional (depende do artigo).

### `STOCK_LOCALIZACAO`
Registo da quantidade de um lote numa localização. Uma localização pode ter vários artigos/lotes. A unidade de medida é sempre a do artigo.

### `OPERACAO_STOCK`
Histórico auditável de todas as entradas, saídas e ajustes. Conserva sempre o responsável e a data. No ajuste, guarda também o valor anterior e a justificação.

### `TRANSFERENCIA`
Movimentação planeada de stock entre localizações. Tem dois momentos distintos:
- **Planeamento** (web, Gestor de Stock)
- **Confirmação** (mobile, Operador)

### `LICITACAO`
Processo de recolha de propostas para fornecimento. Cycle: `Aberto → Encerrado`. Fora do âmbito: compras, contratos, faturação.

### `ARTIGO_LICITACAO`
Detalha os artigos e quantidades pretendidas numa licitação. Uma licitação deve ter pelo menos um artigo.

### `PROPOSTA`
Proposta de um Licitador a uma licitação aberta e dentro do prazo. Conserva valor total e prazo de entrega. Só uma pode ficar marcada como `selecionada`.

---

## 3. Regras de Negócio Críticas

| # | Regra | Onde se aplica |
|---|---|---|
| RN-01 | Quantidade de stock não pode ser negativa | Saída, Ajuste |
| RN-02 | Saída não pode exceder quantidade disponível | `OPERACAO_STOCK` (Saída) |
| RN-03 | Ligações entre localizações de armazéns diferentes são proibidas | `LIGACAO_LOCALIZACAO` |
| RN-04 | Ligações duplicadas são proibidas | `LIGACAO_LOCALIZACAO` |
| RN-05 | Transferência só entre localizações do mesmo armazém | `TRANSFERENCIA` |
| RN-06 | Proposta só pode ser submetida dentro do prazo | `PROPOSTA` |
| RN-07 | Após encerramento, licitação não aceita novas propostas | `LICITACAO` |
| RN-08 | Só uma proposta pode ser selecionada por licitação | `PROPOSTA` |
| RN-09 | Licitação deve ter pelo menos um artigo para ser criada | `ARTIGO_LICITACAO` |
| RN-10 | Quantidade fracionária só é permitida em Kg e Litro | `STOCK_LOCALIZACAO`, `OPERACAO_STOCK` |
| RN-11 | Utilizador inativo não pode autenticar | `UTILIZADOR` |
| RN-12 | Armazém/localização inativos não aceitam novas operações | `ARMAZEM`, `LOCALIZACAO` |

---

## 4. Decisões de Modelação

| Decisão | Justificação |
|---|---|
| `LIGACAO_LOCALIZACAO` é simétrica (sem direção) | O enunciado não menciona direção nas ligações; adjacência é bidirecional |
| `STOCK_LOCALIZACAO` é linha por (localização, lote) | Permite múltiplos lotes e artigos por localização |
| `OPERACAO_STOCK` guarda `quantidade_anterior` e `quantidade_nova` | Exigência explícita de auditabilidade dos ajustes |
| `TRANSFERENCIA` tem dois `FK` para `UTILIZADOR` | Planeamento e confirmação são por atores e momentos distintos |
| `PROPOSTA.selecionada` é boolean | Simples e direto; a regra de unicidade aplica-se na lógica de negócio |
