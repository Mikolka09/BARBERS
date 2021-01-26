CREATE TABLE Barbers(
	[id] INT IDENTITY(1,1) PRIMARY KEY,
	[name] NVARCHAR(200),
	[id_gender] INT,
	[dt_birthday] DATE NOT NULL,
	[dt_work] DATE NOT NULL
)