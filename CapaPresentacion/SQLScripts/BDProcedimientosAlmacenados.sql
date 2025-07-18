USE BDElFogon;
GO

-------------------------------------------
--   Procedimiento almacenado Negocio    --
-------------------------------------------
CREATE PROCEDURE sp_ObtenerNegocio
AS BEGIN
	SELECT Id, Nombre, Direccion, LogoUrl FROM Negocio
	WHERE Estado = 1;
END
GO

CREATE PROCEDURE sp_ObtenerNegocioPorId
@Id INT
AS BEGIN
	SELECT Id, Nombre, Direccion, LogoUrl FROM Negocio
	WHERE Estado = 1 AND Id = @Id;
END
GO

CREATE PROCEDURE sp_EditarNegocio
	@Id INT,
    @Nombre NVARCHAR(150),
    @Direccion NVARCHAR(250),
    @LogoUrl NVARCHAR(300)
AS BEGIN
	UPDATE Negocio
		SET Nombre = @Nombre,
			Direccion = @Direccion,
			LogoUrl = @LogoUrl
		WHERE Id = @Id AND Estado = 1;
END
GO
-------------------------------------------
--   Procedimiento almacenado Rol    --
-------------------------------------------
CREATE PROCEDURE sp_ObtenerRoles
AS BEGIN
	SELECT * FROM Rol
	WHERE Estado = 1;
END
GO

CREATE PROCEDURE sp_ObtenerRolPorId
    @Id INT
AS
BEGIN
    SELECT Id, Nombre, Estado
    FROM Rol
    WHERE Id = @Id AND Estado = 1;

    SELECT PermisosId
    FROM RolPermisos
    WHERE RolId = @Id AND Estado = 1;
END
GO

CREATE TYPE IdListPermisos AS TABLE (
    Id INT
);
GO
CREATE PROCEDURE sp_RegistrarRol
    @Nombre NVARCHAR(100),
    @PermisosIds IdListPermisos READONLY, -- Usamos el tipo de tabla que creamos
    @NuevoRolId INT OUTPUT -- Parámetro de salida para devolver el ID del rol creado
AS
BEGIN
    BEGIN TRANSACTION;
    BEGIN TRY
        INSERT INTO Rol (Nombre)
        VALUES (@Nombre);

        SET @NuevoRolId = SCOPE_IDENTITY();

        INSERT INTO RolPermisos (RolId, PermisosId)
        SELECT @NuevoRolId, Id
        FROM @PermisosIds;

        -- Si todas las inserciones fueron exitosas, confirmamos la transacción.
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        -- Si ocurre algún error durante el proceso, deshacemos todos los cambios
        -- para evitar datos inconsistentes.
        IF @@TRANCOUNT > 0
            ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END
GO

CREATE PROCEDURE sp_EditarRol
    @RolId INT,
    @Nombre NVARCHAR(100),
    @PermisosIds IdListPermisos READONLY 
AS
BEGIN
    BEGIN TRANSACTION;
    BEGIN TRY
        -- Paso 1: Actualizar el nombre del Rol en la tabla principal
        UPDATE Rol
        SET Nombre = @Nombre
        WHERE Id = @RolId;

        -- Paso 2: Borrar TODOS los permisos anteriores de este rol.
        DELETE FROM RolPermisos
        WHERE RolId = @RolId;

        -- Paso 3: Insertar el nuevo conjunto de permisos.
        INSERT INTO RolPermisos (RolId, PermisosId)
        SELECT @RolId, Id
        FROM @PermisosIds;

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0
            ROLLBACK TRANSACTION;
        THROW; -- Relanza el error para que la capa de datos lo atrape
    END CATCH
END
GO

CREATE PROCEDURE sp_EliminarRol
    @Id INT
AS
BEGIN
    -- Variable para almacenar el resultado.
    -- 0 = Éxito, el rol fue desactivado.
    -- 1 = Error, el rol está en uso.
    DECLARE @Resultado INT;

    -- Comprobar si existe algún usuario ACTIVO que tenga asignado este rol.
    IF EXISTS (SELECT 1 FROM Usuario WHERE RolId = @Id AND Estado = 1)
    BEGIN
        -- Si existe al menos un usuario, el rol está en uso.
        -- Establecemos el resultado en 1 (Error).
        SET @Resultado = 1;

        -- Lanzamos un error personalizado que la capa de negocio puede atrapar.
        -- El mensaje es claro y puede mostrarse directamente al usuario.
        RAISERROR ('No se puede eliminar el rol porque está asignado a uno o más usuarios activos.', 16, 1);
        RETURN;
    END
    ELSE
    BEGIN
        -- Si no hay usuarios activos con este rol, procedemos a desactivarlo.
        UPDATE Rol
        SET Estado = 0
        WHERE Id = @Id;

        -- Establecemos el resultado en 0 (Éxito).
        SET @Resultado = 0;
    END
END
GO

-------------------------------------------
--Procedimiento almacenado Login/Usuario --
-------------------------------------------
CREATE PROCEDURE sp_ObtenerUsuarios
AS BEGIN
	SELECT U.Id, U.Nombre, U.Contra, U.RolId, R.Nombre AS NombreRol, U.NegocioId, N.Nombre AS NombreNegocio, U.Estado FROM Usuario AS U
	INNER JOIN Rol AS R ON U.RolId = R.Id
	INNER JOIN Negocio AS N ON U.NegocioId = N.Id
	WHERE U.Estado = 1;
END
GO

CREATE PROCEDURE sp_ObtenerUsuarioPorNombre
	@Nombre NVARCHAR(100)
AS BEGIN
	SELECT Id, Nombre, Contra, RolId, NegocioId, Estado FROM Usuario
	WHERE Nombre = @Nombre AND Estado = 1;
END
GO

CREATE PROCEDURE sp_ObtenerRolIdNombre
	@Nombre NVARCHAR(100)
AS BEGIN
	SELECT Id FROM Usuario
	WHERE Nombre = @Nombre AND Estado = 1;
END
GO

CREATE PROCEDURE sp_ObtenerUsuarioPorId
	@Id INT
