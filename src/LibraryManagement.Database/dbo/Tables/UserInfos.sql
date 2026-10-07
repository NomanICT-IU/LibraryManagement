CREATE TABLE [dbo].[UserInfos] (
    [Id]     INT IDENTITY (1, 1) NOT NULL,
    [UserId] INT NOT NULL,
    [RoleId] INT NOT NULL,
    [Status] INT DEFAULT ((1)) NOT NULL,
    CONSTRAINT [PK_UserInfos] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_UserInfos_Roles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [dbo].[Roles] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_UserInfos_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [dbo].[Users] ([Id]) ON DELETE CASCADE
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_UserInfos_UserId_RoleId]
    ON [dbo].[UserInfos]([UserId] ASC, [RoleId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_UserInfos_RoleId]
    ON [dbo].[UserInfos]([RoleId] ASC);

