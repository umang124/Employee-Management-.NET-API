-- Employee Management System SQL Queries

USE EmployeManagementDb;

-- Insert Employees
INSERT INTO Employees (FirstName, LastName, DateOfBirth, GenderId, ContactNumber, Email, Address)
VALUES
  ('John', 'Doe', '1985-05-15', 1, '555-1234', 'john.doe@example.com', '123 Main St, Cityville'),
  ('Jane', 'Smith', '1990-08-22', 2, '555-5678', 'jane.smith@example.com', '456 Oak Ave, Townsville'),
  ('Mike', 'Johnson', '1988-12-10', 1, '555-9876', 'mike.j@example.com', '789 Elm St, Villagetown'),
  ('Emily', 'Williams', '1992-04-05', 2, '555-6543', 'emily.w@example.com', '321 Pine Ave, Hamlet'),
  ('David', 'Brown', '1980-11-30', 1, '555-8765', 'david.b@example.com', '555 Cedar Rd, Borough'),
  ('Sarah', 'Miller', '1995-07-18', 2, '555-2345', 'sarah.m@example.com', '987 Oak Lane, Village'),
  ('Robert', 'Davis', '1987-02-25', 1, '555-4321', 'robert.d@example.com', '456 Birch Blvd, City'),
  ('Megan', 'Wilson', '1993-09-08', 2, '555-7890', 'megan.w@example.com', '654 Maple St, Township');

-- Insert Job Histories
INSERT INTO JobHistories (EmployeeId, Department, Position, StartDate, EndDate)
VALUES
  (1, 'IT', 'Software Engineer', '2010-07-01', '2015-06-30'),
  (1, 'Finance', 'Financial Analyst', '2016-01-01', NULL),
  (2, 'Marketing', 'Marketing Specialist', '2012-04-15', '2018-09-30'),
  (3, 'HR', 'HR Manager', '2014-10-20', '2020-03-15'),
  (4, 'Sales', 'Sales Representative', '2013-08-05', '2019-12-31'),
  (5, 'IT', 'Systems Analyst', '2011-06-10', '2017-09-28'),
  (6, 'Operations', 'Operations Manager', '2015-03-12', NULL),
  (7, 'Finance', 'Accountant', '2012-09-01', '2016-05-30');

-- Insert Performance Evaluations
INSERT INTO PerformanceEvaluations (EmployeeId, EvaluationDate, Rating, Comments)
VALUES
  (1, '2023-03-15', 4, 'Consistently meets expectations'),
  (2, '2023-03-20', 5, 'Outstanding performance'),
  (3, '2023-02-10', 3, 'Improvement needed in communication'),
  (4, '2023-01-25', 4, 'Adapts well to changing work demands'),
  (5, '2023-04-05', 5, 'Exceeds expectations in project work'),
  (6, '2023-03-08', 3, 'Attention to detail could be improved'),
  (7, '2023-02-28', 4, 'Strong analytical skills'),
  (8, '2023-03-10', 5, 'Exceptional problem-solving abilities');


-- To find male employees with high ratings
CREATE PROCEDURE GetHighlyRatedMaleEmployees
AS
BEGIN
	SELECT
		E.Id,
		E.FirstName,
		E.LastName,
		E.GenderId,
		PE.EvaluationDate,
		PE.Rating,
		PE.Comments
	FROM
		Employees E
	JOIN
		PerformanceEvaluations PE ON E.Id = PE.EmployeeID
	WHERE
		E.GenderId = 1
		AND PE.Rating >= 4
END;

-- Find female employees with low ratings
CREATE PROCEDURE GetLowRatedFemaleEmployees
AS
BEGIN
	SELECT
		E.Id,
		E.FirstName,
		E.LastName,
		E.GenderId,
		PE.EvaluationDate,
		PE.Rating,
		PE.Comments
	FROM
		Employees E
	JOIN
		PerformanceEvaluations PE ON E.Id = PE.EmployeeID
	WHERE
		E.GenderId = 2
		AND PE.Rating < 4
END;


-- Find employees within the age range of 30 to 35
-- DROP PROCEDURE IF EXISTS GetEmployeesBetween30And35;
CREATE PROCEDURE GetEmployeesBetween30And35
AS
BEGIN
	SELECT
		E.Id,
		E.FirstName,
		E.LastName,
		FORMAT(E.DateOfBirth, 'd MMM yyyy') DateOfBirth,
		G.Name as Gender
	FROM
		Employees E
	INNER JOIN 
		Genders G on G.Id = E.GenderId
	WHERE
		DATEDIFF(YEAR, DateOfBirth, GETDATE()) BETWEEN 30 AND 35
END;

-- Select all employees by their age
CREATE PROCEDURE GetEmployeesByAge
AS 
BEGIN
	SELECT
		e.FirstName, 
		e.LastName,
		DATEDIFF(YEAR, DateOfBirth, GETDATE()) Age,
		G.Name Gender
	FROM
		Employees E
	INNER JOIN 
		Genders G ON E.GenderId = G.Id
END;


-- Retrieve current job positions
CREATE PROCEDURE EmployeesWithCurrentJobPosition
AS
BEGIN
	SELECT E.Id, E.FirstName, E.LastName, JH.Department, JH.Position
	FROM Employees E
	JOIN JobHistories JH ON E.Id = JH.EmployeeID
	WHERE JH.EndDate IS NULL
END;

-- Count the number of employees in each department
CREATE PROCEDURE CountEmployeesInEachDepartment
AS
BEGIN
	SELECT Department, COUNT(*) AS EmployeeCount
	FROM JobHistories
	GROUP BY Department
END;

-- Calculate average rating for each employee

CREATE PROCEDURE AVGRATING
AS
BEGIN
	SELECT EmployeeId, AVG(Rating) AS AverageRating
	FROM PerformanceEvaluations
	GROUP BY EmployeeId
END;


EXEC GetHighlyRatedMaleEmployees;

EXEC GetEmployeesBetween30And35;