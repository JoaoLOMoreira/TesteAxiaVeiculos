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
