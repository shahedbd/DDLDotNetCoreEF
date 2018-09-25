USE [DevTest]
GO

/****** Object:  Table [dbo].[GlobalCitizen]    Script Date: 9/26/2018 5:34:36 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[GlobalCitizen](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](150) NULL,
	[CountryName] [nvarchar](150) NULL,
	[Capital] [nvarchar](50) NULL,
	[ContinentCode] [tinyint] NULL,
	[Email] [nchar](10) NULL,
	[Gender] [tinyint] NULL,
	[Status] [tinyint] NULL,
	[CreationUser] [nvarchar](50) NULL,
	[CreationDateTime] [datetime] NULL,
	[LastUpdateUser] [nvarchar](50) NULL,
	[LastUpdateDateTime] [datetime] NULL,
 CONSTRAINT [PK_GlobalCitizen] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO








USE [DevTest]
GO

/****** Object:  Table [dbo].[Continent]    Script Date: 9/26/2018 5:34:01 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Continent](
	[ContinentID] [tinyint] NOT NULL,
	[ContinentName] [nvarchar](50) NULL,
 CONSTRAINT [PK_Continent] PRIMARY KEY CLUSTERED 
(
	[ContinentID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO








USE [DevTest]
GO
/****** Object:  StoredProcedure [dbo].[sp_GlobalCitizen]    Script Date: 9/26/2018 4:46:31 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROC [dbo].[sp_GlobalCitizen] (@ID bigint = NULL,
@Name nvarchar(150) = NULL,
@CountryName nvarchar(150) = NULL,
@Capital nvarchar(150) = NULL,
@ContinentCode tinyint = NULL,
@Email nvarchar(150) = NULL,
@Status tinyint = NULL,
@CreationUser nvarchar(150) = NULL,
@CreationDateTime datetime = NULL,
@LastUpdateUser nvarchar(150) = NULL,
@LastUpdateDateTime datetime = NULL,
@searchString nvarchar(150) = NULL,

@Msg nvarchar(max) = NULL OUT,
@pOptions int)
AS



  --Save GlobalCitizen
  IF (@pOptions = 1)
  BEGIN
    INSERT INTO GlobalCitizen (ID, Name, CountryName, Capital, ContinentCode, Email, Status, CreationUser, CreationDateTime, LastUpdateUser, LastUpdateDateTime)
      VALUES (@ID, @Name, @CountryName, @Capital, @ContinentCode, @Email, @Status, @CreationUser, @CreationDateTime, @LastUpdateUser, @LastUpdateDateTime)
    IF @@ROWCOUNT = 0
    BEGIN
      SET @Msg = 'Warning: No rows were Inserted';
    END
    ELSE
    BEGIN
      SET @Msg = 'Data Saved Successfully';
    END
  END
  --End of Save GlobalCitizen



  --Update GlobalCitizen 
  IF (@pOptions = 2)
  BEGIN
    UPDATE GlobalCitizen
    SET Name = @Name,
        CountryName = @CountryName,
        Capital = @Capital,
        ContinentCode = @ContinentCode,
        Email = @Email,
        Status = @Status,
        CreationUser = @CreationUser,
        CreationDateTime = @CreationDateTime,
        LastUpdateUser = @LastUpdateUser,
        LastUpdateDateTime = @LastUpdateDateTime

    WHERE ID = @ID;
    IF @@ROWCOUNT = 0
    BEGIN
      SET @Msg = 'Warning: No rows were Updated';
    END
    ELSE
    BEGIN
      SET @Msg = 'Data Updated Successfully';
    END
  END
  --End of Update GlobalCitizen 



  --Delete GlobalCitizen
  IF (@pOptions = 3)
  BEGIN
    DELETE FROM GlobalCitizen
    WHERE ID = @ID;
    SET @Msg = 'Data Deleted Successfully';
  END
  --End of Delete GlobalCitizen 



  --Select All GlobalCitizen 
  IF (@pOptions = 4)
  BEGIN
    SELECT
      *
    FROM GlobalCitizen;
    IF (@@ROWCOUNT = 0)
      SET @Msg = 'Data Not Found';
  END
  --End of Select All GlobalCitizen 



  --Select GlobalCitizen By ID 
  IF (@pOptions = 5)
  BEGIN
    SELECT
      *
    FROM GlobalCitizen
    WHERE ID = @ID;
    IF (@@ROWCOUNT = 0)
      SET @Msg = 'Data Not Found';
  END
--End of Select GlobalCitizen By ID 


  --Select GlobalCitizen and Continent by ID
  IF (@pOptions = 6)
  BEGIN
    SELECT G.ID, G.Name,
	 Case when G.Gender = '0' then 'Male' when G.Gender = '1' then 'Female' else 'Other' end Gender,
	G.CountryName,C.ContinentName,G.CreationDateTime FROM GlobalCitizen as G inner join Continent as C
	on G.ContinentCode=C.ContinentID
	where ((@searchString IS NULL or @searchString='') or G.Name like '%'+ @searchString + '%')
	or (G.CountryName like '%'+ @searchString + '%')
	or (C.ContinentName like '%'+ @searchString + '%')
	or (G.CreationDateTime like '%'+ @searchString + '%')
    IF (@@ROWCOUNT = 0)
      SET @Msg = 'Data Not Found';
  END
  --End of Select All GlobalCitizen 


