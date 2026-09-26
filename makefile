run:
	dotnet run --project RedisCacheConsole.csproj

build:
	dotnet build RedisCacheConsole.csproj

test:
	dotnet test RedisCacheConsole.slnx

docker-local-up:
	docker-compose -f docker-compose.local.yaml up

docker-local-down:
	docker-compose -f docker-compose.local.yaml down

podman-local-up:
	podman-compose -f docker-compose.local.yaml up

podman-local-down:
	podman-compose -f docker-compose.local.yaml down

docker-up:
	docker-compose up

docker-down:
	docker-compose down

podman-up:
	podman-compose up

podman-down:
	podman-compose down

local:
	dotnet run --project RedisCacheConsole.csproj