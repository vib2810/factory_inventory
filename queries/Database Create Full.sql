use FactoryData
/* Carton */

/*Carton Table: 12 Columns*/
--CREATE TABLE Carton (
--	Carton_No varchar(20) NOT NULL,
--	Carton_State int NOT NULL,
--	Date_Of_Input date NOT NULL,
--	Date_Of_Billing date NOT NULL,
--	Bill_No varchar(20) NOT NULL,
--	Quality varchar(20) NOT NULL,
--	Company_Name varchar(50) NOT NULL,
--	Carton_Weight decimal(7,3) NOT NULL,
--	Buy_Cost decimal(7,3) NOT NULL,
--	Sell_Cost decimal(7,3) NULL,
--	Date_Of_Issue date NULL,
--	Date_Of_Sale date NULL,
--	Fiscal_Year varchar(15) NOT NULL
--);

--GO
--CREATE NONCLUSTERED INDEX [Carton_No] ON [dbo].[Carton]
--(
--	[Carton_No] ASC
--)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
--GO

--GO
--CREATE NONCLUSTERED INDEX [Carton_State] ON [dbo].[Carton]
--(
--	[Carton_State] ASC
--)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
--GO

--GO
--CREATE NONCLUSTERED INDEX [Date_Of_Input] ON [dbo].[Carton]
--(
--	[Date_Of_Input] ASC
--)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
--GO

--GO
--CREATE NONCLUSTERED INDEX [Quality] ON [dbo].[Carton]
--(
--	[Quality] ASC
--)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
--GO

--/*Carton Voucher Table: 12 columns*/
--CREATE TABLE Carton_Voucher (
--	Voucher_ID int NOT NULL Identity(1,1),
--	Date_Of_Input date NOT NULL,
--	Date_Of_Billing date NOT NULL,
--	Bill_No varchar(20) NOT NULL Primary Key,
--	Quality text NOT NULL,
--	Quality_Arr text NOT NULL,
--	Company_Name varchar(50) NOT NULL,
--	Number_of_Cartons int NOT NULL,
--	Carton_No_Arr text NOT NULL,
--	Carton_Weight_Arr text NOT NULL,
--	Net_Weight decimal(10,3) NOT NULL,
--	Buy_Cost text NOT NULL,
--	Fiscal_Year varchar(15) NOT NULL

--);

--CREATE NONCLUSTERED INDEX [Date_Of_Input] ON [dbo].[Carton_Voucher]
--(
--	[Date_Of_Input] ASC
--)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
--GO

--GO
--CREATE NONCLUSTERED INDEX [Voucher_ID] ON [dbo].[Carton_Voucher]
--(
--	[Voucher_ID] ASC
--)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
--GO

--/*-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------*/

--/*Twist Voucher*/
--/*Twist Voucher Table: 6 columns*/
--CREATE TABLE Twist_Voucher(
--	Voucher_ID int NOT NULL Identity(1,1) Primary Key,
--	Date_Of_Input date NOT NULL,
--	Date_Of_Issue date NOT NULL,
--	Quality varchar(20) NOT NULL,
--	Company_Name varchar(50) NOT NULL,
--	Carton_No_Arr text NOT NULL,
--	Number_of_Cartons int NOT NULL,
--	Fiscal_Year varchar(15) NOT NULL,
--	Carton_Fiscal_Year varchar(15) NOT NULL
--);

--GO
--CREATE NONCLUSTERED INDEX [Date_Of_Input] ON [dbo].[Twist_Voucher]
--(
--	[Date_Of_Input] ASC
--)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
--GO

--GO
--CREATE NONCLUSTERED INDEX [Voucher_ID] ON [dbo].[Twist_Voucher]
--(
--	[Voucher_ID] ASC
--)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
--GO

--/*-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------*/


--/*Sales Voucher*/
--/*Sales Voucher Table: 8 columns*/
--CREATE TABLE Sales_Voucher(
--	Voucher_ID int NOT NULL Identity(1,1) Primary Key,
--	Date_Of_Input date NOT NULL,
--	Date_Of_Issue date NOT NULL,
--	Quality varchar(20) NOT NULL,
--	Company_Name varchar(50) NOT NULL,
--	Customer varchar(50) NOT NULL,
--	Selling_Price decimal(7,2) NOT NULL,
--	Carton_No_Arr text NOT NULL,
--	Number_of_Cartons int NOT NULL,
--	Fiscal_Year varchar(15) NOT NULL,
--	Carton_Fiscal_Year varchar(15) NOT NULL
--);

--GO
--CREATE NONCLUSTERED INDEX [Date_Of_Input] ON [dbo].[Sales_Voucher]
--(
--	[Date_Of_Input] ASC
--)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
--GO

