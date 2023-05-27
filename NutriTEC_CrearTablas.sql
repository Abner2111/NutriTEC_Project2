CREATE TABLE PRODUCTO(
	Id SERIAL,
	Nombre VARCHAR(20) UNIQUE,
	Codigo_barras VARCHAR(30) UNIQUE,
	Tamano_porcion VARCHAR(10) NOT NULL,
	Aprobado Boolean NOT NULL DEFAULT false,
	Grasa INT,
	Energia INT,
	Proteina INT,
	Sodio INT,
	Carbohidratos INT,
	Hierro INT,
	VitaminaD INT,
	VitaminaB6 INT,
	VitaminaC INT,
	Vitaminak INT,
	VitaminaB INT,
	VitaminaB12 INT,
	VitaminaA INT,
	Calcio INT
);

CREATE TABLE RECETA(
	Nombre VARCHAR(100) NOT NULL
);

CREATE TABLE PRODUCTOS_RECETA(
	Receta_name VARCHAR(100) NOT NULL,
	Producto_id INT NOT NULL
);

CREATE TABLE TIEMPO_COMIDA(
	Id SERIAL,
	Nombre VARCHAR(50) NOT NULL
);

CREATE TABLE COMIDA(
	Id INT GENERATED ALWAYS AS IDENTITY NOT NULL,
	Tiempo_comida INT NOT NULL,
	PlanId INT NOT NULL
);

CREATE TABLE RECETA_COMIDA(
	ComidaId INT NOT NULL,
	RecetaName VARCHAR(100) NOT NULL
);

CREATE TABLE PRODUCTO_COMIDA(
	ComidaId INT NOT NULL,
	Producto_id INT NOT NULL
);

CREATE TABLE PLAN(
	Id SERIAL,
	Nombre VARCHAR(100) NOT NULL,
	NutricionistId INT NOT NULL
);

CREATE TABLE NUTRICIONISTA(
	Cedula INT NOT NULL,
	Foto BYTEA,
	Nombre VARCHAR(50) NOT NULL,
	Apellido1 VARCHAR(50) NOT NULL,
	Apellido2 VARCHAR(50),
	Correo VARCHAR(100) NOT NULL,
	Fecha_nacimiento DATE NOT NULL,
	Tipo_cobro INT NOT NULL,
	Codigo VARCHAR(50) UNIQUE NOT NULL,
	Tarjeta_credito VARCHAR(30) NOT NULL,
	Contrasena VARCHAR(100) NOT NULL,
	Direccion VARCHAR(100) NOT NULL,
	Estatura INT NOT NULL,
	Peso INT NOT NULL
);


CREATE TABLE TIPO_COBRO(
	Id SERIAL,
	Descripcion VARCHAR(15) NOT NULL
);

CREATE TABLE CLIENTE(
	Correo VARCHAR(100) NOT NULL,
	Nombre VARCHAR(50) NOT NULL,
	Apellido1 VARCHAR(50) NOT NULL,
	Apellido2 VARCHAR(50),
	Contrasena VARCHAR(100) NOT NULL,
	Pais VARCHAR(25) NOT NULL,
	Fecha_registro VARCHAR(50) NOT NULL,
	Fecha_nacimiento VARCHAR(50) NOT NULL,
	Estatura INT,
	Peso INT
);
	
CREATE TABLE PLANES_CLIENTE(
	Cliente VARCHAR(100) NOT NULL,
	PlanId INT NOT NULL,
	Fecha_inicio DATE NOT NULL,
	Fecha_final DATE NOT NULL
);

CREATE TABLE CONSUMO(
	Id SERIAL,
	Cliente VARCHAR(100) NOT NULL,
	TiempoComidaId INT NOT NULL,
	Fecha DATE NOT NULL
);

CREATE TABLE CONSUMO_RECETA(
	Consumo_id INT NOT NULL,
	Receta_name VARCHAR(100) NOT NULL,
	Cantidad INT DEFAULT 1
);

CREATE TABLE CONSUMO_PRODUCTO(
	Consumo_id INT NOT NULL,
	Producto_id INT NOT NULL,
	Cantidad INT DEFAULT 1
);

CREATE TABLE MEDIDA(
	Id SERIAL,
	Fecha VARCHAR(100),
	MedidaCintura INT,
	PorcentajeGrasa INT,
	PorcentajeMusculo INT,
	MedidaCadera INT,
	MedidaCuello INT,
	CorreoCliente VARCHAR(100) NOT NULL
);

CREATE TABLE CLIENTES_NUTRICIONISTA(
	Nutricionista INT NOT NULL,
	Cliente VARCHAR(100) NOT NULL
);

CREATE TABLE ADMINISTRADOR(
	Correo VARCHAR(100) NOT NULL,
	Contrasena VARCHAR(100) NOT NULL
);


-- Primary keys
ALTER TABLE PRODUCTO
	ADD PRIMARY KEY (Id);
	
