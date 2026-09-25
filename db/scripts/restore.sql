USE [master];
GO

IF DB_ID(N'AdventureWorksLT2025') IS NULL
BEGIN
    RESTORE DATABASE [AdventureWorksLT2025]
    FROM DISK = '/var/opt/mssql/backup/AdventureWorksLT2025.bak'
    WITH
        MOVE 'AdventureWorksLT2025_Data' TO '/var/opt/mssql/data/AdventureWorksLT2025_Data.mdf',
        MOVE 'AdventureWorksLT2025_Log' TO '/var/opt/mssql/data/AdventureWorksLT2025_Log.ldf',
        FILE = 1,
        NOUNLOAD,
        STATS = 5;
END
ELSE
BEGIN
    PRINT N'AdventureWorksLT2025 already exists. Restoration will not be performed.';
END;
GO