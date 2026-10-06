CREATE TABLE [dbo].[Members] (
    [Id]        INT            IDENTITY (1, 1) NOT NULL,
    [MemberId]  NVARCHAR (50)  NOT NULL,
    [Name]      NVARCHAR (100) NOT NULL,
    [Email]     NVARCHAR (50)  NOT NULL,
    [CreatedAt] DATETIME2 (7)  DEFAULT (getutcdate()) NOT NULL,
    [UpdatedAt] DATETIME2 (7)  NULL,
    [Status]    INT            NOT NULL,
    CONSTRAINT [PK_Members] PRIMARY KEY CLUSTERED ([Id] ASC)
);

