FROM mcr.microsoft.com/mssql/server:2025-latest

EXPOSE 1433

COPY --chown=mssql:root docker/scripts/mssql-entrypoint.sh /usr/config/entrypoint.sh
COPY --chown=mssql:root db/scripts/restore.sql /usr/config/scripts/restore.sql
COPY --chown=mssql:root db/backup/AdventureWorksLT2025.bak /var/opt/mssql/backup/AdventureWorksLT2025.bak
RUN chmod +x /usr/config/entrypoint.sh

# Healthy solo cuando AdventureWorksLT2025 ya fue restaurada, no solo cuando SQL Server acepta conexiones.
HEALTHCHECK --interval=10s --timeout=10s --start-period=60s --retries=30 \
    CMD SQLCMD=/opt/mssql-tools18/bin/sqlcmd; \
        [ -x "$SQLCMD" ] || SQLCMD=/opt/mssql-tools/bin/sqlcmd; \
        SQLCMDPASSWORD="$MSSQL_SA_PASSWORD" "$SQLCMD" -S localhost -U sa -C -b -l 5 \
        -d AdventureWorksLT2025 -Q "SET NOCOUNT ON; SELECT 1;" >/dev/null 2>&1 || exit 1

ENTRYPOINT ["/usr/config/entrypoint.sh"]
