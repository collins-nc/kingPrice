var builder = DistributedApplication.CreateBuilder(args);

var sqlPassword = builder.AddParameter("sql-password", secret: true);

var sqlServer = builder
    .AddSqlServer("sql-serve", password: sqlPassword)
    .WithHostPort(11434)
    .WithLifetime(ContainerLifetime.Persistent)
    .WithDataVolume("sql-kingprice-aspire");

var sqlDb = sqlServer.AddDatabase("kingpricedb");

builder.AddProject<Projects.KingPrice_Api>("api", launchProfileName:"http")
    .WithReference(sqlDb)
    .WaitFor(sqlDb);

builder.Build().Run();
