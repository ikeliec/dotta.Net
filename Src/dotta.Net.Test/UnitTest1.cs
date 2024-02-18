namespace dotta.Net.Test;

public class UnitTest1
{
    [Fact]
    public void Test_InstiatDotta_SingleConstructor_Arg()
    {
        // Arrange
        var dotta = new Dotta("ABCDE", DottaEnvironment.Development);

        // Act
        var apiKey = dotta.ApiKey;
        var environment = dotta.Environment;

        // Assert
        Assert.Equal("ABCDE", apiKey);
        Assert.Equal(DottaEnvironment.Development, environment);
    }

    [Fact]
    public void Test_InstiatDotta_DoubleConstructor_Arg()
    {
        // Arrange
        var dotta = new Dotta("ABCDE", "FGHIJ", DottaEnvironment.Development);

        // Act
        var apiKey = dotta.ApiKey;
        var environment = dotta.Environment;

        // Assert
        Assert.Equal("QUJDREU6RkdISUo=", apiKey);
        Assert.Equal(DottaEnvironment.Development, environment);
    }
}