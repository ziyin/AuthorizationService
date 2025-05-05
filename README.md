## SQL setting
- Create User
```
CREATE LOGIN authorizationSa WITH PASSWORD = '!QAZ3edc@WSX';

USE [Authorization];
CREATE USER authorizationSa FOR LOGIN authorizationSa;

ALTER ROLE db_datareader ADD MEMBER authorizationSa; 
ALTER ROLE db_datawriter ADD MEMBER authorizationSa;
```

- Create User Table
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

ALTER TABLE Users ADD Enable BIT NOT NULL DEFAULT 0;
```