AS BEGIN
	SELECT U.Id, U.Nombre, U.Contra, U.RolId, R.Nombre AS NombreRol, U.NegocioId, N.Nombre AS NombreNegocio, U.Estado FROM Usuario AS U
	INNER JOIN Rol AS R ON U.RolId = R.Id
	INNER JOIN Negocio AS N ON U.NegocioId = N.Id
	WHERE U.Id = @Id AND U.Estado = 1;
END
GO

CREATE PROCEDURE sp_RegistrarUsuario
	@Nombre NVARCHAR(100),
    @Contra NVARCHAR(300),
    @NegocioId INT,
    @RolId INT
AS
BEGIN
	INSERT INTO Usuario (Nombre, Contra, NegocioId, RolId)
	VALUES (@Nombre, @Contra, @NegocioId, @RolId);
END
GO

CREATE PROCEDURE sp_EditarUsuario
	@Id INT,
	@Nombre NVARCHAR(100),
    @Contra NVARCHAR(300),
    @NegocioId INT,
    @RolId INT
AS
BEGIN
	UPDATE Usuario
	SET Nombre = @Nombre,
		Contra = @Contra,
		NegocioId = @NegocioId,
		RolId = @RolId
	WHERE Id = @Id AND Estado = 1;
END
GO

CREATE PROCEDURE sp_EliminarUsuario
	@Id INT
AS
BEGIN
	UPDATE Usuario
	SET Estado = 0
	WHERE Id = @Id;
END
GO
-------------------------------------------
--Procedimiento almacenado Form/Permisos --
-------------------------------------------
CREATE PROCEDURE sp_ObtenerPermisos
AS
BEGIN
	SELECT Id, FormNombre, FormRuta, Estado FROM Permisos
	WHERE Estado = 1;
END
GO

CREATE PROCEDURE sp_ObtenerPermisoPorId
@Id INT
AS
BEGIN
	SELECT Id, FormNombre, FormRuta, Estado FROM Permisos
	WHERE Id = @Id AND Estado = 1;
END
GO

CREATE PROCEDURE sp_ObtenerPermisosIdPorNombre
	@FormRuta NVARCHAR(100)
AS BEGIN
	SELECT Id FROM Permisos
	WHERE FormRuta = @FormRuta;
END
GO

CREATE PROCEDURE sp_RegistrarPermisos
	@FormNombre NVARCHAR(100),
	@FormRuta NVARCHAR(100)
AS BEGIN
	INSERT INTO Permisos (FormNombre, FormRuta)
	VALUES (@FormNombre, @FormRuta);
END
GO

CREATE PROCEDURE sp_EditarPermisos
@Id INT,
@FormNombre NVARCHAR(100)
AS
BEGIN
		UPDATE Permisos
        SET FormNombre = @FormNombre
        WHERE Id = @Id AND Estado = 1;
END
GO

-------------------------------------------
-- Procedimiento almacenado RolPermisos  --
-------------------------------------------
CREATE PROCEDURE sp_ObtenerNumeroPermisos
	@RolId INT,
	@FormularioId INT
AS BEGIN
	SELECT * FROM RolPermisos
	WHERE RolId = @RolId AND PermisosId = @FormularioId AND Estado = 1
END
GO

CREATE PROCEDURE sp_ObtenerEsPermitidoForm
	@RolId INT,
	@FormId INT
AS BEGIN
	SELECT Estado FROM RolPermisos
	WHERE RolId = @RolId AND PermisosId = @FormId;
END
GO

CREATE PROCEDURE sp_ActualizarRolPermisos
	@IsAllowed BIT,
	@RolId INT,
	@FormId INT
AS 
BEGIN
	IF EXISTS(SELECT 1 FROM RolPermisos WHERE RolId = @RolId AND PermisosId = @FormId)
	BEGIN
		UPDATE RolPermisos
		SET Estado = @IsAllowed
		WHERE RolId = @RolId AND PermisosId = @FormId;
END
	ELSE
	BEGIN
		INSERT INTO RolPermisos (RolId, PermisosId, Estado)
		VALUES (@RolId, @FormId, @IsAllowed);
	END
END
GO

CREATE PROCEDURE sp_ObtenerRutasPermitidasPorRol
    @RolId INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT p.FormRuta
    FROM RolPermisos rp
    INNER JOIN Permisos p ON rp.PermisosId = p.Id
    WHERE rp.RolId = @RolId AND rp.Estado = 1 AND p.Estado = 1;
END
GO
----------------------------------------------------
--- Procedimiento almacenado ProductoCategoria   ---
----------------------------------------------------
CREATE PROCEDURE sp_ObtenerProductoCategorias
AS
BEGIN
	SELECT Id, Nombre, Estado
	FROM ProductoCategoria
	WHERE Estado = 1;
END
GO

CREATE PROCEDURE sp_ObtenerProductoCategoriasPorId
	@Id INT
AS
BEGIN
	SELECT Id, Nombre, Estado
	FROM ProductoCategoria
	WHERE Estado = 1 AND Id = @Id;
END
GO

CREATE PROCEDURE sp_RegistrarProductoCategoria
	@Nombre NVARCHAR(100)
AS
BEGIN
	INSERT INTO ProductoCategoria(Nombre)
	VALUES (@Nombre);
END
GO

CREATE PROCEDURE sp_EditarProductoCategoria
	@Id INT,
	@Nombre NVARCHAR(100)
AS
BEGIN
	UPDATE ProductoCategoria
	SET Nombre = @Nombre
	WHERE Id = @Id AND Estado = 1;
END
GO

CREATE PROCEDURE sp_EliminarProductoCategoria
	@Id INT
AS
BEGIN
	UPDATE ProductoCategoria
	SET Estado = 0
	WHERE Id = @Id AND Estado = 1;
END
GO
--------------------------------------------
--- Procedimiento almacenado Proveedor   ---
--------------------------------------------
CREATE PROCEDURE sp_ObtenerProveedores
AS
BEGIN
	SELECT Id, Nombre, Contacto, Estado FROM Proveedor
	WHERE Estado = 1;
END
GO

CREATE PROCEDURE sp_ObtenerProveedorPorId
	@Id INT
AS
BEGIN
	SELECT Id, Nombre, Contacto, Estado FROM Proveedor
	WHERE Estado = 1 AND Id = @Id;
END
GO

