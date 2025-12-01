Este proyecto es una API construida en .NET 10 con Visual Studio 2026, siguiendo principios de Arquitectura Limpia, con soporte para:

CRUD de productos

Registro de ventas con cálculo de totales

Almacenamiento de imágenes (mock local)

Autenticación JWT (token fake)

Swagger con protección Bearer

EF Core + SQL Server

Manejo global de errores

Logging estructurado con Serilog

Arquitectura
src/
├── WebApi/           → Controladores, Swagger, Middleware, Configuración
├── Application/      → Interfaces, DTOs, Casos de uso
├── Domain/           → Entidades (Sale, SaleItem, Product)
└── Infrastructure/   → EF Core, DbContext, Repositorios, Storage


Patrones usados:

Repository Pattern

CQRS simple

Dependency Injection

Serilog Logging

Middleware global de excepciones

Requerimientos

Visual Studio 2026

SDK .NET 10

SQL Server (local o Docker)

Node 20+ si se usa frontend Angular

Ejecución

Crear base de datos con el script incluido más abajo.

Abrir solución en Visual Studio 2026.

Restaurar paquetes NuGet.

Ejecutar con F5.

Acceder a Swagger:

https://localhost:{puerto}/swagger

Token JWT (fake)

Generar token para pruebas:

GET /api/auth/fake-token


Aplicarlo en Swagger → botón Authorize

Formato:

Bearer {token}

Endpoints principales
Productos
Método	Endpoint
GET	/api/products
GET	/api/products/{id}
POST	/api/products
PUT	/api/products/{id}
DELETE	/api/products/{id}
Ventas
Método	Endpoint
POST	/api/sales
GET	/api/sales
GET	/api/sales/{id}
🪵 Logging

Serilog configurado con:

Consola

Archivo JSON estructurado:

/logs/log.ndjson

Manejo Global de Excepciones

Middleware captura:

Mensaje

Traza

Path

Timestamp

TraceId

Y devuelve respuesta JSON uniforme.

Script SQL – Base de Datos Completo
CREATE DATABASE PruebaTecnicaDb;
GO
USE PruebaTecnicaDb;
GO

IF OBJECT_ID('dbo.SaleItems', 'U') IS NOT NULL DROP TABLE dbo.SaleItems;
IF OBJECT_ID('dbo.Sales', 'U') IS NOT NULL DROP TABLE dbo.Sales;
IF OBJECT_ID('dbo.Products', 'U') IS NOT NULL DROP TABLE dbo.Products;
GO

CREATE TABLE Products (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(200) NOT NULL,
    Price DECIMAL(18,2) NOT NULL,
    Stock INT NOT NULL,
    ImageUrl NVARCHAR(500),
    CreatedAt DATETIME2 NOT NULL DEFAULT SYSDATETIME(),
    UpdatedAt DATETIME2 NULL
);

CREATE INDEX IX_Products_Name ON Products(Name);

CREATE TABLE Sales (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    SaleDate DATETIME2 NOT NULL DEFAULT SYSDATETIME(),
    Total DECIMAL(18,2) NOT NULL DEFAULT 0,
    CreatedAt DATETIME2 NOT NULL DEFAULT SYSDATETIME(),
    UpdatedAt DATETIME2 NULL
);

CREATE TABLE SaleItems (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    SaleId INT NOT NULL,
    ProductId INT NOT NULL,
    UnitPrice DECIMAL(18,2) NOT NULL,
    Quantity INT NOT NULL,
    CreatedAt DATETIME2 NOT NULL DEFAULT SYSDATETIME(),
    UpdatedAt DATETIME2 NULL,
    FOREIGN KEY (SaleId) REFERENCES Sales(Id) ON DELETE CASCADE,
    FOREIGN KEY (ProductId) REFERENCES Products(Id)
);

CREATE INDEX IX_SaleItems_SaleId ON SaleItems(SaleId);
CREATE INDEX IX_SaleItems_ProductId ON SaleItems(ProductId);

INSERT INTO Products (Name, Price, Stock) VALUES
('Laptop Lenovo', 2500.00, 10),
('Mouse Logitech', 80.00, 50),
('Teclado Mecánico', 150.00, 20);
GO

Paquetes NuGet utilizados

Swashbuckle.AspNetCore 6.6.2

Microsoft.AspNetCore.Authentication.JwtBearer 10.0.0

Serilog.AspNetCore

Serilog.Sinks.Console

Serilog.Sinks.File

System.IdentityModel.Tokens.Jwt

EF Core (en Infrastructure)

✔ Estado Final

La API está completamente funcional, documentada y estructurada de forma profesional utilizando:

.NET 10

Arquitectura Limpia

Swagger

JWT

EF Core

Serilog

ClientApp/
├── app/
│   ├── app.component.ts
│   ├── app.routes.ts
│   ├── services/
│   ├── interceptors/
│   └── pages/
│        ├── login/
│        ├── dashboard/
│        ├── products/
│        ├── sales/
│        └── sales-report/
 