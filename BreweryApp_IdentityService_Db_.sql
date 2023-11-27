USE [master]
GO
CREATE DATABASE [BreweryApp_IdentityService_Db]
GO
CREATE TABLE [T_ServicesConnections](
	[ConnectionId] [int] IDENTITY(1,1) NOT NULL,
	[FromServiceName] [nvarchar](50) NOT NULL,
	[FromServiceId] [uniqueidentifier] NOT NULL,
	[ToServiceName] [nvarchar](50) NOT NULL,
	[ToServiceId] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_T_ServicesConnections] PRIMARY KEY CLUSTERED 
(
	[ConnectionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [T_Sessions](
	[SessionId] [uniqueidentifier] NOT NULL,
	[ConnectionId] [int] NOT NULL,
 CONSTRAINT [PK_T_Sessions] PRIMARY KEY CLUSTERED 
(
	[SessionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [T_ServicesConnections] ON 
GO
INSERT [T_ServicesConnections] ([ConnectionId], [FromServiceName], [FromServiceId], [ToServiceName], [ToServiceId]) VALUES (1, N'TestService', N'63bd5f8e-fe0f-429d-b44b-ebc7bd15e06f', N'GeoCoding', N'aa8f94d1-b550-45ec-82d7-21f76e355452')
GO
SET IDENTITY_INSERT [T_ServicesConnections] OFF
GO
SET ANSI_PADDING ON
GO
ALTER TABLE [T_ServicesConnections] ADD  CONSTRAINT [IX_T_ServicesConnections] UNIQUE NONCLUSTERED 
(
	[FromServiceId] ASC,
	[FromServiceName] ASC,
	[ToServiceId] ASC,
	[ToServiceName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
CREATE NONCLUSTERED INDEX [IX_T_ServicesConnections_ForSelect] ON [T_ServicesConnections]
(
	[FromServiceId] ASC,
	[FromServiceName] ASC,
	[ToServiceId] ASC,
	[ToServiceName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [T_Sessions]  WITH CHECK ADD  CONSTRAINT [FK_T_Sessions_T_ServicesConnections] FOREIGN KEY([ConnectionId])
REFERENCES [T_ServicesConnections] ([ConnectionId])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [T_Sessions] CHECK CONSTRAINT [FK_T_Sessions_T_ServicesConnections]
GO
ALTER TABLE [T_ServicesConnections]  WITH CHECK ADD  CONSTRAINT [CK_T_ServicesConnections] CHECK  ((len(ltrim(rtrim([FromServiceName])))>(0) AND len(ltrim(rtrim([ToServiceName])))>(0)))
GO
ALTER TABLE [T_ServicesConnections] CHECK CONSTRAINT [CK_T_ServicesConnections]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [proc_SessionsInsert]
	@FromServiceName nvarchar(50),
	@FromServiceId uniqueidentifier,
	@ToServiceName nvarchar(50),
	@ToServiceId uniqueidentifier
As
	declare @ConnectionId int = (
		select top(1)
			ConnectionId
		from
			T_ServicesConnections
		where
			FromServiceName = @FromServiceName
			and
			FromServiceId = @FromServiceId
			and
			ToServiceName = @ToServiceName
			and
			ToServiceId = @ToServiceId
		)
	if @ConnectionId is not NULL
	begin
		declare @SessionId uniqueidentifier = NEWID()
		begin try
			insert into T_Sessions
			(
				SessionId,
				ConnectionId
			)
			values
			(
				@SessionId,
				@ConnectionId
			)

			select @SessionId as SessionId
		end try
		begin catch
			select NULL as SessionId
		end catch
	end
	else
	begin
		select NULL as SessionId
	end
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [proc_SessionsSelect]
	@FromServiceId uniqueidentifier,
	@ToServiceId uniqueidentifier,
	@SessionId uniqueidentifier
As
	declare @result bit = 0 

	declare @ConnectionId int = (
		select top(1)
			ConnectionId
		from
			T_ServicesConnections
		where
			FromServiceId = @FromServiceId
			and
			ToServiceId = @ToServiceId
		)

	if @ConnectionId is not NULL
	begin
		if EXISTS(select * from T_Sessions where SessionId = @SessionId and ConnectionId = @ConnectionId)
		begin
			delete from T_Sessions where SessionId = @SessionId
			set @result = 1
		end
	end
	
	select @result as result
GO