--GO
--CREATE NONCLUSTERED INDEX [Voucher_ID] ON [dbo].[Sales_Voucher]
--(
--	[Voucher_ID] ASC
--)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
--GO

--/*-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------*/

--/*Tray Voucher*/
--/*Tray Active Table 15 columns*/
--CREATE TABLE Tray_Active(
--	Tray_Production_Date date NOT NULL,
--	Tray_No varchar(20) NOT NULL Primary Key,
--	Spring varchar(20) NOT NULL,
--	Number_of_Springs int NOT NULL,
--	Tray_Tare decimal(7,3) NOT NULL,
--	Gross_Weight decimal(7,3) NOT NULL,
--	Quality varchar(20) NOT NULL,
--	Company_Name varchar(50) NOT NULL,
--	Dyeing_In_Date date NULL,
--	Dyeing_Out_Date date NULL,
--	Tray_State int NOT NULL,
--	Batch_No int NULL,
--	Tray_ID int NOT NULL Identity(1,1),
--	Net_Weight decimal(7,3) NULL,
--	Dyeing_Company_Name varchar(40) NULL,
--	Fiscal_Year varchar(15) NOT NULL

--);

--GO
--CREATE NONCLUSTERED INDEX [Batch_No] ON [dbo].[Tray_Active]
--(
--	[Batch_No] ASC
--)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
--GO

--GO
--CREATE NONCLUSTERED INDEX [Tray_ID] ON [dbo].[Tray_Active]
--(
--	[Tray_ID] ASC
--)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
--GO

--GO
--CREATE NONCLUSTERED INDEX [Tray_No] ON [dbo].[Tray_Active]
--(
--	[Tray_No] ASC
--)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
--GO

--GO
--CREATE NONCLUSTERED INDEX [Tray_Production_Date] ON [dbo].[Tray_Active]
--(
--	[Tray_Production_Date] ASC
--)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
--GO

--/*Tray History Table: 14 columns*/
--CREATE TABLE Tray_History(
--	Tray_Production_Date date NOT NULL,
--	Tray_No varchar(20) NOT NULL,
--	Spring varchar(20) NOT NULL,
--	Number_of_Springs int NOT NULL,
--	Tray_Tare decimal(7,3) NOT NULL,
--	Gross_Weight decimal(7,3) NOT NULL,
--	Quality varchar(20) NOT NULL,
--	Company_Name varchar(50) NOT NULL,
--	Dyeing_In_Date date NOT NULL,
--	Dyeing_Out_Date date NOT NULL,
--	Batch_No int NOT NULL,
--	Tray_ID int NOT NULL Primary Key,
--	Net_Weight decimal(7,3) NULL,
--	Dyeing_Company_Name varchar(40) NOT NULL,
--	Fiscal_Year varchar(15) NOT NULL

--);

--GO
--CREATE NONCLUSTERED INDEX [Batch_No] ON [dbo].[Tray_History]
--(
--	[Batch_No] ASC
--)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
--GO

--GO
--CREATE NONCLUSTERED INDEX [Tray_ID] ON [dbo].[Tray_History]
--(
--	[Tray_ID] ASC
--)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
--GO

--GO
--CREATE NONCLUSTERED INDEX [Tray_No] ON [dbo].[Tray_History]
--(
--	[Tray_No] ASC
--)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
--GO

--GO
--CREATE NONCLUSTERED INDEX [Tray_Production_Date] ON [dbo].[Tray_History]
--(
--	[Tray_Production_Date] ASC
--)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
--GO

--/*Tray Voucher History Table: 12 columns*/
--CREATE TABLE Tray_Voucher(
--	Voucher_ID int NOT NULL Identity(1,1) Primary Key,
--	Tray_ID int NOT NULL,
--	Tray_Production_Date date NOT NULL,
--	Tray_No varchar(20) NOT NULL,
--	Spring varchar(20) NOT NULL,
--	Number_of_Springs int NOT NULL,
--	Tray_Tare decimal(7,3) NOT NULL,
--	Gross_Weight decimal(7,3) NOT NULL,
--	Quality varchar(20) NOT NULL,
--	Company_Name varchar(50) NOT NULL,
--	Input_Date date NULL,
--	Net_Weight decimal(7,3) NULL,
--	Fiscal_Year varchar(15) NOT NULL

--);

--GO
--CREATE NONCLUSTERED INDEX [Voucher_ID] ON [dbo].[Tray_Voucher]
--(
--	[Voucher_ID] DESC
--)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
--GO

--GO
--CREATE NONCLUSTERED INDEX [Input_Date] ON [dbo].[Tray_Voucher]
--(
--	[Input_Date] ASC
--)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
--GO

--/*-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------*/


