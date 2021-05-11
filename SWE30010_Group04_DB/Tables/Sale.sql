CREATE TABLE [dbo].[Sale]
(
   [SaleId] INT IDENTITY(1,1) NOT NULL,
   [EmployeeId] INT NOT NULL,
   CONSTRAINT PK_SaleId PRIMARY KEY ([SaleId]),
   CONSTRAINT FK_Sale_EmployeeId FOREIGN KEY (EmployeeId) REFERENCES [Employee] (EmployeeId),
)
