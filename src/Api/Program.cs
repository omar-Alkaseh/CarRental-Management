using Car_Rental_Management.Common.Filters;
using CarRental.Api.Exceptions;
using CarRental.Application;
using CarRental.Application.Common.Results;
using CarRental.Infrastructure;
using Microsoft.OpenApi.Models;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers(options =>
{
    options.Filters.Add<ResultActionFilter>();
});

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Car Rental Management API",
        Version = "v1",
        Description = "API for managing vehicles, reservations, rentals, payments and customers.",
        Contact = new OpenApiContact
        {
            Name = "omar alkaseh",
            Email = "omarelkasih@gmail.com"
        }
    });

    var xmlFile =
        $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";

    var xmlPath =
        Path.Combine(AppContext.BaseDirectory, xmlFile);

    options.IncludeXmlComments(xmlPath);
});

builder.Services.AddProblemDetails();

builder.Services.AddExceptionHandler<GlobalExceptionHandler>();

builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

var app = builder.Build();

app.UseExceptionHandler();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.MapGet("/authors/{author}", async (string author, HttpContext context) =>
{
   var data = new
   {
       Id = context.TraceIdentifier,
       Scheme = context.Request.Scheme,
       Host = context.Request.Host,
       Method = context.Request.Method,
       Path = context.Request.Path,
       Query = context.Request.Query,
       Headers = context.Request.Headers,
       RouteValues = context.Request.RouteValues,
       Body = await new StreamReader(context.Request.Body).ReadToEndAsync()
   };

   return Results.Ok(data);
});

app.Run();