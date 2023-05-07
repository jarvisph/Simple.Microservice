
GO

/****** Object:  Table [dbo].[auth_Admin]    Script Date: 2023/5/7 17:57:26 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[auth_Admin](
	[AdminID] [int] IDENTITY(1,1) NOT NULL,
	[RoleID] [int] NOT NULL,
	[AdminName] [varchar](32) NOT NULL,
	[NickName] [nvarchar](16) NOT NULL,
	[Face] [varchar](128) NOT NULL,
	[Password] [varchar](64) NOT NULL,
	[Status] [tinyint] NOT NULL,
	[LoginIP] [varchar](64) NOT NULL,
	[LoginAt] [bigint] NOT NULL,
	[CreateAt] [bigint] NOT NULL,
	[IsAdmin] [bit] NOT NULL,
 CONSTRAINT [PK_auth_Admin] PRIMARY KEY CLUSTERED 
(
	[AdminID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'[ID]管理员ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'auth_Admin', @level2type=N'COLUMN',@level2name=N'AdminID'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'角色ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'auth_Admin', @level2type=N'COLUMN',@level2name=N'RoleID'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'管理账号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'auth_Admin', @level2type=N'COLUMN',@level2name=N'AdminName'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'昵称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'auth_Admin', @level2type=N'COLUMN',@level2name=N'NickName'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'头像' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'auth_Admin', @level2type=N'COLUMN',@level2name=N'Face'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'密码' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'auth_Admin', @level2type=N'COLUMN',@level2name=N'Password'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'[Type=UserStatus]状态' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'auth_Admin', @level2type=N'COLUMN',@level2name=N'Status'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'登录IP' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'auth_Admin', @level2type=N'COLUMN',@level2name=N'LoginIP'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'登录时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'auth_Admin', @level2type=N'COLUMN',@level2name=N'LoginAt'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'auth_Admin', @level2type=N'COLUMN',@level2name=N'CreateAt'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否超级管理员' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'auth_Admin', @level2type=N'COLUMN',@level2name=N'IsAdmin'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'[Admin]管理表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'auth_Admin'
GO