--/*Dyeing Issue Voucher*/
--/*Dyeing Issue Voucher Table: 12 columns*/
--CREATE TABLE Dyeing_Issue_Voucher(
--	Voucher_ID int NOT NULL Identity(1,1) Primary Key,
--	Date_Of_Input date NOT NULL,
--	Date_Of_Issue date NOT NULL,
--	Quality varchar(20) NOT NULL,
--	Company_Name varchar(50) NOT NULL,
--	Colour varchar(20) NOT NULL,
--	Dyeing_Company_Name varchar(50) NOT NULL,
--	Tray_No_Arr text NOT NULL,
--	Number_of_Trays int NOT NULL,
--	Batch_No int NOT NULL,
--	Dyeing_Rate decimal(5,2) NOT NULL,
--	Tray_ID_Arr text NOT NULL,
--	Fiscal_Year varchar(15) NOT NULL

--);

--GO
--CREATE NONCLUSTERED INDEX [Voucher_ID] ON [dbo].[Dyeing_Issue_Voucher]
--(
--	[Voucher_ID] DESC
--)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
--GO

--GO
--CREATE NONCLUSTERED INDEX [Date_Of_Input] ON [dbo].[Dyeing_Issue_Voucher]
--(
--	[Date_Of_Input] ASC
--)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
--GO

--/*-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------*/

--/* Batch */
--/*14 columns*/
/* State 1: Produced. In dyeing
   State 2: Recieved from dyeing. Waiting for production
   State 3: Produced. Waiting for sale
*/
--CREATE TABLE Batch (
--	Batch_No int NOT NULL,
--	Colour varchar(20) NOT NULL,
--	Dyeing_Company_Name varchar(50) NOT NULL,
--	Dyeing_In_Date date NULL,
--	Dyeing_Out_Date date NOT NULL,
--	Date_Of_Packing date NULL,
--	Tray_ID_Arr text NOT NULL,
--	Net_Weight decimal(7,3) NOT NULL,
--	Quality varchar(20) NOT NULL,
--	Company_Name varchar(50) NOT NULL,
--	Number_Of_Trays int NOT NULL,
--	Batch_State int NOT NULL,
--	Dyeing_Rate decimal(5,2) NOT NULL,
--	Bill_No int NULL,
--	Fiscal_Year varchar(15) NOT NULL,
--	Bill_Date date NULL,
--	Voucher_ID int NULL,
--  Printed tinyint NULL,
--);

--GO
--CREATE NONCLUSTERED INDEX [Batch_No] ON [dbo].[Batch]
--(
--	[Batch_No] ASC
--)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
--GO

--GO
--CREATE NONCLUSTERED INDEX [Bill_No] ON [dbo].[Batch]
--(
--	[Bill_No] ASC
--)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
--GO

--GO
--CREATE NONCLUSTERED INDEX [Bill_Date] ON [dbo].[Batch]
--(
--	[Bill_Date] DESC
--)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
--GO

--/*-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------*/

--/*Dyeing Inward Voucher*/
--/*Dyeing Inward Voucher Table: 6 columns*/
--CREATE TABLE Dyeing_Inward_Voucher(
--	Voucher_ID int NOT NULL Primary Key Identity(1,1),
--	Date_Of_Input date NOT NULL,
--	Inward_Date date NOT NULL,
--	Dyeing_Company_Name varchar(40) NOT NULL,
--	Bill_No int NOT NULL,
--	Batch_No_Arr text NOT NULL,
--	Fiscal_Year varchar(15) NOT NULL,
--	Batch_Fiscal_Year varchar(15) NOT NULL,
--	Bill_Date date NULL
--);

--GO
--CREATE NONCLUSTERED INDEX [Voucher_ID] ON [dbo].[Dyeing_Inward_Voucher]
--(
--	[Voucher_ID] DESC
--)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
--GO

--GO
--CREATE NONCLUSTERED INDEX [Date_Of_Input] ON [dbo].[Dyeing_Inward_Voucher]
--(
--	[Date_Of_Input] ASC
--)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
--GO

--GO
--CREATE NONCLUSTERED INDEX [Bill_Date] ON [dbo].[Dyeing_Inward_Voucher]
--(
--	[Bill_Date] DESC
--)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
--GO

--/*-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------*/

--/*Bill Numbers Voucher */
--/* Bill Numbers Voucher: 5 columns*/
--CREATE TABLE BillNos_Voucher
--(
--	Voucher_ID int NULL Primary Key Identity(1,1),
--	Date_Of_Input date NOT NULL,
--	Batch_No_Arr text NOT NULL,
--	Dyeing_Company_Name varchar(50) NOT NULL,
--	Bill_No int NOT NULL,
--	Fiscal_Year varchar(15) NOT NULL,
--	Batch_Fiscal_Year varchar(15) NOT NULL,
--	Bill_Date date NULL,
--);

