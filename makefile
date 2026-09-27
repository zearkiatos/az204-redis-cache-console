run:
	dotnet run --project RedisCacheConsole.csproj

build:
	dotnet build RedisCacheConsole.csproj

test:
	ENVIRONMENT=test dotnet test RedisCacheConsole.slnx

test-coverage:
	ENVIRONMENT=test dotnet test tests/RedisCacheConsole.Tests/RedisCacheConsole.Tests.csproj \
        /p:CollectCoverage=true \
        /p:CoverletOutputFormat=cobertura \
        /p:CoverletOutput=./TestResults/ \
        /p:Threshold=60 \
        /p:ThresholdType=line \
        /p:ThresholdStat=total \
        /p:Exclude="[xunit.*]*"

test-coverage-report:
	ENVIRONMENT=test dotnet reportgenerator -reports:"tests/RedisCacheConsole.Tests/TestResults/coverage.cobertura.xml" -targetdir:"coveragereport" -reporttypes:Html
	sleep 1
	open coveragereport/index.html

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