CREATE VIEW IF NOT EXISTS All_orders AS
SELECT Orders.Id AS order_id, 
		Clients.Name AS client_name,
		Clients.Surname AS client_surname,
		Clients.City AS client_city,
					
		Orders.DateCreate AS OrderDateCreate
FROM Orders 
INNER JOIN Clients ON Orders.ClientId = Clients.Id;