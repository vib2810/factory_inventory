use FactoryData
alter table Quality alter column Print_Colour varchar(30) NULL;
alter table Batch add Grade varchar(10) NULL;
update Batch set Grade='1st';

update Colours set Colours = 'Deep Gold' where Colours = 'Dp. Gold';

update Batch set Colour = 'Deep Gold' where Colour = 'Dp. Gold';
update Carton_Produced set Colour = 'Deep Gold' where Colour = 'Dp. Gold';
update Carton_Production_Voucher set Colour = 'Deep Gold' where Colour = 'Dp. Gold';
update Dyeing_Issue_Voucher set Colour = 'Deep Gold' where Colour = 'Dp. Gold';
update Batch set Colour = 'Deep Gold' where Colour = 'Dp. Gold';


