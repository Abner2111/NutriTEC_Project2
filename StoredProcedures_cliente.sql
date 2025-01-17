CREATE OR REPLACE PROCEDURE udp_newReceta(
	Receta_ VARCHAR
)
language plpgsql
AS $$
BEGIN
	IF NOT EXISTS (SELECT Nombre FROM RECETA WHERE receta.Nombre = Receta_) THEN
		INSERT INTO RECETA (Nombre) VALUES (Receta_);
	END IF;
END
$$


CREATE OR REPLACE PROCEDURE udpasignarproductosareceta(
	Receta_ VARCHAR,
	Producto_ VARCHAR
)
language plpgsql
AS $$
DECLARE 
	IdProducto int;
BEGIN
	IF NOT EXISTS (SELECT Nombre FROM RECETA WHERE RECETA.Nombre = Receta_) THEN
		INSERT INTO RECETA VALUES (Receta_);
	END IF;
	
	IdProducto = (SELECT Id FROM PRODUCTO WHERE PRODUCTO.nombre = Producto_);
	INSERT INTO PRODUCTOS_RECETA (Receta_name, Producto_id) VALUES (Receta_, IdProducto);
	
END
$$

/**
obtiene informacion nutricional de una receta dado el nommbre
**/
create or replace procedure udp_RecetasNutriInfo(inptRecetaName receta.nombre%type)
language sql
as $$
	SELECT RECETA.Nombre,
	SUM(PRODUCTO.Grasa) , 
	SUM(PRODUCTO.Energia) , 
	SUM(PRODUCTO.Proteina), 
	SUM(PRODUCTO.Sodio), 
	SUM(PRODUCTO.Carbohidratos), 
	SUM(PRODUCTO.Hierro), 
	SUM(PRODUCTO.VitaminaD),
	SUM(PRODUCTO.VitaminaB6),
	SUM(PRODUCTO.VitaminaC),
	SUM(PRODUCTO.Vitaminak),
	SUM(PRODUCTO.VitaminaB),
	SUM(PRODUCTO.VitaminaB12),
	SUM(PRODUCTO.VitaminaA), 
	SUM(PRODUCTO.Calcio)
	FROM PRODUCTOS_RECETA
	JOIN RECETA ON PRODUCTOS_RECETA.Receta_name = RECETA.Nombre
	JOIN PRODUCTO ON PRODUCTOS_RECETA.Producto_Id = Producto.Id
	WHERE RECETA.nombre = inptRecetaName
	GROUP BY RECETA.nombre;
	commit;
end;$$;


CREATE OR REPLACE PROCEDURE DeleteProductoReceta(
	Receta_ VARCHAR,
	Producto_id_ INT
)
language plpgsql
AS $$
BEGIN
	DELETE FROM PRODUCTOS_RECETA WHERE Receta_=Receta_name AND Producto_id_ = Producto_id;
	commit;
END
$$;

/**
It gives a list of ingredients for a recipe given its name
**/
create or replace procedure udp_getIngredientesReceta(
	inptreceta_name receta.nombre%type
)
language sql
as $$

	SELECT producto.nombre FROM productos_receta
	JOIN producto ON productos_receta.producto_id = producto.id
	WHERE productos_receta.receta_name = inptreceta_name;
	commit;
end;$$;

-- CLIENTE
CREATE OR REPLACE FUNCTION GetCliente()
RETURNS setof CLIENTE
language sql
AS
$$
	SELECT * FROM CLIENTE;
$$

/**
it adds a new client to the db given all the requiered details
**/
create or replace procedure udp_newClient(
	Correo varchar,
	Nombre varchar,
	Apellido1 varchar,
	Apellido2 varchar,
	Contrasena varchar,
	Pais varchar,
	Fecha_registro varchar,
	Fecha_nacimiento varchar,
	Estatura integer,
	Peso integer
)
language plpgsql    
as $$
begin 
	insert into cliente 
	values(Correo,
		   Nombre, 
		   Apellido1, 
		   Apellido2,
		   Contrasena, 
		   Pais, 
		   Fecha_registro, 
		   Fecha_nacimiento, 
		   Estatura,
		   Peso);
	commit;
end
$$

CREATE OR REPLACE PROCEDURE udp_updateClient(
	Correo_ varchar,
	Nombre_ varchar,
	Apellido1_ varchar,
	Apellido2_ varchar,
	Contrasena_ varchar,
	Pais_ varchar,
	Fecha_registro_ varchar,
	Fecha_nacimiento_ varchar,
	Estatura_ integer,
	Peso_ integer
)
language plpgsql
AS $$
BEGIN
	UPDATE CLIENTE 
	SET Nombre=Nombre_, Apellido1=Apellido1_, Apellido2=Apellido2_, Contrasena=Contrasena_,
	Pais=Pais_, Fecha_registro=Fecha_registro_, Fecha_nacimiento=Fecha_nacimiento_, 
	Estatura=Estatura_, Peso=Peso_
	WHERE Correo=Correo;
	commit;
