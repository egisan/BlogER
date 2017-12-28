use BlogER;

ALTER TABLE [dbo].[Categories]
DROP COLUMN [Description];

CREATE TABLE [dbo].[Categories] ( 
    [CategoryId] INT IDENTITY (1, 1) NOT NULL, 
    [Name] NVARCHAR (200) NULL, 
    CONSTRAINT [PK_dbo.Categories] PRIMARY KEY CLUSTERED ([CategoryId] ASC) 
); 


CREATE TABLE [dbo].[Posts] ( 
    [PostId] INT IDENTITY (1, 1) NOT NULL, 
    [Title] NVARCHAR (50) NULL, 
    [Content] NVARCHAR (2000) NULL, 
	[PostedOn] DATE, 
    [CategoryId] INT NOT NULL, 
    CONSTRAINT [PK_dbo.Posts] 
		PRIMARY KEY CLUSTERED ([PostId] ASC), 
    CONSTRAINT [FK_dbo.Posts_dbo.Categories_CategoryId] 
		FOREIGN KEY ([CategoryId]) REFERENCES [dbo].[Categories] ([CategoryId]) 
		ON DELETE CASCADE 
);