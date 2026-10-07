CREATE TABLE [dbo].[BookIssues] (
    [Id]         INT           IDENTITY (1, 1) NOT NULL,
    [BookId]     INT           NOT NULL,
    [UserInfoId] INT           NOT NULL,
    [IssueDate]  DATETIME2 (7) DEFAULT (getutcdate()) NOT NULL,
    [DueDate]    DATETIME2 (7) NOT NULL,
    [ReturnDate] DATETIME2 (7) NULL,
    CONSTRAINT [PK_BookIssues] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_BookIssues_Books_BookId] FOREIGN KEY ([BookId]) REFERENCES [dbo].[Books] ([Id]),
    CONSTRAINT [FK_BookIssues_UserInfos_UserInfoId] FOREIGN KEY ([UserInfoId]) REFERENCES [dbo].[UserInfos] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_BookIssues_UserInfoId]
    ON [dbo].[BookIssues]([UserInfoId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_BookIssues_BookId_UserInfoId]
    ON [dbo].[BookIssues]([BookId] ASC, [UserInfoId] ASC);

