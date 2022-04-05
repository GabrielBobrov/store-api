# Instructions to run the project


In this project the postgreSQL database is used, it will be necessary to have installed PgAdmin or similar

Create a database named store, remember to change the connection string in src/1-Store.API/appsettings.json to your postgre credentials


To update the database, open the project in the terminal and use this command:

* dotnet ef database update --project src/4-Store.Infra/Store.Infra.csproj

After the database is configured, start the 1-Store.API project with the command: 

* dotnet run --project src/1-Store.API/Store.API.csproj

Or if using visual studio, put the same project as startup project and start

In this project, swagger is used, all endpoints will be available here:

* https://localhost:7045/swagger/index.html

To make calls you will need to be authenticated, use the json below to authenticate and generate a bearer token

```json
{
  "login": "ilia",
  "password": "digital"
}
```
Then paste the token like this in authorize button:

 <img width="673" alt="image" src="https://user-images.githubusercontent.com/63800210/161829833-43923456-dbed-41eb-aaea-6e786ff3fc84.png">

Or if you are using insomnia or postman, add auth with type Bearer Token in the call and paste the generated token, like this:

<img width="607" alt="image" src="https://user-images.githubusercontent.com/63800210/161830213-d3a453aa-9a22-4d2e-8253-a36567879c58.png">

After being authenticated, you can use all endpoints





