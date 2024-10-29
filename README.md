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
