-- Populacion
INSERT INTO TIPO_COBRO(Descripcion) VALUES('Mensual');
INSERT INTO TIPO_COBRO(Descripcion) VALUES('Quincenal');

INSERT INTO NUTRICIONISTA (Cedula, Foto, Nombre, Apellido1,	Apellido2, Correo, Fecha_nacimiento,
						   Tipo_cobro, Codigo, Tarjeta_credito, Contrasena, Direccion, Estatura, Peso)
	               VALUES (123456789, '', 'Ignacio', 'Grané', 'Rojas', 'ignaciograner@gmail.com',
						   '26-04-2023', 1, 'EL4701', '3000-2450-6025-8866', '5f4dcc3b5aa765d61d8327deb882cf99',
						   'Malibu, California, Estados Unidos', 174, 70);

INSERT INTO CLIENTE (Correo, Nombre, Apellido1, Apellido2, Contrasena, Pais, Fecha_registro,
					 Fecha_nacimiento, Estatura, Peso)
			 VALUES ('cliente@estudiantec.cr', 'Charlie', 'Harper', '', '82e8fe7e1194b8ce42addb5374ccb047',
				    'Costa Rica', '10-06-2022', '01-01-1985', 185, 90);

-- Verificacion
SELECT * FROM TIPO_COBRO;
SELECT * FROM NUTRICIONISTA;
SELECT * FROM CLIENTE;
SELECT * FROM PLAN;