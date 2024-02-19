using dotta.Net;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddHttpClient();

builder.Services.AddScoped<Dotta>((serviceProvider) =>
{
    var httpClientFactory = serviceProvider.GetRequiredService<IHttpClientFactory>();
    var httpClient = httpClientFactory.CreateClient();

    return new Dotta(new DottaOptions
    {
        ApiKey = "ODZCNzExNjczMkUyNDI2OUFGRjg2NkJGMERBNjBFNjg6MzE0QzJERkYzNDRENDhFRjlFNkUyOTI5RUQ5MEQwRTM=",
        BaseUrlProduction = "https://apps.securedrecords.com/dotta-biometrics/api",
        BaseUrlSandbox = "https://apps.securedrecords.com/DevDottaBiometrics/api",
        Environment = DottaEnvironment.Sandbox,
        HttpClient = httpClient
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

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
