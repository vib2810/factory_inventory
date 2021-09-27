use FactoryData_1

ALTER TABLE T_Inward_Carton DROP COLUMN Sale_Voucher_ID;
ALTER TABLE T_Inward_Carton DROP COLUMN Sale_Comments;
ALTER TABLE T_Inward_Carton DROP COLUMN Sale_Display_Order;
ALTER TABLE T_Inward_Carton DROP COLUMN Sold_Weight;

ALTER TABLE T_Repacked_Cartons DROP COLUMN Sale_Voucher_ID;
ALTER TABLE T_Repacked_Cartons DROP COLUMN Sale_Comments;
ALTER TABLE T_Repacked_Cartons DROP COLUMN Sale_Display_Order;
ALTER TABLE T_Repacked_Cartons DROP COLUMN Sold_Weight;

--CREATE TABLE T_Carton_Sales (
--    Carton_ID varchar(50) NOT NULL, 
--    Sales_Voucher_ID int NOT NULL,
--    Sale_Comments text NULL,
--	Sale_Display_Order int NOT NULL,
--	Sold_Weight decimal(7,3) NOT NULL
--);