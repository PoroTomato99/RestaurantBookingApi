USE [master]
GO
/****** Object:  Database [RestaurantBookingDb]    Script Date: 5/31/2021 9:25:28 PM ******/
CREATE DATABASE [RestaurantBookingDb]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'RestaurantBookingDb', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.MSSQLSERVER\MSSQL\DATA\RestaurantBookingDb.mdf' , SIZE = 3072KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'RestaurantBookingDb_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.MSSQLSERVER\MSSQL\DATA\RestaurantBookingDb_log.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [RestaurantBookingDb] SET COMPATIBILITY_LEVEL = 120
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [RestaurantBookingDb].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [RestaurantBookingDb] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [RestaurantBookingDb] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [RestaurantBookingDb] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [RestaurantBookingDb] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [RestaurantBookingDb] SET ARITHABORT OFF 
GO
ALTER DATABASE [RestaurantBookingDb] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [RestaurantBookingDb] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [RestaurantBookingDb] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [RestaurantBookingDb] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [RestaurantBookingDb] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [RestaurantBookingDb] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [RestaurantBookingDb] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [RestaurantBookingDb] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [RestaurantBookingDb] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [RestaurantBookingDb] SET  DISABLE_BROKER 
GO
ALTER DATABASE [RestaurantBookingDb] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [RestaurantBookingDb] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [RestaurantBookingDb] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [RestaurantBookingDb] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [RestaurantBookingDb] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [RestaurantBookingDb] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [RestaurantBookingDb] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [RestaurantBookingDb] SET RECOVERY FULL 
GO
ALTER DATABASE [RestaurantBookingDb] SET  MULTI_USER 
GO
ALTER DATABASE [RestaurantBookingDb] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [RestaurantBookingDb] SET DB_CHAINING OFF 
GO
ALTER DATABASE [RestaurantBookingDb] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [RestaurantBookingDb] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
ALTER DATABASE [RestaurantBookingDb] SET DELAYED_DURABILITY = DISABLED 
GO
EXEC sys.sp_db_vardecimal_storage_format N'RestaurantBookingDb', N'ON'
GO
USE [RestaurantBookingDb]
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 5/31/2021 9:25:28 PM ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Address]    Script Date: 5/31/2021 9:25:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Address](
	[RestaurantId] [int] NOT NULL,
	[BuildingNo] [int] NULL,
	[Address1] [nvarchar](100) NULL,
	[Address2] [nvarchar](100) NULL,
	[City] [nvarchar](100) NULL,
	[State] [nvarchar](100) NULL,
	[PostCode] [nvarchar](100) NULL,
	[Country] [nvarchar](100) NULL,
 CONSTRAINT [PK_Address] PRIMARY KEY CLUSTERED 
(
	[RestaurantId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Annoucements]    Script Date: 5/31/2021 9:25:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Annoucements](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Date] [datetime] NULL,
	[Type] [nvarchar](50) NULL,
	[Message] [nvarchar](500) NULL,
	[UserId] [nvarchar](450) NULL,
 CONSTRAINT [PK_Annoucements] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[AspNetRoleClaims]    Script Date: 5/31/2021 9:25:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetRoleClaims](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[RoleId] [nvarchar](450) NOT NULL,
	[ClaimType] [nvarchar](max) NULL,
	[ClaimValue] [nvarchar](max) NULL,
 CONSTRAINT [PK_AspNetRoleClaims] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[AspNetRoles]    Script Date: 5/31/2021 9:25:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetRoles](
	[Id] [nvarchar](450) NOT NULL,
	[Name] [nvarchar](256) NULL,
	[NormalizedName] [nvarchar](256) NULL,
	[ConcurrencyStamp] [nvarchar](max) NULL,
 CONSTRAINT [PK_AspNetRoles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[AspNetUserClaims]    Script Date: 5/31/2021 9:25:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserClaims](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [nvarchar](450) NOT NULL,
	[ClaimType] [nvarchar](max) NULL,
	[ClaimValue] [nvarchar](max) NULL,
 CONSTRAINT [PK_AspNetUserClaims] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[AspNetUserLogins]    Script Date: 5/31/2021 9:25:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserLogins](
	[LoginProvider] [nvarchar](450) NOT NULL,
	[ProviderKey] [nvarchar](450) NOT NULL,
	[ProviderDisplayName] [nvarchar](max) NULL,
	[UserId] [nvarchar](450) NOT NULL,
 CONSTRAINT [PK_AspNetUserLogins] PRIMARY KEY CLUSTERED 
(
	[LoginProvider] ASC,
	[ProviderKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[AspNetUserRoles]    Script Date: 5/31/2021 9:25:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserRoles](
	[UserId] [nvarchar](450) NOT NULL,
	[RoleId] [nvarchar](450) NOT NULL,
 CONSTRAINT [PK_AspNetUserRoles] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[AspNetUsers]    Script Date: 5/31/2021 9:25:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUsers](
	[Id] [nvarchar](450) NOT NULL,
	[UserName] [nvarchar](256) NULL,
	[NormalizedUserName] [nvarchar](256) NULL,
	[Email] [nvarchar](256) NULL,
	[NormalizedEmail] [nvarchar](256) NULL,
	[EmailConfirmed] [bit] NOT NULL,
	[PasswordHash] [nvarchar](max) NULL,
	[SecurityStamp] [nvarchar](max) NULL,
	[ConcurrencyStamp] [nvarchar](max) NULL,
	[PhoneNumber] [nvarchar](max) NULL,
	[PhoneNumberConfirmed] [bit] NOT NULL,
	[TwoFactorEnabled] [bit] NOT NULL,
	[LockoutEnd] [datetimeoffset](7) NULL,
	[LockoutEnabled] [bit] NOT NULL,
	[AccessFailedCount] [int] NOT NULL,
 CONSTRAINT [PK_AspNetUsers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[AspNetUserTokens]    Script Date: 5/31/2021 9:25:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserTokens](
	[UserId] [nvarchar](450) NOT NULL,
	[LoginProvider] [nvarchar](450) NOT NULL,
	[Name] [nvarchar](450) NOT NULL,
	[Value] [nvarchar](max) NULL,
 CONSTRAINT [PK_AspNetUserTokens] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[LoginProvider] ASC,
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Booking]    Script Date: 5/31/2021 9:25:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Booking](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[BookingDate] [datetime] NOT NULL,
	[StatuId] [int] NOT NULL,
	[UserId] [nvarchar](450) NOT NULL,
	[ReservedDate] [date] NOT NULL,
	[ReservedTime] [time](2) NOT NULL,
 CONSTRAINT [PK_Booking] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[BookingDetails]    Script Date: 5/31/2021 9:25:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BookingDetails](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[RestaurantId] [int] NOT NULL,
	[BookingId] [int] NOT NULL,
 CONSTRAINT [PK_BookingDetails_1] PRIMARY KEY CLUSTERED 
(
	[Id] ASC,
	[RestaurantId] ASC,
	[BookingId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[NewRestaurantForm]    Script Date: 5/31/2021 9:25:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[NewRestaurantForm](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[RestaurantName] [nvarchar](50) NULL,
	[Description] [nvarchar](500) NULL,
	[Approval] [int] NULL,
	[UserId] [nvarchar](450) NOT NULL,
	[BuildingNo] [int] NULL,
	[Address1] [nvarchar](100) NULL,
	[Address2] [nvarchar](100) NULL,
	[City] [nvarchar](100) NULL,
	[State] [nvarchar](100) NULL,
	[PostCode] [nvarchar](100) NULL,
	[Country] [nvarchar](100) NULL,
 CONSTRAINT [PK_NewRestaurantForm] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Restaurant]    Script Date: 5/31/2021 9:25:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Restaurant](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Description] [nvarchar](100) NULL,
 CONSTRAINT [PK_Restaurant] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Status]    Script Date: 5/31/2021 9:25:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Status](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Status] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Status] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Table]    Script Date: 5/31/2021 9:25:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Table](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[TableNum] [int] NOT NULL,
	[RestaurantId] [int] NOT NULL,
 CONSTRAINT [PK_Table] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[TimeSlot]    Script Date: 5/31/2021 9:25:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TimeSlot](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[AvailableTime] [time](2) NOT NULL,
	[TimeStatusId] [int] NOT NULL,
	[RestaurantId] [int] NOT NULL,
	[Vacancy] [int] NULL,
 CONSTRAINT [PK_TimeSlot] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[TimeStatus]    Script Date: 5/31/2021 9:25:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TimeStatus](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[TimeStatus] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_TimeStatus] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[UserProfile]    Script Date: 5/31/2021 9:25:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserProfile](
	[Id] [nvarchar](450) NOT NULL,
	[FirstName] [nvarchar](100) NULL,
	[LastName] [nvarchar](100) NULL,
	[DateJoined] [date] NULL,
	[RestaurantId] [int] NULL,
 CONSTRAINT [PK_UserProfile] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20210421081656_AspIdentity', N'5.0.5')
INSERT [dbo].[Address] ([RestaurantId], [BuildingNo], [Address1], [Address2], [City], [State], [PostCode], [Country]) VALUES (3, 123, N'address 1', N'second address', N'restaurant city', N'Selangor', N'1212', N'testcode')
INSERT [dbo].[Address] ([RestaurantId], [BuildingNo], [Address1], [Address2], [City], [State], [PostCode], [Country]) VALUES (4, 296, N'Jalan Kuching', N'Taman Kuching', N'Kuala Lumpur', N'Wilayah Persukutuan', N'52000', N'Malaysia')
INSERT [dbo].[Address] ([RestaurantId], [BuildingNo], [Address1], [Address2], [City], [State], [PostCode], [Country]) VALUES (11, 12345, N'new address 999', N'jalan address', N'Johor Bahru', N'Johor Bahru', N'53530', N'Australia')
INSERT [dbo].[Address] ([RestaurantId], [BuildingNo], [Address1], [Address2], [City], [State], [PostCode], [Country]) VALUES (12, 6, N'KL', N'KL', N'KL', N'KL', N'53100', N'Malaysia')
SET IDENTITY_INSERT [dbo].[Annoucements] ON 

INSERT [dbo].[Annoucements] ([Id], [Date], [Type], [Message], [UserId]) VALUES (1, CAST(N'2021-04-27 11:40:47.547' AS DateTime), N'Testing ', N'Public Announcement to Everyone', NULL)
INSERT [dbo].[Annoucements] ([Id], [Date], [Type], [Message], [UserId]) VALUES (2, CAST(N'2021-04-27 15:14:27.667' AS DateTime), N'TestSend', N'Test to save notification to db in dependency injection console application', NULL)
INSERT [dbo].[Annoucements] ([Id], [Date], [Type], [Message], [UserId]) VALUES (3, CAST(N'2021-04-27 16:37:15.770' AS DateTime), N'Notification', N'Your Booking Order recorded, you will received an email after the booking is approved by admin.', NULL)
INSERT [dbo].[Annoucements] ([Id], [Date], [Type], [Message], [UserId]) VALUES (4, CAST(N'2021-04-27 17:06:47.867' AS DateTime), N'Notification_2', N'Your Booking Order is pending, please wait for email confirmation.', NULL)
INSERT [dbo].[Annoucements] ([Id], [Date], [Type], [Message], [UserId]) VALUES (14, CAST(N'2021-05-05 05:54:04.960' AS DateTime), N'Reservation Status', N'We''re sending this email to inform that your reservatation made on 5/5/2021 1:25:07 AM <br /> for 10:00:00 on 8/8/2021 12:00:00 AM <br />is <strong>Booked</strong>', N'85048e75-2c36-469a-9d5a-3eb5c45e5ddc')
INSERT [dbo].[Annoucements] ([Id], [Date], [Type], [Message], [UserId]) VALUES (15, CAST(N'2021-05-05 05:55:35.953' AS DateTime), N'Reservation Status', N'We''re sending this email to inform that your reservatation made on 5/5/2021 1:25:07 AM <br /> for 10:00:00 on 8/8/2021 12:00:00 AM <br />is <strong>Cancelled</strong>', N'85048e75-2c36-469a-9d5a-3eb5c45e5ddc')
INSERT [dbo].[Annoucements] ([Id], [Date], [Type], [Message], [UserId]) VALUES (16, CAST(N'2021-05-05 06:00:05.307' AS DateTime), N'Reservation Status', N'We''re sending this email to inform that your reservatation made on 5/5/2021 1:25:07 AM <br /> for 10:00:00 on 8/8/2021 12:00:00 AM <br />is <strong>Processing</strong>', N'85048e75-2c36-469a-9d5a-3eb5c45e5ddc')
INSERT [dbo].[Annoucements] ([Id], [Date], [Type], [Message], [UserId]) VALUES (17, CAST(N'2021-05-17 12:25:04.723' AS DateTime), N'Booking No : 51 Status', N'We''re sending this email to inform that your reservatation made on 5/17/2021 12:25:04 PM <br /> for 10:00:00 on 5/17/2021 12:00:00 AM <br />is <strong>Processing</strong><br /> Please remember to provide this booking number :<p> 51 </p> to prove you reservation.', N'5d32d630-487b-4f01-9e32-b9b84f3b4ac1')
INSERT [dbo].[Annoucements] ([Id], [Date], [Type], [Message], [UserId]) VALUES (18, CAST(N'2021-05-17 12:25:17.767' AS DateTime), N'Booking No : 52 Status', N'We''re sending this email to inform that your reservatation made on 5/17/2021 12:25:17 PM <br /> for 10:00:00 on 5/17/2021 12:00:00 AM <br />is <strong>Processing</strong><br /> Please remember to provide this booking number :<p> 52 </p> to prove you reservation.', N'5d32d630-487b-4f01-9e32-b9b84f3b4ac1')
INSERT [dbo].[Annoucements] ([Id], [Date], [Type], [Message], [UserId]) VALUES (19, CAST(N'2021-05-17 12:28:11.870' AS DateTime), N'Booking No : 53 Status', N'We''re sending this email to inform that your reservatation made on 5/17/2021 12:28:11 PM <br /> for 10:00:00 on 5/17/2021 12:00:00 AM <br />is <strong>Processing</strong><br /> Please remember to provide this booking number :<p> 53 </p> to prove you reservation.', N'5d32d630-487b-4f01-9e32-b9b84f3b4ac1')
INSERT [dbo].[Annoucements] ([Id], [Date], [Type], [Message], [UserId]) VALUES (20, CAST(N'2021-05-17 12:28:27.343' AS DateTime), N'Booking No : 54 Status', N'We''re sending this email to inform that your reservatation made on 5/17/2021 12:28:27 PM <br /> for 10:00:00 on 5/17/2021 12:00:00 AM <br />is <strong>Processing</strong><br /> Please remember to provide this booking number :<p> 54 </p> to prove you reservation.', N'5d32d630-487b-4f01-9e32-b9b84f3b4ac1')
INSERT [dbo].[Annoucements] ([Id], [Date], [Type], [Message], [UserId]) VALUES (21, CAST(N'2021-05-17 13:43:37.477' AS DateTime), N'Booking No : 55 Status', N'We''re sending this email to inform that your reservatation made on 5/17/2021 1:43:36 PM <br /> for 04:00:00 on 5/17/2021 12:00:00 AM <br />is <strong>Processing</strong><br /> Please remember to provide this booking number :<p> 55 </p> to prove you reservation.', N'5d32d630-487b-4f01-9e32-b9b84f3b4ac1')
INSERT [dbo].[Annoucements] ([Id], [Date], [Type], [Message], [UserId]) VALUES (22, CAST(N'2021-05-17 14:29:42.150' AS DateTime), N'Booking No : 56 Status', N'We''re sending this email to inform that your reservatation made on 5/17/2021 2:29:41 PM <br /> for 10:00:00 on 5/17/2021 12:00:00 AM <br />is <strong>Processing</strong><br /> Please remember to provide this booking number :<p> 56 </p> to prove you reservation.', N'5d32d630-487b-4f01-9e32-b9b84f3b4ac1')
INSERT [dbo].[Annoucements] ([Id], [Date], [Type], [Message], [UserId]) VALUES (23, CAST(N'2021-05-17 14:29:52.257' AS DateTime), N'Booking No : 57 Status', N'We''re sending this email to inform that your reservatation made on 5/17/2021 2:29:52 PM <br /> for 10:00:00 on 5/17/2021 12:00:00 AM <br />is <strong>Processing</strong><br /> Please remember to provide this booking number :<p> 57 </p> to prove you reservation.', N'5d32d630-487b-4f01-9e32-b9b84f3b4ac1')
INSERT [dbo].[Annoucements] ([Id], [Date], [Type], [Message], [UserId]) VALUES (24, CAST(N'2021-05-17 14:30:18.453' AS DateTime), N'Booking No : 58 Status', N'We''re sending this email to inform that your reservatation made on 5/17/2021 2:30:18 PM <br /> for 10:00:00 on 5/17/2021 12:00:00 AM <br />is <strong>Processing</strong><br /> Please remember to provide this booking number :<p> 58 </p> to prove you reservation.', N'5d32d630-487b-4f01-9e32-b9b84f3b4ac1')
INSERT [dbo].[Annoucements] ([Id], [Date], [Type], [Message], [UserId]) VALUES (25, CAST(N'2021-05-17 14:30:45.700' AS DateTime), N'Booking No : 59 Status', N'We''re sending this email to inform that your reservatation made on 5/17/2021 2:30:45 PM <br /> for 10:00:00 on 5/17/2021 12:00:00 AM <br />is <strong>Processing</strong><br /> Please remember to provide this booking number :<p> 59 </p> to prove you reservation.', N'5d32d630-487b-4f01-9e32-b9b84f3b4ac1')
INSERT [dbo].[Annoucements] ([Id], [Date], [Type], [Message], [UserId]) VALUES (26, CAST(N'2021-05-17 14:30:55.363' AS DateTime), N'Booking No : 60 Status', N'We''re sending this email to inform that your reservatation made on 5/17/2021 2:30:55 PM <br /> for 10:00:00 on 5/17/2021 12:00:00 AM <br />is <strong>Processing</strong><br /> Please remember to provide this booking number :<p> 60 </p> to prove you reservation.', N'5d32d630-487b-4f01-9e32-b9b84f3b4ac1')
INSERT [dbo].[Annoucements] ([Id], [Date], [Type], [Message], [UserId]) VALUES (27, CAST(N'2021-05-17 14:31:08.630' AS DateTime), N'Booking No : 61 Status', N'We''re sending this email to inform that your reservatation made on 5/17/2021 2:31:08 PM <br /> for 10:00:00 on 5/17/2021 12:00:00 AM <br />is <strong>Processing</strong><br /> Please remember to provide this booking number :<p> 61 </p> to prove you reservation.', N'5d32d630-487b-4f01-9e32-b9b84f3b4ac1')
INSERT [dbo].[Annoucements] ([Id], [Date], [Type], [Message], [UserId]) VALUES (28, CAST(N'2021-05-17 14:31:29.450' AS DateTime), N'Booking No : 62 Status', N'We''re sending this email to inform that your reservatation made on 5/17/2021 2:31:29 PM <br /> for 10:00:00 on 5/17/2021 12:00:00 AM <br />is <strong>Processing</strong><br /> Please remember to provide this booking number :<p> 62 </p> to prove you reservation.', N'5d32d630-487b-4f01-9e32-b9b84f3b4ac1')
INSERT [dbo].[Annoucements] ([Id], [Date], [Type], [Message], [UserId]) VALUES (29, CAST(N'2021-05-17 14:31:52.780' AS DateTime), N'Booking No : 63 Status', N'We''re sending this email to inform that your reservatation made on 5/17/2021 2:31:52 PM <br /> for 10:00:00 on 5/17/2021 12:00:00 AM <br />is <strong>Processing</strong><br /> Please remember to provide this booking number :<p> 63 </p> to prove you reservation.', N'5d32d630-487b-4f01-9e32-b9b84f3b4ac1')
INSERT [dbo].[Annoucements] ([Id], [Date], [Type], [Message], [UserId]) VALUES (30, CAST(N'2021-05-17 14:32:04.353' AS DateTime), N'Booking No : 64 Status', N'We''re sending this email to inform that your reservatation made on 5/17/2021 2:32:04 PM <br /> for 10:00:00 on 5/17/2021 12:00:00 AM <br />is <strong>Processing</strong><br /> Please remember to provide this booking number :<p> 64 </p> to prove you reservation.', N'5d32d630-487b-4f01-9e32-b9b84f3b4ac1')
INSERT [dbo].[Annoucements] ([Id], [Date], [Type], [Message], [UserId]) VALUES (31, CAST(N'2021-05-17 14:32:30.717' AS DateTime), N'Booking No : 65 Status', N'We''re sending this email to inform that your reservatation made on 5/17/2021 2:32:30 PM <br /> for 10:00:00 on 5/17/2021 12:00:00 AM <br />is <strong>Processing</strong><br /> Please remember to provide this booking number :<p> 65 </p> to prove you reservation.', N'5d32d630-487b-4f01-9e32-b9b84f3b4ac1')
INSERT [dbo].[Annoucements] ([Id], [Date], [Type], [Message], [UserId]) VALUES (32, CAST(N'2021-05-17 14:32:38.257' AS DateTime), N'Booking No : 66 Status', N'We''re sending this email to inform that your reservatation made on 5/17/2021 2:32:38 PM <br /> for 10:00:00 on 5/17/2021 12:00:00 AM <br />is <strong>Processing</strong><br /> Please remember to provide this booking number :<p> 66 </p> to prove you reservation.', N'5d32d630-487b-4f01-9e32-b9b84f3b4ac1')
INSERT [dbo].[Annoucements] ([Id], [Date], [Type], [Message], [UserId]) VALUES (33, CAST(N'2021-05-17 14:32:46.950' AS DateTime), N'Booking No : 67 Status', N'We''re sending this email to inform that your reservatation made on 5/17/2021 2:32:46 PM <br /> for 10:00:00 on 5/17/2021 12:00:00 AM <br />is <strong>Processing</strong><br /> Please remember to provide this booking number :<p> 67 </p> to prove you reservation.', N'5d32d630-487b-4f01-9e32-b9b84f3b4ac1')
INSERT [dbo].[Annoucements] ([Id], [Date], [Type], [Message], [UserId]) VALUES (34, CAST(N'2021-05-17 14:33:14.913' AS DateTime), N'Booking No : 68 Status', N'We''re sending this email to inform that your reservatation made on 5/17/2021 2:33:14 PM <br /> for 10:00:00 on 5/17/2021 12:00:00 AM <br />is <strong>Processing</strong><br /> Please remember to provide this booking number :<p> 68 </p> to prove you reservation.', N'5d32d630-487b-4f01-9e32-b9b84f3b4ac1')
INSERT [dbo].[Annoucements] ([Id], [Date], [Type], [Message], [UserId]) VALUES (35, CAST(N'2021-05-17 14:33:23.557' AS DateTime), N'Booking No : 69 Status', N'We''re sending this email to inform that your reservatation made on 5/17/2021 2:33:23 PM <br /> for 10:00:00 on 5/17/2021 12:00:00 AM <br />is <strong>Processing</strong><br /> Please remember to provide this booking number :<p> 69 </p> to prove you reservation.', N'5d32d630-487b-4f01-9e32-b9b84f3b4ac1')
INSERT [dbo].[Annoucements] ([Id], [Date], [Type], [Message], [UserId]) VALUES (36, CAST(N'2021-05-17 14:33:52.633' AS DateTime), N'Booking No : 70 Status', N'We''re sending this email to inform that your reservatation made on 5/17/2021 2:33:52 PM <br /> for 10:00:00 on 5/17/2021 12:00:00 AM <br />is <strong>Processing</strong><br /> Please remember to provide this booking number :<p> 70 </p> to prove you reservation.', N'5d32d630-487b-4f01-9e32-b9b84f3b4ac1')
INSERT [dbo].[Annoucements] ([Id], [Date], [Type], [Message], [UserId]) VALUES (37, CAST(N'2021-05-17 14:34:02.603' AS DateTime), N'Booking No : 71 Status', N'We''re sending this email to inform that your reservatation made on 5/17/2021 2:34:02 PM <br /> for 10:00:00 on 5/17/2021 12:00:00 AM <br />is <strong>Processing</strong><br /> Please remember to provide this booking number :<p> 71 </p> to prove you reservation.', N'5d32d630-487b-4f01-9e32-b9b84f3b4ac1')
INSERT [dbo].[Annoucements] ([Id], [Date], [Type], [Message], [UserId]) VALUES (38, CAST(N'2021-05-17 14:34:42.923' AS DateTime), N'Booking No : 72 Status', N'We''re sending this email to inform that your reservatation made on 5/17/2021 2:34:42 PM <br /> for 10:00:00 on 5/17/2021 12:00:00 AM <br />is <strong>Processing</strong><br /> Please remember to provide this booking number :<p> 72 </p> to prove you reservation.', N'5d32d630-487b-4f01-9e32-b9b84f3b4ac1')
INSERT [dbo].[Annoucements] ([Id], [Date], [Type], [Message], [UserId]) VALUES (39, CAST(N'2021-05-17 14:34:51.357' AS DateTime), N'Booking No : 73 Status', N'We''re sending this email to inform that your reservatation made on 5/17/2021 2:34:51 PM <br /> for 10:00:00 on 5/17/2021 12:00:00 AM <br />is <strong>Processing</strong><br /> Please remember to provide this booking number :<p> 73 </p> to prove you reservation.', N'5d32d630-487b-4f01-9e32-b9b84f3b4ac1')
INSERT [dbo].[Annoucements] ([Id], [Date], [Type], [Message], [UserId]) VALUES (40, CAST(N'2021-05-17 14:35:02.510' AS DateTime), N'Booking No : 74 Status', N'We''re sending this email to inform that your reservatation made on 5/17/2021 2:35:02 PM <br /> for 10:00:00 on 5/17/2021 12:00:00 AM <br />is <strong>Processing</strong><br /> Please remember to provide this booking number :<p> 74 </p> to prove you reservation.', N'5d32d630-487b-4f01-9e32-b9b84f3b4ac1')
INSERT [dbo].[Annoucements] ([Id], [Date], [Type], [Message], [UserId]) VALUES (41, CAST(N'2021-05-17 14:35:11.073' AS DateTime), N'Booking No : 75 Status', N'We''re sending this email to inform that your reservatation made on 5/17/2021 2:35:11 PM <br /> for 10:00:00 on 5/17/2021 12:00:00 AM <br />is <strong>Processing</strong><br /> Please remember to provide this booking number :<p> 75 </p> to prove you reservation.', N'5d32d630-487b-4f01-9e32-b9b84f3b4ac1')
INSERT [dbo].[Annoucements] ([Id], [Date], [Type], [Message], [UserId]) VALUES (42, CAST(N'2021-05-17 23:44:08.313' AS DateTime), N'Booking No : 76 Status', N'We''re sending this email to inform that your reservatation made on 5/17/2021 11:44:07 PM <br /> for 10:00:00 on 5/17/2021 12:00:00 AM <br />is <strong>Processing</strong><br /> Please remember to provide this booking number :<p> 76 </p> to prove you reservation.', N'5d32d630-487b-4f01-9e32-b9b84f3b4ac1')
INSERT [dbo].[Annoucements] ([Id], [Date], [Type], [Message], [UserId]) VALUES (43, CAST(N'2021-05-17 23:44:19.650' AS DateTime), N'Booking No : 77 Status', N'We''re sending this email to inform that your reservatation made on 5/17/2021 11:44:19 PM <br /> for 10:00:00 on 5/17/2021 12:00:00 AM <br />is <strong>Processing</strong><br /> Please remember to provide this booking number :<p> 77 </p> to prove you reservation.', N'5d32d630-487b-4f01-9e32-b9b84f3b4ac1')
INSERT [dbo].[Annoucements] ([Id], [Date], [Type], [Message], [UserId]) VALUES (44, CAST(N'2021-05-17 23:48:46.380' AS DateTime), N'Booking No : 79 Status', N'We''re sending this email to inform that your reservatation made on 5/17/2021 11:47:35 PM <br /> for 10:00:00 on 5/17/2021 12:00:00 AM <br />is <strong>Processing</strong><br /> Please remember to provide this booking number :<p> 79 </p> to prove you reservation.', N'5d32d630-487b-4f01-9e32-b9b84f3b4ac1')
INSERT [dbo].[Annoucements] ([Id], [Date], [Type], [Message], [UserId]) VALUES (45, CAST(N'2021-05-17 23:48:46.380' AS DateTime), N'Booking No : 78 Status', N'We''re sending this email to inform that your reservatation made on 5/17/2021 11:48:22 PM <br /> for 10:00:00 on 5/17/2021 12:00:00 AM <br />is <strong>Processing</strong><br /> Please remember to provide this booking number :<p> 78 </p> to prove you reservation.', N'5d32d630-487b-4f01-9e32-b9b84f3b4ac1')
INSERT [dbo].[Annoucements] ([Id], [Date], [Type], [Message], [UserId]) VALUES (46, CAST(N'2021-05-17 23:55:32.960' AS DateTime), N'Booking No : 78 Status', N'We''re sending this email to inform that your reservatation made on 5/17/2021 11:48:22 PM <br /> for 10:00:00 on 5/17/2021 12:00:00 AM <br />is <strong>Completed</strong><br /> Please remember to provide this booking number :<p> 78 </p> to prove you reservation.', N'5d32d630-487b-4f01-9e32-b9b84f3b4ac1')
INSERT [dbo].[Annoucements] ([Id], [Date], [Type], [Message], [UserId]) VALUES (47, CAST(N'2021-05-18 00:01:36.000' AS DateTime), N'Booking No : 80 Status', N'We''re sending this email to inform that your reservatation made on 5/18/2021 12:01:15 AM <br /> for 10:00:00 on 5/18/2021 12:00:00 AM <br />is <strong>Processing</strong><br /> Please remember to provide this booking number :<p> 80 </p> to prove you reservation.', N'5d32d630-487b-4f01-9e32-b9b84f3b4ac1')
INSERT [dbo].[Annoucements] ([Id], [Date], [Type], [Message], [UserId]) VALUES (48, CAST(N'2021-05-18 00:03:06.293' AS DateTime), N'Booking No : 80 Status', N'We''re sending this email to inform that your reservatation made on 5/18/2021 12:01:15 AM <br /> for 10:00:00 on 5/18/2021 12:00:00 AM <br />is <strong>Cancelled</strong><br /> Please remember to provide this booking number :<p> 80 </p> to prove you reservation.', N'5d32d630-487b-4f01-9e32-b9b84f3b4ac1')
INSERT [dbo].[Annoucements] ([Id], [Date], [Type], [Message], [UserId]) VALUES (49, CAST(N'2021-05-18 00:05:32.723' AS DateTime), N'Booking No : 80 Status', N'We''re sending this email to inform that your reservatation made on 5/18/2021 12:01:15 AM <br /> for 10:00:00 on 5/18/2021 12:00:00 AM <br />is <strong>Completed</strong><br /> Please remember to provide this booking number :<p> 80 </p> to prove you reservation.', N'5d32d630-487b-4f01-9e32-b9b84f3b4ac1')
INSERT [dbo].[Annoucements] ([Id], [Date], [Type], [Message], [UserId]) VALUES (50, CAST(N'2021-05-18 00:05:52.557' AS DateTime), N'Booking No : 81 Status', N'We''re sending this email to inform that your reservatation made on 5/18/2021 12:05:48 AM <br /> for 10:00:00 on 5/18/2021 12:00:00 AM <br />is <strong>Processing</strong><br /> Please remember to provide this booking number :<p> 81 </p> to prove you reservation.', N'5d32d630-487b-4f01-9e32-b9b84f3b4ac1')
INSERT [dbo].[Annoucements] ([Id], [Date], [Type], [Message], [UserId]) VALUES (51, CAST(N'2021-05-18 00:06:34.413' AS DateTime), N'Booking No : 81 Status', N'We''re sending this email to inform that your reservatation made on 5/18/2021 12:05:48 AM <br /> for 10:00:00 on 5/18/2021 12:00:00 AM <br />is <strong>Cancelled</strong><br /> Please remember to provide this booking number :<p> 81 </p> to prove you reservation.', N'5d32d630-487b-4f01-9e32-b9b84f3b4ac1')
INSERT [dbo].[Annoucements] ([Id], [Date], [Type], [Message], [UserId]) VALUES (52, CAST(N'2021-05-18 00:23:00.407' AS DateTime), N'Booking No : 82 Status', N'We''re sending this email to inform that your reservatation made on 5/18/2021 12:22:29 AM <br /> for 10:00:00 on 5/18/2021 12:00:00 AM <br />is <strong>Processing</strong><br /> Please remember to provide this booking number :<p> 82 </p> to prove you reservation.', N'5d32d630-487b-4f01-9e32-b9b84f3b4ac1')
INSERT [dbo].[Annoucements] ([Id], [Date], [Type], [Message], [UserId]) VALUES (53, CAST(N'2021-05-18 13:58:27.450' AS DateTime), N'Booking No : 83 Status', N'We''re sending this email to inform that your reservatation made on 5/18/2021 1:57:57 PM <br /> for 10:00:00 on 5/18/2021 12:00:00 AM <br />is <strong>Processing</strong><br /> Please remember to provide this booking number :<p> 83 </p> to prove you reservation.', N'b311d7e8-08b6-41f4-89e4-739165915f84')
INSERT [dbo].[Annoucements] ([Id], [Date], [Type], [Message], [UserId]) VALUES (54, CAST(N'2021-05-18 14:02:37.800' AS DateTime), N'Booking No : 84 Status', N'We''re sending this email to inform that your reservatation made on 5/18/2021 2:02:35 PM <br /> for 10:00:00 on 5/18/2021 12:00:00 AM <br />is <strong>Processing</strong><br /> Please remember to provide this booking number :<p> 84 </p> to prove you reservation.', N'c53afa8f-7f5b-4ed3-badb-f63112e00ee9')
INSERT [dbo].[Annoucements] ([Id], [Date], [Type], [Message], [UserId]) VALUES (55, CAST(N'2021-05-18 14:05:39.820' AS DateTime), N'Booking No : 85 Status', N'We''re sending this email to inform that your reservatation made on 5/18/2021 2:05:39 PM <br /> for 10:00:00 on 5/18/2021 12:00:00 AM <br />is <strong>Processing</strong><br /> Please remember to provide this booking number :<p> 85 </p> to prove you reservation.', N'666a3693-8f7b-43b4-a41e-d674d68230a6')
INSERT [dbo].[Annoucements] ([Id], [Date], [Type], [Message], [UserId]) VALUES (56, CAST(N'2021-05-18 14:08:48.907' AS DateTime), N'Booking No : 86 Status', N'We''re sending this email to inform that your reservatation made on 5/18/2021 2:08:48 PM <br /> for 10:00:00 on 5/18/2021 12:00:00 AM <br />is <strong>Processing</strong><br /> Please remember to provide this booking number :<p> 86 </p> to prove you reservation.', N'3bdab846-0612-4a3a-bcd7-67e1bf11d2ac')
INSERT [dbo].[Annoucements] ([Id], [Date], [Type], [Message], [UserId]) VALUES (57, CAST(N'2021-05-31 15:45:45.367' AS DateTime), N'Booking No : 87 Status', N'We''re sending this email to inform that your reservatation made on 5/31/2021 3:45:44 PM <br /> for 20:00:00 on 5/31/2021 12:00:00 AM <br />is <strong>Processing</strong><br /> Please remember to provide this booking number :<p> 87 </p> to prove you reservation.', N'00e15181-cbf9-4cde-af17-6be097158f8c')
INSERT [dbo].[Annoucements] ([Id], [Date], [Type], [Message], [UserId]) VALUES (58, CAST(N'2021-05-31 15:47:51.747' AS DateTime), N'Booking No : 88 Status', N'We''re sending this email to inform that your reservatation made on 5/31/2021 3:47:51 PM <br /> for 21:00:00 on 5/31/2021 12:00:00 AM <br />is <strong>Processing</strong><br /> Please remember to provide this booking number :<p> 88 </p> to prove you reservation.', N'00e15181-cbf9-4cde-af17-6be097158f8c')
INSERT [dbo].[Annoucements] ([Id], [Date], [Type], [Message], [UserId]) VALUES (59, CAST(N'2021-05-31 15:56:42.793' AS DateTime), N'Booking No : 89 Status', N'We''re sending this email to inform that your reservatation made on 5/31/2021 3:56:42 PM <br /> for 06:00:00 on 5/31/2021 12:00:00 AM <br />is <strong>Processing</strong><br /> Please remember to provide this booking number :<p> 89 </p> to prove you reservation.', N'5f608703-d9e4-478c-b832-a58a75616874')
INSERT [dbo].[Annoucements] ([Id], [Date], [Type], [Message], [UserId]) VALUES (60, CAST(N'2021-05-31 16:00:11.663' AS DateTime), N'Booking No : 89 Status', N'We''re sending this email to inform that your reservatation made on 5/31/2021 3:56:42 PM <br /> for 06:00:00 on 5/31/2021 12:00:00 AM <br />is <strong>Booked</strong><br /> Please remember to provide this booking number :<p> 89 </p> to prove you reservation.', N'5f608703-d9e4-478c-b832-a58a75616874')
INSERT [dbo].[Annoucements] ([Id], [Date], [Type], [Message], [UserId]) VALUES (61, CAST(N'2021-05-31 16:00:11.663' AS DateTime), N'Booking No : 89 Status', N'We''re sending this email to inform that your reservatation made on 5/31/2021 3:56:42 PM <br /> for 06:00:00 on 5/31/2021 12:00:00 AM <br />is <strong>Booked</strong><br /> Please remember to provide this booking number :<p> 89 </p> to prove you reservation.', N'5f608703-d9e4-478c-b832-a58a75616874')
INSERT [dbo].[Annoucements] ([Id], [Date], [Type], [Message], [UserId]) VALUES (62, CAST(N'2021-05-31 16:03:13.630' AS DateTime), N'Booking No : 89 Status', N'We''re sending this email to inform that your reservatation made on 5/31/2021 3:56:42 PM <br /> for 06:00:00 on 5/31/2021 12:00:00 AM <br />is <strong>Completed</strong><br /> Please remember to provide this booking number :<p> 89 </p> to prove you reservation.', N'5f608703-d9e4-478c-b832-a58a75616874')
INSERT [dbo].[Annoucements] ([Id], [Date], [Type], [Message], [UserId]) VALUES (63, CAST(N'2021-05-31 16:03:53.220' AS DateTime), N'Booking No : 90 Status', N'We''re sending this email to inform that your reservatation made on 5/31/2021 4:03:53 PM <br /> for 22:00:00 on 6/1/2021 12:00:00 AM <br />is <strong>Processing</strong><br /> Please remember to provide this booking number :<p> 90 </p> to prove you reservation.', N'5f608703-d9e4-478c-b832-a58a75616874')
SET IDENTITY_INSERT [dbo].[Annoucements] OFF
INSERT [dbo].[AspNetRoles] ([Id], [Name], [NormalizedName], [ConcurrencyStamp]) VALUES (N'5c89c2c9-6678-417e-8166-a006358c393e', N'RestaurantOwner', N'RESTAURANTOWNER', N'7b47fbe8-2560-44f4-b2d1-3fd2256df388')
INSERT [dbo].[AspNetRoles] ([Id], [Name], [NormalizedName], [ConcurrencyStamp]) VALUES (N'8b84d164-f2f6-4943-93cd-cf8e3813058b', N'Admin', N'ADMIN', N'90e307ff-e4e4-4cc4-a9c7-edab39983cf7')
INSERT [dbo].[AspNetRoles] ([Id], [Name], [NormalizedName], [ConcurrencyStamp]) VALUES (N'9c166104-9183-4bac-831a-a086308ddd79', N'Master', N'MASTER', N'2603cd6a-14c3-4e0a-be49-7506b2e611b4')
INSERT [dbo].[AspNetRoles] ([Id], [Name], [NormalizedName], [ConcurrencyStamp]) VALUES (N'cbb3b4c4-bd6f-4161-bca9-437489a25494', N'User', N'USER', N'a1e91917-4882-40a6-b162-26adc76d76ae')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'5179d5fc-c062-401d-ba74-3ebbf508bc24', N'8b84d164-f2f6-4943-93cd-cf8e3813058b')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'532b1ec0-b12d-4dd5-a76c-30269d82c675', N'8b84d164-f2f6-4943-93cd-cf8e3813058b')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'5f608703-d9e4-478c-b832-a58a75616874', N'8b84d164-f2f6-4943-93cd-cf8e3813058b')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'6dcdfdda-b3d4-45b0-a72b-c516354a0b31', N'8b84d164-f2f6-4943-93cd-cf8e3813058b')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'85048e75-2c36-469a-9d5a-3eb5c45e5ddc', N'8b84d164-f2f6-4943-93cd-cf8e3813058b')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'287d75ed-2614-4642-b490-bb74098c03ae', N'9c166104-9183-4bac-831a-a086308ddd79')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'00e15181-cbf9-4cde-af17-6be097158f8c', N'cbb3b4c4-bd6f-4161-bca9-437489a25494')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'05a0f228-6131-4ce1-96f9-43747af5047f', N'cbb3b4c4-bd6f-4161-bca9-437489a25494')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'3bdab846-0612-4a3a-bcd7-67e1bf11d2ac', N'cbb3b4c4-bd6f-4161-bca9-437489a25494')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'3e53ed36-380a-43d0-a9d8-44f2920e6d7f', N'cbb3b4c4-bd6f-4161-bca9-437489a25494')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'5d32d630-487b-4f01-9e32-b9b84f3b4ac1', N'cbb3b4c4-bd6f-4161-bca9-437489a25494')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'666a3693-8f7b-43b4-a41e-d674d68230a6', N'cbb3b4c4-bd6f-4161-bca9-437489a25494')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'7a6ced01-f1f1-4f69-b304-9aa30bafc289', N'cbb3b4c4-bd6f-4161-bca9-437489a25494')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'b311d7e8-08b6-41f4-89e4-739165915f84', N'cbb3b4c4-bd6f-4161-bca9-437489a25494')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'b743512b-1c8f-40a3-953a-59219b8dc9a6', N'cbb3b4c4-bd6f-4161-bca9-437489a25494')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'c53afa8f-7f5b-4ed3-badb-f63112e00ee9', N'cbb3b4c4-bd6f-4161-bca9-437489a25494')
INSERT [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'00e15181-cbf9-4cde-af17-6be097158f8c', N'NormalUser5', N'NORMALUSER5', N'zagnosirko@biyac.com', N'ZAGNOSIRKO@BIYAC.COM', 1, N'AQAAAAEAACcQAAAAEEsb59rJDUif+Zb6vBcIX7H0dESiQGJSvRWdROu+3w45NeHMNpRBeGEj04SDjkFOoA==', N'FRWQDUS6MKP7MFKXQPFXN2OIF76ELSZA', N'87a0fa23-db71-490b-a36d-12d32cfc378d', NULL, 0, 0, NULL, 1, 0)
INSERT [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'05a0f228-6131-4ce1-96f9-43747af5047f', N'SideHi99', N'SIDEHI99', N'sidehi9732@goqoez.com', N'SIDEHI9732@GOQOEZ.COM', 1, N'AQAAAAEAACcQAAAAEDp3U0QEQBfihAfGyue+kaBhSOaJAnFkWw3WhH1mfL2aglCNBaa55/5h4zH7zvlWWw==', N'BG66HJ2YDQRPXOAL7ECOXTKCOIUY2GCJ', N'fe817f91-8694-4378-ae76-0df32179c79a', NULL, 0, 0, NULL, 1, 0)
INSERT [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'287d75ed-2614-4642-b490-bb74098c03ae', N'MasterAdmin99', N'MASTERADMIN99', N'pacef25793@httptuan.com', N'PACEF25793@HTTPTUAN.COM', 1, N'AQAAAAEAACcQAAAAEDXVwQcbrIoViB9Fcnc8MD+/oUAMhv1nFCkJaAeBZnVQVyaoM3e1v9/9MpRh4K+uFw==', N'QMJL2VPRPZJKAS3CPJMOBRMOV3TTFNC3', N'7cac8b0a-6806-4bb0-8ed1-e500f8f36bca', NULL, 0, 0, NULL, 1, 0)
INSERT [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'3bdab846-0612-4a3a-bcd7-67e1bf11d2ac', N'NormalUser4', N'NORMALUSER4', N'feltaforta@biyac.com', N'FELTAFORTA@BIYAC.COM', 1, N'AQAAAAEAACcQAAAAEGvh6vm4Ar9mZwp+mbRPglrVDJX8qow16irEPaTSIDNqWWocRd+/gQ6U5NpPXnpVBA==', N'EO6HU5R2WKMW774CDNY7NUKWS5V4STAQ', N'2f328903-605d-48ec-82d5-9c4b7677f93f', NULL, 0, 0, NULL, 1, 0)
INSERT [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'3e53ed36-380a-43d0-a9d8-44f2920e6d7f', N'Josax73174', N'JOSAX73174', N'josax73174@goqoez.com', N'JOSAX73174@GOQOEZ.COM', 0, N'AQAAAAEAACcQAAAAECtq5eUbiwOUbqXEjyzZ5o/XhGVlFBGolxtEv30PVWYsRHN1vQrlnkUFV0AzFyJh9w==', N'VEWQRMNV57OYTKTINSRD6RNRZEIRTKEA', N'b208dcfe-1859-4145-9327-36bbb0f33666', NULL, 0, 0, NULL, 1, 0)
INSERT [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'5179d5fc-c062-401d-ba74-3ebbf508bc24', N'ChooChoo99', N'CHOOCHOO99', N'alanassignment@gmail.com', N'ALANASSIGNMENT@GMAIL.COM', 1, N'AQAAAAEAACcQAAAAEJtAEFfzTPP4/jfE558QXqC4M6KaGuLbZIXlp2kxpdMBh8+qZXmMyFK1yIY0rmzejQ==', N'4WPLRNMXNJXZOGFLJZHXVUDPTQU4NZ76', N'4eb834f0-5fc8-4985-9c4e-37f0b1f24afc', NULL, 0, 0, NULL, 1, 0)
INSERT [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'532b1ec0-b12d-4dd5-a76c-30269d82c675', N'TestUser99', N'TESTUSER99', N'sestelerze@biyac.com', N'SESTELERZE@BIYAC.COM', 1, N'AQAAAAEAACcQAAAAEIw7mKxKSXUYCA/frCC5jahOOzOLgitvN44UxY/2AJGd0JfLuyePFkAmyM13nOE8mw==', N'FXRMHFSILYGBYMV76ZDFIH7NMIPDXG5B', N'a53aaa69-f85e-48c1-a7d8-9c4f23471a53', NULL, 0, 0, NULL, 1, 0)
INSERT [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'5d32d630-487b-4f01-9e32-b9b84f3b4ac1', N'PoroUser99', N'POROUSER99', N'partuvofye@biyac.com', N'PARTUVOFYE@BIYAC.COM', 1, N'AQAAAAEAACcQAAAAEFSqcraDtTUgfmaftn80uwKiWsQhjfNG/LKJm5y+cBuR3U6vdkVBQRsaq0g5IDpmUg==', N'RPV7J26DUPB4XX6UDSHSRH2KA4EZUV6Q', N'f41c7958-7dd5-4fe3-af1a-ec771cada684', NULL, 0, 0, NULL, 1, 0)
INSERT [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'5f608703-d9e4-478c-b832-a58a75616874', N'NormalUser6', N'NORMALUSER6', N'hilesed896@sc2hub.com', N'HILESED896@SC2HUB.COM', 1, N'AQAAAAEAACcQAAAAEOAlT04k+3CGu9nLB6IU7yluyBcyzaZXzfx51gYPjfkvREzaWwnnFeSY9kBybJnCFA==', N'XLT7WHH44PAA5J62BRF2VFZ5FV3TLDOD', N'445963ea-4342-4daa-81a0-c7c53ee73328', NULL, 0, 0, NULL, 1, 0)
INSERT [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'666a3693-8f7b-43b4-a41e-d674d68230a6', N'NormalUser3', N'NORMALUSER3', N'dulmenofya@biyac.com', N'DULMENOFYA@BIYAC.COM', 1, N'AQAAAAEAACcQAAAAEK2cxBerUGzxg2u8lme232gcYdM8MZHDGnWKaajdw9OdoVkmOZ585TlTDY9M8uDcEA==', N'5LLXRUX5ZTFJIIN33U5M4YVSYNX45HJC', N'bab446e5-f55c-478a-9cdf-c6b4656ed582', NULL, 0, 0, NULL, 1, 0)
INSERT [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'6dcdfdda-b3d4-45b0-a72b-c516354a0b31', N'Test99', N'TEST99', N'draiden750@villagepxt.com', N'DRAIDEN750@VILLAGEPXT.COM', 0, N'AQAAAAEAACcQAAAAEJDaDLyv3ilXWQ3jq43kJRUrrpoTyDWeFZdtwFy9G92VKQt6ITDVtOEaxEXQ9wE49g==', N'VYICQ2IKGCBYC227N5JTHIV5YPS5IAR6', N'f30bc33d-9b66-4773-aa70-113b9dfdddf1', NULL, 0, 0, NULL, 1, 0)
INSERT [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'7a6ced01-f1f1-4f69-b304-9aa30bafc289', N'NewCustomer99', N'NEWCUSTOMER99', N'kinhoe0414@gmail.com', N'KINHOE0414@GMAIL.COM', 1, N'AQAAAAEAACcQAAAAEGBrXRs36vdYgLe+cVSCcpIPVf6Fkjc07LxfpV6BM3KelkEtjFjbH6MT6ot2wrxp7g==', N'R4K5ZQZZGSNEQQSG6IK7RCBAIKOWT6XS', N'0eba7841-72ce-4ee3-b560-51a8b352d495', NULL, 0, 0, NULL, 1, 0)
INSERT [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'85048e75-2c36-469a-9d5a-3eb5c45e5ddc', N'NewAdmin99', N'NEWADMIN99', N'porotomato@gmail.com', N'POROTOMATO@GMAIL.COM', 1, N'AQAAAAEAACcQAAAAEGO4J4KMBxpT6xt55mCPMJmNr8XimBdJG4T2eIeR7+XLCGBInrza232qGo/PR8jjTg==', N'4X5DI4JLEWGZSZGBMCAHLCXTFMQMIPTL', N'423211fe-2eaa-40eb-ab45-09c3dddf51af', NULL, 0, 0, NULL, 1, 0)
INSERT [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'b311d7e8-08b6-41f4-89e4-739165915f84', N'NormarlUser1', N'NORMARLUSER1', N'yimaja1297@troikos.com', N'YIMAJA1297@TROIKOS.COM', 1, N'AQAAAAEAACcQAAAAEKO0ZBaLhWJ50wbb0DoukOSvaFTroP9lEkpXPUmeH+l65WDCwOiVmjCgZLuxuZe12w==', N'ZGVA2FGMQIUSRBBU2D3BEQVCAWGJBFE4', N'd551e32e-568b-4f46-a4d0-5c0badb89184', NULL, 0, 0, NULL, 1, 0)
INSERT [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'b743512b-1c8f-40a3-953a-59219b8dc9a6', N'KinHoe99', N'KINHOE99', N'yapkinhoe1@gmail.com', N'YAPKINHOE1@GMAIL.COM', 1, N'AQAAAAEAACcQAAAAEJ/+wRp9SRqF5HRI4K0ZigAoH109zSIcWSUkZk/KuPHv9iQ4r4psgu+MGLuX94MyEg==', N'A6Y5YEAAIEUOMP2RT57QTMGO67H4HHY4', N'066df6b6-2a79-4e5f-95a6-a5fc352e12d3', NULL, 0, 0, NULL, 1, 0)
INSERT [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'c53afa8f-7f5b-4ed3-badb-f63112e00ee9', N'NormalUser2', N'NORMALUSER2', N'hotraderda@biyac.com', N'HOTRADERDA@BIYAC.COM', 1, N'AQAAAAEAACcQAAAAEMGZm/67h6cWyPYbuBDE4LivbBDE7pc7hxq/5y01KcSUiBJPWEqwiayw6fvlQaejwg==', N'Z4L2JYGTTQ3QWN5MY4CEIIDGDUFWFAII', N'69dc3124-2bf0-410a-a44a-966ddab55694', NULL, 0, 0, NULL, 1, 0)
SET IDENTITY_INSERT [dbo].[Booking] ON 

