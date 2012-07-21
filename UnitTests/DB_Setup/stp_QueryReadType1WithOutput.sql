SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[stp_QueryReadType1WithOutput]
	@iID int,
	@oName nvarchar(10) OUTPUT
AS
BEGIN
	SET NOCOUNT ON;
	SELECT * FROM ReadType1
	SELECT * FROM ReadType2
	
	SELECT @oName = Name FROM ReadType1 WHERE ID = 3
END


GO