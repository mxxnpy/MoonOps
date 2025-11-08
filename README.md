# MoonOps

MoonOps é uma aplicação ASP.NET Core escalável construída com princípios de Domain-Driven Design (DDD), apresentando documentação OpenAPI com Scalar UI.

**Autor:** mxxnpy | **Email:** contato@moonops.dev | **Repositório:** https://github.com/mxxnpy/MoonOps

## Estrutura do Projeto

A solução segue uma arquitetura DDD limpa com clara separação de responsabilidades:

```
MoonOps/
├── src/
│   ├── MoonOps.Api/     # Camada API - endpoints HTTP e middleware
│   │   ├── Controllers/# Controllers organizados hierarquicamente
│   │   │   ├── V1/      # Controllers da versão 1
│   │   │   │   ├── HealthController.cs
│   │   │   │   ├── IntegratorsController.cs
│   │   │ │   └── VersionController.cs    # Discovery da V1
│   │ │   ├── V2/       # Controllers da versão 2
│   │   │   │   └── VersionController.cs# Discovery da V2
│   │   │   └── VersionedControllerBase.cs  # Base para versionamento
│   │   ├── Configuration/    # Configurações da API (OpenAPI, Scalar)
│   │   └── Program.cs        # Ponto de entrada da aplicação
│   ├── MoonOps.Application/       # Camada de Aplicação - orquestração da lógica de negócio
│   │   ├── Configuration/         # Configuração de DI
│   │   ├── Repositories/      # Repositórios temporários em memória
│   │   ├── Services/   # Serviços de aplicação e integrações
│   │   │   ├── IntegratorService.cs
│   │   │   └── MottuServices/     # Serviços específicos Mottu
│   │   │       └── KeyCloak/      # Autenticação KeyCloak
│   │   └── Handlers/     # Handlers HTTP e middleware customizado
│   ├── MoonOps.Domain/            # Camada de Domínio - entidades e regras de negócio
│   │   ├── Entities/     # Entidades do domínio
│   │   ├── Models/          # Modelos para transferência de dados
│   │   ├── Interfaces/    # Interfaces de repositório e serviços
│   │   └── ValueObjects/     # Objetos de valor
│   └── MoonOps.Infrastructure/    # Camada de Infraestrutura - persistência e serviços externos
│       ├── Data/       # Contexto de banco e configurações
│       └── Repositories/          # Implementações de repositório
└── tests/
    └── MoonOps.Tests/             # Testes unitários e de integração
```

## Organização da Documentação API

O Scalar UI está configurado com organização hierárquica em `/docs`:

```
docs/
├── v1/        # Grupo principal versão 1
│   ├── health        # Endpoints de saúde
│   │   ├── GET /api/v1/health     # Status básico
│   │   └── GET /api/v1/health/detailed     # Status detalhado
│   ├── integrators/         # Endpoints de integradores
│   │   ├── GET    /api/v1/integrators
│   │   ├── GET    /api/v1/integrators/{id}
│   │   ├── POST   /api/v1/integrators
│   │   ├── PUT    /api/v1/integrators/{id}
│   │   └── DELETE /api/v1/integrators/{id}
│   └── version/      # Discovery e informações da versão
│       ├── GET /api/v1/version              # Info completa da V1
│       └── GET /api/v1/version/controllers  # Lista controllers V1
└── v2/       # Grupo principal versão 2 (exemplo)
    └── version/       # Discovery da V2
        └── GET /api/v2/version       # Info da V2
```

## Funcionalidades

- ✅ Arquitetura Domain-Driven Design (DDD)
- ✅ Clean Architecture com inversão de dependência
- ✅ Documentação OpenAPI/Swagger com Scalar UI organizada hierarquicamente
- ✅ Versionamento automático de controllers
- ✅ Controllers de discovery de versão (lista endpoints automaticamente)
- ✅ Endpoint de health check único e bem definido
- ✅ Health check detalhado com informações do sistema
- ✅ CRUD completo para integradores
- ✅ Repositório em memória para desenvolvimento ágil
- ✅ Padrão Repository extensível
- ✅ Integração com serviços Mottu (autenticação KeyCloak como cliente)
- ✅ Middleware customizado para autenticação e handlers HTTP
- ✅ Versionamento de API estruturado (V1 implementada, V2 preparada)
- ✅ Injeção de dependência configurada
- ✅ Documentação abrangente com XML summaries
- ✅ Sistema integrador (consome APIs externas, não expõe autenticação)

## Começando

### Pré-requisitos

- .NET 9.0 SDK ou superior
- Visual Studio 2022 ou VS Code (opcional)

### Construindo a Solução

```bash
dotnet build
```

### Executando a Aplicação

```bash
cd src/MoonOps.Api
dotnet run
```

