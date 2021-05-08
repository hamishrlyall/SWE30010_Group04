CREATE TABLE [dbo].[Employee]
(

   [EmployeeId] INT IDENTITY(1,1) NOT NULL,
   [Username] NVARCHAR(50) NOT NULL,
   [FirstName] NVARCHAR(100) NOT NULL,
   [LastName] NVARCHAR(100) NOT NULL,
   [Password] NVARCHAR(50) NOT NULL,
   CONSTRAINT PK_EmployeeId PRIMARY KEY (EmployeeId),
   CONSTRAINT Login_User_Username UNIQUE (Username)
)
