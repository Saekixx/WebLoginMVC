set dateformat dmy

use master
if DB_ID ( 'ExLogin' ) is not null
  drop database ExLogin
go

create database ExLogin
go

use ExLogin
go

CREATE TABLE Usuarios (
    id INT IDENTITY(1,1) PRIMARY KEY,
    nombre NVARCHAR(50) NOT NULL,
    apellido NVARCHAR(50) NOT NULL,
    dni VARCHAR(20) NOT NULL UNIQUE,
    email NVARCHAR(100) NOT NULL UNIQUE,
    contrasenia NVARCHAR(255) NOT NULL
);
GO

CREATE PROCEDURE sp_CreateUser
    @nombre NVARCHAR(50),
    @apellido NVARCHAR(50),
    @dni VARCHAR(20),
    @email NVARCHAR(100),
    @contrasenia NVARCHAR(255)
AS
BEGIN
    SET NOCOUNT ON;
    INSERT INTO Usuarios (nombre, apellido, dni, email, contrasenia)
    VALUES (@nombre, @apellido, @dni, @email, @contrasenia);
END
GO

CREATE PROCEDURE sp_LoginUser
    @email NVARCHAR(100),
    @contrasenia NVARCHAR(255)
AS
BEGIN
    SET NOCOUNT ON;
    SELECT id, nombre, apellido, dni, email, contrasenia
    FROM Usuarios
    WHERE email = @email AND contrasenia = @contrasenia;
END
GO
    
