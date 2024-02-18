using Moq;

namespace dotta.Net.Test;

public class UnitTest1
{
    [Fact]
    public void Test_InstantiateDotta_SingleConstructor_Params()
    {
        // Arrange
        var mockHttpClient = new Mock<HttpClient>();
        var dottaService = new Dotta("ABCDE", DottaEnvironment.Sandbox, mockHttpClient.Object);

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

    [Fact]
    public void Test_InstantiateDotta_DoubleConstructor_Params()
    {
        // Arrange
        var mockHttpClient = new Mock<HttpClient>();
        var dottaService = new Dotta("ABCDE", "FGHIJ", DottaEnvironment.Sandbox, mockHttpClient.Object);

        // Act
        var apiKey = dottaService.ApiKey;
        var environment = dottaService.Environment;
        var prodBaseUrl = dottaService.BaseUrlProduction;
        var sandboxBaseUrl = dottaService.BaseUrlSandbox;

        // Assert
        Assert.Equal("QUJDREU6RkdISUo=", apiKey);
        Assert.Equal(DottaEnvironment.Sandbox, environment);
        Assert.Equal("https://apps.securedrecords.com/dotta-biometrics/api/", prodBaseUrl);
        Assert.Equal("https://apps.securedrecords.com/DevDottaBiometrics/api/", sandboxBaseUrl);
    }
}