CREATE PROCEDURE sp_RegistrarProveedor
	@Nombre NVARCHAR(100),
    @Contacto NVARCHAR(100)
AS
BEGIN
	INSERT INTO Proveedor(Nombre, Contacto)
	VALUES (@Nombre, @Contacto);
END
GO

CREATE PROCEDURE sp_EditarProveedor
	@Id INT,
	@Nombre NVARCHAR(100),
    @Contacto NVARCHAR(100)
AS 
BEGIN
	UPDATE Proveedor
	SET Nombre = @Nombre,
		Contacto = @Contacto
	WHERE Id = @Id AND Estado = 1;
END
GO

CREATE PROCEDURE sp_EliminarProveedor
	@Id INT
AS
BEGIN
	UPDATE Proveedor
	SET Estado = 0
	WHERE Id = @Id AND Estado = 1;
END
GO
--------------------------------------------
--- Procedimiento almacenado Producto   ----
--------------------------------------------
--CREATE PROCEDURE sp_ObtenerProductos
--AS
--BEGIN
--    SELECT
--        P.Id,
--        P.Nombre,
--        P.Precio,
--        P.ProductoCategoriaId,
--        PC.Nombre AS NombreCategoria, 
--        P.FotoUrl
--    FROM Producto P
--    INNER JOIN ProductoCategoria PC ON P.ProductoCategoriaId = PC.Id
--    WHERE P.Estado = 1;
--END
--GO
CREATE PROCEDURE sp_ObtenerProductos
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        P.Id,
        P.Nombre,
        P.Precio,
        P.ProductoCategoriaId,
        PC.Nombre AS NombreCategoria,
        P.FotoUrl,

        -- ===== INICIO: CÁLCULO DE STOCK DISPONIBLE =====
        ISNULL(
            (
                SELECT MIN(
                    CASE
                        WHEN PI.Cantidad > 0 THEN FLOOR(I.Stock / PI.Cantidad)
                        ELSE 0
                    END
                )
                FROM ProductoInsumo PI
                INNER JOIN Insumo I ON PI.InsumoId = I.Id
                WHERE PI.ProductoId = P.Id
                  AND PI.Estado = 1
                  AND I.Estado = 1
            ),
            0
        ) AS Stock
        -- ===== FIN: CÁLCULO DE STOCK DISPONIBLE =====

    FROM Producto P
    INNER JOIN ProductoCategoria PC ON P.ProductoCategoriaId = PC.Id
    WHERE P.Estado = 1;
END
GO


CREATE PROCEDURE sp_ObtenerProductoPorId
@Id INT
AS
BEGIN
	SELECT Id, Nombre, Precio, ProductoCategoriaId, FotoUrl FROM Producto
	WHERE Id = @Id AND Estado = 1;
END
GO

CREATE TYPE ProductoInsumoType AS TABLE (
    InsumoId INT,
    Cantidad DECIMAL(10,2),
    Tipo NVARCHAR(20)
);
GO

CREATE PROCEDURE sp_RegistrarProducto (
    -- Parámetros para la tabla Producto
    @Nombre NVARCHAR(150),
    @Precio DECIMAL(10,2),
    @ProductoCategoriaId INT,
    @FotoUrl NVARCHAR(300),
    -- Parámetro para la lista de insumos (la receta)
    @Insumos dbo.ProductoInsumoType READONLY
)
AS
BEGIN
    BEGIN TRANSACTION;
    BEGIN TRY
        -- Declarar una variable para guardar el ID del nuevo producto
        DECLARE @NuevoProductoId INT;
        INSERT INTO dbo.Producto (Nombre, Precio, ProductoCategoriaId, FotoUrl)
        VALUES (@Nombre, @Precio, @ProductoCategoriaId, @FotoUrl);

        SET @NuevoProductoId = SCOPE_IDENTITY();
        -- Insertar todos los insumos de la receta en la tabla 'ProductoInsumo'
        -- Se utiliza el ID del nuevo producto y se leen los datos del parámetro @Insumos
        INSERT INTO ProductoInsumo (ProductoId, InsumoId, Cantidad, Tipo)
        SELECT
            @NuevoProductoId, -- El ID que acabamos de obtener
            InsumoId,
            Cantidad,
            Tipo
        FROM @Insumos; -- Se lee directamente del parámetro como si fuera una tabla
        -- Si todo salió bien, confirmamos la transacción
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        -- Si ocurre cualquier error, deshacemos todos los cambios
        ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END
GO

CREATE PROCEDURE sp_EditarProductos
-- Parámetros para la tabla Producto
    @Id INT, -- ID del producto a editar
    @Nombre NVARCHAR(150),
    @Precio DECIMAL(10,2),
    @ProductoCategoriaId INT,
    @FotoUrl NVARCHAR(300),
    -- Parámetro para la nueva lista de insumos (la receta actualizada)
    @Insumos ProductoInsumoType READONLY
AS
BEGIN
    BEGIN TRANSACTION;
    BEGIN TRY
        -- Actualizar la tabla principal 'Producto'
        UPDATE Producto
        SET
            Nombre = @Nombre,
            Precio = @Precio,
            ProductoCategoriaId = @ProductoCategoriaId,
            FotoUrl = @FotoUrl
        WHERE Id = @Id;

        -- Borrar todos los insumos anteriores asociados a este producto
        DELETE FROM ProductoInsumo
        WHERE ProductoId = @Id;

        -- Insertar la nueva lista de insumos de la receta
        INSERT INTO ProductoInsumo (ProductoId, InsumoId, Cantidad, Tipo)
        SELECT
            @Id, -- El ID del producto que estamos editando
            InsumoId,
            Cantidad,
            Tipo
        FROM @Insumos; -- Se lee directamente del parámetro

        -- Si todo salió bien, confirmamos la transacción
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        -- Si ocurre cualquier error, deshacemos todos los cambios
        ROLLBACK TRANSACTION;
        -- Re-lanzamos el error para que la aplicación lo pueda capturar
        THROW;
    END CATCH
END
GO

CREATE PROCEDURE sp_EliminarProducto
    @Id INT
AS
BEGIN
    SET NOCOUNT ON;
    UPDATE Producto
		SET Estado = 0
		WHERE Id = @Id;
