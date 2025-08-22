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

//add a migration for future run
builder.Services.AddFluentMigratorCore()
    .ConfigureRunner(rb => rb
        .AddSqlServer()
        .WithGlobalConnectionString(builder.Configuration.GetConnectionString("Default"))
        .ScanIn(typeof(CreatePersonTable).Assembly).For.Migrations())
    .AddLogging(lb => lb.AddFluentMigratorConsole());

var app = builder.Build();

if (args.Contains("--migrate"))
{
    using var scope = app.Services.CreateScope();
    var runner = scope.ServiceProvider.GetRequiredService<IMigrationRunner>();

    // Optional: list discovered migrations to confirm assembly scanning
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