END
$$


CREATE OR REPLACE PROCEDURE udp_deleteClient(
	Correo_ VARCHAR
)
language plpgsql
AS $$
BEGIN
	DELETE FROM CLIENTE WHERE Correo_=Correo;
	commit;
END
$$

/**gets client info when email and passwords match**/
CREATE OR REPLACE FUNCTION udp_loginClient(
	inptCorreo varchar(100),
	inptContrasena varchar(100)
)
RETURNS setof CLIENTE
LANGUAGE SQL
AS $$
SELECT *
FROM CLIENTE
WHERE cliente.correo = inptCorreo AND cliente.contrasena=inptContrasena;
$$;

/**
It registers a consumed product by a client given the user email,
the date, meal time and the id of the consumed product
**/
CREATE OR REPLACE PROCEDURE udp_registroConsumoProducto(
	inptcorreo VARCHAR(100),
	inptfecha VARCHAR,
	inpttiempocomidaid int,
	inptproductoId int	
)
LANGUAGE plpgsql
AS $$
DECLARE 
	consumoExistenteId int;
	cantidad_actual int;
BEGIN
	IF NOT EXISTS(SELECT Id FROM CONSUMO WHERE CONSUMO.cliente = inptcorreo AND CONSUMO.fecha = inptfecha AND CONSUMO.tiempocomidaid = inpttiempocomidaid Limit 1) THEN
		INSERT INTO CONSUMO (Cliente, TiempoComidaId, Fecha) VALUES(inptcorreo, inpttiempocomidaid, inptfecha);
	END IF;
	consumoExistenteId = (SELECT Id from CONSUMO WHERE CONSUMO.cliente = inptcorreo AND CONSUMO.fecha = inptfecha AND CONSUMO.tiempocomidaid = inpttiempocomidaid Limit 1);
	
	IF NOT EXISTS(SELECT  consumo_producto.cantidad from consumo_producto where consumo_producto.consumo_id = consumoExistenteId and consumo_producto.producto_id = inptproductoId Limit 1) THEN
		INSERT INTO CONSUMO_PRODUCTO VALUES(consumoExistenteId, inptproductoId);
	ELSE 
	
		cantidad_actual = (SELECT consumo_producto.cantidad from consumo_producto where consumo_producto.consumo_id = consumoExistenteId and consumo_producto.producto_id = inptproductoId Limit 1);
		UPDATE CONSUMO_PRODUCTO
		SET CANTIDAD = (cantidad_actual+1)
		WHERE consumo_id = consumoExistenteId and producto_id = inptproductoId;
	END IF;
END $$
	
/**
It registers a consumed recipe by a client given the user email,
the date, meal time and the id of the consumed recipe
**/
CREATE OR REPLACE PROCEDURE udp_registroConsumoReceta(
	inptcorreo VARCHAR,
	inptfecha VARCHAR,
	inpttiempocomidaid int,
	inptRecetaName RECETA.nombre%type
)
LANGUAGE plpgsql
AS $$
DECLARE 
	consumoExistenteId int;
	cantidad_actual int;
BEGIN
	IF NOT EXISTS(SELECT Id FROM CONSUMO WHERE CONSUMO.cliente = inptcorreo AND CONSUMO.fecha = inptfecha AND CONSUMO.tiempocomidaid = inpttiempocomidaid Limit 1) THEN
		INSERT INTO CONSUMO (Cliente, TiempoComidaId, Fecha) VALUES(inptcorreo, inpttiempocomidaid, inptfecha);
	END IF;
	consumoExistenteId = (SELECT Id from CONSUMO WHERE CONSUMO.cliente = inptcorreo AND CONSUMO.fecha = inptfecha AND CONSUMO.tiempocomidaid = inpttiempocomidaid Limit 1);
	
	IF NOT EXISTS(SELECT  consumo_receta.cantidad from consumo_receta where consumo_receta.consumo_id = consumoExistenteId and consumo_receta.receta_name = inptRecetaName Limit 1) THEN
		INSERT INTO CONSUMO_RECETA VALUES(consumoExistenteId, inptRecetaName);
	ELSE 
	
		cantidad_actual = (SELECT  consumo_receta.cantidad from consumo_receta where consumo_receta.consumo_id = consumoExistenteId and consumo_receta.receta_name = inptRecetaName Limit 1);
		UPDATE CONSUMO_RECETA
		SET CANTIDAD = (cantidad_actual+1)
		WHERE consumo_id = consumoExistenteId and receta_name = inptRecetaName;
	END IF;
