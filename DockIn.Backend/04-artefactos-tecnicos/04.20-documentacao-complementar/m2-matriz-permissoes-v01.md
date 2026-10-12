## Matriz de Permissões e Acessos — Sistema DockIn

O modelo de controlo de acessos do sistema DockIn baseia-se nos perfis Administrador, Gestor de Stock, Operador e Licitador. As permissões funcionais e as plataformas autorizadas seguem a Proposta de Sistema v04, a Declaração de Âmbito v04 e a Especificação de Requisitos v02.

A matriz utiliza os níveis C/L/A/I/E definidos abaixo. As permissões devem ser interpretadas em conjunto com as regras de autenticação, as restrições de dados e a plataforma autorizada para cada perfil.

### 1. Níveis de Acesso (Legenda)

* **C (Criar):** Permissão para registar, planear ou adicionar nova informação. Em F006, inclui registar a alocação; em F012–F014, inclui registar e efetivar o movimento de stock correspondente.
* **L (Ler):** Permissão para consultar, visualizar e pesquisar os dados abrangidos pela funcionalidade e pelo âmbito de acesso do perfil.
* **A (Atualizar):** Permissão para editar os dados cuja alteração esteja prevista na funcionalidade. Em F001, inclui atribuir perfis e ativar contas; nas funcionalidades de gestão de estado, inclui a alteração para ativo quando aplicável.
* **I (Inativar/Desativar):** Permissão para alterar o estado para inativo, preservando o histórico associado.
* **E (Executar):** Permissão para confirmar a execução de uma transferência planeada ou encerrar uma licitação, incluindo o registo do respetivo resultado.
* **—:** Sem permissão para a operação funcional indicada na linha. Não impede a autenticação nas plataformas autorizadas ao perfil.

As letras representam permissões funcionais do utilizador. A atualização automática do stock ou a conservação automática de histórico pelo sistema não exigem, por si só, a atribuição de A ou de permissões adicionais ao utilizador.

---

### 2. Matriz de Autorizações Funcionais

| Código | Funcionalidade | Administrador | Gestor de Stock | Operador | Licitador | Plataforma(s) Permitida(s) |
| :--- | :--- | :--- | :--- | :--- | :--- | :--- |
| **Configuração e Estrutura** | | | | | | |
| F001 | Gestão de contas e perfis de acesso | **C, L, A, I** | — | — | — | Web — Administrador |
| F002 | Gestão de armazéns | **C, L, A, I** | — | — | — | Web — Administrador |
| F003 | Organização das localizações e ligações | **C, L, A, I**  | — | — | — | Web — Administrador |
| F004 | Gestão do catálogo de artigos | **C, L, A, I** | — | — | — | Web — Administrador |
| **Logística e Gestão de Stock** | | | | | | |
| F005 | Consulta de existências e localizações adjacentes | — | **L** | — | — | Web e Mobile — Gestor de Stock |
| F006 | Alocação de stock a localizações | — | **C** | — | — | Web — Gestor de Stock |
| F007 | Planeamento e acompanhamento de transferências internas | — | **C, L** | — | — | Web — Gestor de Stock |
| F008 | Consulta e confirmação de transferências internas | — | — | **L, E** | — | Mobile — Operador |
| F009 | Consulta de lotes e alertas de validade | — | **L** | — | — | Web e Mobile — Gestor de Stock |
| F010 | Consulta de alertas de stock baixo | — | **L** | — | — | Web e Mobile — Gestor de Stock |
| F011 | Consulta de artigos e existências | — | **L** | **L** | — | Web — Gestor de Stock; Mobile — Operador |
| F012 | Registo de entradas de stock e consulta dos próprios registos | — | — | **C, L**  | — | Mobile — Operador |
| F013 | Registo de saídas de stock e consulta dos próprios registos | — | — | **C, L** | — | Mobile — Operador |
| F014 | Registo de ajustes de inventário e consulta dos próprios registos | — | — | **C, L**| — | Mobile — Operador |
| **Processos de Licitação** | | | | | | |
| F015 | Criação de licitações | — | **C** | — | — | Web — Gestor de Stock |
| F016 | Consulta de licitações abertas | — | — | — | **L** | Web — Licitador |
| F017 | Apresentação e consulta de propostas | — | **L** | — | **C, L**  | Web — Gestor de Stock e Licitador |
| F018 | Encerramento de licitações | — | **E** | — | — | Web — Gestor de Stock |
| F019 | Consulta de histórico de licitações | — | **L**| — | **L**  | Web — Gestor de Stock e Licitador |

