
IF DB_ID('IbTradingBot') IS NULL
BEGIN
    CREATE DATABASE IbTradingBot;
END
GO

USE IbTradingBot;
GO

IF OBJECT_ID('dbo.BotStates', 'U') IS NULL
BEGIN
    CREATE TABLE BotStates (
        Id INT PRIMARY KEY IDENTITY(1,1),
        Symbol NVARCHAR(450) NOT NULL,
        InPosition BIT NOT NULL,
        LastSignal INT NOT NULL DEFAULT 0,
        LastTradeTime DATETIME2 NOT NULL,
        EntryPrice FLOAT NOT NULL,
        MaxPriceSinceEntry FLOAT NOT NULL
    );
END
GO

IF OBJECT_ID('dbo.TradeRecords', 'U') IS NULL
BEGIN
    CREATE TABLE dbo.TradeRecords (
        Id INT IDENTITY(1,1) PRIMARY KEY,
        Symbol NVARCHAR(50) NOT NULL,
        Quantity INT NOT NULL,
        Price FLOAT NOT NULL,
        Side INT NOT NULL,
        Timestamp DATETIME2 NOT NULL
    );
END
GO
