USE master;
create database Main;
go
use Main;
create table Firms_List
(
	Firm_ID int NOT NULL Identity(1,1) Primary Key,
	Firm_Name varchar(100) NOT NULL,
	Active_User varchar(50) NULL,
	Deleted tinyint NULL
);