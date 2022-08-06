create database DB_Contacto
go

use DB_Contacto
go

create table contacto
(
ID_Mensaje int primary key identity (1,1),
Nombre varchar (20) not null,
Apellido varchar (20),
NombreCompleto varchar (50),
Correo varchar (100) not null,
Celular varchar (15),
Mensaje text not null,
FechaMensaje date not null
)
go

select * from contacto
go

