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
BEGIN
	IF NOT EXISTS (SELECT Id FROM PLAN WHERE PLAN.nombre = Nombre_) THEN
		INSERT INTO PLAN(Nombre, NutricionistId)
		VALUES (Nombre_, NutricionistId_);
	END IF;
	IdPlan = (SELECT Id FROM PLAN WHERE PLAN.nombre = Nombre_);
	IdTiempoComida = (SELECT Id FROM TIEMPO_COMIDA WHERE TIEMPO_COMIDA.nombre = Tiempocomida_);
	INSERT INTO COMIDA (tiempo_comida, planid) VALUES (IdTiempoComida, IdPlan);
	
	IdComida = (SELECT Id FROM COMIDA WHERE COMIDA.tiempo_comida = IdTiempoComida AND COMIDA.planid = IdPlan);
	INSERT INTO RECETA_COMIDA (comidaid, recetaname) VALUES (IdComida, Comida_);
	commit;
END
$$

SELECT * FROM GetPlan();
CALL AddPlan('Gana grasa', 123456789, 'Cena', 'Pinto');

CREATE OR REPLACE FUNCTION GetPlan()
RETURNS TABLE(id INT, plan VARCHAR, nutricionistid INT, tiempocomida VARCHAR, comida VARCHAR)
language sql
AS
$$
	--SELECT * FROM PLAN;
	SELECT PLAN.id, PLAN.nombre, PLAN.nutricionistid, TIEMPO_COMIDA.nombre, RECETA_COMIDA.recetaname
	FROM (((PLAN
	INNER JOIN COMIDA ON PLAN.id = COMIDA.planid)
	INNER JOIN TIEMPO_COMIDA ON COMIDA.tiempo_comida = TIEMPO_COMIDA.id)
	INNER JOIN RECETA_COMIDA ON RECETA_COMIDA.comidaid = COMIDA.id);
$$


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


CREATE OR REPLACE FUNCTION GetClientesDeNutricionista()
RETURNS setof CLIENTES_NUTRICIONISTA
language sql
AS
$$
	SELECT * FROM CLIENTES_NUTRICIONISTA;
$$



-- Asignación de un plan
CREATE OR REPLACE PROCEDURE AddPlanToCliente(
	Cliente_ VARCHAR,
	PlanId_ INT,
	Fecha_inicio_ DATE,
	Fecha_final_ DATE
)
language plpgsql
AS $$
BEGIN
	INSERT INTO PLANES_CLIENTE(Cliente, PlanId, Fecha_inicio, Fecha_final)
	VALUES (Cliente_, PlanId_, Fecha_inicio_, Fecha_final_);
	commit;
END
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