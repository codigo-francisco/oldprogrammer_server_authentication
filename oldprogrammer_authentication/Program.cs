using oldprogrammer.authentication.httpclients.EmailClient;
using oldprogrammer_authentication;
using oldprogrammer_authetication.core.HttpClients;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.ConfigureContext();
builder.AddAllowedCors();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("Default");

app.UseAuthorization();

app.MapControllers();

app.Run();
