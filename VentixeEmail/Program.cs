using Azure.Communication.Email;
using VentixeEmail.Services;


var builder = WebApplication.CreateBuilder(args);


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddControllers();
builder.Services.AddMemoryCache();
builder.Services.AddSingleton<Random>();
builder.Services.AddSingleton(x => new EmailClient(builder.Configuration["ConnectionString"]));
builder.Services.AddTransient<IVerificationService, VerificationService>();

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors(x => x.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
app.MapControllers();
app.Run();
