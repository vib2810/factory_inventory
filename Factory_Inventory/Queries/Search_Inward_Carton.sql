--use FactoryData_1

--DECLARE @searchText nvarchar(50) = '45', @date tinyint = 0

SELECT temp4.*, T_Sales_Voucher.Sale_DO_No, T_Sales_Voucher.Fiscal_Year as DO_Fiscal_Year, T_Sales_Voucher.Sale_Rate, T_Sales_Voucher.Date_Of_Sale, T_Sales_Voucher.Type_Of_Sale into #temp
FROM
	(SELECT temp3.*, T_M_Company_Names.Company_Name
	FROM
		(SELECT temp2.*, T_M_Quality_Before_Job.Quality_Before_Job 
		FROM	
			(SELECT temp1.*, T_Repacking_Voucher.Date_Of_Production, T_Repacking_Voucher.Start_Date_Of_Production
			FROM
				(SELECT T_Inward_Carton.*, T_Carton_Inward_Voucher.Date_Of_Billing, T_Carton_Inward_Voucher.Bill_No, T_Carton_Inward_Voucher.Date_Of_Input 
				FROM T_Inward_Carton
				LEFT OUTER JOIN T_Carton_Inward_Voucher
				ON T_Carton_Inward_Voucher.Voucher_ID = T_Inward_Carton.Inward_Voucher_ID) as temp1
			LEFT OUTER JOIN T_Repacking_Voucher
			ON T_Repacking_Voucher.Voucher_ID = temp1.Repacking_Voucher_ID) as temp2
		LEFT OUTER JOIN T_M_Quality_Before_Job
		ON T_M_Quality_Before_Job.Quality_Before_Job_ID = temp2.Quality_ID) as temp3
	LEFT OUTER JOIN T_M_Company_Names
	ON T_M_Company_Names.Company_ID = temp3.Company_ID) as temp4
LEFT OUTER JOIN T_Sales_Voucher
ON T_Sales_Voucher.Voucher_ID = temp4.Sale_Voucher_ID


if @date = 1
	begin
    select *
    from
    (
		select 
			case when Date_Of_Billing like @searchText then 'd1'
				when Date_Of_Production like @searchText then 'd2'
				when Start_Date_Of_Production like @searchText then 'd3'
				when Date_Of_Sale like @searchText then 'd4'
				when Carton_State like @searchText then 'd5'
				
				when Date_Of_Billing like '%' + @searchText + '%' then 'da1'
				when Date_Of_Production like '%' + @searchText + '%' then 'da2'
				when Start_Date_Of_Production like '%' + @searchText + '%' then 'da3'
				when Date_Of_Sale like '%' + @searchText + '%' then 'da4'
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
				when Fiscal_Year like @searchText then '5'
				when Bill_No like @searchText then '6'
				when Sale_DO_No like @searchText then '7'
				when DO_Fiscal_Year like @searchText then '8'
				when Buy_Cost like @searchText then '9'
				when Sale_Rate like @searchText then '10'
				when Comments like @searchText then '11'
				when Grade like @searchText then '12'
				when Sale_Comments like @searchText then '13'
				

				when Carton_No like '%' + @searchText + '%' then 'a1'
				when Quality_Before_Job like '%' + @searchText + '%' then 'a2'
				when Company_Name like '%' + @searchText + '%' then 'a3'
				when Net_Weight like '%' + @searchText + '%' then 'a4'
				when Fiscal_Year like '%' + @searchText + '%' then 'a5'
				when Bill_No like '%' + @searchText + '%' then 'a6'
				when Sale_DO_No like '%' + @searchText + '%' then 'a7'
				when DO_Fiscal_Year like '%' + @searchText + '%' then 'a8'
				when Buy_Cost like '%' + @searchText + '%' then 'a9'
				when Sale_Rate like '%' + @searchText + '%' then 'a10'
				when Comments like '%' + @searchText + '%' then 'a11'
				when Grade like '%' + @searchText + '%' then 'a12'
				when Sale_Comments like '%' + @searchText + '%' then 'a13'
				

		 end as priority,
         T.*
		from #temp as T
	) as C
	where priority is not null
	order by priority asc
	end

drop table #temp