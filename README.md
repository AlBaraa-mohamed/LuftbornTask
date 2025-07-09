# Dotnet Clean Architecture

Dotnet Clean Architecture is a Razor Pages project built with ASP.NET Core targeting .NET 9. It provides a product management system where users can create, edit, delete, and view products. The project also supports different pricing strategies for products.

## Features

- User authentication and product management.
- CRUD operations for products.
- Pricing strategies: Regular, Discount, and Premium.
- Razor Pages for a clean and responsive UI.
- Dependency injection for services and repositories.

## Technologies Used

- **ASP.NET Core**: For building the web application.
- **Entity Framework Core**: For database access and migrations.
- **Razor Pages**: For the user interface.
- **C# 13.0**: The programming language used.
- **.NET 9**: The target framework.

## Installation

1. Clone the repository:
2. Navigate to the project directory:
3. Restore dependencies:
4. Apply database migrations:
5. Run the application:

## Usage

1. Navigate to the application in your browser at `https://localhost:5001`.
2. Log in or register to manage your products.
3. Use the "Create", "Edit", and "Delete" options to manage products.
4. Select a pricing strategy when creating or editing a product.

## Project Structure

- **Controllers**: Handles HTTPS requests and responses.
- **Views**: Razor Pages for the UI.
- **Services**: Business logic for managing products.
- **Repositories**: Data access layer for interacting with the database.
- **Strategies**: Implements different pricing strategies.

## Pricing Strategies

The project supports the following pricing strategies:
- **Regular**: Default pricing.
- **Discount**: Applies a discount to the product price.
- **Premium**: Adds a premium to the product price.
  
