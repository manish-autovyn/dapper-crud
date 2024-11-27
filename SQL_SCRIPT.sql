
--- create db
create database Customerdb;
GO

--- use Customerdb;
use Customerdb;
GO

--- create table
CREATE TABLE Customers (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    FirstName NVARCHAR(100) NOT NULL,
    LastName NVARCHAR(100) NOT NULL,
    Email NVARCHAR(255) NOT NULL UNIQUE,
    DateOfBirth DATETIME NOT NULL
);
GO


--- get all customers
CREATE PROCEDURE sp_GetAllCustomers
as
BEGIN
	SELECT * From Customers;
END;
GO


--- get customer by id
CREATE PROCEDURE sp_GetCustomerById @id INT
as
BEGIN
	IF EXISTS (SELECT 1 FROM Customers WHERE Id = @Id)
    BEGIN
		SELECT * FROM Customers
		where Id = @id
	END
    ELSE
    BEGIN
        RAISERROR('Customer with the specified Id does not exist.',16,1);
    END
END;
GO

--- add new customer
CREATE PROCEDURE sp_AddNewCustomer
    @FirstName VARCHAR(50),
    @LastName VARCHAR(50),
    @Email VARCHAR(100),
    @DateOfBirth DATETIME
AS
BEGIN
    INSERT INTO Customers (FirstName, LastName, Email, DateOfBirth)
    VALUES (@FirstName, @LastName, @Email, @DateOfBirth);

    SELECT Id, FirstName, LastName, Email, DateOfBirth
    FROM Customers
    WHERE Email = @Email;
END;
GO


--- update customer
CREATE PROCEDURE sp_UpdateCustomer
    @Id INT,
    @FirstName VARCHAR(50),
    @LastName VARCHAR(50),
    @Email VARCHAR(100),
    @DateOfBirth DATETIME
AS
BEGIN
    IF EXISTS (SELECT 1 FROM Customers WHERE Id = @Id)
    BEGIN
        UPDATE Customers
        SET FirstName = @FirstName,
            LastName = @LastName,
            Email = @Email,
            DateOfBirth = @DateOfBirth
        WHERE Id = @Id;

        SELECT Id, FirstName, LastName, Email, DateOfBirth
        FROM Customers
        WHERE Id = @Id;
    END
    ELSE
    BEGIN
        RAISERROR('Customer with the specified Id does not exist.', 16, 1);
    END
END;
GO


--- Delete customer
CREATE PROCEDURE sp_DeleteCustomer
    @Id INT
AS
BEGIN
    IF EXISTS (SELECT 1 FROM Customers WHERE Id = @Id)
    BEGIN
        DELETE FROM Customers WHERE Id = @Id;

        SELECT 'Customer deleted successfully.' AS Message, @Id AS DeletedCustomerId;
    END
    ELSE
    BEGIN
        RAISERROR('Customer with the specified Id does not exist.', 16, 1);
    END
END;
GO