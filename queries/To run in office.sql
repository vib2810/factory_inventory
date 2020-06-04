use FactoryData

alter table Quality alter column Print_Colour varchar(30) NULL;

alter table Batch add Grade varchar(10) NULL;
update Batch set Grade='1st';

