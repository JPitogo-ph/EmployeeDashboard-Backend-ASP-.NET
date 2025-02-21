# Employee Dashboard Backend  
This is the ASP .NET backend for the Employee Dashboard Angular project.  
That project was mainly a frontend learning experience, so please don't expect much from this web api project.

## Features  
* Full CRUD support against a single table of Employee data.
* For simplicity, database access code and error handling are stuffed in the controllers instead of using a separate data layer and action filters respectively.
* Support for CORS.

## Requirements
* Install the .NET 8 SDK from Microsoft.
* This project only supports Microsoft SQL Server as its relational database.

## Installation  
1. **Clone the Repository** locally:  
`git clone https://github.com/JPitogo-ph/EmployeeDashboard-Backend-ASP-.NET`  
`cd EmployeeDashboard-Backend-ASP-.NET`  
2. **Restore dependencies**  
`dotnet restore`  
3. **Configuration**  
- Please configure your connection string for MSSQL in appsettings.json  
- Run `dotnet ef database update` if on the .NET cli, or whatever equivalent command you need in order to run the migration and setup the database and table.  
- Please seed the employee table in the db using the employees.csv file in this repo.  
4. **Run the Project**  
`dotnet run`  
