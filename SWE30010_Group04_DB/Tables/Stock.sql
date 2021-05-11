CREATE TABLE [dbo].[Stock]
(
   [StockId] INT IDENTITY(1,1) NOT NULL,
   [ProductId] INT NOT NULL,
   CONSTRAINT PK_StockId PRIMARY KEY ([StockId]),
   CONSTRAINT FK_Stock_ProductId FOREIGN KEY (ProductId) REFERENCES [Product] (ProductId)

)
