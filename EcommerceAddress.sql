use [EcommerceAddress]
GO

create table Country (
	country_id int not null primary key,
	country_name nvarchar(50) not null,
	country_code  nvarchar(50) not null,
	province_id int not null,
	status bit not null,
	created_at nvarchar(50) not null,
	created_by nvarchar(50) not null,
	updated_at nvarchar(50) not null,
	updated_by nvarchar(50) not null
)

create table Province (
	province_id int not null primary key,
	province_name nvarchar(50) not null,
	province_code  nvarchar(50) not null,
	axis_meridian nvarchar(50) not null,
	country_id int not null,
	status bit not null,
	created_at nvarchar(50) not null,
	created_by nvarchar(50) not null,
	updated_at nvarchar(50) not null,
	updated_by nvarchar(50) not null
)

create table District (
	district_id int not null primary key,
	district_name nvarchar(50) not null,
	district_code  nvarchar(50) not null,
	province_id int not null,
	status bit not null,
	created_at nvarchar(50) not null,
	created_by nvarchar(50) not null,
	updated_at nvarchar(50) not null,
	updated_by nvarchar(50) not null
)

create table Ward (
	ward_id int not null primary key,
	ward_name nvarchar(50) not null,
	ward_code nvarchar(50) not null,
	district_id int not null,
	status bit not null,
	created_at nvarchar(50) not null,
	created_by nvarchar(50) not null,
	updated_at nvarchar(50) not null,
	updated_by nvarchar(50) not null
)

create table Address (
	address_id int not null primary key,
	address_text nvarchar(50) not null,
	country_id int not null,
	province_id int not null,
	district_id int not null,
	ward_id int not null,
	town_id int not null,
	status bit not null,
	latitude nvarchar(30) not null,
	longitude nvarchar(30) not null,
	notes nvarchar(100),
	constraint FK_ADDRESS_COUNTRY foreign key (country_id) references Country(country_id),
	constraint FK_ADDRESS_PROVINCE foreign key (province_id) references Province(province_id),
	constraint FK_ADDRESS_DISTRICT foreign key (district_id) references District(district_id),
	constraint FK_ADDRESS_WARD foreign key (ward_id) references Ward(ward_id)
)

/*Country Datas*/
INSERT [dbo].[Country] (country_id, country_name, country_code, province_id, status, created_at, created_by, updated_at, updated_by) VALUES (1, N'Vietnam', N'+84', 1, 1, 'A', 'B', 'C', 'D')
INSERT [dbo].[Country] (country_id, country_name, country_code, province_id, status, created_at, created_by, updated_at, updated_by) VALUES (2, N'USA', N'+44', 2, 1, 'U', 'V', 'W', 'X')
INSERT [dbo].[Country] (country_id, country_name, country_code, province_id, status, created_at, created_by, updated_at, updated_by) VALUES (3, N'Japan', N'+81', 3, 1, 'J', 'K', 'L', 'M')
INSERT [dbo].[Country] (country_id, country_name, country_code, province_id, status, created_at, created_by, updated_at, updated_by) VALUES (4, N'Canada', N'+1', 4, 1, 'E', 'F', 'N', 'O')
INSERT [dbo].[Country] (country_id, country_name, country_code, province_id, status, created_at, created_by, updated_at, updated_by) VALUES (5, N'Germany', N'+49', 5, 1, 'G', 'H', 'I', 'P')

/*Province Datas*/
INSERT [dbo].[Province] (province_id, province_name, province_code, axis_meridian, country_id, status, created_at, created_by, updated_at, updated_by) VALUES (1, N'Dong Nai', N'VN-39', N'102.14 8.33 / 109.53 23.4', 1, 1, 'A', 'B', 'C', 'D')
INSERT [dbo].[Province] (province_id, province_name, province_code, axis_meridian, country_id, status, created_at, created_by, updated_at, updated_by) VALUES (2, N'California', N'CA', N'-172.54 23.81 / -47.74 86.46', 2, 1, 'U', 'V', 'W', 'X')
INSERT [dbo].[Province] (province_id, province_name, province_code, axis_meridian, country_id, status, created_at, created_by, updated_at, updated_by) VALUES (3, N'Tokyo', N'29', N'122.83 20.37 / 154.05 45.54', 3, 1, 'J', 'K', 'L', 'M')
INSERT [dbo].[Province] (province_id, province_name, province_code, axis_meridian, country_id, status, created_at, created_by, updated_at, updated_by) VALUES (4, N'Alberta', N'AB', N'-141.01 38.21 / -40.73 86.46', 4, 1, 'E', 'F', 'N', 'O')
INSERT [dbo].[Province] (province_id, province_name, province_code, axis_meridian, country_id, status, created_at, created_by, updated_at, updated_by) VALUES (5, N'Hamburg', N'HH', N'5.86 47.27 / 15.04 55.09', 5, 1, 'G', 'H', 'I', 'P')

