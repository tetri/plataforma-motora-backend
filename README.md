# Plataforma Motora - Backend

API REST do sistema Plataforma Motora. Este backend é responsável por:

- Cadastro e consulta de veículos por placa
- Autenticação de usuários com JWT
- Auditoria com deleção lógica
- Validação de placas (incluindo Mercosul e países participantes)
- Arquitetura limpa (Clean Architecture)
- Persistência desacoplada (atualmente via Supabase/PostgreSQL)
- Documentação com [Scalar](https://scalar.com)

---

## 🧱 Tecnologias utilizadas

- [.NET 9](https://dotnet.microsoft.com/)
- [EF Core](https://learn.microsoft.com/ef/core/)
- PostgreSQL (via [Supabase](https://supabase.com))
- [Scalar](https://scalar.com)
- JWT para autenticação
- [BCrypt.Net](https://github.com/BcryptNet/bcrypt.net) para segurança de senha
- GitHub Actions (CI/CD)

---

## 🚀 Como rodar o projeto

### 🔧 Pré-requisitos

- .NET 9 SDK
- Conta no Supabase
- Variáveis de ambiente configuradas em um `.env`

### 🏗️ Rodando localmente

```bash
dotnet restore
dotnet ef database update --project PlataformaMotora.Infrastructure --startup-project PlataformaMotora.API
dotnet run --project PlataformaMotora.API
```

### 🔐 Variáveis de ambiente (.env)

Crie um arquivo `.env` na raiz da API com os seguintes valores:

```env
SUPABASE_CONNECTION_STRING=Host=...;Port=...;Database=...;Username=...;Password=...
JWT_SECRET=chave-secreta
JWT_ISSUER=plataforma-motora
JWT_AUDIENCE=plataforma-motora-clients
USUARIO_SISTEMA_EMAIL=system@plataforma.com
USUARIO_SISTEMA_SENHA=senhaforte123
```

---

## 📦 Estrutura do projeto

```
src/
├── PlataformaMotora.Domain
├── PlataformaMotora.Application
├── PlataformaMotora.Infrastructure
└── PlataformaMotora.API
```

---

## ✅ Testes

```bash
dotnet test
```

---

## 🚧 Em desenvolvimento futuro

- Gestão de serviços automotivos
- Controle de estacionamento
- Painel administrativo
- Suporte a múltiplos usuários
- Exportação de dados

---

## 💻 Autor

Desenvolvido por Tetri Mesquita 🚗🚀
