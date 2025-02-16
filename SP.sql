ALTER PROCEDURE proc_ProductMaster
	@flag VARCHAR(50),
	@id INT = NULL,
	@Name VARCHAR(100) = NULL,
    @Email VARCHAR(100) = NULL,
    @Phone VARCHAR(20) = NULL,
    @Address VARCHAR(255) = NULL,
    @Type VARCHAR(50) = NULL,
    @IsActive CHAR = NULL,
	@IsDelete BIT = NULL
AS
BEGIN
	IF @flag = 'i'
	BEGIN
		IF EXISTS(SELECT 'X' FROM Users WHERE email = @Email OR phone = @Phone)
		BEGIN
			EXEC proc_errorMsg 1,'Phone or Email has already been used.', ''
			RETURN;
		END

		INSERT INTO Users (Name, Email, Phone, Address, Type, is_active)
		VALUES (@Name, @Email, @Phone, @Address, @Type, @IsActive);

		EXEC proc_errorMsg 0,'Success', ''
	END

	IF @flag = 'f'
	BEGIN 
		SELECT * FROM Users WHERE is_delete = 0;

		EXEC proc_errorMsg 0, 'Success', ''
	END

	IF @flag = 'd'
	BEGIN 
	IF NOT EXISTS(SELECT 'X' FROM Users WHERE id = @Id)
		BEGIN
			EXEC proc_errorMsg 1,'Id not found.', ''
			RETURN;
		END
		UPDATE Users SET is_delete = 1 WHERE Id = @Id;


		EXEC proc_errorMsg 0, 'Success', ''
	END
END








Select * from Users;

