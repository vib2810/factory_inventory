use FactoryData_1

--DROP TABLE IF EXISTS Payments;
--DROP TABLE IF EXISTS Payments_Voucher;

CREATE TABLE Payments_Voucher (
    Voucher_ID int IDENTITY(1,1) PRIMARY KEY,
    Payment_Date datetime,
    Input_Date datetime,
    Customers varchar(50),
	Narration text
);

CREATE TABLE Payments (
    Payment_ID int IDENTITY(1,1) PRIMARY KEY,
    Payment_Voucher_ID int FOREIGN KEY REFERENCES Payments_Voucher(Voucher_ID),
    Sales_Voucher_ID int FOREIGN KEY REFERENCES Sales_Voucher(Voucher_ID),
    Payment_Amount decimal(18,2)
);

ALTER TABLE Sales_Voucher
ADD DO_Payment_Closed bit;

UPDATE Sales_Voucher SET DO_Payment_Closed = 0;