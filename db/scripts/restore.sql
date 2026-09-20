USE [master];
GO

RESTORE DATABASE [AdventureWorks2025]
FROM DISK = './../backup/AdventureWorks2025.bak'
WITH
    MOVE 'AdventureWorks2025' TO '/var/opt/mssql/data/AdventureWorks2025_Data.mdf',
    MOVE 'AdventureWorks2025_log' TO '/var/opt/mssql/data/AdventureWorks2025_log.ldf',
    FILE = 1,
    NOUNLOAD,
    STATS = 5;
GO