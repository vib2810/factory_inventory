use FactoryData
Declare @sql varchar(max) = ''

select @sql = @sql + ' Alter Table '+t.name + ' Alter Column ' +c.name + ' varchar(20) NULL; '  from sys.columns c
inner join sys.tables t on c.object_id = t.object_id
where c.name='Machine_No'
SELEct @sql;

GO

use FactoryData
create table Quality_Before_Twist
(
	Quality_Before_Twist_ID int primary key Identity(1,1) not null, 
	Quality_Before_Twist varchar(30) unique not null,
)

create table Machine_No
(
	Machine_No_ID int primary key Identity(1,1) not null, 
	Machine_No varchar(20) not null,
)

INSERT INTO dbo.Quality_Before_Twist
    SELECT Quality_Before_Twist
    FROM Quality;  