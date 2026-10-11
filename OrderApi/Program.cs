using Asp.Versioning;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions;
using OrderApi.Models;
using OrderApi.Repository;
using OrderApi.Repository.Interface;
using OrderApi.Services;
using OrderApi.Services.Interface;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<EcommerceContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services
    .AddApiVersioning(options =>
    {
        options.DefaultApiVersion = new ApiVersion(1, 0);
        options.AssumeDefaultVersionWhenUnspecified = false;
        options.ReportApiVersions = true;
        options.ApiVersionReader = new UrlSegmentApiVersionReader();
    })
    .AddMvc();
builder.Services.AddRateLimiter(options =>
{
    // Fix default response from 503 to 429
    options.RejectionStatusCode = StatusCodes.Status429TooManyRequests;

    // Define a "Fixed Window" policy
    options.AddFixedWindowLimiter(policyName: "fixed-policy", fixedOptions =>
    {
        fixedOptions.PermitLimit = 10;                  // Max 10 requests
        fixedOptions.Window = TimeSpan.FromSeconds(10);  // Per 10-second window
        fixedOptions.QueueLimit = 0;                    // Reject immediately if exceeded
    });
});
builder.Services.AddCors(options =>
{
    options.AddPolicy("FrontendPolicy", policy =>
    {
        policy
            .WithOrigins("http://localhost:7125")
            .WithMethods("GET", "POST", "PUT", "DELETE")
            .WithHeaders("Authorization", "Content-Type");
    });
});

builder.Services.AddScoped<IorderRepository, OrderRepository>();
builder.Services.AddScoped<IOrderService, OrderService>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    //app.MapOpenApi();
  
}
app.UseRateLimiter();
app.UseCors("FrontendPolicy");
app.UseMiddleware<OrderApi.Middleware.ExceptionHandlingMiddleware>();
app.UseMiddleware<OrderApi.Middleware.LoggingMiddleware>();
app.UseHttpsRedirection();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
