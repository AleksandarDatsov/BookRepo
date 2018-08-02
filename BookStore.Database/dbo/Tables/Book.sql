CREATE TABLE [dbo].[Book] (
    [Id]             INT  IDENTITY(1,1)         NOT NULL,
    [AuthorId]       INT           NOT NULL,
    [BookName]       NVARCHAR (50) NOT NULL,
    [ReleaseYear]    NVARCHAR (30) NOT NULL,
    [IsInStock]      BIT           NOT NULL,
    [NumbersInStock] INT           NULL,
    [Price] DECIMAL(18, 2) NULL, 
    CONSTRAINT [PK_Book] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Book_Author] FOREIGN KEY ([AuthorId]) REFERENCES [dbo].[Author] ([Id])
);