INSERT [dbo].[Booking] ([Id], [BookingDate], [StatuId], [UserId], [ReservedDate], [ReservedTime]) VALUES (78, CAST(N'2021-05-17 23:48:22.000' AS DateTime), 5, N'5d32d630-487b-4f01-9e32-b9b84f3b4ac1', CAST(N'2021-05-17' AS Date), CAST(N'10:00:00' AS Time))
INSERT [dbo].[Booking] ([Id], [BookingDate], [StatuId], [UserId], [ReservedDate], [ReservedTime]) VALUES (80, CAST(N'2021-05-18 00:01:15.000' AS DateTime), 5, N'5d32d630-487b-4f01-9e32-b9b84f3b4ac1', CAST(N'2021-05-17' AS Date), CAST(N'10:00:00' AS Time))
INSERT [dbo].[Booking] ([Id], [BookingDate], [StatuId], [UserId], [ReservedDate], [ReservedTime]) VALUES (81, CAST(N'2021-05-18 00:05:48.000' AS DateTime), 3, N'5d32d630-487b-4f01-9e32-b9b84f3b4ac1', CAST(N'2021-05-18' AS Date), CAST(N'10:00:00' AS Time))
INSERT [dbo].[Booking] ([Id], [BookingDate], [StatuId], [UserId], [ReservedDate], [ReservedTime]) VALUES (82, CAST(N'2021-05-18 00:22:29.090' AS DateTime), 3, N'5d32d630-487b-4f01-9e32-b9b84f3b4ac1', CAST(N'2021-05-22' AS Date), CAST(N'10:00:00' AS Time))
INSERT [dbo].[Booking] ([Id], [BookingDate], [StatuId], [UserId], [ReservedDate], [ReservedTime]) VALUES (83, CAST(N'2021-05-18 13:57:57.237' AS DateTime), 3, N'b311d7e8-08b6-41f4-89e4-739165915f84', CAST(N'2021-05-22' AS Date), CAST(N'10:00:00' AS Time))
INSERT [dbo].[Booking] ([Id], [BookingDate], [StatuId], [UserId], [ReservedDate], [ReservedTime]) VALUES (84, CAST(N'2021-05-18 14:02:35.973' AS DateTime), 3, N'c53afa8f-7f5b-4ed3-badb-f63112e00ee9', CAST(N'2021-05-22' AS Date), CAST(N'10:00:00' AS Time))
INSERT [dbo].[Booking] ([Id], [BookingDate], [StatuId], [UserId], [ReservedDate], [ReservedTime]) VALUES (85, CAST(N'2021-05-18 14:05:39.770' AS DateTime), 3, N'666a3693-8f7b-43b4-a41e-d674d68230a6', CAST(N'2021-05-22' AS Date), CAST(N'10:00:00' AS Time))
INSERT [dbo].[Booking] ([Id], [BookingDate], [StatuId], [UserId], [ReservedDate], [ReservedTime]) VALUES (86, CAST(N'2021-05-18 14:08:48.850' AS DateTime), 3, N'3bdab846-0612-4a3a-bcd7-67e1bf11d2ac', CAST(N'2021-05-22' AS Date), CAST(N'10:00:00' AS Time))
INSERT [dbo].[Booking] ([Id], [BookingDate], [StatuId], [UserId], [ReservedDate], [ReservedTime]) VALUES (87, CAST(N'2021-05-31 15:45:44.620' AS DateTime), 1, N'00e15181-cbf9-4cde-af17-6be097158f8c', CAST(N'2021-05-31' AS Date), CAST(N'20:00:00' AS Time))
INSERT [dbo].[Booking] ([Id], [BookingDate], [StatuId], [UserId], [ReservedDate], [ReservedTime]) VALUES (88, CAST(N'2021-05-31 15:47:51.640' AS DateTime), 1, N'00e15181-cbf9-4cde-af17-6be097158f8c', CAST(N'2021-05-31' AS Date), CAST(N'21:00:00' AS Time))
INSERT [dbo].[Booking] ([Id], [BookingDate], [StatuId], [UserId], [ReservedDate], [ReservedTime]) VALUES (89, CAST(N'2021-05-31 15:56:42.000' AS DateTime), 3, N'5f608703-d9e4-478c-b832-a58a75616874', CAST(N'2021-05-31' AS Date), CAST(N'06:00:00' AS Time))
INSERT [dbo].[Booking] ([Id], [BookingDate], [StatuId], [UserId], [ReservedDate], [ReservedTime]) VALUES (90, CAST(N'2021-05-31 16:03:53.143' AS DateTime), 1, N'5f608703-d9e4-478c-b832-a58a75616874', CAST(N'2021-06-01' AS Date), CAST(N'22:00:00' AS Time))
SET IDENTITY_INSERT [dbo].[Booking] OFF
SET IDENTITY_INSERT [dbo].[BookingDetails] ON 

