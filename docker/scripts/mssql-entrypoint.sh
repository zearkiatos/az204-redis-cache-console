#!/bin/bash
set -euo pipefail

/opt/mssql/bin/sqlservr &
SQLSERVER_PID=$!
trap 'kill -TERM $SQLSERVER_PID' SIGTERM SIGINT

SQLCMD=/opt/mssql-tools18/bin/sqlcmd
[ -x "$SQLCMD" ] || SQLCMD=/opt/mssql-tools/bin/sqlcmd

# Espera a que SQL Server acepte conexiones antes de restaurar.
for i in $(seq 1 60); do
    if "$SQLCMD" -S localhost -U "$MSSQL_USER" -P "$MSSQL_SA_PASSWORD" -C -Q "SELECT 1" >/dev/null 2>&1; then
        "$SQLCMD" -S localhost -U "$MSSQL_USER" -P "$MSSQL_SA_PASSWORD" -C -i /usr/config/scripts/restore.sql
        break
    fi
    sleep 1
done

wait "$SQLSERVER_PID"