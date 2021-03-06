
GO
/****** Object:  Table [dbo].[StaffInfo]    Script Date: 7/11/2017 6:24:53 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[StaffInfo]') AND type in (N'U'))
DROP TABLE [dbo].[StaffInfo]
GO
/****** Object:  Table [dbo].[Gender]    Script Date: 7/11/2017 6:24:53 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Gender]') AND type in (N'U'))
DROP TABLE [dbo].[Gender]
GO
/****** Object:  Table [dbo].[Gender]    Script Date: 7/11/2017 6:24:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Gender]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Gender](
	[GenderId] [int] NOT NULL,
	[GenderName] [nchar](50) NOT NULL,
 CONSTRAINT [PK_Gender] PRIMARY KEY CLUSTERED 
(
	[GenderId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[StaffInfo]    Script Date: 7/11/2017 6:24:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[StaffInfo]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[StaffInfo](
	[StaffPin] [nchar](8) NOT NULL,
	[StaffName] [nvarchar](50) NOT NULL,
	[DOB] [date] NULL,
	[IsActive] [bit] NOT NULL,
	[GenderId] [int] NOT NULL,
 CONSTRAINT [PK_StaffInfo] PRIMARY KEY CLUSTERED 
(
	[StaffPin] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
