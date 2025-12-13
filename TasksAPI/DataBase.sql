/** 1. User Types  **/
USE [aspnet]
GO

/****** Object:  Table [dbo].[UserTypes]    Script Date: 10/23/2023 4:02:33 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[UserTypes](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserTypeId] [int] NOT NULL,
	[Description] [nvarchar](max) NOT NULL,
	[CreatedDate] [datetime2](7) NOT NULL,
	[UpdatedDate] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_UserTypes] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [AK_UserTypes_UserTypeId] UNIQUE NONCLUSTERED 
(
	[UserTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO



/** 2. USers **/
USE [aspnet]
GO

/****** Object:  Table [dbo].[Users]    Script Date: 10/23/2023 4:03:18 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Users](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Username] [nvarchar](450) NOT NULL,
	[Email] [nvarchar](450) NOT NULL,
	[FirstName] [nvarchar](max) NOT NULL,
	[LastName] [nvarchar](max) NOT NULL,
	[UserTypeId] [int] NOT NULL,
	[Status] [int] NOT NULL,
	[PasswordSalt] [nvarchar](max) NOT NULL,
	[PasswordHash] [nvarchar](max) NOT NULL,
	[CreatedDate] [datetime2](7) NOT NULL,
	[UpdatedDate] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

ALTER TABLE [dbo].[Users]  WITH CHECK ADD  CONSTRAINT [FK_Users_UserTypes_UserTypeId] FOREIGN KEY([UserTypeId])
REFERENCES [dbo].[UserTypes] ([UserTypeId])
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[Users] CHECK CONSTRAINT [FK_Users_UserTypes_UserTypeId]
GO




/** 3. Location Types **/
USE [aspnet]
GO

/****** Object:  Table [dbo].[LocationEntity]    Script Date: 10/23/2023 4:04:01 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[LocationEntity](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[LocationType] [int] NOT NULL,
	[Description] [nvarchar](max) NULL,
	[CreatedDate] [datetime2](7) NOT NULL,
	[UpdatedDate] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_LocationEntity] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [AK_LocationEntity_LocationType] UNIQUE NONCLUSTERED 
(
	[LocationType] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO



/** 4. Locations **/
USE [aspnet]
GO

/****** Object:  Table [dbo].[LocationTypesInstances]    Script Date: 10/23/2023 4:19:59 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[LocationTypesInstances](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[LocationTypeID] [int] NOT NULL,
	[Adress] [nvarchar](max) NOT NULL,
	[Description] [nvarchar](max) NOT NULL,
	[CreatedDate] [datetime2](7) NOT NULL,
	[UpdatedDate] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_LocationTypesInstances] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

ALTER TABLE [dbo].[LocationTypesInstances]  WITH CHECK ADD  CONSTRAINT [FK_LocationTypesInstances_LocationEntity_LocationTypeID] FOREIGN KEY([LocationTypeID])
REFERENCES [dbo].[LocationEntity] ([LocationType])
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[LocationTypesInstances] CHECK CONSTRAINT [FK_LocationTypesInstances_LocationEntity_LocationTypeID]
GO

/** 5.Location Contrains **/

USE [aspnet]
GO

/****** Object:  Table [dbo].[LocationTypesInstancesTasksEntitiesProcurements]    Script Date: 10/23/2023 4:32:30 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[LocationTypesInstancesTasksEntitiesProcurements](
	[Location] [int] NOT NULL,
	[LocationTypesInstancesId] [int] NOT NULL,
 CONSTRAINT [PK_LocationTypesInstancesTasksEntitiesProcurements] PRIMARY KEY CLUSTERED 
(
	[Location] ASC,
	[LocationTypesInstancesId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[LocationTypesInstancesTasksEntitiesProcurements]  WITH CHECK ADD  CONSTRAINT [FK_LocationTypesInstancesTasksEntitiesProcurements_LocationTypesInstances_LocationTypesInstancesId] FOREIGN KEY([LocationTypesInstancesId])
REFERENCES [dbo].[LocationTypesInstances] ([Id])
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[LocationTypesInstancesTasksEntitiesProcurements] CHECK CONSTRAINT [FK_LocationTypesInstancesTasksEntitiesProcurements_LocationTypesInstances_LocationTypesInstancesId]
GO

ALTER TABLE [dbo].[LocationTypesInstancesTasksEntitiesProcurements]  WITH CHECK ADD  CONSTRAINT [FK_LocationTypesInstancesTasksEntitiesProcurements_TasksEntitiesProcurements_Location] FOREIGN KEY([Location])
REFERENCES [dbo].[TasksEntitiesProcurements] ([Id])
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[LocationTypesInstancesTasksEntitiesProcurements] CHECK CONSTRAINT [FK_LocationTypesInstancesTasksEntitiesProcurements_TasksEntitiesProcurements_Location]
GO




/** 6.Good Types **/

