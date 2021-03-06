USE [FactoryData]
GO
/****** Object:  StoredProcedure [dbo].[SearchInBatch]    Script Date: 06-06-2020 04:26:16 PM ******/
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