INSERT [dbo].[BookingDetails] ([Id], [RestaurantId], [BookingId]) VALUES (69, 4, 78)
INSERT [dbo].[BookingDetails] ([Id], [RestaurantId], [BookingId]) VALUES (71, 4, 80)
INSERT [dbo].[BookingDetails] ([Id], [RestaurantId], [BookingId]) VALUES (72, 4, 81)
INSERT [dbo].[BookingDetails] ([Id], [RestaurantId], [BookingId]) VALUES (73, 4, 82)
INSERT [dbo].[BookingDetails] ([Id], [RestaurantId], [BookingId]) VALUES (74, 4, 83)
INSERT [dbo].[BookingDetails] ([Id], [RestaurantId], [BookingId]) VALUES (75, 4, 84)
INSERT [dbo].[BookingDetails] ([Id], [RestaurantId], [BookingId]) VALUES (76, 4, 85)
INSERT [dbo].[BookingDetails] ([Id], [RestaurantId], [BookingId]) VALUES (77, 4, 86)
INSERT [dbo].[BookingDetails] ([Id], [RestaurantId], [BookingId]) VALUES (78, 4, 87)
INSERT [dbo].[BookingDetails] ([Id], [RestaurantId], [BookingId]) VALUES (79, 4, 88)
INSERT [dbo].[BookingDetails] ([Id], [RestaurantId], [BookingId]) VALUES (80, 4, 89)
INSERT [dbo].[BookingDetails] ([Id], [RestaurantId], [BookingId]) VALUES (81, 4, 90)
SET IDENTITY_INSERT [dbo].[BookingDetails] OFF
SET IDENTITY_INSERT [dbo].[NewRestaurantForm] ON 

