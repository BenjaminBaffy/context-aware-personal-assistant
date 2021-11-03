# Context Aware Personal Assistant

Welcome to the backend!

## How to run your backend?

### Get .NET 5.0

[Download Cross Platform SDK](https://dotnet.microsoft.com/download)

Run `dotnet --version`
Response should be at least `5.0.402`

NOTE: .NET 5.0 is the continuation of the .NET Core project (3.1 is the latest of it)

### Run the app

Get to the main directory.
Run `dotnet restore` (not needed every time)
Run `dotnet build`
Run `dotnet run -p backend/Assistant-WebService/Assistant.API/`

You'll be redirected to the Swagger page, where you can see

Or just use the Visual Studio Code

## IDE integration

Currently it's been tested under Visual Studio Code, but it should be seamless if you use the same (though recommended to use what you really like :) )

### .NET Core Solution Structure

#### API

Here are the the entry point interfaces defined for the WebServer and anything WebServer related.
.NET Core MVC is a Model-View-Controller design pattern. We keep the View party empty, as this is only a Web API.
Controller classes accepts message coming from and delegate to the Application Layer. It uses pattern matching to do it.
`[Route("api/something")]` defines the entry url on class level which can be further refined additionally in any method you add.

Startup.cs holds the configuration of the web server: Startup.cs is the DI config where you can set up your class resolution. Also contains the middlewares.

If you introduce a new interface-class pair called `IMyClass` and `MyClass`, make sure it's registered in the Startup.cs as `services.AddTransient<IMyClass, MyClass>();`. After that, you can use it anywhere using constructor injection (there are couple of examples how to use it)

#### Application

This is where the main application logic is happening, small classes handle requests accordingly

#### Domain

These contains the class definitions which later used to hold the data of the application
ViewModels: the input and the output classes directly to the main API
DataBaseModel: contains the sturcture of the database
RasaHttpModel: the communication interface with Rasa rest API

### Other useful links to start with

Firestore related
[Query examples in C#](https://cloud.google.com/firestore/docs/query-data/queries#c)
<https://cloud.google.com/dotnet/docs/reference/Google.Cloud.Firestore/latest>
<https://pieterdlinde.medium.com/netcore-and-cloud-firestore-94628943eb3c>

React related
<https://www.vivienfabing.com/react/2020/12/06/aspnetcore-simple-shared-dtos-with-react.html>

### Guides

TODO

- Add new service
- Add something to the API part
- Registering to DI
