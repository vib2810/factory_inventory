use FactoryData_1
SELECT temp3.*, T_M_Company_Names.Company_Name into #temp4
FROM
    (SELECT temp2.*, T_M_Colours.Colour
    FROM
        (SELECT temp1.*, T_M_Quality_Before_Job.Quality_Before_Job
        FROM
            (SELECT T_Carton_Inward_Voucher.*, T_Inward_Carton.Carton_ID, T_Inward_Carton.Carton_No, T_Inward_Carton.Quality_ID, T_Inward_Carton.Colour_ID, T_Inward_Carton.Net_Weight, T_Inward_Carton.Buy_Cost, T_Inward_Carton.Inward_Voucher_ID, T_Inward_Carton.Comments, T_Inward_Carton.Bill_Type, T_Inward_Carton.Grade
            FROM T_Carton_Inward_Voucher
            FULL OUTER JOIN T_Inward_Carton
            ON T_Carton_Inward_Voucher.Voucher_ID = T_Inward_Carton.Inward_Voucher_ID) as temp1
        LEFT OUTER JOIN T_M_Quality_Before_Job
        ON T_M_Quality_Before_Job.Quality_Before_Job_ID = temp1.Quality_ID) as temp2
    LEFT OUTER JOIN T_M_Colours
    ON T_M_Colours.Colour_ID = temp2.Colour_ID) as temp3
LEFT OUTER JOIN T_M_Company_Names
ON T_M_Company_Names.Company_ID = temp3.Company_ID;
select distinct t.[Voucher_ID]
    ,STUFF((SELECT  ', ' + t1.Carton_No
        from #temp4 t1
        where t.[Voucher_ID] = t1.[Voucher_ID]
        FOR XML PATH(''), TYPE
        ).value('.', 'NVARCHAR(MAX)')
    ,1,2,'') Carton_No_Arr
    ,STUFF((SELECT distinct ', ' + t1.Colour
        from #temp4 t1
        where t.[Voucher_ID] = t1.[Voucher_ID]
        FOR XML PATH(''), TYPE
        ).value('.', 'NVARCHAR(MAX)')
    ,1,2,'') Colour_Arr
	,STUFF((SELECT distinct ', ' + t1.Grade
        from #temp4 t1
        where t.[Voucher_ID] = t1.[Voucher_ID]
        FOR XML PATH(''), TYPE
        ).value('.', 'NVARCHAR(MAX)')
    ,1,2,'') Grade_Arr
    ,STUFF((SELECT distinct ', ' + t1.Quality_Before_Job
        from #temp4 t1
        where t.[Voucher_ID] = t1.[Voucher_ID]
        FOR XML PATH(''), TYPE
        ).value('.', 'NVARCHAR(MAX)')
    ,1,2,'') Quality_Before_Job_Arr
    ,STUFF((SELECT  ', ' + CONVERT(VARCHAR, t1.Comments)
        from #temp4 t1
        where t.[Voucher_ID] = t1.[Voucher_ID]
        FOR XML PATH(''), TYPE
        ).value('.', 'NVARCHAR(MAX)')
    ,1,2,'') Comments_Arr
    ,t.Bill_No, t.Date_Of_Billing, t.Company_Name, t.Date_Of_Input, t.Deleted, t.Fiscal_Year, t.Bill_Type, CONVERT(VARCHAR, t.Narration) Narration
,(Select sum(Net_Weight)from((select Net_Weight from #temp4 where [Voucher_ID] = t.[Voucher_ID] )) t1) Net_Weight
,(Select sum(Buy_Cost)from((select Buy_Cost from #temp4 where [Voucher_ID] = t.[Voucher_ID] )) t1) Buy_Cost
,(Select count(Voucher_ID)from((select Voucher_ID from #temp4 where [Voucher_ID] = t.[Voucher_ID] )) t1) Number_Of_Cartons
from #temp4 t;
drop table #temp4;