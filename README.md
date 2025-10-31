<div align="center">
  
# Entrega de Advanced Business Development with .NET


</div>

---

Neste repositório está presente o desenvolvimento da entrega do challenge de 2025 da matéria **Advanced Business Development with .NET** da faculdade FIAP.

### Membros do grupo:
- Erick Alves - <a href="https://github.com/Erick0105">Erick0105</a> - Rm 5568682
- Vicenzo Oliveira - <a href="https://github.com/fFukurou">fFukurou</a> - Rm 554833
- Luiz Henrique - <a href="https://github.com/LuizHNR">LuizHNR</a> - Rm 556864

---
# 🚗 API de organização

Este é um projeto de uma API RESTful desenvolvida em **ASP.NET Core**, que gerencia **O controle dos dados Refente a organização** para melhorar a resolver o problemas de falta de organização.
O sistema simula uma plataforma de controle de dados, com integração a banco de dados Oracle e uso de validações robustas via **FluentValidation**.

---

## 📌 Rotas Disponíveis

Todas as rotas estão disponíveis no controlador, por Exemplo:

| Método | Rota                  | Descrição                           |
|--------|------------------------|--------------------------------------|
| GET    | `/api/Motorista`       | Retorna todos os motoristas         |
| GET    | `/api/Motorista/{id}`  | Retorna um motorista por ID         |
| POST   | `/api/Motorista`       | Cria um novo motorista              |
| PUT    | `/api/Motorista/{id}`  | Atualiza um motorista existente     |
| DELETE | `/api/Motorista/{id}`  | Remove um motorista do sistema      |

---

## 🏗️ Justificativa da Arquitetura

O projeto foi desenvolvido utilizando **arquitetura em camadas**, com inspiração em **Clean Architecture**, para garantir separação de responsabilidades, fácil manutenção e escalabilidade:

- **Domain** → contém as entidades, enums e regras de negócio principais.  
- **Application** → concentra os DTOs, validações com FluentValidation e casos de uso (Use Cases).  
- **Infrastructure** → responsável pela persistência dos dados, configuração do **Entity Framework Core** e integração com **Oracle Database**.  
- **API** → camada de apresentação, expondo os endpoints REST por meio de controllers.  

Essa abordagem permite **maior testabilidade**, **baixo acoplamento** e facilita futuras mudanças ou integrações.

---

## 🧰 Tecnologias Utilizadas

- **.NET 8.0**
- **Entity Framework Core 8**
  - `Microsoft.EntityFrameworkCore`
  - `Microsoft.EntityFrameworkCore.Design`
  - `Microsoft.EntityFrameworkCore.Tools`
  - `Microsoft.EntityFrameworkCore.Proxies`
- **Oracle.EntityFrameworkCore** — Suporte ao Oracle Database  
- **FluentValidation.AspNetCore** — Validação de dados  
- **Swagger (Swashbuckle.AspNetCore + Filters + Annotations)** — Documentação da API  
- **AutoMapper** — Mapeamento entre entidades e DTOs  


---

## 🚀 Como Executar o Projeto

### ✅ Pré-requisitos

- .NET SDK 8.0 ou superior
- Banco de dados Oracle instalado ou acesso a instância remota
- Ferramenta como DBeaver, Oracle SQL Developer, etc. para gerenciar o Oracle
- Git
- Editor de código (Visual Studio ou VS Code)

---

### 📦 Clonar o projeto

```bash
git clone https://github.com/Os-Tres-Motoqueiros-do-Apocalipse-Verde/Advanced-Business-Development-with-.NET.git
cd Advanced-Business-Development-with-.NET
```

---

### Entrar no visual studio e selecionar o projeto

- Apertar run para executar o projeto

- Selecionar a versão que você deseja testar

- O projeto possui nivel de acessos então para algumas coisas você precisa ter autorização de Admin

- Selecione o post do crud de Usuarios
 ```bash
 {
  "username": "Admin",
  "password": "Admin123"
}
```

- A resposta sera um token você copia ele e cola para se cadastrar no swagger

- Após isso você pode utilizar qualquer Crud de forma livre

- Para utilizar o Health Check você pode rodar a API e colocar na aba de pesquisa:
 ```bash
    http://localhost:5207/health-ui
```

- Ou 
 ```bash
    http://localhost:5207/health
```

### Nosso Professor:

###### Thiago Keller Torquato Vicco	

