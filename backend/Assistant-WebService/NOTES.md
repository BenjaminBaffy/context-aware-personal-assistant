# Notes to the WS Backend

There are some issues with axios code typescript generation (very small). It is turned off.
Add this back to the API.csproj in order to work.

``` xml
<Target Name="NSwag" AfterTargets="PostBuildEvent" Condition=" '$(Configuration)' == 'Debug' ">
    <Message Importance="High" Text="$(NSwagExe_Net50) run nswag.json /variables:Configuration=$(Configuration)" />

    <Exec WorkingDirectory="$(ProjectDir)" EnvironmentVariables="ASPNETCORE_ENVIRONMENT=Development" Command="$(NSwagExe_Net50) run nswag.json /variables:Configuration=$(Configuration)" />

    <Delete Files="$(ProjectDir)\obj\$(MSBuildProjectFile).NSwag.targets" /> <!-- This thingy trigger project rebuild -->
</Target>
```
