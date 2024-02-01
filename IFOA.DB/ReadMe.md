# to run the container
docker run -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=yourStrong@@@@@Password" -p 1433:1433 -d mcr.microsoft.com/mssql/server:2019-latest

#To add new migration
dotnet ef migrations add <MIGRAZIONE NAME>

#To update database
dotnet ef database update --connection "Server=localhost,1433;Database=ifoa;User Id=sa;Password=yourStrong@@@@@Password;TrustServerCertificate=True;" 