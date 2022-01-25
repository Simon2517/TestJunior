drop database TestJuniorSimone
CREATE DATABASE TestJuniorSimone;
GO
USE TestJuniorSimone;
--creating Account Table
CREATE TABLE Account(
Id INT PRIMARY KEY IDENTITY,
Email VARCHAR(100) NOT NULL unique,
Password VARCHAR(100) NOT NULL,
AccountType tinyint not null
)
go
--creating Brand Table
CREATE TABLE Brand(
Id INT PRIMARY KEY IDENTITY,
AccountId INT NOT NULL unique,
BrandName VARCHAR(100) NOT NULL,
Description VARCHAR(255),
)
go
--creating Foreign key between Brand and Account
alter table Brand 
add constraint FK_Account_BrandAccountId 
foreign key(AccountId) references Account(Id);
go
--creating User Table
CREATE TABLE [User](
Id INT PRIMARY KEY IDENTITY,
AccountId INT NOT NULL unique,
Name VARCHAR(100) NOT NULL,
LastName VARCHAR(100) NOT NULL
)
go
--creating Foreign key between User and Account
alter table [User] 
add constraint FK_Account_UserAccountId 
foreign key(AccountId) references Account(Id)
go

--creating Product table
CREATE TABLE Product(
ProductId INT PRIMARY KEY IDENTITY,
BrandId INT NOT NULL,
Name VARCHAR(150) NOT NULL,
ShortDescription VARCHAR(255),
Price DECIMAL(18,2) NOT NULL,
Description VARCHAR(255)
)
go
--creating Foreign key between Brand and Product
alter table Product 
add constraint FK_Product_BrandAccountId 
foreign key (BrandId) references Brand(Id)
go
--creating Category table
CREATE TABLE Category(
Id INT PRIMARY KEY IDENTITY,
Name VARCHAR(50) NOT NULL
)
go
--creating Product Categories table
CREATE TABLE ProductCategories(
ProductId INT ,
CategoryId INT,
PRIMARY KEY(ProductId,CategoryId)
)
go
--creating Foreign key between Product and ProductCatgories
alter table ProductCategories 
add constraint FK_ProductCategories_ProductId 
foreign key (ProductId) references Product(ProductId)
go
--creating Foreign key between Category and ProductCatgories
alter table ProductCategories 
add constraint FK_ProductCategories_CategoryId 
foreign key (CategoryId) references Category(Id)
go
--creating Nation table
CREATE TABLE Nation(
Id INT PRIMARY KEY IDENTITY,
Name VARCHAR(50) NOT NULL
)
go
--creating InfoRequest table
CREATE TABLE InfoRequest(
Id INT PRIMARY KEY IDENTITY,
UserId INT ,
ProductId INT NOT NULL,
Name VARCHAR(100) NOT NULL,
LastName VARCHAR(100) NOT NULL,
Email VARCHAR(100) NOT NULL,
City VARCHAR(100),
StateId INT NOT NULL,
Phone VARCHAR(10),
PostalCode VARCHAR(10),
RequestText VARCHAR(255) NOT NULL,
InsertedDate DATETIME NOT NULL
)
go
--creating foreign key between User and Inforequest
alter table InfoRequest 
add constraint FK_InfoRequest_UserAccountId 
foreign key (UserId)references [User](Id)
go
--creating foreign key between Product and Inforequest
alter table InfoRequest 
add constraint FK_InfoRequest_ProductId 
foreign key (ProductId)references Product(ProductId)
go
--creating foreign key between Nation and Inforequest
alter table InfoRequest 
add constraint FK_InfoRequest_NationId 
foreign key (StateId)references Nation(Id)
go
--creating InforequestReply table
CREATE TABLE InfoRequestReply(
Id INT PRIMARY KEY IDENTITY,
AccountId INT NOT NULL,
InforequestId INT NOT NULL,
ReplyText VARCHAR(255) NOT NULL,
InsertedDate DATETIME NOT NULL
)
go
--creating foreign key between Account and InforequestReply
alter table InfoRequestReply 
add constraint FK_InfoRequestReply_AccountId 
foreign key (AccountId)references Account(Id)
go
--creating foreign key between Inforequest and InforequestReply
alter table InfoRequestReply 
add constraint FK_InfoRequest_InfoRequestId 
foreign key (InfoRequestId)references InfoRequest(Id)
go