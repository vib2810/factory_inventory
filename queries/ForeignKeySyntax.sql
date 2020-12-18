use FactoryData_1

alter table T_Carton_Inward_Voucher
add constraint FK_Company_ID
FOREIGN KEY (Company_ID) REFERENCES T_M_Company_Names(Company_ID)