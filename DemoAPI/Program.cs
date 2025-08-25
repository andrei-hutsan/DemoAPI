using DataAccess;
using DataAccess.DbAccess;
using DataAccess.Interfaces;
using DataAccess.Repositories;
using DemoAPI.Validators;
using FluentMigrator.Runner;
using FluentValidation;
using FluentValidation.AspNetCore;
using Migrations.Migrations;
using ServiceReference;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

//Validator
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssemblyContaining<PersonValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<DepartmentValidator>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddSingleton<ISqlDataAccess, SqlDataAccess>();
builder.Services.AddSingleton<IPersonRepository, PersonRepository>();
builder.Services.AddSingleton<IDepartmentRepository, DepartmentRepository>();
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

//Fluent Migrator
builder.Services.AddFluentMigratorCore()
    .ConfigureRunner(rb => rb
        .AddSqlServer()
        .WithGlobalConnectionString(builder.Configuration.GetConnectionString("Default"))
        .ScanIn(typeof(CreatePersonTable).Assembly).For.Migrations())
    .AddLogging(lb => lb.AddFluentMigratorConsole());

var app = builder.Build();

//Migrations
if (args.Contains("--migrate"))
{
    using var scope = app.Services.CreateScope();
    var runner = scope.ServiceProvider.GetRequiredService<IMigrationRunner>();

    var discovered = runner.MigrationLoader.LoadMigrations();
    Console.WriteLine($"Found {discovered.Count} migrations:");
    foreach (var m in discovered)
        Console.WriteLine($"  {m.Key} -> {m.Value.Migration.GetType().FullName}");

    runner.MigrateUp();
    Console.WriteLine("Migration completed.");
    return;
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();

app.Run();
