Use [BlazorImageGallery]

set ANSI_NULLS ON
set QUOTED_IDENTIFIER ON
Go
-- =========================================================
-- Procure Name: Artist_Insert
-- Author:           Data Juggler - Data Tier.Net Procedure Generator
-- Create Date:   1/10/2020
-- Description:    Insert a new Artist
-- =========================================================

-- Check if the procedure already exists
IF EXISTS (select * from syscomments where id = object_id ('Artist_Insert'))

    -- Procedure Does Exist, Drop First
    BEGIN

        -- Execute Drop
        Drop Procedure Artist_Insert

        -- Test if procedure was dropped
        IF OBJECT_ID('dbo.Artist_Insert') IS NOT NULL

            -- Print Line Drop Failed
            PRINT '<<< Drop Failed On Procedure Artist_Insert >>>'

        Else

            -- Print Line Procedure Dropped
            PRINT '<<< Drop Suceeded On Procedure Artist_Insert >>>'

    End

GO

Create PROCEDURE Artist_Insert

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
Go
-- =========================================================
-- Procure Name: Artist_Update
-- Author:           Data Juggler - Data Tier.Net Procedure Generator
-- Create Date:   1/10/2020
-- Description:    Update an existing Artist
-- =========================================================

-- Check if the procedure already exists
IF EXISTS (select * from syscomments where id = object_id ('Artist_Update'))

    -- Procedure Does Exist, Drop First
    BEGIN

        -- Execute Drop
        Drop Procedure Artist_Update

        -- Test if procedure was dropped
        IF OBJECT_ID('dbo.Artist_Update') IS NOT NULL

            -- Print Line Drop Failed
            PRINT '<<< Drop Failed On Procedure Artist_Update >>>'

        Else

            -- Print Line Procedure Dropped
            PRINT '<<< Drop Suceeded On Procedure Artist_Update >>>'

    End

GO

Create PROCEDURE Artist_Update

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
Go
-- =========================================================
-- Procure Name: Artist_Find
-- Author:           Data Juggler - Data Tier.Net Procedure Generator
-- Create Date:   1/10/2020
-- Description:    Find an existing Artist
-- =========================================================

-- Check if the procedure already exists
IF EXISTS (select * from syscomments where id = object_id ('Artist_Find'))

    -- Procedure Does Exist, Drop First
    BEGIN

        -- Execute Drop
        Drop Procedure Artist_Find

        -- Test if procedure was dropped
        IF OBJECT_ID('dbo.Artist_Find') IS NOT NULL

            -- Print Line Drop Failed
            PRINT '<<< Drop Failed On Procedure Artist_Find >>>'

        Else

            -- Print Line Procedure Dropped
            PRINT '<<< Drop Suceeded On Procedure Artist_Find >>>'

    End

GO

Create PROCEDURE Artist_Find

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
Go
-- =========================================================
-- Procure Name: Artist_Delete
-- Author:           Data Juggler - Data Tier.Net Procedure Generator
-- Create Date:   1/10/2020
-- Description:    Delete an existing Artist
-- =========================================================

-- Check if the procedure already exists
IF EXISTS (select * from syscomments where id = object_id ('Artist_Delete'))

    -- Procedure Does Exist, Drop First
    BEGIN

        -- Execute Drop
        Drop Procedure Artist_Delete

        -- Test if procedure was dropped
        IF OBJECT_ID('dbo.Artist_Delete') IS NOT NULL

            -- Print Line Drop Failed
            PRINT '<<< Drop Failed On Procedure Artist_Delete >>>'

        Else

            -- Print Line Procedure Dropped
            PRINT '<<< Drop Suceeded On Procedure Artist_Delete >>>'

    End

GO

Create PROCEDURE Artist_Delete

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
Go
-- =========================================================
-- Procure Name: Artist_FetchAll
-- Author:           Data Juggler - Data Tier.Net Procedure Generator
-- Create Date:   1/10/2020
-- Description:    Returns all Artist objects
-- =========================================================

