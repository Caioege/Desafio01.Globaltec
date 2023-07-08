<h1 align="center">Desafio 01 para Desenvolvedor da Globaltec</h1>

## Objetivo do desafio:
- [x] Criação de uma aplicação web API utilizando padrões REST
- [x] Utilizar autenticação via token (Bearer)
- [x] Cadastro de usuário
- [x] Autenticação de usuário
- [x] Cadastro de pessoa
- [x] Consulta de pessoa
- [x] Alteração do cadastro de pessoa
- [x] Exclusão do cadastro de pessoa

## Descrição do Projeto
<p>
  A aplicação foi construída com .NET 6. Para executar será necessário ter essa versão instalada. Você pode encontrá-la aqui: https://dotnet.microsoft.com/pt-br/download/dotnet/6.0.
</p>
<p>
  A aplicação foi documentada com Swagger, para facilitar a visualização e execução das rotas. Veja mais: https://swagger.io/.
</p>
<p>
  O projeto não possui comunicação com o banco de dados (conforme orientado no pedido do desafio). Todos os dados são persistidos e consumidos em memória (cache) da aplicação.
</p>
<p>
  A aplicação está divida em três camadas:
</p>
<ul>
  <li>WebAPI: possui toda a documentação com Swagger e rotas que o usuário pode acessar. Essa é a camada que deve ser executada;</li>
  <li>Dominio: possui todas as entidades que a aplicação precisa e algumas constantes;</li>
  <li>Serviço: possui todos os serviços que implementam as regras de negócio da aplicação e comunicação com os dados que estão em cache. Também possui algumas funções comuns à toda a aplicação;</li>
</ul>

<p>
  A aplicação já vem com um usuário carregado, com usuário e senha como 'globaltec'.
</p>
<p>
  A aplicação já possui duas pessoas carregadas em memória, ambas com UF definida como "GO", para facilitar a busca.
</p>

## Execução do projeto
<ul>
  <li>Instale a versão 6 do .NET Framework;</li>
  <li>Clone o repositório;</li>
  <li>Abra a solução;</li>
  <li>Defina o projeto 'Globaltec.WebAPI.CSharp' como projeto de inicialização;</li>
  <li>Execute a aplicação (F5);</li>
  <li>Crie um usuário novo ou autentique com o usuário padrão;</li>
  <li>Copie o token recebido e o coloque na parte de autorização da documentação do Swagger (canto superior direito);</li>
  <li>Execute as rotas de pessoa.</li>
</ul>
