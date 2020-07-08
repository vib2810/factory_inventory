USE master;  
drop database FactoryAttendance
CREATE DATABASE [FactoryAttendance]  
ON      ( NAME = N'FactoryAttendance', FILENAME = 'D:\Databases\FactoryAttendance.mdf') 
GO
use FactoryAttendance
--Group Table
create table Group_Names
(
	Group_ID int primary key identity(1,1) not null,
	Group_Name varchar (50) not null,
)
GO

--Employee Table
create table Employees
(
	Employee_ID int primary key identity(1,1) not null,
	Employee_Name varchar (50) not null,
	Group_ID int FOREIGN KEY REFERENCES  Group_Names(Group_ID) not null,
)
GO
CREATE NONCLUSTERED INDEX Group_ID
    ON dbo.Employees(Group_ID);

--Salary Table
create table Salary
(
	Employee_ID int FOREIGN KEY REFERENCES Employees(Employee_ID) not null,
	Change_Date date not null,
	Salary decimal(8,2) not null
)
GO 
CREATE CLUSTERED INDEX Employee_ID
    ON dbo.Salary(Employee_ID);

create table Employee_Session 
(
	Session_ID int Primary Key Identity(1,1) NOT NULL,
	Employee_ID int FOREIGN KEY REFERENCES Employees(Employee_ID) not null,
	Begin_Date date NOT NULL,
	End_Date date NULL
)

CREATE NONCLUSTERED INDEX Employee_ID
    ON dbo.Employee_Session(Employee_ID Desc);
GO

--Attendance Table
create table Attendance_Log
(
	Session_ID int FOREIGN KEY REFERENCES Employee_Session(Session_ID) not null,
	Record_Date date not null,
	Attendance decimal(4,3) not null,
	Comments text null,
	CONSTRAINT unq UNIQUE(Session_ID, Record_Date)
)
GO
CREATE CLUSTERED INDEX Session_ID
    ON dbo.Attendance_Log(Session_ID);

---check if its being used
CREATE NONCLUSTERED INDEX Record_Date
    ON dbo.Attendance_Log(Record_Date Desc);
GO

