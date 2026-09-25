FROM mcr.microsoft.com/dotnet/sdk:10.0

WORKDIR /app

RUN apt-get update && \
    apt-get upgrade -y && \
    apt-get install -y bash

RUN groupadd -r -g 10001 appGrp && \
    useradd -r -u 10000 -m -d /home/appuser -s /sbin/nologin -g appGrp appuser && \
    chown -R appuser:appGrp /home/appuser

COPY . /app
COPY ./docker/docker-entrypoint.sh /app/

RUN chown -R appuser:appGrp /app

USER appuser

ENTRYPOINT ["dotnet", "run", "--project", "RedisCacheConsole.csproj"]