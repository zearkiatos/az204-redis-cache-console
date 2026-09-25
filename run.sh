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