# 🛵 Ping Mottu

## 👨‍💻 Integrantes

- RM558763 - Eric Issamu de Lima Yoshida
- RM555010 - Gustavo Matias Teixeira
- RM557515 - Gustavo Monção

---
## Descrição

Esta API gerencia entidades do App Mottu, incluindo Condicao, Moto e LocalizacaoUWB.
Possui validações via FluentValidation e documentação automática com Swagger.


## Tecnologias

- .NET 8

- Entity Framework Core

- Oracle Database

- FluentValidation

- Swagger/OpenAPI

- Hateoas


## Execução

1. Clone o repositório:
```
git clone https://github.com/Yoshida672/Backend-DotNet-Mottu
cd Backend-DotNet-Mottu
```
2. Abrir no Visual Studio

Abra o Visual Studio (recomendo a versão 2022 ou superior),
no menu inicial, clique em "Abrir um projeto ou solução",
navegue até a pasta que você clonou (CP2_BackEndMottu_DotNet) e selecione o arquivo .sln (Solution) e clique em Abrir.

3. Restaurar pacotes NuGet
No terminal rode
```
dotnet restore
```

## Exemplo  

Testar a API Condicao

A API possui endpoints para CRUD da entidade Condicao:


| Método | Endpoint           | Descrição                       |
| ------ | ------------------ | ------------------------------- |
| GET    | /api/condicao      | Lista todas as condições        |
| GET    | /api/condicao/{id} | Busca uma condição pelo Id      |
| POST   | /api/condicao      | Cria uma nova condição          |
| PUT    | /api/condicao/{id} | Atualiza uma condição existente |
| DELETE | /api/condicao/{id} | Remove uma condição pelo Id     |

Exemplo de request para criar uma Condição:
- **GET**  ```/api/condicao```

- **POST**  ```/api/condicao```
```
{
  "nome": "Quebrado",
  "cor": "Vermelho"
}
```
- **PUT**  ```/api/condicao/{id}```
```
{
id: ID DA CONDICAO
}
{
  "nome": "Em Manutenção",
  "cor": "Vermelho"
}
```
- **DELETE**  ```/api/condicao/{id}```

### Validação

A API utiliza FluentValidation para garantir que os dados enviados sejam válidos:
```
- Nome não pode estar vazio e deve ter no máximo 100 caracteres

- Cor não pode estar vazio e deve ter no máximo 50 caracteres
```

### Swagger

Após rodar a aplicação, abra o Swagger para testar os endpoints:
https://localhost:7172/swagger/index.html







