use FactoryData_1

CREATE TABLE Payments_Voucher (
    Voucher_ID int IDENTITY(1,1) PRIMARY KEY,
    Payment_Date datetime,
    Input_Date datetime,
    Customer_ID int
);

CREATE TABLE Payments (
    Payment_ID int IDENTITY(1,1) PRIMARY KEY,
    Payment_Voucher_ID int FOREIGN KEY REFERENCES Payments_Voucher(Voucher_ID),
    Sales_Voucher_ID int FOREIGN KEY REFERENCES Sales_Voucher(Voucher_ID),
    Payment_Amount decimal(18,2)
);