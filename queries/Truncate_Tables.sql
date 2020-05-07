use FactoryData

truncate table Batch; 
truncate table BillNos_Voucher;
DBCC CHECKIDENT ('[BillNos_Voucher]', RESEED, 0);
GO
truncate table Carton;
truncate table Carton_Produced;
truncate table Carton_Production_Voucher;
DBCC CHECKIDENT ('[Carton_Production_Voucher]', RESEED, 0);
GO
truncate table Dyeing_Inward_Voucher;
DBCC CHECKIDENT ('[Dyeing_Inward_Voucher]', RESEED, 0);
GO
truncate table Dyeing_Issue_Voucher;
DBCC CHECKIDENT ('[Dyeing_Issue_Voucher]', RESEED, 0);
GO
truncate table Sales_Voucher;
DBCC CHECKIDENT ('[Sales_Voucher]', RESEED, 0);
GO
truncate table Tray_Active;
DBCC CHECKIDENT ('[Tray_Active]', RESEED, 0);
GO
truncate table Tray_Voucher;
DBCC CHECKIDENT ('[Tray_Voucher]', RESEED, 0);
GO
truncate table Tray_History;
truncate table Twist_Voucher;
DBCC CHECKIDENT ('[Twist_Voucher]', RESEED, 0);
GO
truncate table Carton_Voucher;
DBCC CHECKIDENT ('[Carton_Voucher]', RESEED, 0);
GO











