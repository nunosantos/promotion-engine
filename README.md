# promotion-engine
An engine for promotion calculator

## Table of Contents
[1. Getting Started](#1-getting-started)

[2. Technologies](#2-technologies)

[3. Comments](#3-comments)

## 1. Getting Started

1. If running file locally please install the latest .NET SDK

2. Git clone this repo 

2. Build Projects
``` csharp
dotnet build

```

3. Run Tests

``` csharp

dotnet test

```

4. Launch API in visual studio and read API documentation (ie: https://localhost:PORT/swagger/index.html)

![api-doc](https://user-images.githubusercontent.com/3398578/127232096-848dff13-9a0a-43ce-a799-f27968f7c6f0.png)

5. Start by posting a set of items to the API

6- Post an order request. Please note that the order matters given that all orders are stored in memory without the use of a supporting JSON, database, etc. 

## 2. Technologies
The application has been designed on .NET 5.0

## 3. Comments
- The application has been designed to work with Github workflows as a means to continuously integrate into the main branch with passing tests
- The architecture was based on Ardalis Endpoints as a means to expose APIs and preserve single responsibility principles: https://github.com/ardalis/ApiEndpoints
- All data is in memory. No database in memory has been designed for this test
- The application consists of integration, domain and functional tests

Enjoy c",)
