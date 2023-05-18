USE master;

IF NOT EXISTS(SELECT name FROM sys.databases WHERE name = 'LibraryDB')
BEGIN
    CREATE DATABASE LibraryDB;
END;

USE LibraryDB;

IF NOT EXISTS(SELECT name FROM sys.tables WHERE name = 'Books')
BEGIN
    CREATE TABLE Books (
        Id BIGINT IDENTITY(1,1) PRIMARY KEY,
        Title VARCHAR(255) NOT NULL,
        Author VARCHAR(255) NOT NULL,
        IsRead BIT NOT NULL DEFAULT 0,
        PhotoUrl VARCHAR(255) NULL,
        [Year] INT NULL,
        Language VARCHAR(50) NULL,
        [Type] VARCHAR(50) NULL,
        Grade INT NULL,
        Description VARCHAR(255) NULL
    );
END;

IF NOT EXISTS(SELECT name FROM sys.tables WHERE name = 'Movies')
BEGIN
    CREATE TABLE Movies (
		Id BIGINT IDENTITY(1,1) PRIMARY KEY,
		Title VARCHAR(255) NOT NULL,
		IsWatched BIT NOT NULL DEFAULT 0,
		PosterUrl VARCHAR(255) NULL,
		[Year] INT NULL,
		Language VARCHAR(50) NULL,
		[Genre] VARCHAR(50) NULL,
		Grade INT NULL,
        Duration INT NULL,
		Description VARCHAR(255) NULL
	);
END;

IF NOT EXISTS(SELECT name FROM sys.tables WHERE name = 'ComicsBook')
BEGIN
	CREATE TABLE ComicsBook (
		Id BIGINT IDENTITY(1,1) PRIMARY KEY,
		Title VARCHAR(255) NOT NULL,
		Author VARCHAR(255) NOT NULL,
		Brand VARCHAR(255) NOT NULL,
		IsColor BIT NOT NULL DEFAULT 0,
		IsRead BIT NOT NULL DEFAULT 0,
		PhotoUrl VARCHAR(255) NULL,
		[Year] INT NULL,
		Language VARCHAR(50) NULL,
		[Type] VARCHAR(50) NULL,
		Grade INT NULL,
		Description VARCHAR(255) NULL
	);
END;
