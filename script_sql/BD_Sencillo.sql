create database BD_sencillo_market
go

use BD_sencillo_market
go

create table tb_tipo_usuario
(
	id_tipo_usuario int identity(1,1) primary key,
	descripcion varchar(200) not null
)
go

create table tb_usuario
(
	id_usuario int identity(1,1) primary key,
	email_usuario varchar(200) not null,
	contraseña varchar(200) not null,
	id_tipo_usuario int not null,
	constraint fk_tipo_user foreign key (id_tipo_usuario) references tb_tipo_usuario (id_tipo_usuario)
)
go

create table tb_empleado
(
	dni_empleado char(8) primary key,
	nombre_empleado varchar(200) not null,
	apellido_empleado varchar(200) not null,
	direccion_empleado varchar(200) not null,
	fecha_nacimiento date not null,
	id_usuario int not null,
	constraint fk_user_empleado foreign key (id_usuario) references tb_usuario (id_usuario)
)
go

create table tb_cliente
(
	dni_cliente char(8) primary key,
	nombre_cliente varchar(200) not null,
	apellido_cliente varchar(200) not null,
	direccion_cliente varchar(200) not null,
	fecha_nacimiento date not null,
	id_usuario int not null,
	constraint fk_user_cliente foreign key (id_usuario) references tb_usuario (id_usuario)
)
go

create table tb_proveedor
(
	ruc_proveedor char(11) primary key,
	nombre_proveedor varchar(200) not null,
	direccion_proveedor varchar(200) not null,
	telefono_proveedor varchar(9) not null,
	email_proveedor varchar(200) not null,
	estado char(1) check (estado = '1' or estado = '0')
)
go

create table tb_categoria
(
	id_categoria int identity(1,1) primary key,
	descripcion varchar(200) not null,
	estado char(1) check (estado = '1' or estado = '0')
)
go

create table tb_producto
(
	id_producto int identity(1,1) primary key,
	descripcion varchar(200) not null,
	marca varchar(100) not null,
	precio decimal(6,2) not null,
	stock int not null,
	medida varchar(50) not null,
	estado char(1) check (estado = '1' or estado = '0')
	id_categoria int not null,
	constraint fk_categoria_producto foreign key (id_categoria) references tb_categoria (id_categoria)
)
go

create table tb_compra_producto
(
	id_compra int identity(1,1) primary key,
	ruc_proveedor char(11) not null,
	id_producto int not null,
	cantidad int not null,
	fecha date not null,
	constraint fk_compra_proveedor foreign key (ruc_proveedor) references tb_proveedor (ruc_proveedor),
	constraint fk_compra_producto foreign key (id_producto) references tb_producto (id_producto)
)
go

create table tb_pedido
(
	id_pedido int identity(1,1) primary key,
	estado varchar(10) check(estado = 'pendiente' or estado = 'entregado' or estado = 'cancelado'),
	dni_cliente char(8) not null,
	dni_empleado char(8) not null,
	constraint fk_pedido_cliente foreign key (dni_cliente) references tb_cliente (dni_cliente),
	constraint fk_pedido_empleado foreign key (dni_empleado) references tb_empleado (dni_empleado)
)
go

create table tb_boleta
(
	id_boleta int identity(1,1) primary key,
	fecha date not null,
	id_producto int not null,
	cantidad int not null,
	id_pedido int not null,
	constraint fk_boleta_producto foreign key (id_producto) references tb_producto (id_producto),
	constraint fk_boleta_pedido foreign key (id_pedido) references tb_pedido (id_pedido)
)
go


select * from tb_tipo_usuario
go

insert into tb_tipo_usuario (descripcion) values ('empleado')
insert into tb_tipo_usuario (descripcion) values ('cliente')

select * from tb_usuario
go

insert into tb_usuario (email_usuario, contraseña, id_tipo_usuario) values ('usuario01@gmail.com','constraseña01',1)
insert into tb_usuario (email_usuario, contraseña, id_tipo_usuario) values ('usuario02@gmail.com','constraseña02',1)
insert into tb_usuario (email_usuario, contraseña, id_tipo_usuario) values ('usuario03@gmail.com','constraseña03',2)
insert into tb_usuario (email_usuario, contraseña, id_tipo_usuario) values ('usuario04@gmail.com','constraseña04',2)

select * from tb_empleado
go

insert into tb_empleado (dni_empleado, nombre_empleado, apellido_empleado, direccion_empleado, fecha_nacimiento, id_usuario) values ('87654321','Jose','Diaz','Calle de empleado 01','1990/05/10',3)
insert into tb_empleado (dni_empleado, nombre_empleado, apellido_empleado, direccion_empleado, fecha_nacimiento, id_usuario) values ('76543218','Andres','Reyes','Calle de empleado 02','1996/07/20',4)

select * from tb_cliente
go

insert into tb_cliente (dni_cliente, nombre_cliente, apellido_cliente, direccion_cliente, fecha_nacimiento, id_usuario) values ('65432178','Miguel','Hernandez','Calle de cliente 01','1986/12/10',1)
insert into tb_cliente (dni_cliente, nombre_cliente, apellido_cliente, direccion_cliente, fecha_nacimiento, id_usuario) values ('54321678','Gabriel','Osorio','Calle de cliente 02','1971/02/15',2)

