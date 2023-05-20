CREATE OR REPLACE FUNCTION RegistrarNutricionista(
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
RETURNS VOID AS $$
BEGIN
  -- Insertar el nutricionista en la tabla NUTRICIONISTA
  INSERT INTO NUTRICIONISTA (Cedula, Foto, Nombre, Apellido1, Apellido2, Correo, Fecha_nacimiento, Tipo_cobro, Codigo, Tarjeta_credito, Contrasena, Direccion, Estatura, Peso)
  VALUES (cedula, foto, nombre, apellido1, apellido2, correo, fecha_nacimiento, tipo_cobro, codigo, tarjeta_credito, contrasena, direccion, estatura, peso);
  
  -- Puedes agregar aquí el código adicional que necesites después del registro
  
  RAISE NOTICE 'Nutricionista registrado exitosamente.';
END;
$$ LANGUAGE plpgsql;

-- ------------------------------------------------------------------------------------------------------- --

CREATE OR REPLACE FUNCTION AutenticarUsuario(
  email VARCHAR(100),
  password VARCHAR(100)
)
RETURNS VOID AS $$
DECLARE
  existe_nutricionista BOOLEAN;
BEGIN
  -- Verificar si el email y password corresponden a un nutricionista existente
  SELECT TRUE INTO existe_nutricionista
  FROM NUTRICIONISTA
  WHERE Correo = email AND Contrasena = password;
  
  IF existe_nutricionista THEN
    -- El usuario es un nutricionista, realizar la autenticación
    -- Puedes agregar aquí el código de autenticación específico para nutricionistas
    RAISE NOTICE 'Usuario autenticado como nutricionista.';
  ELSE
    -- El usuario no es un nutricionista, registrar como nutricionista
    -- Puedes agregar aquí el código para registrar al usuario como nutricionista
    -- Utiliza el procedimiento o función existente para registrar nutricionistas
    
    RAISE NOTICE 'Usuario no registrado como nutricionista.';
  END IF;
END;
$$ LANGUAGE plpgsql;

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
  codigo_barras VARCHAR(30)
)
AS $$
BEGIN
  -- Eliminar el producto con el código de barras especificado
  DELETE FROM PRODUCTO
  WHERE Codigo_barras = codigo_barras;
  
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
