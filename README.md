# Good Hamburger

API REST desenvolvida para o desafio técnico da **Good Hamburger**, com o objetivo de registrar pedidos de uma lanchonete, calcular descontos promocionais e expor o cardápio disponível.

O projeto foi construído com foco em organização de código, separação de responsabilidades, regras de negócio bem isoladas e facilidade de evolução.

---

## Tecnologias

- .NET 8
- ASP.NET Core Web API
- Entity Framework Core InMemory
- xUnit
- FluentAssertions
- Swagger / OpenAPI

---

## Estrutura da solução

```txt
GoodHamburger/
├── src/
│   ├── GoodHamburger.Api/
│   ├── GoodHamburger.Application/
│   ├── GoodHamburger.Domain/
│   └── GoodHamburger.Infrastructure/
│
├── tests/
│   └── GoodHamburger.UnitTests/
│
├── GoodHamburger.sln
├── LICENSE
└── README.md
```

---

## Arquitetura

A solução segue uma abordagem inspirada em **Clean Architecture**, separando o projeto em camadas com responsabilidades bem definidas.

### Domain

Contém o núcleo da aplicação:

- Entidades de domínio
- Regras de negócio
- Validações principais
- Cálculo de subtotal, desconto e total
- Estratégias de desconto

A entidade `Order` concentra as regras relacionadas ao pedido, evitando que regras de negócio fiquem espalhadas em controllers ou services.

### Application

Responsável por orquestrar os casos de uso da aplicação.

Nessa camada ficam os services, DTOs, requests e mapeamentos utilizados pela API.

### Infrastructure

Contém detalhes técnicos externos ao domínio, como:

- `DbContext`
- Implementação dos repositórios
- Configuração do EF Core InMemory
- Mapeamento das entidades

A camada de Application depende de abstrações, como `IOrderRepository`, enquanto a implementação concreta fica na Infrastructure.

### Api

Camada responsável por expor os endpoints REST, configurar injeção de dependência, Swagger e tratamento das requisições HTTP.

---

## Decisões técnicas

### Uso de Clean Architecture

Mesmo sendo um projeto pequeno, a separação em camadas foi escolhida para demonstrar organização e facilitar evolução futura.

Com essa estrutura, seria possível trocar detalhes técnicos, como banco de dados ou framework de apresentação, sem impactar diretamente as regras de negócio.

---

### Uso de DDD de forma pragmática

O domínio foi modelado de forma simples, sem excesso de abstrações.

A entidade `Order` é responsável por manter a consistência do pedido, incluindo:

- Itens selecionados
- Validação de duplicidade
- Cálculo de subtotal
- Aplicação de desconto
- Cálculo do total final

Essa abordagem mantém as regras mais importantes próximas do modelo de negócio.

---

### Strategy Pattern para descontos

As regras promocionais foram implementadas usando **Strategy Pattern**.

Cada combinação de desconto possui sua própria estratégia:

- Sanduíche + batata + refrigerante
- Sanduíche + refrigerante
- Sanduíche + batata

Quando nenhuma estratégia é aplicável, o comportamento padrão é não aplicar desconto.

Essa decisão facilita a inclusão de novas promoções no futuro sem alterar diretamente a entidade `Order`.

---

### Banco InMemory

O projeto utiliza `Microsoft.EntityFrameworkCore.InMemory`.

Essa escolha foi feita para simplificar a execução do desafio, permitindo que a aplicação seja executada sem necessidade de configurar SQL Server, PostgreSQL, Docker ou connection strings.

A principal limitação é que os dados são perdidos ao encerrar a aplicação.

Para um ambiente real, a camada Infrastructure poderia ser ajustada para utilizar um banco relacional com migrations e seed inicial do cardápio.

---

### Cardápio em memória

O cardápio foi mantido como catálogo fixo em memória porque o desafio exige apenas consulta do cardápio, não manutenção dos produtos.

Em uma evolução futura, os itens do cardápio poderiam ser persistidos no banco de dados e carregados por um repositório próprio.

---

### Snapshot dos itens no pedido

Ao criar um pedido, os dados dos itens são copiados para `OrderItem`, incluindo nome, tipo e preço unitário.

Essa decisão preserva o histórico financeiro do pedido.

Se o preço de um produto mudar no futuro, pedidos antigos continuam mantendo os valores utilizados no momento da compra.

---

## Como executar a API

### Pré-requisitos

- .NET 8 SDK

Na raiz do projeto, execute:

```bash
dotnet restore
dotnet build
dotnet run --project src/GoodHamburger.Api
```

A API estará disponível nas URLs configuradas no `launchSettings.json`.

O Swagger pode ser acessado em:

```txt
https://localhost:{porta}/swagger
```

---

## Como executar os testes

Na raiz do projeto, execute:

```bash
dotnet test
```

Os testes cobrem as principais regras de negócio:

- Desconto de 20%
- Desconto de 15%
- Desconto de 10%
- Pedido sem desconto
- Pedido com item duplicado
- Pedido vazio

---

## Endpoints principais

### Menu

```http
GET /api/menu
```

### Pedidos

```http
POST /api/orders
GET /api/orders
GET /api/orders/{id}
PUT /api/orders/{id}
DELETE /api/orders/{id}
```

Exemplo de criação de pedido:

```json
{
  "items": ["XBurger", "Fries", "SoftDrink"]
}
```

---

## Evolução futura

A arquitetura atual permite algumas evoluções sem grandes mudanças estruturais:

- Trocar EF InMemory por SQL Server ou PostgreSQL
- Criar migrations e seed inicial do cardápio
- Criar autenticação e autorização
- Adicionar paginação na listagem de pedidos
- Criar middleware global para tratamento de exceções
- Criar testes de integração para os endpoints
- Criar Dockerfile e docker-compose
- Adicionar pipeline CI com GitHub Actions
- Criar um frontend para consumo da API

---

## O que ficou fora do escopo

Alguns pontos foram deixados fora intencionalmente para manter o projeto alinhado ao escopo e prazo do desafio:

- Autenticação e autorização
- Banco relacional persistente
- Migrations
- Seed de banco
- CRUD de produtos/cardápio
- Testes de integração
- Deploy em nuvem
- Observabilidade e logs estruturados
- Frontend

Esses pontos são importantes em um ambiente real, mas não eram essenciais para demonstrar as regras principais do desafio.

---

## Licença

Este projeto está licenciado sob a licença MIT.