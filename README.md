## First time use
1. Please create the database first and update the connection string in appsettings.
2. Please use the "Create System Administrator" API to set up the system administrator first.

※ use customer sdk

https://github.com/ziyin?tab=packages
-> need get token from me

## SQL setting
- Create User
```
CREATE LOGIN authorizationSa WITH PASSWORD = '!QAZ3edc@WSX';

USE [Authorization];
CREATE USER authorizationSa FOR LOGIN authorizationSa;

ALTER ROLE db_datareader ADD MEMBER authorizationSa; 
ALTER ROLE db_datawriter ADD MEMBER authorizationSa;
```

- User Table
```
CREATE TABLE Users
(
    Id UNIQUEIDENTIFIER NOT NULL PRIMARY KEY DEFAULT NEWID(),
    [Name] NVARCHAR(100) NOT NULL,
    Account NVARCHAR(100) NOT NULL,
    [Password] NVARCHAR(100) NOT NULL,
    RegionBusinessUnit VARCHAR(10) NOT NULL,
    Email NVARCHAR(255) NULL,
    Phone NVARCHAR(50) NULL,
    [Address] NVARCHAR(255) NULL,
    CreateTime DATETIME NOT NULL DEFAULT GETDATE(),
    Creator UNIQUEIDENTIFIER NOT NULL
);

ALTER TABLE Users ADD [Enable] BIT NOT NULL DEFAULT 1;
ALTER TABLE Users ADD LastModified DATETIME NULL;
ALTER TABLE Users ADD LastModifiedBy UNIQUEIDENTIFIER NULL;

CREATE INDEX IX_Users_Account ON Users(Account);
```

- Roles Table
```
CREATE TABLE Roles (
    Id UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    [Name] NVARCHAR(100) NOT NULL UNIQUE,
    [Enable] BIT NOT NULL DEFAULT 1,
    CreateTime DATETIME NOT NULL DEFAULT GETDATE(),
    Creator UNIQUEIDENTIFIER NOT NULL
);

ALTER TABLE Roles ADD LastModified DATETIME NULL;
ALTER TABLE Roles ADD LastModifiedBy UNIQUEIDENTIFIER NULL;

CREATE INDEX IX_Roles_Name ON Roles(Name);
```

- UserRoles Table
```
CREATE TABLE UserRoles (
    Id UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),

    UserId UNIQUEIDENTIFIER NOT NULL,
    RoleId UNIQUEIDENTIFIER NOT NULL,

    CreateTime DATETIME NOT NULL DEFAULT GETDATE(),
    Creator UNIQUEIDENTIFIER NOT NULL,

    CONSTRAINT FK_UserRoles_User FOREIGN KEY (UserId) REFERENCES Users(Id),
    CONSTRAINT FK_UserRoles_Role FOREIGN KEY (RoleId) REFERENCES Roles(Id),

    CONSTRAINT UQ_UserRoles_UserId_RoleId UNIQUE (UserId, RoleId)
);
```

- Permissions Table
```
CREATE TABLE Permissions (
    Id UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    Code NVARCHAR(100) NOT NULL UNIQUE,
    Name NVARCHAR(100) NOT NULL,
    Enable BIT NOT NULL DEFAULT 1,
    CreateTime DATETIME NOT NULL DEFAULT GETDATE(),
    Creator UNIQUEIDENTIFIER NOT NULL,
    LastModified DATETIME NULL,
    LastModifiedBy UNIQUEIDENTIFIER NULL
);
```

- RolePermissions Table
```
CREATE TABLE RolePermissions (
    Id UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    RoleId UNIQUEIDENTIFIER NOT NULL,
    PermissionId UNIQUEIDENTIFIER NOT NULL,
    CreateTime DATETIME NOT NULL DEFAULT GETDATE(),
    Creator UNIQUEIDENTIFIER NOT NULL,
    CONSTRAINT FK_RolePermissions_Role FOREIGN KEY (RoleId) REFERENCES Roles(Id),
    CONSTRAINT FK_RolePermissions_Permission FOREIGN KEY (PermissionId) REFERENCES Permissions(Id),
    CONSTRAINT UQ_RolePermission_RoleId_PermissionId UNIQUE (RoleId, PermissionId)
);
```