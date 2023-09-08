CREATE DATABASE Abhijith;


CREATE TABLE Employees (
    EmployeeID INT PRIMARY KEY,
    FirstName NVARCHAR(50),
    LastName NVARCHAR(50),
    Age INT,
    Salary DECIMAL(10, 2),
    Email NVARCHAR(100),
    Phone NVARCHAR(20),
    Address NVARCHAR(200),
    HireDate DATE,
	DepartmentID INT,
    FOREIGN KEY (DepartmentID) REFERENCES Departments(DepartmentID)
);


INSERT INTO Employees (EmployeeID, FirstName, LastName, Age, Salary, Email, Phone, Address, HireDate, DepartmentID)
VALUES
    (1, 'Abhi', 'A', 30, 60000.00, 'abhi@mail.com', '555-1234', '123 Main St', '2023-01-15',1),
    (2, 'Anu', 'B', 28, 55000.00, 'anu@mail.com', '555-5678', '456 Elm St', '2023-02-20',2),
    (3, 'John', 'C', 35, 75000.00, 'john@mail.com', '555-9876', '789 Oak St', '2023-03-10',3),
    (4, 'Sam', 'D', 29, 60000.00, 'sam@mail.com', '555-2345', '567 Pine St', '2023-04-05',4),
    (5, 'Jithin', 'E', 32, 65000.00, 'jithin@mail.com', '555-4321', '890 Maple St', '2023-05-08',1),
    (6, 'Sana', 'F', 27, 70000.00, 'sana@mail.com', '555-8765', '345 Walnut St', '2023-06-15',4),
	(7, 'Safa', 'G', 26, 80000.00, 'safa@mail.com', '575-8705', '389 mans St', '2023-08-19',2);


CREATE TABLE Departments (
    DepartmentID INT PRIMARY KEY,
    DepartmentName VARCHAR(50)
);

INSERT INTO Departments (DepartmentID, DepartmentName)
VALUES
    (1, 'HR'),
    (2, 'Finance'),
    (3, 'IT'),
    (4, 'Marketing');



UPDATE Employees
SET Salary = 80000.00
WHERE EmployeeID = 2;

SELECT * FROM Employees;

DELETE FROM Employees
WHERE EmployeeID = 3;



SELECT DISTINCT TOP 1 Salary
FROM Employees
WHERE Salary NOT IN (
    SELECT DISTINCT TOP 1 Salary
    FROM Employees
    ORDER BY Salary DESC
)
ORDER BY Salary DESC;



SELECT DepartmentID, COUNT(*) AS EmployeeCount
FROM Employees
GROUP BY DepartmentID;




SELECT
    E.EmployeeID,
    E.FirstName,
    E.LastName,
    E.Age,
    E.Salary,
    E.Email,
    E.Phone,
    E.Address,
    E.HireDate,
    D.DepartmentName
FROM Employees AS E
INNER JOIN Departments AS D ON E.DepartmentID = D.DepartmentID;

	
SELECT
    E.EmployeeID,
    E.FirstName,
    E.LastName,
    E.Age,
    E.Salary,
    E.Email,
    E.Phone,
    E.Address,
    E.HireDate,
    D.DepartmentName
FROM Employees AS E
LEFT JOIN Departments AS D ON E.DepartmentID = D.DepartmentID;


SELECT
    E.EmployeeID,
    E.FirstName,
    E.LastName,
    E.Age,
    E.Salary,
    E.Email,
    E.Phone,
    E.Address,
    E.HireDate,
    D.DepartmentName
FROM Employees AS E
RIGHT JOIN Departments AS D ON E.DepartmentID = D.DepartmentID;


SELECT
    E.EmployeeID,
    E.FirstName,
    E.LastName,
    E.Age,
    E.Salary,
    E.Email,
    E.Phone,
    E.Address,
    E.HireDate,
    D.DepartmentName
FROM Employees AS E
FULL OUTER JOIN Departments AS D ON E.DepartmentID = D.DepartmentID;


SELECT
    E.FirstName,
    E.LastName,
    D.DepartmentName
FROM Employees AS E
JOIN Departments AS D ON E.DepartmentID = D.DepartmentID;


CREATE PROCEDURE CreateEmployee
    @FirstName NVARCHAR(50),
    @LastName NVARCHAR(50),
    @Age INT,
    @Salary DECIMAL(10, 2),
    @Email NVARCHAR(100),
    @Phone NVARCHAR(20),
    @Address NVARCHAR(200),
    @HireDate DATE,
    @DepartmentID INT
