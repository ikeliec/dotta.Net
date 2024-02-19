## Introduction 
`dotta.Net` is a lightweight and intuitive package designed to streamline the integration process of [dotta API][dottaapidoc] and empower businesses to harness the power of [dotta biometric service][dottawebsite] effortlessly.

[dotta][dottawebsite] offers a wealth of functionality for performing real-time identity verification in the most convenient and efficient approach, but getting started and putting all the codes together can sometimes be complex and time-consuming. With `dotta.Net`, we've simplified the integration process, allowing you to focus on building amazing applications without getting bogged down in implementation details.

## Getting Started
1. Install the `dotta.Net` package from [NuGet][nugetlink].
```
dotnet add package dotta.Net
```

2. Register `dotta.Net` as a service in your `Program.cs` file:
```
// Register HttpClient to enable dotta.Net make http network requests
builder.Services.AddHttpClient();

builder.Services.AddDotta(new DottaServiceOptions
{
    ApiKey = "",
    BaseUrlProduction = "",
    BaseUrlSandbox = "",
    Environment = DottaEnvironment.Sandbox,
    PrivateKey = "",
    PublicKey = ""
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
**Dotta Service Options**
| **Option** | **Description** |
| ---------- | --------------- |
| ApiKey     | Base64 encode string of your dotta public and private API keys concatenated in this format PUBLICKEY:PRIVATEKEY |
| PublicKey | Your dotta public API key |
| PrivateKey | Your dotta private API key |
| Environment | Enum to specify which dotta environment you want to use |
| BaseUrlProduction | API base url for dotta's production environment. |
| BaseUrlSandbox | API base url for dotta's sandbox or test environment. |

Pass the your public and private key if you don't know how to get a base64 string encoding of your keys. Otherwise, just pass the ApiKey. When you pass the ApiKey, you won't need to pass the public and private keys


### Other links
- [NuGet Package][nugetlink]
- [Release Notes](#)
- [Contributing Guidelines](CONTRIBUTING.md)
- [License](LICENSE.md)
- [Stack Overflow](https://stackoverflow.com/questions/tagged/dotta.net)



[dottawebsite]: https://withdotta.com
[dottaapidoc]: https://docs.withdotta.com
[nugetlink]: #