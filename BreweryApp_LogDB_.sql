USE [master]
GO
/****** Object:  Database [BreweryApp_LogDB]    Script Date: 10/11/2023 11:33:20 ******/
CREATE DATABASE [BreweryApp_LogDB]
GO
USE [BreweryApp_LogDB]
GO
/****** Object:  Table [T_WLog]    Script Date: 10/11/2023 11:33:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [T_WLog](
	[RecordId] [bigint] IDENTITY(1,1) NOT NULL,
	[RequestId] [uniqueidentifier] NOT NULL,
	[ApplicationName] [nvarchar](300) NOT NULL,
	[StartAction] [datetime] NOT NULL,
	[Url] [nvarchar](350) NOT NULL,
	[Method] [nvarchar](100) NOT NULL,
	[Arguments] [nvarchar](400) NOT NULL,
	[Messages] [nvarchar](max) NOT NULL,
	[Result] [nvarchar](max) NULL,
	[Exception] [nvarchar](max) NULL,
	[EndAction] [datetime] NOT NULL,
 CONSTRAINT [PK_T_WLog] PRIMARY KEY CLUSTERED 
(
	[RecordId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  StoredProcedure [proc_WLog_Insert]    Script Date: 10/11/2023 11:33:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [proc_WLog_Insert]
	@RequestId uniqueidentifier ,
	@ApplicationName nvarchar(300) ,
	@StartAction datetime ,
	@Url nvarchar(350) ,
	@Method nvarchar(100) ,
	@Arguments nvarchar(400) ,
	@Messages nvarchar(max) ,
	@Result nvarchar(max) = NULL,
	@Exception nvarchar(max) = NULL,
	@EndAction datetime 
AS
	insert into T_WLog
	(
		[RequestId],
		[ApplicationName],
		[StartAction],
		[Url],
		[Method],
		[Arguments],
		[Messages],
		[Result],
		[Exception],
		[EndAction]
	)
	values
	(
		@RequestId,
		@ApplicationName,
		@StartAction,
		@Url,
		@Method,
		@Arguments,
		@Messages,
		@Result,
		@Exception,
		@EndAction
	)
GO

