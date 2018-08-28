
Insert tbCustomer ("Name", Username, "Password")
Values ('query', 'asdf', 'asdf')

Select "username", "password" from tbCustomer 

select * from tbCustomer
select * from tbSavings

select * from tbCustomer where "Username" = 'asdf' and "Password" ='asdf'

update tbCustomer set "Name" = 'update' where "Name" = 'query' 

delete from tbCustomer where "Name" = 'query'
/*insert tbSavings (RoutingNumber, Amount, CustomerId)
values ('123-654', 2100.00, )*/