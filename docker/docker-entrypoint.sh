#!/bin/bash
# Remove global.json to avoid SDK version conflicts in Docker
rm -f /app/global.json

# Run the application
dotnet run --project RedisCacheConsole.csproj