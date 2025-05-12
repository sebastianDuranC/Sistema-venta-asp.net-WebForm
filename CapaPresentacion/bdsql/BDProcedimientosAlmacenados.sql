USE BDElFogon;

---- Procedimiento almacenado Negocio ----
CREATE PROCEDURE sp_Negocio
    @Id INT = NULL,
    @Nombre NVARCHAR(150) = NULL,
    @Direccion NVARCHAR(250) = NULL,
    @LogoUrl NVARCHAR(300) = NULL,
    @EstadoId BIT = 1,
    @Operacion NVARCHAR(10)
AS
BEGIN
    SET NOCOUNT ON;

    IF @Operacion = 'CREATE'
    BEGIN
        INSERT INTO Negocio (Nombre, Direccion, LogoUrl, EstadoId)
        VALUES (@Nombre, @Direccion, @LogoUrl, @EstadoId);
    END
    ELSE IF @Operacion = 'UPDATE'
    BEGIN
        UPDATE Negocio
        SET Nombre = @Nombre,
            Direccion = @Direccion,
            LogoUrl = @LogoUrl,
            EstadoId = @EstadoId
        WHERE Id = @Id;
    END
    ELSE IF @Operacion = 'DELETE'
    BEGIN
        UPDATE Negocio
        SET EstadoId = 0
        WHERE Id = @Id;
    END
    ELSE IF @Operacion = 'SELECT'
    BEGIN
        SELECT * FROM Negocio
        WHERE EstadoId = 1;
    END
    ELSE IF @Operacion = 'SELECT_ID'
    BEGIN
        SELECT * FROM Negocio
        WHERE Id = @Id;
    END
END


---- Procedimiento almacenado rol ----
CREATE PROCEDURE sp_Rol
	@Id INT,
    @Nombre NVARCHAR(100) = NULL,
    @EstadoId BIT = 1,
	@Operacion NVARCHAR(10)
AS
BEGIN 
    SET NOCOUNT ON;

    IF @Operacion = 'CREATE'
    BEGIN
        INSERT INTO Rol (Nombre, EstadoId)
        VALUES (@Nombre, @EstadoId);
    END
	ELSE IF @Operacion = 'UPDATE'
	BEGIN
		UPDATE Rol
		Set Nombre = @Nombre,
		EstadoId = @EstadoId
		WHERE Id = @Id;
	END
	ELSE IF @Operacion = 'DELETE'
	BEGIN
		UPDATE Rol
		SET EstadoId = 0
        WHERE Id = @Id;
	END
	ELSE IF @Operacion = 'SELECT'
	BEGIN
		SELECT * FROM Rol
		WHERE EstadoId = 1;
	END
	ELSE IF @Operacion = 'SELECT_ID'
	BEGIN
		SELECT * FROM Rol
		WHERE Id = @Id;
	END
END

----- Procedimiento almacenado Rol ----
CREATE PROCEDURE sp_ObtenerRoles
AS BEGIN
	SELECT * FROM Rol
	WHERE EstadoId = 1;
END

----- Procedimiento almacenado Login/Usuario ----
CREATE PROCEDURE sp_ObtenerDatosUsuario
	@Nombre NVARCHAR(100),
	@Contra NVARCHAR(300)
AS BEGIN
	SELECT * FROM Usuario
	WHERE Nombre = @Nombre AND Contra = @Contra;
END

CREATE PROCEDURE sp_ObtenerRolIdNombre
	@Nombre NVARCHAR(100)
AS BEGIN
	SELECT Id FROM Usuario
	WHERE Nombre = @Nombre
END

---- Procedimiento almacenado RolPermisosMapping/rolpermisos ----
CREATE PROCEDURE sp_ObtenerNumeroPermisos
	@RolId INT,
	@FormularioId INT
AS BEGIN
	SELECT * FROM RolPermisosMapping
	WHERE RolId = @RolId AND FormId = @FormularioId AND EstadoId = 1
END

CREATE PROCEDURE sp_ObtenerEsPermitidoForm
	@RolId INT,
	@FormId INT
AS BEGIN
	SELECT EstadoId FROM RolPermisosMapping
	WHERE RolId = @RolId AND FormId = @FormId;
END

CREATE PROCEDURE sp_ActualizarRolPermisos
	@IsAllowed BIT,
	@RolId INT,
	@FormId INT
AS BEGIN
	IF EXISTS(SELECT 1 FROM RolPermisosMapping WHERE RolId = @RolId AND FormId = @FormId)
	BEGIN
	UPDATE RolPermisosMapping
	SET EstadoId = @IsAllowed
	WHERE RolId = @RolId AND FormId = @FormId;
END
	ELSE
	BEGIN
	INSERT INTO RolPermisosMapping (RolId, FormId, EstadoId)
	VALUES (@RolId, @FormId, @IsAllowed);
	END
END

---- Procedimiento almacenado Form/permisos ----
CREATE PROCEDURE sp_ObtenerForm
AS BEGIN
	SELECT * FROM Form;
END

CREATE PROCEDURE sp_ObtenerFormularioIdNombre
	@FormNombre NVARCHAR(100)
AS BEGIN
	SELECT Id FROM Form
	WHERE FormNombre = @FormNombre
END

CREATE PROCEDURE sp_InsertarForm
	@FormNombre NVARCHAR(100),
	@FormRuta NVARCHAR(100)
AS BEGIN
	INSERT INTO Form (FormNombre, FormRuta)
	VALUES (@FormNombre, @FormRuta);
END