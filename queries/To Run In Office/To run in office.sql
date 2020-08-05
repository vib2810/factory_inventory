use FactoryData

--RUN IN Polyester: Y 15-07		Cotton: Y 21-07
--alter table Sales_Voucher drop column Carton_No_Arr;
--alter table Sales_Voucher drop column Carton_Fiscal_Year;

----RUN IN Polyester: Y 21-07 Cotton: Y 21-07
--create table Opening_Stock
--(
--	Voucher_ID int NOT NULL Primary Key Identity(1,1),
--	Date_Of_Input date NOT NULL,
--	Voucher_Name varchar(50) NOT NULL,
--	Deleted tinyint NULL
--);

------RUN IN Polyester: Y 21-07 Cotton: Y 21-07
--alter table Carton_Produced alter column Batch_No_Arr text NULL;
--alter table Carton_Produced alter column Dyeing_Company_Name varchar(50) NULL;
--alter table Carton_Produced alter column Carton_Weight decimal(7, 3) NULL;
--alter table Carton_Produced alter column Number_Of_Cones int NULL;
--alter table Carton_Produced alter column Cone_Weight decimal(6, 3) NULL;
--alter table Carton_Produced alter column Gross_Weight decimal(7, 3) NULL;
--alter table Carton_Produced alter column Batch_Fiscal_Year_Arr text NULL;

--RUN IN Polyester: Y 23-07 Cotton: Y 21-07
--alter table Opening_Stock add Deleted tinyint NULL
--alter table Carton_Produced add Opening_Voucher_ID int NULL

--RUN IN Polyester: N Cotton: N
GO
/****** Object:  StoredProcedure [dbo].[SearchInTable]    Script Date: 05-08-2020 18:16:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--use FactoryData
CREATE PROCEDURE [dbo].[SearchInTable] @tableName nvarchar(50), @searchText nvarchar(50), @date tinyint
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
	if (@columnName not like '%voucher%') and (@columnName not like '%fiscal%') and (@columnName not like '%deleted%')
    begin
		if(@date = 1)
		begin
			if @columnName like '%date%' 
			begin SET @sql = @sql + @columnName + ' LIKE ''%' + @searchText + '%'' OR ' end
		end
		else
		begin
			if @columnName not like '%date%' 
			begin SET @sql = @sql + @columnName + ' LIKE ''%' + @searchText + '%'' OR ' end
		end
	end
    FETCH NEXT FROM columns
    INTO @columnName    
END

CLOSE columns;    
DEALLOCATE columns;

SET @sql = LEFT(RTRIM(@sql), LEN(@sql) - 2)
--select @sql
EXEC(@sql)