END
GO
--------------------------------------------
--- Procedimiento almacenado Cliente    ----
--------------------------------------------
CREATE PROCEDURE sp_ObtenerClientes
AS
BEGIN
	SELECT Id, Nombre, EsComerciante, NumeroLocal, Pasillo
	FROM Cliente
	WHERE Estado = 1;
END
GO

CREATE PROCEDURE sp_ObtenerClientesComerciantes
AS
BEGIN
	SELECT Id, Nombre, EsComerciante, NumeroLocal, Pasillo
	FROM Cliente
	WHERE  Estado = 1;
END
GO

CREATE PROCEDURE sp_RegistrarClientes
	@Nombre NVARCHAR(150),
    @NumeroLocal NVARCHAR(20),
    @Pasillo NVARCHAR(50),
	@EsComerciante BIT
AS
BEGIN
	INSERT INTO Cliente(Nombre, NumeroLocal, Pasillo, EsComerciante)
	VALUES (@Nombre, @NumeroLocal, @Pasillo, @EsComerciante);
END
GO

CREATE PROCEDURE sp_ObtenerClientePorId
	@Id INT
AS
BEGIN
	SELECT Id, Nombre, NumeroLocal, Pasillo, EsComerciante
	FROM Cliente
	WHERE Estado = 1 AND Id = @Id;
END
GO

CREATE PROCEDURE sp_EditarCliente
	@Id INT,
	@Nombre NVARCHAR(150),
    @NumeroLocal NVARCHAR(20),
    @Pasillo NVARCHAR(50)
AS
BEGIN
	UPDATE Cliente
	SET Nombre = @Nombre,
		NumeroLocal = @NumeroLocal,
		Pasillo = @Pasillo
	WHERE Estado = 1 AND Id = @Id;
END
GO

CREATE PROCEDURE sp_ElimiarCliente
	@Id INT
AS
BEGIN
	UPDATE Cliente
		SET Estado = 0
		WHERE Id = @Id and Estado = 1;
END
GO

---------------------------------------------
--- Procedimiento almacenado MetodoPago  ----
---------------------------------------------
CREATE PROCEDURE sp_ObtenerMetodosPagoVenta
AS
BEGIN
    SELECT Id, Nombre
    FROM MetodoPago
    WHERE Estado = 1;
END
GO

CREATE PROCEDURE sp_ObtenerMetodoPagoPorId
	@Id INT
AS
BEGIN
	SELECT Id, Nombre, Estado
	FROM MetodoPago
	WHERE Estado = 1 AND Id = @Id;
END
GO

CREATE PROCEDURE sp_RegistrarMetodoPago
	@Nombre NVARCHAR(100)
AS
BEGIN
	INSERT INTO MetodoPago (Nombre)
	VALUES (@Nombre);
END
GO

CREATE PROCEDURE sp_EditarMetodoPago
	@Id INT,
	@Nombre NVARCHAR(100)
AS
BEGIN
	UPDATE MetodoPago
	SET Nombre = @Nombre
	WHERE Estado = 1 AND Id = @Id;
END
GO

CREATE PROCEDURE sp_EliminarMetodoPago
	@Id INT
AS
BEGIN
	UPDATE MetodoPago
	SET Estado = 0
	WHERE Id = @Id and Estado = 1;
END
GO
-----------------------------------------
--- Procedimiento almacenado Ventas   ---
-----------------------------------------
CREATE PROCEDURE sp_ObtenerVentas
AS
BEGIN
    SELECT
        V.Id AS VentaId,
        V.Fecha,
        V.Total,
        CASE 
            WHEN V.EnLocal = 1 THEN 'En Local' 
            WHEN V.EnLocal = 0 THEN 'Para Llevar' 
            ELSE 'Sin Tipo' 
        END AS TipoVenta,
        ISNULL(C.Nombre, 'Cliente normal') AS Cliente,
		V.MontoRecibido,
		v.CambioDevuelto,
        U.Nombre AS Vendedor,
        MP.Nombre AS MetodoPago,
        V.Estado
    FROM Venta V
    LEFT JOIN Cliente C ON V.ClienteId = C.Id
    INNER JOIN Usuario U ON V.UsuarioId = U.Id
    INNER JOIN MetodoPago MP ON V.MetodoPagoId = MP.Id
    WHERE V.Estado = 1
    ORDER BY V.Fecha DESC;
END
GO

CREATE PROCEDURE sp_ObtenerVentaId
@Id INT
AS
BEGIN
    SET NOCOUNT ON; -- Evita que se devuelvan mensajes de conteo de filas
SELECT
    V.Id AS VentaId,
    V.Fecha,
    V.Total,
    V.EnLocal,
    ISNULL(C.Id, 0) AS ClienteId,
	C.Nombre AS Cliente,
	V.MontoRecibido,
	V.CambioDevuelto,
    U.Id AS VendedorId,
	U.Nombre AS Vendedor,
    MP.Id AS MetodoPagoId,
	MP.Nombre AS MetodoPago,
    V.Estado
FROM Venta V
LEFT JOIN Cliente C ON V.ClienteId = C.Id
INNER JOIN Usuario U ON V.UsuarioId = U.Id
INNER JOIN MetodoPago MP ON V.MetodoPagoId = MP.Id
WHERE V.Id = @Id
  AND V.Estado = 1
ORDER BY V.Fecha DESC;
END
GO

CREATE PROCEDURE sp_ObtenerDetallesVenta
    @VentaId INT
AS
BEGIN
    SELECT
        V.Id AS VentaId,
        V.Fecha,
        CASE 
            WHEN V.EnLocal = 1 THEN 'En Local' 
            WHEN V.EnLocal = 0 THEN 'Para Llevar' 
            ELSE 'Sin Tipo' 
        END AS TipoVenta,
        ISNULL(C.Nombre, 'Sin Cliente') AS ClienteNombre,
        U.Nombre AS UsuarioNombre,
        MP.Nombre AS MetodoPagoNombre,
		P.Id AS ProductoId,
        P.Nombre AS ProductoNombre,
        P.Precio AS PrecioUnitario,
        DV.Cantidad,
        DV.SubTotal,
		DV.Id
    FROM DetalleVenta DV
    INNER JOIN Producto P ON DV.ProductoId = P.Id
    INNER JOIN Venta V ON DV.VentaId = V.Id
    LEFT JOIN Cliente C ON V.ClienteId = C.Id
    INNER JOIN Usuario U ON V.UsuarioId = U.Id
    INNER JOIN MetodoPago MP ON V.MetodoPagoId = MP.Id
    WHERE DV.VentaId = @VentaId AND DV.Estado = 1;
