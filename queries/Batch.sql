USE [FactoryInventory]

CREATE TABLE Batch (
	Batch_No int NOT NULL Primary Key Identity(1,1),
	Colour varchar(20) NOT NULL,
	Dyeing_Company_Name varchar(50) NOT NULL,
	Dyeing_In_Date date NOT NULL,
	Dyeing_Out_Date date NULL,
	Date_Of_Packing date NULL,
	Tray_ID_Arr text NOT NULL,
	Net_Weight decimal(7,3) NOT NULL,
	Quality varchar(20) NOT NULL,
	Company_Name varchar(50) NOT NULL,
	Number_Of_Trays int NOT NULL,
);
