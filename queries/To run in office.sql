use FactoryData

alter table Carton add Inward_Voucher_ID int NULL
alter table Carton add TS_Voucher_ID int NULL
EXEC sp_rename 'Batch.Voucher_ID', 'Production_Voucher_ID', 'COLUMN';  
alter table Batch add Dyeing_Out_Voucher_ID int NULL
alter table Batch add Dyeing_In_Voucher_ID int NULL
alter table Batch add Bill_Voucher_ID int NULL
alter table Batch add Redyeing_Voucher_ID int NULL
alter table Carton_Produced add Production_Voucher_ID int NULL
alter table Carton_Produced add Sales_Voucher_ID int NULL
alter table Carton_Production_Voucher drop column Grades_Arr

--alter table Twist_Voucher add Net_Weight decimal(8,3) NULL

