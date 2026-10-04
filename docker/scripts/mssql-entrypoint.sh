#!/bin/bash
set -euo pipefail

/opt/mssql/bin/sqlservr &
SQLSERVER_PID=$!
trap 'kill -TERM "$SQLSERVER_PID" 2>/dev/null || true' EXIT
trap 'exit 143' SIGTERM
trap 'exit 130' SIGINT

SQLCMD=/opt/mssql-tools18/bin/sqlcmd
[ -x "$SQLCMD" ] || SQLCMD=/opt/mssql-tools/bin/sqlcmd

export SQLCMDPASSWORD="$MSSQL_SA_PASSWORD"

RESTORED=false
# Espera a que SQL Server acepte conexiones antes de restaurar.
for i in $(seq 1 60); do
    if "$SQLCMD" -S localhost -U "$MSSQL_USER" -C -b -l 5 -Q "SELECT 1" >/dev/null 2>&1; then
        "$SQLCMD" -S localhost -U "$MSSQL_USER" -C -b -l 5 -i /usr/config/scripts/restore.sql
        RESTORED=true
        break
    fi
    sleep 1
done

if [ "$RESTORED" != true ]; then
    echo "SQL Server no estuvo disponible para restaurar AdventureWorksLT2025." >&2
    exit 1
fi

wait "$SQLSERVER_PID"