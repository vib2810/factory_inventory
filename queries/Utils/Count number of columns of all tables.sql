use FactoryInventory

SELECT Table_Name, count(*) as [No.of Columns]
FROM INFORMATION_SCHEMA.COLUMNS
WHERE table_schema = 'dbo' -- schema name
group by table_name