USE [aspnet]
GO

/****** Object:  Table [dbo].[GoodsTypes]    Script Date: 10/23/2023 6:19:24 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[GoodsTypes](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[GoodModelId] [int] NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[Description] [nvarchar](max) NOT NULL,
	[CreatedDate] [datetime2](7) NOT NULL,
	[UpdatedDate] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_GoodsTypes] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [AK_GoodsTypes_GoodModelId] UNIQUE NONCLUSTERED 
(
	[GoodModelId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO






/** 7. Good Instances **/
USE [aspnet]
GO

/****** Object:  Table [dbo].[GoodsTypesInstances]    Script Date: 10/23/2023 4:21:05 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[GoodsTypesInstances](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[GoodModelId] [int] NOT NULL,
	[Price] [decimal](18, 4) NOT NULL,
	[LocationId] [int] NOT NULL,
	[serialNumber] [nvarchar](max) NOT NULL,
	[Status] [int] NOT NULL,
	[CreatedDate] [datetime2](7) NOT NULL,
	[UpdatedDate] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_GoodsTypesInstances] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

ALTER TABLE [dbo].[GoodsTypesInstances]  WITH CHECK ADD  CONSTRAINT [FK_GoodsTypesInstances_GoodsTypes_GoodModelId] FOREIGN KEY([GoodModelId])
REFERENCES [dbo].[GoodsTypes] ([GoodModelId])
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[GoodsTypesInstances] CHECK CONSTRAINT [FK_GoodsTypesInstances_GoodsTypes_GoodModelId]
GO

ALTER TABLE [dbo].[GoodsTypesInstances]  WITH CHECK ADD  CONSTRAINT [FK_GoodsTypesInstances_LocationTypesInstances_LocationId] FOREIGN KEY([LocationId])
REFERENCES [dbo].[LocationTypesInstances] ([Id])
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[GoodsTypesInstances] CHECK CONSTRAINT [FK_GoodsTypesInstances_LocationTypesInstances_LocationId]
GO

/** 8. Accouts **/
USE [aspnet]
GO

/****** Object:  Table [dbo].[Accounts]    Script Date: 10/23/2023 4:21:45 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Accounts](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Username] [nvarchar](max) NOT NULL,
	[Email] [nvarchar](max) NOT NULL,
	[FirstName] [nvarchar](max) NOT NULL,
	[LastName] [nvarchar](max) NOT NULL,
	[UserTypeId] [int] NOT NULL,
	[Status] [int] NOT NULL,
	[PasswordSalt] [nvarchar](max) NOT NULL,
	[PasswordHash] [nvarchar](max) NOT NULL,
	[CreatedDate] [datetime2](7) NOT NULL,
	[UpdatedDate] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_Accounts] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

ALTER TABLE [dbo].[Accounts]  WITH CHECK ADD  CONSTRAINT [FK_Accounts_UserTypes_UserTypeId] FOREIGN KEY([UserTypeId])
REFERENCES [dbo].[UserTypes] ([UserTypeId])
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[Accounts] CHECK CONSTRAINT [FK_Accounts_UserTypes_UserTypeId]
GO


/** 9. AccountsGoodsEntity **/

USE [aspnet]
GO

