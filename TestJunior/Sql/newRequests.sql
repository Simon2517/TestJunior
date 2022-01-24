--Creazione di una Stored Procedure di paginazione Prodotti. la Stored Procedure prenderà in input i seguenti parametri
--dimensione della pagina,
--il numero di pagina,
--categoria,
--ordinamento: 1 nome prodotto | 2 prezzo crescente | 3 prezzo decrescente

USE TestJuniorSimone;
go
DROP PROCEDURE IF EXISTS dbo.Pricing;  
GO  
CREATE PROCEDURE Pricing
(
@pagenumber int,
@PageSize   INT,
@Category int=0,
@order int=0
)
AS 
BEGIN
	WITH ctepaging 
		 AS (SELECT Product.ProductId,Name,Price,
					Row_number() OVER(ORDER BY case 
													when @order =1 then Product.Name end,
											   case 
													when @order=2 then -Price 
													when @order=3 then  Price end) AS rownum 
			 FROM Product where Product.ProductId in (select ProductId from ProductCategories where @Category=0 or CategoryId=@Category)) 

		SELECT ctepaging.ProductId,ctepaging.Name,ctepaging.Price
		FROM   ctepaging 
		WHERE  rownum BETWEEN ( @PageNumber - 1 )*@PageSize + 1 AND @PageNumber * @PageSize
	
		
END

exec dbo.Pricing 1,10,@order=1
exec dbo.Pricing 1,10,5;


--Batch di allineamento di prodotti senza categorie: (CURSORI)
--prendere tutti i prodotti senza categoria e associarne almeno una random

--deallocate the cursor if exists
IF CURSOR_STATUS('global','ProductCategory_cursor')>=-1
	BEGIN
		DEALLOCATE ProductCategory_cursor
	END
ELSE
	BEGIN
		--allocating cursor
		DECLARE ProductCategory_cursor CURSOR FAST_FORWARD
		FOR select  P.ProductId
		from Product P
		left join ProductCategories PC on P.ProductId=PC.ProductId
		WHERE PC.ProductId IS NULL
	END

declare @random int
declare @ProductId int;
OPEN ProductCategory_cursor  
FETCH NEXT FROM ProductCategory_cursor INTO @ProductId
	WHILE @@FETCH_STATUS = 0 
		BEGIN  
			set @random=(select top 1 Id from Category order by newid())
			insert into ProductCategories values(@ProductId,@random)
			FETCH NEXT FROM ProductCategory_cursor INTO @ProductId
		end
CLOSE ProductCategory_cursor;  
DEALLOCATE ProductCategory_cursor;


--individuare gli utenti guest e registrarli al sito come account utenti

IF CURSOR_STATUS('global','NullRequest_cursor')>=-1
BEGIN
DEALLOCATE NullRequest_cursor
END
ELSE
BEGIN
DECLARE NullRequest_cursor CURSOR FAST_FORWARD
FOR select  Name,LastName,Email
from InfoRequest
WHERE UserId IS NULL
END

declare @Name varchar(100)
declare @LastName varchar(100)
declare @Email varchar(100)
declare @AccountId int
declare @UserId int


OPEN NullRequest_cursor  
FETCH NEXT FROM NullRequest_cursor INTO @Name,@LastName,@Email
WHILE @@FETCH_STATUS = 0 
begin
	if not exists (select Email from Account where Email=@Email)
		begin
			insert into Account values(@Email,'Password'+cast(NEWID() as varchar(100)),2)
			set @AccountId=@@IDENTITY
			insert into [User] values(@AccountId,@Name,@LastName)
			set @UserId=@@IDENTITY
			update InfoRequest set UserId=@UserId where Email=@Email
		end
	FETCH NEXT FROM NullRequest_cursor INTO @Name,@LastName,@Email
end

select * from InfoRequest where UserId is null


--Restituire il seguente elenco prodotti con ordine Custom:
--* come primi, i 20 più recenti,
--* come seconda fascia i primi 100 prodotti con più richieste informazioni ricevute
--* come terza fascia, i 10 con prezzo compreso tra 200 - 500€
--* come quarta fascia, i 100 con nessuna richiesta informazione

select * 
from (select top 10 p.ProductId,p.Name,p.Price 
	  from Product p 
	  order by p.ProductId desc)as First_results

union all
	select Second_results.ProductId,Second_results.Name,Second_results.Price 
	from(select top 100 p.ProductId,p.Name,p.Price,count(Req.Id) as NumReq 
	from InfoRequest Req
	inner join Product P on P.ProductId=Req.ProductId
	group by P.ProductId,P.Name,p.Price
	order by COUNT(Req.Id) desc)as Second_results

union all
	select * from (select top 10 P.ProductId,P.Name,P.Price 
	from Product P 
	where P.Price between 10 and 30 
	order by ProductId) as Third_results

union all 
	select  * from
	(select top 100 P.ProductId,P.Name,P.Price
	from Product P
	left join InfoRequest ir on P.ProductId=ir.ProductId
	WHERE ir.ProductId IS NULL
	order by P.ProductId) as Fourth_results
