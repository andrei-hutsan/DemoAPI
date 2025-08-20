if not exists (select 1 from dbo.[Person])
begin
	insert into dbo.[Person] (Id, Firstname, Lastname)
	values(NEWID(), 'Michael', 'Scot'),
	(NEWID(), 'Jim', 'Halpert'),
	(NEWID(), 'Dwight', 'Schroot'),
	(NEWID(), 'Pam', 'Halpert');
end
