-- Consumo receta
CREATE OR REPLACE FUNCTION d_consumo_receta() 
RETURNS TRIGGER
AS
$$
BEGIN
	DELETE FROM CONSUMO_RECETA
	WHERE consumo_id = OLD.consumo_id;
	RETURN OLD;
END
$$
LANGUAGE plpgsql;

CREATE TRIGGER trigger_consumo_receta
	BEFORE DELETE ON consumo FOR EACH ROW
	EXECUTE PROCEDURE d_consumo_receta();
	

-- Consumo producto
CREATE OR REPLACE FUNCTION d_consumo_producto() 
RETURNS TRIGGER
AS
$$
BEGIN
	DELETE FROM CONSUMO_PRODUCTO
	WHERE consumo_id = OLD.consumo_id;
	RETURN OLD;
END
$$
LANGUAGE plpgsql;

CREATE TRIGGER trigger_consumo_producto
	BEFORE DELETE ON consumo FOR EACH ROW
	EXECUTE PROCEDURE d_consumo_producto();

	
	
	


-- Test
DELETE FROM CONSUMO_RECETA;