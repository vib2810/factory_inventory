SELECT temp4.*, T_Sales_Voucher.Sale_DO_No, T_Sales_Voucher.Fiscal_Year as DO_Fiscal_Year, T_Sales_Voucher.Sale_Rate
FROM
	(SELECT temp3.*, T_M_Company_Names.Company_Name
	FROM
		(SELECT temp2.*, T_M_Quality_Before_Job.Quality_Before_Job 
		FROM	
			(SELECT temp1.*, T_Repacking_Voucher.Date_Of_Production, T_Repacking_Voucher.Start_Date_Of_Production
			FROM
				(SELECT T_Inward_Carton.*, T_Carton_Inward_Voucher.Date_Of_Billing, T_Carton_Inward_Voucher.Bill_No 
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