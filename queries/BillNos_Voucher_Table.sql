USE [FactoryInventory]
CREATE TABLE BillNos_Voucher (
	Voucher_ID int NOT NULL Primary Key Identity(1,1),
	Date_Of_Input date NOT NULL,
	Batch_No_Arr text NOT NULL,
	Dyeing_Company_Name varchar(50) NOT NULL,
	Bill_No int NOT NULL,
);
