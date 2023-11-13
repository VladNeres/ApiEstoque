# ApiProdutos

<p>O Projeto APIProdutos, é um projeto de controle de estoque que desenvolvi para aplicar os conhecimentos e praticar novas tecnologias</p>

<ul>
  <li> O Projeto desenvolvido na linguagem C# em .Net 6 tem com arquitetura o padrão MVC + DDD </li>
</ul>
  
## Tecnologias:  
<ul>
  <li> Microsoft.EntityFrameWorkCore para relacionamento de entidades</li>
  <li>Dapper para comunicação com banco de dados Sql Server</li>
  <li>XUnit para realização de testes unitarios, realizando o mock com o NSubstitute</li>
</ul>


   ## WORK FLow  
  <p> Para organização do projeto escolhi trabalhar com os fundamentos do Git flow, visando organização e maior controle do fluxo de trabalho <br>
   </p>

   ![gitflow_completo](https://github.com/VladNeres/ApiProdutos/assets/92832133/16d2c286-9999-4393-a6a9-52a0ceef28a3)


# Como executar a aplicação.

<ol>
<li> Baixe o docker desktop </li>
<li> Baixe o git em sua maquina </li>
<li> Crie uma nova pasta em sua maquina.</li>
<li> Dentro dessa pasta abra um cmd de sua preferencia e clone os projetos a baixo </li>
<li> APIs, (<a href="https://github.com/VladNeres/ApiProdutos"> APIProduto </a>) e a (<a href="https://github.com/VladNeres/ApiEstoque"> APIEstoque</a>) </li>
<li> Abra um cmd de sua preferencia e navegue até a pasta onde voce clonou as APIs e entre em uma das duas pastas (ApiProdutos, ApiEstoque)</li>
<li> Agora que voce esta dentro da pasta de uma das duas apis e com o aplicativo do docker descktop aberto digite em seu cmd <br> - docker-compose up</li>
</ol>

# Como utilizar a estrutura de banco.

<p>Caso tenha o sql management studio instalado em sua maquina, irei deixar um passoa a passo.
Dentro das pastas do repositorio deixei os scripts de banco para que seja construido toda a estrutura de banco inclusive uma carga de dados para teste.
</p>

 Abra o managemant studio  e clique no icone de tomaga como na imagem abaixo <br>
  ![estruturando sql1](https://github.com/VladNeres/ApiProdutos/assets/92832133/a0c34e99-3f15-40ea-82f8-1808b9c62dea) <br>

 Irá aparecer uma telinha  para se autenticar ao banco <br>
 ![sql2](https://github.com/VladNeres/ApiProdutos/assets/92832133/7c647ede-3f1b-4694-bd40-71c0912d04ce) <br>
 
preencha os campos com as seguintes credenciais: <br>
<ol>
  <li>ServerType: Database Engine </li>
  <li>Server name:  localhost,1443 </li>
  <li> Authentication: SqlServer authentication </li>
  <li>Login : SA </li>
  <li>Password: Teste@Teste123 </li>
</ol>



<p>acesse dentro do repositório: Data/scripts  e dentro dessa pasta tera os scripts de banco que devem ser excutados em ordem </p>

<ol>
  <li> <a href="https://github.com/VladNeres/ApiProdutos/tree/main/Data/Scripts/DataBase"> ./Database </a> -> MercadoDb.sql</li>
  <li> <a href="https://github.com/VladNeres/ApiProdutos/tree/main/Data/Scripts/tables"> ./tables </a> -> CategoriaTable.sql, ProdutoTable.sql, Estoque.sql </li>
  <li> <a href="https://github.com/VladNeres/ApiProdutos/tree/main/Data/Scripts/Procedures">./Procedures </a> -> CriarProduto.sql, AlterarProdutos.sql, Delete.sql</li>
  <li> <a href="https://github.com/VladNeres/ApiProdutos/tree/main/Data/Scripts/Scripts">./scripts </a> -> CargaCategoria.sql, CargaProduto.sql, CargaEstoque.sql </li>
</ol>

Copie cada arquivo na ordem descrita, cole no mssql mmanagement studio e execute.

## Obs: caso queira executar a aplicação localmente sem a utilização do docker
Basta clonar os projetos em sua maquina, va no arquivo appsettings.json e nesse caso voce tem 2 opçoes.

<ol>
  <li>caso esteja utilizando a autenticação do whindos no seu sql, comente a conection que faz refencia ao servidor do docker com as credenciais, e descomente a outra conection, alterando o server para seu server local </li>
  <li> Caso esteja utilizando a autenticação por credenciais do sql, basta alterar o server, user e senha da connection string.</li>
