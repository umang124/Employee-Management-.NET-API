USE BikeStores;

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