/*District Datas*/
INSERT [dbo].[District] (district_id, district_name, district_code, province_id, status, created_at, created_by, updated_at, updated_by) VALUES (1, N'Bien Hoa', N'4801', 1, 1, 'A', 'B', 'C', 'D')
INSERT [dbo].[District] (district_id, district_name, district_code, province_id, status, created_at, created_by, updated_at, updated_by) VALUES (2, N'Alameda', N'510', 2, 1, 'U', 'V', 'W', 'X')
INSERT [dbo].[District] (district_id, district_name, district_code, province_id, status, created_at, created_by, updated_at, updated_by) VALUES (3, N'Ginza', N'03', 3, 1, 'J', 'K', 'L', 'M')
INSERT [dbo].[District] (district_id, district_name, district_code, province_id, status, created_at, created_by, updated_at, updated_by) VALUES (4, N'Athabasca', N'0011', 4, 1, 'E', 'F', 'N', 'O')
INSERT [dbo].[District] (district_id, district_name, district_code, province_id, status, created_at, created_by, updated_at, updated_by) VALUES (5, N'Harburg', N'040', 5, 1, 'G', 'H', 'I', 'P')

/*Ward Datas*/
INSERT [dbo].[Ward] (ward_id, ward_name, ward_code, district_id, status, created_at, created_by, updated_at, updated_by) VALUES (1, N'Thong Nhat', N'26029', 1, 1, 'A', 'B', 'C', 'D')
INSERT [dbo].[Ward] (ward_id, ward_name, ward_code, district_id, status, created_at, created_by, updated_at, updated_by) VALUES (2, N'Ward 1', N'14301', 2, 1, 'U', 'V', 'W', 'X')
INSERT [dbo].[Ward] (ward_id, ward_name, ward_code, district_id, status, created_at, created_by, updated_at, updated_by) VALUES (3, N'Ginza Torreon 16 ', N'8116', 3, 1, 'J', 'K', 'L', 'M')
INSERT [dbo].[Ward] (ward_id, ward_name, ward_code, district_id, status, created_at, created_by, updated_at, updated_by) VALUES (4, N'Athabasca River', N'15793', 4, 1, 'E', 'F', 'N', 'O')
INSERT [dbo].[Ward] (ward_id, ward_name, ward_code, district_id, status, created_at, created_by, updated_at, updated_by) VALUES (5, N'Neuland', N'2483', 5, 1, 'G', 'H', 'I', 'P')

/*Address Datas*/
INSERT [dbo].[Address] (address_id, address_text, country_id, province_id, district_id, ward_id, town_id, status, latitude, longitude, notes) VALUES (1, N'15B Bien Hoa Dong Nai', 1, 1, 1, 1, 15, 1 ,'10.964112', '106.856461', N'Home')
INSERT [dbo].[Address] (address_id, address_text, country_id, province_id, district_id, ward_id, town_id, status, latitude, longitude, notes) VALUES (2, N'423 Alameda California', 2, 2, 2, 2, 34, 1 ,'36.778259', '-119.417931', N'Friend')
INSERT [dbo].[Address] (address_id, address_text, country_id, province_id, district_id, ward_id, town_id, status, latitude, longitude, notes) VALUES (3, N'63 Ginza Tokyo', 3, 3, 3, 3, 77, 1 ,'35.667', '139.767', N'Fav')
INSERT [dbo].[Address] (address_id, address_text, country_id, province_id, district_id, ward_id, town_id, status, latitude, longitude, notes) VALUES (4, N'346 Athabasca Albertia', 4, 4, 4, 4, 82, 1 ,' 54.71581200', '-113.28178700', N'')
INSERT [dbo].[Address] (address_id, address_text, country_id, province_id, district_id, ward_id, town_id, status, latitude, longitude, notes) VALUES (5, N'161 Harburg Hamburg', 5, 5, 5, 5, 41, 1 ,'53.295228103', '10.015194406', N'')

-- select * from Address
-- select * from Ward
-- select * from District
-- select * from Province
-- select * from Country