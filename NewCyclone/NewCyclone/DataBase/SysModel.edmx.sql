
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 05/09/2016 00:07:00
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

IF OBJECT_ID(N'[dbo].[FK_Db_SysDocDb_DocCat]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Db_DocCatSet] DROP CONSTRAINT [FK_Db_SysDocDb_DocCat];
GO
IF OBJECT_ID(N'[dbo].[FK_Db_SysDocDb_DocFile]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Db_DocFileSet] DROP CONSTRAINT [FK_Db_SysDocDb_DocFile];
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
IF OBJECT_ID(N'[dbo].[FK_Db_SysUserLog_inherits_Db_SysMsg]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Db_SysMsgSet_Db_SysUserLog] DROP CONSTRAINT [FK_Db_SysUserLog_inherits_Db_SysMsg];
GO
IF OBJECT_ID(N'[dbo].[FK_Db_CatTree_inherits_Db_SysTree]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Db_SysTreeSet_Db_CatTree] DROP CONSTRAINT [FK_Db_CatTree_inherits_Db_SysTree];
GO
IF OBJECT_ID(N'[dbo].[FK_Db_DocWebCms_inherits_Db_SysDoc]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Db_SysDocSet_Db_DocWebCms] DROP CONSTRAINT [FK_Db_DocWebCms_inherits_Db_SysDoc];
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
IF OBJECT_ID(N'[dbo].[Db_SysFileSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Db_SysFileSet];
GO
IF OBJECT_ID(N'[dbo].[Db_SysDocSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Db_SysDocSet];
GO
IF OBJECT_ID(N'[dbo].[Db_DocCatSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Db_DocCatSet];
GO
IF OBJECT_ID(N'[dbo].[Db_DocFileSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Db_DocFileSet];
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
IF OBJECT_ID(N'[dbo].[Db_SysMsgSet_Db_SysUserLog]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Db_SysMsgSet_Db_SysUserLog];
GO
IF OBJECT_ID(N'[dbo].[Db_SysTreeSet_Db_CatTree]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Db_SysTreeSet_Db_CatTree];
GO
IF OBJECT_ID(N'[dbo].[Db_SysDocSet_Db_DocWebCms]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Db_SysDocSet_Db_DocWebCms];
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
    [passWord] varchar(50)  NOT NULL,
    [role] varchar(50)  NOT NULL
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
    [Id] varchar(50)  NOT NULL,
    [parentId] varchar(50)  NULL,
    [createdOn] datetime  NOT NULL,
    [isDeleted] bit  NOT NULL
);
GO

-- Creating table 'Db_SysFileSet'
CREATE TABLE [dbo].[Db_SysFileSet] (
    [Id] varchar(50)  NOT NULL,
    [createdOn] datetime  NOT NULL,
    [filePath] varchar(max)  NOT NULL,
    [fileName] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Db_SysDocSet'
CREATE TABLE [dbo].[Db_SysDocSet] (
    [Id] nvarchar(50)  NOT NULL,
    [createdOn] datetime  NOT NULL,
    [createdBy] nvarchar(50)  NOT NULL,
    [modifiedOn] datetime  NOT NULL,
    [modifiedBy] nvarchar(50)  NOT NULL,
    [isDeleted] bit  NOT NULL,
    [caption] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Db_DocCatSet'
CREATE TABLE [dbo].[Db_DocCatSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Db_SysDocId] nvarchar(50)  NOT NULL,
    [Db_CatTreeId] nvarchar(50)  NOT NULL
);
GO

-- Creating table 'Db_DocFileSet'
CREATE TABLE [dbo].[Db_DocFileSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Db_SysDocId] nvarchar(50)  NOT NULL,
    [Db_SysFileId] nvarchar(50)  NOT NULL
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

-- Creating table 'Db_SysMsgSet_Db_SysUserLog'
CREATE TABLE [dbo].[Db_SysMsgSet_Db_SysUserLog] (
    [Db_SysUser_loginName] nvarchar(50)  NOT NULL,
    [logType] int  NOT NULL,
    [fkId] nvarchar(50)  NULL,
    [ip] varchar(50)  NULL,
    [device] varchar(max)  NULL,
    [Id] bigint  NOT NULL
);
GO

-- Creating table 'Db_SysTreeSet_Db_CatTree'
CREATE TABLE [dbo].[Db_SysTreeSet_Db_CatTree] (
    [name] nvarchar(max)  NOT NULL,
    [fun] varchar(50)  NULL,
    [sort] int  NULL,
    [Id] varchar(50)  NOT NULL
);
GO

-- Creating table 'Db_SysDocSet_Db_DocWebCms'
CREATE TABLE [dbo].[Db_SysDocSet_Db_DocWebCms] (
    [fun] nvarchar(50)  NOT NULL,
    [Id] nvarchar(50)  NOT NULL
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

-- Creating primary key on [Id] in table 'Db_SysFileSet'
ALTER TABLE [dbo].[Db_SysFileSet]
ADD CONSTRAINT [PK_Db_SysFileSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Db_SysDocSet'
ALTER TABLE [dbo].[Db_SysDocSet]
ADD CONSTRAINT [PK_Db_SysDocSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Db_DocCatSet'
ALTER TABLE [dbo].[Db_DocCatSet]
ADD CONSTRAINT [PK_Db_DocCatSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Db_DocFileSet'
ALTER TABLE [dbo].[Db_DocFileSet]
ADD CONSTRAINT [PK_Db_DocFileSet]
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

-- Creating primary key on [Id] in table 'Db_SysMsgSet_Db_SysUserLog'
ALTER TABLE [dbo].[Db_SysMsgSet_Db_SysUserLog]
ADD CONSTRAINT [PK_Db_SysMsgSet_Db_SysUserLog]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Db_SysTreeSet_Db_CatTree'
ALTER TABLE [dbo].[Db_SysTreeSet_Db_CatTree]
ADD CONSTRAINT [PK_Db_SysTreeSet_Db_CatTree]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Db_SysDocSet_Db_DocWebCms'
ALTER TABLE [dbo].[Db_SysDocSet_Db_DocWebCms]
ADD CONSTRAINT [PK_Db_SysDocSet_Db_DocWebCms]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [Db_SysDocId] in table 'Db_DocCatSet'
ALTER TABLE [dbo].[Db_DocCatSet]
ADD CONSTRAINT [FK_Db_SysDocDb_DocCat]
    FOREIGN KEY ([Db_SysDocId])
    REFERENCES [dbo].[Db_SysDocSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Db_SysDocDb_DocCat'
CREATE INDEX [IX_FK_Db_SysDocDb_DocCat]
ON [dbo].[Db_DocCatSet]
    ([Db_SysDocId]);
GO

-- Creating foreign key on [Db_SysDocId] in table 'Db_DocFileSet'
ALTER TABLE [dbo].[Db_DocFileSet]
ADD CONSTRAINT [FK_Db_SysDocDb_DocFile]
    FOREIGN KEY ([Db_SysDocId])
    REFERENCES [dbo].[Db_SysDocSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Db_SysDocDb_DocFile'
CREATE INDEX [IX_FK_Db_SysDocDb_DocFile]
ON [dbo].[Db_DocFileSet]
    ([Db_SysDocId]);
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

-- Creating foreign key on [Id] in table 'Db_SysMsgSet_Db_SysUserLog'
ALTER TABLE [dbo].[Db_SysMsgSet_Db_SysUserLog]
ADD CONSTRAINT [FK_Db_SysUserLog_inherits_Db_SysMsg]
    FOREIGN KEY ([Id])
    REFERENCES [dbo].[Db_SysMsgSet]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Id] in table 'Db_SysTreeSet_Db_CatTree'
ALTER TABLE [dbo].[Db_SysTreeSet_Db_CatTree]
ADD CONSTRAINT [FK_Db_CatTree_inherits_Db_SysTree]
    FOREIGN KEY ([Id])
    REFERENCES [dbo].[Db_SysTreeSet]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Id] in table 'Db_SysDocSet_Db_DocWebCms'
ALTER TABLE [dbo].[Db_SysDocSet_Db_DocWebCms]
ADD CONSTRAINT [FK_Db_DocWebCms_inherits_Db_SysDoc]
    FOREIGN KEY ([Id])
    REFERENCES [dbo].[Db_SysDocSet]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------