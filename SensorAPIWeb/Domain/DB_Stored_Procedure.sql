CREATE PROCEDURE InsertOrUpdateToken
(
    @SerialNumber VARCHAR(24),
    @Token VARCHAR(64)	
)
AS
BEGIN
Declare @TypeId int
SET NOCOUNT ON;
SET TRANSACTION ISOLATION LEVEL SERIALIZABLE;   
    BEGIN TRY
		BEGIN TRANSACTION
			select @TypeId=Id from Devices where SerialNumber = @SerialNumber 
			IF @@ROWCOUNT=0 
				BEGIN
					INSERT INTO Devices(SerialNumber,UpdateTime) values(@SerialNumber, CURRENT_TIMESTAMP)
					select @TypeId=Id from Devices where SerialNumber = @SerialNumber 
					INSERT INTO Tokens(TypeId,TokenNumber,UpdateTime) values(@TypeId, @Token, CURRENT_TIMESTAMP)
				END
			ELSE
			BEGIN
				select * from Tokens where TypeId=@TypeId 			
				IF @@ROWCOUNT=0 
					BEGIN
						INSERT INTO Tokens(TypeId,TokenNumber,UpdateTime) values(@TypeId, @Token, CURRENT_TIMESTAMP)
					END
				ELSE  
					BEGIN
						UPDATE Tokens set TokenNumber=@Token where TypeId=@TypeId
					END
			END
		COMMIT TRANSACTION
    END TRY
    BEGIN CATCH
		IF @@TRANCOUNT > 0 
		BEGIN
			ROLLBACK TRANSACTION
		END
    END CATCH;
END
GO
drop PROCEDURE InsertOrUpdateToken
