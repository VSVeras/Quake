USE [Quake]
GO
/****** Object:  Table [dbo].[DeadPlayer]    Script Date: 19/05/2018 00:07:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DeadPlayer](
	[IdGame] [int] NOT NULL,
	[Id] [int] NOT NULL,
	[Name] [varchar](30) NULL,
	[TotalKills] [decimal](18, 2) NULL,
 CONSTRAINT [PK_DeadPlayer] PRIMARY KEY CLUSTERED 
(
	[IdGame] ASC,
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Game]    Script Date: 19/05/2018 00:07:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Game](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[TotalKills] [decimal](18, 2) NULL,
 CONSTRAINT [PK_Game] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Player]    Script Date: 19/05/2018 00:07:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Player](
	[IdGame] [int] NOT NULL,
	[Id] [int] NOT NULL,
	[Name] [varchar](30) NULL,
 CONSTRAINT [PK_Player] PRIMARY KEY CLUSTERED 
(
	[IdGame] ASC,
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[DeadPlayer] ADD  CONSTRAINT [DF_DeadPlayer_TotalKills]  DEFAULT ((0)) FOR [TotalKills]
GO
ALTER TABLE [dbo].[Game] ADD  CONSTRAINT [DF_Game_TotalKills]  DEFAULT ((0)) FOR [TotalKills]
GO
ALTER TABLE [dbo].[DeadPlayer]  WITH CHECK ADD  CONSTRAINT [FK_DeadPlayer_Game] FOREIGN KEY([IdGame])
REFERENCES [dbo].[Game] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[DeadPlayer] CHECK CONSTRAINT [FK_DeadPlayer_Game]
GO
ALTER TABLE [dbo].[Player]  WITH CHECK ADD  CONSTRAINT [FK_Player_Game] FOREIGN KEY([IdGame])
REFERENCES [dbo].[Game] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Player] CHECK CONSTRAINT [FK_Player_Game]
GO