INSERT [dbo].[NewRestaurantForm] ([Id], [RestaurantName], [Description], [Approval], [UserId], [BuildingNo], [Address1], [Address2], [City], [State], [PostCode], [Country]) VALUES (6, N'JosaxUser99 Restaurant', N'Sells beer and roast bbq', 1, N'532b1ec0-b12d-4dd5-a76c-30269d82c675', 12345, N'new address 999', N'jalan address', N'Johor Bahru', N'Johor Bahru', N'53530', N'Australia')
INSERT [dbo].[NewRestaurantForm] ([Id], [RestaurantName], [Description], [Approval], [UserId], [BuildingNo], [Address1], [Address2], [City], [State], [PostCode], [Country]) VALUES (7, N'NormalUser6 Restaurant ', N'Testing User 6 Restaurant', 1, N'5f608703-d9e4-478c-b832-a58a75616874', 6, N'KL', N'KL', N'KL', N'KL', N'53100', N'Malaysia')
SET IDENTITY_INSERT [dbo].[NewRestaurantForm] OFF
SET IDENTITY_INSERT [dbo].[Restaurant] ON 

INSERT [dbo].[Restaurant] ([Id], [Name], [Description]) VALUES (3, N'Choo Choo Choo', N'Change from UI again')
INSERT [dbo].[Restaurant] ([Id], [Name], [Description]) VALUES (4, N'Mcd Cafe', N'Prepare fast food, delicious dessert and Coffee')
INSERT [dbo].[Restaurant] ([Id], [Name], [Description]) VALUES (11, N'JosaxUser99 Restaurant', N'Sells beer and roast bbq')
INSERT [dbo].[Restaurant] ([Id], [Name], [Description]) VALUES (12, N'NormalUser6 Restaurant ', N'Testing User 6 Restaurant')
SET IDENTITY_INSERT [dbo].[Restaurant] OFF
SET IDENTITY_INSERT [dbo].[Status] ON 

