using Moq;

namespace dotta.Net.Test;

public class DottaTest
{
    [Fact]
    public void Test_InstantiateDotta()
    {
        // Arrange
        var mockHttpClient = new Mock<HttpClient>();
        var dottaService = new Dotta(new DottaOptions {
            ApiKey = "ABSCDE"
        });

        // Act
        var apiKey = dottaService.ApiKey;
        var environment = dottaService.Environment;
        var prodBaseUrl = dottaService.BaseUrlProduction;
        var sandboxBaseUrl = dottaService.BaseUrlSandbox;

        // Assert
        Assert.Equal("ABCDE", apiKey);
        Assert.Equal(DottaEnvironment.Sandbox, environment);
        Assert.Equal("https://apps.securedrecords.com/dotta-biometrics/api/", prodBaseUrl);
        Assert.Equal("https://apps.securedrecords.com/DevDottaBiometrics/api/", sandboxBaseUrl);
    }
}