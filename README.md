## SQL setting
- Create User
```
CREATE LOGIN authorizationSa WITH PASSWORD = '!QAZ3edc@WSX';

USE [Authorization];
CREATE USER authorizationSa FOR LOGIN authorizationSa;

ALTER ROLE db_datareader ADD MEMBER authorizationSa; 
ALTER ROLE db_datawriter ADD MEMBER authorizationSa;
```