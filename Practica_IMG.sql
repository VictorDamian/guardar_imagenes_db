create database Practica_Imagenes
go
use Practica_Imagenes
go

create table IMAGENES(
IDIMG INT IDENTITY (1,1) PRIMARY KEY,
IMAGEN IMAGE NOT NULL
)
GO
select*from imagenes
SELECT IMAGEN
FROM IMAGENES Where IDIMG=1
