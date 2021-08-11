USE [master]
GO
ALTER DATABASE [FactoryData] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [FactoryData].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [FactoryData] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [FactoryData] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [FactoryData] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [FactoryData] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [FactoryData] SET ARITHABORT OFF 
GO
ALTER DATABASE [FactoryData] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [FactoryData] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [FactoryData] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [FactoryData] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [FactoryData] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [FactoryData] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [FactoryData] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [FactoryData] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [FactoryData] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [FactoryData] SET  DISABLE_BROKER 
GO
ALTER DATABASE [FactoryData] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [FactoryData] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [FactoryData] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [FactoryData] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [FactoryData] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [FactoryData] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [FactoryData] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [FactoryData] SET RECOVERY FULL 
GO
ALTER DATABASE [FactoryData] SET  MULTI_USER 
GO
ALTER DATABASE [FactoryData] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [FactoryData] SET DB_CHAINING OFF 
GO
ALTER DATABASE [FactoryData] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [FactoryData] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [FactoryData] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [FactoryData] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'FactoryData', N'ON'
GO
ALTER DATABASE [FactoryData] SET QUERY_STORE = OFF
GO
USE [FactoryData]
GO
/****** Object:  Table [dbo].[Batch]    Script Date: 11-08-2021 06:51:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Batch](
	[Batch_No] [int] NOT NULL,
	[Colour] [varchar](20) NOT NULL,
	[Dyeing_Company_Name] [varchar](50) NOT NULL,
	[Dyeing_In_Date] [date] NULL,
	[Dyeing_Out_Date] [date] NOT NULL,
	[Date_Of_Production] [date] NULL,
	[Tray_ID_Arr] [text] NULL,
	[Net_Weight] [decimal](7, 3) NOT NULL,
	[Quality] [varchar](30) NULL,
	[Company_Name] [varchar](50) NOT NULL,
	[Number_Of_Trays] [int] NOT NULL,
	[Batch_State] [int] NOT NULL,
	[Dyeing_Rate] [decimal](5, 2) NOT NULL,
	[Bill_No] [int] NULL,
	[Fiscal_Year] [varchar](15) NOT NULL,
	[Bill_Date] [date] NULL,
	[Production_Voucher_ID] [int] NULL,
	[Printed] [tinyint] NULL,
	[Slip_No] [varchar](20) NULL,
	[Redyeing] [varchar](30) NULL,
	[Start_Date_Of_Production] [date] NULL,
	[Grade] [varchar](10) NULL,
	[Dyeing_Out_Voucher_ID] [int] NULL,
	[Dyeing_In_Voucher_ID] [int] NULL,
	[Bill_Voucher_ID] [int] NULL,
	[Redyeing_Voucher_ID] [int] NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BillNos_Voucher]    Script Date: 11-08-2021 06:51:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BillNos_Voucher](
	[Voucher_ID] [int] IDENTITY(1,1) NOT NULL,
	[Date_Of_Input] [date] NOT NULL,
	[Batch_No_Arr] [text] NOT NULL,
	[Dyeing_Company_Name] [varchar](50) NOT NULL,
	[Bill_No] [int] NOT NULL,
	[Fiscal_Year] [varchar](15) NOT NULL,
	[Batch_Fiscal_Year] [varchar](15) NOT NULL,
	[Bill_Date] [date] NULL,
	[Deleted] [tinyint] NULL,
PRIMARY KEY CLUSTERED 
(
	[Voucher_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Carton]    Script Date: 11-08-2021 06:51:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Carton](
	[Carton_No] [varchar](20) NOT NULL,
	[Carton_State] [int] NOT NULL,
	[Date_Of_Input] [date] NOT NULL,
	[Date_Of_Billing] [date] NOT NULL,
	[Bill_No] [varchar](30) NULL,
	[Quality] [varchar](30) NULL,
	[Company_Name] [varchar](50) NOT NULL,
	[Net_Weight] [decimal](7, 3) NOT NULL,
	[Buy_Cost] [decimal](7, 3) NOT NULL,
	[Sale_Rate] [decimal](7, 3) NULL,
	[Date_Of_Issue] [date] NULL,
	[Date_Of_Sale] [date] NULL,
	[Fiscal_Year] [varchar](15) NOT NULL,
	[Sale_DO_No] [varchar](20) NULL,
	[Type_Of_Sale] [int] NULL,
	[DO_Fiscal_Year] [varchar](15) NULL,
	[Inward_Voucher_ID] [int] NULL,
	[TS_Voucher_ID] [int] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Carton_Produced]    Script Date: 11-08-2021 06:51:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Carton_Produced](
	[Carton_No] [varchar](20) NOT NULL,
	[Carton_State] [int] NOT NULL,
	[Date_Of_Production] [date] NOT NULL,
	[Quality] [varchar](30) NULL,
	[Colour] [varchar](20) NOT NULL,
	[Batch_No_Arr] [text] NULL,
	[Dyeing_Company_Name] [varchar](50) NULL,
	[Carton_Weight] [decimal](7, 3) NULL,
	[Number_Of_Cones] [int] NULL,
	[Cone_Weight] [decimal](6, 3) NULL,
	[Gross_Weight] [decimal](7, 3) NULL,
	[Net_Weight] [decimal](7, 3) NOT NULL,
	[Sale_Rate] [decimal](7, 3) NULL,
	[Sale_DO_No] [varchar](20) NULL,
	[Date_Of_Sale] [date] NULL,
	[Fiscal_Year] [varchar](15) NOT NULL,
	[Batch_Fiscal_Year_Arr] [text] NULL,
	[Printed] [tinyint] NULL,
	[Grade] [varchar](10) NULL,
	[Company_Name] [varchar](50) NULL,
	[Type_Of_Sale] [int] NULL,
	[DO_Fiscal_Year] [varchar](15) NULL,
	[Production_Voucher_ID] [int] NULL,
	[Sales_Voucher_ID] [int] NULL,
	[Opening_Voucher_ID] [int] NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Carton_Production_Voucher]    Script Date: 11-08-2021 06:51:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Carton_Production_Voucher](
	[Voucher_ID] [int] IDENTITY(1,1) NOT NULL,
	[Date_Of_Input] [date] NOT NULL,
	[Colour] [varchar](20) NOT NULL,
	[Quality] [varchar](30) NULL,
	[Dyeing_Company_Name] [varchar](50) NOT NULL,
	[Batch_No_Arr] [text] NOT NULL,
	[Carton_No_Production_Arr] [text] NULL,
	[Fiscal_Year] [varchar](15) NULL,
	[Net_Batch_Weight] [decimal](8, 3) NOT NULL,
	[Net_Carton_Weight] [decimal](8, 3) NOT NULL,
	[Voucher_Closed] [tinyint] NOT NULL,
	[Oil_Gain] [decimal](5, 2) NULL,
	[Batch_Fiscal_Year_Arr] [text] NOT NULL,
	[Carton_Fiscal_Year] [varchar](15) NULL,
	[Cone_Weight] [decimal](5, 3) NULL,
	[Date_Of_Production] [date] NULL,
	[Deleted] [tinyint] NULL,
	[Printed] [tinyint] NULL,
	[Start_Date_Of_Production] [date] NULL,
PRIMARY KEY CLUSTERED 
(
	[Voucher_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Carton_Voucher]    Script Date: 11-08-2021 06:51:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Carton_Voucher](
	[Voucher_ID] [int] IDENTITY(1,1) NOT NULL,
	[Date_Of_Input] [date] NOT NULL,
	[Date_Of_Billing] [date] NOT NULL,
	[Bill_No] [varchar](30) NULL,
	[Quality] [text] NOT NULL,
	[Quality_Arr] [text] NOT NULL,
	[Company_Name] [varchar](50) NOT NULL,
	[Number_of_Cartons] [int] NOT NULL,
	[Carton_No_Arr] [text] NOT NULL,
	[Carton_Weight_Arr] [text] NOT NULL,
	[Net_Weight] [decimal](10, 3) NOT NULL,
	[Buy_Cost] [text] NOT NULL,
	[Fiscal_Year] [varchar](15) NOT NULL,
	[Deleted] [tinyint] NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Closing_Stock]    Script Date: 11-08-2021 06:51:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Closing_Stock](
	[Fiscal_Year] [varchar](15) NULL,
	[Grey_Godown] [decimal](15, 3) NOT NULL,
	[Twist_Godown] [decimal](15, 3) NULL,
	[Tray_Godown] [decimal](15, 3) NULL,
	[Batches_In_Dyeing] [decimal](15, 3) NULL,
	[Batches_Awating_Connig] [decimal](15, 3) NULL,
	[Packed_Cartons] [decimal](15, 3) NULL,
	[Is_Set] [tinyint] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Colours]    Script Date: 11-08-2021 06:51:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Colours](
	[Colours] [varchar](20) NOT NULL,
	[Quality] [varchar](30) NULL,
	[Dyeing_Rate] [decimal](5, 2) NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Company_Names]    Script Date: 11-08-2021 06:51:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Company_Names](
	[Company_Names] [varchar](50) NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Cones]    Script Date: 11-08-2021 06:51:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Cones](
	[Cones] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Cones] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Customers]    Script Date: 11-08-2021 06:51:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Customers](
	[Customers] [varchar](50) NOT NULL,
	[GSTIN] [varchar](50) NULL,
	[Customer_Address] [varchar](150) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Defaults]    Script Date: 11-08-2021 06:51:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Defaults](
	[Default_Type] [varchar](50) NOT NULL,
	[Default_Name] [varchar](50) NOT NULL,
	[Default_Value] [varchar](50) NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Dyeing_Company_Names]    Script Date: 11-08-2021 06:51:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Dyeing_Company_Names](
	[Dyeing_Company_Names] [varchar](50) NULL,
	[GSTIN] [varchar](50) NULL,
	[Customer_Address] [varchar](150) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Dyeing_Inward_Voucher]    Script Date: 11-08-2021 06:51:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Dyeing_Inward_Voucher](
	[Voucher_ID] [int] IDENTITY(1,1) NOT NULL,
	[Date_Of_Input] [date] NOT NULL,
	[Inward_Date] [date] NOT NULL,
	[Dyeing_Company_Name] [varchar](50) NULL,
	[Bill_No] [int] NOT NULL,
	[Batch_No_Arr] [text] NOT NULL,
	[Fiscal_Year] [varchar](15) NOT NULL,
	[Batch_Fiscal_Year] [varchar](15) NOT NULL,
	[Bill_Date] [date] NULL,
	[Slip_No_Arr] [text] NULL,
	[Deleted] [tinyint] NULL,
PRIMARY KEY CLUSTERED 
(
	[Voucher_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Dyeing_Issue_Voucher]    Script Date: 11-08-2021 06:51:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Dyeing_Issue_Voucher](
	[Voucher_ID] [int] IDENTITY(1,1) NOT NULL,
	[Date_Of_Input] [date] NOT NULL,
	[Date_Of_Issue] [date] NOT NULL,
	[Quality] [varchar](30) NULL,
	[Company_Name] [varchar](50) NOT NULL,
	[Colour] [varchar](20) NOT NULL,
	[Dyeing_Company_Name] [varchar](50) NOT NULL,
	[Tray_No_Arr] [text] NOT NULL,
	[Number_of_Trays] [int] NOT NULL,
	[Batch_No] [int] NOT NULL,
	[Dyeing_Rate] [decimal](5, 2) NOT NULL,
	[Tray_ID_Arr] [text] NOT NULL,
	[Batch_Fiscal_Year] [varchar](15) NOT NULL,
	[Deleted] [tinyint] NULL,
	[Net_Weight] [decimal](7, 3) NULL,
PRIMARY KEY CLUSTERED 
(
	[Voucher_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Fiscal_Year]    Script Date: 11-08-2021 06:51:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Fiscal_Year](
	[Fiscal_Year] [varchar](15) NOT NULL,
	[Highest_Batch_No] [int] NOT NULL,
	[Highest_Carton_Production_No] [varchar](20) NOT NULL,
	[Highest_1_DO_No] [varchar](10) NOT NULL,
	[Highest_0_DO_No] [varchar](10) NOT NULL,
	[Highest_Repacking_Carton_No] [varchar](20) NOT NULL,
	[Highest_Trading_1_DO_No] [varchar](20) NULL,
	[Highest_Trading_0_DO_No] [varchar](20) NULL,
PRIMARY KEY CLUSTERED 
(
	[Fiscal_Year] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Log]    Script Date: 11-08-2021 06:51:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Log](
	[Username] [varchar](20) NULL,
	[LoginTime] [datetime] NULL,
	[LogoutTime] [datetime] NULL,
	[TimeDuration] [time](7) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Machine_No]    Script Date: 11-08-2021 06:51:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Machine_No](
	[Machine_No_ID] [int] IDENTITY(1,1) NOT NULL,
	[Machine_No] [varchar](20) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Machine_No_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Opening_Stock]    Script Date: 11-08-2021 06:51:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Opening_Stock](
	[Voucher_ID] [int] IDENTITY(1,1) NOT NULL,
	[Date_Of_Input] [date] NOT NULL,
	[Voucher_Name] [varchar](50) NOT NULL,
	[Deleted] [tinyint] NULL,
PRIMARY KEY CLUSTERED 
(
	[Voucher_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Print_Types]    Script Date: 11-08-2021 06:51:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Print_Types](
	[Firm_Name] [varchar](50) NOT NULL,
	[Address] [varchar](100) NOT NULL,
	[GSTIN] [varchar](20) NOT NULL,
	[Phone_Number] [varchar](15) NOT NULL,
	[Email_ID] [varchar](25) NOT NULL,
	[Print_Type_ID] [int] IDENTITY(1,1) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Print_Type_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Quality]    Script Date: 11-08-2021 06:51:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Quality](
	[Quality] [varchar](30) NULL,
	[HSN_No] [varchar](15) NULL,
	[Print_Colour] [varchar](30) NULL,
	[Quality_Before_Twist] [varchar](30) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Quality_Before_Twist]    Script Date: 11-08-2021 06:51:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Quality_Before_Twist](
	[Quality_Before_Twist_ID] [int] IDENTITY(1,1) NOT NULL,
	[Quality_Before_Twist] [varchar](30) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Quality_Before_Twist_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [unq] UNIQUE NONCLUSTERED 
(
	[Quality_Before_Twist] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Redyeing_Voucher]    Script Date: 11-08-2021 06:51:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Redyeing_Voucher](
	[Voucher_ID] [int] IDENTITY(1,1) NOT NULL,
	[Date_Of_Input] [date] NOT NULL,
	[Date_Of_Issue] [date] NOT NULL,
	[Old_Batch_No] [int] NOT NULL,
	[Old_Batch_Fiscal_Year] [varchar](15) NULL,
	[Non_Redyeing_Batch_No] [int] NOT NULL,
	[Redyeing_Batch_No] [int] NOT NULL,
	[Redyeing_Batch_Fiscal_Year] [varchar](15) NULL,
	[Deleted] [tinyint] NULL,
PRIMARY KEY CLUSTERED 
(
	[Voucher_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Sales_Voucher]    Script Date: 11-08-2021 06:51:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Sales_Voucher](
	[Voucher_ID] [int] IDENTITY(1,1) NOT NULL,
	[Date_Of_Input] [date] NOT NULL,
	[Date_Of_Sale] [date] NOT NULL,
	[Quality] [varchar](30) NULL,
	[Company_Name] [varchar](50) NOT NULL,
	[Customer] [varchar](50) NOT NULL,
	[Sale_Rate] [decimal](7, 2) NOT NULL,
	[Fiscal_Year] [varchar](15) NOT NULL,
	[Type_Of_Sale] [int] NULL,
	[Tablename] [varchar](20) NULL,
	[Sale_DO_No] [varchar](20) NULL,
	[Net_Weight] [decimal](7, 3) NULL,
	[Sale_Bill_Date] [date] NULL,
	[Sale_Bill_No] [varchar](20) NULL,
	[Printed] [tinyint] NULL,
	[Deleted] [tinyint] NULL,
PRIMARY KEY CLUSTERED 
(
	[Voucher_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SalesBillNos_Voucher]    Script Date: 11-08-2021 06:51:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SalesBillNos_Voucher](
	[Date_Of_Input] [date] NOT NULL,
	[Sale_Bill_Date] [date] NOT NULL,
	[DO_No_Arr] [text] NOT NULL,
	[Quality] [varchar](30) NULL,
	[DO_Fiscal_Year] [varchar](15) NOT NULL,
	[Fiscal_Year] [varchar](15) NOT NULL,
	[Type_Of_Sale] [int] NOT NULL,
	[Sale_Bill_No] [varchar](20) NOT NULL,
	[Sale_Bill_Weight] [decimal](10, 3) NOT NULL,
	[Sale_Bill_Amount] [decimal](11, 2) NOT NULL,
	[Sale_Bill_Weight_Calc] [decimal](10, 3) NOT NULL,
	[Sale_Bill_Amount_Calc] [decimal](11, 2) NOT NULL,
	[Tablename] [varchar](20) NOT NULL,
	[Voucher_ID] [int] IDENTITY(1,1) NOT NULL,
	[Deleted] [tinyint] NULL,
	[Bill_Customer] [varchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[Voucher_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Spring]    Script Date: 11-08-2021 06:51:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Spring](
	[Spring] [varchar](20) NOT NULL,
	[Spring_Weight] [decimal](4, 3) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Spring] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[T_Carton_Inward_Voucher]    Script Date: 11-08-2021 06:51:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[T_Carton_Inward_Voucher](
	[Voucher_ID] [int] IDENTITY(1,1) NOT NULL,
	[Date_Of_Input] [date] NOT NULL,
	[Type] [tinyint] NOT NULL,
	[Date_Of_Billing] [date] NOT NULL,
	[Bill_No] [varchar](30) NULL,
	[Company_ID] [int] NOT NULL,
	[Fiscal_Year] [varchar](15) NOT NULL,
	[Deleted] [tinyint] NULL,
	[Narration] [text] NULL,
PRIMARY KEY CLUSTERED 
(
	[Voucher_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[T_Inward_Carton]    Script Date: 11-08-2021 06:51:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[T_Inward_Carton](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Carton_ID]  AS ('IN'+right('00000000'+CONVERT([varchar](7),[id]),(7))),
	[Carton_No] [varchar](20) NOT NULL,
	[Carton_State] [int] NOT NULL,
	[Quality_ID] [int] NOT NULL,
	[Colour_ID] [int] NOT NULL,
	[Net_Weight] [decimal](7, 3) NOT NULL,
	[Buy_Cost] [decimal](7, 3) NOT NULL,
	[Fiscal_Year] [varchar](15) NOT NULL,
	[Inward_Voucher_ID] [int] NULL,
	[Sale_Voucher_ID] [int] NULL,
	[Job_Voucher_ID] [int] NULL,
	[Repacking_Voucher_ID] [int] NULL,
	[Deleted] [tinyint] NULL,
	[Comments] [text] NULL,
	[Inward_Type] [tinyint] NOT NULL,
	[Grade] [varchar](10) NULL,
	[Company_ID] [int] NOT NULL,
	[Repacking_Display_Order] [int] NULL,
	[Sale_Comments] [text] NULL,
	[Sale_Display_Order] [int] NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET ARITHABORT ON
SET CONCAT_NULL_YIELDS_NULL ON
SET QUOTED_IDENTIFIER ON
SET ANSI_NULLS ON
SET ANSI_PADDING ON
SET ANSI_WARNINGS ON
SET NUMERIC_ROUNDABORT OFF
GO
/****** Object:  Index [ClusteredIndex-20201218-173651]    Script Date: 11-08-2021 06:51:36 ******/
CREATE UNIQUE CLUSTERED INDEX [ClusteredIndex-20201218-173651] ON [dbo].[T_Inward_Carton]
(
	[Carton_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[T_M_Colours]    Script Date: 11-08-2021 06:51:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[T_M_Colours](
	[Colour_ID] [int] IDENTITY(1,1) NOT NULL,
	[Colour] [varchar](30) NULL,
PRIMARY KEY CLUSTERED 
(
	[Colour_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[T_M_Company_Names]    Script Date: 11-08-2021 06:51:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[T_M_Company_Names](
	[Company_ID] [int] IDENTITY(1,1) NOT NULL,
	[Company_Name] [varchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Company_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[T_M_Cones]    Script Date: 11-08-2021 06:51:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[T_M_Cones](
	[Cone_ID] [int] IDENTITY(1,1) NOT NULL,
	[Cone_Name] [varchar](20) NOT NULL,
	[Cone_Weight] [decimal](7, 3) NOT NULL,
 CONSTRAINT [PK__T_M_Cone__2B4240265839C10D] PRIMARY KEY CLUSTERED 
(
	[Cone_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[Cone_Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[T_M_Customers]    Script Date: 11-08-2021 06:51:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[T_M_Customers](
	[Customer_ID] [int] IDENTITY(1,1) NOT NULL,
	[Customer_Name] [varchar](50) NOT NULL,
	[GSTIN] [varchar](50) NULL,
	[Customer_Address] [varchar](150) NULL,
PRIMARY KEY CLUSTERED 
(
	[Customer_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[T_M_Quality_Before_Job]    Script Date: 11-08-2021 06:51:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[T_M_Quality_Before_Job](
	[Quality_Before_Job_ID] [int] IDENTITY(1,1) NOT NULL,
	[Quality_Before_Job] [varchar](30) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Quality_Before_Job_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [unqi] UNIQUE NONCLUSTERED 
(
	[Quality_Before_Job] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[T_Repacked_Cartons]    Script Date: 11-08-2021 06:51:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[T_Repacked_Cartons](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Carton_ID]  AS ('RP'+right('00000000'+CONVERT([varchar](7),[id]),(7))),
	[Carton_No] [varchar](20) NOT NULL,
	[Carton_State] [int] NOT NULL,
	[Date_Of_Production] [date] NOT NULL,
	[Quality_ID] [int] NOT NULL,
	[Colour_ID] [int] NOT NULL,
	[Company_ID] [int] NOT NULL,
	[Carton_Weight] [decimal](7, 3) NOT NULL,
	[Number_Of_Cones] [int] NOT NULL,
	[Gross_Weight] [decimal](7, 3) NOT NULL,
	[Net_Weight] [decimal](7, 3) NOT NULL,
	[Fiscal_Year] [varchar](15) NOT NULL,
	[Printed] [tinyint] NULL,
	[Grade] [varchar](10) NOT NULL,
	[Repacking_Voucher_ID] [int] NULL,
	[Sale_Voucher_ID] [int] NULL,
	[Inward_Type] [tinyint] NOT NULL,
	[Repack_Comments] [text] NULL,
	[Sale_Comments] [text] NULL,
	[Sale_Display_Order] [int] NULL,
PRIMARY KEY NONCLUSTERED 
(
	[Carton_No] ASC,
	[Fiscal_Year] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET ARITHABORT ON
SET CONCAT_NULL_YIELDS_NULL ON
SET QUOTED_IDENTIFIER ON
SET ANSI_NULLS ON
SET ANSI_PADDING ON
SET ANSI_WARNINGS ON
SET NUMERIC_ROUNDABORT OFF
GO
/****** Object:  Index [ClusteredIndex-20201218-180930]    Script Date: 11-08-2021 06:51:36 ******/
CREATE UNIQUE CLUSTERED INDEX [ClusteredIndex-20201218-180930] ON [dbo].[T_Repacked_Cartons]
(
	[Carton_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[T_Repacking_Voucher]    Script Date: 11-08-2021 06:51:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[T_Repacking_Voucher](
	[Voucher_ID] [int] IDENTITY(1,1) NOT NULL,
	[Date_Of_Input] [date] NOT NULL,
	[Colour_ID] [int] NOT NULL,
	[Quality_ID] [int] NOT NULL,
	[Company_ID] [int] NOT NULL,
	[Inward_Cartons_Type] [int] NOT NULL,
	[Voucher_Closed] [tinyint] NOT NULL,
	[Oil_Gain] [decimal](5, 2) NULL,
	[Carton_Fiscal_Year] [varchar](15) NULL,
	[Cone_ID] [int] NULL,
	[Date_Of_Production] [date] NULL,
	[Deleted] [tinyint] NULL,
	[Start_Date_Of_Production] [date] NULL,
	[Narration] [text] NULL,
	[Fiscal_Year] [varchar](10) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Voucher_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[T_Sales_Voucher]    Script Date: 11-08-2021 06:51:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[T_Sales_Voucher](
	[Voucher_ID] [int] IDENTITY(1,1) NOT NULL,
	[Date_Of_Input] [date] NOT NULL,
	[Date_Of_Sale] [date] NOT NULL,
	[Quality_ID] [int] NOT NULL,
	[Company_ID] [int] NOT NULL,
	[Customer_ID] [int] NOT NULL,
	[Sale_Rate] [decimal](7, 2) NOT NULL,
	[Fiscal_Year] [varchar](15) NOT NULL,
	[Type_Of_Sale] [int] NOT NULL,
	[Sale_DO_No] [varchar](20) NOT NULL,
	[Net_Weight] [decimal](7, 3) NOT NULL,
	[Sale_Bill_Date] [date] NULL,
	[Sale_Bill_No] [varchar](20) NULL,
	[Printed] [tinyint] NULL,
	[Deleted] [tinyint] NULL,
	[SalesBillNos_Voucher_ID] [int] NULL,
	[SalesBillNos_Display_Order] [int] NULL,
	[Narration] [text] NULL,
	[Bill_Comments] [text] NULL,
PRIMARY KEY CLUSTERED 
(
	[Voucher_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[T_SalesBillNos_Voucher]    Script Date: 11-08-2021 06:51:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[T_SalesBillNos_Voucher](
	[Date_Of_Input] [date] NOT NULL,
	[Sale_Bill_Date] [date] NOT NULL,
	[Quality_ID] [varchar](30) NULL,
	[DO_Fiscal_Year] [varchar](15) NOT NULL,
	[Fiscal_Year] [varchar](15) NOT NULL,
	[Type_Of_Sale] [int] NOT NULL,
	[Sale_Bill_No] [varchar](20) NOT NULL,
	[Sale_Bill_Weight] [decimal](10, 3) NOT NULL,
	[Sale_Bill_Amount] [decimal](11, 2) NOT NULL,
	[Sale_Bill_Weight_Calc] [decimal](10, 3) NOT NULL,
	[Sale_Bill_Amount_Calc] [decimal](11, 2) NOT NULL,
	[Voucher_ID] [int] IDENTITY(1,1) NOT NULL,
	[Deleted] [tinyint] NULL,
	[Bill_Customer_ID] [varchar](50) NULL,
	[Narration] [text] NULL,
PRIMARY KEY CLUSTERED 
(
	[Voucher_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tray_Active]    Script Date: 11-08-2021 06:51:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tray_Active](
	[Tray_Production_Date] [date] NOT NULL,
	[Tray_No] [varchar](20) NOT NULL,
	[Spring] [varchar](20) NOT NULL,
	[Number_of_Springs] [int] NOT NULL,
	[Tray_Tare] [decimal](7, 3) NOT NULL,
	[Gross_Weight] [decimal](7, 3) NOT NULL,
	[Quality] [varchar](30) NULL,
	[Company_Name] [varchar](50) NOT NULL,
	[Dyeing_In_Date] [date] NULL,
	[Dyeing_Out_Date] [date] NULL,
	[Tray_State] [int] NOT NULL,
	[Batch_No] [int] NULL,
	[Tray_ID] [int] IDENTITY(1,1) NOT NULL,
	[Net_Weight] [decimal](7, 3) NULL,
	[Dyeing_Company_Name] [varchar](50) NULL,
	[Fiscal_Year] [varchar](15) NOT NULL,
	[Machine_No] [varchar](5) NULL,
	[Quality_Before_Twist] [varchar](30) NULL,
	[Batch_Fiscal_Year] [varchar](15) NULL,
	[Redyeing] [decimal](10, 6) NULL,
	[Grade] [varchar](10) NULL,
	[No_Of_Springs_RD] [int] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tray_History]    Script Date: 11-08-2021 06:51:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tray_History](
	[Tray_Production_Date] [date] NOT NULL,
	[Tray_No] [varchar](20) NOT NULL,
	[Spring] [varchar](20) NOT NULL,
	[Number_of_Springs] [int] NOT NULL,
	[Tray_Tare] [decimal](7, 3) NOT NULL,
	[Gross_Weight] [decimal](7, 3) NOT NULL,
	[Quality] [varchar](30) NULL,
	[Company_Name] [varchar](50) NOT NULL,
	[Dyeing_In_Date] [date] NOT NULL,
	[Dyeing_Out_Date] [date] NOT NULL,
	[Batch_No] [int] NOT NULL,
	[Tray_ID] [int] NOT NULL,
	[Net_Weight] [decimal](7, 3) NULL,
	[Dyeing_Company_Name] [varchar](50) NULL,
	[Fiscal_Year] [varchar](15) NOT NULL,
	[Machine_No] [varchar](5) NULL,
	[Quality_Before_Twist] [varchar](30) NULL,
	[Batch_Fiscal_Year] [varchar](15) NULL,
	[Redyeing] [decimal](10, 6) NULL,
	[Grade] [varchar](10) NULL,
	[No_Of_Springs_RD] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[Tray_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tray_Voucher]    Script Date: 11-08-2021 06:51:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tray_Voucher](
	[Voucher_ID] [int] IDENTITY(1,1) NOT NULL,
	[Tray_ID] [int] NOT NULL,
	[Tray_Production_Date] [date] NOT NULL,
	[Tray_No] [varchar](20) NOT NULL,
	[Spring] [varchar](20) NOT NULL,
	[Number_of_Springs] [int] NOT NULL,
	[Tray_Tare] [decimal](7, 3) NOT NULL,
	[Gross_Weight] [decimal](7, 3) NOT NULL,
	[Quality] [varchar](30) NULL,
	[Company_Name] [varchar](50) NOT NULL,
	[Input_Date] [date] NULL,
	[Net_Weight] [decimal](7, 3) NULL,
	[Fiscal_Year] [varchar](15) NOT NULL,
	[Machine_No] [varchar](5) NULL,
	[Quality_Before_Twist] [varchar](30) NULL,
	[Deleted] [tinyint] NULL,
	[Grade] [varchar](10) NULL,
PRIMARY KEY CLUSTERED 
(
	[Voucher_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Twist_Voucher]    Script Date: 11-08-2021 06:51:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Twist_Voucher](
	[Voucher_ID] [int] IDENTITY(1,1) NOT NULL,
	[Date_Of_Input] [date] NOT NULL,
	[Date_Of_Issue] [date] NOT NULL,
	[Quality] [varchar](30) NULL,
	[Company_Name] [varchar](50) NOT NULL,
	[Carton_No_Arr] [text] NOT NULL,
	[Number_of_Cartons] [int] NOT NULL,
	[Fiscal_Year] [varchar](15) NOT NULL,
	[Carton_Fiscal_Year] [varchar](15) NOT NULL,
	[Deleted] [tinyint] NULL,
	[Net_Weight] [decimal](8, 3) NULL,
PRIMARY KEY CLUSTERED 
(
	[Voucher_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 11-08-2021 06:51:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[SLNO] [int] IDENTITY(1,1) NOT NULL,
	[Username] [varchar](20) NOT NULL,
	[PasswordHash] [binary](40) NOT NULL,
	[AccessLevel] [tinyint] NULL,
PRIMARY KEY CLUSTERED 
(
	[Username] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Index [Bill_Date]    Script Date: 11-08-2021 06:51:36 ******/
CREATE NONCLUSTERED INDEX [Bill_Date] ON [dbo].[BillNos_Voucher]
(
	[Bill_Date] DESC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [Date_Of_Input]    Script Date: 11-08-2021 06:51:36 ******/
CREATE NONCLUSTERED INDEX [Date_Of_Input] ON [dbo].[BillNos_Voucher]
(
	[Date_Of_Input] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [Voucher_ID]    Script Date: 11-08-2021 06:51:36 ******/
CREATE NONCLUSTERED INDEX [Voucher_ID] ON [dbo].[BillNos_Voucher]
(
	[Voucher_ID] DESC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [Voucher_ID]    Script Date: 11-08-2021 06:51:36 ******/
CREATE NONCLUSTERED INDEX [Voucher_ID] ON [dbo].[Carton_Production_Voucher]
(
	[Voucher_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [Date_Of_Input]    Script Date: 11-08-2021 06:51:36 ******/
CREATE NONCLUSTERED INDEX [Date_Of_Input] ON [dbo].[Carton_Voucher]
(
	[Date_Of_Input] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [Voucher_ID]    Script Date: 11-08-2021 06:51:36 ******/
CREATE NONCLUSTERED INDEX [Voucher_ID] ON [dbo].[Carton_Voucher]
(
	[Voucher_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [Bill_Date]    Script Date: 11-08-2021 06:51:36 ******/
CREATE NONCLUSTERED INDEX [Bill_Date] ON [dbo].[Dyeing_Inward_Voucher]
(
	[Bill_Date] DESC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [Date_Of_Input]    Script Date: 11-08-2021 06:51:36 ******/
CREATE NONCLUSTERED INDEX [Date_Of_Input] ON [dbo].[Dyeing_Inward_Voucher]
(
	[Date_Of_Input] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [Voucher_ID]    Script Date: 11-08-2021 06:51:36 ******/
CREATE NONCLUSTERED INDEX [Voucher_ID] ON [dbo].[Dyeing_Inward_Voucher]
(
	[Voucher_ID] DESC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [Date_Of_Input]    Script Date: 11-08-2021 06:51:36 ******/
CREATE NONCLUSTERED INDEX [Date_Of_Input] ON [dbo].[Dyeing_Issue_Voucher]
(
	[Date_Of_Input] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [Voucher_ID]    Script Date: 11-08-2021 06:51:36 ******/
CREATE NONCLUSTERED INDEX [Voucher_ID] ON [dbo].[Dyeing_Issue_Voucher]
(
	[Voucher_ID] DESC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [LoginTime]    Script Date: 11-08-2021 06:51:36 ******/
CREATE NONCLUSTERED INDEX [LoginTime] ON [dbo].[Log]
(
	[LoginTime] DESC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [Voucher_ID]    Script Date: 11-08-2021 06:51:36 ******/
CREATE NONCLUSTERED INDEX [Voucher_ID] ON [dbo].[Redyeing_Voucher]
(
	[Voucher_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [Date_Of_Input]    Script Date: 11-08-2021 06:51:36 ******/
CREATE NONCLUSTERED INDEX [Date_Of_Input] ON [dbo].[Sales_Voucher]
(
	[Date_Of_Input] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [Voucher_ID]    Script Date: 11-08-2021 06:51:36 ******/
CREATE NONCLUSTERED INDEX [Voucher_ID] ON [dbo].[Sales_Voucher]
(
	[Voucher_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [Tray_ID]    Script Date: 11-08-2021 06:51:36 ******/
CREATE NONCLUSTERED INDEX [Tray_ID] ON [dbo].[Tray_Active]
(
	[Tray_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [Tray_No]    Script Date: 11-08-2021 06:51:36 ******/
CREATE NONCLUSTERED INDEX [Tray_No] ON [dbo].[Tray_Active]
(
	[Tray_No] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [Tray_ID]    Script Date: 11-08-2021 06:51:36 ******/
CREATE NONCLUSTERED INDEX [Tray_ID] ON [dbo].[Tray_History]
(
	[Tray_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [Tray_No]    Script Date: 11-08-2021 06:51:36 ******/
CREATE NONCLUSTERED INDEX [Tray_No] ON [dbo].[Tray_History]
(
	[Tray_No] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [Input_Date]    Script Date: 11-08-2021 06:51:36 ******/
CREATE NONCLUSTERED INDEX [Input_Date] ON [dbo].[Tray_Voucher]
(
	[Input_Date] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [Voucher_ID]    Script Date: 11-08-2021 06:51:36 ******/
CREATE NONCLUSTERED INDEX [Voucher_ID] ON [dbo].[Tray_Voucher]
(
	[Voucher_ID] DESC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [Date_Of_Input]    Script Date: 11-08-2021 06:51:36 ******/
CREATE NONCLUSTERED INDEX [Date_Of_Input] ON [dbo].[Twist_Voucher]
(
	[Date_Of_Input] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [Voucher_ID]    Script Date: 11-08-2021 06:51:36 ******/
CREATE NONCLUSTERED INDEX [Voucher_ID] ON [dbo].[Twist_Voucher]
(
	[Voucher_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Fiscal_Year] ADD  DEFAULT ('0') FOR [Highest_Repacking_Carton_No]
GO
ALTER TABLE [dbo].[T_Inward_Carton] ADD  DEFAULT ((1)) FOR [Company_ID]
GO
ALTER TABLE [dbo].[T_Repacking_Voucher] ADD  DEFAULT ('2020-2021') FOR [Fiscal_Year]
GO
ALTER TABLE [dbo].[T_Carton_Inward_Voucher]  WITH CHECK ADD  CONSTRAINT [FK_Company_ID] FOREIGN KEY([Company_ID])
REFERENCES [dbo].[T_M_Company_Names] ([Company_ID])
GO
ALTER TABLE [dbo].[T_Carton_Inward_Voucher] CHECK CONSTRAINT [FK_Company_ID]
GO
ALTER TABLE [dbo].[T_Inward_Carton]  WITH CHECK ADD FOREIGN KEY([Colour_ID])
REFERENCES [dbo].[T_M_Colours] ([Colour_ID])
GO
ALTER TABLE [dbo].[T_Inward_Carton]  WITH CHECK ADD FOREIGN KEY([Inward_Voucher_ID])
REFERENCES [dbo].[T_Carton_Inward_Voucher] ([Voucher_ID])
GO
ALTER TABLE [dbo].[T_Inward_Carton]  WITH CHECK ADD FOREIGN KEY([Quality_ID])
REFERENCES [dbo].[T_M_Quality_Before_Job] ([Quality_Before_Job_ID])
GO
ALTER TABLE [dbo].[T_Repacked_Cartons]  WITH CHECK ADD FOREIGN KEY([Colour_ID])
REFERENCES [dbo].[T_M_Colours] ([Colour_ID])
GO
ALTER TABLE [dbo].[T_Repacked_Cartons]  WITH CHECK ADD FOREIGN KEY([Company_ID])
REFERENCES [dbo].[T_M_Company_Names] ([Company_ID])
GO
ALTER TABLE [dbo].[T_Repacked_Cartons]  WITH CHECK ADD FOREIGN KEY([Quality_ID])
REFERENCES [dbo].[T_M_Quality_Before_Job] ([Quality_Before_Job_ID])
GO
ALTER TABLE [dbo].[T_Repacked_Cartons]  WITH CHECK ADD FOREIGN KEY([Repacking_Voucher_ID])
REFERENCES [dbo].[T_Repacking_Voucher] ([Voucher_ID])
GO
ALTER TABLE [dbo].[T_Repacking_Voucher]  WITH CHECK ADD FOREIGN KEY([Colour_ID])
REFERENCES [dbo].[T_M_Colours] ([Colour_ID])
GO
ALTER TABLE [dbo].[T_Repacking_Voucher]  WITH CHECK ADD FOREIGN KEY([Company_ID])
REFERENCES [dbo].[T_M_Company_Names] ([Company_ID])
GO
ALTER TABLE [dbo].[T_Repacking_Voucher]  WITH CHECK ADD FOREIGN KEY([Quality_ID])
REFERENCES [dbo].[T_M_Quality_Before_Job] ([Quality_Before_Job_ID])
GO
/****** Object:  StoredProcedure [dbo].[SearchInBatch]    Script Date: 11-08-2021 06:51:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--use FactoryData
CREATE PROCEDURE [dbo].[SearchInBatch] @searchText nvarchar(50), @date tinyint
AS
if @date = 1
	begin
    select *
    from
    (
		
		select 
			case when Dyeing_Out_Date like @searchText then 'd1'
				when Dyeing_In_Date like @searchText then 'd2'
				when Start_Date_Of_Production like @searchText then 'd3'
				when Date_Of_Production like @searchText then 'd4'
				when Bill_Date like @searchText then 'd5'
				
				when Dyeing_Out_Date like @searchText + '%' then 'da1'
				when Dyeing_In_Date like @searchText + '%' then 'da2'
				when Start_Date_Of_Production like @searchText + '%' then 'da3'
				when Date_Of_Production like @searchText + '%' then 'da4'
				when Bill_Date like @searchText + '%' then 'da5'

		 end as priority,
         T.*
		from Batch as T
	) as C
	where priority is not null
	order by priority asc
	end

	if @date = 0
	begin
	select *
    from
    (
		select 
			case when Batch_No like @searchText then '1'
			when Colour like @searchText then '2'
			when Quality like @searchText then '3'
			when Net_Weight like @searchText then '4'
			when Dyeing_Company_Name like @searchText then '5'
			when Fiscal_Year like @searchText then '6'
			when Bill_No like @searchText then '7'
			when Slip_No like @searchText then '8'
			when Company_Name like @searchText then '9'
			when Batch_No like @searchText + '%' then 'a1'
			when Colour like @searchText + '%' then 'a2'
			when Quality like @searchText + '%' then 'a3'
			when Net_Weight like @searchText + '%' then 'a4'
			when Dyeing_Company_Name like @searchText + '%' then 'a5'
			when Fiscal_Year like @searchText + '%' then 'a6'
			when Bill_No like @searchText + '%' then 'a7'
			when Slip_No like @searchText + '%' then 'a8'
			when Company_Name like @searchText + '%' then 'a9'

		 end as priority,
         T.*
		from Batch as T
	) as C
	where priority is not null
	order by priority asc
	end


GO
/****** Object:  StoredProcedure [dbo].[SearchInCarton]    Script Date: 11-08-2021 06:51:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--use FactoryData
CREATE PROCEDURE [dbo].[SearchInCarton] @searchText nvarchar(50), @date tinyint
AS
if @date = 1
	begin
    select *
    from
    (
		
		select 
			case when Date_Of_Billing like @searchText then 'd1'
				when Date_Of_Issue like @searchText then 'd2'
				when Date_Of_Sale like @searchText then 'd3'
				when Carton_State like @searchText then 'd4'
				
				when Date_Of_Billing like '%' + @searchText + '%' then 'da1'
				when Date_Of_Issue like '%' + @searchText + '%' then 'da2'
				when Date_Of_Sale like '%' + @searchText + '%' then 'da3'
				when Carton_State like '%' + @searchText + '%' then 'da4'

		 end as priority,
         T.*
		from Carton as T
	) as C
	where priority is not null
	order by priority asc
	end

	if @date = 0
	begin
	select *
    from
    (
		select 
			case when Carton_No like @searchText then '1'
				when Quality like @searchText then '2'
				when Company_Name like @searchText then '3'
				when Net_Weight like @searchText then '4'
				when Fiscal_Year like @searchText then '5'
				when Bill_No like @searchText then '6'
				when Sale_DO_No like @searchText then '7'
				when DO_Fiscal_Year like @searchText then '8'
				when Buy_Cost like @searchText then '9'
				when Sale_Rate like @searchText then '10'
				

				when Carton_No like '%' + @searchText + '%' then 'a1'
				when Quality like '%' + @searchText + '%' then 'a2'
				when Company_Name like '%' + @searchText + '%' then 'a3'
				when Net_Weight like '%' + @searchText + '%' then 'a4'
				when Fiscal_Year like '%' + @searchText + '%' then 'a5'
				when Bill_No like '%' + @searchText + '%' then 'a6'
				when Sale_DO_No like '%' + @searchText + '%' then 'a7'
				when DO_Fiscal_Year like '%' + @searchText + '%' then 'a8'
				when Buy_Cost like '%' + @searchText + '%' then 'a9'
				when Sale_Rate like '%' + @searchText + '%' then 'a10'
				

		 end as priority,
         T.*
		from Carton as T
	) as C
	where priority is not null
	order by priority asc
	end



	
GO
/****** Object:  StoredProcedure [dbo].[SearchInCartonProduced]    Script Date: 11-08-2021 06:51:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--use FactoryData
CREATE PROCEDURE [dbo].[SearchInCartonProduced] @searchText nvarchar(50), @date tinyint
AS
if @date = 1
	begin
    select *
    from
    (
		
		select 
			case when Date_Of_Production like @searchText then 'd1'
				when Date_Of_Sale like @searchText then 'd2'
				when Carton_State like @searchText then 'd3'
				
				when Date_Of_Production like '%' + @searchText + '%' then 'da1'
				when Date_Of_Sale like '%' + @searchText + '%' then 'da2'
				when Carton_State like '%' + @searchText + '%' then 'da3'

		 end as priority,
         T.*
		from Carton_Produced as T
	) as C
	where priority is not null
	order by priority asc
	end

	if @date = 0
	begin
	select *
    from
    (
		select 
			case when Carton_No like @searchText then '1'
				when Quality like @searchText then '2'
				when Colour like @searchText then '3'
				when Batch_No_Arr like @searchText then '4'
				when Batch_Fiscal_Year_Arr like @searchText then '5'
				when Date_Of_Production like @searchText then '6'
				when Net_Weight like @searchText then '7'
				when Dyeing_Company_Name like @searchText then '8'
				when Sale_DO_No like @searchText then '9'
				when DO_Fiscal_Year like @searchText then '10'
				when Sale_Rate like @searchText then '11'
				when Grade like @searchText then '12'
				when Fiscal_Year like @searchText then '13'

				when Carton_No like '%' + @searchText + '%' then 'a1'
				when Quality like '%' + @searchText + '%' then 'a2'
				when Colour like '%' + @searchText + '%' then 'a3'
				when Batch_No_Arr like '%' + @searchText + '%' then 'a4'
				when Batch_Fiscal_Year_Arr like '%' + @searchText + '%' then 'a5'
				when Date_Of_Production like '%' + @searchText + '%' then 'a6'
				when Net_Weight like '%' + @searchText + '%' then 'a7'
				when Dyeing_Company_Name like '%' + @searchText + '%' then 'a8'
				when Sale_DO_No like '%' + @searchText + '%' then 'a9'
				when DO_Fiscal_Year like '%' + @searchText + '%' then 'a10'
				when Sale_Rate like '%' + @searchText + '%' then 'a11'
				when Grade like '%' + @searchText + '%' then 'a12'
				when Fiscal_Year like '%' + @searchText + '%' then 'a13'

		 end as priority,
         T.*
		from Carton_Produced as T
	) as C
	where priority is not null
	order by priority asc
	end


	
GO
/****** Object:  StoredProcedure [dbo].[SearchInTable]    Script Date: 11-08-2021 06:51:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--use FactoryData
CREATE PROCEDURE [dbo].[SearchInTable] @tableName nvarchar(50), @searchText nvarchar(50), @date tinyint
AS
SET NOCOUNT ON;
DECLARE @columnName NVARCHAR(100)
DECLARE @sql NVARCHAR(1000) = 'SELECT * FROM ' + @tableName +' WHERE '

DECLARE columns CURSOR FOR
SELECT sys.columns.name FROM sys.tables
INNER JOIN sys.columns ON sys.columns.object_id = sys.tables.object_id
WHERE sys.tables.name = @tableName

OPEN columns
FETCH NEXT FROM columns
INTO @columnName

WHILE @@FETCH_STATUS = 0

BEGIN
	if (@columnName not like '%voucher%') and (@columnName not like '%fiscal%') and (@columnName not like '%deleted%')
    begin
		if(@date = 1)
		begin
			if @columnName like '%date%' 
			begin SET @sql = @sql + @columnName + ' LIKE ''%' + @searchText + '%'' OR ' end
		end
		else
		begin
			if @columnName not like '%date%' 
			begin SET @sql = @sql + @columnName + ' LIKE ''%' + @searchText + '%'' OR ' end
		end
	end
    FETCH NEXT FROM columns
    INTO @columnName    
END

CLOSE columns;    
DEALLOCATE columns;

SET @sql = LEFT(RTRIM(@sql), LEN(@sql) - 2)
--select @sql
EXEC(@sql)



GO
/****** Object:  StoredProcedure [dbo].[SearchInTray]    Script Date: 11-08-2021 06:51:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[SearchInTray] @searchText nvarchar(50), @date tinyint
AS
begin
SET NOCOUNT ON;
DECLARE @columnName NVARCHAR(100)
DECLARE @cols NVARCHAR(1000) = ''
DECLARE @sql NVARCHAR(4000) = 'SELECT '

DECLARE columns CURSOR FOR
SELECT sys.columns.name FROM sys.tables
INNER JOIN sys.columns ON sys.columns.object_id = sys.tables.object_id
WHERE sys.tables.name = 'Tray_History'

OPEN columns
FETCH NEXT FROM columns
INTO @columnName

WHILE @@FETCH_STATUS = 0

BEGIN
	SET @cols = @cols + @columnName + ','
	FETCH NEXT FROM columns
	INTO @columnName  
END

SET @cols = LEFT(RTRIM(@cols), LEN(@cols) - 1)
CLOSE columns;    
DEALLOCATE columns;
SET @sql = @sql+@cols+', Tray_State into S FROM Tray_Active UNION SELECT '+@cols+', -1 AS Tray_State FROM Tray_History' ;
EXEC(@sql)

if @date = 1
	begin
    select *
    from
    (
		
		select 
			case when Tray_Production_Date like @searchText then 'd1'
				when Dyeing_Out_Date like @searchText then 'd2'
				when Dyeing_In_Date like @searchText then 'd3'
				when Tray_State like @searchText then 'd4'
				
				when Tray_Production_Date like '%' + @searchText + '%' then 'da1'
				when Dyeing_Out_Date like '%' + @searchText + '%' then 'da2'
				when Dyeing_In_Date like '%' + @searchText + '%' then 'da3'
				when Tray_State like '%' + @searchText + '%' then 'da4'

		 end as priority,
         T.*
		from S as T
	) as C
	where priority is not null
	order by priority asc
	end

	if @date = 0
	begin
	select *
    from
    (
		select 
			case when Tray_No like @searchText then '1'
				when Quality like @searchText then '2'
				when Batch_No like @searchText then '3'
				when Batch_Fiscal_Year like @searchText then '4'
				when Spring like @searchText then '5'
				when Number_of_Springs like @searchText then '6'
				when Net_Weight like @searchText then '7'
				when Dyeing_Company_Name like @searchText then '8'
				when Machine_No like @searchText then '9'
				when Grade like @searchText then '10'
				when Fiscal_Year like @searchText then '11'
				
				when Tray_No like '%' + @searchText + '%' then 'a1'
				when Quality like '%' + @searchText + '%' then 'a2'
				when Batch_No like '%' + @searchText + '%' then 'a3'
				when Batch_Fiscal_Year like '%' + @searchText + '%' then 'a4'
				when Spring like '%' + @searchText + '%' then 'a5'
				when Number_of_Springs like '%' + @searchText + '%' then 'a6'
				when Net_Weight like '%' + @searchText + '%' then 'a7'
				when Dyeing_Company_Name like '%' + @searchText + '%' then 'a8'
				when Machine_No like '%' + @searchText + '%' then 'a9'
				when Grade like '%' + @searchText + '%' then 'a10'
				when Fiscal_Year like '%' + @searchText + '%' then 'a11'
				

		 end as priority,
         T.*
		from S as T
	) as C
	where priority is not null
	order by priority asc
	end

drop table S
end



GO
/****** Object:  StoredProcedure [dbo].[test]    Script Date: 11-08-2021 06:51:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[test] @searchText nvarchar(50), @date tinyint
AS

SELECT temp3.*, T_M_Company_Names.Company_Name into #temp
FROM
    (SELECT temp2.*, T_M_Colours.Colour
    FROM
        (SELECT temp1.*, T_M_Quality_Before_Job.Quality_Before_Job
        FROM
            (SELECT T_Carton_Inward_Voucher.*, T_Inward_Carton.Carton_ID, T_Inward_Carton.Carton_No, T_Inward_Carton.Quality_ID, T_Inward_Carton.Colour_ID, T_Inward_Carton.Net_Weight, T_Inward_Carton.Buy_Cost, T_Inward_Carton.Inward_Voucher_ID, T_Inward_Carton.Comments, T_Inward_Carton.Inward_Type, T_Inward_Carton.Grade
            FROM T_Carton_Inward_Voucher
            FULL OUTER JOIN T_Inward_Carton
            ON T_Carton_Inward_Voucher.Voucher_ID = T_Inward_Carton.Inward_Voucher_ID) as temp1
        LEFT OUTER JOIN T_M_Quality_Before_Job
        ON T_M_Quality_Before_Job.Quality_Before_Job_ID = temp1.Quality_ID) as temp2
    LEFT OUTER JOIN T_M_Colours
    ON T_M_Colours.Colour_ID = temp2.Colour_ID) as temp3
LEFT OUTER JOIN T_M_Company_Names
ON T_M_Company_Names.Company_ID = temp3.Company_ID;

select distinct t.[Voucher_ID]
    ,STUFF((SELECT  ', ' + t1.Carton_No
        from #temp t1
        where t.[Voucher_ID] = t1.[Voucher_ID]
        FOR XML PATH(''), TYPE
        ).value('.', 'NVARCHAR(MAX)')
    ,1,2,'') Carton_No_Arr
    ,STUFF((SELECT distinct ', ' + t1.Colour
        from #temp t1
        where t.[Voucher_ID] = t1.[Voucher_ID]
        FOR XML PATH(''), TYPE
        ).value('.', 'NVARCHAR(MAX)')
    ,1,2,'') Colour_Arr
    ,STUFF((SELECT distinct ', ' + t1.Quality_Before_Job
        from #temp t1
        where t.[Voucher_ID] = t1.[Voucher_ID]
        FOR XML PATH(''), TYPE
        ).value('.', 'NVARCHAR(MAX)')
    ,1,2,'') Quality_Before_Job_Arr
    ,STUFF((SELECT distinct ', ' + t1.Grade
        from #temp t1
        where t.[Voucher_ID] = t1.[Voucher_ID]
        FOR XML PATH(''), TYPE
        ).value('.', 'NVARCHAR(MAX)')
    ,1,2,'') Grade_Arr
    ,STUFF((SELECT  ', ' + CONVERT(VARCHAR, t1.Comments)
        from #temp t1
        where t.[Voucher_ID] = t1.[Voucher_ID]
        FOR XML PATH(''), TYPE
        ).value('.', 'NVARCHAR(MAX)')
    ,1,2,'') Comments_Arr
    ,t.Bill_No, t.Date_Of_Billing, t.Company_Name, t.Date_Of_Input, t.Deleted, t.Fiscal_Year, t.Inward_Type, CONVERT(VARCHAR, t.Narration) Narration
,(Select sum(Net_Weight) from ((select Net_Weight from #temp where [Voucher_ID] = t.[Voucher_ID] )) t1) Net_Weight
,(Select sum(Buy_Cost) from ((select Buy_Cost from #temp where [Voucher_ID] = t.[Voucher_ID] )) t1) Buy_Cost
,(Select count(Voucher_ID) from ((select Voucher_ID from #temp where [Voucher_ID] = t.[Voucher_ID] )) t1) Number_Of_Cartons into #tt 
from #temp t order by Voucher_ID DESC;
select * from #tt;
select * from #temp;

SET NOCOUNT ON;
DECLARE @columnName NVARCHAR(100)
DECLARE @sql NVARCHAR(1000) = 'SELECT * FROM #tt WHERE '

DECLARE columns CURSOR FOR
SELECT name
FROM   tempdb.sys.columns
WHERE  object_id = Object_id('tempdb..#tt')

OPEN columns
FETCH NEXT FROM columns
INTO @columnName

SELECT @@FETCH_STATUS

WHILE @@FETCH_STATUS = 0

BEGIN
	if (@columnName not like '%voucher%') and (@columnName not like '%fiscal%') and (@columnName not like '%deleted%')
    begin
		if(@date = 1)
		begin
			if @columnName like '%date%' 
			begin SET @sql = @sql + @columnName + ' LIKE ''%' + @searchText + '%'' OR ' end
		end
		else
		begin
			if @columnName not like '%date%' 
			begin SET @sql = @sql + @columnName + ' LIKE ''%' + @searchText + '%'' OR ' end
		end
	end
    FETCH NEXT FROM columns
    INTO @columnName    
END

CLOSE columns;    
DEALLOCATE columns;

select @sql
SET @sql = LEFT(RTRIM(@sql), LEN(@sql) - 2)
EXEC(@sql)

drop table #temp;
drop table #tt;

GO
USE [master]
GO
ALTER DATABASE [FactoryData] SET  READ_WRITE 
GO
