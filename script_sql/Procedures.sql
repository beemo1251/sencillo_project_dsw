use BD_market
go

-- procedure  para  el  login Paulo
create  proc sp_logueo
@email varchar(200),
@contraseņa varchar(200)
as
Select top 1 c.* from tb_tipo_usuario c 
where id_tipo_usuario = (Select id_tipo_usuario from tb_usuario where email_usuario = @email and contraseņa = @contraseņa)

go

--prueba del  procedure
exec sp_logueo 'usuario01@gmail.com' , 'constraseņa01'
go


-- procedure para el registro Gabriel
create or alter proc sp_registro_cliente
	@dni char(8),
	@nombre varchar(200),
	@apellido varchar(200),
	@email varchar(200),
	@contraseņa varchar(200)
as
begin
	set nocount on;
    insert into tb_cliente(dni_cliente, nombre_cliente, apellido_cliente, id_usuario)
	values(@dni , @nombre, @apellido, 1)
	insert into tb_usuario(email_usuario, contraseņa, id_tipo_usuario)
	values(@email, @contraseņa, 2)
end
go



-- trigger para enlazar el usuario con cliente
create or alter trigger userCliente
on tb_usuario
for insert
as
-- declarar variables
declare @idusu int
declare @idcli int
-- asignar variables
set @idusu = (select id_usuario from inserted)
set @idcli = (select max(id_cliente) from tb_cliente)
-- actualizar tb_cliente
update tb_cliente set id_usuario = @idusu
where id_cliente = @idcli

exec sp_registro_cliente '45678937','jose','estrada','jose@gmail.com','jose'

create or alter proc sp_listar_prod
@descrip varchar(200)
as
select * from tb_producto where descripcion like '%' + @descrip + '%'
go

exec sp_listar_prod 'c'




