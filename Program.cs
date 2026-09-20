using HRM.API.Helpers;
using HRM.API.Repository;
using HRM.API.Services;
using HRM.API.Services.Email;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddScoped<AdoHelper>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IEmailService, EmailService>();
// OpenAPI
//builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    //{
    //    app.MapOpenApi();

    //    app.UseSwaggerUI(options =>
    //    {
    //        options.SwaggerEndpoint("/openapi/v1.json", "HRM API V1");
    //    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();