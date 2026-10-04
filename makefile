run:
	dotnet run --project RedisCacheConsole.csproj

build:
	dotnet build RedisCacheConsole.csproj

test:
	make docker-test-up
	ENVIRONMENT=test dotnet test RedisCacheConsole.slnx
	make docker-test-down
	rm -rf data-test

test-coverage:
	make docker-test-up
	ENVIRONMENT=test dotnet test tests/RedisCacheConsole.Tests/RedisCacheConsole.Tests.csproj \
        /p:CollectCoverage=true \
        /p:CoverletOutputFormat=cobertura \
        /p:CoverletOutput=./TestResults/ \
        /p:Threshold=60 \
        /p:ThresholdType=line \
        /p:ThresholdStat=total \
        /p:Exclude="[xunit.*]*"
	make docker-test-down
	rm -rf data-test

test-coverage-report:
	make docker-test-up
	ENVIRONMENT=test dotnet reportgenerator -reports:"tests/RedisCacheConsole.Tests/TestResults/coverage.cobertura.xml" -targetdir:"coveragereport" -reporttypes:Html
	make docker-test-down
	rm -rf data-test
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

docker-test-up:
	docker-compose -f docker-compose.test.yaml up -d --wait

docker-test-down:
	docker-compose -f docker-compose.test.yaml down

podman-test-up:
	podman-compose -f docker-compose.test.yaml up -d --wait

podman-test-down:
	podman-compose -f docker-compose.test.yaml down

podman-down:
	podman-compose down

local:
	dotnet run --project RedisCacheConsole.csproj