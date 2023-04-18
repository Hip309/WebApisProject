using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using StoreEmployees.OAuth.Configuration;
using StoreEmployees.OAuth.Extensions;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);
var env = builder.Environment;
var migrationAssembly = typeof(Program).GetTypeInfo().Assembly.GetName().Name;
 
builder.Services.AddIdentityServer()
    .AddTestUsers(InMemoryConfig.GetTestUsers())
    .AddDeveloperSigningCredential() //not something we want to use in a production environment;
    .AddConfigurationStore(opt =>
    {
        opt.ConfigureDbContext = c => c.UseSqlServer(builder.Configuration.GetConnectionString("sqlConnection"),
            sql => sql.MigrationsAssembly(migrationAssembly));
    })
    .AddOperationalStore(opt =>
    {
        opt.ConfigureDbContext = o => o.UseSqlServer(builder.Configuration.GetConnectionString("sqlConnection"),
            sql => sql.MigrationsAssembly(migrationAssembly));
    });

builder.Services.AddControllersWithViews();



var app = builder.Build();
app.MigrateDatabase();

if (env.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseStaticFiles();
app.UseRouting();

app.UseIdentityServer();

app.UseAuthorization();
app.UseEndpoints(endpoints =>
{
    endpoints.MapDefaultControllerRoute();
});



app.Run();
