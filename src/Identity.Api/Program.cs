using Identity.Api.Extensions;
using Identity.Api.Filters;
using Identity.Application;
using Identity.Repository;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers(c => c.Filters.Add(typeof(ExceptionFilter)));
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwagger();

builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<OauthService>();

builder.Services.AddJwtAuthorization(builder.Configuration);

//Add configurações do contexto para ef e do asp.net identity
builder.Services.AddDataBase(builder.Configuration["ConnectionStrings"]);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();


app.MapControllers();

//Add metodo para realizar migração do banco ao iniciar alicação
await app.AddMigrate();

app.Run();