END
GO

CREATE TYPE dbo.DetalleVentaType AS TABLE (
    ProductoId INT,
    Cantidad INT,
    SubTotal DECIMAL(10,2)
);
GO

CREATE PROCEDURE sp_RegistrarVenta
    @EnLocal BIT,
    @ClienteId INT = NULL,
    @UsuarioId INT,
    @MetodoPagoId INT,
    @MontoRecibido DECIMAL(10,2),
    @Detalles dbo.DetalleVentaType READONLY
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @Total DECIMAL(10,2) = (SELECT SUM(SubTotal) FROM @Detalles);
    DECLARE @CambioDevuelto DECIMAL(10,2) = @MontoRecibido - @Total;
    DECLARE @NuevaVentaId INT;

    BEGIN TRANSACTION;
    BEGIN TRY
        -- Insertar cabecera de la venta (sin cambios)
        INSERT INTO Venta (Fecha, Total, EnLocal, ClienteId, UsuarioId, MetodoPagoId, MontoRecibido, CambioDevuelto)
        VALUES (GETDATE(), @Total, @EnLocal, @ClienteId, @UsuarioId, @MetodoPagoId, @MontoRecibido, @CambioDevuelto);

        SET @NuevaVentaId = SCOPE_IDENTITY();

        -- Insertar detalles de la venta (sin cambios)
        INSERT INTO DetalleVenta (VentaId, ProductoId, Cantidad, SubTotal)
        SELECT @NuevaVentaId, ProductoId, Cantidad, SubTotal
        FROM @Detalles;

        -- Registrar el movimiento de inventario
        INSERT INTO MovimientoInventario (InsumoId, Fecha, TipoMovimiento, Cantidad, Observacion, UsuarioId)
        SELECT 
            pi.InsumoId, 
            GETDATE(), 
            'Salida', 
            -- Aseguramos que la multiplicación se trate como DECIMAL
            CAST((pi.Cantidad * dv.Cantidad) AS DECIMAL(10,2)), 
            CONCAT('Nº Venta: ', @NuevaVentaId), 
            @UsuarioId
        FROM @Detalles dv
        INNER JOIN ProductoInsumo pi ON dv.ProductoId = pi.ProductoId
        WHERE pi.Estado = 1;

        -- Actualizar stock de insumos
        UPDATE I
        SET I.Stock = I.Stock - Mov.CantidadTotalDescontada
        FROM Insumo I
        INNER JOIN (
            SELECT
                pi.InsumoId,
                -- Aseguramos que la suma de las multiplicaciones sea DECIMAL
                SUM(CAST((pi.Cantidad * dv.Cantidad) AS DECIMAL(10,2))) AS CantidadTotalDescontada
            FROM @Detalles dv
            INNER JOIN ProductoInsumo pi ON dv.ProductoId = pi.ProductoId
            WHERE pi.Estado = 1
            GROUP BY pi.InsumoId
        ) AS Mov ON I.Id = Mov.InsumoId;

        COMMIT;
    END TRY
    BEGIN CATCH
        ROLLBACK;
        THROW;
    END CATCH
END
GO

-----------------------------------------
-- PROCEDIMIENTO PARA EDITAR VENTA ------
-----------------------------------------
CREATE TYPE dbo.DetalleVentaTipo AS TABLE (
    ProductoId INT,
    Cantidad INT,
    SubTotal DECIMAL(10,2)
);
GO

CREATE PROCEDURE sp_EditarVenta
    @VentaId INT,
    @Detalles DetalleVentaTipo READONLY,
    @UsuarioEditaId INT,
    @ClienteId INT,           
    @MetodoPagoId INT,       
    @EnLocal BIT,            
    @Total DECIMAL(10,2),
	@MontoRecibido DECIMAL(10,2)
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE @CambioDevuelto DECIMAL(10,2) = @MontoRecibido - @Total;
    DECLARE @FechaActual DATETIME = GETDATE();

    BEGIN TRY
        BEGIN TRAN;

        -- 1. Validar si la venta existe y está activa
        IF NOT EXISTS (SELECT 1 FROM Venta WHERE Id = @VentaId AND Estado = 1)
        BEGIN
            RAISERROR('La venta no existe o ya fue anulada.', 16, 1);
            ROLLBACK;
            RETURN;
        END

        -- 2. Actualizar los datos principales de la venta
        UPDATE Venta 
        SET ClienteId = @ClienteId,
            MetodoPagoId = @MetodoPagoId,
            EnLocal = @EnLocal,
            Total = @Total,
            Fecha = @FechaActual,
			MontoRecibido = @MontoRecibido,
			CambioDevuelto = @CambioDevuelto
        WHERE Id = @VentaId;

        -- 3. Anular los detalles anteriores
        UPDATE DetalleVenta SET Estado = 0 WHERE VentaId = @VentaId;

        -- 4. Revertir el inventario sumando los insumos usados antes
        UPDATE I
        SET I.Stock = I.Stock + R.Cantidad
        FROM Insumo I
        INNER JOIN (
            SELECT PI.InsumoId, SUM(DV.Cantidad * PI.Cantidad) AS Cantidad
            FROM DetalleVenta DV
            INNER JOIN ProductoInsumo PI ON PI.ProductoId = DV.ProductoId
            WHERE DV.VentaId = @VentaId AND DV.Estado = 1 AND PI.Estado = 1
            GROUP BY PI.InsumoId
        ) AS R ON I.Id = R.InsumoId;

        -- 5. Marcar movimientos anteriores como anulados
        UPDATE MovimientoInventario
        SET Estado = 0, UsuarioId = @UsuarioEditaId
        WHERE Observacion LIKE CONCAT('%venta #', @VentaId, '%') AND TipoMovimiento = 'Salida' AND Estado = 1;

        -- 6. Insertar nuevos detalles
        INSERT INTO DetalleVenta (VentaId, ProductoId, Cantidad, SubTotal, Estado)
        SELECT @VentaId, ProductoId, Cantidad, SubTotal, 1
        FROM @Detalles;

        -- 7. Descontar los nuevos insumos del inventario
        UPDATE I
        SET I.Stock = I.Stock - D.Cantidad
        FROM Insumo I
        INNER JOIN (
            SELECT PI.InsumoId, SUM(DV.Cantidad * PI.Cantidad) AS Cantidad
            FROM @Detalles DV
            INNER JOIN ProductoInsumo PI ON PI.ProductoId = DV.ProductoId AND PI.Estado = 1
            GROUP BY PI.InsumoId
        ) AS D ON I.Id = D.InsumoId;

        -- 8. Registrar nuevos movimientos de salida
        INSERT INTO MovimientoInventario (Fecha, TipoMovimiento, InsumoId, Cantidad, Observacion, UsuarioId, Estado)
        SELECT
            @FechaActual,
            'Salida',
            PI.InsumoId,
            DV.Cantidad * PI.Cantidad,
            CONCAT('Salida por edición de venta #', @VentaId),
            @UsuarioEditaId,
            1
        FROM @Detalles DV
        INNER JOIN ProductoInsumo PI ON PI.ProductoId = DV.ProductoId AND PI.Estado = 1;

        COMMIT TRAN;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0
            ROLLBACK TRAN;
        DECLARE @Err NVARCHAR(4000) = ERROR_MESSAGE();
        RAISERROR(@Err, 16, 1);
    END CATCH
