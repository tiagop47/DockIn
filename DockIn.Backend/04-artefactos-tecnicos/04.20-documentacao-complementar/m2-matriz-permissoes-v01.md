# Matriz de Permissões — DockIn

**Artefacto:** M2, versão 01  
**Grupo:** G02

Esta matriz relaciona os quatro perfis com operações e dados. `C` significa consultar, `G` gerir ou alterar, `R` registar, `P` planear, `F` confirmar e `—` sem permissão. As permissões são verificadas pela aplicação em cada pedido, incluindo o acesso a um registo concreto.

| Operação / dados | Administrador | Gestor de Stock | Operador | Licitador |
|---|---|---|---|---|
| Autenticar-se e consultar os próprios dados | C | C | C | C |
| Criar, alterar, desativar utilizadores e atribuir perfis | G | — | — | — |
| Gerir armazéns, localizações e ligações | G | — | — | — |
| Consultar localizações, adjacências e ocupação | C | C | C (dados necessários no mobile) | — |
| Gerir catálogo de artigos | G | — | — | — |
| Consultar artigos, lotes e existências | C | C | C | — |
| Alocar stock a localização | — | G | — | — |
| Registar entrada, saída e ajuste de inventário | — | — | R | — |
| Consultar histórico de operações de stock | — | C | C (operações necessárias no mobile) | — |
| Planear transferência interna | — | P | — | — |
| Consultar transferências pendentes | — | C | C (para confirmação) | — |
| Confirmar transferência após movimento físico | — | — | F | — |
| Consultar alertas de validade e stock baixo | — | C | C (dados disponíveis no mobile) | — |
| Criar licitação com artigos, quantidades e prazo | — | G | — | — |
| Consultar licitações abertas | — | C | — | C |
| Consultar histórico de licitações | — | C | — | C (apenas processos em que submeteu proposta) |
| Submeter proposta a licitação aberta dentro do prazo | — | — | — | R |
| Consultar propostas recebidas de uma licitação | — | C | — | C (apenas as próprias) |
| Consultar dados de propostas de outros Licitadores | — | C | — | — |
| Alterar ou eliminar proposta submetida | — | — | — | — |
| Selecionar proposta e encerrar licitação | — | G | — | — |
| Encerrar licitação sem selecionar proposta | — | G | — | — |

## Regras de acesso aos dados

1. Um utilizador inativo não pode autenticar-se nem executar operações. A sessão ou pedido identifica utilizador e perfil; a autorização é revalidada no servidor.
2. O Licitador pode consultar as licitações abertas necessárias para preparar uma proposta. Os dados comerciais e a identidade das propostas dos outros Licitadores não lhe são apresentados.
3. O Licitador consulta apenas as suas propostas e os processos a que estão associadas após o encerramento. A consulta do resultado apresenta a sua situação sem divulgar os dados das propostas alheias.
4. O Gestor de Stock consulta todas as propostas recebidas para os processos sob gestão e regista a decisão. A seleção de uma proposta é opcional e só é registada no encerramento; nunca pode haver mais de uma selecionada por licitação.
5. O Operador consulta apenas os dados necessários às tarefas mobile. O acesso parcial indicado nos casos de uso não concede gestão de armazéns, catálogo, licitações ou propostas.
6. A ausência de permissão nesta matriz significa recusa da operação, mesmo que o cliente apresente um botão ou envie diretamente um pedido à API.

## Relação com os artefactos

Esta matriz detalha os [casos de uso](../04.04-casos-de-uso/casos-de-uso.md) e a decisão de autorização da [arquitetura](../04.05-arquitetura/m2-documentacao-arquitetura-v01.md). Os contratos concretos da API devem aplicar os mesmos limites de perfil e de propriedade dos dados.
