use FactoryData

--RUN IN Polyester: Y		Cotton: N
alter table Sales_Voucher drop column Carton_No_Arr;
alter table Sales_Voucher drop column Carton_Fiscal_Year;
