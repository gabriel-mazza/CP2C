Gabriel Barros Mazzariol RM555410
Jefferson Junior Alvarez Urbina RM558497

Produto Bancário Escolhido: Máquina de Cartão
A Máquina de Cartão foi escolhida por ser o produto bancário mais presente no cotidiano brasileiro, utilizado por milhões de estabelecimentos comerciais. Seu modelo de negócio é baseado na taxa MDR (Merchant Discount Rate), cobrada a cada transação, o que nos permitiu implementar uma regra de negócio extra clara e relevante: a variação da taxa conforme o perfil do cliente. Pessoa Jurídica recebe 30% de desconto por representar menor risco e maior volume de transações, enquanto Pessoa Física paga a taxa integral.

Diagrama de Classes

<img width="857" height="1600" alt="image" src="https://github.com/user-attachments/assets/5b53b9f6-c515-4037-846e-b9b356011806" />


Endpoints Disponíveis

POST /api/Agencias
Request:
json
{
  "numero": "0001",
  "nome": "Agência Central",
  "cidade": "São Paulo"
}

Response 201:
json
{
  "id": 1,
  "numero": "0001",
  "nome": "Agência Central"
}

GET /api/Agencias/{id}
Response 200:
json
{
  "id": 1,
  "numero": "0001",
  "nome": "Agência Central"
}

POST /api/Clientes/pf
Request:
json
{
  "nome": "João da Silva",
  "agenciaId": 1,
  "cpf": "123.456.789-09",
  "dataNascimento": "1990-01-01"
}

Response 201:
json
{
  "id": 1,
  "nome": "João da Silva",
  "cpf": "123.456.789-09",
  "agenciaId": 1
}


POST /api/Clientes/pj
Request:
json
{
  "nome": "Empresa Tech",
  "agenciaId": 1,
  "cnpj": "11.222.333/0001-44",
  "razaoSocial": "Empresa Tech Ltda"
}

Response 201:
json
{
  "id": 2,
  "nome": "Empresa Tech",
  "cnpj": "11.222.333/0001-44",
  "agenciaId": 1
}
GET /api/Clientes/{id}
Response 200:
json
{
  "id": 1,
  "nome": "João da Silva",
  "agenciaId": 1,
  "tipoCliente": "PF",
  "documento": "123.456.789-09"
}

POST /api/Produtos
Request:
json
{
  "nome": "Máquina de Cartão Standard",
  "descricao": "Equipamento POS para débito e crédito",
  "modeloEquipamento": "POS",
  "taxaMdrBase": 1.99
}

Response 201:
json
{
  "id": 1,
  "nome": "Máquina de Cartão Standard",
  "descricao": "Equipamento POS para débito e crédito",
  "modeloEquipamento": "POS",
  "taxaMdrBase": 1.99
}

POST /api/Contratacoes
Request:
json
{
  "clienteId": 1,
  "produtoId": 1
}

Response 201 — Cliente PF (taxa cheia):
json
{
  "id": 1,
  "clienteId": 1,
  "nomeCliente": "João da Silva",
  "produtoId": 1,
  "nomeProduto": "Máquina de Cartão Standard",
  "status": "Aprovada",
  "taxaMdrEfetiva": 1.99,
  "observacao": "Taxa MDR aplicada: 1,99% (cliente PF — taxa base 1,99%)",
  "dataSolicitacao": "2026-05-07T00:12:56Z"
}

Response 201 — Cliente PJ (30% de desconto):
json
{
  "id": 2,
  "clienteId": 2,
  "nomeCliente": "Empresa Tech",
  "produtoId": 1,
  "nomeProduto": "Máquina de Cartão Standard",
  "status": "Aprovada",
  "taxaMdrEfetiva": 1.39,
  "observacao": "Taxa MDR aplicada: 1,39% (cliente PJ — taxa base 1,99%)",
  "dataSolicitacao": "2026-05-07T00:13:00Z"
}

GET /api/Contratacoes/{id}
Response 200:
json
{
  "id": 1,
  "clienteId": 1,
  "nomeCliente": "João da Silva",
  "produtoId": 1,
  "nomeProduto": "Máquina de Cartão Standard",
  "status": "Aprovada",
  "taxaMdrEfetiva": 1.99,
  "observacao": "Taxa MDR aplicada: 1,99% (cliente PF — taxa base 1,99%)",
  "dataSolicitacao": "2026-05-07T00:12:56Z"
}


Testes
<img width="1920" height="1053" alt="image" src="https://github.com/user-attachments/assets/aa97826f-a382-4372-bab3-3c0f531550bf" />


Evidências de Funcionamento

Swagger com contratação aprovada
<img width="1199" height="686" alt="image" src="https://github.com/user-attachments/assets/7f09f0fb-be82-48b3-808e-c9e93191f838" />


Como Executar o Projeto

Clone o repositório
Configure as credenciais Oracle no appsettings.json:
json
{
  "ConnectionStrings": {
    "OracleFIAP": "Data Source=oracle.fiap.com.br:1521/ORCL;User Id=SEU_RM;Password=SUA_SENHA;"
  }
}

No console NuGet rode:
Add-Migration Inicial
Update-Database
4. Rode a API
5. Acesse o Swagger em: https://localhost:7129/swagger
