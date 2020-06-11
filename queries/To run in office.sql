use FactoryData

--DONE
--alter table Quality alter column Print_Colour varchar(30) NULL;
----alter table Batch add Grade varchar(10) NULL;
--update Batch set Grade='1st';

--update Colours set Colours = 'Deep Gold' where Colours = 'Dp. Gold';

--update Batch set Colour = 'Deep Gold' where Colour = 'Dp. Gold';
--update Carton_Produced set Colour = 'Deep Gold' where Colour = 'Dp. Gold';
--update Carton_Production_Voucher set Colour = 'Deep Gold' where Colour = 'Dp. Gold';
--update Dyeing_Issue_Voucher set Colour = 'Deep Gold' where Colour = 'Dp. Gold';
--update Batch set Colour = 'Deep Gold' where Colour = 'Dp. Gold';

--NOW RUN THIS:
alter table Tray_Active alter column Redyeing decimal(10,6) NULL;
alter table Tray_History alter column Redyeing decimal(10,6) NULL;
alter table Tray_Active add No_Of_Springs_RD int NULL
alter table Tray_History add No_Of_Springs_RD int NULL
update Tray_Active  set Redyeing = 0, No_Of_Springs_RD = 0;
update Tray_History set Redyeing = 0, No_Of_Springs_RD = 0;

DECLARE @NAME VARCHAR(100)
DECLARE @SQL NVARCHAR(300)
DECLARE @columnName NVARCHAR(100)

DECLARE CUR CURSOR FOR
  SELECT NAME
  FROM   SYS.TABLES
  WHERE  TYPE = 'U'
         AND SCHEMA_ID = 1

OPEN CUR

FETCH NEXT FROM CUR INTO @NAME

WHILE @@FETCH_STATUS = 0
  BEGIN
	DECLARE columns CURSOR FOR
	SELECT sys.columns.name FROM sys.tables
	INNER JOIN sys.columns ON sys.columns.object_id = sys.tables.object_id
	WHERE sys.tables.name = @NAME

	OPEN columns
	FETCH NEXT FROM columns
	INTO @columnName
	
	WHILE @@FETCH_STATUS = 0
	BEGIN
	  declare @datatype varchar(100)
	  SELECT @datatype = DATA_TYPE 
	  FROM INFORMATION_SCHEMA.COLUMNS
	  WHERE 
		TABLE_NAME = @NAME AND 
		COLUMN_NAME = @columnName
      
	  SET @SQL = 'UPDATE '+@NAME+' SET '+ @columnName +'=''Gray'' WHERE '+@columnName +'=''Grey'' AND '''+@datatype+''' = ''varchar'''

      PRINT @SQL
      EXEC Sp_executesql
        @SQL
	FETCH NEXT FROM columns
	INTO @columnName  
	END
	CLOSE columns;    
	DEALLOCATE columns;
  FETCH NEXT FROM CUR INTO @NAME
  END

CLOSE CUR

DEALLOCATE CUR 

