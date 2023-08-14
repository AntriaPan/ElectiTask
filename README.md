# ElectiTask

# Bookstore Application Setup Guide

This guide provides comprehensive instructions for setting up and running the Bookstore application, a hypothetical bookstore management system. The application features user authentication, book management, API documentation using Swagger, logging with NLog, testing, and Docker deployment. The application is built using .NET 6+, React (for the frontend), and Entity Framework.

## Table of Contents

1. [Prerequisites](#prerequisites)
2. [Getting Started](#getting-started)
3. [Database Setup](#database-setup)
4. [API Development](#api-development)
5. [Swagger Documentation](#swagger-documentation)
6. [Logging with NLog](#logging-with-nlog)
7. [Testing](#testing)
8. [Frontend Development](#frontend-development)
9. [Docker Deployment](#docker-deployment)
10. [Deliverables](#deliverables)

## Prerequisites

Before you begin, ensure that you have the following tools installed on your system:

- [.NET 6 SDK](https://dotnet.microsoft.com/download/dotnet/6.0)
- [Docker](https://docs.docker.com/get-docker/)

## Getting Started

1. **Clone the repository:**

```bash
git clone https://github.com/AntriaPan/ElectiTask.git
cd https://github.com/AntriaPan/ElectiTask.git
```
1. Navigated to the appropriate subdirectories for the API (BookstoreApi) and the frontend (BookstoreFrontend).

## Database Setup
1. Designed a database to store books and user information (by yourself locally).
2. Set up your database connection string in the appsettings.json file of the API project.

## API Development
1. Developed the API using .NET 6+ and Entity Framework.
2. Implemented JWT authentication for protecting book-related endpoints.
3. Implemented user registration and login endpoints.

## Swagger Documentation
1. Implemented Swagger for API documentation.
2. Accessed the Swagger UI at http://localhost:5283/swagger.

## Logging with NLog
1. Implemented logging using NLog in your API project.
2. Configured NLog to log important events (accessed from the folder on your system: "C:\logs").

## Testing
1. Wrote unit tests for the business logic.
2. Wrote integration tests for the API endpoints.
3. Ran tests using the dotnet test command.

## Frontend Development
1. Developed the frontend using React.
2. Implemented user interfaces for viewing, adding, updating, and deleting books.
3. Implemented user registration and login functionality.

## Docker Deployment
Dockerized the application:

Created Dockerfiles for the API (BookstoreApi) and frontend (BookstoreFrontend).
Created a docker-compose.yml file to define a multi-container application.
Built and ran the Docker containers:

```bash
Copy code
docker-compose up```
Access the frontend at http://localhost:3000.
