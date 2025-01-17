# ProductCatalog
A RESTful API service that provides endpoints for managing an e-commerce product catalog. The application supports listing products, retrieving a single product by ID, and updating product description. It includes support for API versioning and Swagger documentation.
SQLLite is used for simplicity and xUnit Tests with Moq easily switchable via appsettings.json in ProductCatalog project.

## Prerequisites
Ensure the following are installed on your machine:
.NET SDK: Download .NET SDK (latest LTS version recommended)
SQLite: SQLite is included in the application and requires no external installation.
Visual Studio or Visual Studio Code: For development and debugging.

## How to run
1. Clone the repository
2. Open the solution in Visual Studio
1. Run unit tests by dotnet test command or in Test Explorer
3. Run ProductCatalog project
4. Access at: https://localhost:7268/swagger