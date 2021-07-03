use FactoryData_1
IF  NOT EXISTS (SELECT * FROM sys.objects 
WHERE object_id = OBJECT_ID(N'[dbo].[Print_Types]') AND type in (N'U'))
begin
CREATE TABLE Print_Types (
	Firm_Name varchar(50) NOT NULL,
	Address varchar(100) NOT NULL,
	GSTIN varchar(20) NOT NULL,
	Phone_Number varchar(15) NOT NULL,
	Email_ID varchar(25) NOT NULL,
	Print_Type_ID int NOT NULL Primary Key Identity(1,1),
);
end

go

insert into defaults values ('Print:Carton_Slip', 'Number of Styles', 2)
insert into defaults values ('Print:Carton_Slip', 'Default Print Type', 1) --specific default
go

--Manually insert values of Print in default to Print_Types table and then
delete from defaults where Default_Type='Print'
insert into defaults values ('Print', 'Default Print Type', 1) --global default