ALTER TABLE RECETA
	ADD PRIMARY KEY (Nombre);

ALTER TABLE PRODUCTOS_RECETA
	ADD PRIMARY KEY (Receta_name, Producto_id);
	
ALTER TABLE TIEMPO_COMIDA
	ADD PRIMARY KEY (Id);
	
ALTER TABLE COMIDA
	ADD PRIMARY KEY (Id);

ALTER TABLE RECETA_COMIDA
	ADD PRIMARY KEY (ComidaId, RecetaName);
	
ALTER TABLE PRODUCTO_COMIDA
	ADD PRIMARY KEY (ComidaId, Producto_id);
	
ALTER TABLE PLAN
	ADD PRIMARY KEY (Id);
	
ALTER TABLE NUTRICIONISTA
	ADD PRIMARY KEY (Cedula);
	
ALTER TABLE TIPO_COBRO
	ADD PRIMARY KEY (Id);
	
ALTER TABLE CLIENTE
	ADD PRIMARY KEY (Correo);
	
ALTER TABLE PLANES_CLIENTE
	ADD PRIMARY KEY (Cliente, PlanId);

ALTER TABLE CONSUMO
	ADD PRIMARY KEY (Id);
	
ALTER TABLE MEDIDA
	ADD PRIMARY KEY (Id);

ALTER TABLE CLIENTES_NUTRICIONISTA
	ADD PRIMARY KEY (Nutricionista, Cliente);

ALTER TABLE ADMINISTRADOR
	ADD PRIMARY KEY (Correo);

ALTER TABLE CONSUMO_RECETA
	ADD PRIMARY KEY(Consumo_id, Receta_name);

ALTER TABLE CONSUMO_PRODUCTO
	ADD PRIMARY KEY(Consumo_id, Producto_id);

-- Foreign keys
ALTER TABLE PRODUCTOS_RECETA
	ADD FOREIGN KEY (Producto_id) REFERENCES PRODUCTO(Id),
	ADD FOREIGN KEY (Receta_name) REFERENCES RECETA(Nombre);

ALTER TABLE COMIDA
	ADD FOREIGN KEY (PlanId) REFERENCES PLAN(Id),
	ADD FOREIGN KEY (Tiempo_comida) REFERENCES TIEMPO_COMIDA(Id);

ALTER TABLE PLAN
	ADD FOREIGN KEY (NutricionistId) REFERENCES NUTRICIONISTA(Cedula);
	
ALTER TABLE PLANES_CLIENTE
	ADD FOREIGN KEY (Cliente) REFERENCES CLIENTE(Correo),
	ADD FOREIGN KEY (PlanId) REFERENCES PLAN(Id);
	
ALTER TABLE CONSUMO
	ADD FOREIGN KEY (Cliente) REFERENCES CLIENTE(Correo),
	ADD FOREIGN KEY (tiempocomidaid) REFERENCES tiempo_comida(Id);

ALTER TABLE MEDIDA
	ADD FOREIGN KEY (CorreoCliente) REFERENCES CLIENTE(Correo);

ALTER TABLE CONSUMO_RECETA
	ADD FOREIGN KEY (Consumo_id) REFERENCES CONSUMO(Id),
	ADD FOREIGN KEY (Receta_name) REFERENCES RECETA(Nombre);

ALTER TABLE CONSUMO_PRODUCTO
	ADD FOREIGN KEY (Consumo_id) REFERENCES CONSUMO(Id),
	ADD FOREIGN KEY (Producto_id) REFERENCES PRODUCTO(Id);
	
ALTER TABLE NUTRICIONISTA
	ADD FOREIGN KEY (Tipo_cobro) REFERENCES TIPO_COBRO(Id);
	
ALTER TABLE PRODUCTO_COMIDA
	ADD FOREIGN KEY (ComidaId) REFERENCES COMIDA(Id),
	ADD FOREIGN KEY (Producto_id) REFERENCES PRODUCTO(Id);

ALTER TABLE RECETA_COMIDA
	ADD FOREIGN KEY (ComidaId) REFERENCES COMIDA(Id),
	ADD FOREIGN KEY (RecetaName) REFERENCES RECETA(Nombre);
	
ALTER TABLE CLIENTES_NUTRICIONISTA
	ADD FOREIGN KEY (Nutricionista) REFERENCES NUTRICIONISTA(Cedula),
	ADD FOREIGN KEY (Cliente) REFERENCES CLIENTE(Correo);
	
-- Inserts
INSERT INTO TIEMPO_COMIDA(Nombre) VALUES('Desayuno');
INSERT INTO TIEMPO_COMIDA(Nombre) VALUES('Merienda mañana');
INSERT INTO TIEMPO_COMIDA(Nombre) VALUES('Almuerzo');
INSERT INTO TIEMPO_COMIDA(Nombre) VALUES('Merienda tarde');
INSERT INTO TIEMPO_COMIDA(Nombre) VALUES('Cena');
 