/****** Object:  Table [dbo].[AccountsGoodsEntity]    Script Date: 10/23/2023 4:22:47 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[AccountsGoodsEntity](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[AccountId] [int] NOT NULL,
	[GoodId] [int] NOT NULL,
	[price] [float] NOT NULL,
	[Status] [int] NOT NULL,
	[CreatedDate] [datetime2](7) NOT NULL,
	[UpdatedDate] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_AccountsGoodsEntity] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[AccountsGoodsEntity]  WITH CHECK ADD  CONSTRAINT [FK_AccountsGoodsEntity_Accounts_AccountId] FOREIGN KEY([AccountId])
REFERENCES [dbo].[Accounts] ([Id])
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[AccountsGoodsEntity] CHECK CONSTRAINT [FK_AccountsGoodsEntity_Accounts_AccountId]
GO


/** 10. CashRegister Entity **/
USE [aspnet]
GO

/****** Object:  Table [dbo].[CashRegisterEntity]    Script Date: 10/23/2023 4:23:05 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[CashRegisterEntity](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[LocationID] [int] NOT NULL,
	[Notes] [nvarchar](max) NULL,
	[CreatedDate] [datetime2](7) NOT NULL,
	[UpdatedDate] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_CashRegisterEntity] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

ALTER TABLE [dbo].[CashRegisterEntity]  WITH CHECK ADD  CONSTRAINT [FK_CashRegisterEntity_LocationTypesInstances_LocationID] FOREIGN KEY([LocationID])
REFERENCES [dbo].[LocationTypesInstances] ([Id])
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[CashRegisterEntity] CHECK CONSTRAINT [FK_CashRegisterEntity_LocationTypesInstances_LocationID]
GO


/** 11. CashRegisterEntitySessions **/
USE [aspnet]
GO

/****** Object:  Table [dbo].[CashRegisterEntitySessions]    Script Date: 10/23/2023 4:23:57 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[CashRegisterEntitySessions](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[SessionStatus] [int] NOT NULL,
	[AssignedClerk] [int] NOT NULL,
	[CashRegisterID] [int] NOT NULL,
	[OpenHour] [datetime2](7) NOT NULL,
	[CloseHour] [datetime2](7) NOT NULL,
	[Notes] [nvarchar](max) NULL,
	[CreatedDate] [datetime2](7) NOT NULL,
	[UpdatedDate] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_CashRegisterEntitySessions] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

ALTER TABLE [dbo].[CashRegisterEntitySessions]  WITH CHECK ADD  CONSTRAINT [FK_CashRegisterEntitySessions_CashRegisterEntity_CashRegisterID] FOREIGN KEY([CashRegisterID])
REFERENCES [dbo].[CashRegisterEntity] ([Id])
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[CashRegisterEntitySessions] CHECK CONSTRAINT [FK_CashRegisterEntitySessions_CashRegisterEntity_CashRegisterID]
GO

ALTER TABLE [dbo].[CashRegisterEntitySessions]  WITH CHECK ADD  CONSTRAINT [FK_CashRegisterEntitySessions_Users_AssignedClerk] FOREIGN KEY([AssignedClerk])
REFERENCES [dbo].[Users] ([Id])
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[CashRegisterEntitySessions] CHECK CONSTRAINT [FK_CashRegisterEntitySessions_Users_AssignedClerk]
GO


/** 12. TasksEntities **/
USE [aspnet]
GO

/****** Object:  Table [dbo].[TasksEntities]    Script Date: 10/23/2023 4:25:47 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[TasksEntities](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[TaskType] [int] NOT NULL,
	[TaskStatus] [int] NOT NULL,
	[userID] [int] NOT NULL,
	[Description] [nvarchar](max) NOT NULL,
	[CreatedDate] [datetime2](7) NOT NULL,
	[UpdatedDate] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_TasksEntities] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

ALTER TABLE [dbo].[TasksEntities]  WITH CHECK ADD  CONSTRAINT [FK_TasksEntities_Users_userID] FOREIGN KEY([userID])
REFERENCES [dbo].[Users] ([Id])
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[TasksEntities] CHECK CONSTRAINT [FK_TasksEntities_Users_userID]
GO



/** 13.TasksEntitiesProcurements **/
USE [aspnet]
GO

