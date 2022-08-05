CREATE DATABASE DB_Imagenes
GO

USE DB_Imagenes
GO

CREATE TABLE Imagenes(
	id int identity(1,1) primary key,
	imagen image not null 
)
GO
