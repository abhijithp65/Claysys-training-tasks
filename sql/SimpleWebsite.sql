CREATE DATABASE SimpleWebsite;


USE SimpleWebsite;


CREATE TABLE Users (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Username NVARCHAR(50) NOT NULL,
    Password NVARCHAR(255) NOT NULL,
    Email NVARCHAR(100) NOT NULL
);

CREATE TABLE UserRegistration (
    Id INT PRIMARY KEY IDENTITY(1,1),
    FirstName NVARCHAR(50) NOT NULL,
    LastName NVARCHAR(50) NOT NULL,
    DateOfBirth DATE NOT NULL,
    Age INT NOT NULL,
    Gender NVARCHAR(10) NOT NULL,
    PhoneNumber NVARCHAR(20) NOT NULL,
    Email NVARCHAR(100) NOT NULL,
    Address NVARCHAR(200) NOT NULL,
    State NVARCHAR(50) NOT NULL,
    City NVARCHAR(50) NOT NULL,
    Username NVARCHAR(50) NOT NULL,
    Password NVARCHAR(255) NOT NULL
);

CREATE TABLE Messages (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Name NVARCHAR(100) NOT NULL,
    Email NVARCHAR(100) NOT NULL,
    Message TEXT NOT NULL,
    Timestamp DATETIME DEFAULT GETDATE()
);



USE SimpleWebsite;
GO

CREATE PROCEDURE InsertUserRegistration
    @FirstName NVARCHAR(50),
    @LastName NVARCHAR(50),
    @DateOfBirth DATE,
    @Age INT,
    @Gender NVARCHAR(10),
    @PhoneNumber NVARCHAR(20),
    @Email NVARCHAR(100),
    @Address NVARCHAR(200),
    @State NVARCHAR(50),
    @City NVARCHAR(50),
    @Username NVARCHAR(50),
    @Password NVARCHAR(255)
AS
BEGIN
    INSERT INTO UserRegistration (FirstName, LastName, DateOfBirth, Age, Gender, PhoneNumber, Email, Address, State, City, Username, Password)
    VALUES (@FirstName, @LastName, @DateOfBirth, @Age, @Gender, @PhoneNumber, @Email, @Address, @State, @City, @Username, @Password);
END;



EXEC InsertUserRegistration
    @FirstName = 'John',
    @LastName = 'Doe',
    @DateOfBirth = '1990-01-15',
    @Age = 31,
    @Gender = 'Male',
    @PhoneNumber = '123-456-7890',
    @Email = 'johndoe@example.com',
    @Address = '123 Main St',
    @State = 'California',
    @City = 'Los Angeles',
    @Username = 'johndoe',
    @Password = 'hashed_password_here';




CREATE PROCEDURE GetUserRegistrations
AS
BEGIN
    SELECT * FROM UserRegistration;
END;



CREATE PROCEDURE UpdateUserRegistration
    @Id INT,
    @FirstName NVARCHAR(50),
    @LastName NVARCHAR(50),
    @DateOfBirth DATE,
    @Age INT,
    @Gender NVARCHAR(10),
    @PhoneNumber NVARCHAR(20),
    @Email NVARCHAR(100),
    @Address NVARCHAR(200),
    @State NVARCHAR(50),
    @City NVARCHAR(50),
    @Username NVARCHAR(50),
    @Password NVARCHAR(255)
AS
BEGIN
    UPDATE UserRegistration
    SET FirstName = @FirstName, LastName = @LastName, DateOfBirth = @DateOfBirth, Age = @Age,
        Gender = @Gender, PhoneNumber = @PhoneNumber, Email = @Email, Address = @Address,
        State = @State, City = @City, Username = @Username, Password = @Password
    WHERE Id = @Id;
END;





CREATE PROCEDURE DeleteUserRegistration
    @Id INT
AS
BEGIN
    DELETE FROM UserRegistration WHERE Id = @Id;
END;



CREATE PROCEDURE InsertMessage
    @Name NVARCHAR(100),
    @Email NVARCHAR(100),
    @Message TEXT
AS
BEGIN
    INSERT INTO Messages (Name, Email, Message)
    VALUES (@Name, @Email, @Message);
END;