/****** Object:  Table [dbo].[TasksEntitiesProcurements]    Script Date: 10/23/2023 4:26:16 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[TasksEntitiesProcurements](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[TaskID] [int] NOT NULL,
	[Location] [int] NOT NULL,
	[GoodTypeID] [int] NOT NULL,
	[Quantity] [int] NOT NULL,
	[RemainingQuantity] [int] NOT NULL,
	[CreatedDate] [datetime2](7) NOT NULL,
	[UpdatedDate] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_TasksEntitiesProcurements] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[TasksEntitiesProcurements]  WITH CHECK ADD  CONSTRAINT [FK_TasksEntitiesProcurements_GoodsTypes_GoodTypeID] FOREIGN KEY([GoodTypeID])
REFERENCES [dbo].[GoodsTypes] ([GoodModelId])
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[TasksEntitiesProcurements] CHECK CONSTRAINT [FK_TasksEntitiesProcurements_GoodsTypes_GoodTypeID]
GO

ALTER TABLE [dbo].[TasksEntitiesProcurements]  WITH CHECK ADD  CONSTRAINT [FK_TasksEntitiesProcurements_TasksEntities_TaskID] FOREIGN KEY([TaskID])
REFERENCES [dbo].[TasksEntities] ([Id])
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[TasksEntitiesProcurements] CHECK CONSTRAINT [FK_TasksEntitiesProcurements_TasksEntities_TaskID]
GO



/** 14.TasksEntitiesTransfer **/ 
USE [aspnet]
GO

/****** Object:  Table [dbo].[TasksEntitiesTransfer]    Script Date: 10/23/2023 4:26:46 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[TasksEntitiesTransfer](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[TaskID] [int] NOT NULL,
	[GoodID] [int] NOT NULL,
	[serialNumber] [nvarchar](max) NOT NULL,
	[FromLocation] [int] NOT NULL,
	[ToLocation] [int] NOT NULL,
	[TaskStatus] [int] NOT NULL,
	[CreatedDate] [datetime2](7) NOT NULL,
	[UpdatedDate] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_TasksEntitiesTransfer] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

ALTER TABLE [dbo].[TasksEntitiesTransfer]  WITH CHECK ADD  CONSTRAINT [FK_TasksEntitiesTransfer_GoodsTypesInstances_GoodID] FOREIGN KEY([GoodID])
REFERENCES [dbo].[GoodsTypesInstances] ([Id])
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[TasksEntitiesTransfer] CHECK CONSTRAINT [FK_TasksEntitiesTransfer_GoodsTypesInstances_GoodID]
GO

ALTER TABLE [dbo].[TasksEntitiesTransfer]  WITH CHECK ADD  CONSTRAINT [FK_TasksEntitiesTransfer_LocationTypesInstances_ToLocation] FOREIGN KEY([ToLocation])
REFERENCES [dbo].[LocationTypesInstances] ([Id])
GO

ALTER TABLE [dbo].[TasksEntitiesTransfer] CHECK CONSTRAINT [FK_TasksEntitiesTransfer_LocationTypesInstances_ToLocation]
GO

ALTER TABLE [dbo].[TasksEntitiesTransfer]  WITH CHECK ADD  CONSTRAINT [FK_TasksEntitiesTransfer_TasksEntities_TaskID] FOREIGN KEY([TaskID])
REFERENCES [dbo].[TasksEntities] ([Id])
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[TasksEntitiesTransfer] CHECK CONSTRAINT [FK_TasksEntitiesTransfer_TasksEntities_TaskID]
GO

/** 15. ItemMovementEntity **/
USE [aspnet]
GO

/****** Object:  Table [dbo].[ItemMovementEntity]    Script Date: 10/23/2023 4:27:54 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[ItemMovementEntity](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[goodId] [int] NOT NULL,
	[FromLocation] [int] NOT NULL,
	[ToLocation] [int] NOT NULL,
	[FromStatus] [int] NOT NULL,
	[ToStatus] [int] NOT NULL,
	[UserId] [int] NOT NULL,
	[CreatedDate] [datetime2](7) NOT NULL,
	[UpdatedDate] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_ItemMovementEntity] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

/** 16. StoreCartsEntity **/
USE [aspnet]
GO