--GO
--CREATE NONCLUSTERED INDEX [Voucher_ID] ON [dbo].[BillNos_Voucher]
--(
--	[Voucher_ID] DESC
--)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
--GO

--GO
--CREATE NONCLUSTERED INDEX [Date_Of_Input] ON [dbo].[BillNos_Voucher]
--(
--	[Date_Of_Input] ASC
--)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
--GO

--GO
--CREATE NONCLUSTERED INDEX [Bill_Date] ON [dbo].[BillNos_Voucher]
--(
--	[Bill_Date] DESC
--)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
--GO


/*-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------*/

/*Carton Production Voucher */
/* Carton Production Voucher :  13 columns*/
create table Carton_Production_Voucher
(
	Voucher_ID int NOT NULL Primary Key Identity(1,1),
	Date_Of_Input date NOT NULL,
	Colour varchar(20) NOT NULL,
	Quality varchar(20) NOT NULL,
	Dyeing_Company_Name varchar(50) NOT NULL,
	Batch_No_Arr text NOT NULL,
	Carton_No_Production_Arr text NULL,
	Fiscal_Year varchar(20) NOT NULL,
	Net_Batch_Weight decimal(8,3) NOT NULL,
	Net_Carton_Weight decimal(8,3) NOT NULL,
	Oil_Gain decimal(5,2) NOT NULL,
	Voucher_Closed tinyint NOT NULL  
);

GO
CREATE NONCLUSTERED INDEX [Voucher_ID] ON [dbo].[Carton_Production_Voucher]
(
	[Voucher_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO

/* Carton Produced :  19 columns*/
CREATE TABLE Carton_Produced
(
	Carton_No varchar(20) NOT NULL,
	Carton_State int NOT NULL,
	Date_Of_Production date NOT NULL,
	Quality varchar(20) NOT NULL,
	Colour varchar(20) NOT NULL,
	Batch_No_Arr text NOT NULL,
	Dyeing_Company_Name varchar(50) NOT NULL,
	Carton_Weight decimal(7,3) NOT NULL,
	Number_Of_Cones int NOT NULL,
	Cone_Weight decimal(6,3) NOT NULL,
	Gross_Weight decimal(7,3) NOT NULL,
	Net_weight decimal(7,3) NOT NULL,
	Sale_Rate decimal(7,3) NULL,
	Sale_Bill_Date date NULL,
	Sale_Bill_No varchar(20) NULL,
	Sale_DO_No varchar(10) NULL,
	Sale_DO_Date date NULL,
	Customer_Name varchar(50) NULL,
	Fiscal_Year varchar(15) NOT NULL
);



/*-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------*/

/* Utility Tables*/
/* Colours Table: 3 columns*/
--CREATE TABLE Colours (
--	Colours varchar(20) NOT NULL,
--	Quality varchar(20) NOT NULL,
--	Dyeing_Rate decimal(5,2) NOT NULL
--);

--/* Company Names Table: 1 column*/
--CREATE TABLE Company_Names(
--	Company_Names varchar(50) NOT NULL
--);

--/* Customers Table: 1 column*/
--CREATE TABLE Customers(
--	Customers varchar(50) NOT NULL
--);

--/* Customers Table: 1 column*/
--CREATE TABLE Dyeing_Company_Names(
--	Dyeing_Company_Names varchar(40) NOT NULL
--);

--/* Quality: 1 column*/
--CREATE TABLE Quality(
--	Quality varchar(20) NOT NULL
--);

--/*Spring: 2 columns*/
--CREATE TABLE Spring (
--	Spring varchar(20) Primary Key NOT NULL,
--	Spring_Weight decimal(4,3) NOT NULL,
--);

--/* Login Log Table: 4 columns*/
--CREATE TABLE dbo.[Log](
--	Username varchar(50) NOT NULL,
--	LoginTime datetime NULL,
--	LogoutTime datetime NULL,
--	TimeDuration time(7) NULL
--);

--GO
--CREATE NONCLUSTERED INDEX [LoginTime] ON [dbo].[Log]
--(
--	[LoginTime] DESC
--)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
--GO

--/* Users Table: 4 columns*/
--CREATE TABLE Users(
--	SLNO int NOT NULL Identity(1,1),
--	Username varchar(20) NOT NULL Primary Key,
--	PasswordHash binary(40) NOT NULL,
--	AccessLevel tinyint NULL
--);

--CREATE TABLE Fiscal_Year
--(
--	Fiscal_Year varchar(15) NOT NULL Primary Key,
--	Highest_Batch_No int NOT NULL,
--	Highest_Carton_Production_No varchar(20) NOT NULL,
--);

--create table Cones
--(
--	Cones int NOT NULL Primary Key
--);
