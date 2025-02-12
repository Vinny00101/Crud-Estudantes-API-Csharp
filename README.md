<h1 align="center">Documentação da API de Sistema de Estudantes</h1>

## Introdução 

<p>Esta documentação fornece informações sobre como usar a API para gerenciar dados de estudantes. A API permite realizar operações CRUD (Criar, Ler, Atualizar, Excluir) nos dados dos estudantes.</p>


## Requisitos

<ul>
    <li>.NET Core mais atual</li>
    <li>Visual Studio / VS Code</li>
    <li>Postman ou outro cliente HTTP para testar a API</li>
    <li>Banco de dados MySQL</li>
</ul>

## Configuração

Primeiro Clone o repositorio na bash:

```
    git clone https://github.com/Vinny00101/Crud-Estudantes-API-Csharp.git
```
Após garantir que todos os requisitos estão atendidos e o repositório foi clonado, execute o seguinte comando Dotnet no terminal, dentro do diretório da API:

```
    dotnet restore
```

##

Primeiro, faça a criação de um banco de dados no MySQL. Após criar o banco de dados, você deve criar uma string de conexão que contenha o `server`, `port`, `user`, `password` e `database`. Abaixo, mostramos um exemplo de como deve ser feito:
```
    "server=LocalHost;port=3000;user=user;password=senha0120;database=dataBase;"

```

Depois de fazer isso, você deve criar um arquivo .env e nomear a variável como `stringConection` para que o arquivo `Program.cs` possa ler a conexão com o `MySQL`. A baixo o exemplo:

```
    stringConection="server=LocalHost;port=3000;user=user;password=senha0120;database=dataBase;"

```

Finalmente, para aplicar as migrações e criar as tabelas no banco de dados, use o comando `dotnet ef database update`. Este comando aplica todas as migrações pendentes ao banco de dados.

```
    dotnet ef database update
```

Através dessa rota a abaixo você pode realizar testes na API.

```
    http://localhost:5255/swagger/index.html
```

##

<div align="center">
    <img src="https://cdn.jsdelivr.net/gh/devicons/devicon/icons/csharp/csharp-original.svg" height="40" alt="csharp logo" />
    <img width="12" />
    <img src="https://cdn.jsdelivr.net/gh/devicons/devicon/icons/dotnetcore/dotnetcore-original.svg" height="40" alt="dotnetcore logo" />
</div>