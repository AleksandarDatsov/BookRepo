CREATE TABLE [dbo].[Purchase]
(
	[Id] INT					NOT NULL, 
    [BookId] INT				NULL, 
    [UserId] INT				NULL, 
    [IsPurchasedFinished] BIT	NULL,
	CONSTRAINT [PK_Purchase] PRIMARY KEY CLUSTERED ([Id] ASC),
	CONSTRAINT [FK_Purchase_User] FOREIGN KEY (UserId) REFERENCES [dbo].[User] ([Id]),
	CONSTRAINT [FK_Purchase_BookId] FOREIGN KEY ([BookId]) REFERENCES [dbo].[Book] ([Id]),

);
