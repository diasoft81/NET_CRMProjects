# CRM API & Blazor UI – Technical Assessment (.NET 8)

This solution is a complete technical assessment project using **.NET 8**, **Entity Framework Core**, **Blazor**, and **Clean Architecture principles**. It demonstrates API-first design with validation, logging, caching, unit testing, and integration with a message broker.

---

## 🚀 How to Run

1. **Requirements**:
   - [.NET 8 SDK](https://dotnet.microsoft.com/download)
   - (Optional) RabbitMQ Server (local or Docker)

2. **Steps**:
   ```bash
   dotnet restore
   dotnet build
   dotnet run --project CRM.API
   dotnet run --project TestApp
   ```

3. **Access**:
   - Swagger: https://localhost:{port}/swagger
   - Blazor UI: https://localhost:{port}/

---

## 🧱 Architecture Overview

| Layer              | Description                                   |
|--------------------|-----------------------------------------------|
| `CRM.API`          | ASP.NET Core Web API (Swagger, Middleware)    |
| `CRM.Repositories` | Entity models + Repository Pattern (EF Core)  |
| `TestApp`          | Blazor Server UI (User/Role Management)       |
| `CRM.Tests`        | NUnit test project (EF InMemory & SQLite)     |

---

## ✅ Implemented Features

### 📘 Functional

- ✅ CRUD API for **User**, **Role**, **UserRole**
- ✅ Transactional logic: `CreateUserWithRoleAsync`
- ✅ Relational modeling: 1-N & N-N (User ↔ Role)
- ✅ Data Validation with `[Required]`, `[EmailAddress]`, etc.
- ✅ Global Exception Handling Middleware
- ✅ Swagger Documentation

### 🔧 Technical Enhancements

| Feature         | Implementation                            |
|-----------------|---------------------------------------------|
| **Logging**     | Built-in `ILogger` + `Serilog`              |
| **Caching**     | `IMemoryCache` for GET APIs                 |
| **Message Broker** | `RabbitMQ.Client` to publish user-created events |
| **Unit Tests**  | `NUnit` with EF Core InMemory + SQLite      |

---

## 🧪 Unit Test Coverage

- ✅ UserService: transactional creation + role assignment
- ✅ Test isolation with in-memory SQLite
- ✅ Easy-to-extend NUnit-based structure

Run tests:
```bash
dotnet test CRM.Tests
```

---

## 📬 RabbitMQ Event

User creation triggers this message:
```json
{
  "event": "user.created",
  "userId": 123,
  "userName": "jdoe"
}
```

Published to `user.created` queue using RabbitMQ.

---

## 📂 UI Navigation (Blazor)

| Page       | Path        |
|------------|-------------|
| Home       | `/`         |
| Users      | `/user`     |
| Roles      | `/role`     |
| User Roles | `/userrole` |

---

## 🧠 Notes for Reviewers

This project balances clarity, modularity, and practical demonstration of critical backend concepts:
- Clean layering
- Transaction safety
- Realistic feature integration (logging, cache, broker)
- Well-structured test coverage

Built to be extendable into production-grade service if needed.

---

## 📄 License

MIT – free to use and extend.