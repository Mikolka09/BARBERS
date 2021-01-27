CREATE TABLE [Clients](
	[id] INT IDENTITY(1,1) PRIMARY KEY,
	[name] NVARCHAR(200),
	[phone] VARCHAR(50),
	[email] VARCHAR(100),
	[id_gender] INT
)