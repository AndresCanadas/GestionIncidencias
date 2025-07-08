INSERT INTO Estados (EstadoID, Nombre)
VALUES ('1','Abierta'),('2','En Proceso'),('3','Cerrada');
SELECT * FROM Estados;

INSERT INTO Empleados (Nombre, Email)
VALUES 
  ('Sin asignar', 'sin.asignar@empresa.com'),
  ('Andrés Cañadas', 'andres.canadas@gmail.com'),
  ('Laura Martínez', 'laura.martinez@example.com'),
  ('Carlos Pérez', 'carlos.perez@example.com');

INSERT INTO Incidencias (Titulo, Descripcion, FechaCreacion, EstadoID, EmpleadoID)
VALUES 
  ('Pantalla azul', 'El PC se reinicia constantemente con error de pantalla azul.',GETDATE(), 1, 1),
  ('Problemas de red', 'Sin acceso a Internet en el segundo piso.', DATEADD(DAY, -3, GETDATE()), 2, 2),
  ('No imprime', 'La impresora de administración no responde.', DATEADD(DAY, -10, GETDATE()), 1, 1),
  ('Software caído', 'El sistema de gestión dejó de funcionar.', DATEADD(DAY, -1, GETDATE()), 1, 2);