select * from tb_proveedor
go

insert into tb_proveedor(ruc_proveedor, nombre_proveedor, direccion_proveedor, telefono_proveedor, email_proveedor, estado) values ('12345678912','Distribuidores SAC','Calle de proveedor 01','987654321','proveedor01@gmail.com','1')
insert into tb_proveedor(ruc_proveedor, nombre_proveedor, direccion_proveedor, telefono_proveedor, email_proveedor, estado) values ('23456789123','Productores SAC','Calle de proveedor 02','876543219','proveedor02@gmail.com','1')

select * from tb_categoria
go

insert into tb_categoria(descripcion, estado) values ('Licores y bebidas','1')
insert into tb_categoria(descripcion, estado) values ('Piqueos y snack','1')
insert into tb_categoria(descripcion, estado) values ('Confiteria y galletas','1')
insert into tb_categoria(descripcion, estado) values ('Panadería y pastelería','1')
insert into tb_categoria(descripcion, estado) values ('Lacteos, helados y embutidos','1')
insert into tb_categoria(descripcion, estado) values ('Limpieza, salud e higiene','1')
insert into tb_categoria(descripcion, estado) values ('Abarrotes','1')

select * from tb_producto
go

insert into tb_producto (descripcion, marca, precio, stock, medida, id_categoria) values ('Pisco Embajador Quebranta', 'Embajador', 25.90, 50, '700ml', '1', 1)
insert into tb_producto (descripcion, marca, precio, stock, medida, id_categoria) values ('Cerveza Revolucion Craft', 'Revolucion', 10.90, 20, '330ml' '1',, 1)
insert into tb_producto (descripcion, marca, precio, stock, medida, id_categoria) values ('Papas Lays Clasicas', 'Frito Lay', 16.50, 30, '160gr', '1', 2)
insert into tb_producto (descripcion, marca, precio, stock, medida, id_categoria) values ('Maní Crocante Karinto', 'Karinto', 3.40, 25, '100gr', '1', 2)
insert into tb_producto (descripcion, marca, precio, stock, medida, id_categoria) values ('Chicle Trident Fresa', 'Trident', 2.90, 35, '30gr', '1', 3)
insert into tb_producto (descripcion, marca, precio, stock, medida, id_categoria) values ('Bizcocho Chocman', 'Costa', 2.90, 30, '30gr', '1', 3)
insert into tb_producto (descripcion, marca, precio, stock, medida, id_categoria) values ('Tres Leches Tambo', 'Tambo', 3.90, 10, '1und', '1', 4)
insert into tb_producto (descripcion, marca, precio, stock, medida, id_categoria) values ('Pan Bimbo Integral', 'Bimbo', 6.90, 15, '480gr', '1', 4)
insert into tb_producto (descripcion, marca, precio, stock, medida, id_categoria) values ('Leche Gloria UHT', 'Gloria', 3.90, 35, '900ml', '1', 5)
insert into tb_producto (descripcion, marca, precio, stock, medida, id_categoria) values ('Helado Donofrio Sublime', 'Donofrio', 2.80, 20, '75ml', '1', 5)
insert into tb_producto (descripcion, marca, precio, stock, medida, id_categoria) values ('Detergente Ariel Revitacolor', 'Ariel', 5.80, 25, '500gr', '1', 6)
insert into tb_producto (descripcion, marca, precio, stock, medida, id_categoria) values ('Lavavajilla Ayudin Limon', 'Ayudin', 2.90, 10, '300gr', '1', 6)
insert into tb_producto (descripcion, marca, precio, stock, medida, id_categoria) values ('Aceite Primor clasico', 'Primor', 8.00, 30, '1lt', '1', 7)
insert into tb_producto (descripcion, marca, precio, stock, medida, id_categoria) values ('Conserva Trozos de Atun Campomar', 'Campomar', 5.20, 30, '170gr', '1', 7)

select * from tb_compra_producto
go

insert into tb_compra_producto (ruc_proveedor, id_producto, cantidad, fecha) values ('12345678912','1',10,'2021/03/20')
insert into tb_compra_producto (ruc_proveedor, id_producto, cantidad, fecha) values ('12345678912','2',5,'2021/02/12')
insert into tb_compra_producto (ruc_proveedor, id_producto, cantidad, fecha) values ('12345678912','3',20,'2021/01/18')
insert into tb_compra_producto (ruc_proveedor, id_producto, cantidad, fecha) values ('23456789123','4',8,'2020/12/06')
insert into tb_compra_producto (ruc_proveedor, id_producto, cantidad, fecha) values ('23456789123','5',16,'2021/03/22')
insert into tb_compra_producto (ruc_proveedor, id_producto, cantidad, fecha) values ('23456789123','6',15,'2021/02/11')

select * from tb_pedido
go

insert into tb_pedido (estado, dni_cliente, dni_empleado) values ('entregado', '65432178', '87654321')
insert into tb_pedido (estado, dni_cliente, dni_empleado) values ('pendiente', '54321678', '76543218')

select * from tb_boleta
go

insert into tb_boleta (fecha, id_producto, cantidad, id_pedido) values ('2021/05/22',1,1,2)
insert into tb_boleta (fecha, id_producto, cantidad, id_pedido) values ('2021/05/22',5,1,1)