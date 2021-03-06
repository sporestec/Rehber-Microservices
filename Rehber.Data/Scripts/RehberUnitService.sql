USE [master]
GO
/****** Object:  Database [RehberUnitService]    Script Date: 02/06/2019 4:05:39 PM ******/
CREATE DATABASE [RehberUnitService]
GO
ALTER DATABASE [RehberUnitService] SET COMPATIBILITY_LEVEL = 140
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [RehberUnitService].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [RehberUnitService] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [RehberUnitService] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [RehberUnitService] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [RehberUnitService] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [RehberUnitService] SET ARITHABORT OFF 
GO
ALTER DATABASE [RehberUnitService] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [RehberUnitService] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [RehberUnitService] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [RehberUnitService] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [RehberUnitService] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [RehberUnitService] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [RehberUnitService] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [RehberUnitService] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [RehberUnitService] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [RehberUnitService] SET  DISABLE_BROKER 
GO
ALTER DATABASE [RehberUnitService] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [RehberUnitService] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [RehberUnitService] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [RehberUnitService] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [RehberUnitService] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [RehberUnitService] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [RehberUnitService] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [RehberUnitService] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [RehberUnitService] SET  MULTI_USER 
GO
ALTER DATABASE [RehberUnitService] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [RehberUnitService] SET DB_CHAINING OFF 
GO
ALTER DATABASE [RehberUnitService] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [RehberUnitService] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [RehberUnitService] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [RehberUnitService] SET QUERY_STORE = OFF
GO
USE [RehberUnitService]
GO
/****** Object:  Table [dbo].[Units]    Script Date: 02/06/2019 4:05:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Units](
	[UnitId] [int] IDENTITY(1,1) NOT NULL,
	[UnitName] [nvarchar](max) NULL,
	[ParentId] [int] NULL,
 CONSTRAINT [PK_Units] PRIMARY KEY CLUSTERED 
(
	[UnitId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Units] ON 

INSERT [dbo].[Units] ([UnitId], [UnitName], [ParentId]) VALUES (1, N'Sakarya Universitesi', NULL)
INSERT [dbo].[Units] ([UnitId], [UnitName], [ParentId]) VALUES (2, N'Subu Universitesi', NULL)
INSERT [dbo].[Units] ([UnitId], [UnitName], [ParentId]) VALUES (3, N'Sakarya Rektorlugu', 1)
INSERT [dbo].[Units] ([UnitId], [UnitName], [ParentId]) VALUES (4, N'Subu Rektorlugu', 2)
INSERT [dbo].[Units] ([UnitId], [UnitName], [ParentId]) VALUES (5, N'Bilgisayar ve Bilisim Fakultesi', 3)
INSERT [dbo].[Units] ([UnitId], [UnitName], [ParentId]) VALUES (6, N'Bilgisayar Bolumu', 5)
INSERT [dbo].[Units] ([UnitId], [UnitName], [ParentId]) VALUES (10, N'AFET YÖNETİMİ UYGULAMA VE ARAŞTIRMA MERKEZİ', NULL)
INSERT [dbo].[Units] ([UnitId], [UnitName], [ParentId]) VALUES (11, N'Ekonomi Ar-Ge Merkezi', NULL)
INSERT [dbo].[Units] ([UnitId], [UnitName], [ParentId]) VALUES (13, N'İdari Birim', 10)
SET IDENTITY_INSERT [dbo].[Units] OFF
ALTER TABLE [dbo].[Units]  WITH CHECK ADD  CONSTRAINT [FK_Units_Units_ParentId] FOREIGN KEY([ParentId])
REFERENCES [dbo].[Units] ([UnitId])
GO
ALTER TABLE [dbo].[Units] CHECK CONSTRAINT [FK_Units_Units_ParentId]
GO
USE [master]
GO
ALTER DATABASE [RehberUnitService] SET  READ_WRITE 
GO
