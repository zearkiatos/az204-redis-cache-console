run:
	dotnet run --project RedisCacheConsole.csproj

build:
	dotnet build RedisCacheConsole.csproj

docker-local-up:
	docker-compose -f docker-compose.local.yaml up

docker-local-down:
	docker-compose -f docker-compose.local.yaml down