-- Check if the procedure already exists
IF EXISTS (select * from syscomments where id = object_id ('Artist_FetchAll'))

    -- Procedure Does Exist, Drop First
    BEGIN

        -- Execute Drop
        Drop Procedure Artist_FetchAll

        -- Test if procedure was dropped
        IF OBJECT_ID('dbo.Artist_FetchAll') IS NOT NULL

            -- Print Line Drop Failed
            PRINT '<<< Drop Failed On Procedure Artist_FetchAll >>>'

        Else

            -- Print Line Procedure Dropped
            PRINT '<<< Drop Suceeded On Procedure Artist_FetchAll >>>'

    End

GO

Create PROCEDURE Artist_FetchAll

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
Go
-- =========================================================
-- Procure Name: Image_Insert
-- Author:           Data Juggler - Data Tier.Net Procedure Generator
-- Create Date:   1/10/2020
-- Description:    Insert a new Image
-- =========================================================

-- Check if the procedure already exists
IF EXISTS (select * from syscomments where id = object_id ('Image_Insert'))

    -- Procedure Does Exist, Drop First
    BEGIN

        -- Execute Drop
        Drop Procedure Image_Insert

        -- Test if procedure was dropped
        IF OBJECT_ID('dbo.Image_Insert') IS NOT NULL

            -- Print Line Drop Failed
            PRINT '<<< Drop Failed On Procedure Image_Insert >>>'

        Else

            -- Print Line Procedure Dropped
            PRINT '<<< Drop Suceeded On Procedure Image_Insert >>>'

    End

GO

Create PROCEDURE Image_Insert

    -- Add the parameters for the stored procedure here
    @CreatedDate datetime,
    @Extension nvarchar(10),
    @FileSize int,
    @FullPath nvarchar(512),
    @Height int,
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
    ([CreatedDate],[Extension],[FileSize],[FullPath],[Height],[ImageUrl],[Name],[OwnerId],[SitePath],[Visible],[Width])

    -- Begin Values List
    Values(@CreatedDate, @Extension, @FileSize, @FullPath, @Height, @ImageUrl, @Name, @OwnerId, @SitePath, @Visible, @Width)

    -- Return ID of new record
    SELECT SCOPE_IDENTITY()

END

set ANSI_NULLS ON
set QUOTED_IDENTIFIER ON
Go
-- =========================================================
-- Procure Name: Image_Update
-- Author:           Data Juggler - Data Tier.Net Procedure Generator
-- Create Date:   1/10/2020
-- Description:    Update an existing Image
-- =========================================================

-- Check if the procedure already exists
IF EXISTS (select * from syscomments where id = object_id ('Image_Update'))

    -- Procedure Does Exist, Drop First
    BEGIN

        -- Execute Drop
        Drop Procedure Image_Update

        -- Test if procedure was dropped
        IF OBJECT_ID('dbo.Image_Update') IS NOT NULL

            -- Print Line Drop Failed
            PRINT '<<< Drop Failed On Procedure Image_Update >>>'

        Else

            -- Print Line Procedure Dropped
            PRINT '<<< Drop Suceeded On Procedure Image_Update >>>'

    End

GO

Create PROCEDURE Image_Update

    -- Add the parameters for the stored procedure here
    @CreatedDate datetime,
    @Extension nvarchar(10),
    @FileSize int,
    @FullPath nvarchar(512),
    @Height int,
    @Id int,
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
Go
-- =========================================================
-- Procure Name: Image_Find
-- Author:           Data Juggler - Data Tier.Net Procedure Generator
-- Create Date:   1/10/2020
-- Description:    Find an existing Image
-- =========================================================

-- Check if the procedure already exists
IF EXISTS (select * from syscomments where id = object_id ('Image_Find'))

    -- Procedure Does Exist, Drop First
    BEGIN

        -- Execute Drop
        Drop Procedure Image_Find

        -- Test if procedure was dropped
        IF OBJECT_ID('dbo.Image_Find') IS NOT NULL

            -- Print Line Drop Failed
            PRINT '<<< Drop Failed On Procedure Image_Find >>>'

        Else

            -- Print Line Procedure Dropped
            PRINT '<<< Drop Suceeded On Procedure Image_Find >>>'

    End

