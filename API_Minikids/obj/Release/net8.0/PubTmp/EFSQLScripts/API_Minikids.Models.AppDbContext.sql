IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240902223103_CriandoBancoDeDados'
)
BEGIN
    CREATE TABLE [Clientes] (
        [Id] int NOT NULL IDENTITY,
        [Nome] nvarchar(max) NOT NULL,
        [Sobrenome] nvarchar(max) NOT NULL,
        [Celular] nvarchar(max) NOT NULL,
        [Email] nvarchar(max) NOT NULL,
        [Endereco] nvarchar(max) NOT NULL,
        CONSTRAINT [PK_Clientes] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240902223103_CriandoBancoDeDados'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20240902223103_CriandoBancoDeDados', N'8.0.8');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240903031843_AddEventoTable'
)
BEGIN
    CREATE TABLE [Eventos] (
        [Id] int NOT NULL IDENTITY,
        [Data] datetime2 NOT NULL,
        [Pacote] nvarchar(max) NOT NULL,
        [TempoDeFesta] time NOT NULL,
        [Endereco] nvarchar(max) NOT NULL,
        [ClienteId] int NOT NULL,
        CONSTRAINT [PK_Eventos] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Eventos_Clientes_ClienteId] FOREIGN KEY ([ClienteId]) REFERENCES [Clientes] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240903031843_AddEventoTable'
)
BEGIN
    CREATE INDEX [IX_Eventos_ClienteId] ON [Eventos] ([ClienteId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240903031843_AddEventoTable'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20240903031843_AddEventoTable', N'8.0.8');
END;
GO

COMMIT;
GO

