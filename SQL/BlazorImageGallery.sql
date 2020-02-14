USE [BlazorImageGallery]
GO
/****** Object:  Table [dbo].[Artist]    Script Date: 2/13/2020 10:59:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Artist](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](40) NOT NULL,
	[EmailAddress] [nvarchar](80) NOT NULL,
	[PasswordHash] [nvarchar](255) NOT NULL,
	[ProfilePicture] [nvarchar](255) NULL,
	[FolderPath] [nvarchar](255) NOT NULL,
	[ImagesCount] [int] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[LastUpdated] [datetime] NOT NULL,
	[IsAdmin] [bit] NOT NULL,
	[Active] [bit] NOT NULL,
 CONSTRAINT [PK_Artist] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Image]    Script Date: 2/13/2020 10:59:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Image](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](40) NOT NULL,
	[FullPath] [nvarchar](512) NOT NULL,
	[Extension] [nvarchar](10) NOT NULL,
	[ImageUrl] [nvarchar](255) NOT NULL,
	[SitePath] [nvarchar](255) NOT NULL,
	[OwnerId] [int] NOT NULL,
	[FileSize] [int] NOT NULL,
	[Height] [int] NOT NULL,
	[Width] [int] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[Visible] [bit] NOT NULL,
	[ImageNumber] [int] NULL,
 CONSTRAINT [PK_Image] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Artist] ADD  CONSTRAINT [DF_Artist_Active]  DEFAULT ((1)) FOR [Active]
GO
ALTER TABLE [dbo].[Image] ADD  CONSTRAINT [DF_Image_Visible]  DEFAULT ((1)) FOR [Visible]
GO
/****** Object:  StoredProcedure [dbo].[Artist_Delete]    Script Date: 2/13/2020 10:59:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create PROCEDURE [dbo].[Artist_Delete]

    -- Primary Key Paramater
    @Id int

AS
BEGIN

    -- SET NOCOUNT ON added to prevent extra result sets from
    -- interfering with SELECT statements.
    SET NOCOUNT ON

    -- Begin Delete Statement
    Delete From [Artist]

    -- Delete Matching Record
    Where [Id] = @Id

END

set ANSI_NULLS ON
set QUOTED_IDENTIFIER ON
GO
/****** Object:  StoredProcedure [dbo].[Artist_FetchAll]    Script Date: 2/13/2020 10:59:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create PROCEDURE [dbo].[Artist_FetchAll]

AS
BEGIN

    -- SET NOCOUNT ON added to prevent extra result sets from
    -- interfering with SELECT statements.
    SET NOCOUNT ON

    -- Begin Select Statement
    Select [Active],[CreatedDate],[EmailAddress],[FolderPath],[Id],[ImagesCount],[IsAdmin],[LastUpdated],[Name],[PasswordHash],[ProfilePicture]

    -- From tableName
    From [Artist]

END

set ANSI_NULLS ON
set QUOTED_IDENTIFIER ON
GO
/****** Object:  StoredProcedure [dbo].[Artist_Find]    Script Date: 2/13/2020 10:59:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create PROCEDURE [dbo].[Artist_Find]

    -- Primary Key Paramater
    @Id int

AS
BEGIN

    -- SET NOCOUNT ON added to prevent extra result sets from
    -- interfering with SELECT statements.
    SET NOCOUNT ON

    -- Begin Select Statement
    Select [Active],[CreatedDate],[EmailAddress],[FolderPath],[Id],[ImagesCount],[IsAdmin],[LastUpdated],[Name],[PasswordHash],[ProfilePicture]

    -- From tableName
    From [Artist]

    -- Find Matching Record
    Where [Id] = @Id

END

set ANSI_NULLS ON
set QUOTED_IDENTIFIER ON
GO
/****** Object:  StoredProcedure [dbo].[Artist_FindByEmailAddress]    Script Date: 2/13/2020 10:59:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create PROCEDURE [dbo].[Artist_FindByEmailAddress]

    -- Create @EmailAddress Paramater
    @EmailAddress nvarchar(80)

AS
BEGIN

    -- SET NOCOUNT ON added to prevent extra result sets from
    -- interfering with SELECT statements.
    SET NOCOUNT ON

    -- Begin Select Statement
    Select [Active],[CreatedDate],[EmailAddress],[FolderPath],[Id],[ImagesCount],[IsAdmin],[LastUpdated],[Name],[PasswordHash],[ProfilePicture]

    -- From tableName
    From [Artist]

    -- Find Matching Record
    Where [EmailAddress] = @EmailAddress

END

set ANSI_NULLS ON
set QUOTED_IDENTIFIER ON
GO
/****** Object:  StoredProcedure [dbo].[Artist_Insert]    Script Date: 2/13/2020 10:59:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create PROCEDURE [dbo].[Artist_Insert]

    -- Add the parameters for the stored procedure here
    @Active bit,
    @CreatedDate datetime,
    @EmailAddress nvarchar(80),
    @FolderPath nvarchar(255),
    @ImagesCount int,
    @IsAdmin bit,
    @LastUpdated datetime,
    @Name nvarchar(40),
    @PasswordHash nvarchar(255),
    @ProfilePicture nvarchar(255)

AS
BEGIN

    -- SET NOCOUNT ON added to prevent extra result sets from
    -- interfering with SELECT statements.
    SET NOCOUNT ON

    -- Begin Insert Statement
    Insert Into [Artist]
    ([Active],[CreatedDate],[EmailAddress],[FolderPath],[ImagesCount],[IsAdmin],[LastUpdated],[Name],[PasswordHash],[ProfilePicture])

    -- Begin Values List
    Values(@Active, @CreatedDate, @EmailAddress, @FolderPath, @ImagesCount, @IsAdmin, @LastUpdated, @Name, @PasswordHash, @ProfilePicture)

    -- Return ID of new record
    SELECT SCOPE_IDENTITY()

END

set ANSI_NULLS ON
set QUOTED_IDENTIFIER ON
GO
/****** Object:  StoredProcedure [dbo].[Artist_Update]    Script Date: 2/13/2020 10:59:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create PROCEDURE [dbo].[Artist_Update]

    -- Add the parameters for the stored procedure here
    @Active bit,
    @CreatedDate datetime,
    @EmailAddress nvarchar(80),
    @FolderPath nvarchar(255),
    @Id int,
    @ImagesCount int,
    @IsAdmin bit,
    @LastUpdated datetime,
    @Name nvarchar(40),
    @PasswordHash nvarchar(255),
    @ProfilePicture nvarchar(255)

AS
BEGIN

    -- SET NOCOUNT ON added to prevent extra result sets from
    -- interfering with SELECT statements.
    SET NOCOUNT ON

    -- Begin Update Statement
    Update [Artist]

    -- Update Each field
    Set [Active] = @Active,
    [CreatedDate] = @CreatedDate,
    [EmailAddress] = @EmailAddress,
    [FolderPath] = @FolderPath,
    [ImagesCount] = @ImagesCount,
    [IsAdmin] = @IsAdmin,
    [LastUpdated] = @LastUpdated,
    [Name] = @Name,
    [PasswordHash] = @PasswordHash,
    [ProfilePicture] = @ProfilePicture

    -- Update Matching Record
    Where [Id] = @Id

END

set ANSI_NULLS ON
set QUOTED_IDENTIFIER ON
GO
/****** Object:  StoredProcedure [dbo].[Image_Delete]    Script Date: 2/13/2020 10:59:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create PROCEDURE [dbo].[Image_Delete]

    -- Primary Key Paramater
    @Id int

AS
BEGIN

    -- SET NOCOUNT ON added to prevent extra result sets from
    -- interfering with SELECT statements.
    SET NOCOUNT ON

    -- Begin Delete Statement
    Delete From [Image]

    -- Delete Matching Record
    Where [Id] = @Id

END

set ANSI_NULLS ON
set QUOTED_IDENTIFIER ON
GO
/****** Object:  StoredProcedure [dbo].[Image_FetchAll]    Script Date: 2/13/2020 10:59:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create PROCEDURE [dbo].[Image_FetchAll]

AS
BEGIN

    -- SET NOCOUNT ON added to prevent extra result sets from
    -- interfering with SELECT statements.
    SET NOCOUNT ON

    -- Begin Select Statement
    Select [CreatedDate],[Extension],[FileSize],[FullPath],[Height],[Id],[ImageNumber],[ImageUrl],[Name],[OwnerId],[SitePath],[Visible],[Width]

    -- From tableName
    From [Image]

END

-- Begin Custom Methods


set ANSI_NULLS ON
set QUOTED_IDENTIFIER ON
GO
/****** Object:  StoredProcedure [dbo].[Image_FetchAllForOwnerId]    Script Date: 2/13/2020 10:59:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Image_FetchAllForOwnerId]

    -- Create @OwnerId Paramater
    @OwnerId int


AS
BEGIN

    -- SET NOCOUNT ON added to prevent extra result sets from
    -- interfering with SELECT statements.
    SET NOCOUNT ON

    -- Begin Select Statement
    Select [CreatedDate],[Extension],[FileSize],[FullPath],[Height],[Id],[ImageNumber],[ImageUrl],[Name],[OwnerId],[SitePath],[Visible],[Width]

    -- From tableName
    From [Image]

    -- Load Matching Records
    Where [OwnerId] = @OwnerId

	-- Order By ImageNumber
	Order By ImageNumber

END


-- End Custom Methods

-- Thank you for using DataTier.Net.

GO
/****** Object:  StoredProcedure [dbo].[Image_Find]    Script Date: 2/13/2020 10:59:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create PROCEDURE [dbo].[Image_Find]

    -- Primary Key Paramater
    @Id int

AS
BEGIN

    -- SET NOCOUNT ON added to prevent extra result sets from
    -- interfering with SELECT statements.
    SET NOCOUNT ON

    -- Begin Select Statement
    Select [CreatedDate],[Extension],[FileSize],[FullPath],[Height],[Id],[ImageNumber],[ImageUrl],[Name],[OwnerId],[SitePath],[Visible],[Width]

    -- From tableName
    From [Image]

    -- Find Matching Record
    Where [Id] = @Id

END

set ANSI_NULLS ON
set QUOTED_IDENTIFIER ON
GO
/****** Object:  StoredProcedure [dbo].[Image_Insert]    Script Date: 2/13/2020 10:59:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create PROCEDURE [dbo].[Image_Insert]

    -- Add the parameters for the stored procedure here
    @CreatedDate datetime,
    @Extension nvarchar(10),
    @FileSize int,
    @FullPath nvarchar(512),
    @Height int,
    @ImageNumber int,
    @ImageUrl nvarchar(255),
    @Name nvarchar(40),
    @OwnerId int,
    @SitePath nvarchar(255),
    @Visible bit,
    @Width int

AS
BEGIN

    -- SET NOCOUNT ON added to prevent extra result sets from
    -- interfering with SELECT statements.
    SET NOCOUNT ON

    -- Begin Insert Statement
    Insert Into [Image]
    ([CreatedDate],[Extension],[FileSize],[FullPath],[Height],[ImageNumber],[ImageUrl],[Name],[OwnerId],[SitePath],[Visible],[Width])

    -- Begin Values List
    Values(@CreatedDate, @Extension, @FileSize, @FullPath, @Height, @ImageNumber, @ImageUrl, @Name, @OwnerId, @SitePath, @Visible, @Width)

    -- Return ID of new record
    SELECT SCOPE_IDENTITY()

END

set ANSI_NULLS ON
set QUOTED_IDENTIFIER ON
GO
/****** Object:  StoredProcedure [dbo].[Image_Update]    Script Date: 2/13/2020 10:59:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create PROCEDURE [dbo].[Image_Update]

    -- Add the parameters for the stored procedure here
    @CreatedDate datetime,
    @Extension nvarchar(10),
    @FileSize int,
    @FullPath nvarchar(512),
    @Height int,
    @Id int,
    @ImageNumber int,
    @ImageUrl nvarchar(255),
    @Name nvarchar(40),
    @OwnerId int,
    @SitePath nvarchar(255),
    @Visible bit,
    @Width int

AS
BEGIN

    -- SET NOCOUNT ON added to prevent extra result sets from
    -- interfering with SELECT statements.
    SET NOCOUNT ON

    -- Begin Update Statement
    Update [Image]

    -- Update Each field
    Set [CreatedDate] = @CreatedDate,
    [Extension] = @Extension,
    [FileSize] = @FileSize,
    [FullPath] = @FullPath,
    [Height] = @Height,
    [ImageNumber] = @ImageNumber,
    [ImageUrl] = @ImageUrl,
    [Name] = @Name,
    [OwnerId] = @OwnerId,
    [SitePath] = @SitePath,
    [Visible] = @Visible,
    [Width] = @Width

    -- Update Matching Record
    Where [Id] = @Id

END

set ANSI_NULLS ON
set QUOTED_IDENTIFIER ON
GO
