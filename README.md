# GitHubRepositorySearch API
## Demo.Api
## Overview

This is a C# REST API built with .NET Core 7 that implements user authentication using JWT (JSON Web Tokens). The API demonstrates the use of middleware and dependency injection to manage requests and services efficiently. Additionally, it integrates Swagger for API documentation and Jetbear for enhanced API testing features.

## Features

- JWT-based user authentication
- Middleware for request handling
- Dependency injection for service management
- Swagger for API documentation
- Jetbear for enhanced API testing and interaction

## Prerequisites

Before you begin, ensure you have the following installed:

- Visual Studio 2022
- .NET Core SDK (v7 or later)

## Installation

1. **Clone the repository:**

   ```bash
   git clone <repository-url>
   cd <project-directory>

2. **Restore dependencies:**
Run the following command to restore the required packages:
   ```bash
   dotnet restore

## Running the API
To run the API, navigate to the Demo.Api project directory and execute the following command:

    ```bash
    dotnet run
Once the API is running, you can access the Swagger documentation by navigating to http://localhost:5000/swagger (or the port specified in your configuration).

## Endpoints
The main endpoint to run the API is located in:

Demo.Api > Controllers > GitHubRepositoryController

## How It Works

### Authentication
- The API uses JWT for authenticating users. Upon successful login, a JWT token is generated and returned to the user.
- This token must be included in the Authorization header of subsequent requests to access protected routes.

### Middleware
- Middleware is used to handle authentication, logging, and other cross-cutting concerns, allowing for cleaner and more maintainable code.

### Dependency Injection
- Services are injected into controllers via dependency injection, promoting a loosely coupled architecture.

### Swagger Integration
- Swagger provides an interactive API documentation page, allowing you to test endpoints easily. Access it at [http://localhost:5000/swagger](http://localhost:5000/swagger).

### Jetbear Integration
- Jetbear is added to the Swagger documentation to provide enhanced API testing features, allowing for a more streamlined testing process directly from the Swagger UI.

