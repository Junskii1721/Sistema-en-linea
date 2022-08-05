--crearemos la base de datos
create database DB_Loggin
go 


--usaremos la base de datos llamada DB_Loggin
use  Db_Loggin
go

	create table Rol(
	id int primary key,
	nombre varchar (90)
	)
	go

	create table Modulo(
	id int primary key,
	nombre varchar (100)
	)
	go


	create table Operaciones(
	id int primary key,
	nombre varchar(100),
	idModulo int,
	CONSTRAINT FK_Operaciones_idModulo FOREIGN KEY (idModulo)
        REFERENCES Modulo (id)
        ON DELETE CASCADE
        ON UPDATE CASCADE
	)
	go


	create table Rol_Operacion(
		id int primary key ,
		idRol int,
		idOperacion int,
		CONSTRAINT FK_Rol_Operacion_idRol FOREIGN KEY (idRol)
        REFERENCES Rol (id)
        ON DELETE CASCADE
        ON UPDATE CASCADE,

		CONSTRAINT FK_Rol_Operacion_idOperacion FOREIGN KEY (idOperacion)
        REFERENCES Operaciones (id)
        ON DELETE CASCADE
        ON UPDATE CASCADE
	)
	go

	
--Crearemos una tabla llamada Usuario
create table Usuario (
IdUsuario int primary key identity (1,1),
Correo varchar (200),
Contraseña varchar (300),
idRol int ,

CONSTRAINT FK_Usuario FOREIGN KEY (idRol)
REFERENCES Rol (id)
ON DELETE CASCADE
ON UPDATE CASCADE

)
go
--Primer procedimiento almacenado para registrar un usuario

create proc sp_RegistrarUsuario (
@Correo varchar (200),
@Contraseña varchar (300)

)

as
begin
if(not exists(select*from Usuario where Correo = @Correo))
begin
insert into Usuario(Correo,Contraseña,idRol) values (@Correo,@Contraseña,2)

end
end
select * from Usuario






 --ejecutamos el primer procedure

	

	--datos en Roles
	insert into Rol (id,nombre)values(1,'Administrador')
	insert into Rol (id,nombre)values(2,'Usuario')

	select*from Rol
	--Datos Modulo
	insert into Modulo(id,nombre)values(1,'Index')
	insert into Modulo(id,nombre)values(2,'Acerca de')
	insert into Modulo(id,nombre)values(3,'Contacto')
	insert into Modulo(id,nombre)values(4,'Agregar Producto')
	insert into Modulo(id,nombre)values(5,'Inventario')
	insert into Modulo(id,nombre)values(6,'Control Carrusel')

	
	--insertamos datos en Operaciones
	--datos en index
	insert into Operaciones(id,nombre,idModulo)values(1,'Acceso completo',1)
	--datos en acerca de
	insert into Operaciones(id,nombre,idModulo)values(2,'Acceso completo',2)
	insert into Operaciones(id,nombre,idModulo)values(3,'Acceso completo',3)
	insert into Operaciones(id,nombre,idModulo)values(4,'Acceso completo',4)
	insert into Operaciones(id,nombre,idModulo)values(5,'Acceso completo',5)
	insert into Operaciones(id,nombre,idModulo)values(6,'Acceso completo',6)

	--datos Usuario
	insert into Usuario(Correo,Contraseña,idRol)values('juniorjsolis@gmail.com','a665a45920422f9d417e4867efdc4fb8a04a1f3fff1fa07e998e86f7f7a27ae3',1) 

	select*from Usuario

	--datos Rol_Operacion para admin
	insert into Rol_Operacion(id,idRol,idOperacion)values(1,1,1)
	insert into Rol_Operacion(id,idRol,idOperacion)values(2,1,2)
	insert into Rol_Operacion(id,idRol,idOperacion)values(3,1,3)
	insert into Rol_Operacion(id,idRol,idOperacion)values(4,1,4)
	insert into Rol_Operacion(id,idRol,idOperacion)values(5,1,5)
	insert into Rol_Operacion(id,idRol,idOperacion)values(6,1,6)
	--datos Rol_Operacion para usuario
	insert into Rol_Operacion(id,idRol,idOperacion)values(7,2,1)
	insert into Rol_Operacion(id,idRol,idOperacion)values(8,2,2)
	insert into Rol_Operacion(id,idRol,idOperacion)values(9,2,3)

	