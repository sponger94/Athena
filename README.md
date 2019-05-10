# Athena
[WIP] Build status

## Description

This is a sample task and time management assistant made with .NET Core Microservices that follows Pomodoro Technique.

[WIP] Screenshots

### Features

- **Tasks management** - Manage your tasks and count your time towards them. You can take notes, add sub-tasks, attachments and track the status of each of them.
- **Reminders** - Set reminders on any task and get notified. Also settings date allows you to track your tasks by calendar.
- **Labels** - Mark your tasks by adding labels to them.
- **Pomodoro** - Start pomodoro against a task and track your time. Configurable pomodoro duration as well as breaks timer.
- **Statistics** - Track your pomodoro history and statistics.

## Architecture Overview

Cross-platform microservices app based on .NET Core and running on Docker host. This app is made for sample purposes only and may not fully follow production ready code. Tasks microservice is made by using DDD / CQRS architecture only for sample purposes, however it's mostly only CRUD service. 

> It's important to understand that CQRS and most DDD patterns (like DDD layers or a domain model with aggregates) are not architectural styles, but only architecture patterns. Microservices, SOA, and event-driven architecture (EDA) are examples of architectural styles. They describe a system of many components, such as many microservices. CQRS and DDD patterns describe something inside a single system or component; in this case, something inside a microservice. (TODO: Link to book)

### Image Description
[WIP Architecture Image]

### OpenAPI

#### What Is OpenAPI?
>OpenAPI Specification (formerly Swagger Specification) is an API description format for REST APIs. An OpenAPI file allows you to describe your entire API, including:
	• Available endpoints (/users) and operations on each endpoint (GET /users, POST /users)
	• Operation parameters Input and output for each operation
	• Authentication methods
	• Contact information, license, terms of use and other information.
API specifications can be written in YAML or JSON. The format is easy to learn and readable to both humans and machines. The complete OpenAPI Specification can be found on GitHub: OpenAPI 3.0 Specification.
From <https://swagger.io/docs/specification/about/>

### Microservice List

|  No. |  Service |  Description | OpenAPI  | Build Status |
| ------------ | ------------ | ------------ | ------------ | ------------ |
|  1 | Tasks  | Manages user tasks, labels, attachments etc. Follows DDD / CQRS architecture. | [View](http://https://petstore.swagger.io/?url=https://raw.githubusercontent.com/sponger94/Athena/develop/src/Services/Tasks/Tasks.API/api-docs/v1/swagger.json "View") | [WIP] Status |
|  2 | Pomodoro  | Serves for tracking user pomodoro's(towards which task, duration and date)  |  [View](https://petstore.swagger.io/?url=https://raw.githubusercontent.com/sponger94/Athena/develop/src/Services/Pomodoro/Pomodoro.API/api-docs/v1/swagger.json) | [WIP] Status  |
|  3 | Statistics (Soon)  | Runs for keeping statistics and analytics of users activity. | Soon  | [WIP] Status  |
|  4 | Identity Server4 (Soon)  | Stands for user authorization and user roles. | Soon  | [WIP] Status  |
|  5 | API Gateway (Soon) | Intercepts user requests and re-routes them to the corresponding microservices. | Soon  | [WIP] Status  |
|  6 | WebApp MVC (Soon) | Bridges browser requests with the backend. | Soon  | [WIP] Status  |

## Technologies Used

|  Backend | Testing | DevOps (Soon)  |
| ------------ | ------------ | ------------ |
| ASP.NET Core Web API  |  xUnit |   |
|  EF Core | Moq  |   |
| IdentityServer4  | NUnit (Soon)  |   |
| Dapper | NSubstitute (Soon)  |   |
| SQL Server |   |   |   |

## Prerequisites

[WIP] Coming soon

## Installation

[WIP] Coming soon



## Contributing

1. Fork it!
2. Create your feature branch: `git checkout -b my-new-feature`
3. Commit your changes: `git commit -am 'Add some feature'`
4. Push to the branch: `git push origin my-new-feature`
5. Submit a pull request

## Licence

Code released under TODO: [the MIT license](https://github.com/sponger94/Athena.git/).
