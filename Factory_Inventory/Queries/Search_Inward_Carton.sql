--DECLARE @searchText nvarchar(50) = '5', @date tinyint = 0

SELECT temp4.*, SUM(T_Carton_Sales.Sold_Weight) as Sold_Weight into #tempFROM	(SELECT temp3.*, T_M_Company_Names.Company_Name	FROM		(SELECT temp2.*, T_M_Quality_Before_Job.Quality_Before_Job 		FROM				(SELECT temp1.*, T_Repacking_Voucher.Date_Of_Production, T_Repacking_Voucher.Start_Date_Of_Production			FROM				(SELECT T_Inward_Carton.Carton_ID, T_Inward_Carton.Carton_No, T_Inward_Carton.Carton_State, T_Inward_Carton.Quality_ID, T_Inward_Carton.Colour_ID, T_Inward_Carton.Net_Weight, T_Inward_Carton.Buy_Cost, T_Inward_Carton.Fiscal_Year, T_Inward_Carton.Inward_Voucher_ID, T_Inward_Carton.Job_Voucher_ID, T_Inward_Carton.Repacking_Voucher_ID  ,T_Inward_Carton.Deleted, CAST(T_Inward_Carton.Comments AS NVARCHAR(1000)) as Inward_Comments, T_Inward_Carton.Inward_Type, T_Inward_Carton.Grade, T_Inward_Carton.Company_ID, T_Inward_Carton.Repacking_Display_Order, T_Carton_Inward_Voucher.Date_Of_Billing, T_Carton_Inward_Voucher.Bill_No, T_Carton_Inward_Voucher.Date_Of_Input 				FROM T_Inward_Carton				LEFT OUTER JOIN T_Carton_Inward_Voucher				ON T_Carton_Inward_Voucher.Voucher_ID = T_Inward_Carton.Inward_Voucher_ID) as temp1			LEFT OUTER JOIN T_Repacking_Voucher			ON T_Repacking_Voucher.Voucher_ID = temp1.Repacking_Voucher_ID) as temp2		LEFT OUTER JOIN T_M_Quality_Before_Job		ON T_M_Quality_Before_Job.Quality_Before_Job_ID = temp2.Quality_ID) as temp3	LEFT OUTER JOIN T_M_Company_Names	ON T_M_Company_Names.Company_ID = temp3.Company_ID) as temp4LEFT OUTER JOIN T_Carton_SalesON T_Carton_Sales.Carton_ID = temp4.Carton_IDGROUP BY temp4.Carton_ID, temp4.Carton_No, temp4.Carton_State, temp4.Quality_ID, temp4.Colour_ID, temp4.Net_Weight, temp4.Buy_Cost, temp4.Fiscal_Year, temp4.Inward_Voucher_ID, temp4.Job_Voucher_ID, temp4.Repacking_Voucher_ID, temp4.Deleted, CAST(temp4.Inward_Comments AS NVARCHAR(1000)), temp4.Inward_Type, temp4.Grade, temp4.Company_ID, temp4.Repacking_Display_Order, temp4.Date_Of_Billing, temp4.Bill_No, temp4.Date_Of_Input, temp4.Date_Of_Production, temp4.Start_Date_Of_Production, temp4.Quality_Before_Job, temp4.Company_Name



if @date = 1
	begin
    select *
    from
    (
		select 
			case when Date_Of_Billing like @searchText then 'd1'
				when Date_Of_Production like @searchText then 'd2'
				when Start_Date_Of_Production like @searchText then 'd3'
				when Carton_State like @searchText then 'd5'
				
				when Date_Of_Billing like '%' + @searchText + '%' then 'da1'
				when Date_Of_Production like '%' + @searchText + '%' then 'da2'
				when Start_Date_Of_Production like '%' + @searchText + '%' then 'da3'
				when Carton_State like '%' + @searchText + '%' then 'da5'

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
				when Company_Name like @searchText then '3'
				when Net_Weight like @searchText then '4'
				when Sold_Weight like @searchText then '5'
				when Fiscal_Year like @searchText then '6'
				when Bill_No like @searchText then '7'
				when Buy_Cost like @searchText then '9'
				when Inward_Comments like @searchText then '11'
				when Grade like @searchText then '12'
				

				when Carton_No like '%' + @searchText + '%' then 'a1'
				when Quality_Before_Job like '%' + @searchText + '%' then 'a2'
				when Company_Name like '%' + @searchText + '%' then 'a3'
				when Net_Weight like '%' + @searchText + '%' then 'a4'
				when Sold_Weight like '%' + @searchText + '%' then 'a5'
				when Fiscal_Year like '%' + @searchText + '%' then 'a6'
				when Bill_No like '%' + @searchText + '%' then 'a7'
				when Buy_Cost like '%' + @searchText + '%' then 'a9'
				when Inward_Comments like '%' + @searchText + '%' then 'a11'
				when Grade like '%' + @searchText + '%' then 'a12'
				

		 end as priority,
         T.*
		from #temp as T
	) as C
	where priority is not null
	order by priority asc
	end

drop table #temp