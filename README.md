# Backend-DotNet-Mottu

## Descrição

Esta API gerencia entidades do App Mottu, incluindo **Condicao**, **Moto** e **LocalizacaoUWB**.
Possui validações via **FluentValidation**, documentação automática com **Swagger**, autenticação via **JWT** e health checks para monitoramento da aplicação.

O projeto foi estruturado em múltiplos projetos .NET para melhor organização:

* `Backend-DotNet-Mottu.API` → contém a API principal
* `Backend-DotNet-Mottu.Application` → lógica de negócio
* `Backend-DotNet-Mottu.Domain` → entidades e enums
* `Backend-DotNet-Mottu.Infrastructure` → persistência e acesso a banco
* `Backend-DotNet-Mottu.Tests` → testes unitários com xUnit

---

## Tecnologias

* .NET 8
* Entity Framework Core
* Oracle Database
* FluentValidation
* Swagger/OpenAPI
* JWT Authentication
* Docker / Docker Compose
* xUnit (Testes unitários)

---

## Estrutura de Pastas

```
Backend-DotNet-Mottu/
│  docker-compose.yml
│  Dockerfile
│  .env
│
├─ Backend-DotNet-Mottu.API/          # API principal
├─ Backend-DotNet-Mottu.Application/  # Lógica de negócio
├─ Backend-DotNet-Mottu.Domain/       # Entidades e enums
├─ Backend-DotNet-Mottu.Infrastructure/# Persistência
└─ Backend-DotNet-Mottu.Tests/        # Testes xUnit
```

---

## Execução com Docker

1. **Copie o arquivo de ambiente**:

```powershell
cp .env.example .env
```

2. **Suba os containers**:

```powershell
docker compose up -d --build
```

* MongoDB e API serão criados automaticamente.
* Health check disponível em: `http://localhost:5000/health` (ou porta configurada no `.env`).

3. **Logs**:

```powershell
docker compose logs -f
```

4. **Parar containers**:

```powershell
docker compose down
```

---

## Execução local (sem Docker)

1. Abra o Visual Studio 2022 ou superior.
2. Abra a solução `Backend-DotNet-Mottu.sln`.
3. Restaure pacotes NuGet:

```powershell
dotnet restore
```

4. Rode a API:

```powershell
cd Backend-DotNet-Mottu.API
dotnet run
```

* Swagger disponível em: `https://localhost:7172/swagger/index.html`
* Health check: `https://localhost:7172/health`

---

## Testes Unitários

Os testes estão na pasta `Backend-DotNet-Mottu.Tests`.
Para rodar todos os testes:

```powershell
cd Backend-DotNet-Mottu.Tests
dotnet test
```

---

## Autenticação JWT

* Para acessar endpoints protegidos, inclua no header:

```
Authorization: Bearer <seu_token_jwt>
```

* O token é gerado após login via endpoint de autenticação (ex: `/api/auth/login`).

---

## API Condicao (Exemplo)

| Método | Endpoint           | Descrição                       |
| ------ | ------------------ | ------------------------------- |
| GET    | /api/condicao      | Lista todas as condições        |
| GET    | /api/condicao/{id} | Busca uma condição pelo Id      |
| POST   | /api/condicao      | Cria uma nova condição          |
| PUT    | /api/condicao/{id} | Atualiza uma condição existente |
| DELETE | /api/condicao/{id} | Remove uma condição pelo Id     |

### Exemplo de request:

* **POST /api/condicao**

```json
{
  "nome": "Quebrado",
  "cor": "Vermelho"
}
```

* **PUT /api/condicao/{id}**

```json
{
  "nome": "Em Manutenção",
  "cor": "Amarelo"
}
```

---

## Validação

A API utiliza **FluentValidation**:

* `Nome` → obrigatório, máximo 100 caracteres
* `Cor` → obrigatório, máximo 50 caracteres

---

## Integrantes

* 558763 - Eric Issamu de Lima Yoshida
* 555010 - Gustavo Matias Teixeira
