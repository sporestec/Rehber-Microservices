USE [master]
GO
/****** Object:  Database [RehberEmployeeService]    Script Date: 02/06/2019 4:03:06 PM ******/
CREATE DATABASE [RehberEmployeeService]
GO
ALTER DATABASE [RehberEmployeeService] SET COMPATIBILITY_LEVEL = 140
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [RehberEmployeeService].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [RehberEmployeeService] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [RehberEmployeeService] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [RehberEmployeeService] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [RehberEmployeeService] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [RehberEmployeeService] SET ARITHABORT OFF 
GO
ALTER DATABASE [RehberEmployeeService] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [RehberEmployeeService] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [RehberEmployeeService] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [RehberEmployeeService] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [RehberEmployeeService] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [RehberEmployeeService] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [RehberEmployeeService] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [RehberEmployeeService] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [RehberEmployeeService] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [RehberEmployeeService] SET  DISABLE_BROKER 
GO
ALTER DATABASE [RehberEmployeeService] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [RehberEmployeeService] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [RehberEmployeeService] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [RehberEmployeeService] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [RehberEmployeeService] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [RehberEmployeeService] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [RehberEmployeeService] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [RehberEmployeeService] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [RehberEmployeeService] SET  MULTI_USER 
GO
ALTER DATABASE [RehberEmployeeService] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [RehberEmployeeService] SET DB_CHAINING OFF 
GO
ALTER DATABASE [RehberEmployeeService] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [RehberEmployeeService] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [RehberEmployeeService] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [RehberEmployeeService] SET QUERY_STORE = OFF
GO
USE [RehberEmployeeService]
GO
/****** Object:  Table [dbo].[Employees]    Script Date: 02/06/2019 4:03:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Employees](
	[EmployeeId] [int] IDENTITY(1,1) NOT NULL,
	[UnitId] [int] NOT NULL,
	[FirstName] [nvarchar](250) NULL,
	[LastName] [nvarchar](250) NULL,
	[WebSite] [nvarchar](250) NULL,
	[Email] [nvarchar](250) NULL,
	[TelephoneNumber] [nvarchar](250) NULL,
	[ExtraInfo] [nvarchar](250) NULL,
 CONSTRAINT [PK_Employees] PRIMARY KEY CLUSTERED 
(
	[EmployeeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Employees] ON 

INSERT [dbo].[Employees] ([EmployeeId], [UnitId], [FirstName], [LastName], [WebSite], [Email], [TelephoneNumber], [ExtraInfo]) VALUES (1, 1, N'Aras', N'Shaban', N'www.aras.com', N'aras@gmail.com', N'5071678078', N'muhendis')
INSERT [dbo].[Employees] ([EmployeeId], [UnitId], [FirstName], [LastName], [WebSite], [Email], [TelephoneNumber], [ExtraInfo]) VALUES (2, 2, N'Ahmet', N'toma', N'www.ahmet.com', N'ahmet@gmaill.com', N'5076479529', N'Muhendis')
INSERT [dbo].[Employees] ([EmployeeId], [UnitId], [FirstName], [LastName], [WebSite], [Email], [TelephoneNumber], [ExtraInfo]) VALUES (3, 3, N'Tarik', N'Alhasimi', N'www.tarik.com', N'tarik@gmail.com', N'5032476659', N'Muhendis')
INSERT [dbo].[Employees] ([EmployeeId], [UnitId], [FirstName], [LastName], [WebSite], [Email], [TelephoneNumber], [ExtraInfo]) VALUES (4, 3, N'ibrahim', N'shaban', N'www.ibrahim.com', N'ibrahim@hotmail.ocm', N'5013468686', N'doktor')
INSERT [dbo].[Employees] ([EmployeeId], [UnitId], [FirstName], [LastName], [WebSite], [Email], [TelephoneNumber], [ExtraInfo]) VALUES (7, 6, N'şalçö', N'hasan', N'www.hasan.com', N'hasan@gmial.com', N'5115362269', N'muhendis')
INSERT [dbo].[Employees] ([EmployeeId], [UnitId], [FirstName], [LastName], [WebSite], [Email], [TelephoneNumber], [ExtraInfo]) VALUES (11, 2, N'TARİK', N'TEST', N'', N'TAe@geag.com', N'', N'')
INSERT [dbo].[Employees] ([EmployeeId], [UnitId], [FirstName], [LastName], [WebSite], [Email], [TelephoneNumber], [ExtraInfo]) VALUES (12, 1, N'mmerhavb', N'mmerhavb', N'', N'mmerhavb@ge.com', N'', N'')
INSERT [dbo].[Employees] ([EmployeeId], [UnitId], [FirstName], [LastName], [WebSite], [Email], [TelephoneNumber], [ExtraInfo]) VALUES (13, 11, N'mmerhavbmmerhavb', N'mmerhavb', N'', N'', N'', N'')
INSERT [dbo].[Employees] ([EmployeeId], [UnitId], [FirstName], [LastName], [WebSite], [Email], [TelephoneNumber], [ExtraInfo]) VALUES (15, 10, N'sandimandi', N'sandimandi', N'sandimandi', N'sandimandi@fa.com', N'sandimandi', N'')
INSERT [dbo].[Employees] ([EmployeeId], [UnitId], [FirstName], [LastName], [WebSite], [Email], [TelephoneNumber], [ExtraInfo]) VALUES (16, 10, N'sandimandi', N'sandimandi', N'sandimandi', N'sandimandi@dqa.com', N'sandimandi', N'')
INSERT [dbo].[Employees] ([EmployeeId], [UnitId], [FirstName], [LastName], [WebSite], [Email], [TelephoneNumber], [ExtraInfo]) VALUES (18, 11, N'Test Kişi', N'Test Kişi', N'', N'Test Kişi@gmail.com', N'', N'')
INSERT [dbo].[Employees] ([EmployeeId], [UnitId], [FirstName], [LastName], [WebSite], [Email], [TelephoneNumber], [ExtraInfo]) VALUES (19, 1, N'MOHAMED', N'ALHASHME', N'', N'ta745v@gmail.com', N'5442943810', N'')
INSERT [dbo].[Employees] ([EmployeeId], [UnitId], [FirstName], [LastName], [WebSite], [Email], [TelephoneNumber], [ExtraInfo]) VALUES (20, 11, N'ahmet', N'toma', N'taf@fda.com', N'ahmet@gmam.com', N'54411515', N'')
SET IDENTITY_INSERT [dbo].[Employees] OFF
USE [master]
GO
ALTER DATABASE [RehberEmployeeService] SET  READ_WRITE 
GO
