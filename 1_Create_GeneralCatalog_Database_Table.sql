USE [master]
DECLARE @dbname nvarchar(50) = N'GenearalCatalog'
IF NOT EXISTS (SELECT name FROM master.sys.databases WHERE name = @dbname)
	CREATE DATABASE GeneralCatalog;
GO
	USE [GeneralCatalog]
GO

/****** Object:  Table [dbo].[Bank]    Script Date: 09/16/2024 7:59:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Bank]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Bank](
	[id] [int] NOT NULL,
	[bank_type_id] [int] NOT NULL,
	[name] [nvarchar](100) NOT NULL,
	[name_en] [nvarchar](100) NULL,
	[trade_name] [nvarchar](50) NULL,
	[site_url] [nvarchar](150) NULL,
	[status] [int] NULL,
	[is_default] [int] NULL,
	[description] [nvarchar](250) NULL,
	[timer] [datetime] NOT NULL,
 CONSTRAINT [PK_Bank] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[BankType]    Script Date: 09/16/2024 7:59:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[BankType]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[BankType](
	[id] [int] NOT NULL,
	[name] [nvarchar](200) NOT NULL,
	[status] [int] NULL,
	[created_at] [datetime] NULL,
	[created_by] [int] NULL,
	[updated_at] [datetime] NULL,
	[updated_by] [int] NULL,
	[timer] [datetime] NOT NULL,
 CONSTRAINT [PK_BankType] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[Country]    Script Date: 09/16/2024 7:59:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Country]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Country](
	[id] [int] NOT NULL,
	[name] [nvarchar](50) NOT NULL,
	[name_slug] [nvarchar](100) NULL,
	[country_code] [nvarchar](5) NULL,
	[status] [int] NULL,
	[created_at] [datetime] NULL,
	[created_by] [int] NULL,
	[updated_at] [datetime] NULL,
	[updated_by] [int] NULL,
	[remark] [nvarchar](100) NULL,
	[timer] [datetime] NOT NULL,
 CONSTRAINT [PK_Country] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[District]    Script Date: 09/16/2024 7:59:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[District]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[District](
	[id] [int] NOT NULL,
	[name] [nvarchar](50) NOT NULL,
	[name_slug] [nvarchar](100) NULL,
	[district_code] [nvarchar](5) NULL,
	[province_id] [int] NOT NULL,
	[created_at] [datetime] NOT NULL,
	[created_by] [int] NOT NULL,
	[updated_at] [datetime] NULL,
	[updated_by] [int] NOT NULL,
	[status] [int] NULL,
	[timer] [datetime] NOT NULL,
 CONSTRAINT [pk_District] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[Folk]    Script Date: 09/16/2024 7:59:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Folk]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Folk](
	[id] [int] NOT NULL,
	[name] [nvarchar](50) NULL,
	[name_slug] [nvarchar](100) NULL,
	[description] [nvarchar](250) NULL,
	[created_at] [datetime] NOT NULL,
	[created_by] [int] NOT NULL,
	[updated_at] [datetime] NULL,
	[updated_by] [int] NOT NULL,
	[status] [int] NULL,
	[timer] [datetime] NOT NULL,
 CONSTRAINT [pk_Folk] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[Province]    Script Date: 09/16/2024 7:59:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Province]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Province](
	[id] [int] NOT NULL,
	[name] [nvarchar](50) NOT NULL,
	[name_slug] [nvarchar](100) NULL,
	[province_code] [nvarchar](5) NULL,
	[axis_meridian] [decimal](18, 2) NULL,
	[created_at] [datetime] NOT NULL,
	[created_by] [int] NOT NULL,
	[updated_at] [datetime] NULL,
	[updated_by] [int] NOT NULL,
	[status] [int] NULL,
	[timer] [datetime] NOT NULL,
	[country_id] [int] NOT NULL,
 CONSTRAINT [pk_Province] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[Religion]    Script Date: 09/16/2024 7:59:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Religion]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Religion](
	[id] [int] NOT NULL,
	[name] [nvarchar](50) NOT NULL,
	[name_slug] [nvarchar](100) NULL,
	[description] [nvarchar](150) NULL,
	[created_at] [datetime] NOT NULL,
	[created_by] [int] NOT NULL,
	[updated_at] [datetime] NULL,
	[updated_by] [int] NOT NULL,
	[status] [int] NULL,
	[timer] [datetime] NOT NULL,
 CONSTRAINT [pk_Religion] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[School]    Script Date: 09/16/2024 7:59:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[School]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[School](
	[id] [int] NOT NULL,
	[school_code] [nvarchar](5) NULL,
	[school_level] [int] NULL,
	[country_id] [int] NULL,
	[province_id] [int] NULL,
	[name] [nvarchar](100) NOT NULL,
	[name_en] [nvarchar](100) NULL,
	[status] [int] NULL,
	[remark] [nvarchar](150) NULL,
	[created_at] [datetime] NOT NULL,
	[created_by] [int] NOT NULL,
	[updated_at] [datetime] NULL,
	[updated_by] [int] NOT NULL,
	[timer] [datetime] NOT NULL,
 CONSTRAINT [PK_School] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[Ward]    Script Date: 09/16/2024 7:59:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Ward]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Ward](
	[id] [int] NOT NULL,
	[name] [nvarchar](50) NOT NULL,
	[name_slug] [nvarchar](100) NULL,
	[ward_code] [nvarchar](5) NULL,
	[district_id] [int] NOT NULL,
	[created_at] [datetime] NOT NULL,
	[created_by] [int] NOT NULL,
	[updated_at] [datetime] NULL,
	[updated_by] [int] NOT NULL,
	[status] [int] NULL,
	[timer] [datetime] NOT NULL,
 CONSTRAINT [pk_Wards] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Bank_BankType]') AND parent_object_id = OBJECT_ID(N'[dbo].[Bank]'))
ALTER TABLE [dbo].[Bank]  WITH CHECK ADD  CONSTRAINT [FK_Bank_BankType] FOREIGN KEY([bank_type_id])
REFERENCES [dbo].[BankType] ([id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Bank_BankType]') AND parent_object_id = OBJECT_ID(N'[dbo].[Bank]'))
ALTER TABLE [dbo].[Bank] CHECK CONSTRAINT [FK_Bank_BankType]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_District_Province]') AND parent_object_id = OBJECT_ID(N'[dbo].[District]'))
ALTER TABLE [dbo].[District]  WITH CHECK ADD  CONSTRAINT [FK_District_Province] FOREIGN KEY([province_id])
REFERENCES [dbo].[Province] ([id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_District_Province]') AND parent_object_id = OBJECT_ID(N'[dbo].[District]'))
ALTER TABLE [dbo].[District] CHECK CONSTRAINT [FK_District_Province]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Province_Country]') AND parent_object_id = OBJECT_ID(N'[dbo].[Province]'))
ALTER TABLE [dbo].[Province]  WITH CHECK ADD  CONSTRAINT [FK_Province_Country] FOREIGN KEY([country_id])
REFERENCES [dbo].[Country] ([id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Province_Country]') AND parent_object_id = OBJECT_ID(N'[dbo].[Province]'))
ALTER TABLE [dbo].[Province] CHECK CONSTRAINT [FK_Province_Country]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Ward_District]') AND parent_object_id = OBJECT_ID(N'[dbo].[Ward]'))
ALTER TABLE [dbo].[Ward]  WITH CHECK ADD  CONSTRAINT [FK_Ward_District] FOREIGN KEY([district_id])
REFERENCES [dbo].[District] ([id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Ward_District]') AND parent_object_id = OBJECT_ID(N'[dbo].[Ward]'))
ALTER TABLE [dbo].[Ward] CHECK CONSTRAINT [FK_Ward_District]
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'School', N'COLUMN',N'school_level'))
	EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'1: Tiểu học; 2: TH Cơ sở; 3: THPT; 4: Trung cấp; 5: Cao đẳng; 6: Đại học; 7: Thạc sĩ; 8: Tiến sĩ' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'School', @level2type=N'COLUMN',@level2name=N'school_level'
GO