A API estará disponível em:
- HTTP: `http://localhost:5000`
- HTTPS: `https://localhost:5001`

No modo de desenvolvimento, você pode acessar:
- **Scalar UI**: `https://localhost:5001/docs`
- **OpenAPI JSON**: `https://localhost:5001/openapi/v1.json`

### Executando Testes

```bash
dotnet test
```

## Endpoints da API

### Health Check (v1)
- **GET** `/api/v1/health` - Status básico da API
- **GET** `/api/v1/health/detailed` - Status detalhado com informações do sistema

### Integradores (v1/integrators)
- **GET** `/api/v1/integrators` - Lista todos os integradores
- **GET** `/api/v1/integrators/{id}` - Obtém integrador específico
- **POST** `/api/v1/integrators` - Cria novo integrador
- **PUT** `/api/v1/integrators/{id}` - Atualiza integrador existente
- **DELETE** `/api/v1/integrators/{id}` - Remove integrador

### Discovery e Versionamento (v1/version)
- **GET** `/api/v1/version` - **Informações completas da V1 com lista de todos os controllers registrados**
- **GET** `/api/v1/version/controllers` - **Lista simplificada de controllers disponíveis na V1**

### Discovery V2 (v2/version) - Exemplo
- **GET** `/api/v2/version` - Informações da versão 2 (exemplo para futuro)

## Versionamento Automático

### Sistema de Discovery de Versões
O MoonOps possui **controllers de versionamento** que automaticamente descobrem e listam todos os endpoints registrados em cada versão:

```json
// GET /api/v1/version - Exemplo de resposta
{
  "version": "v1",
  "apiVersion": "1.0.0",
  "controllers": {
    "health": [
      { "method": "GET", "path": "/api/v1/health" },
      { "method": "GET", "path": "/api/v1/health/detailed" }
    ],
    "integrators": [
      { "method": "GET", "path": "/api/v1/integrators" },
  { "method": "POST", "path": "/api/v1/integrators" },
      { "method": "GET", "path": "/api/v1/integrators/{id}" },
      { "method": "PUT", "path": "/api/v1/integrators/{id}" },
      { "method": "DELETE", "path": "/api/v1/integrators/{id}" }
    ]
  },
  "totalControllers": 3,
  "totalEndpoints": 8,
  "documentation": "/docs"
}
```

### Como Criar Novos Controllers

#### **Para V1:**
```csharp
[ApiExplorerSettings(GroupName = "v1/exemplo")]
public class ExemploController : V1ControllerBase
{
    [HttpGet] // Rota automática: api/v1/exemplo
    public IActionResult Get() => Ok("V1 Example");
}
```

#### **Para V2:**
```csharp
[ApiExplorerSettings(GroupName = "v2")]
public class ExemploController : V2ControllerBase  
{
    [HttpGet] // Rota automática: api/v2/exemplo
    public IActionResult Get() => Ok("V2 Example");
}
```

### Estrutura de Versionamento
- **V1ControllerBase** - Base para V1 com `[Route("api/v1/[controller]")]`
- **V2ControllerBase** - Base para V2 com `[Route("api/v2/[controller]")]`
- **VersionController** - Discovery automático de endpoints por versão

## Arquitetura de Integração

### MoonOps como Sistema Integrador
O MoonOps atua como um **sistema integrador**, ou seja:
- **Consome** APIs de terceiros (como Mottu via KeyCloak)
- **Não expõe** endpoints de autenticação próprios
- **Gerencia** tokens automaticamente via `AuthTokenKeeperHandler`
- **Processa** e transforma dados entre sistemas
- **Fornece** CRUD para gerenciar integradores cadastrados
- **Oferece discovery** de versões e endpoints automaticamente

### Fluxo de Autenticação
1. `KeyCloakService` obtém tokens dos serviços Mottu
2. `AuthTokenKeeperHandler` injeta tokens automaticamente nas requisições
3. Controllers processam dados sem se preocupar com autenticação

### Persistência Temporária
- **InMemoryRepository**: Implementação thread-safe para desenvolvimento
- **Dados em memória**: Perdidos ao reiniciar a aplicação
- **Substituível**: Será trocado por persistência real (EF Core, etc.)

## Próximos Passos

- Implementar persistência real (Entity Framework Core)
- Adicionar validação de modelos
- Implementar testes unitários
- Configurar logging estruturado
- Adicionar controllers de negócio na V1
- Implementar funcionalidades da V2
- Adicionar autenticação/autorização se necessário

## Autor e Contato

**Desenvolvedor:** mxxnpy  
**Email:** contato@moonops.dev  
**Repositório:** https://github.com/mxxnpy/MoonOps

## Licença

Este projeto está licenciado sob a Licença MIT.