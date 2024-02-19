## Introduction 
`dotta.Net` is a lightweight and intuitive package designed to streamline the integration process of [dotta][dottawebsite] API and empower businesses to harness the power of [dotta biometric service][dottawebsite] effortlessly.

[dotta biometric service][dottawebsite] offers a wealth of functionality for performing real-time identity verification in the most convenient and efficient approach, but getting started and putting all the codes together can sometimes be complex and time-consuming. With `dotta.Net`, we've simplified the integration process, allowing you to focus on building amazing applications without getting bogged down in implementation details.

## Getting Started
1. Install the `dotta.Net` package from [NuGet][nugetlink].
```
dotnet add package dotta.Net
```

2. Register `dotta.Net` as a service in your `Program.cs` file:
```
// Register HttpClient to enable dotta.Net make http network requests
builder.Services.AddHttpClient();

builder.Services.AddScoped<Dotta>((serviceProvider) =>
{
    var httpClientFactory = serviceProvider.GetRequiredService<IHttpClientFactory>();
    var httpClient = httpClientFactory.CreateClient();

    return new Dotta(new DottaOptions
    {
        ApiKey = "",
        BaseUrlProduction = "",
        BaseUrlSandbox = "",
        Environment = DottaEnvironment.Sandbox,
        HttpClient = httpClient
    });
});
```

3. Inject `dotta.Net` service into any controller or service in your application
```
private readonly Dotta _dotta;

public ApiController(Dotta dotta)
{
    _dotta = dotta;
}
```

4. You can now access any member of the `dotta.Net` service
```
var response = await _dotta.FaceAttributes(photo);
```

### Other links
- [NuGet Package][nugetlink]
- [Release Notes](#)
- [Contributing Guidelines](CONTRIBUTING.md)
- [License](LICENSE.md)
- [Stack Overflow](https://stackoverflow.com/questions/tagged/dotta.net)



[dottawebsite]: https://withdotta.com
[nugetlink]: #