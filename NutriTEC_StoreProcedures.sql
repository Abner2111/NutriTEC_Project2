-- Plan
CREATE OR REPLACE PROCEDURE AddPlan(
	Nombre_ VARCHAR,
	NutricionistId_ INT,
	Tiempocomida_ VARCHAR,
	Comida_ VARCHAR
)
language plpgsql
AS $$
DECLARE
	IdPlan INT;
	IdComida INT;
	IdTiempoComida INT;
	IdProducto INT;
BEGIN
	IF NOT EXISTS (SELECT Id FROM PLAN WHERE PLAN.nombre = Nombre_) THEN
		INSERT INTO PLAN(Nombre, NutricionistId)
		VALUES (Nombre_, NutricionistId_);
	END IF;
	
	IdPlan = (SELECT Id FROM PLAN WHERE PLAN.nombre = Nombre_);
	IdTiempoComida = (SELECT Id FROM TIEMPO_COMIDA WHERE TIEMPO_COMIDA.nombre = Tiempocomida_);
	INSERT INTO COMIDA (tiempo_comida, planid) VALUES (IdTiempoComida, IdPlan);
	
	IdComida = (SELECT Id FROM COMIDA WHERE COMIDA.tiempo_comida = IdTiempoComida AND COMIDA.planid = IdPlan);
	
	IF EXISTS (SELECT RecetaName FROM RECETA_COMIDA WHERE RECETA_COMIDA.recetaname = Comida_) THEN
		INSERT INTO RECETA_COMIDA (comidaid, recetaname) VALUES (IdComida, Comida_);
	ELSE
		IdProducto = (SELECT Id FROM PRODUCTO WHERE PRODUCTO.nombre = Comida_);
		INSERT INTO PRODUCTO_COMIDA (comidaid, producto_id) VALUES (IdComida, IdProducto);
	END IF;
	
	commit;
END
$$


CREATE OR REPLACE FUNCTION GetPlanes()
RETURNS setof PLAN
language sql
AS
$$
	SELECT * FROM PLAN;
$$


CREATE OR REPLACE FUNCTION GetPlanRecetas()
RETURNS TABLE(id INT, plan VARCHAR, nutricionistid INT, tiempocomida VARCHAR, comida VARCHAR)
language sql
AS
$$
	SELECT PLAN.id, PLAN.nombre, PLAN.nutricionistid, TIEMPO_COMIDA.nombre, RECETA_COMIDA.recetaname
	FROM (((PLAN
	INNER JOIN COMIDA ON PLAN.id = COMIDA.planid)
	INNER JOIN TIEMPO_COMIDA ON COMIDA.tiempo_comida = TIEMPO_COMIDA.id)
	INNER JOIN RECETA_COMIDA ON RECETA_COMIDA.comidaid = COMIDA.id);
$$

CREATE OR REPLACE FUNCTION GetPlanProductos()
RETURNS TABLE(id INT, plan VARCHAR, nutricionistid INT, tiempocomida VARCHAR, comida VARCHAR)
language sql
AS
$$
	SELECT PLAN.id, PLAN.nombre, PLAN.nutricionistid, TIEMPO_COMIDA.nombre, PRODUCTO.nombre
	FROM ((((PLAN
	INNER JOIN COMIDA ON PLAN.id = COMIDA.planid)
	INNER JOIN TIEMPO_COMIDA ON COMIDA.tiempo_comida = TIEMPO_COMIDA.id)
	INNER JOIN PRODUCTO_COMIDA ON PRODUCTO_COMIDA.comidaid = COMIDA.id)
	INNER JOIN PRODUCTO ON PRODUCTO.id = PRODUCTO_COMIDA.producto_id);
$$

SELECT * FROM GetPlanRecetas();
SELECT * FROM GetPlanProductos();



CREATE OR REPLACE FUNCTION GetPlanById(
	Id_ INT
)
RETURNS TABLE(id INT, plan VARCHAR, nutricionistid INT, tiempocomida VARCHAR, comida VARCHAR)
language sql
AS $$
	--SELECT * FROM PLAN WHERE plan.Id = Id_;
	SELECT PLAN.id, PLAN.nombre, PLAN.nutricionistid, TIEMPO_COMIDA.nombre, RECETA_COMIDA.recetaname
	FROM (((PLAN
	INNER JOIN COMIDA ON PLAN.id = COMIDA.planid)
	INNER JOIN TIEMPO_COMIDA ON COMIDA.tiempo_comida = TIEMPO_COMIDA.id)
	INNER JOIN RECETA_COMIDA ON RECETA_COMIDA.comidaid = COMIDA.id)
	WHERE plan.Id = Id_;
$$;


CREATE OR REPLACE PROCEDURE PutPlan(
	Id_ INT,
	Nombre_ VARCHAR,
	NutricionistId_ INT
)
language plpgsql
AS $$
BEGIN
	UPDATE PLAN 
	SET Nombre=Nombre_, NutricionistId=NutricionistId_
	WHERE Id_=Id;
	commit;
END
$$;


CREATE OR REPLACE PROCEDURE DeletePlan(
	Id_ INT
)
language plpgsql
AS $$
BEGIN
	DELETE FROM plan WHERE Id_=Id;
	commit;
END
$$;

CALL DeletePlan(1);

