USE TestJuniorSimone;

--initializing every Identity if the table has at least one record
IF(not EXISTS(SELECT top (1) * FROM dbo.InfoRequestReply))

begin
DBCC CHECKIDENT ('InfoRequestReply', RESEED , 1)
end
else
begin
DELETE FROM InfoRequestReply
DBCC CHECKIDENT ('InfoRequestReply', RESEED , 0)
end

IF(not EXISTS(SELECT top (1) * FROM dbo.InfoRequest))
begin
DBCC CHECKIDENT ('InfoRequest', RESEED , 1)
end
else
begin
DELETE FROM InfoRequest
DBCC CHECKIDENT ('InfoRequest', RESEED , 0)
end

DELETE FROM ProductCategories

IF(not EXISTS(SELECT top (1) * FROM dbo.Category))
begin
DBCC CHECKIDENT ('Category', RESEED , 1)
end
else
begin
DELETE FROM Category
DBCC CHECKIDENT ('Category', RESEED , 0)
end

IF(not EXISTS(SELECT top (1) * FROM dbo.Nation))
begin
DBCC CHECKIDENT ('Nation', RESEED , 1)
end
else
begin
DELETE FROM Nation
DBCC CHECKIDENT ('Nation', RESEED , 0)
end

IF(not EXISTS(SELECT top (1) * FROM dbo.Product))
begin
DBCC CHECKIDENT ('Product', RESEED , 1)
end
else
begin
DELETE FROM Product
DBCC CHECKIDENT ('Product', RESEED , 0)
end

IF(not EXISTS(SELECT top (1) * FROM dbo.Brand))
begin
DBCC CHECKIDENT ('Brand', RESEED , 1)
end
else
begin
DELETE FROM Brand
DBCC CHECKIDENT ('Brand', RESEED , 0)
end

IF(not EXISTS(SELECT top (1) * FROM dbo.[User]))
begin
DBCC CHECKIDENT ('[User]', RESEED , 1)
end
else
begin
DELETE FROM [User]
DBCC CHECKIDENT ('[User]', RESEED , 0)
end


IF(not EXISTS(SELECT top (1) * FROM dbo.Account))
begin
DBCC CHECKIDENT ('Account', RESEED , 1)
end
else
begin
DELETE FROM Account
DBCC CHECKIDENT ('Account', RESEED , 0)
end

--declaring variables
DECLARE @i INT;
declare @NumOfCategories int=20;
declare @NumOfAccounts int=100;
declare @Random int
declare @BrandId int;
declare @ProductId int;
declare @RequestId int;


--declaring cursors
IF CURSOR_STATUS('global','Brand_cursor')>=-1
BEGIN
DEALLOCATE Brand_cursor
END
ELSE
BEGIN
DECLARE Brand_cursor CURSOR FAST_FORWARD
FOR SELECT Id  FROM Brand
END

IF CURSOR_STATUS('global','Product_cursor')>=-1
BEGIN
DEALLOCATE Product_cursor
END
ELSE
BEGIN
DECLARE Product_cursor CURSOR FAST_FORWARD
FOR SELECT ProductId  
FROM Product
END

IF CURSOR_STATUS('global','Request_cursor')>=-1
BEGIN
DEALLOCATE Request_cursor
END
ELSE
BEGIN
DECLARE Request_cursor CURSOR FAST_FORWARD
FOR SELECT Id  
FROM InfoRequest
END



--inserting Nations
insert into Nation (Name) values('Italy');
insert into Nation (Name) values('England');
insert into Nation (Name) values('US');
insert into Nation (Name) values('Brazil');


--insertion of 20 Categories
set @i=1;
WHILE (@i<=@NumOfCategories)
	BEGIN
		INSERT INTO Category(Name)
		VALUES('Category Name '+CONVERT(varchar(255),@i));
		SET @i=@i+1;
	END
		SET @i=1;



--Inserting 100 Accounts of which 
--half are of type Brand and the others are of type User
while @i<=@NumOfAccounts
	begin
		if @i<=@NumOfAccounts/2
		begin
			insert into Account(Email,Password,AccountType)
			values('Email '+convert(varchar(100),@i),'Password '+convert(varchar(100),@i),1)
			insert into Brand(AccountId,BrandName,Description)
			values(@@IDENTITY,'BrandName '+convert(varchar(10),@i),'Description')
		end
		else
		begin
			insert into Account(Email,Password,AccountType)
			values('Email '+convert(varchar(100),@i),'Password '+convert(varchar(10),@i),2)
			insert into [User](AccountId,Name,LastName)
			values(@@IDENTITY,'FisrtName '+convert(varchar(10),@i-@NumOfAccounts/2),'LastName'+convert(varchar(10),@i-@NumOfAccounts/2))
		end
		set @i+=1

	end

