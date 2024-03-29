USE [master]
GO

IF DB_ID('final_capstone') IS NOT NULL
BEGIN
	ALTER DATABASE final_capstone SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
	DROP DATABASE final_capstone;
END

/****** Object:  Database [final_capstone]    Script Date: 12/16/2021 4:19:35 PM ******/
CREATE DATABASE [final_capstone]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'final_capstone', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\final_capstone.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'final_capstone_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\final_capstone_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [final_capstone] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [final_capstone].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [final_capstone] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [final_capstone] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [final_capstone] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [final_capstone] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [final_capstone] SET ARITHABORT OFF 
GO
ALTER DATABASE [final_capstone] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [final_capstone] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [final_capstone] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [final_capstone] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [final_capstone] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [final_capstone] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [final_capstone] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [final_capstone] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [final_capstone] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [final_capstone] SET  ENABLE_BROKER 
GO
ALTER DATABASE [final_capstone] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [final_capstone] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [final_capstone] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [final_capstone] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [final_capstone] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [final_capstone] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [final_capstone] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [final_capstone] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [final_capstone] SET  MULTI_USER 
GO
ALTER DATABASE [final_capstone] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [final_capstone] SET DB_CHAINING OFF 
GO
ALTER DATABASE [final_capstone] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [final_capstone] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [final_capstone] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [final_capstone] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [final_capstone] SET QUERY_STORE = OFF
GO
USE [final_capstone]
GO
/****** Object:  Table [dbo].[ad_hoc_notes]    Script Date: 12/16/2021 4:19:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ad_hoc_notes](
	[note_id] [int] IDENTITY(500001,1) NOT NULL,
	[camper_code] [int] NOT NULL,
	[parameter] [nvarchar](1000) NOT NULL,
	[value] [nvarchar](1000) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[note_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[camper_updates]    Script Date: 12/16/2021 4:19:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[camper_updates](
	[request_id] [int] IDENTITY(400001,1) NOT NULL,
	[field_to_be_changed] [nvarchar](50) NULL,
	[camper_code] [int] NOT NULL,
	[action] [nvarchar](20) NOT NULL,
	[new_data] [nvarchar](1000) NOT NULL,
	[old_data] [nvarchar](1000) NULL,
	[requestor] [nvarchar](100) NOT NULL,
	[status] [nvarchar](20) NOT NULL,
	[request_date] [date] NOT NULL,
	[finalize_date] [date] NULL,
PRIMARY KEY CLUSTERED 
(
	[request_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[campers]    Script Date: 12/16/2021 4:19:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[campers](
	[camper_code] [int] IDENTITY(200001,1) NOT NULL,
	[family_id] [int] NOT NULL,
	[first_name] [nvarchar](50) NULL,
	[last_name] [nvarchar](50) NULL,
	[dob] [date] NOT NULL,
	[medications] [nvarchar](200) NULL,
	[allergies] [nvarchar](200) NULL,
	[special_needs] [nvarchar](1000) NULL,
	[registrar] [nvarchar](100) NULL,
	[payment_status] [bit] NULL,
	[active_status] [bit] NULL,
	[date_added] [date] NULL,
PRIMARY KEY CLUSTERED 
(
	[camper_code] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[family]    Script Date: 12/16/2021 4:19:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[family](
	[family_id] [int] IDENTITY(100001,1) NOT NULL,
	[parent_guardian_name] [nvarchar](100) NOT NULL,
	[address] [nvarchar](200) NULL,
	[city] [nvarchar](100) NULL,
	[state] [nvarchar](2) NULL,
	[zip] [nvarchar](10) NULL,
	[phone] [nvarchar](12) NULL,
	[email_address] [nvarchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[family_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[family_updates]    Script Date: 12/16/2021 4:19:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[family_updates](
	[request_id] [int] IDENTITY(300001,1) NOT NULL,
	[field_to_be_changed] [nvarchar](50) NULL,
	[family_id] [int] NOT NULL,
	[action] [nvarchar](20) NOT NULL,
	[new_data] [nvarchar](1000) NOT NULL,
	[old_data] [nvarchar](1000) NULL,
	[requestor] [nvarchar](100) NOT NULL,
	[status] [nvarchar](20) NOT NULL,
	[request_date] [date] NOT NULL,
	[finalize_date] [date] NULL,
PRIMARY KEY CLUSTERED 
(
	[request_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[users]    Script Date: 12/16/2021 4:19:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[users](
	[user_id] [int] IDENTITY(100001,1) NOT NULL,
	[username] [varchar](50) NOT NULL,
	[password_hash] [varchar](200) NOT NULL,
	[salt] [varchar](200) NOT NULL,
	[user_role] [varchar](50) NOT NULL,
 CONSTRAINT [PK_user] PRIMARY KEY CLUSTERED 
(
	[user_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[ad_hoc_notes] ON 

INSERT [dbo].[ad_hoc_notes] ([note_id], [camper_code], [parameter], [value]) VALUES (500001, 200006, N'Members of Team Oscar', N'Caleb, Dan, & Eric')
SET IDENTITY_INSERT [dbo].[ad_hoc_notes] OFF
GO
SET IDENTITY_INSERT [dbo].[camper_updates] ON 

INSERT [dbo].[camper_updates] ([request_id], [field_to_be_changed], [camper_code], [action], [new_data], [old_data], [requestor], [status], [request_date], [finalize_date]) VALUES (400001, N'last_name', 200003, N'Update', N'Bowersmith', N'Bowers', N'user', N'Pending', CAST(N'2021-10-09' AS Date), NULL)
INSERT [dbo].[camper_updates] ([request_id], [field_to_be_changed], [camper_code], [action], [new_data], [old_data], [requestor], [status], [request_date], [finalize_date]) VALUES (400002, N'first_name', 200005, N'Update', N'Erica', N'Eric', N'user', N'Approved', CAST(N'2021-11-09' AS Date), CAST(N'2021-11-10' AS Date))
INSERT [dbo].[camper_updates] ([request_id], [field_to_be_changed], [camper_code], [action], [new_data], [old_data], [requestor], [status], [request_date], [finalize_date]) VALUES (400003, N'payment_status', 200003, N'Update', N'true', N'false', N'user', N'Rejected', CAST(N'2021-11-23' AS Date), CAST(N'2021-11-24' AS Date))
INSERT [dbo].[camper_updates] ([request_id], [field_to_be_changed], [camper_code], [action], [new_data], [old_data], [requestor], [status], [request_date], [finalize_date]) VALUES (400004, N'medications', 200004, N'Update', N'Aspirin', N'Asparan', N'admin', N'Approved', CAST(N'2021-12-16' AS Date), CAST(N'2021-12-16' AS Date))
INSERT [dbo].[camper_updates] ([request_id], [field_to_be_changed], [camper_code], [action], [new_data], [old_data], [requestor], [status], [request_date], [finalize_date]) VALUES (400005, N'active_status', 200005, N'Update', N'true', N'false', N'user', N'Rejected', CAST(N'2021-12-16' AS Date), CAST(N'2021-12-17' AS Date))
INSERT [dbo].[camper_updates] ([request_id], [field_to_be_changed], [camper_code], [action], [new_data], [old_data], [requestor], [status], [request_date], [finalize_date]) VALUES (400006, N'active_status', 200006, N'Update', N'false', N'true', N'user', N'Pending', CAST(N'2021-12-09' AS Date), NULL)
SET IDENTITY_INSERT [dbo].[camper_updates] OFF
GO
SET IDENTITY_INSERT [dbo].[campers] ON 

INSERT [dbo].[campers] ([camper_code], [family_id], [first_name], [last_name], [dob], [medications], [allergies], [special_needs], [registrar], [payment_status], [active_status], [date_added]) VALUES (200001, 100001, N'Alex', N'Andrews', CAST(N'2010-01-01' AS Date), N'', N'', N'', N'user', 0, 1, CAST(N'2021-12-10' AS Date))
INSERT [dbo].[campers] ([camper_code], [family_id], [first_name], [last_name], [dob], [medications], [allergies], [special_needs], [registrar], [payment_status], [active_status], [date_added]) VALUES (200002, 100001, N'Brandy', N'', CAST(N'2009-02-03' AS Date), N'', N'', N'', N'user', 1, 1, CAST(N'2021-12-10' AS Date))
INSERT [dbo].[campers] ([camper_code], [family_id], [first_name], [last_name], [dob], [medications], [allergies], [special_needs], [registrar], [payment_status], [active_status], [date_added]) VALUES (200003, 100002, N'Cathy', N'Bowers', CAST(N'2011-03-03' AS Date), N'', N'', N'', N'user', 0, 1, CAST(N'2021-12-10' AS Date))
INSERT [dbo].[campers] ([camper_code], [family_id], [first_name], [last_name], [dob], [medications], [allergies], [special_needs], [registrar], [payment_status], [active_status], [date_added]) VALUES (200004, 100003, N'David', N'Carter', CAST(N'2010-04-04' AS Date), N'Asprin', N'Bees, Nuts, Grass', N'Likes Bedtime Stories, Nightlight', N'user', 0, 1, CAST(N'2021-12-10' AS Date))
INSERT [dbo].[campers] ([camper_code], [family_id], [first_name], [last_name], [dob], [medications], [allergies], [special_needs], [registrar], [payment_status], [active_status], [date_added]) VALUES (200005, 100003, N'Erica', N'', CAST(N'2009-05-05' AS Date), N'', N'', N'', N'user', 0, 0, CAST(N'2021-12-10' AS Date))
INSERT [dbo].[campers] ([camper_code], [family_id], [first_name], [last_name], [dob], [medications], [allergies], [special_needs], [registrar], [payment_status], [active_status], [date_added]) VALUES (200006, 100004, N'Team Oscar', N'Cohort 15', CAST(N'2020-12-03' AS Date), N'Coffee', N'Bright Light, Early Mornings, Bad Reviews', N'Hugs, Appreciation', N'admin', 1, 1, CAST(N'2021-12-10' AS Date))
SET IDENTITY_INSERT [dbo].[campers] OFF
GO
SET IDENTITY_INSERT [dbo].[family] ON 

INSERT [dbo].[family] ([family_id], [parent_guardian_name], [address], [city], [state], [zip], [phone], [email_address]) VALUES (100001, N'Mary Andrews', N'100 First St.', N'Columbus', N'OH', N'43111', N'614-222-3333', N'mary@mary.com')
INSERT [dbo].[family] ([family_id], [parent_guardian_name], [address], [city], [state], [zip], [phone], [email_address]) VALUES (100002, N'Mike Bowers', N'200 Second Ave.', N'Dublin', N'OH', N'43222', N'614-444-5555', N'mike@mike.com')
INSERT [dbo].[family] ([family_id], [parent_guardian_name], [address], [city], [state], [zip], [phone], [email_address]) VALUES (100003, N'Molly Carter', N'300 Third St.', N'Hilliard', N'OH', N'43333', N'614-666-7777', N'molly@molly.com')
INSERT [dbo].[family] ([family_id], [parent_guardian_name], [address], [city], [state], [zip], [phone], [email_address]) VALUES (100004, N'Cohort 15 Team Oscar', N'100 Wow Way', N'Columbus', N'OH', N'43333', N'614-888-9999', N'ioicaleb@hotmail.com')
SET IDENTITY_INSERT [dbo].[family] OFF
GO
SET IDENTITY_INSERT [dbo].[family_updates] ON 

INSERT [dbo].[family_updates] ([request_id], [field_to_be_changed], [family_id], [action], [new_data], [old_data], [requestor], [status], [request_date], [finalize_date]) VALUES (300001, N'address', 100002, N'Update', N'200 Secondary Road', N'200 Second Ave.', N'user', N'Pending', CAST(N'2021-10-09' AS Date), NULL)
INSERT [dbo].[family_updates] ([request_id], [field_to_be_changed], [family_id], [action], [new_data], [old_data], [requestor], [status], [request_date], [finalize_date]) VALUES (300002, N'parent_guardian_name', 100004, N'Update', N'Cohort 15 Team Mega-Awesome', N'Cohort 15 Team Oscar', N'user', N'Rejected', CAST(N'2021-11-24' AS Date), NULL)
INSERT [dbo].[family_updates] ([request_id], [field_to_be_changed], [family_id], [action], [new_data], [old_data], [requestor], [status], [request_date], [finalize_date]) VALUES (300003, N'parent_guardian_name', 100002, N'Update', N'Mike Bowers', N'Mike Flowers', N'admin', N'Approved', CAST(N'2021-12-05' AS Date), NULL)
INSERT [dbo].[family_updates] ([request_id], [field_to_be_changed], [family_id], [action], [new_data], [old_data], [requestor], [status], [request_date], [finalize_date]) VALUES (300004, N'email_address', 100003, N'Update', N'mollygolly@molly.com', N'molly@molly.com', N'user', N'Pending', CAST(N'2021-11-27' AS Date), NULL)
INSERT [dbo].[family_updates] ([request_id], [field_to_be_changed], [family_id], [action], [new_data], [old_data], [requestor], [status], [request_date], [finalize_date]) VALUES (300005, N'city', 100001, N'Update', N'Flavortown', N'Columbus', N'user', N'Rejected', CAST(N'2021-12-05' AS Date), NULL)
INSERT [dbo].[family_updates] ([request_id], [field_to_be_changed], [family_id], [action], [new_data], [old_data], [requestor], [status], [request_date], [finalize_date]) VALUES (300006, N'zip', 100004, N'Update', N'43430', N'43333', N'user', N'Pending', CAST(N'2021-12-02' AS Date), NULL)
SET IDENTITY_INSERT [dbo].[family_updates] OFF
GO
SET IDENTITY_INSERT [dbo].[users] ON 

INSERT [dbo].[users] ([user_id], [username], [password_hash], [salt], [user_role]) VALUES (100001, N'user', N'Jg45HuwT7PZkfuKTz6IB90CtWY4=', N'LHxP4Xh7bN0=', N'user')
INSERT [dbo].[users] ([user_id], [username], [password_hash], [salt], [user_role]) VALUES (100002, N'admin', N'YhyGVQ+Ch69n4JMBncM4lNF/i9s=', N'Ar/aB2thQTI=', N'admin')
INSERT [dbo].[users] ([user_id], [username], [password_hash], [salt], [user_role]) VALUES (100003, N'user1', N'xoHXL8H19UA/rJxt5ZPjLTBMsBs=', N'qhrP2JaB7hU=', N'user')
INSERT [dbo].[users] ([user_id], [username], [password_hash], [salt], [user_role]) VALUES (100004, N'admin1', N'Ezfb61BifCDjp/Nd+07FuFMGTtg=', N'HSkPrlLRtLA=', N'user')
SET IDENTITY_INSERT [dbo].[users] OFF
GO
ALTER TABLE [dbo].[campers] ADD  DEFAULT ('') FOR [medications]
GO
ALTER TABLE [dbo].[campers] ADD  DEFAULT ('') FOR [allergies]
GO
ALTER TABLE [dbo].[campers] ADD  DEFAULT ('') FOR [special_needs]
GO
ALTER TABLE [dbo].[campers] ADD  DEFAULT ('true') FOR [active_status]
GO
ALTER TABLE [dbo].[ad_hoc_notes]  WITH CHECK ADD FOREIGN KEY([camper_code])
REFERENCES [dbo].[campers] ([camper_code])
GO
ALTER TABLE [dbo].[camper_updates]  WITH CHECK ADD FOREIGN KEY([camper_code])
REFERENCES [dbo].[campers] ([camper_code])
GO
ALTER TABLE [dbo].[campers]  WITH CHECK ADD FOREIGN KEY([family_id])
REFERENCES [dbo].[family] ([family_id])
GO
ALTER TABLE [dbo].[family_updates]  WITH CHECK ADD FOREIGN KEY([family_id])
REFERENCES [dbo].[family] ([family_id])
GO
ALTER TABLE [dbo].[camper_updates]  WITH CHECK ADD  CONSTRAINT [Chk_camper_updates_action] CHECK  (([action]='Delete' OR [action]='Update' OR [action]='ADD'))
GO
ALTER TABLE [dbo].[camper_updates] CHECK CONSTRAINT [Chk_camper_updates_action]
GO
ALTER TABLE [dbo].[camper_updates]  WITH CHECK ADD  CONSTRAINT [Chk_camper_updates_status] CHECK  (([status]='Rejected' OR [status]='Updated' OR [status]='Approved' OR [status]='Pending'))
GO
ALTER TABLE [dbo].[camper_updates] CHECK CONSTRAINT [Chk_camper_updates_status]
GO
ALTER TABLE [dbo].[family_updates]  WITH CHECK ADD  CONSTRAINT [Chk_family_updates_action] CHECK  (([action]='Delete' OR [action]='Update' OR [action]='ADD'))
GO
ALTER TABLE [dbo].[family_updates] CHECK CONSTRAINT [Chk_family_updates_action]
GO
ALTER TABLE [dbo].[family_updates]  WITH CHECK ADD  CONSTRAINT [Chk_family_updates_status] CHECK  (([status]='Rejected' OR [status]='Updated' OR [status]='Approved' OR [status]='Pending'))
GO
ALTER TABLE [dbo].[family_updates] CHECK CONSTRAINT [Chk_family_updates_status]
GO
USE [master]
GO
ALTER DATABASE [final_capstone] SET  READ_WRITE 
GO
