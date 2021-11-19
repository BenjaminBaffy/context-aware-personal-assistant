# Context Aware Personal Assistant

Welcome to the backend!

## How to run your backend?

### Get .NET 5.0

[Download Cross Platform SDK](https://dotnet.microsoft.com/download)

Run `dotnet --version`
Response should be at least `5.0.402`

**NOTE**: .NET 5.0 is the continuation of the .NET Core project (and on it's philosophy)

### Run the app

Get to the main directory of the GIT repository.
Run `dotnet restore` (not needed every time)
Run `dotnet build`
Optionally the unit tests can be run with `dotnet test` or `dotnet test --logger "console;verbosity=detailed"` for a more detailed answer
Run `dotnet run -p backend/Assistant-WebService/Assistant.API/`

Running the page should open the browser, and redirect to the Swagger page, where you can see the exposed

Alternatively, Visual Studio Code can be used

## IDE integration

Currently it's been tested under Visual Studio Code, but it should be seamless if you use the same.
There's a launch profile to run the service. Select the web service  in the `Run and Debug` panel. and RUN it.

### .NET Core Solution Structure

#### API

Here are the the entry point interfaces defined for the WebServer and anything WebServer related.
.NET Core MVC is based on Model-View-Controller design pattern. We keep the View party empty, as this is only a Web API, we only returns results in JSON (which can be intepreted as the View)
Controller classes accepts message coming from the FrontEnd and delegate them to the Application Layer. Routes are using pattern matching, to end up in the right method. Ex.

##### Controllers

`[Route("api/something")]` defines the entry url on class level which can be further refined additionally in any method you add.

##### Startup.cs

Startup.cs holds the configuration of the web server
It confifures the DI (Dependency Injection) at `ConfigureServices` method where you can register your service classes. You don't have to worry about creating service class instances later.
Example:
If you introduce a new interface-class pair called `IMyClass` and `MyClass`, make sure it's registered in the Startup.cs as `services.AddTransient<IMyClass, MyClass>();`. After that, you can use it anywhere using constructor injection (there are couple of examples how to use it)

Also contains the middleware configuration, which essentially the pipeline each web request go through.Ex. routing, authentication, forwarding requests to controllers.
Note that the order is fixed in the order configured at the `Configure` method.

#### Application

This is where the main application logic is happening, small classes handle requests accordingly

#### Domain

These contains the class definitions which later used to hold the data going through the application
ViewModels: the input and the output classes directly of the main API
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

- How to add a new service
- Add something to the API part
- Registering to DI

- Investigate: Use Rasa Tracker API to get / set the slots

- Finish Auth API and integrate it


### Rasa API helpers

<https://forum.rasa.com/t/is-there-any-way-to-set-slot-by-api-not-by-action/11501/5>
https://rasa.com/docs/rasa/next/reaching-out-to-user/> -> how to handle conversations