END $$;

CALL udp_registroConsumoReceta('cliente@estudiantec.cr', '27-05-2023', 11, 'Pinto');

/**
Registers the fisiological data given by the user in a specific date.
It acceps various null values. If a registry is given by the user in the
same date it overrides the values that are already stored
**/
CREATE OR REPLACE PROCEDURE udp_registroMedidas(
	inptFecha VARCHAR,
	inptMedidaCintura INT,
	inptPorcentajeGrasa INT,
	inptPorcentajeMusculo INT,
	inptMedidaCadera INT,
	inptMedidaCuello INT,
	inptCorreoCliente VARCHAR
)
LANGUAGE plpgsql
AS $$
DECLARE 
	medidaExistenteId INT;
BEGIN
	IF NOT EXISTS(SELECT Id FROM MEDIDA WHERE MEDIDA.correocliente=inptCorreoCliente AND MEDIDA.fecha = inptFecha)
	THEN
	INSERT INTO MEDIDA (
		Fecha,
		MedidaCintura,
		PorcentajeGrasa,
		PorcentajeMusculo,
		MedidaCadera,
		MedidaCuello,
		CorreoCliente)
	VALUES(inptFecha, 
			inptMedidaCintura, 
			inptPorcentajeGrasa, 
			inptPorcentajeMusculo,
			inptMedidaCadera,
			inptMedidaCuello,
			inptCorreoCliente);
	ELSE
		medidaExistenteId = (SELECT Id FROM MEDIDA WHERE MEDIDA.correocliente=inptCorreoCliente AND MEDIDA.fecha = inptFecha);
		IF NOT (inptMedidaCintura IS NULL OR inptMedidaCintura = 0) THEN
			UPDATE MEDIDA
			SET medidacintura = inptMedidaCintura
			WHERE id = medidaExistenteId;
		END IF;
		IF NOT (inptPorcentajeGrasa IS NULL OR inptPorcentajeGrasa = 0) THEN
			UPDATE MEDIDA
			SET PorcentajeGrasa = inptPorcentajeGrasa
			WHERE id = medidaExistenteId;
		END IF;
		IF  NOT (inptPorcentajeMusculo IS NULL OR inptPorcentajeMusculo = 0) THEN
			UPDATE MEDIDA
			SET PorcentajeMusculo = inptPorcentajeMusculo
			WHERE id = medidaExistenteId;
		END IF;
		IF NOT (inptMedidaCadera IS NULL OR inptMedidaCadera = 0) THEN
			UPDATE MEDIDA
			SET MedidaCadera = inptMedidaCadera
			WHERE id = medidaExistenteId;
		END IF;
		IF NOT (inptMedidaCuello IS NULL OR inptMedidaCuello = 0) THEN
			UPDATE MEDIDA
			SET MedidaCuello = inptMedidaCuello
			WHERE id = medidaExistenteId;
		END IF;
		IF NOT (inptCorreoCliente IS NULL) THEN
			UPDATE MEDIDA
			SET CorreoCliente = inptCorreoCliente
			WHERE id = medidaExistenteId;
		END IF;
	END IF;
END $$;


CREATE OR REPLACE PROCEDURE udp_newProduct(
	inptNombre VARCHAR(20),
	inptCodigo_barras VARCHAR(30),
	inptTamano_porcion VARCHAR(10),
	inptGrasa INT,
	inptEnergia INT,
	inptProteina INT,
	inptSodio INT,
	inptCarbohidratos INT,
	inptHierro INT,
	inptVitaminaD INT,
	inptVitaminaB6 INT,
	inptVitaminaC INT,
	inptVitaminak INT,
	inptVitaminaB INT,
	inptVitaminaB12 INT,
	inptVitaminaA INT,
	inptCalcio INT)
LANGUAGE plpgsql
AS $$
BEGIN
	INSERT INTO producto(Nombre,
		Codigo_barras,
		Tamano_porcion,
		Grasa,
		Energia,
		Proteina,
		Sodio,
		Carbohidratos,
		Hierro,
		VitaminaD,
		VitaminaB6,
		VitaminaC,
		Vitaminak,
		VitaminaB,
		VitaminaB12,
		VitaminaA,
		Calcio)
		VALUES(
		inptNombre,
		inptCodigo_barras,
		inptTamano_porcion,
		inptGrasa,
		inptEnergia,
		inptProteina,
		inptSodio,
		inptCarbohidratos,
		inptHierro,
		inptVitaminaD,
		inptVitaminaB6,
		inptVitaminaC,
		inptVitaminak,
		inptVitaminaB,
		inptVitaminaB12,
		inptVitaminaA,
		inptCalcio
		);
		COMMIT;
end
$$