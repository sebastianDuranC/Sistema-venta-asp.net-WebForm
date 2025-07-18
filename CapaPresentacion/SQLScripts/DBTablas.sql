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
    Estado BIT NOT NULL DEFAULT 1
);

CREATE TABLE Rol (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Nombre NVARCHAR(100) NOT NULL,
    Estado BIT NOT NULL DEFAULT 1
);

CREATE TABLE Usuario (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Nombre NVARCHAR(100) NOT NULL,
    Contra NVARCHAR(300) NOT NULL,
    Estado BIT NOT NULL DEFAULT 1,
    NegocioId INT NOT NULL,
    RolId INT NOT NULL,
    FOREIGN KEY (NegocioId) REFERENCES Negocio(Id),
    FOREIGN KEY (RolId) REFERENCES Rol(Id)
);

CREATE TABLE Permisos (
    Id INT PRIMARY KEY IDENTITY(1,1),
    FormNombre NVARCHAR(100) NOT NULL,
    FormRuta NVARCHAR(100) NOT NULL, -- Page/FormsAccess.aspx, Page/Login.aspx
    Estado BIT NOT NULL DEFAULT 1
);

CREATE TABLE RolPermisos (
    Id INT PRIMARY KEY IDENTITY(1,1),
    RolId INT NOT NULL,
    PermisosId INT NOT NULL,
    Estado BIT NOT NULL DEFAULT 1,
    FOREIGN KEY (RolId) REFERENCES Rol(Id),
    FOREIGN KEY (PermisosId) REFERENCES Permisos(Id)
);

CREATE TABLE ProductoCategoria (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Nombre NVARCHAR(100) NOT NULL,
    Estado BIT NOT NULL DEFAULT 1
);

CREATE TABLE Proveedor (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Nombre NVARCHAR(100) NOT NULL,
    Contacto NVARCHAR(100) NULL,
    Estado BIT NOT NULL DEFAULT 1
);

CREATE TABLE Producto (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Nombre NVARCHAR(150) NOT NULL,
    Precio DECIMAL(10,2) NOT NULL,
    ProductoCategoriaId INT NOT NULL,
    FotoUrl NVARCHAR(300) NULL,
    Estado BIT NOT NULL DEFAULT 1,
    FOREIGN KEY (ProductoCategoriaId) REFERENCES ProductoCategoria(Id),
);

CREATE TABLE Cliente (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Nombre NVARCHAR(150) NOT NULL,
    EsComerciante BIT NOT NULL DEFAULT 0, --1= Cliente de tipo Comerciante, 0= Cliente normal
    NumeroLocal NVARCHAR(20) NULL,
    Pasillo NVARCHAR(50) NULL,
	--SaldoPlatosPendiente DECIMAL(10,2)
    Estado BIT NOT NULL DEFAULT 1
);

CREATE TABLE MetodoPago (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Nombre NVARCHAR(100) NOT NULL,
    Estado BIT NOT NULL DEFAULT 1
);

CREATE TABLE Venta (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Fecha DATETIME NOT NULL DEFAULT GETDATE(), --año, mes, día, hora, minutos
    Total DECIMAL(10,2) NOT NULL,
    EnLocal BIT NOT NULL, -- 1 = En local, 0 = Para llevar
    ClienteId INT NULL,
    UsuarioId INT NOT NULL,
    MetodoPagoId INT NOT NULL,
	MontoRecibido DECIMAL(10,2) NULL,
	CambioDevuelto DECIMAL(10,2) NULL,
    Estado BIT NOT NULL DEFAULT 1,
    FOREIGN KEY (ClienteId) REFERENCES Cliente(Id),
    FOREIGN KEY (UsuarioId) REFERENCES Usuario(Id),
    FOREIGN KEY (MetodoPagoId) REFERENCES MetodoPago(Id)
);

CREATE TABLE DetalleVenta (
    Id INT PRIMARY KEY IDENTITY(1,1),
    VentaId INT NOT NULL,
    ProductoId INT NOT NULL,
    Cantidad INT NOT NULL, --ANTES DECIMAL ESTABA MAL
    SubTotal DECIMAL(10,2) NOT NULL,
    Estado BIT NOT NULL DEFAULT 1,
    FOREIGN KEY (VentaId) REFERENCES Venta(Id),
    FOREIGN KEY (ProductoId) REFERENCES Producto(Id)
);

CREATE TABLE Compra (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Fecha DATETIME NOT NULL DEFAULT GETDATE(),
    Total DECIMAL(10,2) NOT NULL,
    UsuarioId INT NOT NULL,
	ProveedorId INT NOT NULL,
    Estado BIT NOT NULL DEFAULT 1,
	FOREIGN KEY (UsuarioId) REFERENCES Usuario(Id),
    FOREIGN KEY (ProveedorId) REFERENCES Proveedor(Id)
);

CREATE TABLE UnidadesMedida (
   Id INT PRIMARY KEY IDENTITY(1,1),
   Nombre NVARCHAR(50) NOT NULL,
   Abreviatura NVARCHAR(50) NOT NULL,
   Estado BIT NOT NULL DEFAULT 1		
);

CREATE TABLE InsumoCategoria(
	Id INT PRIMARY KEY IDENTITY(1,1),
    Nombre NVARCHAR(100) NOT NULL,
    Estado BIT NOT NULL DEFAULT 1
);

CREATE TABLE Insumo (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Nombre NVARCHAR(150) NOT NULL,
    Costo DECIMAL(10,2) NULL DEFAULT 0.00,
    Stock DECIMAL(10,2) NULL DEFAULT 0.00, --stock actual
	StockMinimo DECIMAL(10,2) NULL DEFAULT 0.00,
    InsumoCategoriaId INT NOT NULL,
    ProveedorId INT NOT NULL,
    FotoUrl NVARCHAR(300) NULL,
    Estado BIT NOT NULL DEFAULT 1,
    UnidadesMedidaId INT NOT NULL DEFAULT 1,
    FOREIGN KEY (InsumoCategoriaId) REFERENCES InsumoCategoria(Id),
    FOREIGN KEY (ProveedorId) REFERENCES Proveedor(Id),
    FOREIGN KEY (UnidadesMedidaId) REFERENCES UnidadesMedida(Id)
);

CREATE TABLE DetalleCompra (
    Id INT PRIMARY KEY IDENTITY(1,1),
    CompraId INT NOT NULL,
    InsumoId INT NOT NULL,
    Cantidad DECIMAL(10,2) NOT NULL,
    Costo DECIMAL(10,2) NOT NULL,
    Estado BIT NOT NULL DEFAULT 1,
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
    Estado BIT NOT NULL DEFAULT 1
    FOREIGN KEY (InsumoId) REFERENCES Insumo(Id)
);

--control de insumo en un plato(carnes) y que plato y vaso va usar(descartables)
CREATE TABLE ProductoInsumo (
    Id INT PRIMARY KEY IDENTITY(1,1),
    ProductoId INT NOT NULL,
    InsumoId INT NOT NULL,
    Cantidad DECIMAL(10,2) NOT NULL,
    Tipo NVARCHAR(20) NOT NULL CHECK (Tipo IN ('Comestible', 'Descartable')),
    Estado BIT NOT NULL DEFAULT 1,
    FOREIGN KEY (ProductoId) REFERENCES Producto(Id),
    FOREIGN KEY (InsumoId) REFERENCES Insumo(Id)
);
