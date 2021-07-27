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
## 2. Technologies
The application has been designed on .NET 5.0

## 3. Comments
- The application has been designed to work with Github workflows as a means to continuously integrate into the main branch with passing tests
- The architecture was based on Ardalis Endpoints as a means to expose APIs and preserve single responsibility principles: https://github.com/ardalis/ApiEndpoints
- All data is in memory. No database in memory has been designed for this test
- The application consists of integration, domain and functional tests

Enjoy c",)