INSERT [dbo].[Status] ([Id], [Status]) VALUES (1, N'Processing')
INSERT [dbo].[Status] ([Id], [Status]) VALUES (2, N'Booked')
INSERT [dbo].[Status] ([Id], [Status]) VALUES (3, N'Completed')
INSERT [dbo].[Status] ([Id], [Status]) VALUES (4, N'Cancelled')
INSERT [dbo].[Status] ([Id], [Status]) VALUES (5, N'Expired')
SET IDENTITY_INSERT [dbo].[Status] OFF
SET IDENTITY_INSERT [dbo].[Table] ON 

INSERT [dbo].[Table] ([Id], [TableNum], [RestaurantId]) VALUES (1, 99, 3)
INSERT [dbo].[Table] ([Id], [TableNum], [RestaurantId]) VALUES (2, 2, 3)
INSERT [dbo].[Table] ([Id], [TableNum], [RestaurantId]) VALUES (3, 3, 3)
INSERT [dbo].[Table] ([Id], [TableNum], [RestaurantId]) VALUES (4, 4, 3)
INSERT [dbo].[Table] ([Id], [TableNum], [RestaurantId]) VALUES (5, 5, 3)
INSERT [dbo].[Table] ([Id], [TableNum], [RestaurantId]) VALUES (6, 1, 4)
INSERT [dbo].[Table] ([Id], [TableNum], [RestaurantId]) VALUES (7, 2, 4)
INSERT [dbo].[Table] ([Id], [TableNum], [RestaurantId]) VALUES (8, 3, 4)
INSERT [dbo].[Table] ([Id], [TableNum], [RestaurantId]) VALUES (9, 4, 4)
INSERT [dbo].[Table] ([Id], [TableNum], [RestaurantId]) VALUES (10, 5, 4)
INSERT [dbo].[Table] ([Id], [TableNum], [RestaurantId]) VALUES (11, 6, 4)
INSERT [dbo].[Table] ([Id], [TableNum], [RestaurantId]) VALUES (12, 7, 4)
INSERT [dbo].[Table] ([Id], [TableNum], [RestaurantId]) VALUES (13, 8, 4)
INSERT [dbo].[Table] ([Id], [TableNum], [RestaurantId]) VALUES (14, 9, 4)
INSERT [dbo].[Table] ([Id], [TableNum], [RestaurantId]) VALUES (15, 10, 4)
INSERT [dbo].[Table] ([Id], [TableNum], [RestaurantId]) VALUES (16, 6, 3)
SET IDENTITY_INSERT [dbo].[Table] OFF
SET IDENTITY_INSERT [dbo].[TimeSlot] ON 

