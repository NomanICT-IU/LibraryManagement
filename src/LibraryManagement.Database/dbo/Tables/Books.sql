CREATE TABLE [dbo].[Books] (
    [Id]        INT            IDENTITY (1, 1) NOT NULL,
    [Name]      NVARCHAR (255) NOT NULL,
    [Author]    NVARCHAR (255) NOT NULL,
    [ISBN]      NVARCHAR (20)  NOT NULL,
    [CreatedAt] DATETIME2 (7)  DEFAULT (getutcdate()) NOT NULL,
    [Quantity]  INT            NOT NULL,
    [Status]    INT            DEFAULT ((1)) NOT NULL,
    CONSTRAINT [PK_Books] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_Books_ISBN]
    ON [dbo].[Books]([ISBN] ASC);