/****** Object:  Table [dbo].[StoreCartsEntity]    Script Date: 10/23/2023 4:28:28 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[StoreCartsEntity](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[clerktId] [int] NOT NULL,
	[storeLocation] [int] NOT NULL,
	[clientId] [int] NOT NULL,
	[SessionID] [int] NOT NULL,
	[Status] [int] NOT NULL,
	[Total] [decimal](18, 4) NOT NULL,
	[Paid] [decimal](18, 4) NOT NULL,
	[Remaining] [decimal](18, 4) NOT NULL,
	[CreatedDate] [datetime2](7) NOT NULL,
	[UpdatedDate] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_StoreCartsEntity] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO



/** 17.StoreCartsEntityDetails **/
USE [aspnet]
GO

/****** Object:  Table [dbo].[StoreCartsEntityDetails]    Script Date: 10/23/2023 4:29:01 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[StoreCartsEntityDetails](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CartId] [int] NOT NULL,
	[OperationType] [int] NOT NULL,
	[GoodId] [int] NOT NULL,
	[Price] [decimal](18, 4) NOT NULL,
	[Notes] [nvarchar](max) NULL,
	[CreatedDate] [datetime2](7) NOT NULL,
	[UpdatedDate] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_StoreCartsEntityDetails] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

ALTER TABLE [dbo].[StoreCartsEntityDetails]  WITH CHECK ADD  CONSTRAINT [FK_StoreCartsEntityDetails_StoreCartsEntity_CartId] FOREIGN KEY([CartId])
REFERENCES [dbo].[StoreCartsEntity] ([Id])
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[StoreCartsEntityDetails] CHECK CONSTRAINT [FK_StoreCartsEntityDetails_StoreCartsEntity_CartId]
GO

/** 18. ReportsEntities **/
USE [aspnet]
GO

/****** Object:  Table [dbo].[ReportsEntities]    Script Date: 10/23/2023 4:30:32 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[ReportsEntities](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Descrption] [nvarchar](max) NOT NULL,
	[ReportType] [int] NOT NULL,
	[ReportMode] [int] NOT NULL,
	[ReportStatus] [int] NOT NULL,
	[Params] [nvarchar](max) NOT NULL,
	[CreatedDate] [datetime2](7) NOT NULL,
	[UpdatedDate] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_ReportsEntities] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

/** 19.ReportsEntitiesResults  **/
USE [aspnet]
GO

/****** Object:  Table [dbo].[ReportsEntitiesResults]    Script Date: 10/23/2023 4:31:19 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[ReportsEntitiesResults](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ReportID] [int] NOT NULL,
	[RunDate] [datetime2](7) NOT NULL,
	[ReportResults] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_ReportsEntitiesResults] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

ALTER TABLE [dbo].[ReportsEntitiesResults]  WITH CHECK ADD  CONSTRAINT [FK_ReportsEntitiesResults_ReportsEntities_ReportID] FOREIGN KEY([ReportID])
REFERENCES [dbo].[ReportsEntities] ([Id])
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[ReportsEntitiesResults] CHECK CONSTRAINT [FK_ReportsEntitiesResults_ReportsEntities_ReportID]
GO

/** 20. Add users Types **/
USE [aspnet]
GO

INSERT INTO [dbo].[UserTypes]
           ([UserTypeId]
           ,[Description]
           ,[CreatedDate]
           ,[UpdatedDate])
     VALUES
           (2
           ,'Client'
		   ,GETUTCDATE()
		   ,GETUTCDATE())
		   ,(3
           ,'Clerk'
		   ,GETUTCDATE()
		   ,GETUTCDATE())
		   ,(4
           ,'Supervisor'
		   ,GETUTCDATE()
		   ,GETUTCDATE())
GO


/** 21. Add locatio Types **/
USE [aspnet]
GO

INSERT INTO [dbo].[LocationEntity]
           ([LocationType]
           ,[Description]
           ,[CreatedDate]
           ,[UpdatedDate])
     VALUES
           (1
           ,'Warehouse'
           ,GETUTCDATE()
           ,GETUTCDATE())
		   ,(2
           ,'STORE'
           ,GETUTCDATE()
           ,GETUTCDATE())
		   ,(3
           ,'CLIENT'
           ,GETUTCDATE()
           ,GETUTCDATE())
		   ,(4
           ,'SUPPLIER'
           ,GETUTCDATE()
           ,GETUTCDATE())
GO


