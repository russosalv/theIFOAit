sudo docker run -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=Password!!!!!!" -p 1433:1433 --name sql1 --hostname sql1 -d mcr.microsoft.com/mssql/server:2022-latest

dotnet ef migrations add InitialCreate

dotnet ef database update --connection "Server=localhost,1433;Database=ifoa;User Id=sa;Password=Password!!!!!!;TrustServerCertificate=True;" 