INSERT [dbo].[TimeSlot] ([Id], [AvailableTime], [TimeStatusId], [RestaurantId], [Vacancy]) VALUES (1, CAST(N'10:00:00' AS Time), 0, 3, 11)
INSERT [dbo].[TimeSlot] ([Id], [AvailableTime], [TimeStatusId], [RestaurantId], [Vacancy]) VALUES (7, CAST(N'12:00:00' AS Time), 0, 3, 10)
INSERT [dbo].[TimeSlot] ([Id], [AvailableTime], [TimeStatusId], [RestaurantId], [Vacancy]) VALUES (8, CAST(N'02:00:00' AS Time), 0, 3, 10)
INSERT [dbo].[TimeSlot] ([Id], [AvailableTime], [TimeStatusId], [RestaurantId], [Vacancy]) VALUES (9, CAST(N'04:00:00' AS Time), 0, 3, 10)
INSERT [dbo].[TimeSlot] ([Id], [AvailableTime], [TimeStatusId], [RestaurantId], [Vacancy]) VALUES (10, CAST(N'06:00:00' AS Time), 0, 3, 10)
INSERT [dbo].[TimeSlot] ([Id], [AvailableTime], [TimeStatusId], [RestaurantId], [Vacancy]) VALUES (11, CAST(N'08:00:00' AS Time), 0, 3, 10)
INSERT [dbo].[TimeSlot] ([Id], [AvailableTime], [TimeStatusId], [RestaurantId], [Vacancy]) VALUES (12, CAST(N'10:00:00' AS Time), 0, 4, 5)
INSERT [dbo].[TimeSlot] ([Id], [AvailableTime], [TimeStatusId], [RestaurantId], [Vacancy]) VALUES (13, CAST(N'12:00:00' AS Time), 0, 4, 5)
INSERT [dbo].[TimeSlot] ([Id], [AvailableTime], [TimeStatusId], [RestaurantId], [Vacancy]) VALUES (14, CAST(N'02:00:00' AS Time), 0, 4, 5)
INSERT [dbo].[TimeSlot] ([Id], [AvailableTime], [TimeStatusId], [RestaurantId], [Vacancy]) VALUES (15, CAST(N'04:00:00' AS Time), 0, 4, 5)
INSERT [dbo].[TimeSlot] ([Id], [AvailableTime], [TimeStatusId], [RestaurantId], [Vacancy]) VALUES (16, CAST(N'06:00:00' AS Time), 0, 4, 5)
INSERT [dbo].[TimeSlot] ([Id], [AvailableTime], [TimeStatusId], [RestaurantId], [Vacancy]) VALUES (17, CAST(N'08:00:00' AS Time), 0, 4, 5)
INSERT [dbo].[TimeSlot] ([Id], [AvailableTime], [TimeStatusId], [RestaurantId], [Vacancy]) VALUES (19, CAST(N'10:00:00' AS Time), 0, 11, 10)
INSERT [dbo].[TimeSlot] ([Id], [AvailableTime], [TimeStatusId], [RestaurantId], [Vacancy]) VALUES (20, CAST(N'16:00:00' AS Time), 0, 4, 5)
INSERT [dbo].[TimeSlot] ([Id], [AvailableTime], [TimeStatusId], [RestaurantId], [Vacancy]) VALUES (21, CAST(N'18:00:00' AS Time), 0, 4, 5)
INSERT [dbo].[TimeSlot] ([Id], [AvailableTime], [TimeStatusId], [RestaurantId], [Vacancy]) VALUES (22, CAST(N'20:00:00' AS Time), 0, 4, 5)
INSERT [dbo].[TimeSlot] ([Id], [AvailableTime], [TimeStatusId], [RestaurantId], [Vacancy]) VALUES (23, CAST(N'21:00:00' AS Time), 0, 4, 5)
INSERT [dbo].[TimeSlot] ([Id], [AvailableTime], [TimeStatusId], [RestaurantId], [Vacancy]) VALUES (24, CAST(N'22:00:00' AS Time), 0, 4, 5)
SET IDENTITY_INSERT [dbo].[TimeSlot] OFF
SET IDENTITY_INSERT [dbo].[TimeStatus] ON 

