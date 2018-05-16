# webapi

## TODO
1. send email
2. token


## https://github.com/wilsonwu/netcoreauth

##
CREATE TABLE [token] (
    [Id] int IDENTITY(1,1) PRIMARY KEY,
    [user_id] int NOT NULL,
    [jwt_token] nvarchar(max) NOT NULL,
    [token_type] nvarchar(max) NOT NULL
);
##
alter table tbluser add [is_activated] bit default 0 NOT NULL,
    [is_disabled] bit default 0 NOT NULL,
    [password] nvarchar(max)


## get Dapper:
Install-Package Dapper -Version 1.50.4

## get mailkit
Install-Package MailKit

## Swashbuckle.AspNetCore for Swagger API documentation
Package Manager : Install-Package Swashbuckle.AspNetCore
http://localhost:50075/swagger/