-- inserting 10 to 50 Products per each Brand Account 
-- the number is generated randomly
declare @CountBrand int=1;
OPEN Brand_cursor  
FETCH NEXT FROM Brand_cursor INTO @BrandId

WHILE @@FETCH_STATUS = 0  
BEGIN  
	set @Random=ROUND(RAND() * (50-10)+10, 0);
	while(@CountBrand<=@Random)
		begin
		insert into Product(BrandId,Name,Price,ShortDescription,Description)
		values(@BrandId,convert(varchar(10),@BrandId)+'Name'+convert(varchar(10),@CountBrand),ROUND(RAND() * (50-10)+10, 2),'Short','Description')
		SET @CountBrand+=1
	end
	set @CountBrand=1
	FETCH NEXT FROM Brand_cursor INTO @BrandId
END
CLOSE Brand_cursor;  
DEALLOCATE Brand_cursor;


--inserting 0 to 5 categories for each products
--Number of categories generated randomly

declare @CountBrandCategories int=1
OPEN Product_cursor
FETCH NEXT FROM Product_cursor INTO @ProductId
WHILE @@FETCH_STATUS = 0  
BEGIN  
	SET @Random = ROUND(RAND() * (5), 0)
	while (@CountBrandCategories<=@Random)
		begin
			INSERT INTO ProductCategories (ProductId,CategoryId)
			select top 1 @ProductId, Id from Category where Id not in (select CategoryId from ProductCategories where ProductId=@ProductId )order by newid()
			SET @CountBrandCategories=@CountBrandCategories+1
		end
	set @CountBrandCategories=1
	FETCH NEXT FROM Product_cursor INTO @ProductId
end
close Product_cursor
--inserting 0 to 10 InfoRequest for each products
--Number of requests generated randomly

open Product_cursor
declare @CountInfoReq int=1
declare @RandomUser int;
FETCH NEXT FROM Product_cursor INTO @ProductId
WHILE @@FETCH_STATUS = 0  
BEGIN  
	SET @Random = ROUND(RAND() * (10), 0)
	while(@CountInfoReq<@Random)
		begin
			set @RandomUser= ROUND(RAND() * (50), 0)
			if @RandomUser>0
				begin
					INSERT INTO InfoRequest(UserId,ProductId,Name,LastName,Email,InsertedDate,StateId,RequestText)
					select top 1 U.Id,@ProductId,U.Name,U.LastName,'Email'+cast(U.AccountId as varchar(36)),
					DATEADD(day, (ABS(CHECKSUM(NEWID())) % 65530), 0),N.Id,'text'
					from [User] U,Nation N
					where U.Id=@RandomUser
					order by NEWID()
				end
			else
				begin
					INSERT INTO InfoRequest(ProductId,Name,LastName,Email,InsertedDate,StateId,RequestText)
					select top 1 @ProductId,'GuestName'+CONVERT(varchar(10),@CountInfoReq),'GuestLastName'+CONVERT(varchar(10),@CountInfoReq),
								'Email'+cast(newid() as varchar(36)),DATEADD(day, (ABS(CHECKSUM(NEWID())) % 65530), 0),N.Id,'text'
					from Nation N
					order by NEWID()
				end
			set @CountInfoReq+=1
		end
		set @CountInfoReq=1
		FETCH NEXT	FROM Product_cursor INTO @ProductId
	END
close Product_cursor
DEALLOCATE Product_cursor;

--inserting from 1 to 3 replies for each inforequest
--the number is generated randomly and the first request always comes from a guest or User
open Request_cursor
declare @CountReplies int=1
declare @RandomAccount int;
FETCH NEXT FROM Request_cursor INTO @RequestId
WHILE @@FETCH_STATUS = 0  
BEGIN  
	SET @Random = ROUND(RAND() * (3-1)+1, 0)
	while(@CountReplies<=@Random)
		begin
			if @CountReplies=1
				begin					set @RandomAccount= ROUND(RAND() * (@NumOfAccounts/2)+@NumOfAccounts/2, 0)
					INSERT INTO InfoRequestReply(InfoRequestId,AccountId,ReplyText,InsertedDate)
					values (@RequestId,@RandomAccount ,'text',DATEADD(day, (ABS(CHECKSUM(NEWID())) % 65530), 0))
				end
			else
				begin
					set @RandomAccount= ROUND(RAND() * (@NumOfAccounts-1)+1, 0)
					INSERT INTO InfoRequestReply(InfoRequestId,AccountId,ReplyText,InsertedDate)
					values (@RequestId,@RandomAccount ,'text',DATEADD(day, (ABS(CHECKSUM(NEWID())) % 65530), 0))
				end
			set @CountReplies+=1
		end
		set @CountReplies=1
		FETCH NEXT	FROM Request_cursor INTO @RequestId
END
close Request_cursor
DEALLOCATE Request_cursor;
