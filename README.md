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

## 🧰 Tecnologias Utilizadas

- **ASP.NET Core 8**
- **Entity Framework Core 8**
  - `Microsoft.EntityFrameworkCore`
  - `Microsoft.EntityFrameworkCore.Design`
  - `Microsoft.EntityFrameworkCore.Tools`
  - `Microsoft.EntityFrameworkCore.Proxies`
- **Oracle.EntityFrameworkCore** — Suporte ao Oracle Database
- **FluentValidation.AspNetCore** — Validação de dados via FluentValidation
- **Swagger (Swashbuckle.AspNetCore)** — Documentação da API REST
- **Camadas de arquitetura**:
  - `Domain` (entidades e regras de negócio)
  - `Application` (DTOs, casos de uso)
  - `Infrastructure` (acesso a dados, contexto EF)
  - `API` (controllers e endpoints)

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



### Nosso Professor:

###### Thiago Keller Torquato Vicco	

