use FactoryData_1

drop table z_inward
drop table z_cartons
drop table z_qualities
drop table z_colour


create table z_inward
(
	id int identity(1,1) not null,
	col1 varchar(50) not null,
	col2 varchar(50) not null,
	col3 varchar(50) not null,
	col4 varchar(50) not null,
);

create table z_cartons
(
	carton_id int identity(1,1) not null,
	quality_id int not null,
	colour_id int not null,
	col3 varchar(50) not null,
	inward_voucher_id int not null,
);

create table z_qualities
(
	quality_id int identity(1,1) not null,
	col3 varchar(50) not null,
);

create table z_colour
(
	colour_id int identity(1,1) not null,
	col3 varchar(50) not null,
);