AS
BEGIN
    
    DECLARE @NewEmployeeID INT;
    
    IF OBJECTPROPERTY(OBJECT_ID('Employees'), 'TableHasIdentity') = 1
    BEGIN
       
        SET @NewEmployeeID = IDENT_CURRENT('Employees') + 1;
    END
    ELSE
    BEGIN
        
        SET @NewEmployeeID = (SELECT MAX(EmployeeID) FROM Employees) + 1;
    END
    
    INSERT INTO Employees (EmployeeID, FirstName, LastName, Age, Salary, Email, Phone, Address, HireDate, DepartmentID)
    VALUES (@NewEmployeeID, @FirstName, @LastName, @Age, @Salary, @Email, @Phone, @Address, @HireDate, @DepartmentID);
END;


CREATE PROCEDURE GetEmployeeByID
    @EmployeeID INT
AS
BEGIN
    SELECT * FROM Employees WHERE EmployeeID = @EmployeeID;
END;



CREATE PROCEDURE UpdateEmployee
    @EmployeeID INT,
    @FirstName NVARCHAR(50),
    @LastName NVARCHAR(50),
    @Age INT,
    @Salary DECIMAL(10, 2),
    @Email NVARCHAR(100),
    @Phone NVARCHAR(20),
    @Address NVARCHAR(200),
    @HireDate DATE,
    @DepartmentID INT
AS
BEGIN
    UPDATE Employees
    SET FirstName = @FirstName,
        LastName = @LastName,
        Age = @Age,
        Salary = @Salary,
        Email = @Email,
        Phone = @Phone,
        Address = @Address,
        HireDate = @HireDate,
        DepartmentID = @DepartmentID
    WHERE EmployeeID = @EmployeeID;
END;

CREATE PROCEDURE DeleteEmployee
    @EmployeeID INT
AS
BEGIN
    DELETE FROM Employees WHERE EmployeeID = @EmployeeID;
END;




EXEC CreateEmployee
    @FirstName = 'John',
    @LastName = 'H',
    @Age = 30,
    @Salary = 60000.00,
    @Email = 'john@mail.com',
    @Phone = '555-1234',
    @Address = '123 Main St',
    @HireDate = '2023-08-23',
    @DepartmentID = 1;

EXEC GetEmployeeByID 2;

EXEC UpdateEmployee 1, 'Updated', 'Employee', 26, 55000.00, 'updated@example.com', '555-1234', '123 Elm St', '2023-08-24', 4;

EXEC DeleteEmployee 2;


--single stored procedure

CREATE PROCEDURE ManageEmployee
    @Operation CHAR(1),
    @EmployeeID INT = NULL,
    @FirstName NVARCHAR(50) = NULL,
    @LastName NVARCHAR(50) = NULL,
    @Age INT = NULL,
    @Salary DECIMAL(10, 2) = NULL,
    @Email NVARCHAR(100) = NULL,
    @Phone NVARCHAR(20) = NULL,
    @Address NVARCHAR(200) = NULL,
    @HireDate DATE = NULL,
    @DepartmentID INT = NULL
AS
BEGIN
    IF @Operation = 'C'
    BEGIN
       
        INSERT INTO Employees (FirstName, LastName, Age, Salary, Email, Phone, Address, HireDate, DepartmentID)
        VALUES (@FirstName, @LastName, @Age, @Salary, @Email, @Phone, @Address, @HireDate, @DepartmentID);
    END
    ELSE IF @Operation = 'R'
    BEGIN
       
        SELECT * FROM Employees WHERE EmployeeID = @EmployeeID;
    END
    ELSE IF @Operation = 'U'
    BEGIN
        UPDATE Employees
        SET FirstName = @FirstName,
            LastName = @LastName,
            Age = @Age,
            Salary = @Salary,
            Email = @Email,
            Phone = @Phone,
            Address = @Address,
            HireDate = @HireDate,
            DepartmentID = @DepartmentID
        WHERE EmployeeID = @EmployeeID;
    END
    ELSE IF @Operation = 'D'
    BEGIN
       
        DELETE FROM Employees WHERE EmployeeID = @EmployeeID;
    END
END;




EXEC ManageEmployee 'C', NULL, 'John', 'Doe', 30, 60000.00, 'john@example.com', '555-1234', '123 Main St', '2023-08-23', 1;


EXEC ManageEmployee 'R', 1;


EXEC ManageEmployee 'U', 1, 'Updated', 'Employee', 26, 55000.00, 'updated@example.com', '555-1234', '123 Elm St', '2023-08-24', 4;


EXEC ManageEmployee 'D', 1;
