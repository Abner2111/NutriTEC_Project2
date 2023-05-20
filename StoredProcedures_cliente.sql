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

create or replace procedure udp_newClient(
	Correo varchar,
	Nombre varchar,
	Apellido1 varchar,
	Apellido2 varchar,
	Contrasena varchar,
	Pais varchar,
	Fecha_registro date,
	Fecha_nacimiento date,
	Estatura integer,
	Peso integer,
	OUT Msg VARCHAR)
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
	Msg = 'New Client Ok';

exception when others then
	Msg = 'Error';
end;$$; 

call udp_newClient('abneraq73@gmail.com'::varchar,
				   'Abner'::varchar,
				   'Arroyo'::varchar,
				   'null'::varchar,
				   '1'::varchar,
				   'costa rica'::varchar,
				   '19000101'::date,
				   '19000101'::date,
				   6,
				   5,
				  null::varchar);
		   
	
	