USE [DevTest]
GO
/****** Object:  StoredProcedure [dbo].[sp_GlobalCitizen]    Script Date: 9/22/2018 4:57:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROC [dbo].[sp_GlobalCitizen] (@ID bigint = NULL,
@Name nvarchar = NULL,
@CountryName nvarchar = NULL,
@Capital nvarchar = NULL,
@ContinentCode tinyint = NULL,
@Email nchar(10) = NULL,
@Status tinyint = NULL,
@CreationUser nvarchar = NULL,
@CreationDateTime datetime = NULL,
@LastUpdateUser nvarchar = NULL,
@LastUpdateDateTime datetime = NULL,

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
    SELECT G.Name,G.CountryName,C.ContinentName,G.CreationDateTime FROM GlobalCitizen as G inner join Continent as C
	on G.ContinentCode=C.ContinentID;
    IF (@@ROWCOUNT = 0)
      SET @Msg = 'Data Not Found';
  END
  --End of Select All GlobalCitizen 

