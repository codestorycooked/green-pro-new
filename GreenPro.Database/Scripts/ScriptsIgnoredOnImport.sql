
USE [master]
GO

/****** Object:  Database [greenpro.database]    Script Date: 10/13/2016 00:01:15 ******/
CREATE DATABASE [greenpro.database] ON  PRIMARY 
( NAME = N'greenpro.database', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL10_50.MSSQLSERVER\MSSQL\DATA\greenpro.database.mdf' , SIZE = 102400KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'greenpro.database_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL10_50.MSSQLSERVER\MSSQL\DATA\greenpro.database_log.ldf' , SIZE = 131072KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO

ALTER DATABASE [greenpro.database] SET COMPATIBILITY_LEVEL = 100
GO

IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [greenpro.database].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO

ALTER DATABASE [greenpro.database] SET ANSI_NULL_DEFAULT ON
GO

ALTER DATABASE [greenpro.database] SET ANSI_NULLS ON
GO

ALTER DATABASE [greenpro.database] SET ANSI_PADDING ON
GO

ALTER DATABASE [greenpro.database] SET ANSI_WARNINGS ON
GO

ALTER DATABASE [greenpro.database] SET ARITHABORT ON
GO

ALTER DATABASE [greenpro.database] SET AUTO_CLOSE OFF
GO

ALTER DATABASE [greenpro.database] SET AUTO_CREATE_STATISTICS ON
GO

ALTER DATABASE [greenpro.database] SET AUTO_SHRINK OFF
GO

ALTER DATABASE [greenpro.database] SET AUTO_UPDATE_STATISTICS ON
GO

ALTER DATABASE [greenpro.database] SET CURSOR_CLOSE_ON_COMMIT OFF
GO

ALTER DATABASE [greenpro.database] SET CURSOR_DEFAULT  LOCAL
GO

ALTER DATABASE [greenpro.database] SET CONCAT_NULL_YIELDS_NULL ON
GO

ALTER DATABASE [greenpro.database] SET NUMERIC_ROUNDABORT OFF
GO

ALTER DATABASE [greenpro.database] SET QUOTED_IDENTIFIER ON
GO

ALTER DATABASE [greenpro.database] SET RECURSIVE_TRIGGERS OFF
GO

ALTER DATABASE [greenpro.database] SET  DISABLE_BROKER
GO

ALTER DATABASE [greenpro.database] SET AUTO_UPDATE_STATISTICS_ASYNC OFF
GO

ALTER DATABASE [greenpro.database] SET DATE_CORRELATION_OPTIMIZATION OFF
GO

ALTER DATABASE [greenpro.database] SET TRUSTWORTHY OFF
GO

ALTER DATABASE [greenpro.database] SET ALLOW_SNAPSHOT_ISOLATION OFF
GO

ALTER DATABASE [greenpro.database] SET PARAMETERIZATION SIMPLE
GO

ALTER DATABASE [greenpro.database] SET READ_COMMITTED_SNAPSHOT OFF
GO

ALTER DATABASE [greenpro.database] SET HONOR_BROKER_PRIORITY OFF
GO

ALTER DATABASE [greenpro.database] SET  READ_WRITE
GO

ALTER DATABASE [greenpro.database] SET RECOVERY FULL
GO

ALTER DATABASE [greenpro.database] SET  MULTI_USER
GO

ALTER DATABASE [greenpro.database] SET PAGE_VERIFY NONE
GO

ALTER DATABASE [greenpro.database] SET DB_CHAINING OFF
GO

USE [greenpro.database]
GO

/****** Object:  Table [dbo].[Logs]    Script Date: 10/13/2016 00:01:18 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/****** Object:  Table [dbo].[LogDetailCarSide]    Script Date: 10/13/2016 00:01:18 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/****** Object:  Table [dbo].[LeaderWork]    Script Date: 10/13/2016 00:01:18 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/****** Object:  Table [dbo].[GarageTeam]    Script Date: 10/13/2016 00:01:18 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/****** Object:  Table [dbo].[Services]    Script Date: 10/13/2016 00:01:18 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/****** Object:  Table [dbo].[Packages]    Script Date: 10/13/2016 00:01:18 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/****** Object:  Table [dbo].[AutomaticPayments]    Script Date: 10/13/2016 00:01:18 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/****** Object:  Table [dbo].[AspNetRoles]    Script Date: 10/13/2016 00:01:18 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/****** Object:  Table [dbo].[Garage_LeaderSetting]    Script Date: 10/13/2016 00:01:18 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/****** Object:  Table [dbo].[Garage_CarDaySetting]    Script Date: 10/13/2016 00:01:18 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/****** Object:  Table [dbo].[Weekdays]    Script Date: 10/13/2016 00:01:18 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/****** Object:  UserDefinedFunction [dbo].[Splitstring_to_table]    Script Date: 10/13/2016 00:01:19 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/****** Object:  Table [dbo].[CarTypes]    Script Date: 10/13/2016 00:01:19 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/****** Object:  Table [dbo].[Tax]    Script Date: 10/13/2016 00:01:19 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/****** Object:  Table [dbo].[States]    Script Date: 10/13/2016 00:01:19 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/****** Object:  Table [dbo].[StateProvince]    Script Date: 10/13/2016 00:01:19 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/****** Object:  StoredProcedure [dbo].[Sproc_InsertOrUpdateLeaderSetting]    Script Date: 10/13/2016 00:01:21 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/****** Object:  StoredProcedure [dbo].[Sproc_InsertOrUpdateGarage_CarDaySetting]    Script Date: 10/13/2016 00:01:21 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/****** Object:  Table [dbo].[Cities]    Script Date: 10/13/2016 00:01:21 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/****** Object:  Table [dbo].[Package_Services]    Script Date: 10/13/2016 00:01:21 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/****** Object:  Table [dbo].[Garages]    Script Date: 10/13/2016 00:01:21 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/****** Object:  Table [dbo].[AspNetUsers]    Script Date: 10/13/2016 00:01:21 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/****** Object:  Table [dbo].[WorkerGarages]    Script Date: 10/13/2016 00:01:21 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/****** Object:  Table [dbo].[WorkDone]    Script Date: 10/13/2016 00:01:21 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/****** Object:  StoredProcedure [dbo].[GetAllAvailableGaragesCitiesList]    Script Date: 10/13/2016 00:01:21 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/****** Object:  Table [dbo].[GarrageWeekday]    Script Date: 10/13/2016 00:01:21 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/****** Object:  Table [dbo].[GargesTimeingSlots]    Script Date: 10/13/2016 00:01:21 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/****** Object:  Table [dbo].[AspNetUserRoles]    Script Date: 10/13/2016 00:01:21 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/****** Object:  Table [dbo].[AspNetUserLogins]    Script Date: 10/13/2016 00:01:21 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/****** Object:  Table [dbo].[AspNetUserClaims]    Script Date: 10/13/2016 00:01:21 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/****** Object:  Table [dbo].[CarUsers]    Script Date: 10/13/2016 00:01:21 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/****** Object:  Table [dbo].[CrewAdminMember]    Script Date: 10/13/2016 00:01:21 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/****** Object:  Table [dbo].[GarageMaxCars]    Script Date: 10/13/2016 00:01:21 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/****** Object:  Table [dbo].[LeaderMember]    Script Date: 10/13/2016 00:01:21 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/****** Object:  Table [dbo].[LeaderGarageDay]    Script Date: 10/13/2016 00:01:21 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/****** Object:  Table [dbo].[LeaderCarJob]    Script Date: 10/13/2016 00:01:21 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/****** Object:  Table [dbo].[WorkLogDetails]    Script Date: 10/13/2016 00:01:21 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/****** Object:  Table [dbo].[CarServiceEntries]    Script Date: 10/13/2016 00:01:21 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/****** Object:  Table [dbo].[UserPackages]    Script Date: 10/13/2016 00:01:21 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/****** Object:  Table [dbo].[UserTransactions]    Script Date: 10/13/2016 00:01:21 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/****** Object:  Table [dbo].[UserPackagesAddons]    Script Date: 10/13/2016 00:01:21 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/****** Object:  Table [dbo].[UnAssignedCars]    Script Date: 10/13/2016 00:01:21 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/****** Object:  StoredProcedure [dbo].[SetNextWashedDate]    Script Date: 10/13/2016 00:01:21 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/****** Object:  Table [dbo].[AdhocUserPackages]    Script Date: 10/13/2016 00:01:21 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/****** Object:  Table [dbo].[PayPalLogs]    Script Date: 10/13/2016 00:01:21 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

SET ANSI_PADDING ON
GO

/****** Object:  Table [dbo].[PaypalAutoPayments]    Script Date: 10/13/2016 00:01:21 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/****** Object:  StoredProcedure [dbo].[GetServicesByCarId]    Script Date: 10/13/2016 00:01:22 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/****** Object:  StoredProcedure [p].[GetGarage_CarDaySettingPaymentDetail]    Script Date: 10/13/2016 00:01:22 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/****** Object:  StoredProcedure [dbo].[GetGarage_CarDaySettingPaymentDetail]    Script Date: 10/13/2016 00:01:22 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/****** Object:  Table [dbo].[AdhocUserPackagesAddons]    Script Date: 10/13/2016 00:01:22 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

USE [master]
GO

/****** Object:  Database [greenpro.database]    Script Date: 10/13/2016 00:01:15 ******/
CREATE DATABASE [greenpro.database] ON  PRIMARY 
( NAME = N'greenpro.database', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL10_50.MSSQLSERVER\MSSQL\DATA\greenpro.database.mdf' , SIZE = 102400KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'greenpro.database_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL10_50.MSSQLSERVER\MSSQL\DATA\greenpro.database_log.ldf' , SIZE = 131072KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO

ALTER DATABASE [greenpro.database] SET COMPATIBILITY_LEVEL = 100
GO

IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [greenpro.database].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO

ALTER DATABASE [greenpro.database] SET ANSI_NULL_DEFAULT ON
GO

ALTER DATABASE [greenpro.database] SET ANSI_NULLS ON
GO

ALTER DATABASE [greenpro.database] SET ANSI_PADDING ON
GO

ALTER DATABASE [greenpro.database] SET ANSI_WARNINGS ON
GO

ALTER DATABASE [greenpro.database] SET ARITHABORT ON
GO

ALTER DATABASE [greenpro.database] SET AUTO_CLOSE OFF
GO

ALTER DATABASE [greenpro.database] SET AUTO_CREATE_STATISTICS ON
GO

ALTER DATABASE [greenpro.database] SET AUTO_SHRINK OFF
GO

ALTER DATABASE [greenpro.database] SET AUTO_UPDATE_STATISTICS ON
GO

ALTER DATABASE [greenpro.database] SET CURSOR_CLOSE_ON_COMMIT OFF
GO

ALTER DATABASE [greenpro.database] SET CURSOR_DEFAULT  LOCAL
GO

ALTER DATABASE [greenpro.database] SET CONCAT_NULL_YIELDS_NULL ON
GO

ALTER DATABASE [greenpro.database] SET NUMERIC_ROUNDABORT OFF
GO

ALTER DATABASE [greenpro.database] SET QUOTED_IDENTIFIER ON
GO

ALTER DATABASE [greenpro.database] SET RECURSIVE_TRIGGERS OFF
GO

ALTER DATABASE [greenpro.database] SET  DISABLE_BROKER
GO

ALTER DATABASE [greenpro.database] SET AUTO_UPDATE_STATISTICS_ASYNC OFF
GO

ALTER DATABASE [greenpro.database] SET DATE_CORRELATION_OPTIMIZATION OFF
GO

ALTER DATABASE [greenpro.database] SET TRUSTWORTHY OFF
GO

ALTER DATABASE [greenpro.database] SET ALLOW_SNAPSHOT_ISOLATION OFF
GO

ALTER DATABASE [greenpro.database] SET PARAMETERIZATION SIMPLE
GO

ALTER DATABASE [greenpro.database] SET READ_COMMITTED_SNAPSHOT OFF
GO

ALTER DATABASE [greenpro.database] SET HONOR_BROKER_PRIORITY OFF
GO

ALTER DATABASE [greenpro.database] SET  READ_WRITE
GO

ALTER DATABASE [greenpro.database] SET RECOVERY FULL
GO

ALTER DATABASE [greenpro.database] SET  MULTI_USER
GO

ALTER DATABASE [greenpro.database] SET PAGE_VERIFY NONE
GO

ALTER DATABASE [greenpro.database] SET DB_CHAINING OFF
GO

USE [greenpro.database]
GO

/****** Object:  Table [dbo].[Logs]    Script Date: 10/13/2016 00:01:18 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/****** Object:  Table [dbo].[LogDetailCarSide]    Script Date: 10/13/2016 00:01:18 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/****** Object:  Table [dbo].[LeaderWork]    Script Date: 10/13/2016 00:01:18 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/****** Object:  Table [dbo].[GarageTeam]    Script Date: 10/13/2016 00:01:18 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/****** Object:  Table [dbo].[Services]    Script Date: 10/13/2016 00:01:18 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/****** Object:  Table [dbo].[Packages]    Script Date: 10/13/2016 00:01:18 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/****** Object:  Table [dbo].[AutomaticPayments]    Script Date: 10/13/2016 00:01:18 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/****** Object:  Table [dbo].[AspNetRoles]    Script Date: 10/13/2016 00:01:18 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/****** Object:  Table [dbo].[Garage_LeaderSetting]    Script Date: 10/13/2016 00:01:18 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/****** Object:  Table [dbo].[Garage_CarDaySetting]    Script Date: 10/13/2016 00:01:18 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/****** Object:  Table [dbo].[Weekdays]    Script Date: 10/13/2016 00:01:18 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/****** Object:  UserDefinedFunction [dbo].[Splitstring_to_table]    Script Date: 10/13/2016 00:01:19 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/****** Object:  Table [dbo].[CarTypes]    Script Date: 10/13/2016 00:01:19 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/****** Object:  Table [dbo].[Tax]    Script Date: 10/13/2016 00:01:19 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/****** Object:  Table [dbo].[States]    Script Date: 10/13/2016 00:01:19 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/****** Object:  Table [dbo].[StateProvince]    Script Date: 10/13/2016 00:01:19 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/****** Object:  StoredProcedure [dbo].[Sproc_InsertOrUpdateLeaderSetting]    Script Date: 10/13/2016 00:01:21 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/****** Object:  StoredProcedure [dbo].[Sproc_InsertOrUpdateGarage_CarDaySetting]    Script Date: 10/13/2016 00:01:21 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/****** Object:  Table [dbo].[Cities]    Script Date: 10/13/2016 00:01:21 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/****** Object:  Table [dbo].[Package_Services]    Script Date: 10/13/2016 00:01:21 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/****** Object:  Table [dbo].[Garages]    Script Date: 10/13/2016 00:01:21 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/****** Object:  Table [dbo].[AspNetUsers]    Script Date: 10/13/2016 00:01:21 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/****** Object:  Table [dbo].[WorkerGarages]    Script Date: 10/13/2016 00:01:21 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/****** Object:  Table [dbo].[WorkDone]    Script Date: 10/13/2016 00:01:21 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/****** Object:  StoredProcedure [dbo].[GetAllAvailableGaragesCitiesList]    Script Date: 10/13/2016 00:01:21 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/****** Object:  Table [dbo].[GarrageWeekday]    Script Date: 10/13/2016 00:01:21 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/****** Object:  Table [dbo].[GargesTimeingSlots]    Script Date: 10/13/2016 00:01:21 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/****** Object:  Table [dbo].[AspNetUserRoles]    Script Date: 10/13/2016 00:01:21 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/****** Object:  Table [dbo].[AspNetUserLogins]    Script Date: 10/13/2016 00:01:21 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/****** Object:  Table [dbo].[AspNetUserClaims]    Script Date: 10/13/2016 00:01:21 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/****** Object:  Table [dbo].[CarUsers]    Script Date: 10/13/2016 00:01:21 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/****** Object:  Table [dbo].[CrewAdminMember]    Script Date: 10/13/2016 00:01:21 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/****** Object:  Table [dbo].[GarageMaxCars]    Script Date: 10/13/2016 00:01:21 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/****** Object:  Table [dbo].[LeaderMember]    Script Date: 10/13/2016 00:01:21 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/****** Object:  Table [dbo].[LeaderGarageDay]    Script Date: 10/13/2016 00:01:21 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/****** Object:  Table [dbo].[LeaderCarJob]    Script Date: 10/13/2016 00:01:21 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/****** Object:  Table [dbo].[WorkLogDetails]    Script Date: 10/13/2016 00:01:21 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/****** Object:  Table [dbo].[CarServiceEntries]    Script Date: 10/13/2016 00:01:21 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/****** Object:  Table [dbo].[UserPackages]    Script Date: 10/13/2016 00:01:21 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/****** Object:  Table [dbo].[UserTransactions]    Script Date: 10/13/2016 00:01:21 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/****** Object:  Table [dbo].[UserPackagesAddons]    Script Date: 10/13/2016 00:01:21 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/****** Object:  Table [dbo].[UnAssignedCars]    Script Date: 10/13/2016 00:01:21 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/****** Object:  StoredProcedure [dbo].[SetNextWashedDate]    Script Date: 10/13/2016 00:01:21 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/****** Object:  Table [dbo].[AdhocUserPackages]    Script Date: 10/13/2016 00:01:21 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/****** Object:  Table [dbo].[PayPalLogs]    Script Date: 10/13/2016 00:01:21 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

SET ANSI_PADDING ON
GO

/****** Object:  Table [dbo].[PaypalAutoPayments]    Script Date: 10/13/2016 00:01:21 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/****** Object:  StoredProcedure [dbo].[GetServicesByCarId]    Script Date: 10/13/2016 00:01:22 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/****** Object:  StoredProcedure [p].[GetGarage_CarDaySettingPaymentDetail]    Script Date: 10/13/2016 00:01:22 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/****** Object:  StoredProcedure [dbo].[GetGarage_CarDaySettingPaymentDetail]    Script Date: 10/13/2016 00:01:22 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/****** Object:  Table [dbo].[AdhocUserPackagesAddons]    Script Date: 10/13/2016 00:01:22 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO
