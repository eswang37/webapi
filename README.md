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

## POST: /api/users to create account
{
  "userId": 0,
  "password": "test",
  "accountId": 1,
  "firstName": "jun",
  "lastName": "test",
  "email": "xxx@gmail.com",
  "message": "",
  "created": "2018-05-15T21:15:17.284Z",
  "is_Activated": true,
  "is_Disabled": true
}

## PUT: /api/users/active/{token}

## POST: http://api.andmap.co/api/tokens/access 
POST /api/tokens/access
{
  "email": "xxxx@xxxx.com",
  "password": "xxxxxxxxxxxxxxxxxxxxxxx"
}