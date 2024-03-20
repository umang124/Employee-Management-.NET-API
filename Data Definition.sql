CREATE TABLE Gender (
    Id INT PRIMARY KEY,
    Name NVARCHAR(50)
);

CREATE TABLE Employee (
    Id INT PRIMARY KEY,
    FirstName NVARCHAR(50),
    LastName NVARCHAR(50),
    DateOfBirth DATE,
    GenderId INT,
    ContactNumber NVARCHAR(20),
    Email NVARCHAR(100),
    Address NVARCHAR(255),
    FOREIGN KEY (GenderId) REFERENCES Gender(Id)
);

CREATE TABLE JobHistory (
    Id INT PRIMARY KEY,
    EmployeeId INT,
    Department NVARCHAR(50),
    Position NVARCHAR(50),
    StartDate DATE,
    EndDate DATE NULL,
    FOREIGN KEY (EmployeeId) REFERENCES Employee(Id)
);

CREATE TABLE PerformanceEvaluation (
    Id INT PRIMARY KEY,
    EmployeeId INT,
    EvaluationDate DATE,
    Rating INT,
    Comments NVARCHAR(MAX),
    FOREIGN KEY (EmployeeId) REFERENCES Employee(Id)
);
