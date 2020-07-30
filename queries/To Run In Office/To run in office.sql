use FactoryData

--RUN IN Polyester: Y 15-07		Cotton: N
--alter table Sales_Voucher drop column Carton_No_Arr;
--alter table Sales_Voucher drop column Carton_Fiscal_Year;

----RUN IN Polyester: N Cotton: N
--create table Opening_Stock
--(
--	Voucher_ID int NOT NULL Primary Key Identity(1,1),
--	Date_Of_Input date NOT NULL,
--	Voucher_Name varchar(50) NOT NULL,
--	Deleted tinyint NULL
--);

------RUN IN Polyester: N Cotton: N
--alter table Carton_Produced alter column Batch_No_Arr text NULL;
--alter table Carton_Produced alter column Dyeing_Company_Name varchar(50) NULL;
--alter table Carton_Produced alter column Carton_Weight decimal(7, 3) NULL;
--alter table Carton_Produced alter column Number_Of_Cones int NULL;
--alter table Carton_Produced alter column Cone_Weight decimal(6, 3) NULL;
--alter table Carton_Produced alter column Gross_Weight decimal(7, 3) NULL;
--alter table Carton_Produced alter column Batch_Fiscal_Year_Arr text NULL;

--RUN IN Polyester: N Cotton: N
--alter table Opening_Stock add Deleted tinyint NULL
alter table Carton_Produced add Opening_Voucher_ID int NULL


