CREATE PROCEDURE AddEmployee @empname varchar(50), @groupID int, @joiningDate varchar(20), @salary decimal(8,2)
AS

BEGIN TRANSACTION

begin try
	DECLARE @employeeID int;
	INSERT INTO Employees (Employee_Name, Group_ID, Date_Of_Joining) VALUES (@empname, @groupID, @joiningDate);
	SELECT @employeeID = SCOPE_IDENTITY();
	INSERT INTO Salary VALUES (@employeeID, @joiningDate, @salary);
    COMMIT TRANSACTION
end try
begin catch
    ROLLBACK TRANSACTION
end catch