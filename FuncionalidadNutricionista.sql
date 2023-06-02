CREATE OR REPLACE PROCEDURE RegistrarNutricionista(
  cedula INT,
  foto BYTEA,
  nombre VARCHAR(50),
  apellido1 VARCHAR(50),
  apellido2 VARCHAR(50),
  correo VARCHAR(100),
  fecha_nacimiento DATE,
  tipo_cobro INT,
  codigo VARCHAR(50),
  tarjeta_credito VARCHAR(30),
  contrasena VARCHAR(100),
  direccion VARCHAR(100),
  estatura INT,
  peso INT
)
LANGUAGE plpgsql
AS $$
BEGIN
  -- Insertar el nutricionista en la tabla NUTRICIONISTA
  INSERT INTO NUTRICIONISTA (Cedula, Foto, Nombre, Apellido1, Apellido2, Correo, Fecha_nacimiento, Tipo_cobro, Codigo, Tarjeta_credito, Contrasena, Direccion, Estatura, Peso)
  VALUES (cedula, foto, nombre, apellido1, apellido2, correo, fecha_nacimiento, tipo_cobro, codigo, tarjeta_credito, contrasena, direccion, estatura, peso);
  
  -- Puedes agregar aquí el código adicional que necesites después del registro
  
  RAISE NOTICE 'Nutricionista registrado exitosamente.';
END;
$$;


-- ------------------------------------------------------------------------------------------------------- --

CREATE OR REPLACE FUNCTION udp_validar_credencialesN(p_correo VARCHAR(100), p_contrasena VARCHAR(100))
  RETURNS BOOLEAN AS
$$
BEGIN
  RETURN EXISTS (
    SELECT 1
    FROM NUTRICIONISTA
    WHERE Correo = p_correo AND Contrasena = p_contrasena
  );
END;
$$
LANGUAGE plpgsql;



-- --------------------------------------------------------------------------------------------------- --

CREATE OR REPLACE FUNCTION AgregarProducto(
  nombre VARCHAR(20),
  codigo_barras VARCHAR(30),
  tamano_porcion VARCHAR(10),
  grasa INT,
  energia INT,
  proteina INT,
  sodio INT,
  carbohidratos INT,
  hierro INT,
  vitamina_d INT,
  vitamina_b6 INT,
  vitamina_c INT,
  vitamina_k INT,
  vitamina_b INT,
  vitamina_b12 INT,
  vitamina_a INT,
  calcio INT
)
RETURNS VOID AS $$
BEGIN
  -- Insertar el nuevo producto en la tabla PRODUCTO
  INSERT INTO PRODUCTO (Nombre, Codigo_barras, Tamano_porcion, Grasa, Energia, Proteina, Sodio, Carbohidratos, Hierro, VitaminaD, VitaminaB6, VitaminaC, Vitaminak, VitaminaB, VitaminaB12, VitaminaA, Calcio)
  VALUES (nombre, codigo_barras, tamano_porcion, grasa, energia, proteina, sodio, carbohidratos, hierro, vitamina_d, vitamina_b6, vitamina_c, vitamina_k, vitamina_b, vitamina_b12, vitamina_a, calcio);
  
  RAISE NOTICE 'Producto agregado exitosamente.';
END;
$$ LANGUAGE plpgsql;

-- -------------------------------------------------------------------------------------- --

CREATE OR REPLACE PROCEDURE AprobarProducto(
  producto_id INT
)
AS $$
BEGIN
  -- Actualizar el campo Aprobado del producto a true
  UPDATE PRODUCTO
  SET Aprobado = true
  WHERE Id = producto_id;
  
  RAISE NOTICE 'Producto aprobado exitosamente.';
END;
$$ LANGUAGE plpgsql;

-- ------------------------------------------------------------------------------------ --

CREATE OR REPLACE PROCEDURE EliminarProductoPorCodigoBarras(
  codigo_barras_param VARCHAR(30)
)
AS $$
BEGIN
  -- Eliminar el producto con el código de barras especificado
  DELETE FROM PRODUCTO
  WHERE PRODUCTO.Codigo_barras = codigo_barras_param;
  
  RAISE NOTICE 'Producto eliminado exitosamente.';
