	-- BD PARA SISTEMA DE VENTA EL FOGON CON ASP.NET WEB FORM
-- INSERTS
USE BDElFogon;
GO

INSERT INTO Negocio (Nombre) VALUES ('El Fogón');
GO

INSERT INTO Rol (Nombre)
VALUES ('Administrador'),
	   ('Cajero');
GO

INSERT INTO Usuario(Nombre, Contra, NegocioId, RolId)
VALUES ('David', '$2a$11$Zhs4wRW6CUXxOUOvIJczNuWcKIMPPZNR.XudrE5VfWKJ1FD.Z0ECy', 1, 1),
	   ('pepe','$2a$11$Zhs4wRW6CUXxOUOvIJczNuWcKIMPPZNR.XudrE5VfWKJ1FD.Z0ECy',1,2);
GO

INSERT INTO Permisos(FormRuta, FormNombre)
VALUES
-- =================================================================
-- Dashboard y Reportes
-- =================================================================
('~/Default.aspx', 'Dashboard'),

-- =================================================================
-- Módulo de Administración: Usuarios
-- =================================================================
('~/Pages/Usuarios/Usuarios.aspx', 'Ver Usuarios'),
('~/Pages/Usuarios/RegistrarUsuarios.aspx', 'Registrar Usuario'),
('~/Pages/Usuarios/EditarUsuario.aspx', 'Editar Usuario'),
('~/Pages/Usuarios/EliminarUsuario.aspx', 'Eliminar Usuario'),

-- =================================================================
-- Módulo de Administración: Roles
-- =================================================================
('~/Pages/Rol/Rol.aspx', 'Ver Roles'),
('~/Pages/Rol/RegistrarRol.aspx', 'Registrar Rol'),
('~/Pages/Rol/EditarRol.aspx', 'Editar Rol'),
('~/Pages/Rol/EliminarRol.aspx', 'Eliminar Rol'),

-- =================================================================
-- Módulo de Administración: Permisos
-- =================================================================
('~/Pages/Permisos/Permisos.aspx', 'Ver Permisos'),
('~/Pages/Permisos/RegistrarPermisos.aspx', 'Registrar Permiso'),
('~/Pages/Permisos/EditarPermisos.aspx', 'Editar Permiso'),
('~/pages/RolPermisos/RolPermisos.aspx', 'Asignar Permisos a Rol'),

-- =================================================================
-- Módulo de Ventas
-- =================================================================
('~/Pages/Ventas/Ventas.aspx', 'Ver Ventas'),
('~/Pages/Ventas/RegistrarVentas.aspx', 'Registrar Venta'),
('~/Pages/Ventas/EditarVentas.aspx', 'Editar Venta'),
('~/Pages/Ventas/EliminarVentas.aspx', 'Eliminar Venta'),
('~/Pages/Ventas/DetalleVentas.aspx', 'Ver Detalle de Venta'),

-- =================================================================
-- Módulo de Clientes
-- =================================================================
('~/Pages/Clientes/Clientes.aspx', 'Ver Clientes'),
('~/Pages/Clientes/RegistrarCliente.aspx', 'Registrar Cliente'),
('~/Pages/Clientes/EditarCliente.aspx', 'Editar Cliente'),
('~/Pages/Clientes/EliminarCliente.aspx', 'Eliminar Cliente'),

-- =================================================================
-- Módulo de Productos
-- =================================================================
('~/Pages/Productos/Productos.aspx', 'Ver Productos'),
('~/Pages/Productos/RegistrarProductos.aspx', 'Registrar Producto'),
('~/Pages/Productos/EditarProducto.aspx', 'Editar Producto'),
('~/Pages/Productos/EliminarProducto.aspx', 'Editar Producto'),

-- =================================================================
-- Módulo de Categorías de Producto
-- =================================================================
('~/Pages/ProductoCategoria/ProductoCategorias.aspx', 'Ver Categorías de Producto'),
('~/Pages/ProductoCategoria/RegistrarProductoCategoria.aspx', 'Registrar Categoría de Producto'),
('~/Pages/ProductoCategoria/EditarProductoCategorias.aspx', 'Editar Categoría de Producto'),
('~/Pages/ProductoCategoria/EliminarProductoCategoria.aspx', 'Eliminar Categoría de Producto'),

-- =================================================================
-- Módulo de Compras
-- =================================================================
('~/Pages/Compras/Compras.aspx', 'Ver Compras'),
('~/Pages/Compras/RegistrarCompra.aspx', 'Registrar Compra'),
('~/Pages/Compras/VerCompra.aspx', 'Ver Detalle de Compra'),

-- =================================================================
-- Módulo de Proveedores
-- =================================================================
('~/Pages/Proveedores/Proveedores.aspx', 'Ver Proveedores'),
('~/Pages/Proveedores/RegistrarProveedor.aspx', 'Registrar Proveedor'),
('~/Pages/Proveedores/EditarProveedor.aspx', 'Editar Proveedor'),
('~/Pages/Proveedores/EliminarProveedor.aspx', 'Eliminar Proveedor'),

