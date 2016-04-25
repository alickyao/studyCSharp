
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 04/25/2016 19:49:20
-- Generated from EDMX file: E:\studyCSharp\NewCyclone\NewCyclone\DataBase\SysModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [newcyclone];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_Db_SysUserSysUserLog]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Db_SysMsgSet_Db_SysUserLog] DROP CONSTRAINT [FK_Db_SysUserSysUserLog];
GO
IF OBJECT_ID(N'[dbo].[FK_Db_SysUserLog_inherits_Db_SysMsg]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Db_SysMsgSet_Db_SysUserLog] DROP CONSTRAINT [FK_Db_SysUserLog_inherits_Db_SysMsg];
GO
IF OBJECT_ID(N'[dbo].[FK_Db_ManagerUser_inherits_Db_SysUser]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Db_SysUserSet_Db_ManagerUser] DROP CONSTRAINT [FK_Db_ManagerUser_inherits_Db_SysUser];
GO
IF OBJECT_ID(N'[dbo].[FK_Db_MemberUser_inherits_Db_SysUser]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Db_SysUserSet_Db_MemberUser] DROP CONSTRAINT [FK_Db_MemberUser_inherits_Db_SysUser];
GO
IF OBJECT_ID(N'[dbo].[FK_Db_SysExceptionLog_inherits_Db_SysMsg]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Db_SysMsgSet_Db_SysExceptionLog] DROP CONSTRAINT [FK_Db_SysExceptionLog_inherits_Db_SysMsg];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Db_SysUserSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Db_SysUserSet];
GO
IF OBJECT_ID(N'[dbo].[Db_SysMsgSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Db_SysMsgSet];
GO
IF OBJECT_ID(N'[dbo].[Db_SysTreeSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Db_SysTreeSet];
GO
IF OBJECT_ID(N'[dbo].[Db_SysMsgSet_Db_SysUserLog]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Db_SysMsgSet_Db_SysUserLog];
GO
IF OBJECT_ID(N'[dbo].[Db_SysUserSet_Db_ManagerUser]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Db_SysUserSet_Db_ManagerUser];
GO
IF OBJECT_ID(N'[dbo].[Db_SysUserSet_Db_MemberUser]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Db_SysUserSet_Db_MemberUser];
GO
IF OBJECT_ID(N'[dbo].[Db_SysMsgSet_Db_SysExceptionLog]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Db_SysMsgSet_Db_SysExceptionLog];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Db_SysUserSet'
CREATE TABLE [dbo].[Db_SysUserSet] (
    [loginName] nvarchar(50)  NOT NULL,
    [createdOn] datetime  NOT NULL,
    [isDeleted] bit  NOT NULL,
    [isDisabled] bit  NOT NULL,
    [lastLoginTime] datetime  NULL,
    [passWord] varchar(50)  NOT NULL
);
GO

-- Creating table 'Db_SysMsgSet'
CREATE TABLE [dbo].[Db_SysMsgSet] (
    [Id] bigint IDENTITY(1,1) NOT NULL,
    [createdOn] datetime  NOT NULL,
    [msgType] int  NOT NULL,
    [message] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Db_SysTreeSet'
CREATE TABLE [dbo].[Db_SysTreeSet] (
    [Id] nvarchar(50)  NOT NULL,
    [name] nvarchar(max)  NOT NULL,
    [parentId] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Db_SysMsgSet_Db_SysUserLog'
CREATE TABLE [dbo].[Db_SysMsgSet_Db_SysUserLog] (
    [Db_SysUser_loginName] nvarchar(50)  NOT NULL,
    [logType] int  NOT NULL,
    [fkId] nvarchar(50)  NULL,
    [Id] bigint  NOT NULL
);
GO

-- Creating table 'Db_SysUserSet_Db_ManagerUser'
CREATE TABLE [dbo].[Db_SysUserSet_Db_ManagerUser] (
    [fullName] nvarchar(50)  NOT NULL,
    [mobilePhone] nvarchar(50)  NULL,
    [jobTitle] nvarchar(50)  NULL,
    [loginName] nvarchar(50)  NOT NULL
);
GO

-- Creating table 'Db_SysUserSet_Db_MemberUser'
CREATE TABLE [dbo].[Db_SysUserSet_Db_MemberUser] (
    [nickName] nvarchar(50)  NULL,
    [loginName] nvarchar(50)  NOT NULL
);
GO

-- Creating table 'Db_SysMsgSet_Db_SysExceptionLog'
CREATE TABLE [dbo].[Db_SysMsgSet_Db_SysExceptionLog] (
    [errorCode] int  NOT NULL,
    [condtion] varchar(max)  NULL,
    [source] varchar(max)  NOT NULL,
    [stackTrace] varchar(max)  NOT NULL,
    [targetSite] varchar(max)  NOT NULL,
    [Id] bigint  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [loginName] in table 'Db_SysUserSet'
ALTER TABLE [dbo].[Db_SysUserSet]
ADD CONSTRAINT [PK_Db_SysUserSet]
    PRIMARY KEY CLUSTERED ([loginName] ASC);
GO

-- Creating primary key on [Id] in table 'Db_SysMsgSet'
ALTER TABLE [dbo].[Db_SysMsgSet]
ADD CONSTRAINT [PK_Db_SysMsgSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Db_SysTreeSet'
ALTER TABLE [dbo].[Db_SysTreeSet]
ADD CONSTRAINT [PK_Db_SysTreeSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Db_SysMsgSet_Db_SysUserLog'
ALTER TABLE [dbo].[Db_SysMsgSet_Db_SysUserLog]
ADD CONSTRAINT [PK_Db_SysMsgSet_Db_SysUserLog]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [loginName] in table 'Db_SysUserSet_Db_ManagerUser'
ALTER TABLE [dbo].[Db_SysUserSet_Db_ManagerUser]
ADD CONSTRAINT [PK_Db_SysUserSet_Db_ManagerUser]
    PRIMARY KEY CLUSTERED ([loginName] ASC);
GO

-- Creating primary key on [loginName] in table 'Db_SysUserSet_Db_MemberUser'
ALTER TABLE [dbo].[Db_SysUserSet_Db_MemberUser]
ADD CONSTRAINT [PK_Db_SysUserSet_Db_MemberUser]
    PRIMARY KEY CLUSTERED ([loginName] ASC);
GO

-- Creating primary key on [Id] in table 'Db_SysMsgSet_Db_SysExceptionLog'
ALTER TABLE [dbo].[Db_SysMsgSet_Db_SysExceptionLog]
ADD CONSTRAINT [PK_Db_SysMsgSet_Db_SysExceptionLog]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [Db_SysUser_loginName] in table 'Db_SysMsgSet_Db_SysUserLog'
ALTER TABLE [dbo].[Db_SysMsgSet_Db_SysUserLog]
ADD CONSTRAINT [FK_Db_SysUserSysUserLog]
    FOREIGN KEY ([Db_SysUser_loginName])
    REFERENCES [dbo].[Db_SysUserSet]
        ([loginName])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Db_SysUserSysUserLog'
CREATE INDEX [IX_FK_Db_SysUserSysUserLog]
ON [dbo].[Db_SysMsgSet_Db_SysUserLog]
    ([Db_SysUser_loginName]);
GO

-- Creating foreign key on [Id] in table 'Db_SysMsgSet_Db_SysUserLog'
ALTER TABLE [dbo].[Db_SysMsgSet_Db_SysUserLog]
ADD CONSTRAINT [FK_Db_SysUserLog_inherits_Db_SysMsg]
    FOREIGN KEY ([Id])
    REFERENCES [dbo].[Db_SysMsgSet]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating foreign key on [loginName] in table 'Db_SysUserSet_Db_ManagerUser'
ALTER TABLE [dbo].[Db_SysUserSet_Db_ManagerUser]
ADD CONSTRAINT [FK_Db_ManagerUser_inherits_Db_SysUser]
    FOREIGN KEY ([loginName])
    REFERENCES [dbo].[Db_SysUserSet]
        ([loginName])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating foreign key on [loginName] in table 'Db_SysUserSet_Db_MemberUser'
ALTER TABLE [dbo].[Db_SysUserSet_Db_MemberUser]
ADD CONSTRAINT [FK_Db_MemberUser_inherits_Db_SysUser]
    FOREIGN KEY ([loginName])
    REFERENCES [dbo].[Db_SysUserSet]
        ([loginName])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Id] in table 'Db_SysMsgSet_Db_SysExceptionLog'
ALTER TABLE [dbo].[Db_SysMsgSet_Db_SysExceptionLog]
ADD CONSTRAINT [FK_Db_SysExceptionLog_inherits_Db_SysMsg]
    FOREIGN KEY ([Id])
    REFERENCES [dbo].[Db_SysMsgSet]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------