END;
$$ LANGUAGE plpgsql;

-- ------------------------------------------------------------------------------------ --

CREATE OR REPLACE PROCEDURE EditarProductoPorCodigoBarras(
  codigo_barras_param VARCHAR(30),
  tamano_porcion_param VARCHAR(10),
  grasa_param INT,
  energia_param INT,
  proteina_param INT,
  sodio_param INT,
  carbohidratos_param INT,
  hierro_param INT,
  vitamina_d_param INT,
  vitamina_b6_param INT,
  vitamina_c_param INT,
  vitamina_k_param INT,
  vitamina_b_param INT,
  vitamina_b12_param INT,
  vitamina_a_param INT,
  calcio_param INT
)
AS $$
BEGIN
  -- Actualizar los datos del producto con el código de barras especificado
  UPDATE PRODUCTO
  SET Tamano_porcion = tamano_porcion_param,
      Grasa = grasa_param,
      Energia = energia_param,
      Proteina = proteina_param,
      Sodio = sodio_param,
      Carbohidratos = carbohidratos_param,
      Hierro = hierro_param,
      VitaminaD = vitamina_d_param,
      VitaminaB6 = vitamina_b6_param,
      VitaminaC = vitamina_c_param,
      Vitaminak = vitamina_k_param,
      VitaminaB = vitamina_b_param,
      VitaminaB12 = vitamina_b12_param,
      VitaminaA = vitamina_a_param,
      Calcio = calcio_param
  WHERE PRODUCTO.Codigo_barras = codigo_barras_param;
  
  RAISE NOTICE 'Producto editado exitosamente.';
END;
$$ LANGUAGE plpgsql;

-- ----------------------------------------------------------------------------------------------- --

CREATE OR REPLACE FUNCTION ObtenerNutricionistas()
RETURNS SETOF NUTRICIONISTA AS
$$
BEGIN
  RETURN QUERY SELECT * FROM NUTRICIONISTA;
END;
$$
LANGUAGE plpgsql;

-- ----------------------------------------------------------------------------------------------- --

CREATE OR REPLACE PROCEDURE EliminarNutricionista(IN p_cedula INT)
LANGUAGE plpgsql
AS $$
BEGIN
  DELETE FROM NUTRICIONISTA WHERE NUTRICIONISTA.Cedula = p_cedula;
  RAISE NOTICE 'Nutricionista eliminado exitosamente.';
END;
$$;

-- ----------------------------------------------------------------------------------------------- --
CREATE OR REPLACE PROCEDURE udp_editarnutricionista(
  IN p_cedula INT,
  IN p_foto BYTEA,
  IN p_nombre VARCHAR(50),
  IN p_apellido1 VARCHAR(50),
  IN p_apellido2 VARCHAR(50),
  IN p_correo VARCHAR(100),
  IN p_fecha_nacimiento DATE,
  IN p_tipo_cobro INT,
  IN p_codigo VARCHAR(50),
  IN p_tarjeta_credito VARCHAR(30),
  IN p_contrasena VARCHAR(100),
  IN p_direccion VARCHAR(100),
  IN p_estatura INT,
  IN p_peso INT
)
LANGUAGE plpgsql
AS $$
BEGIN
  UPDATE NUTRICIONISTA
  SET
    Foto = p_foto,
    Nombre = p_nombre,
    Apellido1 = p_apellido1,
    Apellido2 = p_apellido2,
    Correo = p_correo,
    Fecha_nacimiento = p_fecha_nacimiento,
    Tipo_cobro = p_tipo_cobro,
    Codigo = p_codigo,
    Tarjeta_credito = p_tarjeta_credito,
    Contrasena = p_contrasena,
    Direccion = p_direccion,
    Estatura = p_estatura,
    Peso = p_peso
  WHERE Cedula = p_cedula;

  RAISE NOTICE 'Nutricionista actualizado exitosamente.';
END;
$$;
-- ----------------------------------------------------------------------------------------------- --
CREATE OR REPLACE FUNCTION ObtenerProductos()
  RETURNS SETOF PRODUCTO AS $$
BEGIN
  RETURN QUERY SELECT * FROM PRODUCTO;
END;
$$ LANGUAGE plpgsql;
