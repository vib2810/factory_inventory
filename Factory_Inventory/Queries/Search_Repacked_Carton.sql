SELECT temp4.*, T_M_Cones.Cone_Weight into #temp
FROM
	(SELECT temp3.*, T_M_Colours.Colour
	FROM	
		(SELECT temp2.*,T_M_Quality_Before_Job.Quality_Before_Job
		FROM	
			(SELECT temp1.*, T_Repacking_Voucher.Start_Date_Of_Production, T_Repacking_Voucher.Cone_ID
			FROM
				(SELECT T_Repacked_Cartons.Number_Of_Cones, T_Repacked_Cartons.Carton_ID, T_Repacked_Cartons.Gross_Weight, T_Repacked_Cartons.Printed, T_Repacked_Cartons.Carton_Weight, T_Repacked_Cartons.Quality_ID, T_Repacked_Cartons.Colour_ID, T_Repacked_Cartons.Date_Of_Production, T_Repacked_Cartons.Repacking_Voucher_ID, T_Repacked_Cartons.Carton_State, T_Repacked_Cartons.Carton_No, T_Repacked_Cartons.Net_Weight, T_Repacked_Cartons.Grade, CAST(T_Repacked_Cartons.Repack_Comments AS NVARCHAR(1000)) as Repack_Comments, T_Repacked_Cartons.Fiscal_Year, SUM(T_Carton_Sales.Sold_Weight) as Sold_Weight
				FROM T_Repacked_Cartons
				LEFT OUTER JOIN T_Carton_Sales
				ON T_Carton_Sales.Carton_ID = T_Repacked_Cartons.Carton_ID 
				GROUP BY T_Repacked_Cartons.Number_Of_Cones, T_Repacked_Cartons.Carton_ID, T_Repacked_Cartons.Gross_Weight, T_Repacked_Cartons.Printed, T_Repacked_Cartons.Carton_Weight, T_Repacked_Cartons.Quality_ID, T_Repacked_Cartons.Colour_ID, T_Repacked_Cartons.Date_Of_Production, T_Repacked_Cartons.Repacking_Voucher_ID, T_Repacked_Cartons.Carton_State, T_Repacked_Cartons.Carton_No, T_Repacked_Cartons.Net_Weight, T_Repacked_Cartons.Grade, CAST(T_Repacked_Cartons.Repack_Comments AS NVARCHAR(1000)), T_Repacked_Cartons.Fiscal_Year) as temp1
			LEFT OUTER JOIN T_Repacking_Voucher
			ON T_Repacking_Voucher.Voucher_ID = temp1.Repacking_Voucher_ID) as temp2
		LEFT OUTER JOIN T_M_Quality_Before_Job
		ON T_M_Quality_Before_Job.Quality_Before_Job_ID = temp2.Quality_ID) as temp3
	LEFT OUTER JOIN T_M_Colours
	ON T_M_Colours.Colour_ID = temp3.Colour_ID) as temp4
LEFT OUTER JOIN T_M_Cones
ON T_M_Cones.Cone_ID = temp4.Cone_ID

if @date = 1
	begin
    select *
    from
    (
		
		select 
			case when Date_Of_Production like @searchText then 'd1'
				when Start_Date_Of_Production like @searchText then 'd3'
				when Carton_State like @searchText then 'd4'
				
				when Date_Of_Production like '%' + @searchText + '%' then 'da1'
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
				when Sold_Weight like @searchText then '5'
				when Grade like @searchText then '8'
				when Repack_Comments like @searchText then '9'
				when Fiscal_Year like @searchText then '11'

				when Carton_No like '%' + @searchText + '%' then 'a1'
				when Quality_Before_Job like '%' + @searchText + '%' then 'a2'
				when Colour like '%' + @searchText + '%' then 'a3'
				when Net_Weight like '%' + @searchText + '%' then 'a4'
				when Sold_Weight like '%' + @searchText + '%' then 'a5'
				when Grade like '%' + @searchText + '%' then 'a8'
				when Repack_Comments like '%' + @searchText + '%' then 'a9'
				when Fiscal_Year like '%' + @searchText + '%' then 'a11'

		 end as priority,
         T.*
		from #temp as T
	) as C
	where priority is not null
	order by priority asc
	end

drop table #temp