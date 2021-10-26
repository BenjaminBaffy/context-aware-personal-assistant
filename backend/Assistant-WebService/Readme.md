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
Run `dotnet run -p backend/assistant_core/Assistant.API/`

You'll be redirected to the Swagger page, where you can see

Or just use the Visual Studio Code

## IDE integration

Currently it's been tested under Visual Studio Code, but it should be seamless if you use the same (though recommended to use what you really like :) )

### Solution Structure

TODO

#### API

#### Domain

### Other useful links to start with

[Query examples in C#](https://cloud.google.com/firestore/docs/query-data/queries#c)

<https://cloud.google.com/dotnet/docs/reference/Google.Cloud.Firestore/latest>
<https://pieterdlinde.medium.com/netcore-and-cloud-firestore-94628943eb3c>

## TODO (notes not to be forgotten)

More secure credentials
Set up unit test project properly
Finish Database Service

Start to create real DB model
