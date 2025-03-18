
ALTER PROCEDURE proc_Product
	@flag VARCHAR(50),
	@Id INT = NULL,
    @ProductName VARCHAR(100)= NULL,
    @Quantity INT= NULL,
    @Price DECIMAL(10,2)= NULL,
    @AddedBy VARCHAR(100)= NULL,
    @IsActive BIT= NULL,
	
    -- Sales Parameters
    @SaleId INT = NULL,
	@ProductID INT = NULL,
    @BuyerName VARCHAR(255) = NULL,
    @QuantityPurchased INT = NULL
	--@TotalPrice INT = NULL
AS
BEGIN
	IF @flag = 'i'
	BEGIN
		INSERT INTO Product (product_name, quantity, price, added_date, added_by, is_active, is_deleted)
		VALUES (@ProductName, @Quantity, @Price, GETDATE(), @AddedBy, @IsActive, 0);

		EXEC proc_errorMsg 0,'Success', ''
	END
    
	IF @flag = 'f'
	BEGIN
		IF NOT EXISTS(SELECT 'X' FROM Product WHERE id = @Id )
		BEGIN
			EXEC proc_errorMsg 1,'Product Id not found.', ''
			RETURN;
		END
		SELECT id, product_name, quantity, price, added_date, added_by, is_active
        FROM Product WITH(NOLOCK)
        WHERE is_active = 1 AND is_deleted = 0;

		EXEC proc_errorMsg 0,'Success', ''
	END

	IF @flag = 'u'
	BEGIN
		IF NOT EXISTS(SELECT 'X' FROM Product WHERE id = @Id )
		BEGIN
			EXEC proc_errorMsg 1,'Product Id not found.', ''
			RETURN;
		END
		UPDATE Product
        SET product_name = @ProductName,
            quantity = @Quantity,
            price = @Price,
            is_active = @IsActive
        WHERE id = @Id;

		EXEC proc_errorMsg 0,'Success', ''
	END

	IF @flag = 'd'
	BEGIN
		IF NOT EXISTS(SELECT 'X' FROM Product WHERE id = @Id )
		BEGIN
			EXEC proc_errorMsg 1,'Product Id not found.', ''
			RETURN;
		END
		 UPDATE Product
        SET is_deleted = 1
        WHERE id = @Id;

		EXEC proc_errorMsg 0,'Success', ''
	END

	-- Insert a sale and update stock
	IF @flag = 'is'
	BEGIN
		IF NOT EXISTS(SELECT 'X' FROM Product WHERE id = @Id )
		BEGIN
			EXEC proc_errorMsg 1,'Product Id not found.', ''
			RETURN;
		END
		--DECLARE @Price DECIMAL(10,2);
        --SET @Price = (SELECT price FROM Product WHERE id = @Id);

        -- Insert into Sales table
        INSERT INTO Sales ( buyer_name, quantity_purchased, total_price, purchase_date, product_id)
        VALUES ( @BuyerName, @QuantityPurchased, (@Price * @QuantityPurchased), GETDATE(), @ProductID);

        -- Update product stock
        UPDATE Product
        SET quantity = quantity - @QuantityPurchased
        WHERE id = @ProductID;

		EXEC proc_errorMsg 0,'Success', ''
	END

	-- Fetch all sales records
	IF @flag = 'fs'
	BEGIN
		IF NOT EXISTS(SELECT 'X' FROM Product WHERE id = @Id )
		BEGIN
			EXEC proc_errorMsg 1,'Product Id not found.', ''
			RETURN;
		END
		SELECT s.id, p.product_name, s.buyer_name, s.quantity_purchased, s.total_price, s.purchase_date
        FROM Sales s
        INNER JOIN Product p ON s.product_id = p.id;

		EXEC proc_errorMsg 0,'Success', ''
	END
END;

