/*
Post-Deployment Script Template                     
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.      
 Use SQLCMD syntax to include a file in the post-deployment script.         
 Example:      :r .\myfile.sql                        
 Use SQLCMD syntax to reference a variable in the post-deployment script.      
 Example:      :setvar TableName MyTable                     
               SELECT * FROM [$(TableName)]               
--------------------------------------------------------------------------------------
*/
BEGIN

MERGE INTO [Employee] AS Target USING (
   VALUES
   ( 1, 'jjohnson', 'Johnny', 'Johnson', 'password' ),
   ( 2, 'bbenson', 'Benny', 'Benson', 'password'),
   ( 3, 'llarinson', 'Larry', 'Larinson', 'password' ),
   ( 4, 'ppeterson', 'Peter', 'Peterson', 'password')
)
AS Source ([EmployeeId], [Username], [FirstName], [LastName], [Password])
ON Target.EmployeeId = Source.EmployeeId
WHEN NOT MATCHED BY TARGET THEN
INSERT ([Username], [FirstName], [LastName], [Password])
VALUES ([Username], [FirstName], [LastName], [Password]); 

MERGE INTO [Product] AS Target USING (
   VALUES
   ( 1, 'Hemmorhoid Cream'),
   ( 2, 'Aussie Butt Cream'),
   ( 3, 'Genital Shampoo'),
   ( 4, 'Deep Heat' ),
   ( 5, 'Adult Diapers' )
)
AS Source ([ProductId], [ProductName])
ON Target.ProductId = Source.ProductId
WHEN NOT MATCHED BY TARGET THEN
INSERT ([ProductName])
VALUES ([ProductName]);

MERGE INTO [Stock] AS Target USING (
   VALUES
   ( 1, 1 ), ( 2, 1 ), ( 3, 1 ), ( 4, 1 ), ( 5, 1 ), ( 6, 1 ), ( 7, 1 ), ( 8, 1 ), ( 9, 1 ), ( 10, 1 ),
   ( 11, 2 ), ( 12, 2 ), ( 13, 2 ), ( 14, 2 ), ( 15, 2 ), ( 16, 2 ), ( 17, 2 ), ( 18, 2 ), ( 19, 2 ), ( 20, 2 ),
   ( 21, 3 ), ( 22, 3 ), ( 23, 3 ), ( 24, 3 ), ( 25, 3 ), ( 26, 3 ), ( 27, 3 ), ( 28, 3 ), ( 29, 3 ), ( 30, 3 ),
   ( 31, 4 ), ( 32, 4 ), ( 33, 4 ), ( 34, 4 ), ( 35, 4 ), ( 36, 4 ), ( 37, 4 ), ( 38, 4 ), ( 39, 4 ), ( 40, 4 ),
   ( 41, 5 ), ( 42, 5 ), ( 43, 5 ), ( 44, 5 ), ( 45, 5 ), ( 46, 5 ), ( 47, 5 ), ( 48, 5 ), ( 49, 5 ), ( 50, 5 )
)
AS Source ([StockId], [ProductId] )
ON Target.StockId = Source.StockId
WHEN NOT MATCHED  BY TARGET THEN
INSERT ([ProductId])
VALUES ([ProductId]);

END;