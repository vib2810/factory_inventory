USE master;
create database Main;
go
use Main;
create table Firms_List
(
	Firm_Name varchar(100) NOT NULL,
	Active_User varchar(50) NULL,
	Firm_ID int NOT NULL Primary Key,
	Deleted tinyint NULL
);