use FactoryData_1

alter table T_Repacking_Voucher
add constraint FK_Cone_ID
FOREIGN KEY (Cone_ID) REFERENCES T_M_Cones(Cone_ID)