using DataAccess;
using DataAccess.DbAccess;
using DataAccess.Interfaces;
using DemoAPI;
using DemoAPI.Validators;
using FluentMigrator.Runner;
using FluentValidation;
using FluentValidation.AspNetCore;
using Migrations.Migrations;
using ServiceReference;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//add a migration for future run
builder.Services.AddFluentMigratorCore()
    .ConfigureRunner(rb => rb
        .AddSqlServer()
        .WithGlobalConnectionString(
            builder.Configuration.GetConnectionString("Default"))
        .ScanIn(typeof(AddEmailToPerson).Assembly).For.Migrations())
    .AddLogging(lb => lb.AddFluentMigratorConsole());


builder.Services.AddControllers();

builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssemblyContaining<PersonValidator>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddSingleton<ISqlDataAccess, SqlDataAccess>();
builder.Services.AddSingleton<IPersonRepository, PersonRepository>();
builder.Services.AddSingleton<IUnitOfWork, UnitOfWork>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    var xmlFile = "DemoAPI.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    options.IncludeXmlComments(xmlPath);
});

builder.Services.AddScoped<Func<PersonServiceClient>>(_ =>
{
    return () => new PersonServiceClient(
        PersonServiceClient.EndpointConfiguration.BasicHttpBinding_IPersonService,
        "http://localhost:63510/PersonService.svc");
});

var app = builder.Build();

//run the migration
using (var scope = app.Services.CreateScope())
{
    var runner = scope.ServiceProvider.GetRequiredService<IMigrationRunner>();
    runner.MigrateUp(); //applies Up method to DB
    //runner.MigrateDown(-migrationId-); //rollsback all migrations till the -migrationId- inclusive
    //runner.Rollback(1); // rollback 1 stetp of migrations
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();

app.Run();
