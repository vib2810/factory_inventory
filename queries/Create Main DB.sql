USE master;
GO
ALTER DATABASE Main SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
GO
drop database Main;
go
create database Main;
go
use Main;
create table Firms_List
(
	Firm_ID int NOT NULL Identity(1,1) Primary Key,
	Firm_Name varchar(100) NOT NULL,
	Active_User varchar(50) NULL
);

insert into Firms_List values ('Krishna Sales and Industries',NULL)