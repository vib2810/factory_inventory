use FactoryData

truncate table Carton;
truncate table Carton_Voucher;
DBCC CHECKIDENT ('[Carton_Voucher]', RESEED, 1);
GO

truncate table Twist_Voucher;
DBCC CHECKIDENT ('[Twist_Voucher]', RESEED, 1);
GO

truncate table Sales_Voucher;
DBCC CHECKIDENT ('[Sales_Voucher]', RESEED, 1);
GO
truncate table SalesBillNos_Voucher;
DBCC CHECKIDENT ('[SalesBillNos_Voucher]', RESEED, 1);
GO

truncate table Tray_Active;
DBCC CHECKIDENT ('[Tray_Active]', RESEED, 1);
GO
truncate table Tray_History;
truncate table Tray_Voucher;
DBCC CHECKIDENT ('[Tray_Voucher]', RESEED, 1);
GO

truncate table Batch; 
truncate table Dyeing_Issue_Voucher;
DBCC CHECKIDENT ('[Dyeing_Issue_Voucher]', RESEED, 1);
GO

truncate table Dyeing_Inward_Voucher;
DBCC CHECKIDENT ('[Dyeing_Inward_Voucher]', RESEED, 1);
GO

truncate table BillNos_Voucher;
DBCC CHECKIDENT ('[BillNos_Voucher]', RESEED, 1);
GO

truncate table Redyeing_Voucher;
DBCC CHECKIDENT ('[Redyeing_Voucher]', RESEED, 1);
GO

truncate table Carton_Produced;
truncate table Carton_Production_Voucher;
DBCC CHECKIDENT ('[Carton_Production_Voucher]', RESEED, 1);
GO


update Fiscal_Year set Highest_Batch_No=0;
update Fiscal_Year set Highest_Carton_Production_No='0'; 
update Fiscal_Year set Highest_1_DO_No='BB0';
update Fiscal_Year set Highest_0_DO_No='RR0';
truncate table dbo.[Log];

















