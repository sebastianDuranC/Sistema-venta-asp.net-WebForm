-- BD PARA SISTEMA DE VENTA EL FOGON CON ASP.NET WEB FORM

CREATE DATABASE BDElFogon;
GO
USE BDElFogon;
GO

CREATE TABLE Negocio (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Nombre NVARCHAR(150) NOT NULL,
    Direccion NVARCHAR(250) NULL,
    LogoUrl NVARCHAR(300) NULL,
    EstadoId BIT NOT NULL DEFAULT 1
);

CREATE TABLE Rol (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Nombre NVARCHAR(100) NOT NULL,
    EstadoId BIT NOT NULL DEFAULT 1
);

CREATE TABLE Usuario (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Nombre NVARCHAR(100) NOT NULL,
    Apellido NVARCHAR(100) NULL,
    Contra NVARCHAR(300) NOT NULL,
    EstadoId BIT NOT NULL DEFAULT 1,
    NegocioId INT NOT NULL,
    RolId INT NOT NULL,
    FOREIGN KEY (NegocioId) REFERENCES Negocio(Id),
    FOREIGN KEY (RolId) REFERENCES Rol(Id)
);
--insert into Negocio (Nombre) values ('El Fogón');
--insert into Rol (Nombre) 
--values ('Administrador'),
--('Cajero');
--insert into Usuario(Nombre, Contra, NegocioId, RolId)
--values ('David', '0000', 1, 1);
CREATE TABLE Form (
    Id INT PRIMARY KEY IDENTITY(1,1),
    FormNombre NVARCHAR(100) NOT NULL,
    FormRuta NVARCHAR(100) NOT NULL, -- Page/FormsAccess.aspx, Page/Login.aspx
    EstadoId BIT NOT NULL DEFAULT 1
);

CREATE TABLE RolPermisosMapping (
    Id INT PRIMARY KEY IDENTITY(1,1),
    RolId INT NOT NULL,
    FormId INT NOT NULL,
    EstadoId BIT NOT NULL DEFAULT 1,
    FOREIGN KEY (RolId) REFERENCES Rol(Id),
    FOREIGN KEY (FormId) REFERENCES Form(Id)
);

CREATE TABLE Categoria (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Nombre NVARCHAR(100) NOT NULL,
    EstadoId BIT NOT NULL DEFAULT 1
);

CREATE TABLE Proveedor (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Nombre NVARCHAR(100) NOT NULL,
    Contacto NVARCHAR(100) NULL,
    EstadoId BIT NOT NULL DEFAULT 1
);

CREATE TABLE Producto (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Nombre NVARCHAR(150) NOT NULL,
    Precio DECIMAL(10,2) NOT NULL,
    Stock DECIMAL(10,2) NULL,
    CategoriaId INT NOT NULL,
    FotoUrl NVARCHAR(300) NULL,
    EstadoId BIT NOT NULL DEFAULT 1,
    FOREIGN KEY (CategoriaId) REFERENCES Categoria(Id),
);

CREATE TABLE Cliente (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Nombre NVARCHAR(150) NOT NULL,
    EsComerciante BIT NOT NULL DEFAULT 0, --1= Cliente de tipo Comerciante, 0= Cliente normal
    NumeroLocal NVARCHAR(20) NULL,
    Pasillo NVARCHAR(50) NULL,
    EstadoId BIT NOT NULL DEFAULT 1
);

CREATE TABLE MetodoPago (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Nombre NVARCHAR(100) NOT NULL,
    EstadoId BIT NOT NULL DEFAULT 1
);

CREATE TABLE Venta (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Fecha DATETIME NOT NULL DEFAULT GETDATE(), --año, mes, día, hora, minutos
    Total DECIMAL(10,2) NOT NULL,
    EnLocal BIT NOT NULL, -- 1 = En local, 0 = Para llevar
    ClienteId INT NULL,
    UsuarioId INT NOT NULL,
    MetodoPagoId INT NOT NULL,
    EstadoId BIT NOT NULL DEFAULT 1,
    FOREIGN KEY (ClienteId) REFERENCES Cliente(Id),
    FOREIGN KEY (UsuarioId) REFERENCES Usuario(Id),
    FOREIGN KEY (MetodoPagoId) REFERENCES MetodoPago(Id)
);

