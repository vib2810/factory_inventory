--use FactoryData
CREATE PROCEDURE SearchInTable @tableName nvarchar(50), @searchText nvarchar(50)
AS
SET NOCOUNT ON;
DECLARE @columnName NVARCHAR(100)
DECLARE @sql NVARCHAR(1000) = 'SELECT * FROM ' + @tableName +' WHERE '

DECLARE columns CURSOR FOR
SELECT sys.columns.name FROM sys.tables
INNER JOIN sys.columns ON sys.columns.object_id = sys.tables.object_id
WHERE sys.tables.name = @tableName

OPEN columns
FETCH NEXT FROM columns
INTO @columnName

WHILE @@FETCH_STATUS = 0

BEGIN

    SET @sql = @sql + @columnName + ' LIKE ''%' + @searchText + '%'' OR '
    FETCH NEXT FROM columns
    INTO @columnName    
END

CLOSE columns;    
DEALLOCATE columns;

SET @sql = LEFT(RTRIM(@sql), LEN(@sql) - 2)
select @sql
EXEC(@sql)
GO

