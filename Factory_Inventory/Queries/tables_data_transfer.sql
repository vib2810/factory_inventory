SET @column = SUBSTRING((SELECT ', ' + QUOTENAME(COLUMN_NAME)
        FROM INFORMATION_SCHEMA.COLUMNS
        WHERE TABLE_NAME = 'table_name'
        ORDER BY ORDINAL_POSITION
        FOR XML path('')),3,200000)
SET @sql ='BEGIN TRY set IDENTITY_INSERT dest_db.dbo.table_name ON END TRY BEGIN CATCH END CATCH;
INSERT INTO dest_db.dbo.table_name ('+@column+')
SELECT * FROM  source_db.dbo.table_name';
EXEC(@sql)