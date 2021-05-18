CREATE TABLE [dbo].[Product]
(
   [ProductId] INT IDENTITY(1,1) NOT NULL,
   [ProductName] NVARCHAR(100) NOT NULL,
   [Price] MONEY NOT NULL, 
   CONSTRAINT PK_ProductId PRIMARY KEY (ProductId)
)
