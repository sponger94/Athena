# Athena

[![build status](https://sponger94.gitlab.io/Athena/badges/pomodoro_api_build.svg)](https://gitlab.com/sponger94/Athena/badges/develop/pipeline.svg)
[![build status](https://sponger94.gitlab.io/Athena/badges/tasks_api_build.svg)](https://gitlab.com/sponger94/Athena/badges/develop/pipeline.svg)

### Sample User Interface

<img src="https://user-images.githubusercontent.com/45746997/57596429-1457f980-7564-11e9-8de7-d1fd92a1c9ae.png" width="280"/> <img src="https://user-images.githubusercontent.com/45746997/57596413-06a27400-7564-11e9-9e93-4444e66d7aac.png" width="280"/> <img src="https://user-images.githubusercontent.com/45746997/57596419-0c985500-7564-11e9-9fba-7cfa5717b773.png" width="280"/> 


> Note: These images are mockups and made only for server-side requirenment clarifications only.


### Features

- **Tasks management** - Manage your tasks and count your time towards them. You can take notes, add sub-tasks, attachments and track the status of each of them.
- **Reminders** - Set reminders on any task and get notified. Also settings date allows you to track your tasks by calendar.
- **Labels** - Mark your tasks by adding labels to them.
- **Pomodoro** - Start pomodoro against a task and track your time. Configurable pomodoro duration as well as breaks timer.
- **Statistics** - Track your pomodoro history and statistics.

## Architecture Overview

Cross-platform microservices app based on .NET Core and running on Docker host. This app is made for sample purposes only and may not fully follow production ready code. Tasks microservice is made by using DDD / CQRS architecture only for sample purposes, however it's mostly only CRUD service. 

> It's important to understand that CQRS and most DDD patterns (like DDD layers or a domain model with aggregates) are not architectural styles, but only architecture patterns. Microservices, SOA, and event-driven architecture (EDA) are examples of architectural styles. They describe a system of many components, such as many microservices. CQRS and DDD patterns describe something inside a single system or component; in this case, something inside a microservice.
From [this book](https://dotnet.microsoft.com/download/thank-you/microservices-architecture-ebook) page 186.

### Image Description

![Architecture-01](https://user-images.githubusercontent.com/45746997/57594712-ddc9b100-755a-11e9-9478-d657d71480b9.png)

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
|  1 | Tasks  | Manages user tasks, labels, attachments etc. Follows DDD / CQRS architecture. | [View](https://petstore.swagger.io/?url=https://raw.githubusercontent.com/sponger94/Athena/develop/src/Services/Tasks/Tasks.API/api-docs/v1/swagger.json "View") | [![build status](https://sponger94.gitlab.io/Athena/badges/tasks_api_build.svg)](https://gitlab.com/sponger94/Athena/badges/develop/pipeline.svg) |
|  2 | Pomodoro  | Serves for tracking user pomodoro's(towards which task, duration and date)  |  [View](https://petstore.swagger.io/?url=https://raw.githubusercontent.com/sponger94/Athena/develop/src/Services/Pomodoro/Pomodoro.API/api-docs/v1/swagger.json) | [![build status](https://sponger94.gitlab.io/Athena/badges/pomodoro_api_build.svg)](https://gitlab.com/sponger94/Athena/badges/develop/pipeline.svg) |
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

## Contributing

1. Fork it!
2. Create your feature branch: `git checkout -b my-new-feature`
3. Commit your changes: `git commit -am 'Add some feature'`
4. Push to the branch: `git push origin my-new-feature`
5. Submit a pull request

## Licence

Code released under the [MIT license](https://github.com/sponger94/Athena/blob/develop/LICENSE).
