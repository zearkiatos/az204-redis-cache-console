# Azure for redis

## Download Adventure Works sample database

```txt
URL: https://learn.microsoft.com/en-us/sql/samples/adventureworks-install-configure?view=sql-server-ver17&tabs=tsql
```

## Configuration test coverage

```sh
$ dotnet new tool-manifest

$ dotnet tool install dotnet-reportgenerator-globaltool

$ dotnet reportgenerator \
  -reports:"tests/RedisCacheConsole.Tests/TestResults/**/coverage.cobertura.xml" \
  -targetdir:"coveragereport" \
  -reporttypes:Html
```