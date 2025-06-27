-- Create the LoanDB database
CREATE DATABASE LoanDB;
GO

-- Use the created database
USE LoanDB;
GO

-- Create the Loans table
CREATE TABLE Loans (
    LoanId INT PRIMARY KEY,
    CustomerId INT,
    PrincipalAmount FLOAT,
    InterestRate FLOAT,
    LoanTerm INT,
    LoanType VARCHAR(50),
    LoanStatus VARCHAR(20)
);
