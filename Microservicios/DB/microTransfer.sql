USE [master]
GO
/****** Object:  Database [microTransfer]    Script Date: 15/5/2023 23:05:34 ******/
CREATE DATABASE [microTransfer]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'microTransfer', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS\MSSQL\DATA\microTransfer.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'microTransfer_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS\MSSQL\DATA\microTransfer_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [microTransfer] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [microTransfer].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [microTransfer] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [microTransfer] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [microTransfer] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [microTransfer] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [microTransfer] SET ARITHABORT OFF 
GO
ALTER DATABASE [microTransfer] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [microTransfer] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [microTransfer] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [microTransfer] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [microTransfer] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [microTransfer] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [microTransfer] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [microTransfer] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [microTransfer] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [microTransfer] SET  DISABLE_BROKER 
GO
ALTER DATABASE [microTransfer] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [microTransfer] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [microTransfer] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [microTransfer] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [microTransfer] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [microTransfer] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [microTransfer] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [microTransfer] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [microTransfer] SET  MULTI_USER 
GO
ALTER DATABASE [microTransfer] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [microTransfer] SET DB_CHAINING OFF 
GO
ALTER DATABASE [microTransfer] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [microTransfer] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [microTransfer] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [microTransfer] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [microTransfer] SET QUERY_STORE = ON
GO
ALTER DATABASE [microTransfer] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [microTransfer]
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 15/5/2023 23:05:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[__EFMigrationsHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Respuestas]    Script Date: 15/5/2023 23:05:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Respuestas](
	[respuestaId] [int] IDENTITY(1,1) NOT NULL,
	[resultado] [int] NOT NULL,
	[cbuOrigen] [nvarchar](max) NOT NULL,
	[cbuDestino] [nvarchar](max) NOT NULL,
	[importe] [decimal](18, 2) NOT NULL,
 CONSTRAINT [PK_Respuestas] PRIMARY KEY CLUSTERED 
(
	[respuestaId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Transferencias]    Script Date: 15/5/2023 23:05:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Transferencias](
	[transferenciaId] [int] IDENTITY(1,1) NOT NULL,
	[cuilOriginante] [nvarchar](max) NOT NULL,
	[cuilDestinatario] [nvarchar](max) NOT NULL,
	[cbuOrigen] [nvarchar](max) NOT NULL,
	[cbuDestino] [nvarchar](max) NOT NULL,
	[importe] [decimal](18, 2) NOT NULL,
	[concepto] [nvarchar](max) NOT NULL,
	[descripcion] [nvarchar](max) NOT NULL,
	[respuestaId] [int] NULL,
 CONSTRAINT [PK_Transferencias] PRIMARY KEY CLUSTERED 
(
	[transferenciaId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Index [IX_Transferencias_respuestaId]    Script Date: 15/5/2023 23:05:34 ******/
CREATE NONCLUSTERED INDEX [IX_Transferencias_respuestaId] ON [dbo].[Transferencias]
(
	[respuestaId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Transferencias]  WITH CHECK ADD  CONSTRAINT [FK_Transferencias_Respuestas_respuestaId] FOREIGN KEY([respuestaId])
REFERENCES [dbo].[Respuestas] ([respuestaId])
GO
ALTER TABLE [dbo].[Transferencias] CHECK CONSTRAINT [FK_Transferencias_Respuestas_respuestaId]
GO
USE [master]
GO
ALTER DATABASE [microTransfer] SET  READ_WRITE 
GO
