use FactoryData_1

--DROP TABLE IF EXISTS Payments;
--DROP TABLE IF EXISTS Payments_Voucher;

--CREATE TABLE Payments_Voucher (
--    Voucher_ID int IDENTITY(1,1) PRIMARY KEY,
--    Payment_Date date,
--    Input_Date date,
--    Customers varchar(50),
--	Narration text
--);

--CREATE TABLE Payments (
--    Payment_ID int IDENTITY(1,1) PRIMARY KEY,
--    Payment_Voucher_ID int FOREIGN KEY REFERENCES Payments_Voucher(Voucher_ID),
--    Sales_Voucher_ID int FOREIGN KEY REFERENCES Sales_Voucher(Voucher_ID),
--    Payment_Amount decimal(18,2),
--	Display_Order int,
--	Comments text
--);

--ALTER TABLE Sales_Voucher
--ADD DO_Payment_Closed bit;

--UPDATE Sales_Voucher SET DO_Payment_Closed = 0;

-------------------------------------ALREADY RAN---------------------------------------------------------------

--ALTER TABLE Payments_Voucher
--ADD Deleted int;

--ALTER TABLE T_Sales_Voucher
--ADD DO_Payment_Closed bit;

--CREATE TABLE T_Payments_Voucher (
--    Voucher_ID int IDENTITY(1,1) PRIMARY KEY,
--    Payment_Date date,
--    Input_Date date,
--    Customers varchar(50),
--	Narration text
--);

--CREATE TABLE T_Payments (
--    Payment_ID int IDENTITY(1,1) PRIMARY KEY,
--    Payment_Voucher_ID int FOREIGN KEY REFERENCES T_Payments_Voucher(Voucher_ID),
--    Sales_Voucher_ID int FOREIGN KEY REFERENCES T_Sales_Voucher(Voucher_ID),
--    Payment_Amount decimal(18,2),
--	Display_Order int,
--	Comments text
--);

UPDATE T_Sales_Voucher SET DO_Payment_Closed = 0;

