# RolX
The time tracking tool from developers for developers.

# Getting Started

## Prerequisites

### Database

RolX requires a MariaDB instance >=10.3. The easiest way to to get one, is using docker and the provided docker-compose file under ``tools/rolx-test-db``

```bash
$ cd tools/rolx-test-db
$ docker compose up -d
```

### .NET SDK
RolX requires the .NET 6 SDK version 6.0.402 (or later patch). Download and install it from https://dotnet.microsoft.com/en-us/download/dotnet/6.0

### node.js
RolX requires node.js version 12.20.x/14.15.x/16.10.x or later minor, 18.12.x might work as well. Download and install it from https://nodejs.org/en/download/

## Build and Run

### Backend

```bash
$ cd source/RolXServer/RolXServer
$ dotnet build
$ dotnet run
```

### Frontend

```bash
$ cd source/rolx-client
$ npm install
$ npm start
```

- Open a browser and navigate to http://localhost:4200
- Sign-in using your Google account
