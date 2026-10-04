namespace RedisCacheConsole.Tests.Configuration
{
    // Serializa los tests que leen/mutan el estado estático de AppConfiguration; xunit corre
    // colecciones distintas en paralelo y eso filtraba el mock de fake entre clases de test.
    [CollectionDefinition("AppConfiguration", DisableParallelization = true)]
    public class AppConfigurationCollection
    {
    }
}