GO

Create PROCEDURE Image_Find

    -- Primary Key Paramater
    @Id int

AS
BEGIN

    -- SET NOCOUNT ON added to prevent extra result sets from
    -- interfering with SELECT statements.
    SET NOCOUNT ON

    -- Begin Select Statement
    Select [CreatedDate],[Extension],[FileSize],[FullPath],[Height],[Id],[ImageUrl],[Name],[OwnerId],[SitePath],[Visible],[Width]

    -- From tableName
    From [Image]

    -- Find Matching Record
    Where [Id] = @Id

END

set ANSI_NULLS ON
set QUOTED_IDENTIFIER ON
Go
-- =========================================================
-- Procure Name: Image_Delete
-- Author:           Data Juggler - Data Tier.Net Procedure Generator
-- Create Date:   1/10/2020
-- Description:    Delete an existing Image
-- =========================================================

-- Check if the procedure already exists
IF EXISTS (select * from syscomments where id = object_id ('Image_Delete'))

    -- Procedure Does Exist, Drop First
    BEGIN

        -- Execute Drop
        Drop Procedure Image_Delete

        -- Test if procedure was dropped
        IF OBJECT_ID('dbo.Image_Delete') IS NOT NULL

            -- Print Line Drop Failed
            PRINT '<<< Drop Failed On Procedure Image_Delete >>>'

        Else

            -- Print Line Procedure Dropped
            PRINT '<<< Drop Suceeded On Procedure Image_Delete >>>'

    End

GO

Create PROCEDURE Image_Delete

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
Go
-- =========================================================
-- Procure Name: Image_FetchAll
-- Author:           Data Juggler - Data Tier.Net Procedure Generator
-- Create Date:   1/10/2020
-- Description:    Returns all Image objects
-- =========================================================

-- Check if the procedure already exists
IF EXISTS (select * from syscomments where id = object_id ('Image_FetchAll'))

    -- Procedure Does Exist, Drop First
    BEGIN

        -- Execute Drop
        Drop Procedure Image_FetchAll

        -- Test if procedure was dropped
        IF OBJECT_ID('dbo.Image_FetchAll') IS NOT NULL

            -- Print Line Drop Failed
            PRINT '<<< Drop Failed On Procedure Image_FetchAll >>>'

        Else

            -- Print Line Procedure Dropped
            PRINT '<<< Drop Suceeded On Procedure Image_FetchAll >>>'

    End

GO

Create PROCEDURE Image_FetchAll

AS
BEGIN

    -- SET NOCOUNT ON added to prevent extra result sets from
    -- interfering with SELECT statements.
    SET NOCOUNT ON

    -- Begin Select Statement
    Select [CreatedDate],[Extension],[FileSize],[FullPath],[Height],[Id],[ImageUrl],[Name],[OwnerId],[SitePath],[Visible],[Width]

    -- From tableName
    From [Image]

END

-- Begin Custom Methods


set ANSI_NULLS ON
set QUOTED_IDENTIFIER ON
Go
-- =========================================================
-- Procure Name: Artist_FindByEmailAddress
-- Author:           Data Juggler - Data Tier.Net Procedure Generator
-- Create Date:   1/10/2020
-- Description:    Find an existing Artist for the EmailAddress given.
-- =========================================================

-- Check if the procedure already exists
IF EXISTS (select * from syscomments where id = object_id ('Artist_FindByEmailAddress'))

    -- Procedure Does Exist, Drop First
    BEGIN

        -- Execute Drop
        Drop Procedure Artist_FindByEmailAddress

        -- Test if procedure was dropped
        IF OBJECT_ID('dbo.Artist_FindByEmailAddress') IS NOT NULL

            -- Print Line Drop Failed
            PRINT '<<< Drop Failed On Procedure Artist_FindByEmailAddress >>>'

        Else

            -- Print Line Procedure Dropped
            PRINT '<<< Drop Suceeded On Procedure Artist_FindByEmailAddress >>>'

    End

GO

Create PROCEDURE Artist_FindByEmailAddress

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


-- End Custom Methods

-- Thank you for using DataTier.Net.

