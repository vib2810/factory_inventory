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


USE [FactoryData]
GO
/****** Object:  StoredProcedure [dbo].[SearchInTray]    Script Date: 11-06-2020 11:09:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--use FactoryData
ALTER PROCEDURE [dbo].[SearchInTray] @searchText nvarchar(50), @date tinyint
AS
begin
SET NOCOUNT ON;
DECLARE @columnName NVARCHAR(100)
DECLARE @cols NVARCHAR(1000) = ''
DECLARE @sql NVARCHAR(4000) = 'SELECT '

DECLARE columns CURSOR FOR
SELECT sys.columns.name FROM sys.tables
INNER JOIN sys.columns ON sys.columns.object_id = sys.tables.object_id
WHERE sys.tables.name = 'Tray_History'

OPEN columns
FETCH NEXT FROM columns
INTO @columnName

WHILE @@FETCH_STATUS = 0

BEGIN
	SET @cols = @cols + @columnName + ','
	FETCH NEXT FROM columns
	INTO @columnName  
END

SET @cols = LEFT(RTRIM(@cols), LEN(@cols) - 1)
CLOSE columns;    
DEALLOCATE columns;
SET @sql = @sql+@cols+', Tray_State into S FROM Tray_Active UNION SELECT '+@cols+', -1 AS Tray_State FROM Tray_History' ;
EXEC(@sql)

if @date = 1
	begin
    select *
    from
    (
		
		select 
			case when Tray_Production_Date like @searchText then 'd1'
				when Dyeing_Out_Date like @searchText then 'd2'
				when Dyeing_In_Date like @searchText then 'd3'
				when Tray_State like @searchText then 'd4'
				
				when Tray_Production_Date like '%' + @searchText + '%' then 'da1'
				when Dyeing_Out_Date like '%' + @searchText + '%' then 'da2'
				when Dyeing_In_Date like '%' + @searchText + '%' then 'da3'
				when Tray_State like '%' + @searchText + '%' then 'da4'

		 end as priority,
         T.*
		from S as T
	) as C
	where priority is not null
	order by priority asc
	end

	if @date = 0
	begin
	select *
    from
    (
		select 
			case when Tray_No like @searchText then '1'
				when Quality like @searchText then '2'
				when Batch_No like @searchText then '3'
				when Batch_Fiscal_Year like @searchText then '4'
				when Spring like @searchText then '5'
				when Number_of_Springs like @searchText then '6'
				when Net_Weight like @searchText then '7'
				when Dyeing_Company_Name like @searchText then '8'
				when Machine_No like @searchText then '9'
				when Grade like @searchText then '10'
				when Fiscal_Year like @searchText then '11'
				
				when Tray_No like '%' + @searchText + '%' then 'a1'
				when Quality like '%' + @searchText + '%' then 'a2'
				when Batch_No like '%' + @searchText + '%' then 'a3'
				when Batch_Fiscal_Year like '%' + @searchText + '%' then 'a4'
				when Spring like '%' + @searchText + '%' then 'a5'
				when Number_of_Springs like '%' + @searchText + '%' then 'a6'
				when Net_Weight like '%' + @searchText + '%' then 'a7'
				when Dyeing_Company_Name like '%' + @searchText + '%' then 'a8'
				when Machine_No like '%' + @searchText + '%' then 'a9'
				when Grade like '%' + @searchText + '%' then 'a10'
				when Fiscal_Year like '%' + @searchText + '%' then 'a11'
				

		 end as priority,
         T.*
		from S as T
	) as C
	where priority is not null
	order by priority asc
	end

drop table S
end


USE [FactoryData]
GO
/****** Object:  StoredProcedure [dbo].[SearchInCartonProduced]    Script Date: 11-06-2020 11:09:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--use FactoryData
ALTER PROCEDURE [dbo].[SearchInCartonProduced] @searchText nvarchar(50), @date tinyint
AS
if @date = 1
	begin
    select *
    from
    (
		
		select 
			case when Date_Of_Production like @searchText then 'd1'
				when Date_Of_Sale like @searchText then 'd2'
				when Carton_State like @searchText then 'd3'
				
				when Date_Of_Production like '%' + @searchText + '%' then 'da1'
				when Date_Of_Sale like '%' + @searchText + '%' then 'da2'
				when Carton_State like '%' + @searchText + '%' then 'da3'

		 end as priority,
         T.*
		from Carton_Produced as T
	) as C
	where priority is not null
	order by priority asc
	end

	if @date = 0
	begin
	select *
    from
    (
		select 
			case when Carton_No like @searchText then '1'
				when Quality like @searchText then '2'
				when Colour like @searchText then '3'
				when Batch_No_Arr like @searchText then '4'
				when Batch_Fiscal_Year_Arr like @searchText then '5'
				when Date_Of_Production like @searchText then '6'
				when Net_Weight like @searchText then '7'
				when Dyeing_Company_Name like @searchText then '8'
				when Sale_DO_No like @searchText then '9'
				when DO_Fiscal_Year like @searchText then '10'
				when Sale_Rate like @searchText then '11'
				when Grade like @searchText then '12'
				when Fiscal_Year like @searchText then '13'

				when Carton_No like '%' + @searchText + '%' then 'a1'
				when Quality like '%' + @searchText + '%' then 'a2'
				when Colour like '%' + @searchText + '%' then 'a3'
				when Batch_No_Arr like '%' + @searchText + '%' then 'a4'
				when Batch_Fiscal_Year_Arr like '%' + @searchText + '%' then 'a5'
				when Date_Of_Production like '%' + @searchText + '%' then 'a6'
				when Net_Weight like '%' + @searchText + '%' then 'a7'
				when Dyeing_Company_Name like '%' + @searchText + '%' then 'a8'
				when Sale_DO_No like '%' + @searchText + '%' then 'a9'
				when DO_Fiscal_Year like '%' + @searchText + '%' then 'a10'
				when Sale_Rate like '%' + @searchText + '%' then 'a11'
				when Grade like '%' + @searchText + '%' then 'a12'
				when Fiscal_Year like '%' + @searchText + '%' then 'a13'

		 end as priority,
         T.*
		from Carton_Produced as T
	) as C
	where priority is not null
	order by priority asc
	end


	USE [FactoryData]
GO
/****** Object:  StoredProcedure [dbo].[SearchInCarton]    Script Date: 11-06-2020 11:09:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--use FactoryData
ALTER PROCEDURE [dbo].[SearchInCarton] @searchText nvarchar(50), @date tinyint
AS
if @date = 1
	begin
    select *
    from
    (
		
		select 
			case when Date_Of_Billing like @searchText then 'd1'
				when Date_Of_Issue like @searchText then 'd2'
				when Date_Of_Sale like @searchText then 'd3'
				when Carton_State like @searchText then 'd4'
				
				when Date_Of_Billing like '%' + @searchText + '%' then 'da1'
				when Date_Of_Issue like '%' + @searchText + '%' then 'da2'
				when Date_Of_Sale like '%' + @searchText + '%' then 'da3'
				when Carton_State like '%' + @searchText + '%' then 'da4'

		 end as priority,
         T.*
		from Carton as T
	) as C
	where priority is not null
	order by priority asc
	end

	if @date = 0
	begin
	select *
    from
    (
		select 
			case when Carton_No like @searchText then '1'
				when Quality like @searchText then '2'
				when Company_Name like @searchText then '3'
				when Net_Weight like @searchText then '4'
				when Fiscal_Year like @searchText then '5'
				when Bill_No like @searchText then '6'
				when Sale_DO_No like @searchText then '7'
				when DO_Fiscal_Year like @searchText then '8'
				when Buy_Cost like @searchText then '9'
				when Sale_Rate like @searchText then '10'
				

				when Carton_No like '%' + @searchText + '%' then 'a1'
				when Quality like '%' + @searchText + '%' then 'a2'
				when Company_Name like '%' + @searchText + '%' then 'a3'
				when Net_Weight like '%' + @searchText + '%' then 'a4'
				when Fiscal_Year like '%' + @searchText + '%' then 'a5'
				when Bill_No like '%' + @searchText + '%' then 'a6'
				when Sale_DO_No like '%' + @searchText + '%' then 'a7'
				when DO_Fiscal_Year like '%' + @searchText + '%' then 'a8'
				when Buy_Cost like '%' + @searchText + '%' then 'a9'
				when Sale_Rate like '%' + @searchText + '%' then 'a10'
				

		 end as priority,
         T.*
		from Carton as T
	) as C
	where priority is not null
	order by priority asc
	end



	USE [FactoryData]
GO
/****** Object:  StoredProcedure [dbo].[SearchInBatch]    Script Date: 11-06-2020 11:09:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--use FactoryData
ALTER PROCEDURE [dbo].[SearchInBatch] @searchText nvarchar(50), @date tinyint
AS
if @date = 1
	begin
    select *
    from
    (
		
		select 
			case when Dyeing_Out_Date like @searchText then 'd1'
				when Dyeing_In_Date like @searchText then 'd2'
				when Start_Date_Of_Production like @searchText then 'd3'
				when Date_Of_Production like @searchText then 'd4'
				when Bill_Date like @searchText then 'd5'
				
				when Dyeing_Out_Date like @searchText + '%' then 'da1'
				when Dyeing_In_Date like @searchText + '%' then 'da2'
				when Start_Date_Of_Production like @searchText + '%' then 'da3'
				when Date_Of_Production like @searchText + '%' then 'da4'
				when Bill_Date like @searchText + '%' then 'da5'

		 end as priority,
         T.*
		from Batch as T
	) as C
	where priority is not null
	order by priority asc
	end

	if @date = 0
	begin
	select *
    from
    (
		select 
			case when Batch_No like @searchText then '1'
			when Colour like @searchText then '2'
			when Quality like @searchText then '3'
			when Net_Weight like @searchText then '4'
			when Dyeing_Company_Name like @searchText then '5'
			when Fiscal_Year like @searchText then '6'
			when Bill_No like @searchText then '7'
			when Slip_No like @searchText then '8'
			when Company_Name like @searchText then '9'
			when Batch_No like @searchText + '%' then 'a1'
			when Colour like @searchText + '%' then 'a2'
			when Quality like @searchText + '%' then 'a3'
			when Net_Weight like @searchText + '%' then 'a4'
			when Dyeing_Company_Name like @searchText + '%' then 'a5'
			when Fiscal_Year like @searchText + '%' then 'a6'
			when Bill_No like @searchText + '%' then 'a7'
			when Slip_No like @searchText + '%' then 'a8'
			when Company_Name like @searchText + '%' then 'a9'

		 end as priority,
         T.*
		from Batch as T
	) as C
	where priority is not null
	order by priority asc
	end


