dotnet add package Dapper;
dotnet add package microsoft.entityframeworkcore.sqlserver;
dotnet add package microsoft.entityframeworkcore;
dotnet add package microsoft.data.sqlclient;


dotnet watch run     

docker stop sql_connect
docker start sql_connect