END
GO

-----------------------------------------
-- PROCEDIMIENTO PARA ELIMINAR VENTA  ---
-----------------------------------------
CREATE PROCEDURE sp_EliminarVenta
    @VentaId INT
AS
BEGIN
    --SET NOCOUNT ON;

    BEGIN TRY
        BEGIN TRAN;

        -- 1. Verificar si la venta ya está anulada
        IF EXISTS (SELECT 1 FROM Venta WHERE Id = @VentaId AND Estado = 0)
        BEGIN
            RAISERROR('La venta ya está anulada.', 16, 1);
            ROLLBACK;
            RETURN;
        END

        -- 2. Marcar Venta y DetalleVenta como anulados (Estado = 0)
        UPDATE Venta SET Estado = 0 WHERE Id = @VentaId;
        UPDATE DetalleVenta SET Estado = 0 WHERE VentaId = @VentaId;

        -- 3. Revertir el movimiento de inventario (sumar insumos de nuevo)
        INSERT INTO MovimientoInventario (Fecha, TipoMovimiento, InsumoId, Cantidad, Observacion, UsuarioId)
        SELECT 
            GETDATE(),
            'Anulación',
            PI.InsumoId,
            DV.Cantidad * PI.Cantidad,
            CONCAT('Reversión por anulación de venta #', V.Id),
            V.UsuarioId
        FROM DetalleVenta DV
        INNER JOIN Venta V ON V.Id = DV.VentaId
        INNER JOIN ProductoInsumo PI ON PI.ProductoId = DV.ProductoId
        WHERE DV.VentaId = @VentaId
          AND PI.Estado = 1;

        -- 4. Sumar nuevamente al stock los insumos revertidos
        UPDATE I
        SET I.Stock = I.Stock + Reverso.Cantidad
        FROM Insumo I
        INNER JOIN (
            SELECT 
                PI.InsumoId,
                SUM(DV.Cantidad * PI.Cantidad) AS Cantidad
            FROM DetalleVenta DV
            INNER JOIN ProductoInsumo PI ON PI.ProductoId = DV.ProductoId
            WHERE DV.VentaId = @VentaId AND PI.Estado = 1
            GROUP BY PI.InsumoId
        ) AS Reverso ON I.Id = Reverso.InsumoId;

        -- 5. (Opcional) Marcar los movimientos anteriores como anulados
        UPDATE MovimientoInventario
        SET Estado = 0
        WHERE Observacion LIKE CONCAT('%venta #', @VentaId, '%') AND TipoMovimiento = 'Salida';

        COMMIT;
    END TRY
    BEGIN CATCH
        ROLLBACK;

        DECLARE @ErrorMsg NVARCHAR(4000) = ERROR_MESSAGE();
        RAISERROR(@ErrorMsg, 16, 1);
    END CATCH
END
GO

-----------------------------------------
-- PROCEDIMIENTO PARA  COMPRA        ----
-----------------------------------------
CREATE PROCEDURE sp_ObtenerCompras
AS
BEGIN
	SELECT C.Id,
		   C.Fecha, 
		   C.Total,
		   C.UsuarioId,
		   U.Nombre AS NombreUsuario,
		   C.ProveedorId,
		   P.Nombre AS NombreProveedor,
		   C.Estado
	FROM Compra AS C
	INNER JOIN Usuario AS U ON UsuarioId = U.Id
	INNER JOIN Proveedor AS P ON ProveedorId = P.Id
	WHERE C.Estado = 1;
END
GO

CREATE PROCEDURE sp_ObtenerCompraPorId
    @Id INT
AS
BEGIN
    -- 1. Devolver los datos de la cabecera de la compra
    -- Se une con Usuario y Proveedor para obtener sus nombres.
    SELECT 
        C.Id,
        C.Fecha,
        C.Total,
        C.UsuarioId,
        U.Nombre AS NombreUsuario,
        C.ProveedorId,
        P.Nombre AS NombreProveedor,
        C.Estado
    FROM Compra AS C
    INNER JOIN Usuario AS U ON C.UsuarioId = U.Id
    INNER JOIN Proveedor AS P ON C.ProveedorId = P.Id
    WHERE C.Id = @Id AND C.Estado = 1;

    -- 2. Devolver la lista de detalles de esa misma compra
    -- Se une con Insumo para obtener el nombre de cada producto comprado.
    SELECT 
        DC.Id,
        DC.CompraId,
        DC.InsumoId,
        I.Nombre AS NombreInsumo,
        DC.Cantidad,
        DC.Costo
    FROM DetalleCompra AS DC
    INNER JOIN Insumo AS I ON DC.InsumoId = I.Id
    WHERE DC.CompraId = @Id AND DC.Estado = 1;
END
GO

