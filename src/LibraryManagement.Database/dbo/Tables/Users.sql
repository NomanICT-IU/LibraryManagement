CREATE TABLE [dbo].[Users] (
    [Id]           INT            IDENTITY (1, 1) NOT NULL,
    [Name]         NVARCHAR (150) NOT NULL,
    [Email]        NVARCHAR (255) NOT NULL,
    [PasswordHash] NVARCHAR (500) NOT NULL,
    [CreatedAt]    DATETIME2 (7)  DEFAULT (getutcdate()) NOT NULL,
    [UpdatedAt]    DATETIME2 (7)  NULL,
    CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_Users_Email]
    ON [dbo].[Users]([Email] ASC);

