---carton from to
DECLARE @from varchar(20)
DECLARE @to varchar(20)
    SET @from = '2020-05-28';
    SET @to = '2020-05-28';
    select *
    from
    (
		select 
         case when(T.Date_Of_Billing < @from AND((T.Date_Of_Issue IS NULL AND T.Date_Of_Sale IS Null) OR(T.Date_Of_Issue IS NULL AND T.Date_Of_Sale >= @from) OR(T.Date_Of_Sale IS NULL AND T.Date_Of_Issue >= @from))) then 1
         end as Opening,
         case when T.[Date_Of_Billing] between @from and @to then 1
         end as Transact_Input,
         case 
         when(T.[Date_Of_Issue] between @from and @to) then 1
         when(T.[Date_Of_Sale] between @from and @to) then 2
         end as Transact_Output,
         T.*
    from Carton as T
     where
        (T.Date_Of_Billing<@from AND ((T.Date_Of_Issue IS NULL AND T.Date_Of_Sale IS Null) OR(T.Date_Of_Issue IS NULL AND T.Date_Of_Sale >= @from) OR(T.Date_Of_Sale IS NULL AND T.Date_Of_Issue >= @from)))  OR
         T.[Date_Of_Billing] between @from and @to  OR
         (T.[Date_Of_Issue] between @from and @to) OR(T.[Date_Of_Sale] between @from and @to)
	) as C