CREATE TYPE DetalleCompraType AS TABLE (
    InsumoId INT,
    Cantidad DECIMAL(10,2),
    Costo DECIMAL(10,2)
);
GO
CREATE PROCEDURE sp_RegistrarCompra
    @UsuarioId INT,
    @ProveedorId INT,
    @Detalles DetalleCompraType READONLY 
AS
BEGIN
    DECLARE @TotalCompra DECIMAL(10,2);
    DECLARE @NuevaCompraId INT;
    BEGIN TRANSACTION;
    BEGIN TRY

        -- Calculamos el total aquí, multiplicando Cantidad * Costo
        SET @TotalCompra = (SELECT SUM(Cantidad * Costo) FROM @Detalles);

        INSERT INTO Compra (Total, UsuarioId, ProveedorId)
        VALUES (@TotalCompra, @UsuarioId, @ProveedorId);
        SET @NuevaCompraId = SCOPE_IDENTITY();


        -- Insertamos en DetalleCompra solo las columnas que existen en tu tabla.
        INSERT INTO DetalleCompra (CompraId, InsumoId, Cantidad, Costo)
        SELECT 
            @NuevaCompraId,
            InsumoId,
            Cantidad,
            Costo
        FROM @Detalles;

        -- Actualizar el stock de cada insumo (esta parte sigue igual)
        UPDATE Insumo
        SET 
            Stock = Stock + d.Cantidad,
            Costo = d.Costo -- Actualizamos el costo del insumo al más reciente
        FROM Insumo i
        INNER JOIN @Detalles d ON i.Id = d.InsumoId;

        -- Registrar el movimiento de inventario (esta parte sigue igual)
        INSERT INTO MovimientoInventario (InsumoId, Fecha, TipoMovimiento, Cantidad, Observacion, UsuarioId)
        SELECT 
            InsumoId,
            GETDATE(),
            'Entrada',
            Cantidad,
            CONCAT('Nº Compra: ', @NuevaCompraId),
            @UsuarioId
        FROM @Detalles;


        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0

            ROLLBACK TRANSACTION;
        THROW; 
    END CATCH
END
GO

-----------------------------------------------
-- PROCEDIMIENTO PARA MOVIMIENTO INVENTARIO  --
-----------------------------------------------
CREATE PROCEDURE sp_ObtenerMovimientoInventario
AS
BEGIN
    SELECT 
        MI.Id,
        MI.InsumoId,
        I.Nombre AS NombreInsumo,
        MI.Fecha,
        MI.TipoMovimiento,
        MI.Cantidad,
        MI.Observacion,
        MI.UsuarioId,
        U.Nombre AS NombreUsuario,
        MI.Estado
    FROM MovimientoInventario AS MI
    INNER JOIN Insumo AS I ON MI.InsumoId = I.Id
    INNER JOIN Usuario AS U ON MI.UsuarioId = U.Id
    WHERE MI.Estado = 1;
END
GO
-----------------------------------------
-- PROCEDIMIENTO PARA UNIDADES MEDIDA  --
-----------------------------------------
CREATE PROCEDURE sp_ObtenerUnidadesMedida
AS
BEGIN
	SELECT Id, Nombre, Abreviatura
	FROM UnidadesMedida
	WHERE Estado = 1;
END
GO

CREATE PROCEDURE sp_ObtenerUnidadMedidaPorId
	@Id INT
AS
BEGIN
	SELECT Id, Nombre, Abreviatura, Estado
	FROM UnidadesMedida
	WHERE Estado = 1 AND Id = @Id;
END
GO

CREATE PROCEDURE sp_RegistrarUnidadMedida
	@Nombre NVARCHAR(50),
    @Abreviatura NVARCHAR(50)
AS
BEGIN
	INSERT INTO UnidadesMedida (Nombre, Abreviatura)
	VALUES (@Nombre, @Abreviatura);
END
GO

CREATE PROCEDURE sp_EditarUnidadMedida
	@Id INT,
	@Nombre NVARCHAR(50),
    @Abreviatura NVARCHAR(50)
AS
BEGIN
	UPDATE UnidadesMedida
	SET Nombre = @Nombre,
		Abreviatura = @Abreviatura
	WHERE Id = @Id AND Estado = 1;
END
GO

CREATE PROCEDURE sp_EliminarUnidadMedida
	@Id INT
AS
BEGIN
	UPDATE UnidadesMedida
	SET Estado = 0
	WHERE Id = @Id AND Estado = 1;
END
GO
------------------------------------------
-- PROCEDIMIENTO PARA INSUMO CATEGORIA  --
------------------------------------------
CREATE PROCEDURE sp_ObtenerInsumoCategoria
AS
BEGIN
	SELECT Id, Nombre, Estado
	FROM InsumoCategoria
	WHERE Estado = 1;
END
GO

CREATE PROCEDURE sp_ObtenerInsumoCategoriaPorId
	@Id INT
AS
BEGIN
	SELECT Id, Nombre, Estado
	FROM InsumoCategoria
	WHERE Estado = 1 AND Id = @Id;
END
GO

CREATE PROCEDURE sp_RegistrarInsumoCategoria
	@Nombre NVARCHAR(100)
AS
BEGIN
	INSERT INTO InsumoCategoria (Nombre)
	VALUES (@Nombre);
END
GO

CREATE PROCEDURE sp_EditarInsumoCategoria
	@Id INT,
	@Nombre NVARCHAR(100)
AS
BEGIN
	UPDATE InsumoCategoria
	SET Nombre = @Nombre
	WHERE Estado = 1 AND Id = @Id;
END
GO

CREATE PROCEDURE sp_EliminarInsumoCategoria
	@Id INT
AS
BEGIN
	UPDATE InsumoCategoria
	SET Estado = 0
	WHERE Estado = 1 AND Id = @Id;
END
GO
-----------------------------------------
-- PROCEDIMIENTO PARA INSuMOS          --
-----------------------------------------
CREATE PROCEDURE sp_ObtenerInsumos
AS
BEGIN
    SET NOCOUNT ON;
    SELECT
        Ins.Id,
        Ins.Nombre,
		Ins.Costo,
        Ins.Stock,
        Ins.StockMinimo,
        Ins.InsumoCategoriaId,
        Ic.Nombre AS NombreCategoria,
        Ins.ProveedorId,
        P.Nombre AS NombreProveedor,
        Ins.FotoUrl,
        Ins.UnidadesMedidaId,
        UM.Nombre AS NombreMedidas
    FROM Insumo AS Ins
    INNER JOIN InsumoCategoria AS Ic ON Ins.InsumoCategoriaId = Ic.Id
    INNER JOIN Proveedor AS P ON Ins.ProveedorId = P.Id
    INNER JOIN UnidadesMedida AS UM ON Ins.UnidadesMedidaId = UM.Id
    WHERE Ins.Estado = 1;