CREATE TABLE DetalleVenta (
    Id INT PRIMARY KEY IDENTITY(1,1),
    VentaId INT NOT NULL,
    ProductoId INT NOT NULL,
    Cantidad DECIMAL(10,2) NOT NULL,
    SubTotal DECIMAL(10,2) NOT NULL,
    EstadoId BIT NOT NULL DEFAULT 1,
    FOREIGN KEY (VentaId) REFERENCES Venta(Id),
    FOREIGN KEY (ProductoId) REFERENCES Producto(Id)
);

CREATE TABLE Compra (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Fecha DATE NOT NULL DEFAULT GETDATE(), --'Año-mes-dia'
    Total DECIMAL(10,2) NOT NULL,
    UsuarioId INT NOT NULL,
    EstadoId BIT NOT NULL DEFAULT 1
);

CREATE TABLE UnidadesMedida (
   Id INT PRIMARY KEY IDENTITY(1,1),
   Nombre NVARCHAR(50) NOT NULL,
   Abreviatura NVARCHAR(50) NOT NULL,
   EstadoId BIT NOT NULL DEFAULT 1		
);

CREATE TABLE Insumo (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Nombre NVARCHAR(150) NOT NULL,
    Costo DECIMAL(10,2) NULL,
    Stock DECIMAL(10,2) NULL,
    CategoriaId INT NOT NULL,
    ProveedorId INT NOT NULL,
    FotoUrl NVARCHAR(300) NULL,
    EstadoId BIT NOT NULL DEFAULT 1,
    UnidadesMedidaId INT NOT NULL DEFAULT 1,
    FOREIGN KEY (CategoriaId) REFERENCES Categoria(Id),
    FOREIGN KEY (ProveedorId) REFERENCES Proveedor(Id),
    FOREIGN KEY (UnidadesMedidaId) REFERENCES UnidadesMedida(Id)
);

CREATE TABLE DetalleCompra (
    Id INT PRIMARY KEY IDENTITY(1,1),
    CompraId INT NOT NULL,
    InsumoId INT NOT NULL,
    Cantidad DECIMAL(10,2) NOT NULL,
    Costo DECIMAL(10,2) NOT NULL,
    EstadoId BIT NOT NULL DEFAULT 1,
    FOREIGN KEY (CompraId) REFERENCES Compra(Id),
    FOREIGN KEY (InsumoId) REFERENCES Insumo(Id)
);

CREATE TABLE MovimientoInventario (
    Id INT PRIMARY KEY IDENTITY(1,1),
    InsumoId INT NOT NULL,
    Fecha DATETIME NOT NULL DEFAULT GETDATE(),
    TipoMovimiento NVARCHAR(50) NOT NULL, -- Entrada, Salida, Daño, UsoInterno
    Cantidad DECIMAL(10,2) NOT NULL,
    Observacion NVARCHAR(300) NULL,
    UsuarioId INT NOT NULL,
    EstadoId BIT NOT NULL DEFAULT 1
    FOREIGN KEY (InsumoId) REFERENCES Insumo(Id)
);

--PARA CONTROL DE INSUMOS DENTRO DE UN PRODUCTO (ALA DE POLLO TIENE: ALA, ARROZ, PAPA, ETC)

CREATE TABLE ComposicionProducto (
    Id INT PRIMARY KEY IDENTITY(1,1),
    ProductoId INT NOT NULL,
    InsumoId INT NOT NULL,
    Cantidad DECIMAL(10,2) NOT NULL,
    EstadoId BIT NOT NULL DEFAULT 1
    FOREIGN KEY (ProductoId) REFERENCES Producto(Id),
    FOREIGN KEY (InsumoId) REFERENCES Insumo(Id)
);

--PARA CONTROL DE DESCARTABLES PARA UN PRODUCTO PARA LLEVAR

CREATE TABLE ConfiguracionDescartables (
    Id INT PRIMARY KEY IDENTITY(1,1),
    ProductoId INT NOT NULL,
    InsumoId INT NOT NULL,
    Cantidad DECIMAL(10,2) NOT NULL,
    EstadoId BIT NOT NULL DEFAULT 1
    FOREIGN KEY (ProductoId) REFERENCES Producto(Id),
    FOREIGN KEY (InsumoId) REFERENCES Insumo(Id)
);