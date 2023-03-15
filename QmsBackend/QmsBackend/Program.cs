using DataAccessLayer;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using ServiceLayer;
using ServiceLayer.Jwt;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//injected region
builder.Services.AddRepository(builder.Configuration);
builder.Services.AddDataContext(builder.Configuration);

//implementing jwt
var appsettingssection = builder.Configuration.GetSection("AppSetting");
builder.Services.Configure<Appsetting>(appsettingssection);
var appsetting = appsettingssection.Get<Appsetting>();
var key = Encoding.ASCII.GetBytes(appsetting.Secret);
builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(x =>
{
    x.RequireHttpsMetadata = false;
    x.SaveToken = true;
    x.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = false,
        ValidateAudience = false,
    };
});
//cores
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "MyPolicy",
   builder =>
   {
       builder.WithOrigins("http://localhost:4200")
.AllowAnyOrigin()
.AllowAnyHeader()
.AllowAnyMethod();
   });


});

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
app.UseCors("MyPolicy");
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