END
GO

CREATE PROCEDURE sp_ObtenerProductoInsumoPorId --LO USO EN EL CREATE Y EDIT PARA LA TABLA PRODUCTOS
    @ProductoId INT 
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        pi.InsumoId,
        i.Nombre AS InsumoNombre,
        pi.Cantidad,
        pi.Tipo
    FROM dbo.ProductoInsumo AS pi
    INNER JOIN Insumo AS i ON pi.InsumoId = i.Id 
    WHERE pi.ProductoId = @ProductoId;
END
GO

CREATE PROCEDURE sp_ObtenerInsumoPorId
	@Id INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT
        Ins.Id,
        Ins.Nombre,
        Ins.Stock,
		Ins.Costo,
        Ins.StockMinimo,
        Ins.InsumoCategoriaId,
        Ic.Nombre AS NombreCategoria,
        Ins.ProveedorId,
        P.Nombre AS NombreProveedor,
        Ins.FotoUrl,
        Ins.UnidadesMedidaId,
        UM.Nombre AS NombreMedidas
    FROM Insumo AS Ins
    INNER JOIN InsumoCategoria AS Ic ON Ins.InsumoCategoriaId = Ic.Id
    INNER JOIN Proveedor AS P ON Ins.ProveedorId = P.Id
    INNER JOIN UnidadesMedida AS UM ON Ins.UnidadesMedidaId = UM.Id
    WHERE Ins.Estado = 1 AND Ins.Id = @Id;
END
GO

CREATE PROCEDURE sp_RegistrarInsumo
	@Nombre NVARCHAR(150),
    @Costo DECIMAL(10,2),
    @Stock DECIMAL(10,2),
	@StockMinimo DECIMAL(10,2),
    @InsumoCategoriaId INT,
    @ProveedorId INT,
    @FotoUrl NVARCHAR(300),
    @UnidadesMedidaId INT
AS
BEGIN
	INSERT INTO Insumo (Nombre, Costo, Stock, StockMinimo, InsumoCategoriaId, ProveedorId, FotoUrl, UnidadesMedidaId)
	VALUES (@Nombre, @Costo, @Stock, @StockMinimo, @InsumoCategoriaId, @ProveedorId, @FotoUrl, @UnidadesMedidaId);
END
GO

CREATE PROCEDURE sp_EditarInsumo
	@ID INT,
	@Nombre NVARCHAR(150),
    @Costo DECIMAL(10,2),
    @Stock DECIMAL(10,2),
	@StockMinimo DECIMAL(10,2),
    @InsumoCategoriaId INT,
    @ProveedorId INT,
    @FotoUrl NVARCHAR(300),
    @UnidadesMedidaId INT
AS
BEGIN
	UPDATE Insumo
	SET Nombre = @Nombre,
		Costo = @Costo,
		Stock = @Stock,
		StockMinimo = @StockMinimo,
		InsumoCategoriaId = @InsumoCategoriaId,
		ProveedorId = @ProveedorId,
		FotoUrl = @FotoUrl,
		UnidadesMedidaId = @UnidadesMedidaId
	WHERE Estado = 1 AND Id = @ID;
END
GO

CREATE PROCEDURE sp_EliminarInsumo
	@Id INT
AS
BEGIN
	UPDATE Insumo
	SET Estado = 0
	WHERE Estado = 1 AND Id = @Id;
END
GO
--------------------------------------
---             DASBOARD           ---
--------------------------------------
CREATE PROCEDURE sp_Dashboard_VentasTotalesMes
AS
BEGIN
    SET NOCOUNT ON;

    -- Esta consulta suma la columna MontoTotal y devuelve UN ÚNICO VALOR
    SELECT 
        ISNULL(SUM(Total), 0) AS Total
    FROM 
        Venta
    WHERE 
        -- Filtra por el mes y año actual
        MONTH(Fecha) = MONTH(GETDATE()) AND 
        YEAR(Fecha) = YEAR(GETDATE()) AND
        -- Y solo considera las ventas con Estado = 1 (activas)
        Estado = 1;
END
GO

CREATE PROCEDURE sp_Dashboard_ProductosVendidosMes
AS
BEGIN
    SELECT ISNULL(SUM(dv.Cantidad), 0) AS Total
    FROM DetalleVenta dv
    INNER JOIN Venta v ON dv.VentaId = v.Id
    WHERE MONTH(v.Fecha) = MONTH(GETDATE()) AND 
		  YEAR(v.Fecha) = YEAR(GETDATE()) AND 
		  V.Estado = 1;
END
GO

CREATE PROCEDURE sp_Dashboard_TotalItemsInventario
AS
BEGIN
    -- Contamos los insumos que están activos
    SELECT COUNT(*) AS Total FROM Insumo WHERE Estado = 1;
END
GO

-- Resumen de Ventas de los Últimos 7 Días
CREATE PROCEDURE sp_Dashboard_VentasSemana
AS
BEGIN
    SELECT
        CONVERT(CHAR(10), Fecha, 120) AS Dia,
        SUM(Total) AS Total
    FROM Venta
    WHERE Fecha >= DATEADD(DAY, -7, GETDATE()) AND Estado = 1
    GROUP BY CONVERT(CHAR(10), Fecha, 120)
    ORDER BY Dia;
END
GO

-- Top 5 Productos Más Vendidos (por cantidad)
CREATE PROCEDURE sp_Dashboard_TopProductosVendidos
AS
BEGIN
    SELECT TOP 5
        p.Nombre AS Producto,
        SUM(dv.Cantidad) AS CantidadVendida
    FROM DetalleVenta dv
    INNER JOIN Producto p ON dv.ProductoId = p.Id
    GROUP BY p.Nombre
    ORDER BY CantidadVendida DESC;
END
GO