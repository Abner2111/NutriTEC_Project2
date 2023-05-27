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
CREATE OR REPLACE PROCEDURE udp_loginClient(
	inptCorreo varchar(100),
	inptContrasena varchar(100)
)
LANGUAGE SQL
AS $$
SELECT cliente.nombre, cliente.apellido1, cliente.apellido2
FROM cliente
WHERE cliente.correo = inptCorreo AND cliente.contrasena=inptContrasena;
END;$$;

/**
It registers a consumed product by a client given the user email,
the date, meal time and the id of the consumed product
**/
CREATE OR REPLACE PROCEDURE udp_registroConsumoProducto(
	inptcorreo VARCHAR(100),
	intpfecha date,
	inpttiempocomidaid int,
	inptproductoId int	
)
LANGUAGE plpgsql
AS $$
DECLARE 
	consumoExistenteId int;
BEGIN
	IF NOT EXISTS(SELECT Id FROM CONSUMO WHERE CONSUMO.cliente = inptcorreo AND CONSUMO.fecha = inptfecha AND CONSUMO.tiempocomidaid = inpttiempocomidaid) THEN
		INSERT INTO CONSUMO VALUES(inptcorreo, inpttiempocomidaid, inptfecha);
	END IF;
	consumoExistenteId = (SELECT Id from CONSUMO WHERE CONSUMO.cliente = inptcorreo AND CONSUMO.fecha = inptfecha);
	INSERT INTO CONSUMO_PROUCTO VALUES(consumoExistenteId, inptproductoId);
END $$
	
/**
It registers a consumed recipe by a client given the user email,
the date, meal time and the id of the consumed recipe
**/
CREATE OR REPLACE PROCEDURE udp_registroConsumoReceta(
	inptcorreo VARCHAR(100),
	intpfecha date,
	inpttiempocomidaid int,
	inptRecetaName RECETA.nombre%type
)
LANGUAGE plpgsql
AS $$
DECLARE 
	consumoExistenteId int;
BEGIN
	IF NOT EXISTS(SELECT Id FROM CONSUMO WHERE CONSUMO.cliente = inptcorreo AND CONSUMO.fecha = inptfecha AND CONSUMO.tiempocomidaid = inpttiempocomidaid) THEN
		INSERT INTO CONSUMO VALUES(inptcorreo, inpttiempocomidaid, inptfecha);
	END IF;
	consumoExistenteId = (SELECT Id from CONSUMO WHERE CONSUMO.cliente = inptcorreo AND CONSUMO.fecha = inptfecha);
	INSERT INTO CONSUMO_RECETA VALUES(consumoExistenteId, inptRecetaName);
end $$

/**
Registers the fisiological data given by the user in a specific date.
It acceps various null values. If a registry is given by the user in the
same date it overrides the values that are already stored
**/
CREATE OR REPLACE PROCEDURE udp_registroMedidas(
	inptFecha DATE,
	inptMedidaCintura INT,
	inptPorcentajeGrasa INT,
	inptPorcentajeMusculo INT,
	inptMedidaCadera INT,
	inptMedidaCuello INT,
	inptCorreoCliente VARCHAR(100)
)
LANGUAGE plpgsql
AS $$
DECLARE 
	medidaExistenteId INT;
BEGIN
	IF NOT EXISTS(SELECT Id FROM MEDIDA WHERE MEDIDA.correoclient=intCorreoCliente AND MEDIDA.fecha = inptFecha)
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
		medidaExistenteId = (SELECT Id FROM MEDIDA WHERE MEDIDA.correoclient=intCorreoCliente AND MEDIDA.fecha = inptFecha);
		IF NOT (inptMedidaCintura IS NULL) THEN
			UPDATE MEDIDA
			SET medidacintura = inptMedidaCintura
			WHERE id = medidaExistenteId;
		END IF;
		IF NOT (inptPorcentajeGrasa IS NULL) THEN
			UPDATE MEDIDA
			SET PorcentajeGrasa = inptPorcentajeGrasa
			WHERE id = medidaExistenteId;
		END IF;
		IF  NOT (inptPorcentajeMusculo IS NULL) THEN
			UPDATE MEDIDA
			SET PorcentajeMusculo = inptPorcentajeMusculo
			WHERE id = medidaExistenteId;
		END IF;
		IF NOT (inptMedidaCadera IS NULL) THEN
			UPDATE MEDIDA
			SET MedidaCadera = inptMedidaCadera
			WHERE id = medidaExistenteId;
		END IF;
		IF NOT (inptMedidaCuello IS NULL) THEN
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
LANGUAGE SQL
AS $$

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
end;$$;

