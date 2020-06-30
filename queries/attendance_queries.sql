USE master;  
CREATE DATABASE [FactoryAttendance]  
ON      ( NAME = N'FactoryAttendance', FILENAME = 'D:\Databases\FactoryAttendance.mdf') 
GO
use FactoryAttendance

create table Employees
(
	Employee_ID int primary key identity(1,1) not null,
	Employee_Name varchar (50) not null,
	Employee_Group varchar(50) not null,
	Date_Of_Joining date not null,
)
GO
create table Salary
(
	Employee_ID int FOREIGN KEY REFERENCES Employees(Employee_ID),
	Change_Date date not null,
	Salary decimal(8,2) not null
)
GO 
CREATE CLUSTERED INDEX Employee_ID
    ON dbo.Salary(Employee_ID);

create table Attendance_Log
(
	Employee_ID int FOREIGN KEY REFERENCES Employees(Employee_ID),
	Record_Date date not null,
	Attendance decimal(4,3) not null,
	Comments text null,
)
GO
CREATE CLUSTERED INDEX Employee_ID
    ON dbo.Attendance_Log(Employee_ID);

---check if its being used
CREATE NONCLUSTERED INDEX Record_Date
    ON dbo.Attendance_Log(Record_Date Desc);
GO
