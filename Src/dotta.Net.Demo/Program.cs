using dotta.Net;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddHttpClient();
builder.Services.AddDotta(new DottaServiceOptions
{
    ApiKey = "",
    BaseUrlProduction = "https://apps.securedrecords.com/dotta-biometrics/api",
    BaseUrlSandbox = "https://apps.securedrecords.com/DevDottaBiometrics/api",
    Environment = DottaEnvironment.Sandbox,
    PrivateKey = "",
    PublicKey = ""
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

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
