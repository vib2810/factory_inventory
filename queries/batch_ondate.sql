--SELECT * FROM Batch WHERE Dyeing_In_Date <='" + d.Date.ToString("yyyy-MM-dd").Substring(0, 10) + "' AND (Date_Of_Production>'" + d.Date.ToString("yyyy-MM-dd").Substring(0, 10) + "' OR Date_Of_Production IS NULL) AND Batch_State!=4 ORDER BY Dyeing_In_Date
--SELECT * FROM Batch WHERE Dyeing_Out_Date <='" + d.Date.ToString("yyyy-MM-dd").Substring(0, 10) + "' AND (Dyeing_In_Date>'" + d.Date.ToString("yyyy-MM-dd").Substring(0, 10) + "'  OR Dyeing_In_Date IS NULL) ORDER BY Dyeing_Out_Date
use FactoryData
DECLARE @from varchar(20)
DECLARE @to varchar(20)
    SET @from = '2020-06-04';
    select *
    from
    (
		select 
         case 
			when Dyeing_Out_Date <=@from AND (Dyeing_In_Date>@from OR Dyeing_In_Date IS NULL) then 1
			when Dyeing_In_Date <= @from AND Batch_State!=4 then
				(case 
					when (Start_Date_Of_Production <= @from AND Date_Of_Production IS NULL)  then 2
					when (Date_Of_Production >= @from OR Date_Of_Production IS NULL) then 3
				end)
		 end as dyecon,
         T.*
    from Batch as T 
		where Dyeing_Out_Date <=@from OR Dyeing_In_Date <= @from
	) as C where dyecon is not null