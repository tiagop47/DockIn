# Casos de Uso — DockIn


## 2. Diagrama de Casos de Uso por Domínio

### 2.1 Autenticação e Controlo de Acesso

```mermaid
flowchart LR
    ADM(["Administrador"])
    GS(["Gestor de Stock"])
    OP(["Operador"])
    LIC(["Licitador"])

    AU01["UC-AU-01\nAutenticar no sistema"]
    AU02["UC-AU-02\nGerir contas de utilizador"]
    AU03["UC-AU-03\nAtribuir perfis de acesso"]

    ADM --> AU01
    GS  --> AU01
    OP  --> AU01
    LIC --> AU01

    ADM --> AU02
    ADM --> AU03
```

---

### 2.2 Gestão de Armazéns e Localizações

```mermaid
flowchart LR
    ADM(["Administrador"])
    GS(["Gestor de Stock"])
    OP(["Operador\n(mobile)"])

    AR01["UC-AR-01\nGerir armazéns"]
    AR02["UC-AR-02\nGerir localizações"]
    AR03["UC-AR-03\nGerir ligações entre localizações"]
    AR04["UC-AR-04\nConsultar ocupação e espaços disponíveis"]
    AR05["UC-AR-05\nConsultar localizações adjacentes"]

    ADM --> AR01
    ADM --> AR02
    ADM --> AR03

    GS  --> AR04
    GS  --> AR05

    OP  -.->|"acesso parcial\n(mobile)"| AR04
    OP  -.->|"acesso parcial\n(mobile)"| AR05
```

---

### 2.3 Artigos e Gestão de Stock

```mermaid
flowchart LR
    ADM(["Administrador"])
    GS(["Gestor de Stock"])
    OP(["Operador\n(mobile)"])

    ST01["UC-ST-01\nGerir catálogo de artigos"]
    ST02["UC-ST-02\nAlocar stock a localização"]
    ST03["UC-ST-03\nConsultar artigos e existências"]
    ST04["⬆️ UC-ST-04\nRegistar entrada de stock"]
    ST05["⬇️ UC-ST-05\nRegistar saída de stock"]
    ST06["UC-ST-06\nRegistar ajuste de inventário"]

    ADM --> ST01
    GS  --> ST02
    GS  --> ST03
    OP  --> ST03
    OP  --> ST04
    OP  --> ST05
    OP  --> ST06
```

---

### 2.4 Transferências Internas

```mermaid
flowchart LR
    GS(["Gestor de Stock\n(web)"])
    OP(["Operador\n(mobile)"])

    TR01["UC-TR-01\nPlanear transferência interna"]
    TR02["UC-TR-02\nConfirmar transferência após\nmovimento físico"]

    GS --> TR01
    OP --> TR02
    TR01 -. "gera transferência\npendente" .-> TR02
```

---

### 2.5 Alertas e Monitorização

```mermaid
flowchart LR
    SIS(["Sistema"])
    GS(["Gestor de Stock"])
    OP(["Operador\n(mobile)"])

    AL01["UC-AL-01\nSinalizar lotes próximos\nda validade"]
    AL02["UC-AL-02\nConsultar alertas de validade"]
    AL03["UC-AL-03\nSinalizar stock baixo"]
    AL04["UC-AL-04\nConsultar alertas de stock baixo"]

    SIS --> AL01
    SIS --> AL03

    GS  --> AL02
    GS  --> AL04

    OP  -.->|"acesso parcial\n(mobile)"| AL02
    OP  -.->|"acesso parcial\n(mobile)"| AL04
```

---

### 2.6 Licitações

```mermaid
flowchart LR
    GS(["Gestor de Stock"])
    LIC(["Licitador"])

    LC01["UC-LC-01\nCriar processo de licitação"]
    LC02["UC-LC-02\nConsultar licitações abertas"]
    LC03["UC-LC-03\nSubmeter proposta"]
    LC04["UC-LC-04\nConsultar propostas recebidas"]
    LC05["UC-LC-05\nEncerrar licitação e\nregistar decisão"]
    LC06["UC-LC-06\nConsultar histórico de licitações"]

    GS  --> LC01
    GS  -.->|"acesso parcial"| LC02
    GS  --> LC04
    GS  --> LC05
    GS  --> LC06

    LIC --> LC02
    LIC --> LC03
    LIC -.->|"só as próprias"| LC04
    LIC -.->|"só as próprias"| LC06
```

---

## 3. Fluxos Principais (Descrição Textual)

### UC-TR-01/02 — Transferência Interna (fluxo completo)

| Passo | Quem | O quê | Interface |
|---|---|---|---|
| 1 | Gestor de Stock | Identifica necessidade de mover stock | Web |
| 2 | Gestor de Stock | Cria transferência (artigo, lote, qtd, origem, destino) | Web |
| 3 | Sistema | Regista transferência como **Pendente** | — |
| 4 | Operador | Realiza movimento físico no armazém | Campo |
| 5 | Operador | Confirma transferência na app | Mobile |
| 6 | Sistema | Atualiza existências (−origem / +destino) | — |

**Exceções:**
- Origem ou destino inativa → transferência não permitida
- Quantidade insuficiente na origem → não confirmável

---

### UC-LC-01/03/05 — Licitação (fluxo completo)

| Passo | Quem | O quê |
|---|---|---|
| 1 | Gestor de Stock | Cria licitação (armazém, artigos, qtds, prazo) |
| 2 | Sistema | Licitação fica no estado **Aberto** |
| 3 | Licitador | Consulta licitações abertas |
| 4 | Licitador | Submete proposta (valor total, prazo entrega) |
| 5 | Gestor de Stock | Consulta propostas recebidas |
| 6 | Gestor de Stock | Encerra: seleciona proposta **ou** encerra sem seleção |
| 7 | Sistema | Licitação passa a **Encerrado** |

**Exceções:**
- Submeter proposta após prazo → não permitido
- Encerrar licitação sem nenhuma proposta → permitido (sem seleção)

---

### UC-ST-04/05/06 — Operações de Stock pelo Operador

| Caso de Uso | Entrada | Validação | Resultado |
|---|---|---|---|
| **Entrada** | Artigo, lote, qtd, localização, validade (se aplic.) | qtd > 0; lote com info obrigatória | Existências aumentam |
| **Saída** | Artigo, lote, qtd, localização | qtd ≤ disponível | Existências diminuem |
| **Ajuste** | Artigo, lote, localização, nova qtd, justificação | nova qtd ≥ 0 | Existências corrigidas; histórico preservado |
