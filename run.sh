run() {
    dotnet run --project RedisCacheConsole.csproj
}

build() {
    dotnet build RedisCacheConsole.csproj
}

docker_local_up() {
    docker-compose -f docker-compose.local.yaml up
}

docker_local_down() {
    docker-compose -f docker-compose.local.yaml down
}

podman_local_up() {
    podman-compose -f docker-compose.local.yaml up
}

podman_local_down() {
    podman-compose -f docker-compose.local.yaml down
}

docker_up() {
    docker-compose up
}

docker_down() {
    docker-compose down
}

podman_up() {
    podman-compose up
}

podman_down() {
    podman-compose down
}

local() {
    dotnet run --project RedisCacheConsole.csproj
}

test() {
    ENVIRONMENT=test dotnet test RedisCacheConsole.slnx
}

test_coverage() {
    ENVIRONMENT=test dotnet test tests/RedisCacheConsole.Tests/RedisCacheConsole.Tests.csproj \
        /p:CollectCoverage=true \
        /p:CoverletOutputFormat=cobertura \
        /p:CoverletOutput=./TestResults/ \
        /p:Threshold=60 \
        /p:ThresholdType=line \
        /p:ThresholdStat=total \
        /p:Exclude="[xunit.*]*"
}

test_coverage_report() {
    ENVIRONMENT=test dotnet reportgenerator -reports:"tests/RedisCacheConsole.Tests/TestResults/coverage.cobertura.xml" -targetdir:"coveragereport" -reporttypes:Html
    sleep 1
    open coveragereport/index.html
}