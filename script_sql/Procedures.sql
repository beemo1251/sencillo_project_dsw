use BD_sencillo_market
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


-- procedure para el registro Gabriel
create proc sp_registro_cliente
	@dni char(8),
	@nombre varchar(200),
	@apellido varchar(200),
	@email varchar(200),
	@contraseņa varchar(200)
as
begin
	set nocount on;
    insert into tb_cliente(dni_cliente, nombre_cliente, apellido_cliente, id_usuario)
	values(@dni , @nombre, @apellido, 5)
	insert into tb_usuario(email_usuario, contraseņa, id_tipo_usuario)
	values(@email, @contraseņa, 2)
end
go