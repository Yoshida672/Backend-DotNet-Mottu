
# 📦 CP2_BackEndMottu_DotNet

## 📑 Descrição do Projeto

Este projeto é uma API RESTful desenvolvida com **ASP.NET Core** e **Entity Framework Core** para integração com banco de dados **Oracle**. A API gerencia recursos relacionados a **Motos** e **Localizações UWB**, permitindo operações de CRUD completas, além de validações e documentação via Swagger/OpenAPI.

Este sistema foi desenvolvido como parte da entrega da **1ª Sprint**, com foco em:
- Integração com Oracle via EF Core (com migrations)
- Implementação de controllers RESTful
- Utilização de boas práticas de design de API (códigos HTTP apropriados)
- Documentação com Swagger
- Organização e clareza no código

## 🚀 Tecnologias Utilizadas

- ASP.NET Core 7
- Entity Framework Core
- Oracle (via Oracle.EntityFrameworkCore)
- Swagger (Swashbuckle)
- FluentValidation
- Visual Studio 2022
- Git/GitHub

## 📌 Como Rodar o Projeto

### 🧰 Pré-requisitos

- [.NET SDK 7.0+](https://dotnet.microsoft.com/)
- [Oracle Database](https://www.oracle.com/database/)
- [Oracle Data Provider for .NET](https://www.oracle.com/database/technologies/dotnet-odacdeploy-downloads.html)

### 🛠️ Instalação

1. Clone o repositório:
   ```bash
   git clone https://github.com/seuusuario/CP2_BackEndMottu_DotNet.git
   cd CP2_BackEndMottu_DotNet
   ```

2. Configure a string de conexão no `appsettings.json`:
   ```json
   "ConnectionStrings": {
     "DefaultConnection": "User Id=usuario;Password=senha;Data Source=localhost:1521/xe;"
   }
   ```

3. Execute as migrations para criar o banco:
   ```bash
   dotnet ef database update
   ```

4. Rode o projeto:
   ```bash
   dotnet run
   ```

5. Acesse a documentação Swagger:
   - [http://localhost:5000/swagger](http://localhost:5000/swagger)

## 🔁 Rotas Implementadas

### 🏍️ MotoController (`/api/moto`)

| Método | Rota                      | Descrição                              | Parametros        | Status HTTP         |
|--------|---------------------------|----------------------------------------|-------------------|---------------------|
| GET    | `/api/moto`               | Lista todas as motos                   | —                 | 200 OK              |
| GET    | `/api/moto/{id}`          | Retorna uma moto por ID                | `PathParam`       | 200 OK / 404 NotFound |
| POST   | `/api/moto`               | Cria uma nova moto                     | `Body`            | 201 Created / 400 BadRequest |
| PUT    | `/api/moto/{id}`          | Atualiza uma moto existente            | `PathParam + Body`| 204 NoContent / 400 / 404 |
| DELETE | `/api/moto/{id}`          | Deleta uma moto por ID                 | `PathParam`       | 204 NoContent / 404 |

### 📡 LocalizacaoUWBController (`/api/localizacao`)

| Método | Rota                            | Descrição                                | Parametros        | Status HTTP         |
|--------|----------------------------------|------------------------------------------|-------------------|---------------------|
| GET    | `/api/localizacao`              | Lista todas as localizações              | —                 | 200 OK              |
| GET    | `/api/localizacao/{id}`         | Retorna localização por ID               | `PathParam`       | 200 OK / 404        |
| GET    | `/api/localizacao/por-moto/{id}`| Retorna localização por ID da moto       | `PathParam`       | 200 OK / 404        |
| POST   | `/api/localizacao`              | Cria nova localização                    | `Body`            | 201 Created / 400   |
| PUT    | `/api/localizacao/{id}`         | Atualiza uma localização existente       | `PathParam + Body`| 200 OK / 400 / 404  |
| DELETE | `/api/localizacao/{id}`         | Deleta uma localização por ID            | `PathParam`       | 204 NoContent / 404 |

## 🧪 Migrations EF Core

Para gerar ou atualizar o banco Oracle:

```bash
dotnet ef migrations add InitialCreate
dotnet ef database update
```

## 📘 Documentação Swagger

A documentação da API foi gerada com Swagger e está disponível automaticamente ao rodar o projeto.

- Interface interativa: [http://localhost:5000/swagger](http://localhost:5000/swagger)
- Suporte completo a:
  - Tipagem de parâmetros
  - Códigos de resposta (200, 201, 400, 404, 204)
  - Modelos de requisição e resposta
  - Validações com FluentValidation

## 👨‍💻 Desenvolvedores


- Eric Yoshida:   https://github.com/Yoshida672
- Gustavo Matias: https://github.com/Gustavo295
- Gustavo Monção: https://github.com/moncaogustavo


### Condição (CondicaoController)

- **GET** `/api/condicao` → Lista todas as condições
- **GET** `/api/condicao/{id}` → Retorna uma condição pelo ID
- **POST** `/api/condicao` → Cria uma nova condição
- **PUT** `/api/condicao/{id}` → Atualiza uma condição existente
- **DELETE** `/api/condicao/{id}` → Remove uma condição