#### 2.1. Autenticação e acesso — componente transversal de F001

A gestão de contas e perfis apresentada na matriz é exclusiva do Administrador. A autenticação e o acesso às funcionalidades autorizadas também pertencem a F001 e aplicam-se aos quatro perfis:

| Perfil | Autenticação na Web | Autenticação no Mobile |
| :--- | :---: | :---: |
| Administrador | Permitida | Não permitida |
| Gestor de Stock | Permitida | Permitida |
| Operador | Não permitida | Permitida |
| Licitador | Permitida | Não permitida |

O acesso às funcionalidades depende de autenticação válida, conta ativa, perfil autorizado e plataforma permitida. Uma conta desativada não pode aceder ao sistema. A autenticação numa plataforma não concede acesso a todas as funcionalidades dessa plataforma.

#### 2.2. Restrições e interpretação das permissões

* **F002, F003 e F006:** um armazém inativo mantém o histórico, mas não admite novas operações de organização ou alocação. A alocação exige uma localização ativa pertencente ao armazém correspondente. Em F003, I aplica-se ao estado das localizações, não à inativação de ligações.
* **F007 e F008:** o Gestor planeia e acompanha a transferência na Web; o Operador consulta as transferências planeadas e confirma a execução no Mobile. A confirmação atualiza as existências de origem e destino, conserva os dados da operação e não altera o total de stock do armazém.
* **F012–F014:** C permite criar o registo e efetivar a entrada, saída ou ajuste, respeitando as validações de cada operação. Não concede permissão para editar ou eliminar movimentos já registados. Para explicitar a consulta posterior prevista nos requisitos de histórico, esta matriz adota L limitado aos registos efetuados pelo próprio Operador. Este detalhe de acesso concretiza os RF-F012-02, RF-F013-02 e RF-F014-02; não atribui consulta geral do histórico de movimentos a outros perfis.
* **F015:** a criação associa a licitação a um armazém, inclui pelo menos um artigo, as quantidades e um prazo válido, e coloca a licitação no estado Aberto.
* **F016 e F017:** o Licitador consulta a informação disponibilizada nas licitações abertas. Só pode apresentar propostas em licitações abertas e dentro do prazo, ficando cada proposta associada ao próprio. A consulta pelo Gestor abrange as propostas recebidas nas licitações que acompanha.
* **F018:** E inclui selecionar manualmente uma proposta ou encerrar sem seleção, registando resultado, justificação, responsável e data. O encerramento impede novas propostas e não altera o stock. A consulta das propostas necessárias à decisão é abrangida por F017.
* **F019:** o Licitador consulta o histórico das próprias participações e os resultados das licitações em que participou, incluindo se a sua proposta foi selecionada. Não tem acesso à informação reservada das propostas de outros Licitadores.
* A conservação automática do histórico não concede acesso indiscriminado aos registos. As permissões de leitura mantêm os limites indicados na matriz.

---

### 3. Diretrizes de Fronteira do Sistema

* **Administrador:** utiliza exclusivamente a Web para gerir contas, perfis, armazéns, localizações, ligações e catálogo de artigos. Este perfil não recebe permissões para operações logísticas diárias ou processos de licitação.
* **Gestor de Stock:** utiliza a Web para consultar existências, alocar stock, planear e acompanhar transferências e gerir os processos de licitação previstos. No Mobile, tem acesso apenas às consultas F005, F009 e F010, além da autenticação. Não regista entradas, saídas ou ajustes, nem confirma a execução de transferências.
* **Operador:** utiliza exclusivamente o Mobile para consultar artigos e existências, consultar e confirmar transferências planeadas e registar entradas, saídas e ajustes de inventário. Pode consultar os próprios registos de entradas, saídas e ajustes nos termos da secção 2.2. Não tem acesso à interface Web.
* **Licitador:** utiliza exclusivamente a área de licitações na Web. Pode consultar a informação disponibilizada nas licitações abertas, apresentar e consultar as próprias propostas e consultar o histórico e os resultados das suas participações. O sistema deverá impedir o acesso à informação reservada das propostas de terceiros e às funcionalidades internas de gestão de armazéns e stock. Esta restrição não impede a consulta dos artigos, quantidades e demais informação disponibilizada na própria licitação.

As permissões aqui definidas não incluem compras, encomendas, contratos, faturação, pagamentos ou receção comercial de mercadoria. A seleção de uma proposta constitui o ponto final da licitação; uma eventual entrada posterior de artigos é registada autonomamente através de F012.
