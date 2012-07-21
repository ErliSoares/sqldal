SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[stp_QueryReadType1WithTableType]
	@iIDs IDTableType READONLY
AS
BEGIN
	SELECT rt1.Name FROM ReadType1 rt1
	JOIN @iIDs i ON rt1.ID = i.ID
END


GO