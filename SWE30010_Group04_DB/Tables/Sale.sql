CREATE TABLE [dbo].[Sale]
(
   [SaleId] INT NOT NULL,
   [EmployeeId] INT NOT NULL,
   [StockId] INT NOT NULL,
   CONSTRAINT PK_SaleId PRIMARY KEY ([SaleId]),
   CONSTRAINT FK_Sale_EmployeeId FOREIGN KEY (EmployeeId) REFERENCES [Employee] (EmployeeId),
   CONSTRAINT FK_Sale_StockId FOREIGN KEY (StockId) REFERENCES [Stock] (StockId)
)
