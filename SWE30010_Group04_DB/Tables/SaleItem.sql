CREATE TABLE [dbo].[SaleItem]
(
   [SaleItemId] INT IDENTITY(1,1) NOT NULL,
   [SaleId] INT NOT NULL,
   [StockId] INT NOT NULL,
   CONSTRAINT PK_SaleItemId PRIMARY KEY ([SaleItemId]),
   CONSTRAINT FK_SaleItem_StockId FOREIGN KEY (StockId) REFERENCES [Stock] (StockId),
   CONSTRAINT FK_SaleItem_SaleId FOREIGN KEY (SaleId) REFERENCES [Sale] (SaleId)

)
