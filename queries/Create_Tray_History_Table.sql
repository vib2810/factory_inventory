USE [FactoryInventory]

CREATE TABLE Tray_History (
	Tray_ID int NOT NULL Primary Key,
	Tray_Production_Date date NOT NULL,
	Tray_No varchar(20) not null,
	Spring varchar(20) NOT NULL,
	Number_Of_Springs int not null,
	Tray_Tare decimal(7,3) NOT NULL,
	Gross_Weight decimal(7,3) NOT NULL,
	Quality varchar(20) NOT NULL,
	Company_Name varchar(50) NOT NULL,
	Dyeing_Company_Name varchar(50) NOT NULL,
	Dyeing_In_Date date NOT NULL,
	Dyeing_Out_Date date NOT NULL,
	Batch_No int NOT NULL
);
