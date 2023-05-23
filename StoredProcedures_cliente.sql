create or replace procedure udp_RecetasNutriInfo()
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
	GROUP BY RECETA.Nombre;
	commit;
end;$$;