-- =================================================================
-- Módulo de Inventario: Insumos
-- =================================================================
('~/Pages/Insumos/Insumos.aspx', 'Ver Insumos'),
('~/Pages/Insumos/RegistrarInsumo.aspx', 'Registrar Insumo'),
('~/Pages/Insumos/EditarInsumo.aspx', 'Editar Insumo'),
('~/Pages/Insumos/EliminarInsumo.aspx', 'Eliminar Insumo'),

-- =================================================================
-- Módulo de Inventario: Categorías de Insumo
-- =================================================================
('~/Pages/InsumoCategoria/InsumoCategorias.aspx', 'Ver Categorías de Insumo'),
('~/Pages/InsumoCategoria/RegistrarInsumoCategoria.aspx', 'Registrar Categoría de Insumo'),
('~/Pages/InsumoCategoria/EditarInsumoCategoria.aspx', 'Editar Categoría de Insumo'),
('~/Pages/InsumoCategoria/EliminarInsumoCategoria.aspx', 'Eliminar Categoría de Insumo'),

-- =================================================================
-- Módulo de Inventario: Movimientos
-- =================================================================
('~/Pages/MovimientoInventario/MovimientosInventario.aspx', 'Ver Movimientos de Inventario'),

-- =================================================================
-- Módulo de Configuración: Unidades de Medida
-- =================================================================
('~/Pages/UnidadesMedida/UnidadesMedida.aspx', 'Ver Unidades de Medida'),
('~/Pages/UnidadesMedida/RegistrarUnidadMedida.aspx', 'Registrar Unidad de Medida'),
('~/Pages/UnidadesMedida/EditarUnidadMedida.aspx', 'Editar Unidad de Medida'),
('~/Pages/UnidadesMedida/EliminarUnidadMedida.aspx', 'Eliminar Unidad de Medida'),

-- =================================================================
-- Módulo de Configuración: Métodos de Pago
-- =================================================================
('~/Pages/MetodosPago/MetodosPago.aspx', 'Ver Métodos de Pago'),
('~/Pages/MetodosPago/RegistrarMetodoPago.aspx', 'Registrar Método de Pago'),
('~/Pages/MetodosPago/EditarMetodoPago.aspx', 'Editar Método de Pago'),
('~/Pages/MetodosPago/EliminarMetodoPago.aspx', 'Eliminar Método de Pago'),

-- =================================================================
-- Módulo de Configuración: Negocio
-- =================================================================
('~/Pages/Negocio/Negocio.aspx', 'Ver Datos del Negocio'),
('~/Pages/Negocio/EditarNegocio.aspx', 'Editar Datos del Negocio');

INSERT INTO RolPermisos(RolId, PermisosId)
VALUES (1, 1), 
	   (1, 2), 
       (1, 3), 
       (1, 4), 
       (1, 5), 
       (1, 6), 
       (1, 7), 
       (1, 8), 
       (1, 9), 
       (1, 10),
       (1, 11),
       (1, 12),
       (1, 13),
       (1, 14),
       (1, 15),
       (1, 16),
       (1, 17),
       (1, 18),
       (1, 19),
       (1, 20),
       (1, 21),
       (1, 22),
       (1, 23),
       (1, 24),
       (1, 25),
       (1, 26),
       (1, 27),
       (1, 28),
       (1, 29),
       (1, 30),
       (1, 31),
       (1, 32),
       (1, 33),
       (1, 34),
       (1, 35),
       (1, 36),
       (1, 37),
       (1, 38),
       (1, 39),
       (1, 40),
       (1, 41),
       (1, 42),
       (1, 43),
       (1, 44),
       (1, 45),
       (1, 46),
       (1, 47),
       (1, 48),
       (1, 49),
       (1, 50),
       (1, 51),
       (1, 52),
       (1, 53),
       (1, 54),
       (1, 55);
GO

INSERT INTO ProductoCategoria(Nombre)
VALUES	('Platos'),
		('Gaseosas'),
		('Refrescos');
GO

INSERT INTO Proveedor(Nombre, Contacto)
VALUES	('Jose Covarrubias','61316555'),
		('Daniel Perez','76504224'),
		('Maria','4234223'),
		('Ruben','61316566');
GO

INSERT INTO Producto(Nombre, Precio, ProductoCategoriaId)
VALUES	('Ala de pollo (1/4)',19,1);
GO

INSERT INTO Cliente(Nombre, EsComerciante, NumeroLocal, Pasillo)
VALUES	('Cliente normal',0,'',''),
		('Pedro',1,'15','D'),
		('Jose',1,'13','C');
GO

INSERT INTO MetodoPago(Nombre)
VALUES	('Efectivo'),
		('Qr');
GO

INSERT INTO UnidadesMedida(Nombre, Abreviatura)
VALUES	('Kilogramos','Kg'),
		('Unidad', 'Un');
GO

INSERT INTO InsumoCategoria(Nombre)
VALUES	('Carnes'),
		('Vegetales'),
		('Descartables');
GO

INSERT INTO Insumo(Nombre, InsumoCategoriaId, ProveedorId, UnidadesMedidaId)
VALUES	('Pollo',1,1,2),
		('Botella Coca Cola 2l',3,3,2),
		('Vaso plastico',3,2,2);
GO

INSERT INTO ProductoInsumo(ProductoId, InsumoId, Cantidad, Tipo)
VALUES	(1, 1, 0.25 , 'Comestible');
GO