USE master;

IF NOT EXISTS(SELECT name FROM sys.databases WHERE name = 'LibraryDB')
BEGIN
    CREATE DATABASE LibraryDB;
END;

USE LibraryDB;

IF NOT EXISTS(SELECT name FROM sys.tables WHERE name = 'Books')
BEGIN
    CREATE TABLE Books (
        Id INT IDENTITY(1,1) PRIMARY KEY,
        Title VARCHAR(255) NOT NULL,
        Author VARCHAR(255) NOT NULL,
        IsRead BIT NOT NULL DEFAULT 0
    );
END;
