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
    docker_test_up
    sleep 5
    ENVIRONMENT=test dotnet test RedisCacheConsole.slnx
    docker_test_down
    rm -rf data-test
}

test_coverage() {
    docker_test_up
    sleep 5
    ENVIRONMENT=test dotnet test tests/RedisCacheConsole.Tests/RedisCacheConsole.Tests.csproj \
        /p:CollectCoverage=true \
        /p:CoverletOutputFormat=cobertura \
        /p:CoverletOutput=./TestResults/ \
        /p:Threshold=60 \
        /p:ThresholdType=line \
        /p:ThresholdStat=total \
        /p:Exclude="[xunit.*]*"
    docker_test_down
    rm -rf data-test
}

test_coverage_report() {
    docker_test_up
    sleep 5
    ENVIRONMENT=test dotnet reportgenerator -reports:"tests/RedisCacheConsole.Tests/TestResults/coverage.cobertura.xml" -targetdir:"coveragereport" -reporttypes:Html
    sleep 1
    open coveragereport/index.html
    docker_test_down
    rm -rf data-test
}

docker_test_up() {
    docker-compose -f docker-compose.test.yaml up -d --wait
}

docker_test_down() {
    docker-compose -f docker-compose.test.yaml down
}

podman_test_up() {
    podman-compose -f docker-compose.test.yaml up -d --wait
}

podman_test_down() {
    podman-compose -f docker-compose.test.yaml down
}