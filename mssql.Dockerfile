FROM mcr.microsoft.com/mssql/server:2025-latest

EXPOSE 1433

COPY --chown=mssql:root docker/scripts/mssql-entrypoint.sh /usr/config/entrypoint.sh
COPY --chown=mssql:root db/scripts/restore.sql /usr/config/scripts/restore.sql
COPY --chown=mssql:root db/backup/AdventureWorksLT2025.bak /var/opt/mssql/backup/AdventureWorksLT2025.bak
RUN chmod +x /usr/config/entrypoint.sh

HEALTHCHECK --interval=30s --timeout=10s --start-period=45s --retries=5 \
    CMD /opt/mssql-tools18/bin/sqlcmd -S localhost -U sa -P "$MSSQL_SA_PASSWORD" -C -Q "SELECT 1" 2>/dev/null \
        || /opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P "$MSSQL_SA_PASSWORD" -Q "SELECT 1" 2>/dev/null \
        || exit 1

ENTRYPOINT ["/usr/config/entrypoint.sh"]
