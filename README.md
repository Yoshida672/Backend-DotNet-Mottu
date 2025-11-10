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


## Pré-requisitos

* .NET 8 SDK instalado
* MongoDB instalado e rodando localmente
* Node.js e npm/yarn (se houver frontend)

---

## Configuração do ambiente

1. **Clone o repositório**

```bash
git clone <URL_DO_REPOSITORIO>
cd Backend-Dotnet-Mottu
```

2. **Copie o arquivo de exemplo do ambiente**

```bash
cp .env.example .env
```

> Configure as variáveis de ambiente no arquivo `.env` conforme sua máquina.

3. **Inicie o MongoDB local**

* Certifique-se de que o MongoDB está rodando.
* Crie o banco de dados definido no `.env`.

4. **Crie o usuário administrador**

```bash
mongo <nome_do_banco> ./scripts/01-create-user.js
```

> O script adicionará o usuário admin necessário para acessar o sistema.

---

## Executando o backend no Visual Studio

1. Abra a solução `Backend-Dotnet-Mottu.sln` no Visual Studio.
2. Configure o projeto **Backend-Dotnet-Mottu.API** como projeto de inicialização.
3. Rode o projeto (`F5` ou `Ctrl+F5`).
4. O Swagger estará disponível em:

```
http://localhost:<porta>/swagger
```

---

## Executando via linha de comando (opcional)

```bash
cd Backend-Dotnet-Mottu.API
dotnet restore
dotnet build
dotnet run
```

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
