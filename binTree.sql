create table bin_tree(
id int,
name varchar(255),
leftval int,
rightval int
)
with recursive traversed(id,name,path,leftval,rightval,level) as(
	select b.id,cast(b.name as varchar(255)) ,CAST(b.id as varchar(255)),b.leftval,b.rightval, 1
	from bin_tree as b where id = 1
	UNION 
select b.id,b.name,CAST(t.path || '_' ||b.id as varchar(255)),b.leftval,b.rightval,t.level + 1
	from traversed as t
	left join bin_tree as b on ( b.id = t.leftval or b.id = t.rightval) 
	where  t.leftval is not null or t.rightval is not null
)
select * from traversed
