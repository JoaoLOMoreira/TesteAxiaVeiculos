# Axia Veiculos

API REST para cadastro, consulta, atualizacão e remocão de veiculos.

## Tecnologias

- .NET 8
- ASP.NET Core Web API com Controllers
- OpenAPI/Swagger
- Entity Framework Core
- Microsoft.EntityFrameworkCore.InMemory
- MediatR
- FluentValidation

## Estrutura

- `AxiaVeiculosDomain`: entidade `Veiculo`, enumerador `MarcaVeiculo` e interface `IVeiculoRepository`.
- `AxiaVeiculosApplication`: commands, queries, handlers, validadores, `IVeiculoService` e `VeiculoService`.
- `AxiaVeiculosInfra`: `DbContext`, configuracao do EF Core InMemory e implementacao do repositorio.
- `AxiaVeiculosWebApi`: controllers, Swagger, configuracao da aplicacao e tratamento HTTP.

## Fluxo da aplicacão

As requisições chegam pelo `VeiculosController`, que envia commands ou queries pelo MediatR. Os handlers delegam a execucão ao `IVeiculoService`. O service aplica o caso de uso e acessa dados somente pela interface `IVeiculoRepository`. A implementacão do repositorio usa o `VeiculosDbContext` com provider InMemory.

As validações são executadas por um pipeline do MediatR usando FluentValidation. Erros de validacão retornam HTTP 400, veiculos inexistentes retornam HTTP 404 e erros inesperados retornam HTTP 500.

## Executar pelo Visual Studio

1. Abra a solucao no Visual Studio.
2. Defina `AxiaVeiculosWebApi` como projeto de inicializacao.
3. Execute o perfil `http` ou `https`.
4. Acesse o Swagger em `/swagger`.

## Executar com dotnet run

```bash
dotnet restore
dotnet run --project AxiaVeiculosWebApi/AxiaVeiculosWebApi.csproj
```

## Executar testes

```bash
dotnet test
```

O projeto `AxiaVeiculosTests` cobre validadores, service, fluxo MediatR e repositorio com EF Core InMemory.

Swagger:

- `http://localhost:5214/swagger`
- `https://localhost:7279/swagger`

## Endpoints

- `POST /api/veiculos`
- `PUT /api/veiculos/{id}`
- `GET /api/veiculos/{id}`
- `GET /api/veiculos`
- `DELETE /api/veiculos/{id}`

## Exemplo de cadastro

```json
{
  "descricao": "Honda Civic EXL",
  "marca": "Honda",
  "modelo": "Civic",
  "opcionais": "Cambio automatico, ar-condicionado digital",
  "valor": 145000.00
}
```

## Exemplo de atualizacão

```json
{
  "descricao": "Honda Civic Touring",
  "marca": "Honda",
  "modelo": "Civic",
  "opcionais": "Teto solar, bancos em couro",
  "valor": 158000.00
}
```

## Armazenamento em memoria

Os dados são armazenados com o provider `Microsoft.EntityFrameworkCore.InMemory`. Esse provider mantem os registros apenas durante a execucao da aplicacao; ao encerrar a API, os dados podem ser perdidos.

## Migrations

Migrations do Entity Framework Core são destinadas a providers relacionais. Como o projeto usa obrigatoriamente o provider InMemory, não ha suporte real para executar migrations. Por isso, a inicializacao utiliza `EnsureCreated`, sem adicionar SQLite, SQL Server ou outro provider apenas para gerar arquivos de migration.

## Migrations

Não foram criadas migrations do Entity Framework Core porque o projeto utiliza o provider `Microsoft.EntityFrameworkCore.InMemory`.

As migrations são voltadas para bancos relacionais, onde existe um schema físico a ser versionado e atualizado. Como o provider InMemory armazena os dados apenas em memória durante a execução da aplicação, não há um banco real nem estrutura persistente para aplicar migrations.

Por esse motivo, a aplicação utiliza `EnsureCreated` para inicializar o contexto em memória. Também foi evitado adicionar SQLite, SQL Server ou outro provider relacional apenas para gerar migrations, pois isso alteraria a proposta simples do projeto e criaria uma dependência que não é necessária para o escopo atual.

Caso futuramente o projeto passe a usar um banco relacional, as migrations poderão ser adicionadas normalmente.

## Testes automatizados

Os testes automatizados foram adicionados como um diferencial do projeto. Eles não eram um requisito obrigatorio do teste tecnico, mas foram incluidos para demonstrar maior confiabilidade na implementação.

O projeto de testes `AxiaVeiculosTests` cobre:

- validacoes dos commands com FluentValidation;
- regras do `VeiculoService`;
- fluxo de comandos e queries via MediatR;
- operacoes do repositorio utilizando EF Core InMemory.

Para executar os testes:

```bash
dotnet test