-- Busqueda de clientes
CREATE OR REPLACE FUNCTION GetClienteByCorreo(
	Correo_ VARCHAR
)
RETURNS setof CLIENTE
language sql
AS $$
	SELECT * FROM CLIENTE WHERE cliente.Correo = Correo_;
$$;

SELECT * FROM GetClienteByCorreo('string');


CREATE OR REPLACE FUNCTION GetClienteByNombre(
	Nombre_ VARCHAR
)
RETURNS setof CLIENTE
language sql
AS $$
	SELECT * FROM CLIENTE WHERE cliente.Nombre = Nombre_;
$$;


CREATE OR REPLACE FUNCTION GetClienteByNombreApellido(
	Nombre_ VARCHAR,
	Apellido1_ VARCHAR
)
RETURNS setof CLIENTE
language sql
AS $$
	SELECT * FROM CLIENTE WHERE cliente.Nombre = Nombre_ AND cliente.Apellido1 = Apellido1_;
$$;



-- Asociación de clientes
CREATE OR REPLACE PROCEDURE AddClienteToNutricionista(
	Nutricionista_ INT,
	Cliente_ VARCHAR
)
language plpgsql
AS $$
BEGIN
	INSERT INTO CLIENTES_NUTRICIONISTA(Nutricionista, Cliente)
	VALUES (Nutricionista_, Cliente_);
	commit;
END
$$


CREATE VIEW GetNutricionistaInfo
AS
SELECT NUTRICIONISTA.correo, NUTRICIONISTA.nombre || ' ' || NUTRICIONISTA.apellido1 || ' ' ||  NUTRICIONISTA.apellido2 as nombre_completo, NUTRICIONISTA.tarjeta_credito, TIPO_COBRO.descripcion, COUNT(CLIENTES_NUTRICIONISTA.cliente) AS clientes,
	CASE 
		WHEN TIPO_COBRO.descripcion = 'Semanal' THEN 0
		WHEN TIPO_COBRO.descripcion = 'Mensual' THEN 5
		WHEN TIPO_COBRO.descripcion = 'Anual' THEN 10
		ELSE 0
	END AS descuento,
	CASE 
		WHEN TIPO_COBRO.descripcion = 'Semanal' THEN COUNT(CLIENTES_NUTRICIONISTA.cliente)
		WHEN TIPO_COBRO.descripcion = 'Mensual' THEN COUNT(CLIENTES_NUTRICIONISTA.cliente) - COUNT(CLIENTES_NUTRICIONISTA.cliente) * 0.05
		WHEN TIPO_COBRO.descripcion = 'Anual' THEN COUNT(CLIENTES_NUTRICIONISTA.cliente) - COUNT(CLIENTES_NUTRICIONISTA.cliente) * 0.10
		ELSE 0
	END AS monto_cobrar
	FROM ((NUTRICIONISTA
	INNER JOIN TIPO_COBRO ON TIPO_COBRO.id = NUTRICIONISTA.tipo_cobro)
	INNER JOIN CLIENTES_NUTRICIONISTA ON CLIENTES_NUTRICIONISTA.nutricionista = NUTRICIONISTA.cedula)
	GROUP BY NUTRICIONISTA.cedula, TIPO_COBRO.id;


SELECT * FROM GetNutricionistaInfo;


-- Asignación de un plan
CREATE OR REPLACE PROCEDURE AddPlanToCliente(
	Cliente_ VARCHAR,
	PlanId_ INT,
	Fecha_inicio_ VARCHAR,
	Fecha_final_ VARCHAR
)
language plpgsql
AS $$
BEGIN
	INSERT INTO PLANES_CLIENTE(Cliente, PlanId, Fecha_inicio, Fecha_final)
	VALUES (Cliente_, PlanId_, Fecha_inicio_, Fecha_final_);
	commit;
END
$$


CREATE OR REPLACE FUNCTION GetPlanesCliente(
)
RETURNS setof PLANES_CLIENTE
language sql
AS
$$
	SELECT * FROM PLANES_CLIENTE;
$$

CREATE OR REPLACE FUNCTION GetPlanesOfCliente(
	Correo_ VARCHAR
)
RETURNS setof PLANES_CLIENTE
language sql
AS
$$
	SELECT * FROM PLANES_CLIENTE WHERE planes_cliente.Cliente = Correo_;
$$


CREATE OR REPLACE FUNCTION udp_validar_credencialesA(p_correo VARCHAR(100), p_contrasena VARCHAR(100))
  RETURNS BOOLEAN AS
$$
BEGIN
  RETURN EXISTS (
    SELECT 1
    FROM ADMINISTRADOR
    WHERE Correo = p_correo AND Contrasena = p_contrasena
  );
END;
$$
LANGUAGE plpgsql;


-- Pruebas
CALL AddPlan('Quema grasa', 123456789);
CALL AddPlan('Comer bien', 123456789);
CALL AddPlan('Plan A', 123456789);

SELECT * FROM GetPlan();
SELECT * FROM GetPlanById(4);

--CALL PutPlan(3, 'Gana grasa', 123456789);
--CALL DeletePlan(#number);

SELECT * FROM GetClienteByCorreo('cliente@estudiantec.cr');
SELECT * FROM GetClienteByNombre('Charlie');
SELECT * FROM GetClienteByNombreApellido('Charlie', 'Harper');

CALL AddPlanToCliente('cliente@estudiantec.cr', 3, '20-10-2022', '20-12-2022');

SELECT * FROM GetPlanesOfCliente('cliente@estudiantec.cr');