A user management system implemented with c# .net core, entity framework and sql server. It allows creating new users and groups and assigning groups for these users.
To run the system, follow these steps:
1- Update the "default connection" connection string in appsettings.json
2- To initialize the migration, use the following command from the dotnet CLI from inside the user-management directory:
 dotnet ef migrations add init
 3- To create the required tables in the database, use the following command:
 dotnet ef database update
 4- To test the endpoints using swagerUI run the following command:
 dotnet watch run
 5-The default admin user has the credentials admin:Admin123!

