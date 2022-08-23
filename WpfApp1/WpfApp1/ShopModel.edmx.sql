
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 04/12/2022 18:07:54
-- Generated from EDMX file: C:\Users\annag\Desktop\WpfApp1\WpfApp1\ShopModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [Shop];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_compukter_corpus]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[compukter] DROP CONSTRAINT [FK_compukter_corpus];
GO
IF OBJECT_ID(N'[dbo].[FK_compukter_Employee]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[compukter] DROP CONSTRAINT [FK_compukter_Employee];
GO
IF OBJECT_ID(N'[dbo].[FK_compukter_hard_drive]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[compukter] DROP CONSTRAINT [FK_compukter_hard_drive];
GO
IF OBJECT_ID(N'[dbo].[FK_compukter_mother]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[compukter] DROP CONSTRAINT [FK_compukter_mother];
GO
IF OBJECT_ID(N'[dbo].[FK_compukter_power]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[compukter] DROP CONSTRAINT [FK_compukter_power];
GO
IF OBJECT_ID(N'[dbo].[FK_compukter_processor]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[compukter] DROP CONSTRAINT [FK_compukter_processor];
GO
IF OBJECT_ID(N'[dbo].[FK_compukter_ram]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[compukter] DROP CONSTRAINT [FK_compukter_ram];
GO
IF OBJECT_ID(N'[dbo].[FK_compukter_videoadapter]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[compukter] DROP CONSTRAINT [FK_compukter_videoadapter];
GO
IF OBJECT_ID(N'[dbo].[FK_mother_socket]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[mother] DROP CONSTRAINT [FK_mother_socket];
GO
IF OBJECT_ID(N'[dbo].[FK_processor_socket]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[processor] DROP CONSTRAINT [FK_processor_socket];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[compukter]', 'U') IS NOT NULL
    DROP TABLE [dbo].[compukter];
GO
IF OBJECT_ID(N'[dbo].[corpus]', 'U') IS NOT NULL
    DROP TABLE [dbo].[corpus];
GO
IF OBJECT_ID(N'[dbo].[Employee]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Employee];
GO
IF OBJECT_ID(N'[dbo].[hard_drive]', 'U') IS NOT NULL
    DROP TABLE [dbo].[hard_drive];
GO
IF OBJECT_ID(N'[dbo].[mother]', 'U') IS NOT NULL
    DROP TABLE [dbo].[mother];
GO
IF OBJECT_ID(N'[dbo].[power]', 'U') IS NOT NULL
    DROP TABLE [dbo].[power];
GO
IF OBJECT_ID(N'[dbo].[processor]', 'U') IS NOT NULL
    DROP TABLE [dbo].[processor];
GO
IF OBJECT_ID(N'[dbo].[ram]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ram];
GO
IF OBJECT_ID(N'[dbo].[socket]', 'U') IS NOT NULL
    DROP TABLE [dbo].[socket];
GO
IF OBJECT_ID(N'[dbo].[sysdiagrams]', 'U') IS NOT NULL
    DROP TABLE [dbo].[sysdiagrams];
GO
IF OBJECT_ID(N'[dbo].[videoadapter]', 'U') IS NOT NULL
    DROP TABLE [dbo].[videoadapter];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'compukter'
CREATE TABLE [dbo].[compukter] (
    [id] nvarchar(50)  NOT NULL,
    [name] nvarchar(50)  NOT NULL,
    [desc] nvarchar(500)  NULL,
    [price] decimal(18,2)  NOT NULL,
    [instock] bit  NULL,
    [photo] varbinary(max)  NOT NULL,
    [id_processor] nvarchar(50)  NOT NULL,
    [id_mother] nvarchar(50)  NOT NULL,
    [id_videoadapter] nvarchar(50)  NULL,
    [id_power] nvarchar(50)  NOT NULL,
    [id_hard_drive] nvarchar(50)  NOT NULL,
    [id_ram] nvarchar(50)  NOT NULL,
    [id_corpus] nvarchar(50)  NOT NULL,
    [id_employee] nvarchar(50)  NOT NULL
);
GO

-- Creating table 'corpus'
CREATE TABLE [dbo].[corpus] (
    [id] nvarchar(50)  NOT NULL,
    [name] nvarchar(50)  NOT NULL,
    [price] decimal(18,2)  NOT NULL,
    [color] int  NOT NULL,
    [formfactor] int  NOT NULL
);
GO

-- Creating table 'Employee'
CREATE TABLE [dbo].[Employee] (
    [id] nvarchar(50)  NOT NULL,
    [name] nvarchar(50)  NOT NULL,
    [login] nvarchar(50)  NOT NULL,
    [password] nvarchar(50)  NOT NULL
);
GO

-- Creating table 'hard_drive'
CREATE TABLE [dbo].[hard_drive] (
    [id] nvarchar(50)  NOT NULL,
    [name] nvarchar(50)  NOT NULL,
    [price] decimal(18,2)  NOT NULL,
    [storage] int  NOT NULL,
    [type] int  NOT NULL
);
GO

-- Creating table 'mother'
CREATE TABLE [dbo].[mother] (
    [id] nvarchar(50)  NOT NULL,
    [name] nvarchar(50)  NOT NULL,
    [id_socket] nvarchar(50)  NOT NULL,
    [freeze] nvarchar(50)  NULL,
    [price] decimal(18,2)  NOT NULL
);
GO

-- Creating table 'power'
CREATE TABLE [dbo].[power] (
    [id] nvarchar(50)  NOT NULL,
    [name] nvarchar(50)  NOT NULL,
    [price] decimal(18,2)  NOT NULL,
    [power1] nvarchar(50)  NOT NULL
);
GO

-- Creating table 'processor'
CREATE TABLE [dbo].[processor] (
    [id] nvarchar(50)  NOT NULL,
    [name] nvarchar(50)  NOT NULL,
    [frequency] decimal(18,2)  NOT NULL,
    [cache_1] nvarchar(50)  NOT NULL,
    [cache_2] nvarchar(50)  NOT NULL,
    [cache_3] nvarchar(50)  NOT NULL,
    [id_socket] nvarchar(50)  NOT NULL,
    [price] decimal(18,2)  NOT NULL
);
GO

-- Creating table 'ram'
CREATE TABLE [dbo].[ram] (
    [id] nvarchar(50)  NOT NULL,
    [name] nvarchar(50)  NOT NULL,
    [price] decimal(18,2)  NOT NULL,
    [storage] int  NOT NULL,
    [type] int  NOT NULL
);
GO

-- Creating table 'socket'
CREATE TABLE [dbo].[socket] (
    [id] nvarchar(50)  NOT NULL,
    [name] nvarchar(50)  NOT NULL
);
GO

-- Creating table 'sysdiagrams'
CREATE TABLE [dbo].[sysdiagrams] (
    [name] nvarchar(128)  NOT NULL,
    [principal_id] int  NOT NULL,
    [diagram_id] int IDENTITY(1,1) NOT NULL,
    [version] int  NULL,
    [definition] varbinary(max)  NULL
);
GO

-- Creating table 'videoadapter'
CREATE TABLE [dbo].[videoadapter] (
    [id] nvarchar(50)  NOT NULL,
    [name] nvarchar(50)  NOT NULL,
    [price] decimal(18,2)  NOT NULL,
    [video_memory] int  NOT NULL,
    [type] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [id] in table 'compukter'
ALTER TABLE [dbo].[compukter]
ADD CONSTRAINT [PK_compukter]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- Creating primary key on [id] in table 'corpus'
ALTER TABLE [dbo].[corpus]
ADD CONSTRAINT [PK_corpus]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- Creating primary key on [id] in table 'Employee'
ALTER TABLE [dbo].[Employee]
ADD CONSTRAINT [PK_Employee]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- Creating primary key on [id] in table 'hard_drive'
ALTER TABLE [dbo].[hard_drive]
ADD CONSTRAINT [PK_hard_drive]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- Creating primary key on [id] in table 'mother'
ALTER TABLE [dbo].[mother]
ADD CONSTRAINT [PK_mother]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- Creating primary key on [id] in table 'power'
ALTER TABLE [dbo].[power]
ADD CONSTRAINT [PK_power]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- Creating primary key on [id] in table 'processor'
ALTER TABLE [dbo].[processor]
ADD CONSTRAINT [PK_processor]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- Creating primary key on [id] in table 'ram'
ALTER TABLE [dbo].[ram]
ADD CONSTRAINT [PK_ram]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- Creating primary key on [id] in table 'socket'
ALTER TABLE [dbo].[socket]
ADD CONSTRAINT [PK_socket]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- Creating primary key on [diagram_id] in table 'sysdiagrams'
ALTER TABLE [dbo].[sysdiagrams]
ADD CONSTRAINT [PK_sysdiagrams]
    PRIMARY KEY CLUSTERED ([diagram_id] ASC);
GO

-- Creating primary key on [id] in table 'videoadapter'
ALTER TABLE [dbo].[videoadapter]
ADD CONSTRAINT [PK_videoadapter]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [id_corpus] in table 'compukter'
ALTER TABLE [dbo].[compukter]
ADD CONSTRAINT [FK_compukter_corpus]
    FOREIGN KEY ([id_corpus])
    REFERENCES [dbo].[corpus]
        ([id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_compukter_corpus'
CREATE INDEX [IX_FK_compukter_corpus]
ON [dbo].[compukter]
    ([id_corpus]);
GO

-- Creating foreign key on [id_employee] in table 'compukter'
ALTER TABLE [dbo].[compukter]
ADD CONSTRAINT [FK_compukter_Employee]
    FOREIGN KEY ([id_employee])
    REFERENCES [dbo].[Employee]
        ([id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_compukter_Employee'
CREATE INDEX [IX_FK_compukter_Employee]
ON [dbo].[compukter]
    ([id_employee]);
GO

-- Creating foreign key on [id_hard_drive] in table 'compukter'
ALTER TABLE [dbo].[compukter]
ADD CONSTRAINT [FK_compukter_hard_drive]
    FOREIGN KEY ([id_hard_drive])
    REFERENCES [dbo].[hard_drive]
        ([id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_compukter_hard_drive'
CREATE INDEX [IX_FK_compukter_hard_drive]
ON [dbo].[compukter]
    ([id_hard_drive]);
GO

-- Creating foreign key on [id_mother] in table 'compukter'
ALTER TABLE [dbo].[compukter]
ADD CONSTRAINT [FK_compukter_mother]
    FOREIGN KEY ([id_mother])
    REFERENCES [dbo].[mother]
        ([id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_compukter_mother'
CREATE INDEX [IX_FK_compukter_mother]
ON [dbo].[compukter]
    ([id_mother]);
GO

-- Creating foreign key on [id_power] in table 'compukter'
ALTER TABLE [dbo].[compukter]
ADD CONSTRAINT [FK_compukter_power]
    FOREIGN KEY ([id_power])
    REFERENCES [dbo].[power]
        ([id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_compukter_power'
CREATE INDEX [IX_FK_compukter_power]
ON [dbo].[compukter]
    ([id_power]);
GO

-- Creating foreign key on [id_processor] in table 'compukter'
ALTER TABLE [dbo].[compukter]
ADD CONSTRAINT [FK_compukter_processor]
    FOREIGN KEY ([id_processor])
    REFERENCES [dbo].[processor]
        ([id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_compukter_processor'
CREATE INDEX [IX_FK_compukter_processor]
ON [dbo].[compukter]
    ([id_processor]);
GO

-- Creating foreign key on [id_ram] in table 'compukter'
ALTER TABLE [dbo].[compukter]
ADD CONSTRAINT [FK_compukter_ram]
    FOREIGN KEY ([id_ram])
    REFERENCES [dbo].[ram]
        ([id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_compukter_ram'
CREATE INDEX [IX_FK_compukter_ram]
ON [dbo].[compukter]
    ([id_ram]);
GO

-- Creating foreign key on [id_videoadapter] in table 'compukter'
ALTER TABLE [dbo].[compukter]
ADD CONSTRAINT [FK_compukter_videoadapter]
    FOREIGN KEY ([id_videoadapter])
    REFERENCES [dbo].[videoadapter]
        ([id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_compukter_videoadapter'
CREATE INDEX [IX_FK_compukter_videoadapter]
ON [dbo].[compukter]
    ([id_videoadapter]);
GO

-- Creating foreign key on [id_socket] in table 'mother'
ALTER TABLE [dbo].[mother]
ADD CONSTRAINT [FK_mother_socket]
    FOREIGN KEY ([id_socket])
    REFERENCES [dbo].[socket]
        ([id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_mother_socket'
CREATE INDEX [IX_FK_mother_socket]
ON [dbo].[mother]
    ([id_socket]);
GO

-- Creating foreign key on [id_socket] in table 'processor'
ALTER TABLE [dbo].[processor]
ADD CONSTRAINT [FK_processor_socket]
    FOREIGN KEY ([id_socket])
    REFERENCES [dbo].[socket]
        ([id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_processor_socket'
CREATE INDEX [IX_FK_processor_socket]
ON [dbo].[processor]
    ([id_socket]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------