USE [jobvieclam]
GO
/****** Object:  UserDefinedFunction [dbo].[getAllJobOfCampagn]    Script Date: 12/10/2024 11:56:39 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE  FUNCTION [dbo].[getAllJobOfCampagn]
(
@campagnId  int = -1

)
RETURNS   @temp table ( LineCode  varchar(10))
AS


begin
   
	if( @campagnId < 1)
	begin 
		return;
	end
	
	insert into @temp  
	select  jt.Id  from  jobItems jt  where jt.Campagn =@campagnId 
	
  	return;

end 



GO
/****** Object:  UserDefinedFunction [dbo].[getAllJobOfHuman]    Script Date: 12/10/2024 11:56:39 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE  FUNCTION [dbo].[getAllJobOfHuman]
(
@userId int = -1

)
RETURNS   @temp table ( LineCode  varchar(10))
AS


begin
	insert into @temp  
	select  jt.Id  from  jobItems jt  where jt.Campagn  in  (select id from Campaign cp where cp.RelId =@userId 

	)
	
  	return;

end 



GO
/****** Object:  UserDefinedFunction [dbo].[getFullNameCampagn]    Script Date: 12/10/2024 11:56:39 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create FUNCTION [dbo].[getFullNameCampagn]
(
@id  int null

)
RETURNS nvarchar(100)
AS
BEGIN
	-- Declare the return variable here
	DECLARE @name nvarchar(100)
	-- Add the T-SQL statements to compute the return value here
	select top 1  @name = [Name]  from Campaign 
	where id  = @id 
	-- Return the result of the function
	RETURN @name

END
GO
/****** Object:  UserDefinedFunction [dbo].[getFullNameCategory]    Script Date: 12/10/2024 11:56:39 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE FUNCTION [dbo].[getFullNameCategory]
(
@id  varchar(5) null,
@CategoryId   int  =-1

)
RETURNS nvarchar(100)
AS
BEGIN

   

	if(@id = '2,8')
	begin 
		set @id =  CAST( @CategoryId as varchar(5))
	end 
	-- Declare the return variable here
	DECLARE @name nvarchar(100)
	-- Add the T-SQL statements to compute the return value here
	select top 1  @name = Title  from CategoryArticle 
	where id  = cast(@id as int)
	-- Return the result of the function
	RETURN @name

END
GO
/****** Object:  UserDefinedFunction [dbo].[getFullNameJob]    Script Date: 12/10/2024 11:56:39 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create FUNCTION [dbo].[getFullNameJob]
(
	@id  int null

)
RETURNS nvarchar(100)
AS
BEGIN
	
	DECLARE @name nvarchar(100)

	select top 1  @name =   [Name]  from  jobItems 
	where id  = @id 
	
	RETURN @name

END
GO
/****** Object:  UserDefinedFunction [dbo].[getNameMaster]    Script Date: 12/10/2024 11:56:39 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create FUNCTION [dbo].[getNameMaster]
(
	@code  int null

)
RETURNS nvarchar(100)
AS
BEGIN
	
	DECLARE @name nvarchar(100)

	select top 1  @name =  [Text]    from  MasterData 
	where id  = @code 
	
	RETURN @name

END
GO
/****** Object:  Table [dbo].[ActiveCodeRecruiter]    Script Date: 12/10/2024 11:56:39 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ActiveCodeRecruiter](
	[UserId] [int] NULL,
	[Code] [nvarchar](30) NULL,
	[Email] [varchar](30) NULL,
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Deleted] [bit] NULL,
	[Status] [int] NULL,
	[CreateAt] [datetime] NULL,
	[CreatedBy] [int] NULL,
	[UpdateAt] [datetime] NULL,
	[UpdatedBy] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Articles]    Script Date: 12/10/2024 11:56:39 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Articles](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Deleted] [bit] NULL,
	[Status] [int] NULL,
	[CreateAt] [datetime] NULL,
	[CreatedBy] [int] NULL,
	[UpdateAt] [datetime] NULL,
	[UpdatedBy] [int] NULL,
	[Title] [nvarchar](100) NULL,
	[CoverImage] [nvarchar](500) NULL,
	[KeyWord] [nvarchar](200) NULL,
	[Content] [ntext] NULL,
	[ShortDes] [nvarchar](500) NULL,
	[linked] [varchar](5) NULL,
	[slug] [varchar](100) NULL,
	[AuthorPost] [nvarchar](100) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AuthenUser]    Script Date: 12/10/2024 11:56:39 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AuthenUser](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[userName] [varchar](30) NULL,
	[typeUser] [int] NULL,
	[pass] [varchar](30) NULL,
	[userId] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BusinessLicense]    Script Date: 12/10/2024 11:56:39 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BusinessLicense](
	[Email] [varchar](30) NULL,
	[RelId] [int] NULL,
	[linkFile] [varchar](500) NULL,
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Deleted] [bit] NULL,
	[Status] [int] NULL,
	[CreateAt] [datetime] NULL,
	[CreatedBy] [int] NULL,
	[UpdateAt] [datetime] NULL,
	[UpdatedBy] [int] NULL,
	[Note] [nvarchar](300) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BusinessLicenseLog]    Script Date: 12/10/2024 11:56:39 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BusinessLicenseLog](
	[Email] [varchar](30) NULL,
	[RelId] [int] NULL,
	[Noted] [nvarchar](300) NULL,
	[ExtraInfo] [nvarchar](200) NULL,
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Deleted] [bit] NULL,
	[Status] [int] NULL,
	[CreateAt] [datetime] NULL,
	[CreatedBy] [int] NULL,
	[UpdateAt] [datetime] NULL,
	[UpdatedBy] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Campaign]    Script Date: 12/10/2024 11:56:39 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Campaign](
	[Name] [nvarchar](200) NULL,
	[from] [datetime] NULL,
	[to] [datetime] NULL,
	[Email] [varchar](30) NULL,
	[RelId] [int] NULL,
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Deleted] [bit] NULL,
	[Status] [int] NULL,
	[CreateAt] [datetime] NULL,
	[CreatedBy] [int] NULL,
	[UpdateAt] [datetime] NULL,
	[UpdatedBy] [int] NULL,
	[businessDate] [nvarchar](30) NULL,
	[Package] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Candidate]    Script Date: 12/10/2024 11:56:39 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Candidate](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Deleted] [bit] NULL,
	[Status] [int] NULL,
	[CreateAt] [datetime] NULL,
	[CreatedBy] [int] NULL,
	[UpdateAt] [datetime] NULL,
	[UpdatedBy] [int] NULL,
	[Phone] [varchar](15) NULL,
	[FirstName] [nvarchar](30) NULL,
	[FullName] [nvarchar](30) NULL,
	[Email] [varchar](30) NULL,
	[UserName] [varchar](30) NULL,
	[Password] [varchar](30) NULL,
	[DateRegister] [datetime] NULL,
	[sourceData] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CandidateInfo]    Script Date: 12/10/2024 11:56:39 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CandidateInfo](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Status] [int] NULL,
	[userId] [int] NULL,
	[email] [varchar](30) NULL,
	[privateMode] [bit] NULL,
	[publicMode] [bit] NULL,
	[CreatedBy] [int] NULL,
	[UpdateAt] [datetime] NULL,
	[UpdatedBy] [int] NULL,
	[Deleted] [bit] NULL,
	[CreateAt] [datetime] NULL,
	[AvatarLink] [varchar](500) NULL,
	[sourceData] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CategoryArticle]    Script Date: 12/10/2024 11:56:39 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CategoryArticle](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Deleted] [bit] NULL,
	[Status] [int] NULL,
	[CreateAt] [datetime] NULL,
	[CreatedBy] [int] NULL,
	[UpdateAt] [datetime] NULL,
	[UpdatedBy] [int] NULL,
	[Title] [nvarchar](100) NULL,
	[CoverImage] [nvarchar](500) NULL,
	[KeyWord] [nvarchar](200) NULL,
	[ShortDes] [nvarchar](500) NULL,
	[slug] [varchar](30) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CertifyUser]    Script Date: 12/10/2024 11:56:39 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CertifyUser](
	[TypeData] [int] NULL,
	[FullName] [nvarchar](100) NULL,
	[CompanyName] [nvarchar](60) NULL,
	[MonthGet] [int] NULL,
	[YearGet] [int] NULL,
	[introduction] [nvarchar](1000) NULL,
	[linkFile] [varchar](1000) NULL,
	[RelId] [int] NULL,
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Deleted] [bit] NULL,
	[Status] [int] NULL,
	[CreateAt] [datetime] NULL,
	[CreatedBy] [int] NULL,
	[UpdateAt] [datetime] NULL,
	[UpdatedBy] [int] NULL,
	[MonthExpired] [varchar](4) NULL,
	[YearExpired] [varchar](4) NULL,
	[IsExpired] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CompanyFavorite]    Script Date: 12/10/2024 11:56:39 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CompanyFavorite](
	[RelId] [int] NULL,
	[userId] [int] NULL,
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Deleted] [bit] NULL,
	[Status] [int] NULL,
	[CreateAt] [datetime] NULL,
	[CreatedBy] [int] NULL,
	[UpdateAt] [datetime] NULL,
	[UpdatedBy] [int] NULL,
	[ExtraText] [nvarchar](100) NULL,
	[ExtraLink] [varchar](100) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CompanyFollow]    Script Date: 12/10/2024 11:56:39 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CompanyFollow](
	[RelId] [int] NULL,
	[userId] [int] NULL,
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Deleted] [bit] NULL,
	[Status] [int] NULL,
	[CreateAt] [datetime] NULL,
	[CreatedBy] [int] NULL,
	[UpdateAt] [datetime] NULL,
	[UpdatedBy] [int] NULL,
	[ExtraText] [nvarchar](100) NULL,
	[ExtraLink] [varchar](100) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CompanyInfo]    Script Date: 12/10/2024 11:56:39 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CompanyInfo](
	[TaxCode] [varchar](12) NULL,
	[Website] [varchar](500) NULL,
	[Capacity] [varchar](100) NULL,
	[Email] [varchar](30) NULL,
	[RelId] [int] NULL,
	[FullName] [nvarchar](300) NULL,
	[Field] [varchar](20) NULL,
	[AddressInfo] [nvarchar](3000) NULL,
	[Phone] [varchar](12) NULL,
	[shortDes] [nvarchar](500) NULL,
	[LogoLink] [varchar](500) NULL,
	[CoverLink] [varchar](500) NULL,
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Deleted] [bit] NULL,
	[Status] [int] NULL,
	[CreateAt] [datetime] NULL,
	[CreatedBy] [int] NULL,
	[UpdateAt] [datetime] NULL,
	[UpdatedBy] [int] NULL,
	[EmailCompany] [varchar](30) NULL,
	[slug] [varchar](100) NULL,
	[MapInfo] [varchar](1000) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[educationUser]    Script Date: 12/10/2024 11:56:39 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[educationUser](
	[SchoolName] [nvarchar](100) NULL,
	[major] [nvarchar](60) NULL,
	[position] [nvarchar](50) NULL,
	[fromMonth] [int] NULL,
	[fromYear] [int] NULL,
	[toMonth] [int] NULL,
	[toYear] [int] NULL,
	[isEnd] [bit] NULL,
	[introduction] [nvarchar](1000) NULL,
	[linkFile] [varchar](1000) NULL,
	[RelId] [int] NULL,
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Deleted] [bit] NULL,
	[Status] [int] NULL,
	[CreateAt] [datetime] NULL,
	[CreatedBy] [int] NULL,
	[UpdateAt] [datetime] NULL,
	[UpdatedBy] [int] NULL,
	[Rank] [nvarchar](20) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[experienceUser]    Script Date: 12/10/2024 11:56:39 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[experienceUser](
	[CompanyName] [nvarchar](100) NULL,
	[position] [nvarchar](50) NULL,
	[fromMonth] [int] NULL,
	[fromYear] [int] NULL,
	[toMonth] [int] NULL,
	[toYear] [int] NULL,
	[isEnd] [bit] NULL,
	[introduction] [nvarchar](1000) NULL,
	[linkFile] [varchar](1000) NULL,
	[RelId] [int] NULL,
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Deleted] [bit] NULL,
	[Status] [int] NULL,
	[CreateAt] [datetime] NULL,
	[CreatedBy] [int] NULL,
	[UpdateAt] [datetime] NULL,
	[UpdatedBy] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ForgetPassword]    Script Date: 12/10/2024 11:56:39 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ForgetPassword](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Deleted] [bit] NULL,
	[Status] [int] NULL,
	[CreateAt] [datetime] NULL,
	[CreatedBy] [int] NULL,
	[UpdateAt] [datetime] NULL,
	[UpdatedBy] [int] NULL,
	[UserId] [int] NULL,
	[Code] [nvarchar](30) NULL,
	[TypeUser] [int] NULL,
	[sendMailStatus] [int] NULL,
	[Email] [varchar](30) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[jobApply]    Script Date: 12/10/2024 11:56:39 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[jobApply](
	[JobId] [varchar](50) NULL,
	[CVId] [nvarchar](100) NULL,
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Deleted] [bit] NULL,
	[Status] [int] NULL,
	[CreateAt] [datetime] NULL,
	[CreatedBy] [int] NULL,
	[UpdateAt] [datetime] NULL,
	[UpdatedBy] [int] NULL,
	[ViewMode] [int] NULL,
	[FullName] [nvarchar](100) NULL,
	[Email] [varchar](30) NULL,
	[Phone] [varchar](12) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[JobApplyStatus]    Script Date: 12/10/2024 11:56:39 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[JobApplyStatus](
	[RelId] [varchar](50) NULL,
	[Note] [nvarchar](500) NULL,
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Deleted] [bit] NULL,
	[Status] [int] NULL,
	[CreateAt] [datetime] NULL,
	[CreatedBy] [int] NULL,
	[UpdateAt] [datetime] NULL,
	[UpdatedBy] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[jobInfo]    Script Date: 12/10/2024 11:56:39 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[jobInfo](
	[profession] [nvarchar](30) NULL,
	[expired_date] [datetime] NULL,
	[type_of_work] [varchar](30) NULL,
	[rank] [int] NULL,
	[JobId] [int] NULL,
	[CapagnId] [int] NULL,
	[experience] [int] NULL,
	[locations] [varchar](max) NULL,
	[time_workings] [varchar](max) NULL,
	[aggrement] [bit] NULL,
	[salary_from] [int] NULL,
	[salary_to] [int] NULL,
	[type_money] [int] NULL,
	[gender] [int] NULL,
	[description] [nvarchar](max) NULL,
	[requirement] [nvarchar](max) NULL,
	[benefit] [nvarchar](max) NULL,
	[skill] [varchar](max) NULL,
	[fullName] [nvarchar](100) NULL,
	[phone] [varchar](12) NULL,
	[emails] [varchar](300) NULL,
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Deleted] [bit] NULL,
	[Status] [int] NULL,
	[CreateAt] [datetime] NULL,
	[CreatedBy] [int] NULL,
	[UpdateAt] [datetime] NULL,
	[UpdatedBy] [int] NULL,
	[Position] [nvarchar](400) NULL,
	[Name] [nvarchar](400) NULL,
	[quantity] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[jobItemDisplay]    Script Date: 12/10/2024 11:56:39 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[jobItemDisplay](
	[JobId] [int] NULL,
	[recurId] [int] NULL,
	[JobName] [nvarchar](500) NULL,
	[CompanyName] [nvarchar](500) NULL,
	[RangeSalary] [nvarchar](200) NULL,
	[salaryfrom] [varchar](30) NULL,
	[salaryTo] [int] NULL,
	[ExperienceText] [nvarchar](200) NULL,
	[LocationText] [nvarchar](200) NULL,
	[Hashtags] [nvarchar](200) NULL,
	[Slug] [nvarchar](500) NULL,
	[ProfessionText] [nvarchar](200) NULL,
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Deleted] [bit] NULL,
	[Status] [int] NULL,
	[CreateAt] [datetime] NULL,
	[CreatedBy] [int] NULL,
	[UpdateAt] [datetime] NULL,
	[UpdatedBy] [int] NULL,
	[dateExpried] [datetime] NULL,
	[locationSearch] [varchar](200) NULL,
	[typeOfWork] [nvarchar](200) NULL,
	[Rank] [nvarchar](30) NULL,
	[RankSearch] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[jobItems]    Script Date: 12/10/2024 11:56:39 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[jobItems](
	[Name] [nvarchar](300) NULL,
	[from] [datetime] NULL,
	[to] [datetime] NULL,
	[Campagn] [varchar](30) NULL,
	[Reason] [int] NULL,
	[RelId] [int] NULL,
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Deleted] [bit] NULL,
	[Status] [int] NULL,
	[CreateAt] [datetime] NULL,
	[CreatedBy] [int] NULL,
	[UpdateAt] [datetime] NULL,
	[UpdatedBy] [int] NULL,
	[Package] [int] NULL,
	[Slug] [varchar](100) NULL,
	[businessTime] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[JobLogView]    Script Date: 12/10/2024 11:56:39 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[JobLogView](
	[RelId] [varchar](50) NULL,
	[userId] [nvarchar](500) NULL,
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Deleted] [bit] NULL,
	[Status] [int] NULL,
	[CreateAt] [datetime] NULL,
	[CreatedBy] [int] NULL,
	[UpdateAt] [datetime] NULL,
	[UpdatedBy] [int] NULL,
	[ExtraText] [nvarchar](100) NULL,
	[ExtraLink] [varchar](100) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[JobOverViewCounter]    Script Date: 12/10/2024 11:56:39 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[JobOverViewCounter](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Deleted] [bit] NULL,
	[Status] [int] NULL,
	[CreateAt] [datetime] NULL,
	[CreatedBy] [int] NULL,
	[UpdateAt] [datetime] NULL,
	[UpdatedBy] [int] NULL,
	[dayReport] [datetime] NULL,
	[TotalViewer] [int] NULL,
	[TotalApply] [int] NULL,
	[JobId] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[jobSave]    Script Date: 12/10/2024 11:56:39 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[jobSave](
	[JobId] [varchar](50) NULL,
	[UserId] [int] NULL,
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Deleted] [bit] NULL,
	[Status] [int] NULL,
	[CreateAt] [datetime] NULL,
	[CreatedBy] [int] NULL,
	[UpdateAt] [datetime] NULL,
	[UpdatedBy] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[LogAction]    Script Date: 12/10/2024 11:56:39 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LogAction](
	[userId] [int] NULL,
	[content] [nvarchar](4000) NULL,
	[BusinessTime] [datetime] NULL,
	[typeData] [int] NULL,
	[actor] [int] NULL,
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Deleted] [bit] NULL,
	[Status] [int] NULL,
	[CreateAt] [datetime] NULL,
	[CreatedBy] [int] NULL,
	[UpdateAt] [datetime] NULL,
	[UpdatedBy] [int] NULL,
	[source] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[mailInfo]    Script Date: 12/10/2024 11:56:39 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[mailInfo](
	[MailTo] [varchar](50) NULL,
	[Subject] [nvarchar](100) NULL,
	[Content] [nvarchar](max) NULL,
	[TypeContent] [int] NULL,
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Deleted] [bit] NULL,
	[Status] [int] NULL,
	[CreateAt] [datetime] NULL,
	[CreatedBy] [int] NULL,
	[UpdateAt] [datetime] NULL,
	[UpdatedBy] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MasterData]    Script Date: 12/10/2024 11:56:39 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MasterData](
	[Code] [varchar](4) NULL,
	[Text] [nvarchar](100) NULL,
	[TypeData] [int] NULL,
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Deleted] [bit] NULL,
	[Status] [int] NULL,
	[CreateAt] [datetime] NULL,
	[CreatedBy] [int] NULL,
	[UpdateAt] [datetime] NULL,
	[UpdatedBy] [int] NULL,
	[slug] [varchar](100) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Notification]    Script Date: 12/10/2024 11:56:39 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Notification](
	[userId] [nvarchar](500) NULL,
	[Title] [nvarchar](200) NULL,
	[RelId] [int] NULL,
	[UserName] [varchar](30) NULL,
	[LableText] [nvarchar](100) NULL,
	[TypeInfo] [int] NULL,
	[Content] [nvarchar](300) NULL,
	[LinkFile] [varchar](300) NULL,
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Deleted] [bit] NULL,
	[Status] [int] NULL,
	[CreateAt] [datetime] NULL,
	[CreatedBy] [int] NULL,
	[UpdateAt] [datetime] NULL,
	[UpdatedBy] [int] NULL,
	[Source] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[OtherProfileUser]    Script Date: 12/10/2024 11:56:39 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OtherProfileUser](
	[FullName] [nvarchar](100) NULL,
	[TypeData] [int] NULL,
	[Level] [nvarchar](60) NULL,
	[Description] [nvarchar](50) NULL,
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Deleted] [bit] NULL,
	[Status] [int] NULL,
	[CreateAt] [datetime] NULL,
	[CreatedBy] [int] NULL,
	[UpdateAt] [datetime] NULL,
	[UpdatedBy] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PageContent]    Script Date: 12/10/2024 11:56:39 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PageContent](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Deleted] [bit] NULL,
	[Status] [int] NULL,
	[CreateAt] [datetime] NULL,
	[CreatedBy] [int] NULL,
	[UpdateAt] [datetime] NULL,
	[UpdatedBy] [int] NULL,
	[TitlePage] [nvarchar](100) NULL,
	[Content] [ntext] NULL,
	[TypeData] [int] NULL,
	[Description] [nvarchar](500) NULL,
	[KeyWord] [nvarchar](200) NULL,
	[Image] [nvarchar](500) NULL,
	[slug] [varchar](100) NULL,
	[AuthorPost] [nvarchar](100) NULL,
	[source] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ProfileCV]    Script Date: 12/10/2024 11:56:39 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProfileCV](
	[FullName] [nvarchar](100) NULL,
	[position] [nvarchar](100) NULL,
	[level] [nvarchar](30) NULL,
	[gender] [int] NULL,
	[Email] [varchar](30) NULL,
	[phoneNumber] [varchar](15) NULL,
	[introduction] [nvarchar](1000) NULL,
	[avatarLink] [varchar](500) NULL,
	[RelId] [int] NULL,
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Deleted] [bit] NULL,
	[Status] [int] NULL,
	[CreateAt] [datetime] NULL,
	[CreatedBy] [int] NULL,
	[UpdateAt] [datetime] NULL,
	[UpdatedBy] [int] NULL,
	[AddressInfo] [nvarchar](500) NULL,
	[sourceData] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ProjectUser]    Script Date: 12/10/2024 11:56:39 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProjectUser](
	[ProjectName] [nvarchar](100) NULL,
	[CustomerName] [nvarchar](60) NULL,
	[NumOfMember] [int] NULL,
	[Position] [nvarchar](200) NULL,
	[Techology] [nvarchar](1000) NULL,
	[fromYear] [int] NULL,
	[fromMonth] [int] NULL,
	[toMonth] [int] NULL,
	[toYear] [int] NULL,
	[isEnd] [bit] NULL,
	[introduction] [nvarchar](1000) NULL,
	[linkFile] [varchar](1000) NULL,
	[RelId] [int] NULL,
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Deleted] [bit] NULL,
	[Status] [int] NULL,
	[CreateAt] [datetime] NULL,
	[CreatedBy] [int] NULL,
	[UpdateAt] [datetime] NULL,
	[UpdatedBy] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Recruiter]    Script Date: 12/10/2024 11:56:39 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Recruiter](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Deleted] [bit] NULL,
	[Status] [int] NULL,
	[CreateAt] [datetime] NULL,
	[CreatedBy] [int] NULL,
	[UpdateAt] [datetime] NULL,
	[UpdatedBy] [int] NULL,
	[Phone] [varchar](15) NULL,
	[Name] [nvarchar](30) NULL,
	[Taxcode] [nvarchar](30) NULL,
	[Email] [varchar](30) NULL,
	[UserName] [varchar](30) NULL,
	[Password] [varchar](30) NULL,
	[DateRegister] [datetime] NULL,
	[Gender] [bit] NULL,
	[NumberLightning] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RecruiterInfo]    Script Date: 12/10/2024 11:56:39 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RecruiterInfo](
	[levelAuthen] [int] NULL,
	[DateActive] [datetime] NULL,
	[Email] [varchar](30) NULL,
	[RelId] [int] NULL,
	[AvatarLink] [varchar](500) NULL,
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Deleted] [bit] NULL,
	[Status] [int] NULL,
	[CreateAt] [datetime] NULL,
	[CreatedBy] [int] NULL,
	[UpdateAt] [datetime] NULL,
	[UpdatedBy] [int] NULL,
	[Gender] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[regionals]    Script Date: 12/10/2024 11:56:39 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[regionals](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Deleted] [bit] NULL,
	[Status] [int] NULL,
	[CreateAt] [datetime] NULL,
	[CreatedBy] [int] NULL,
	[UpdateAt] [datetime] NULL,
	[UpdatedBy] [int] NULL,
	[Code] [varchar](5) NULL,
	[Name] [nvarchar](100) NULL,
	[Slug] [varchar](30) NULL,
	[Level1] [varchar](4) NULL,
	[Level2] [varchar](4) NULL,
	[datatype] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[resumes]    Script Date: 12/10/2024 11:56:39 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[resumes](
	[UserId] [varchar](50) NULL,
	[Email] [nvarchar](100) NULL,
	[TemplateId] [nvarchar](max) NULL,
	[TypeData] [int] NULL,
	[DataInput] [text] NULL,
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Deleted] [bit] NULL,
	[Status] [int] NULL,
	[CreateAt] [datetime] NULL,
	[CreatedBy] [int] NULL,
	[UpdateAt] [datetime] NULL,
	[UpdatedBy] [int] NULL,
	[LinkFile] [varchar](500) NULL,
	[sourceData] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RewardTransaction]    Script Date: 12/10/2024 11:56:39 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RewardTransaction](
	[Rel] [int] NULL,
	[Point] [int] NULL,
	[Content] [nvarchar](100) NULL,
	[BusinessDate] [datetime] NULL,
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Deleted] [bit] NULL,
	[Status] [int] NULL,
	[CreateAt] [datetime] NULL,
	[CreatedBy] [int] NULL,
	[UpdateAt] [datetime] NULL,
	[UpdatedBy] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SearchCV]    Script Date: 12/10/2024 11:56:39 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SearchCV](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Deleted] [bit] NULL,
	[Status] [int] NULL,
	[CreateAt] [datetime] NULL,
	[CreatedBy] [int] NULL,
	[UpdateAt] [datetime] NULL,
	[UpdatedBy] [int] NULL,
	[fullName] [nvarchar](100) NULL,
	[phone] [nvarchar](30) NULL,
	[email] [nvarchar](30) NULL,
	[Position] [nvarchar](100) NULL,
	[CandidateId] [int] NULL,
	[LocationText] [nvarchar](100) NULL,
	[ExperienceText] [nvarchar](100) NULL,
	[Educationtext] [nvarchar](100) NULL,
	[IntroductionText] [nvarchar](200) NULL,
	[CountView] [int] NULL,
	[CountContact] [int] NULL,
	[sourceData] [int] NULL,
	[LocationCode] [varchar](4) NULL,
	[gender] [int] NULL,
	[NumberLightning] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SearchCVExtra]    Script Date: 12/10/2024 11:56:39 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SearchCVExtra](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Deleted] [bit] NULL,
	[searchId] [int] NULL,
	[typeCount] [int] NULL,
	[RelId] [int] NULL,
	[Status] [int] NULL,
	[CreateAt] [datetime] NULL,
	[CreatedBy] [int] NULL,
	[UpdateAt] [datetime] NULL,
	[UpdatedBy] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SearchResult]    Script Date: 12/10/2024 11:56:39 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SearchResult](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[searchId] [int] NULL,
	[campaignId] [int] NULL,
	[relId] [int] NULL,
	[Deleted] [bit] NULL,
	[Status] [int] NULL,
	[CreateAt] [datetime] NULL,
	[CreatedBy] [int] NULL,
	[UpdateAt] [datetime] NULL,
	[UpdatedBy] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TicketItem]    Script Date: 12/10/2024 11:56:39 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TicketItem](
	[Title] [nvarchar](300) NULL,
	[Content] [nvarchar](300) NULL,
	[LinkFile] [varchar](100) NULL,
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Deleted] [bit] NULL,
	[Status] [int] NULL,
	[CreateAt] [datetime] NULL,
	[CreatedBy] [int] NULL,
	[UpdateAt] [datetime] NULL,
	[UpdatedBy] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserInfo]    Script Date: 12/10/2024 11:56:39 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserInfo](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Deleted] [bit] NULL,
	[Status] [int] NULL,
	[CreateAt] [datetime] NULL,
	[CreatedBy] [int] NULL,
	[UpdateAt] [datetime] NULL,
	[UpdatedBy] [int] NULL,
	[phone] [varchar](15) NULL,
	[firstName] [nvarchar](30) NULL,
	[FullName] [nvarchar](30) NULL,
	[email] [varchar](30) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserJobSetting]    Script Date: 12/10/2024 11:56:39 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserJobSetting](
	[Gender] [int] NULL,
	[Position] [nvarchar](100) NULL,
	[Field] [varchar](100) NULL,
	[skill] [varchar](100) NULL,
	[experience] [varchar](100) NULL,
	[salary] [varchar](100) NULL,
	[locationAddress] [varchar](100) NULL,
	[RelId] [int] NULL,
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Deleted] [bit] NULL,
	[Status] [int] NULL,
	[CreateAt] [datetime] NULL,
	[CreatedBy] [int] NULL,
	[UpdateAt] [datetime] NULL,
	[UpdatedBy] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserModel]    Script Date: 12/10/2024 11:56:39 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserModel](
	[Code] [varchar](8) NULL,
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserName] [varchar](10) NULL,
	[Password] [varchar](100) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[ActiveCodeRecruiter] ADD  DEFAULT ((0)) FOR [Deleted]
GO
ALTER TABLE [dbo].[ActiveCodeRecruiter] ADD  DEFAULT ((1)) FOR [Status]
GO
ALTER TABLE [dbo].[ActiveCodeRecruiter] ADD  DEFAULT (getdate()) FOR [CreateAt]
GO
ALTER TABLE [dbo].[ActiveCodeRecruiter] ADD  DEFAULT (getdate()) FOR [UpdateAt]
GO
ALTER TABLE [dbo].[Articles] ADD  DEFAULT ((0)) FOR [Deleted]
GO
ALTER TABLE [dbo].[Articles] ADD  DEFAULT ((0)) FOR [Status]
GO
ALTER TABLE [dbo].[Articles] ADD  DEFAULT (getdate()) FOR [CreateAt]
GO
ALTER TABLE [dbo].[Articles] ADD  DEFAULT (getdate()) FOR [UpdateAt]
GO
ALTER TABLE [dbo].[BusinessLicense] ADD  DEFAULT ((0)) FOR [Deleted]
GO
ALTER TABLE [dbo].[BusinessLicense] ADD  DEFAULT ((0)) FOR [Status]
GO
ALTER TABLE [dbo].[BusinessLicense] ADD  DEFAULT (getdate()) FOR [CreateAt]
GO
ALTER TABLE [dbo].[BusinessLicense] ADD  DEFAULT (getdate()) FOR [UpdateAt]
GO
ALTER TABLE [dbo].[BusinessLicenseLog] ADD  DEFAULT ((0)) FOR [Deleted]
GO
ALTER TABLE [dbo].[BusinessLicenseLog] ADD  DEFAULT ((0)) FOR [Status]
GO
ALTER TABLE [dbo].[BusinessLicenseLog] ADD  DEFAULT (getdate()) FOR [CreateAt]
GO
ALTER TABLE [dbo].[BusinessLicenseLog] ADD  DEFAULT (getdate()) FOR [UpdateAt]
GO
ALTER TABLE [dbo].[Campaign] ADD  DEFAULT ((0)) FOR [Deleted]
GO
ALTER TABLE [dbo].[Campaign] ADD  DEFAULT ((0)) FOR [Status]
GO
ALTER TABLE [dbo].[Campaign] ADD  DEFAULT (getdate()) FOR [CreateAt]
GO
ALTER TABLE [dbo].[Campaign] ADD  DEFAULT (getdate()) FOR [UpdateAt]
GO
ALTER TABLE [dbo].[Candidate] ADD  DEFAULT ((0)) FOR [Deleted]
GO
ALTER TABLE [dbo].[Candidate] ADD  DEFAULT ((0)) FOR [Status]
GO
ALTER TABLE [dbo].[Candidate] ADD  DEFAULT (getdate()) FOR [CreateAt]
GO
ALTER TABLE [dbo].[Candidate] ADD  DEFAULT (getdate()) FOR [UpdateAt]
GO
ALTER TABLE [dbo].[Candidate] ADD  DEFAULT ((0)) FOR [sourceData]
GO
ALTER TABLE [dbo].[CandidateInfo] ADD  DEFAULT ((0)) FOR [Status]
GO
ALTER TABLE [dbo].[CandidateInfo] ADD  DEFAULT ((1)) FOR [privateMode]
GO
ALTER TABLE [dbo].[CandidateInfo] ADD  DEFAULT ((1)) FOR [publicMode]
GO
ALTER TABLE [dbo].[CandidateInfo] ADD  DEFAULT (getdate()) FOR [UpdateAt]
GO
ALTER TABLE [dbo].[CandidateInfo] ADD  DEFAULT ((0)) FOR [Deleted]
GO
ALTER TABLE [dbo].[CandidateInfo] ADD  DEFAULT (getdate()) FOR [CreateAt]
GO
ALTER TABLE [dbo].[CandidateInfo] ADD  DEFAULT ((0)) FOR [sourceData]
GO
ALTER TABLE [dbo].[CategoryArticle] ADD  DEFAULT ((0)) FOR [Deleted]
GO
ALTER TABLE [dbo].[CategoryArticle] ADD  DEFAULT ((0)) FOR [Status]
GO
ALTER TABLE [dbo].[CategoryArticle] ADD  DEFAULT (getdate()) FOR [CreateAt]
GO
ALTER TABLE [dbo].[CategoryArticle] ADD  DEFAULT (getdate()) FOR [UpdateAt]
GO
ALTER TABLE [dbo].[CertifyUser] ADD  DEFAULT ((0)) FOR [Deleted]
GO
ALTER TABLE [dbo].[CertifyUser] ADD  DEFAULT ((0)) FOR [Status]
GO
ALTER TABLE [dbo].[CertifyUser] ADD  DEFAULT (getdate()) FOR [CreateAt]
GO
ALTER TABLE [dbo].[CertifyUser] ADD  DEFAULT (getdate()) FOR [UpdateAt]
GO
ALTER TABLE [dbo].[CompanyFavorite] ADD  DEFAULT ((0)) FOR [Deleted]
GO
ALTER TABLE [dbo].[CompanyFavorite] ADD  DEFAULT ((0)) FOR [Status]
GO
ALTER TABLE [dbo].[CompanyFavorite] ADD  DEFAULT (getdate()) FOR [CreateAt]
GO
ALTER TABLE [dbo].[CompanyFavorite] ADD  DEFAULT (getdate()) FOR [UpdateAt]
GO
ALTER TABLE [dbo].[CompanyFollow] ADD  DEFAULT ((0)) FOR [Deleted]
GO
ALTER TABLE [dbo].[CompanyFollow] ADD  DEFAULT ((0)) FOR [Status]
GO
ALTER TABLE [dbo].[CompanyFollow] ADD  DEFAULT (getdate()) FOR [CreateAt]
GO
ALTER TABLE [dbo].[CompanyFollow] ADD  DEFAULT (getdate()) FOR [UpdateAt]
GO
ALTER TABLE [dbo].[CompanyInfo] ADD  DEFAULT ((0)) FOR [Deleted]
GO
ALTER TABLE [dbo].[CompanyInfo] ADD  DEFAULT ((0)) FOR [Status]
GO
ALTER TABLE [dbo].[CompanyInfo] ADD  DEFAULT (getdate()) FOR [CreateAt]
GO
ALTER TABLE [dbo].[CompanyInfo] ADD  DEFAULT (getdate()) FOR [UpdateAt]
GO
ALTER TABLE [dbo].[educationUser] ADD  DEFAULT ((0)) FOR [Deleted]
GO
ALTER TABLE [dbo].[educationUser] ADD  DEFAULT ((0)) FOR [Status]
GO
ALTER TABLE [dbo].[educationUser] ADD  DEFAULT (getdate()) FOR [CreateAt]
GO
ALTER TABLE [dbo].[educationUser] ADD  DEFAULT (getdate()) FOR [UpdateAt]
GO
ALTER TABLE [dbo].[experienceUser] ADD  DEFAULT ((0)) FOR [Deleted]
GO
ALTER TABLE [dbo].[experienceUser] ADD  DEFAULT ((0)) FOR [Status]
GO
ALTER TABLE [dbo].[experienceUser] ADD  DEFAULT (getdate()) FOR [CreateAt]
GO
ALTER TABLE [dbo].[experienceUser] ADD  DEFAULT (getdate()) FOR [UpdateAt]
GO
ALTER TABLE [dbo].[ForgetPassword] ADD  DEFAULT ((0)) FOR [Deleted]
GO
ALTER TABLE [dbo].[ForgetPassword] ADD  DEFAULT ((1)) FOR [Status]
GO
ALTER TABLE [dbo].[ForgetPassword] ADD  DEFAULT (getdate()) FOR [CreateAt]
GO
ALTER TABLE [dbo].[ForgetPassword] ADD  DEFAULT (getdate()) FOR [UpdateAt]
GO
ALTER TABLE [dbo].[ForgetPassword] ADD  DEFAULT ((0)) FOR [TypeUser]
GO
ALTER TABLE [dbo].[ForgetPassword] ADD  DEFAULT ((0)) FOR [sendMailStatus]
GO
ALTER TABLE [dbo].[jobApply] ADD  DEFAULT ((0)) FOR [Deleted]
GO
ALTER TABLE [dbo].[jobApply] ADD  DEFAULT ((0)) FOR [Status]
GO
ALTER TABLE [dbo].[jobApply] ADD  DEFAULT (getdate()) FOR [CreateAt]
GO
ALTER TABLE [dbo].[jobApply] ADD  DEFAULT (getdate()) FOR [UpdateAt]
GO
ALTER TABLE [dbo].[jobApply] ADD  DEFAULT ((0)) FOR [ViewMode]
GO
ALTER TABLE [dbo].[JobApplyStatus] ADD  DEFAULT ((0)) FOR [Deleted]
GO
ALTER TABLE [dbo].[JobApplyStatus] ADD  DEFAULT ((0)) FOR [Status]
GO
ALTER TABLE [dbo].[JobApplyStatus] ADD  DEFAULT (getdate()) FOR [CreateAt]
GO
ALTER TABLE [dbo].[JobApplyStatus] ADD  DEFAULT (getdate()) FOR [UpdateAt]
GO
ALTER TABLE [dbo].[jobInfo] ADD  DEFAULT ((1)) FOR [aggrement]
GO
ALTER TABLE [dbo].[jobInfo] ADD  DEFAULT ((-1)) FOR [salary_from]
GO
ALTER TABLE [dbo].[jobInfo] ADD  DEFAULT ((-1)) FOR [salary_to]
GO
ALTER TABLE [dbo].[jobInfo] ADD  DEFAULT ((1)) FOR [type_money]
GO
ALTER TABLE [dbo].[jobInfo] ADD  DEFAULT ((-1)) FOR [gender]
GO
ALTER TABLE [dbo].[jobInfo] ADD  DEFAULT ((0)) FOR [Deleted]
GO
ALTER TABLE [dbo].[jobInfo] ADD  DEFAULT ((0)) FOR [Status]
GO
ALTER TABLE [dbo].[jobInfo] ADD  DEFAULT (getdate()) FOR [CreateAt]
GO
ALTER TABLE [dbo].[jobInfo] ADD  DEFAULT (getdate()) FOR [UpdateAt]
GO
ALTER TABLE [dbo].[jobItemDisplay] ADD  DEFAULT ((0)) FOR [Deleted]
GO
ALTER TABLE [dbo].[jobItemDisplay] ADD  DEFAULT ((0)) FOR [Status]
GO
ALTER TABLE [dbo].[jobItemDisplay] ADD  DEFAULT (getdate()) FOR [CreateAt]
GO
ALTER TABLE [dbo].[jobItemDisplay] ADD  DEFAULT (getdate()) FOR [UpdateAt]
GO
ALTER TABLE [dbo].[jobItems] ADD  DEFAULT ((0)) FOR [Deleted]
GO
ALTER TABLE [dbo].[jobItems] ADD  DEFAULT ((0)) FOR [Status]
GO
ALTER TABLE [dbo].[jobItems] ADD  DEFAULT (getdate()) FOR [CreateAt]
GO
ALTER TABLE [dbo].[jobItems] ADD  DEFAULT (getdate()) FOR [UpdateAt]
GO
ALTER TABLE [dbo].[JobLogView] ADD  DEFAULT ((0)) FOR [Deleted]
GO
ALTER TABLE [dbo].[JobLogView] ADD  DEFAULT ((0)) FOR [Status]
GO
ALTER TABLE [dbo].[JobLogView] ADD  DEFAULT (getdate()) FOR [CreateAt]
GO
ALTER TABLE [dbo].[JobLogView] ADD  DEFAULT (getdate()) FOR [UpdateAt]
GO
ALTER TABLE [dbo].[JobOverViewCounter] ADD  DEFAULT ((0)) FOR [Deleted]
GO
ALTER TABLE [dbo].[JobOverViewCounter] ADD  DEFAULT ((0)) FOR [Status]
GO
ALTER TABLE [dbo].[JobOverViewCounter] ADD  DEFAULT (getdate()) FOR [CreateAt]
GO
ALTER TABLE [dbo].[JobOverViewCounter] ADD  DEFAULT (getdate()) FOR [UpdateAt]
GO
ALTER TABLE [dbo].[jobSave] ADD  DEFAULT ((0)) FOR [Deleted]
GO
ALTER TABLE [dbo].[jobSave] ADD  DEFAULT ((0)) FOR [Status]
GO
ALTER TABLE [dbo].[jobSave] ADD  DEFAULT (getdate()) FOR [CreateAt]
GO
ALTER TABLE [dbo].[jobSave] ADD  DEFAULT (getdate()) FOR [UpdateAt]
GO
ALTER TABLE [dbo].[LogAction] ADD  DEFAULT ((0)) FOR [Deleted]
GO
ALTER TABLE [dbo].[LogAction] ADD  DEFAULT ((0)) FOR [Status]
GO
ALTER TABLE [dbo].[LogAction] ADD  DEFAULT (getdate()) FOR [CreateAt]
GO
ALTER TABLE [dbo].[LogAction] ADD  DEFAULT (getdate()) FOR [UpdateAt]
GO
ALTER TABLE [dbo].[LogAction] ADD  DEFAULT ((0)) FOR [source]
GO
ALTER TABLE [dbo].[mailInfo] ADD  DEFAULT ((0)) FOR [Deleted]
GO
ALTER TABLE [dbo].[mailInfo] ADD  DEFAULT ((0)) FOR [Status]
GO
ALTER TABLE [dbo].[mailInfo] ADD  DEFAULT (getdate()) FOR [CreateAt]
GO
ALTER TABLE [dbo].[mailInfo] ADD  DEFAULT (getdate()) FOR [UpdateAt]
GO
ALTER TABLE [dbo].[MasterData] ADD  DEFAULT ((0)) FOR [Deleted]
GO
ALTER TABLE [dbo].[MasterData] ADD  DEFAULT ((0)) FOR [Status]
GO
ALTER TABLE [dbo].[MasterData] ADD  DEFAULT (getdate()) FOR [CreateAt]
GO
ALTER TABLE [dbo].[MasterData] ADD  DEFAULT (getdate()) FOR [UpdateAt]
GO
ALTER TABLE [dbo].[Notification] ADD  DEFAULT ((0)) FOR [Deleted]
GO
ALTER TABLE [dbo].[Notification] ADD  DEFAULT ((0)) FOR [Status]
GO
ALTER TABLE [dbo].[Notification] ADD  DEFAULT (getdate()) FOR [CreateAt]
GO
ALTER TABLE [dbo].[Notification] ADD  DEFAULT (getdate()) FOR [UpdateAt]
GO
ALTER TABLE [dbo].[OtherProfileUser] ADD  DEFAULT ((0)) FOR [Deleted]
GO
ALTER TABLE [dbo].[OtherProfileUser] ADD  DEFAULT ((0)) FOR [Status]
GO
ALTER TABLE [dbo].[OtherProfileUser] ADD  DEFAULT (getdate()) FOR [CreateAt]
GO
ALTER TABLE [dbo].[OtherProfileUser] ADD  DEFAULT (getdate()) FOR [UpdateAt]
GO
ALTER TABLE [dbo].[PageContent] ADD  DEFAULT ((0)) FOR [Deleted]
GO
ALTER TABLE [dbo].[PageContent] ADD  DEFAULT ((0)) FOR [Status]
GO
ALTER TABLE [dbo].[PageContent] ADD  DEFAULT (getdate()) FOR [CreateAt]
GO
ALTER TABLE [dbo].[PageContent] ADD  DEFAULT (getdate()) FOR [UpdateAt]
GO
ALTER TABLE [dbo].[PageContent] ADD  DEFAULT ((1)) FOR [TypeData]
GO
ALTER TABLE [dbo].[ProfileCV] ADD  DEFAULT ((0)) FOR [Deleted]
GO
ALTER TABLE [dbo].[ProfileCV] ADD  DEFAULT ((0)) FOR [Status]
GO
ALTER TABLE [dbo].[ProfileCV] ADD  DEFAULT (getdate()) FOR [CreateAt]
GO
ALTER TABLE [dbo].[ProfileCV] ADD  DEFAULT (getdate()) FOR [UpdateAt]
GO
ALTER TABLE [dbo].[ProfileCV] ADD  DEFAULT ((1)) FOR [sourceData]
GO
ALTER TABLE [dbo].[ProjectUser] ADD  DEFAULT ((0)) FOR [Deleted]
GO
ALTER TABLE [dbo].[ProjectUser] ADD  DEFAULT ((0)) FOR [Status]
GO
ALTER TABLE [dbo].[ProjectUser] ADD  DEFAULT (getdate()) FOR [CreateAt]
GO
ALTER TABLE [dbo].[ProjectUser] ADD  DEFAULT (getdate()) FOR [UpdateAt]
GO
ALTER TABLE [dbo].[Recruiter] ADD  DEFAULT ((0)) FOR [Deleted]
GO
ALTER TABLE [dbo].[Recruiter] ADD  DEFAULT ((0)) FOR [Status]
GO
ALTER TABLE [dbo].[Recruiter] ADD  DEFAULT (getdate()) FOR [CreateAt]
GO
ALTER TABLE [dbo].[Recruiter] ADD  DEFAULT (getdate()) FOR [UpdateAt]
GO
ALTER TABLE [dbo].[Recruiter] ADD  DEFAULT ((0)) FOR [NumberLightning]
GO
ALTER TABLE [dbo].[RecruiterInfo] ADD  DEFAULT ((0)) FOR [levelAuthen]
GO
ALTER TABLE [dbo].[RecruiterInfo] ADD  DEFAULT ((0)) FOR [Deleted]
GO
ALTER TABLE [dbo].[RecruiterInfo] ADD  DEFAULT ((0)) FOR [Status]
GO
ALTER TABLE [dbo].[RecruiterInfo] ADD  DEFAULT (getdate()) FOR [CreateAt]
GO
ALTER TABLE [dbo].[RecruiterInfo] ADD  DEFAULT (getdate()) FOR [UpdateAt]
GO
ALTER TABLE [dbo].[regionals] ADD  DEFAULT ((0)) FOR [Deleted]
GO
ALTER TABLE [dbo].[regionals] ADD  DEFAULT ((0)) FOR [Status]
GO
ALTER TABLE [dbo].[regionals] ADD  DEFAULT (getdate()) FOR [CreateAt]
GO
ALTER TABLE [dbo].[regionals] ADD  DEFAULT (getdate()) FOR [UpdateAt]
GO
ALTER TABLE [dbo].[resumes] ADD  DEFAULT ((0)) FOR [Deleted]
GO
ALTER TABLE [dbo].[resumes] ADD  DEFAULT ((0)) FOR [Status]
GO
ALTER TABLE [dbo].[resumes] ADD  DEFAULT (getdate()) FOR [CreateAt]
GO
ALTER TABLE [dbo].[resumes] ADD  DEFAULT (getdate()) FOR [UpdateAt]
GO
ALTER TABLE [dbo].[resumes] ADD  DEFAULT ((0)) FOR [sourceData]
GO
ALTER TABLE [dbo].[RewardTransaction] ADD  DEFAULT ((0)) FOR [Deleted]
GO
ALTER TABLE [dbo].[RewardTransaction] ADD  DEFAULT ((0)) FOR [Status]
GO
ALTER TABLE [dbo].[RewardTransaction] ADD  DEFAULT (getdate()) FOR [CreateAt]
GO
ALTER TABLE [dbo].[RewardTransaction] ADD  DEFAULT (getdate()) FOR [UpdateAt]
GO
ALTER TABLE [dbo].[SearchCV] ADD  DEFAULT ((0)) FOR [Deleted]
GO
ALTER TABLE [dbo].[SearchCV] ADD  DEFAULT ((0)) FOR [Status]
GO
ALTER TABLE [dbo].[SearchCV] ADD  DEFAULT (getdate()) FOR [CreateAt]
GO
ALTER TABLE [dbo].[SearchCV] ADD  DEFAULT (getdate()) FOR [UpdateAt]
GO
ALTER TABLE [dbo].[SearchCV] ADD  DEFAULT ((0)) FOR [sourceData]
GO
ALTER TABLE [dbo].[SearchCV] ADD  DEFAULT ((0)) FOR [gender]
GO
ALTER TABLE [dbo].[SearchCV] ADD  DEFAULT ((0)) FOR [NumberLightning]
GO
ALTER TABLE [dbo].[SearchCVExtra] ADD  DEFAULT ((0)) FOR [Deleted]
GO
ALTER TABLE [dbo].[SearchCVExtra] ADD  DEFAULT ((0)) FOR [searchId]
GO
ALTER TABLE [dbo].[SearchCVExtra] ADD  DEFAULT ((0)) FOR [typeCount]
GO
ALTER TABLE [dbo].[SearchCVExtra] ADD  DEFAULT ((0)) FOR [RelId]
GO
ALTER TABLE [dbo].[SearchCVExtra] ADD  DEFAULT ((0)) FOR [Status]
GO
ALTER TABLE [dbo].[SearchCVExtra] ADD  DEFAULT (getdate()) FOR [CreateAt]
GO
ALTER TABLE [dbo].[SearchCVExtra] ADD  DEFAULT (getdate()) FOR [UpdateAt]
GO
ALTER TABLE [dbo].[SearchResult] ADD  DEFAULT ((0)) FOR [Deleted]
GO
ALTER TABLE [dbo].[SearchResult] ADD  DEFAULT ((0)) FOR [Status]
GO
ALTER TABLE [dbo].[SearchResult] ADD  DEFAULT (getdate()) FOR [CreateAt]
GO
ALTER TABLE [dbo].[SearchResult] ADD  DEFAULT (getdate()) FOR [UpdateAt]
GO
ALTER TABLE [dbo].[TicketItem] ADD  DEFAULT ((0)) FOR [Deleted]
GO
ALTER TABLE [dbo].[TicketItem] ADD  DEFAULT ((0)) FOR [Status]
GO
ALTER TABLE [dbo].[TicketItem] ADD  DEFAULT (getdate()) FOR [CreateAt]
GO
ALTER TABLE [dbo].[TicketItem] ADD  DEFAULT (getdate()) FOR [UpdateAt]
GO
ALTER TABLE [dbo].[UserInfo] ADD  DEFAULT ((0)) FOR [Deleted]
GO
ALTER TABLE [dbo].[UserInfo] ADD  DEFAULT ((0)) FOR [Status]
GO
ALTER TABLE [dbo].[UserInfo] ADD  DEFAULT (getdate()) FOR [CreateAt]
GO
ALTER TABLE [dbo].[UserInfo] ADD  DEFAULT (getdate()) FOR [UpdateAt]
GO
ALTER TABLE [dbo].[UserJobSetting] ADD  DEFAULT ((0)) FOR [Deleted]
GO
ALTER TABLE [dbo].[UserJobSetting] ADD  DEFAULT ((0)) FOR [Status]
GO
ALTER TABLE [dbo].[UserJobSetting] ADD  DEFAULT (getdate()) FOR [CreateAt]
GO
ALTER TABLE [dbo].[UserJobSetting] ADD  DEFAULT (getdate()) FOR [UpdateAt]
GO
/****** Object:  StoredProcedure [dbo].[ArticleGetAll]    Script Date: 12/10/2024 11:56:39 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE procedure [dbo].[ArticleGetAll]
(

 @CategoryId  int =null, 
 @Status  varchar(30) = NULL, 
 @SlugCategory varchar(30) = '',
 @Slug varchar(100) = '',
 @page int = 1,
 @userId int  = null, 
 @From datetime =null , 
 @To datetime =null, 
 @limit int =100
)
as
begin


declare @where  nvarchar(max) = ' where  isnull(d.Deleted,0) = 0';
declare @mainClause nvarchar(max);
declare @params nvarchar(300);





declare @offset int = 0;
set @offset = (@page-1)*@Limit;
set @mainClause = '  select d.*,dbo.getFullNameCategory( d.linked, @CategoryId ) as CategoryName from Articles d ';

declare @idTemp int = 0;
if( @SlugCategory != '')
begin 
		select  top 1 @idTemp = id from CategoryArticle where slug = @SlugCategory
end

if(@idTemp >0)
begin 
  set @CategoryId = @idTemp

end



if( @Status >-1 )
begin 

	set @where += ' and d.Status =  @Status '; 

end 

if( @CategoryId >-1 )
begin 

	set @where += ' and  CAST( @CategoryId as varchar(5) )  IN (SELECT value FROM STRING_SPLIT(linked, '','')) '; 

end 

if( @Slug != '' )
begin 

	set @where += ' and d.slug =  @slug '; 

end 


set @where +=' order by d.UpdateAt desc'; 
set @where += ' offset @offset ROWS FETCH NEXT @limit ROWS ONLY'
set @mainClause = @mainClause +  @where
set @params =N' @offset int, @limit int, @fromDate datetime,@toDate datetime , @CategoryId int, @idTemp int,    @slug varchar(100), @Status int , @userId int ';

EXECUTE sp_executesql @mainClause,@params, @offset = @offset, @limit = @limit,
@fromDate = @From,@toDate =@To, @Status = @Status ,@userId = @userId, @Slug = @Slug, @CategoryId= @CategoryId , @idTemp= @idTemp

end
  



GO
/****** Object:  StoredProcedure [dbo].[importCandidate]    Script Date: 12/10/2024 11:56:39 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE procedure [dbo].[importCandidate]
(
	
	@Email varchar(30) = '',
	@Name nvarchar(100) = '',
	@Phone varchar(20) = 10,
	@CVLink varchar(1000) = '', 
	@CVLink2 varchar(1000) = '',
	@PostionJob nvarchar(200) =''
)
as
begin
		declare @idInput int;
		set @idInput = 0;
		select top 1 @idInput = id  from  Candidate where Phone = @Phone or Email = @Email  
		if(@idInput > 0)
		begin 
		return;
		end
		insert into Candidate(email, phone,  FirstName, FullName, UserName, Password, CreateAt, DateRegister, CreatedBy,sourceData)
		values(@Email, @Phone,'', @Name, @Email, '123456', GETDATE(), getdate(), 1,2);

		declare @idCand  int; 
		set @idCand = IDENT_CURRENT('Candidate');
		insert into CandidateInfo (AvatarLink,  CreateAt, CreatedBy, Deleted, email,privateMode,publicMode, Status, 
		userId, UpdateAt, UpdatedBy,sourceData)
		values( '', getdate(), 1,0,@Email, 0, 1, 1, @idCand, GETDATE(), 1,2)

		declare @cvlinkInsert varchar(1000);
		set @cvlinkInsert ='';

		if(@CVLink != '')
		begin 
			set @cvlinkInsert = @CVLink;
		end 

		else 

		begin 
			set @cvlinkInsert = @CVLink2;
		end  


		if(@cvlinkInsert != '')
		begin 
			
				INSERT INTO [dbo].[resumes]
			   ([UserId]
			   ,[Email]
			   ,[TemplateId]
			   ,[TypeData]
			   ,[DataInput]
			   ,[Deleted]
			   ,[Status]
			   ,[CreateAt]
			   ,[CreatedBy]
			   ,[UpdateAt]
			   ,[UpdatedBy]
			   ,[LinkFile]
			   ,[sourceData])
			VALUES
				( @idCand, @Email, 1,2, '',0,1,getdate(), 1, getdate(), 1 ,@cvlinkInsert, 2 )
		end 
		
		INSERT INTO [dbo].[ProfileCV]
           ([FullName]
           ,[position]
           ,[level]
           ,[gender]
           ,[Email]
           ,[phoneNumber]
           ,[introduction]
           ,[avatarLink]
           ,[RelId]
           ,[Deleted]
           ,[Status]
           ,[CreateAt]
           ,[CreatedBy]
           ,[UpdateAt]
           ,[UpdatedBy]
           ,[AddressInfo]
           ,[sourceData])
     VALUES
           ( @Name
           ,@PostionJob
           ,''
           ,2
           ,@Email
           ,@Phone
           ,''
           ,''
           ,@idCand
           ,0
           ,1
           ,getdate()
           ,@idCand
           ,getdate()
           ,@idCand
           ,''
           ,2)
end
  
  



  alter table ProfileCV alter column position nvarchar(100)
GO
/****** Object:  StoredProcedure [dbo].[sp_applyJobWithCreateCV]    Script Date: 12/10/2024 11:56:39 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE procedure [dbo].[sp_applyJobWithCreateCV]
(
	@TypeData int =-1,
	@TemplateID int = 1,
	@LinkFile varchar(500), 
	@FullName nvarchar(50), 
	@Phone varchar(12) null, 
	@Email varchar(30) null,
	@UserId int null,
	@jobId int null
)
as
begin
declare @params nvarchar(300);
insert into resumes( UserId ,Email, TemplateId, TypeData, DataInput, Status, CreateAt,  CreatedBy ,
LinkFile) values ( @UserId,@Email, @TemplateID,@TypeData, '', 1, getdate(), @UserId,@LinkFile)
declare @ressumesId  int; 
set @ressumesId = IDENT_CURRENT('resumes')
INSERT INTO [dbo].[jobApply]
           ([JobId]
           ,[CVId]
           ,[Deleted]
           ,[Status]
           ,[CreateAt]
           ,[CreatedBy]
           ,[UpdateAt]
           ,[UpdatedBy]
           ,[ViewMode]
           ,[FullName]
           ,[Email]
           ,[Phone])
     VALUES
( 
@jobId, @ressumesId, 0, 1, getdate(), @UserId, GETDATE(), @UserId, 0, @FullName, @Email, @Phone
		   
)
declare @cvApplyId  int; 
set @cvApplyId = IDENT_CURRENT('jobApply')
declare @position nvarchar(300);
set @position ='';
declare @recruiterId int;
set @recruiterId = -1;
select top 1 @position = [Name], @recruiterId = CreatedBy from jobInfo where JobId = @jobId
declare @content nvarchar(400);
set @content ='';
set @content = CONCAT(N'Ứng viên  đã  nộp hồ sơ  cho vị trí',' ',@position);
declare @userNameHuman varchar(30);
set @userNameHuman =''
select top 1 @userNameHuman = UserName  from Recruiter where id = @recruiterId
insert INTO [dbo].[Notification]
           ([userId]
           ,[Title]
           ,[RelId]
           ,[UserName]
           ,[LableText]
           ,[TypeInfo]
           ,[Content]
           ,[LinkFile]
           ,[Deleted]
           ,[Status]
           ,[CreateAt]
           ,[CreatedBy]
           ,[UpdateAt]
           ,[UpdatedBy]
           ,[Source])
		 VALUES
           (
		   @UserId , N'Có CV mới', 
		   @cvApplyId,@userNameHuman,
		   N'Ứng tuyển', 2, @content, 
		   '',0,0,getdate(),@UserId,
		   getdate(),  @UserId,2
		   )
end
  


  
GO
/****** Object:  StoredProcedure [dbo].[sp_article_getAll]    Script Date: 12/10/2024 11:56:39 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create procedure [dbo].[sp_article_getAll]
(

 @CategoryId  varchar(30) NULL, 
 @Status  varchar(30) NULL, 
 @SlugCategory nvarchar(50) = '',
 @Slug nvarchar(50) null,
 @page int = 1,
 @userId int  = null, 
 @From datetime null , 
 @To datetime null, 
 @limit int =10
)
as
begin


declare @where  nvarchar(max) = ' where  isnull(d.Deleted,0) = 0';
declare @mainClause nvarchar(max);
declare @params nvarchar(300);



declare @offset int = 0;
set @offset = (@page-1)*@Limit;
set @mainClause = '  select * from Articles  ';

declare @idTemp int = 0;
if( @SlugCategory != '')
begin 
		select  top 1 @idTemp = id from CategoryArticle where slug = @SlugCategory
end


if(@idTemp >0)
begin 
	set @where += ' and d.linked =  @idTemp '; 
end



if( @Status >-1 )
begin 

	set @where += ' and d.Status =  @Status '; 

end 

if( @Slug >-1 )
begin 

	set @where += ' and d.slug =  @slug '; 

end 
if( @SlugCategory >-1 )
begin 

	set @where += ' and d.slug =  @slug '; 

end 

set @where +=' order by d.UpdateAt desc'; 
set @where += ' offset @offset ROWS FETCH NEXT @limit ROWS ONLY'
set @mainClause = @mainClause +  @where
set @params =N' @offset int, @limit int, @fromDate datetime,@toDate datetime , @Status int , @userId int ';

EXECUTE sp_executesql @mainClause,@params, @offset = @offset, @limit = @limit,
@fromDate = @From,@toDate =@To, @Status = @Status ,@userId = @userId

end
  



GO
/****** Object:  StoredProcedure [dbo].[sp_articles_insert]    Script Date: 12/10/2024 11:56:39 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



CREATE procedure [dbo].[sp_articles_insert]
(
  @Title nvarchar(100) null,
  @CoverImage nvarchar(250) null,
  @KeyWord nvarchar(200) null, 
  @Content ntext null, 
  @ShortDes nvarchar(500) null, 
  @linked varchar (5) null,
  @Slug varchar(100) null,
  @AuthorPost nvarchar(200) null
)
as
begin
INSERT INTO [dbo].[Articles]
           ([Deleted]
           ,[Status]
           ,[CreateAt]
           ,[CreatedBy]
           ,[UpdateAt]
           ,[UpdatedBy]
           ,[Title]
           ,[CoverImage]
           ,[KeyWord]
           ,[Content]
           ,[ShortDes]
           ,[linked]
           ,[slug])
     VALUES
           (
		     0,1,GETDATE(),1,getdate(), 1,@Title, @CoverImage,@KeyWord,@Content,@ShortDes,@linked,@Slug
		   
		   
		   )
  
end
  



GO
/****** Object:  StoredProcedure [dbo].[sp_campaign_getAll]    Script Date: 12/10/2024 11:56:39 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


create procedure [dbo].[sp_campaign_getAll]
(
	
	@OrderBy varchar(30) = '',
	@Page int =1,
	@Limit int = 10,
	@status int  = -1, 
	@email  varchar(30) null,
	@From datetime = null,
	@To datetime = null 
)
as
begin

declare @where  nvarchar(max) = ' where  ISNULL(d.Deleted,0) = 0  ';
declare @mainClause nvarchar(max);
declare @params nvarchar(300);

declare @offset int = 0;
set @offset = (@page-1)*@Limit;

set @mainClause = '  select count(d.id) over() as TotalRecord, d.* from  Campaign d  ';

if( @status > -1)
begin 
	set @where +=	 ' and d.status =  @status ';
end 

if( @email <>  '' )
begin 
	set @where +=	 ' and d.email =  @email  ';
end 

set @where +=' order by d.UpdateAt desc'; 
set @where += ' offset @offset ROWS FETCH NEXT @limit ROWS ONLY'
set @mainClause = @mainClause +  @where
set @params =N' @offset int, @limit int, @fromDate datetime,@toDate datetime , @status int ,  @email  varchar(30)   ';
EXECUTE sp_executesql @mainClause,@params, @offset = @offset, @limit = @limit, @status = @status,
@fromDate = @From,@toDate =@To, @email= @email



end
  

GO
/****** Object:  StoredProcedure [dbo].[sp_Candidate_register]    Script Date: 12/10/2024 11:56:39 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE procedure [dbo].[sp_Candidate_register]
(
 @UserName  varchar(30) NULL, 
 @Pass  varchar(30) NULL, 
 @FirstName nvarchar(50) null,
 @FullName nvarchar(50) null, 
 @Email varchar(30) null, 
 @Phone varchar(15) null,
 @CreatedBy  int null 
)
as
begin
     
		INSERT INTO [dbo].[Candidate]
           (
		    [Deleted]
           ,[Status]
           ,[CreateAt]
           ,[CreatedBy]
           ,[UpdateAt]
           ,[UpdatedBy]
           ,[Phone]
           ,[FirstName]
           ,[FullName]
           ,[Email]
           ,[UserName]
           ,[Password]
		   , DateRegister
		   )
     VALUES
           (
		     0, 
			 1,
			 getdate(), 
			 @CreatedBy,
			 getdate(), 
			 @CreatedBy,
			 @Phone,
			 @FirstName,
			 @FullName, 
			 @Email, 
			 @UserName,
			 @Pass,
			 getdate()
			 )


end
  


GO
/****** Object:  StoredProcedure [dbo].[sp_Candidate_search]    Script Date: 12/10/2024 11:56:39 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create procedure [dbo].[sp_Candidate_search]
(
 @Email  varchar(30) NULL, 
 @Password  varchar(30) NULL

)
as
begin
 
 select top 1 * from  Candidate where email = @Email and [Password] = @Password


end
  


GO
/****** Object:  StoredProcedure [dbo].[sp_cv_getDeatailSearchId]    Script Date: 12/10/2024 11:56:39 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[sp_cv_getDeatailSearchId]
(
	@searchId int  null
)

as
begin
select d.fullName, f.gender, d.LocationCode,
d.LocationText,  
d.ExperienceText , 
f.level,  dbo.getNameMaster(f.level) as LevelText,
0 as SalaryFrom,
0 as SalaryTo

from  SearchCV d 
inner join Candidate e
on d.CandidateId = e.id
inner join ProfileCV f
on e.Email = f.email
where d.id  =  @searchId


end
  

GO
/****** Object:  StoredProcedure [dbo].[sp_cv_getInfo]    Script Date: 12/10/2024 11:56:39 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE procedure [dbo].[sp_cv_getInfo]
(
  @id  int null
)
as
begin

declare @where  nvarchar(max) = ' where  1=1  ';
declare @mainClause nvarchar(max);
declare @params nvarchar(300);

set @mainClause = ' select  f.Phone,  d.id as CVId, concat(f.FirstName, f.FullName) 
as FullName, d.CreateAt, f.Email,  d.UserId,
d.LinkFile as  LinkCV , d.Status 

from resumes d 
inner join Candidate  f on d.UserId   =  f.Id
 ';
set @where +=	 ' and d.id =  @id ';
set @mainClause = @mainClause +  @where
set @params =N' @id int  ';

EXECUTE sp_executesql @mainClause,@params, @id  = @id 

end
  

GO
/****** Object:  StoredProcedure [dbo].[sp_cv_search]    Script Date: 12/10/2024 11:56:39 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[sp_cv_search]
(
	@KeyWord nvarchar(100) ='',
	@LocationCode varchar(3) = '',
	@CvKey nvarchar(100) = '',
	@Gender int  = 0, 
	@DayOfBirth datetime = null, 
	@SchoolSearch  nvarchar(100)  = '',
	@OrderBy varchar(30) = '',
	@Page int =1,
	@Limit int = 10 
)

as
begin
declare @where  nvarchar(max) = ' where  1=1  ';
declare @mainClause nvarchar(max);
declare @params nvarchar(300);
declare @offset int = 0;
set @offset = (@page-1)*@Limit;
set @mainClause = 'select LocationCode, LocationText,
Position, ExperienceText,
IntroductionText as JobObjectiveText ,
UpdateAt as LastUpdate,
0 as SalaryFrom,
0 as SalaryTo,
CountView as TotalView, 
CountContact as TotalContact,
id as SearchId, 
fullName,
'''' as Avatarlink
from SearchCV d  ';

if(@LocationCode <> '' and @LocationCode is not  null  )
begin 
	set @where += ' and d.LocationCode =  @LocationCode '; 
end

if(@KeyWord <> ''  and @KeyWord is not  null  )
begin

 set @where += ' and (d.fullName like  N''%' + @KeyWord +'%''';
 set @where += ' or d.phone like  N''%' + @KeyWord +'%''';
 set @where += ' or d.Educationtext like  N''%' + @KeyWord +'%''';
 set @where += ' or d.IntroductionText like  N''%' + @KeyWord +'%''';
 set @where += ' or d.email like  N''%' + @KeyWord +'%'')';
end;

if(@SchoolSearch  <> ''  and @SchoolSearch is not  null )
begin 
	set @where += ' and d.Educationtext =  @SchoolSearch '; 
end

if(@Gender > 0  and @Gender is not null)
begin 
	set @where += ' and d.gender =  @Gender '; 
end

set @where +=' order by UpdateAt desc'; 
set @mainClause = @mainClause +  @where

set @params =N' @limit int , @LocationCode varchar(3) , @Gender int, @SchoolSearch  nvarchar(100) ';
EXECUTE sp_executesql @mainClause,@params,  @limit = @limit, @LocationCode = @LocationCode, @Gender = @Gender,
@SchoolSearch  = @SchoolSearch 


end
  

GO
/****** Object:  StoredProcedure [dbo].[sp_getAllCV]    Script Date: 12/10/2024 11:56:39 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE procedure [dbo].[sp_getAllCV]
(

 @UserId  int =null, 
 @TypeData  varchar(30) = NULL, 
 @TemplateID nvarchar(50) = '',
 @DataInput varchar(30)  ='',
 @page int = 1,
 @Status int =-1,
 @limit int =100
)
as
begin
declare @where  nvarchar(max) = ' where  isnull(d.Deleted,0) = 0';
declare @mainClause nvarchar(max);
declare @params nvarchar(300);
declare @offset int = 0;
set @offset = (@page-1)*@Limit;
set @mainClause = '  select d.* from resumes d  ';

if(@UserId >0)
begin 
	set @where += ' and d.UserId =  @UserId '; 
end

if( @Status >-1 )
begin 

	set @where += ' and d.Status =  @Status '; 
end 

set @where +=' order by d.UpdateAt desc'; 
set @where += ' offset @offset ROWS FETCH NEXT @limit ROWS ONLY'
set @mainClause = @mainClause +  @where
set @params =N' @offset int, @limit int, @Status int , @userId int , @TypeData int ,  @TemplateID int   ';

EXECUTE sp_executesql @mainClause,@params, @offset = @offset, @limit = @limit,
@Status = @Status,@TypeData =@TypeData ,@TemplateID = @TemplateID

end
  



GO
/****** Object:  StoredProcedure [dbo].[sp_getAllCVApplyUserId]    Script Date: 12/10/2024 11:56:39 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE procedure [dbo].[sp_getAllCVApplyUserId]
(

  @UserId int  =-1

)
as
begin
select e.JobId as JobId, f.JobName as PositionText  , co.FullName as CompanyName,
e.CreateAt as BusinessDate, co.LogoLink as ShortLinkLogo,
f.RangeSalary as  SalaryText, f.salaryfrom as SalaryFrom, f.salaryTo as SalaryTo, f.ProfessionText as FieldArray, 
e.Id,  f.LocationText,

f.JobId as  JobId ,
f.Slug as JobSlug,
f.UpdateAt as LastUpdate, 
d.LinkFile as LinkFile,
e.Status as  ReasonId,
'' as 'Note' from  resumes d inner join jobApply e  on d.Id = e.CVId 
  inner join jobItemDisplay f on  e.JobId = f.JobId 
  inner join CompanyInfo co  on co.RelId = f.recurId
  inner join Candidate  g on g.id = d.UserId

  where d.UserId = @UserId


end
  



GO
/****** Object:  StoredProcedure [dbo].[sp_getAllCVByCampangn]    Script Date: 12/10/2024 11:56:39 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE procedure [dbo].[sp_getAllCVByCampangn]
(

 @JobId int =null, 
 @UserId  varchar(30) = NULL, 
 @CampagnId int =-1, 
 @TypeData int =-1, 
 @page int = 1,
 @Status int =-1,
 @key nvarchar(100)  ='' ,
 @limit int =100
)
as
begin
declare @where  nvarchar(max) = ' where  isnull(d.Deleted,0) = 0';
declare @mainClause nvarchar(max);
declare @params nvarchar(300);
declare @offset int = 0;
set @offset = (@page-1)*@Limit;
set @mainClause = '  select g.Phone, e.id, concat (g.FirstName, g.FullName) as fullName, g.Email,  e.JobId as JobId,
  dbo.getFullNameCampagn( f.Campagn)  as CampagnText, 
  f.Name as JobName, e.Status as StatusCode, d.id  as cvId , dbo.getNameMaster(e.Status) as StatusText  , e.CreateAt, d.LinkFile 
  from resumes d inner join jobApply e  on d.Id = e.CVId 
  inner join jobItems f on  e.JobId = f.Id 
  inner join Candidate  g on g.id = d.UserId  ';


if(@JobId >0)
begin 
	set @where += ' and  e.JobId =  @JobId '; 
end

set @where += ' and  e.JobId in  ( select * from   dbo.getAllJobOfHuman(@UserId) ) '; 


if(@TypeData >0)
begin 
	set @where += ' and d.TypeData =  @TypeData '; 
end
if( @Status >-1 )
begin 

	set @where += ' and e.Status =  @Status '; 
end 



 if(@key <> '' )
begin

 set @where += ' and (g.FullName like  N''%' + @key +'%''';
 set @where += ' or g.UserName like  N''%' + @key +'%''';
  set @where += ' or g.FirstName like  N''%' + @key +'%''';
 set @where += ' or g.Phone like  N''%' + @key +'%'')';
end;


if(@CampagnId >0)
begin 
	set @where += ' and  e.JobId in  ( select * from   dbo.getAllJobOfCampagn(@CampagnId) )  '; 
end


set @where +=' order by d.UpdateAt desc'; 
set @where += ' offset @offset ROWS FETCH NEXT @limit ROWS ONLY'
set @mainClause = @mainClause +  @where
set @params =N' @offset int, @limit int, @Status int , @userId int ,  @CampagnId int, 
@JobId int , @TypeData int   ';



EXECUTE sp_executesql @mainClause,@params, @offset = @offset, @limit = @limit,
@Status = @Status,@TypeData =@TypeData ,@JobId = @JobId, @userId = @userId, @CampagnId = @CampagnId

end
  



GO
/****** Object:  StoredProcedure [dbo].[sp_getAllCVByJob]    Script Date: 12/10/2024 11:56:39 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE procedure [dbo].[sp_getAllCVByJob]
(

 @JobId int =null, 
 @UserId  int = NULL, 
 @TypeData int =-1, 
 @page int = 1,
 @Status int =-1,
 @limit int =100
)
as
begin
declare @where  nvarchar(max) = ' where  isnull(d.Deleted,0) = 0';
declare @mainClause nvarchar(max);
declare @params nvarchar(300);
declare @offset int = 0;
set @offset = (@page-1)*@Limit;
set @mainClause = '   select g.Phone,e.id,  concat (g.FirstName, g.FullName) as fullName, g.Email,  e.JobId as JobId,
  dbo.getFullNameCampagn( f.Campagn)  as CampagnText, 
  f.Name as JobName, e.Status as StatusCode, d.id  as cvId ,  e.CreateAt, d.LinkFile, e.ViewMode
  from resumes d inner join jobApply e  on d.Id = e.CVId 
  inner join jobItems f on  e.JobId = f.Id 
  inner join Candidate  g on g.id = d.UserId   ';


if(@JobId >0)
begin 
	set @where += ' and  e.JobId =  @JobId '; 
end
set @where += ' and  e.JobId in  ( select * from  dbo.getAllJobOfHuman(@UserId) ) '; 


if(@TypeData >0)
begin 
	set @where += ' and d.TypeData =  @TypeData '; 
end
if( @Status >-1 )
begin 

	set @where += ' and d.Status =  @Status '; 
end 

set @where +=' order by d.UpdateAt desc'; 
set @where += ' offset @offset ROWS FETCH NEXT @limit ROWS ONLY'
set @mainClause = @mainClause +  @where
set @params =N' @offset int, @limit int, @Status int , @userId int , 
@JobId int , @TypeData int   ';

EXECUTE sp_executesql @mainClause,@params, @offset = @offset, @limit = @limit,
@Status = @Status,@TypeData =@TypeData ,@JobId = @JobId, @userId = @userId



end
  



GO
/****** Object:  StoredProcedure [dbo].[sp_getAllCVCandidate]    Script Date: 12/10/2024 11:56:39 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE procedure [dbo].[sp_getAllCVCandidate]
(

 @UserId  int =null, 
 @TypeData  varchar(30) = NULL 
 
)
as
begin
declare @where  nvarchar(max) = ' where  isnull(d.Deleted,0) = 0';
declare @mainClause nvarchar(max);
declare @params nvarchar(300);

set @mainClause = '  select d.* from resumes d  ';
if(@UserId >0)
begin 
	set @where += ' and d.UserId =  @UserId '; 
end
set @where +=' order by d.CreateAt desc'; 
set @mainClause = @mainClause +  @where
set @params =N' @userId int , @TypeData int ';

EXECUTE sp_executesql @mainClause,@params, @TypeData =@TypeData , @UserId= @UserId

end
  



GO
/****** Object:  StoredProcedure [dbo].[sp_GetAllJobOfCompany]    Script Date: 12/10/2024 11:56:39 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE procedure [dbo].[sp_GetAllJobOfCompany]
(
 @page int = 1,
 @companyId int  = null, 
 @limit int =100
)
as
begin


declare @where  nvarchar(max) = ' where  isnull(e.Deleted,0) = 0';
declare @mainClause nvarchar(max);
declare @params nvarchar(300);

declare @companyName nvarchar(100);
set @companyName ='';
declare @avatarLink varchar(500);
set @avatarLink ='';

select top 1 @companyName = FullName, @avatarLink =  LogoLink from  CompanyInfo where RelId = @companyId

declare @offset int = 0;
set @offset = (@page-1)*@Limit;


set @mainClause = ' select top 100  e.UpdateAt as LastUpdate, e.RangeSalary, e.JobName,
 e.JobName as PositionText, e.Slug as JobSlug, e.LocationText, co.FullName as CompanyName,
co.LogoLink as ShortLinkLogo,
e.LocationText,
e.JobId as jobId,
f.type_money as TypeMoney,
f.aggrement, 
e.ProfessionText as FieldArray,
f.salary_from as SalaryFrom,
f.salary_to as SalaryTo
from jobItemDisplay e inner join CompanyInfo co 
on e.recurId = co.RelId 
inner join jobInfo  f on   f.JobId =  e.JobId  ';


set @where += ' and e.recurId =  @companyId '; 

set @where +=' order by e.UpdateAt desc';
set @mainClause = @mainClause +  @where
set @params =N' @offset int, @limit int , @companyId int , @companyName nvarchar(100) , @avatarLink varchar(500)  ';


EXECUTE sp_executesql @mainClause,@params, @offset = @offset, @limit = @limit,
@companyId = @companyId, @companyName= @companyName, @avatarLink = @avatarLink
end
  


GO
/****** Object:  StoredProcedure [dbo].[sp_getAllNotification]    Script Date: 12/10/2024 11:56:39 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE procedure [dbo].[sp_getAllNotification]
(
	@UserName varchar(30) = '',
	@Source int =2,
	@status int  = -1
)
as
begin

declare @where  nvarchar(max) = ' where  ISNULL(d.Deleted,0) = 0  ';
declare @mainClause nvarchar(max);
declare @params nvarchar(300);

declare @offset int = 0;


set @mainClause = '  select count(d.id) over() as TotalRecord, d.* from  Notification d  ';

if(  @Source > 0)
begin 
	set @where +=	 ' and d.Source =  @Source ';
end 

if( @status > 0)
begin 
	set @where +=	 ' and d.status =  @status ';
end 


if( @UserName <>  '' )
begin 
	set @where +=	 ' and d.UserName =  @UserName  ';
end 

set @where +=' order by d.CreateAt desc'; 

set @mainClause = @mainClause +  @where
set @params =N'   @status int ,  @UserName  varchar(30) , @Source int    ';
EXECUTE sp_executesql @mainClause,@params,
@status = @status, @Source= @Source,
@UserName= @UserName



end
  

GO
/****** Object:  StoredProcedure [dbo].[sp_getInfoOverview]    Script Date: 12/10/2024 11:56:39 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE procedure [dbo].[sp_getInfoOverview]
(

 @JobId int = -1,
 @userId int  = null, 
 @From datetime null , 
 @To datetime null

)
as
begin

declare @where  nvarchar(max) = ' where  isnull(d.Deleted,0) = 0';
declare @mainClause nvarchar(max);
declare @params nvarchar(300);

declare @offset int = 0;

set @mainClause = '  select * from JobOverViewCounter d  ';

declare @idTemp int = 0;

if(@JobId >0)
begin 
	set @where += ' and d.JobId =  @JobId '; 
end

if(@From  is not null)
begin 
	set @where += ' and d.dayReport >= @fromDate '; 
end

if(@To  is not null)
begin 
	set @where += ' and d.dayReport <= @toDate '; 
end

set @where +=' order by d.UpdateAt desc'; 
set @mainClause = @mainClause +  @where
set @params =N' @fromDate datetime, @toDate datetime , 
@JobId  int ,@userId int ';

EXECUTE sp_executesql @mainClause,@params, 
@fromDate = @From,@toDate =@To ,@userId = @userId , @JobId = @JobId

end
  



GO
/****** Object:  StoredProcedure [dbo].[sp_GetMailImp]    Script Date: 12/10/2024 11:56:39 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create procedure [dbo].[sp_GetMailImp]

as
begin
	select top 2 * from  mailInfo where [status] =0
end
  


GO
/****** Object:  StoredProcedure [dbo].[sp_job_getGetAttractiveJobs]    Script Date: 12/10/2024 11:56:39 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE procedure [dbo].[sp_job_getGetAttractiveJobs]
(
	
	@OrderBy varchar(30) = '',
	@Page int =1,
	@Limit int = 10,
	@LocationSearch  varchar(100) = '',

	@From datetime = null,
	@To datetime = null 
)
as
begin

declare @where  nvarchar(max) = ' where  1= 1  ';
declare @mainClause nvarchar(max);
declare @params nvarchar(300);

declare @offset int = 0;
set @offset = (@page-1)*@Limit;

set @mainClause = '  select top 100  e.UpdateAt as LastUpdate, e.SalaryFrom, e.salaryTo, e.RangeSalary, 
 e.JobName as PositionText, e.Slug as JobSlug, e.LocationText, co.FullName as CompanyName,
co.LogoLink as ShortLinkLogo,
e.LocationText,
e.JobId as jobId
from jobItemDisplay e inner join CompanyInfo co 
on e.recurId = co.RelId 
  ';
if( @LocationSearch <> '')
begin 
set @where+= ' and  e.locationSearch in (@LocationSearch) '

end 

set @where +=' order by e.UpdateAt desc'; 

set @mainClause = @mainClause +  @where
set @params =N' @offset int, @limit int,  @From datetime, @To datetime , @LocationSearch varchar(100)   ';
EXECUTE sp_executesql @mainClause,@params, @offset = @offset, @limit = @limit,  @LocationSearch = @LocationSearch,
@From = @From,@To =@To



end
  

GO
/****** Object:  StoredProcedure [dbo].[sp_job_getJobOptimization]    Script Date: 12/10/2024 11:56:39 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE procedure [dbo].[sp_job_getJobOptimization]
(
	
	@OrderBy varchar(30) = '',
	@Page int =1,
	@Limit int = 10,
	@salaryfrom int = -1,
	@salaryto int = -1,
	@professionId int =-1,
	@experienceId int =-1,
	@From datetime = null,
	@To datetime = null 
)
as
begin

declare @where  nvarchar(max) = ' where  1= 1  ';
declare @mainClause nvarchar(max);
declare @params nvarchar(300);

declare @offset int = 0;
set @offset = (@page-1)*@Limit;

set @mainClause = '  select top 100  e.UpdateAt as LastUpdate, e.RangeSalary, e.JobName as JobName,
 e.JobName as PositionText, e.Slug as JobSlug, e.LocationText, co.FullName as CompanyName,
co.LogoLink as ShortLinkLogo,
e.LocationText,
e.JobId as jobId,
f.type_money as TypeMoney,
f.aggrement, 
e.ProfessionText as FieldArray,
f.salary_from as SalaryFrom,
f.salary_to as SalaryTo
from jobItemDisplay e inner join CompanyInfo co 
on e.recurId = co.RelId 
inner join jobInfo  f on   f.JobId =  e.JobId
  ';
if( @salaryfrom > 0)
begin 
 set @where += '  and f.salaryfrom >= ' + @salaryfrom;
end 


if( @salaryto > 0 )
begin 
 set @where += '  and f.salaryTo <= ' + @salaryto;
end 

if(@experienceId > 0 )
begin 
set @where += '  and f.experience = @experienceId ';
end 

if(@professionId > 0 )
begin 
set @where += '  and f.profession = @professionId ';
end 

set @where +=' order by e.UpdateAt desc'; 

set @mainClause = @mainClause +  @where
set @params =N' @offset int, @limit int,  @From datetime, @To datetime , @experienceId int, @professionId int ';
EXECUTE sp_executesql @mainClause,@params, @offset = @offset, @limit = @limit,  
@From = @From,@To =@To, @experienceId = @experienceId, @professionId = @professionId



end
  

GO
/****** Object:  StoredProcedure [dbo].[sp_job_getJobOptimizationByPlace]    Script Date: 12/10/2024 11:56:39 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE procedure [dbo].[sp_job_getJobOptimizationByPlace]
(

	@OrderBy varchar(30) = '',
	@Page int =1,
	@Limit int = 10,
	@searchLocation  int = null,
	@From datetime = null,
	@To datetime = null 
)
as
begin

declare @where  nvarchar(max) = ' where  1= 1  ';
declare @mainClause nvarchar(max);
declare @params nvarchar(300);

declare @offset int = 0;
set @offset = (@page-1)*@Limit;

set @mainClause = '  select top 100  e.UpdateAt as LastUpdate, e.SalaryFrom, e.salaryTo, e.RangeSalary, 
 e.JobName as PositionText, e.Slug as JobSlug, e.LocationText, co.FullName as CompanyName,
co.LogoLink as ShortLinkLogo,
e.ProfessionText as FieldArray,
e.LocationText,
e.JobId as jobId
from jobItemDisplay e inner join CompanyInfo co 
on e.recurId = co.RelId 
  ';

declare @LocationSearch varchar(100);
set @LocationSearch = '';

select top 1 @LocationSearch = slug from regionals where Code = @searchLocation



if( @LocationSearch <> '')
begin 
set @where+= ' and  e.locationSearch in (@LocationSearch) '

end 


set @where +=' order by e.UpdateAt desc'; 

set @mainClause = @mainClause +  @where
set @params =N' @offset int, @limit int,  @From datetime, @To datetime , @LocationSearch varchar(100)  ';
EXECUTE sp_executesql @mainClause,@params, @offset = @offset, @limit = @limit, @LocationSearch  = @LocationSearch, 
@From = @From,@To =@To



end
  

GO
/****** Object:  StoredProcedure [dbo].[sp_job_getRelation]    Script Date: 12/10/2024 11:56:39 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE procedure [dbo].[sp_job_getRelation]
(
	@Slug varchar(100) null
)
as
begin

declare @where  nvarchar(max) = ' where  1= 1  ';
declare @mainClause nvarchar(max);
declare @params nvarchar(300);

declare @offset int =0;



declare @jobId int ;
set @jobId =0;

declare @profession int;
set @profession =0;

select top 1 @jobId = Id   from  jobItems where Slug = @Slug


select top 1  @profession = profession  from jobInfo where JobId = @jobId


set @mainClause = '  select top 10  e.UpdateAt as LastUpdate, e.RangeSalary, 
 e.JobName as PositionText, e.Slug as JobSlug, e.LocationText, co.FullName as CompanyName,
co.LogoLink as ShortLinkLogo,
e.LocationText,
e.JobId as jobId,
f.type_money as TypeMoney,
e.ProfessionText as FieldArray,
f.aggrement, 
f.salary_from as SalaryFrom,
f.salary_to as SalaryTo
from jobItemDisplay e inner join CompanyInfo co 
on e.recurId = co.RelId 
inner join jobInfo  f on   f.JobId =  e.JobId 
  ';


if(@profession > 0)
begin 

 set @where += ' and  f.profession = @profession  '

end 

if( @jobId < 0)
begin 
		set @where += ' and  e.JobId <> @jobId  '
end 

set @where +=' order by e.UpdateAt desc'; 

set @mainClause = @mainClause +  @where
set @params =N' @offset int,   @Slug varchar(100) , @jobId int , @profession int  ';
EXECUTE sp_executesql @mainClause,@params, @offset = @offset,   @Slug = @Slug,  @jobId = @jobId, @profession= @profession



end
  

GO
/****** Object:  StoredProcedure [dbo].[sp_job_getSuitableJob]    Script Date: 12/10/2024 11:56:39 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE procedure [dbo].[sp_job_getSuitableJob]
(
	
	@OrderBy varchar(30) = '',
	@Page int =1,
	@Limit int = 10,
	@LocationSearch  varchar(100) = '',

	@From datetime = null,
	@To datetime = null 
)
as
begin

declare @where  nvarchar(max) = ' where  1= 1  ';
declare @mainClause nvarchar(max);
declare @params nvarchar(300);

declare @offset int = 0;
set @offset = (@page-1)*@Limit;

set @mainClause = '  select top 100  e.UpdateAt as LastUpdate, e.SalaryFrom, e.salaryTo, e.RangeSalary, 
 e.JobName as PositionText, e.Slug as JobSlug, e.LocationText, co.FullName as CompanyName,
co.LogoLink as ShortLinkLogo,
e.ProfessionText as FieldArray,
e.LocationText,
e.JobId as jobId
from jobItemDisplay e inner join CompanyInfo co 
on e.recurId = co.RelId 
  ';
if( @LocationSearch <> '')
begin 
set @where+= ' and  e.locationSearch in (@LocationSearch) '

end 

set @where +=' order by e.UpdateAt desc'; 

set @mainClause = @mainClause +  @where
set @params =N' @offset int, @limit int,  @From datetime, @To datetime , @LocationSearch varchar(100)   ';
EXECUTE sp_executesql @mainClause,@params, @offset = @offset, @limit = @limit,  @LocationSearch = @LocationSearch,
@From = @From,@To =@To



end
  

GO
/****** Object:  StoredProcedure [dbo].[sp_job_searchAll]    Script Date: 12/10/2024 11:56:39 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE procedure [dbo].[sp_job_searchAll]
(
	
	@OrderBy varchar(30) = '',
	@Page int =1,
	@Limit int = 10,
	@UserId int =-1, 
	@CampagnId int = -1, 
	@status int  = -1, 
	
	@From datetime = null,
	@To datetime = null 
)
as
begin

declare @where  nvarchar(max) = ' where  ISNULL(d.Deleted,0) = 0  ';
declare @mainClause nvarchar(max);
declare @params nvarchar(300);

declare @offset int = 0;
set @offset = (@page-1)*@Limit;

set @mainClause = '  select count(d.id) over() as TotalRecord, d.* ,
d.Campagn as CampaignId,
dbo.getFullNameCampagn(d.Campagn) as CampaignName

from  jobItems d  ';

if( @status > -1)
begin 
	set @where +=	 ' and d.status =  @status ';
end 

if( @CampagnId > -1)
begin 
	set @where +=	 ' and d.Campagn =  @CampagnId ';
end 

if( @UserId > -1 )
begin 
	set @where +=	 ' and d.RelId =  @UserId  ';
end 


set @where +=' order by d.UpdateAt desc'; 
set @where += ' offset @offset ROWS FETCH NEXT @limit ROWS ONLY'
set @mainClause = @mainClause +  @where
set @params =N' @offset int, @limit int, @CampagnId int,  @fromDate datetime,@toDate datetime , @status int ,  @UserId  int  ';
EXECUTE sp_executesql @mainClause,@params, @offset = @offset, @limit = @limit, @status = @status,
@fromDate = @From,@toDate =@To, @UserId= @UserId, @CampagnId = @CampagnId



end
  

GO
/****** Object:  StoredProcedure [dbo].[sp_jobLogview_searchAll]    Script Date: 12/10/2024 11:56:39 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


create procedure [dbo].[sp_jobLogview_searchAll]
(
	
	@OrderBy varchar(30) = '',
	@JobId int =-1, 

	@Page int =1,
	@Limit int = 10,
	@UserId int =-1, 
	@CampagnId int = -1, 
	@status int  = -1, 
	
	@From datetime = null,
	@To datetime = null 
)
as
begin

declare @where  nvarchar(max) = ' where  ISNULL(d.Deleted,0) = 0  ';
declare @mainClause nvarchar(max);
declare @params nvarchar(300);

declare @offset int = 0;
set @offset = (@page-1)*@Limit;

set @mainClause = ' select concat(e.FirstName, e.FullName) as FullName,
e.Phone, e.Email, d.*  
from  JobLogView  d 
inner join Candidate e on d.userId = e.id  ';




if( @JobId > -1 )
begin 
	set @where +=	 ' and d.RelId =  @UserId  ';
end 


set @where +=' order by d.UpdateAt desc'; 
set @where += ' offset @offset ROWS FETCH NEXT @limit ROWS ONLY'
set @mainClause = @mainClause +  @where
set @params =N' @offset int, @limit int, @CampagnId int,  @fromDate datetime,@toDate datetime , @status int ,  @UserId  int  ';
EXECUTE sp_executesql @mainClause,@params, @offset = @offset, @limit = @limit, @status = @status,
@fromDate = @From,@toDate =@To, @UserId= @UserId, @CampagnId = @CampagnId



end
  

GO
/****** Object:  StoredProcedure [dbo].[sp_Recruiter_register]    Script Date: 12/10/2024 11:56:39 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE procedure [dbo].[sp_Recruiter_register]
(
 @UserName  varchar(30) NULL, 
 @Pass  varchar(30) NULL, 
 @Name nvarchar(50) null,

 @Email varchar(30) null, 
 @TaxCode varchar(30) null,
 @Phone varchar(15) null,
 @CreatedBy  int null 
)
as
begin
     
		INSERT INTO [dbo].[Recruiter]
           ([Deleted]
           ,[Status]
           ,[CreateAt]
           ,[CreatedBy]
           ,[UpdateAt]
           ,[UpdatedBy]
           ,[Phone]
           ,[Name]
           ,[Taxcode]
           ,[Email]
           ,[UserName]
           ,[Password]
           ,[DateRegister])
     VALUES
           (0
           ,0
           ,getdate()
           ,@CreatedBy
           ,getdate()
               ,@CreatedBy
                ,@Phone
           ,@Name
           ,@TaxCode
           ,@Email
           ,@UserName
           ,@Pass
           ,getdate())

	 declare @idInsert int =0;

	 SELECT top 1 @idInsert = Id  FROM Recruiter WHERE Email = @Email

	 if( @idInsert >0)
	 begin 
			INSERT INTO [dbo].[RecruiterInfo]
           ([levelAuthen]
           ,[DateActive]
           ,[Email]
           ,[RelId]
           ,[AvatarLink]
           ,[Deleted]
           ,[Status]
           ,[CreateAt]
           ,[CreatedBy]
           ,[UpdateAt]
           ,[UpdatedBy])
     VALUES
           (null
           ,null
           ,@Email
           ,@idInsert
           ,''
           ,0
           ,0
           ,getdate()
           ,@CreatedBy
           ,getdate()
           ,@CreatedBy)

	 end 

end
  


GO
/****** Object:  StoredProcedure [dbo].[sp_Recruiter_search]    Script Date: 12/10/2024 11:56:39 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create procedure [dbo].[sp_Recruiter_search]
(
 @Email  varchar(30) NULL, 
 @Password  varchar(30) NULL

)
as
begin
 
 select top 1 * from  Recruiter where email = @Email and [Password] = @Password


end
  


GO
/****** Object:  StoredProcedure [dbo].[sp_regional_insert]    Script Date: 12/10/2024 11:56:39 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



CREATE procedure [dbo].[sp_regional_insert]
(
  @code varchar(5) null,
  @Name nvarchar(100) null, 
  @Level1 varchar(4) null, 
  @Level2 varchar(4) null, 
  @Slug varchar(30) null, 

  @datatype int null

)
as
begin
INSERT INTO [dbo].[regionals]
           ([Deleted]
           ,[Status]
           ,[CreateAt]
           ,[CreatedBy]
           ,[UpdateAt]
           ,[UpdatedBy]
           ,[Code]
           ,[Name]
           ,[Slug]
           ,[Level1]
           ,[Level2]
		   ,datatype
		   )
     VALUES
           (
		    0, 1, getdate(),1, getdate(), 1, @code, @Name, @Slug, @Level1, @Level2,@datatype
		   
		   )
  
end
  



GO
/****** Object:  StoredProcedure [dbo].[sp_regionals_getall]    Script Date: 12/10/2024 11:56:39 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE procedure [dbo].[sp_regionals_getall]
(
	
	@OrderBy varchar(30) = '',
	@Level1 varchar(10) = '',
	@Type int  = 1, 
	@Level2 varchar(10) = 10,
	@Slug varchar(100)  = null
	
)
as
begin

declare @where  nvarchar(max) = ' where  1=1  ';
declare @mainClause nvarchar(max);
declare @params nvarchar(300);



set @mainClause = '  select * from  regionals  d ';

if( @Type > -1)
begin 
	set @where +=	 ' and d.datatype =  @Type ';
end 

if( @Level1 <>  '' )
begin 
	set @where +=	 ' and d.Level1 = @Level1  ';
end 

set @where +=' order by d.Name asc'; 

set @mainClause = @mainClause +  @where
set @params =N' @Type int, @Level1 varchar(10), @Level2 varchar(10)  ';
EXECUTE sp_executesql @mainClause,@params, @Type = @Type,  @Level1 =  @Level1, @Level2  = @Level2 



end
  

GO
/****** Object:  StoredProcedure [dbo].[sp_search_job]    Script Date: 12/10/2024 11:56:39 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE procedure [dbo].[sp_search_job]
(
	
	@OrderBy varchar(30) = '',
	@Page int =1,
	@Limit int = 10,
	@TypeOfWork int = -1,
	@KeyWord nvarchar(100) = -1,
	@Field int =-1,
	@Location int =-1,
	@RankLevel int =-1

)
as
begin

declare @where  nvarchar(max) = ' where  1= 1  ';
declare @mainClause nvarchar(max);
declare @params nvarchar(300);

declare @offset int = 0;
set @offset = (@page-1)*@Limit;

set @mainClause = '  select top 100  e.UpdateAt as LastUpdate, e.RangeSalary, 
 e.JobName as PositionText, e.Slug as JobSlug, e.LocationText, co.FullName as CompanyName,
co.LogoLink as ShortLinkLogo,
e.LocationText,
e.JobId as jobId,
f.type_money as TypeMoney,
f.aggrement, 
f.salary_from as SalaryFrom,
f.salary_to as SalaryTo
from jobItemDisplay e inner join CompanyInfo co 
on e.recurId = co.RelId 
inner join jobInfo  f on   f.JobId =  e.JobId
  ';
if( @RankLevel > 0)
begin 
 set @where += '  and f.rank == @RankLevel ';
end 

if( @Field > 0)
begin 
 set @where += '  and f.profession = @Field ';
end 

declare @LocationSearch varchar(100);
set @LocationSearch = '';

select top 1 @LocationSearch = slug from regionals where Code = @Location



if( @LocationSearch <> '')
begin 
set @where+= ' and  e.locationSearch in (@LocationSearch) '

end 
if(@KeyWord <> '' )
begin

 set @where += ' and (f.name like  N''%' + @KeyWord +'%''';
 set @where += ' or f.FullName like  N''%' + @KeyWord +'%'')';

end;

set @where +=' order by e.UpdateAt desc'; 

set @mainClause = @mainClause +  @where


set @params =N' @offset int, @limit int , @RankLevel int, @Field int , @KeyWord nvarchar(100), @LocationSearch  varchar(100)  ';
EXECUTE sp_executesql @mainClause,@params, @offset = @offset, @limit = @limit,  
 @RankLevel = @RankLevel, @Field = @Field, @KeyWord = @KeyWord, @LocationSearch = @LocationSearch 

end
  

GO
/****** Object:  StoredProcedure [dbo].[sp_searchJobofCampagn]    Script Date: 12/10/2024 11:56:39 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE procedure [dbo].[sp_searchJobofCampagn]
(
	
	@OrderBy varchar(30) = '',
	@Page int =1,
	@Limit int = 10,
	@status int  = -1, 
	@email  varchar(30) null,
	@From datetime = null,
	@CampagnId int =-1, 
	@To datetime = null 
)
as
begin

declare @where  nvarchar(max) = ' where  ISNULL(d.Deleted,0) = 0  ';
declare @mainClause nvarchar(max);
declare @params nvarchar(300);

declare @offset int = 0;
set @offset = (@page-1)*@Limit;

set @mainClause = '  select count(d.id) over() as TotalRecord, d.* from  jobItems d  ';

if( @status > -1)
begin 
	set @where +=	 ' and d.status =  @status ';
end 

if( @CampagnId > -1)
begin 
	set @where +=	 ' and d.CampagnId =  @CampagnId ';
end 

if( @email <>  '' )
begin 
	set @where +=	 ' and d.email =  @email  ';
end 


set @where +=' order by d.UpdateAt desc'; 
set @where += ' offset @offset ROWS FETCH NEXT @limit ROWS ONLY'
set @mainClause = @mainClause +  @where
set @params =N' @offset int, @limit int, @CampagnId int,  @fromDate datetime,@toDate datetime , @status int ,  @email  varchar(30)   ';
EXECUTE sp_executesql @mainClause,@params, @offset = @offset, @limit = @limit, @status = @status,
@fromDate = @From,@toDate =@To, @email= @email, @CampagnId = @CampagnId



end
  

GO
/****** Object:  StoredProcedure [dbo].[sp_user_getallJobSave]    Script Date: 12/10/2024 11:56:39 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE procedure [dbo].[sp_user_getallJobSave]
(
	
	@UserId int null
)
as
begin
select e.UpdateAt as LastUpdate, e.salaryfrom, e.salaryTo, e.RangeSalary, 
f.Id, e.JobName as PositionText, e.Slug as JobSlug, e.LocationText,
co.LogoLink as ShortLinkLogo,

e.JobId as jobId,
f.CreateAt as BusinessDate, 
co.FullName as CompanyName, e.ProfessionText as FieldArray
from jobItemDisplay e inner join jobSave f 
on e.JobId = f.JobId
inner join CompanyInfo  co  on e.recurId = co.RelId
where f.UserId = @UserId and   ISNULL( f.Deleted,0) =0
end
  

GO
/****** Object:  StoredProcedure [dbo].[sql_getAllCompany]    Script Date: 12/10/2024 11:56:39 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE procedure [dbo].[sql_getAllCompany]
(

 @UserId  int =null, 
 @KeyWord varchar(10)  = '',
 @Status int =-1,
 @limit int =100,
  @page int = 1
)
as
begin
declare @where  nvarchar(max) = ' where  1 = 1 ';
declare @mainClause nvarchar(max);
declare @params nvarchar(300);
declare @offset int = 0;
set @offset = (@page-1)*@Limit;
set @mainClause = ' select e.CoverLink, e.LogoLink,e.id,   e.FullName,  e.slug as slug 
		from CompanyInfo e inner join Recruiter f 
		on  e.RelId = f.Id   and e.FullName is not null ';



if( @KeyWord  <>  '' )
begin

 set @where += ' and (e.FullName like  N''%' + @KeyWord +'%''';
 set @where += ' or e.Email like  N''%' + @KeyWord +'%'')';
end;


if(@UserId >0)
begin 
	set @where += ' and e.RelId =  @UserId '; 
end


set @where +=' order by f.CreateAt desc'; 
set @where += ' offset @offset ROWS FETCH NEXT @limit ROWS ONLY'
set @mainClause = @mainClause +  @where
set @params =N' @offset int, @limit int, @Status int , @userId int  ';

EXECUTE sp_executesql @mainClause,@params, @offset = @offset, @limit = @limit,
@Status = @Status, @UserId = @UserId

end
  



GO