INSERT [dbo].[TimeStatus] ([Id], [TimeStatus]) VALUES (0, N'Available')
INSERT [dbo].[TimeStatus] ([Id], [TimeStatus]) VALUES (1, N'Fully Booked')
SET IDENTITY_INSERT [dbo].[TimeStatus] OFF
INSERT [dbo].[UserProfile] ([Id], [FirstName], [LastName], [DateJoined], [RestaurantId]) VALUES (N'5179d5fc-c062-401d-ba74-3ebbf508bc24', N'Choo', N'Changed', CAST(N'0001-01-01' AS Date), 3)
INSERT [dbo].[UserProfile] ([Id], [FirstName], [LastName], [DateJoined], [RestaurantId]) VALUES (N'532b1ec0-b12d-4dd5-a76c-30269d82c675', N'Test', N'User', CAST(N'0001-01-01' AS Date), 11)
INSERT [dbo].[UserProfile] ([Id], [FirstName], [LastName], [DateJoined], [RestaurantId]) VALUES (N'5f608703-d9e4-478c-b832-a58a75616874', N'', N'', CAST(N'2021-05-31' AS Date), 12)
INSERT [dbo].[UserProfile] ([Id], [FirstName], [LastName], [DateJoined], [RestaurantId]) VALUES (N'6dcdfdda-b3d4-45b0-a72b-c516354a0b31', N'Test', N'Poro', CAST(N'2021-05-05' AS Date), 3)
INSERT [dbo].[UserProfile] ([Id], [FirstName], [LastName], [DateJoined], [RestaurantId]) VALUES (N'85048e75-2c36-469a-9d5a-3eb5c45e5ddc', N'New', N'Admin', CAST(N'2021-05-04' AS Date), 4)
INSERT [dbo].[UserProfile] ([Id], [FirstName], [LastName], [DateJoined], [RestaurantId]) VALUES (N'b743512b-1c8f-40a3-953a-59219b8dc9a6', N'Yap', N'Kin Hoe', CAST(N'2021-05-04' AS Date), 3)
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_AspNetRoleClaims_RoleId]    Script Date: 5/31/2021 9:25:28 PM ******/
CREATE NONCLUSTERED INDEX [IX_AspNetRoleClaims_RoleId] ON [dbo].[AspNetRoleClaims]
(
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [RoleNameIndex]    Script Date: 5/31/2021 9:25:28 PM ******/
CREATE UNIQUE NONCLUSTERED INDEX [RoleNameIndex] ON [dbo].[AspNetRoles]
(
	[NormalizedName] ASC
)
WHERE ([NormalizedName] IS NOT NULL)
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_AspNetUserClaims_UserId]    Script Date: 5/31/2021 9:25:28 PM ******/
CREATE NONCLUSTERED INDEX [IX_AspNetUserClaims_UserId] ON [dbo].[AspNetUserClaims]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_AspNetUserLogins_UserId]    Script Date: 5/31/2021 9:25:28 PM ******/
CREATE NONCLUSTERED INDEX [IX_AspNetUserLogins_UserId] ON [dbo].[AspNetUserLogins]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_AspNetUserRoles_RoleId]    Script Date: 5/31/2021 9:25:28 PM ******/
CREATE NONCLUSTERED INDEX [IX_AspNetUserRoles_RoleId] ON [dbo].[AspNetUserRoles]
(
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [EmailIndex]    Script Date: 5/31/2021 9:25:28 PM ******/
CREATE NONCLUSTERED INDEX [EmailIndex] ON [dbo].[AspNetUsers]
(
	[NormalizedEmail] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [UserNameIndex]    Script Date: 5/31/2021 9:25:28 PM ******/
CREATE UNIQUE NONCLUSTERED INDEX [UserNameIndex] ON [dbo].[AspNetUsers]
(
	[NormalizedUserName] ASC
)
WHERE ([NormalizedUserName] IS NOT NULL)
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Address]  WITH CHECK ADD  CONSTRAINT [FK_Address_Restaurant] FOREIGN KEY([RestaurantId])
REFERENCES [dbo].[Restaurant] ([Id])
GO
ALTER TABLE [dbo].[Address] CHECK CONSTRAINT [FK_Address_Restaurant]
GO
ALTER TABLE [dbo].[AspNetRoleClaims]  WITH CHECK ADD  CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[AspNetRoles] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetRoleClaims] CHECK CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId]
GO
ALTER TABLE [dbo].[AspNetUserClaims]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserClaims] CHECK CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserLogins]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserLogins] CHECK CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[AspNetRoles] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId]
GO
ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserTokens]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserTokens_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserTokens] CHECK CONSTRAINT [FK_AspNetUserTokens_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[Booking]  WITH CHECK ADD  CONSTRAINT [FK_Booking_AspNetUsers] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
GO
ALTER TABLE [dbo].[Booking] CHECK CONSTRAINT [FK_Booking_AspNetUsers]
GO
ALTER TABLE [dbo].[Booking]  WITH CHECK ADD  CONSTRAINT [FK_Booking_Status] FOREIGN KEY([StatuId])
REFERENCES [dbo].[Status] ([Id])
GO
ALTER TABLE [dbo].[Booking] CHECK CONSTRAINT [FK_Booking_Status]
GO
ALTER TABLE [dbo].[BookingDetails]  WITH CHECK ADD  CONSTRAINT [FK_BookingDetails_Booking] FOREIGN KEY([BookingId])
REFERENCES [dbo].[Booking] ([Id])
GO
ALTER TABLE [dbo].[BookingDetails] CHECK CONSTRAINT [FK_BookingDetails_Booking]
GO
ALTER TABLE [dbo].[BookingDetails]  WITH CHECK ADD  CONSTRAINT [FK_BookingDetails_Restaurant] FOREIGN KEY([RestaurantId])
REFERENCES [dbo].[Restaurant] ([Id])
GO
ALTER TABLE [dbo].[BookingDetails] CHECK CONSTRAINT [FK_BookingDetails_Restaurant]
GO
ALTER TABLE [dbo].[Table]  WITH CHECK ADD  CONSTRAINT [FK_Table_Restaurant] FOREIGN KEY([RestaurantId])
REFERENCES [dbo].[Restaurant] ([Id])
GO
ALTER TABLE [dbo].[Table] CHECK CONSTRAINT [FK_Table_Restaurant]
GO
ALTER TABLE [dbo].[TimeSlot]  WITH CHECK ADD  CONSTRAINT [FK_TimeSlot_Restaurant] FOREIGN KEY([RestaurantId])
REFERENCES [dbo].[Restaurant] ([Id])
GO
ALTER TABLE [dbo].[TimeSlot] CHECK CONSTRAINT [FK_TimeSlot_Restaurant]
GO
ALTER TABLE [dbo].[TimeSlot]  WITH CHECK ADD  CONSTRAINT [FK_TimeSlot_TimeStatus1] FOREIGN KEY([TimeStatusId])
REFERENCES [dbo].[TimeStatus] ([Id])
GO
ALTER TABLE [dbo].[TimeSlot] CHECK CONSTRAINT [FK_TimeSlot_TimeStatus1]
GO
ALTER TABLE [dbo].[UserProfile]  WITH CHECK ADD  CONSTRAINT [FK_UserProfile_AspNetUsers] FOREIGN KEY([Id])
REFERENCES [dbo].[AspNetUsers] ([Id])
GO
ALTER TABLE [dbo].[UserProfile] CHECK CONSTRAINT [FK_UserProfile_AspNetUsers]
GO
ALTER TABLE [dbo].[UserProfile]  WITH CHECK ADD  CONSTRAINT [FK_UserProfile_Restaurant] FOREIGN KEY([RestaurantId])
REFERENCES [dbo].[Restaurant] ([Id])
GO
ALTER TABLE [dbo].[UserProfile] CHECK CONSTRAINT [FK_UserProfile_Restaurant]
GO
USE [master]
GO
ALTER DATABASE [RestaurantBookingDb] SET  READ_WRITE 
GO
