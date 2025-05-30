
IF DB_ID('IbTradingBot') IS NULL
BEGIN
    CREATE DATABASE IbTradingBot;
END
GO

USE IbTradingBot;
GO

IF OBJECT_ID('dbo.BotStates', 'U') IS NULL
BEGIN
    CREATE TABLE dbo.BotStates (
        Id INT PRIMARY KEY,
        InPosition BIT NOT NULL,
        LastSignal INT NOT NULL,
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
