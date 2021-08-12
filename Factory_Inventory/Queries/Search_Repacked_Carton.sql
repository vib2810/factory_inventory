--use FactoryData_1

--DECLARE @searchText nvarchar(50) = 'new', @date tinyint = 0
SELECT temp5.*, T_M_Customers.Customer_Name into #temp
FROM
	(SELECT temp4.*, T_M_Cones.Cone_Weight 
	FROM
		(SELECT temp3.*, T_M_Colours.Colour
		FROM	
			(SELECT temp2.*,T_M_Quality_Before_Job.Quality_Before_Job
			FROM	
				(SELECT temp1.*, T_Repacking_Voucher.Start_Date_Of_Production, T_Repacking_Voucher.Cone_ID
				FROM
					(SELECT T_Repacked_Cartons.Number_Of_Cones, T_Repacked_Cartons.ID, T_Repacked_Cartons.Carton_ID, T_Repacked_Cartons.Gross_Weight, T_Repacked_Cartons.Printed, T_Repacked_Cartons.Carton_Weight, T_Repacked_Cartons.Quality_ID, T_Repacked_Cartons.Colour_ID, T_Repacked_Cartons.Date_Of_Production, T_Repacked_Cartons.Repacking_Voucher_ID, T_Repacked_Cartons.Sale_Voucher_ID, T_Repacked_Cartons.Carton_State, T_Repacked_Cartons.Carton_No, T_Repacked_Cartons.Net_Weight, T_Repacked_Cartons.Grade, T_Repacked_Cartons.Repack_Comments, T_Repacked_Cartons.Sale_Comments, T_Repacked_Cartons.Fiscal_Year, T_Sales_Voucher.Date_Of_Sale, T_Sales_Voucher.Sale_DO_No, T_Sales_Voucher.Fiscal_Year as DO_Fiscal_Year, T_Sales_Voucher.Sale_Rate, T_Sales_Voucher.Customer_ID
					FROM T_Repacked_Cartons
					LEFT OUTER JOIN T_Sales_Voucher
					ON T_Sales_Voucher.Voucher_ID = T_Repacked_Cartons.Repacking_Voucher_ID) as temp1
				LEFT OUTER JOIN T_Repacking_Voucher
				ON T_Repacking_Voucher.Voucher_ID = temp1.Repacking_Voucher_ID) as temp2
			LEFT OUTER JOIN T_M_Quality_Before_Job
			ON T_M_Quality_Before_Job.Quality_Before_Job_ID = temp2.Quality_ID) as temp3
		LEFT OUTER JOIN T_M_Colours
		ON T_M_Colours.Colour_ID = temp3.Colour_ID) as temp4
	LEFT OUTER JOIN T_M_Cones
	ON T_M_Cones.Cone_ID = temp4.Cone_ID) as temp5
LEFT OUTER JOIN T_M_Customers
ON T_M_Customers.Customer_ID = temp5.Customer_ID

if @date = 1
	begin
    select *
    from
    (
		
		select 
			case when Date_Of_Production like @searchText then 'd1'
				when Date_Of_Sale like @searchText then 'd2'
				when Start_Date_Of_Production like @searchText then 'd3'
				when Carton_State like @searchText then 'd4'
				
				when Date_Of_Production like '%' + @searchText + '%' then 'da1'
				when Date_Of_Sale like '%' + @searchText + '%' then 'da2'
				when Start_Date_Of_Production like '%' + @searchText + '%' then 'da3'
				when Carton_State like '%' + @searchText + '%' then 'da4'

		 end as priority,
         T.*
		from #temp as T
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
				when Quality_Before_Job like @searchText then '2'
				when Colour like @searchText then '3'
				when Net_Weight like @searchText then '4'
				when Sale_DO_No like @searchText then '5'
				when DO_Fiscal_Year like @searchText then '6'
				when Sale_Rate like @searchText then '7'
				when Grade like @searchText then '8'
				when Repack_Comments like @searchText then '9'
				when Sale_Comments like @searchText then '10'
				when Fiscal_Year like @searchText then '11'

				when Carton_No like '%' + @searchText + '%' then 'a1'
				when Quality_Before_Job like '%' + @searchText + '%' then 'a2'
				when Colour like '%' + @searchText + '%' then 'a3'
				when Net_Weight like '%' + @searchText + '%' then 'a4'
				when Sale_DO_No like '%' + @searchText + '%' then 'a5'
				when DO_Fiscal_Year like '%' + @searchText + '%' then 'a6'
				when Sale_Rate like '%' + @searchText + '%' then 'a7'
				when Grade like '%' + @searchText + '%' then 'a8'
				when Repack_Comments like '%' + @searchText + '%' then 'a9'
				when Sale_Comments like '%' + @searchText + '%' then 'a10'
				when Fiscal_Year like '%' + @searchText + '%' then 'a11'

		 end as priority,
         T.*
		from #temp as T
	) as C
	where priority is not null
	order by priority asc
	end

drop table #temp