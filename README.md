# API REST - Gestor de Incidencias (.NET 9)

Este proyecto es una API REST desarrollada con **.NET 9 (Minimal API)** y **Entity Framework Core**, que gestiona un sistema de incidencias, empleados y estados. Utiliza SQL Server como base de datos.

## Instrucciones para ejecutar el proyecto

### Requisitos previos

- .NET SDK 9
- SQL Server Express o Developer
- SQL Server Management Studio
- Visual Studio 2022 o superior
- Postman

### Configuración paso a paso

#### 1. Clonar el proyecto

URL GIT: https://github.com/AndresCanadas/GestionIncidencias.git

#### 2. Configuración BBDD

#### 2.1 Creación Instancia SQL

Durante la instalación de SQL Server Express o Developer:

 1. Selecciona "Nueva instalación independiente de SQL Server".

 2. En el paso de configuración de instancia:

  - Elige "Instancia con nombre" y escribe por ejemplo: GESTORINCIDENCIAS

 3. Completa la instalación con configuración por defecto o personalizada.

Una vez instalada:

  - Puedes conectarte desde SQL Server Management Studio (SSMS) o Visual Studio SQL Server a localhost\GESTORINCIDENCIAS

#### 2.2 Ejecutar ScriptSQL

Ejecuta el archivo ScriptGestionIncidenciasDB.sql y ScriptDatos.sql respectivamente (incluido en el proyecto) para crear la base de datos con sus tablas y añadir datos en la instancia de SQL.

#### 2.3 Configuración cadena de conexión

#### Nota Importante

Una vez creada la instancia y ejecutado el ScriptSQL se puede conexionar localmente en el visual Studio o con el SQL Server Management Studio (SSMS), las cadenas de conexión son diferentes.

En `appsettings.json`:

> Visual Studio SQL Server:

```json
"ConnectionStrings": {
    "DefaultConnection": "Initial Catalog=NombreBaseDatos;Integrated Security=True;Trust Server Certificate=True;"
}
```

> SQL Server Management Studio (SSMS):

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=NombreDeTuInstancia;Database=NombreBaseDatos;Trusted_Connection=True;TrustServerCertificate=True;"
}
```


#### 4. Ejecutar la API

#### 4.1. Visual Studio

Ejecutar el proyecto en el "Iniciar https".

La API estará disponible en `https://localhost:5174` o `http://localhost:7107`

---


## Endpoints disponibles

### Empleados

| Método | Ruta                  | Descripción               |
| ------ | --------------------- | ------------------------- |
| GET    | `/api/empleados`      | Lista todos los empleados |
| POST   | `/api/empleados/add`  | Crear nuevo empleado      |

**Ejemplo POST: `/api/empleados/add` **

```json
{
  "nombre": "Andrés Cañadas",
  "email": "andres.canadas@gmail.com"
}
```

### Incidencias

| Método | Ruta                              | Descripción                          |
| ------ | --------------------------------- | ------------------------------------ |
| GET    | `/api/listaIncidencias`           | Lista todas las incidencias          |
| GET    | `/api/incidencias?estado=Abierta` | Filtrar incidencias por estado       |
| POST   | `/api/incidencias`                | Crear nueva incidencia               |
| PUT    | `/api/incidencias/{id}/fecha`     | Editar fecha a incidencia            |
| PUT    | `/api/incidencias/{id}/asignar`   | Asignar empleado a incidencia        |

**Ejemplo POST: `/api/incidencias` **

```json
{
  "Titulo": "Pantalla negra",
  "Descripcion": "No enciende el sistema",
  "EstadoID": 1,
  "EmpleadoID": 2,
  "Fecha": "2025-07-01T10:00:00"
}
```

**Ejemplo PUT asignación o fecha**

 `/api/incidencias/{id}/asignar` :

```json
{
  "EmpleadoID": 2
}
```

`/api/incidencias/{id}/fecha` :

```json
{
  "Fecha": "2025-04-01"
}
```

### Estados

| Método | Ruta           | Descripción             |
| ------ | -------------- | ----------------------- |
| GET    | `/api/estados` | Lista todos los estados |

**Ejemplo respuesta:**

```json
[
  { "estadoID": 1, "nombre": "Abierta" },
  { "estadoID": 2, "nombre": "En progreso" },
  { "estadoID": 3, "nombre": "Resuelta" }
]
```

## Postman Ejemplos

https://andresteam-1205.postman.co/workspace/GestionIncidencias~be043a24-03fc-4a50-a51c-cd2d01075fe3/collection/6512371-87ef34f3-4feb-417f-93cc-6fdbc6a6ce9c?action=share&creator=6512371

---

## Servicio Incidencias

Creado un Servicio que borra incidencias "Cambia el estado a Cerrada" en la consola del proyecto cada 1 min aparecera un mensaje con las incidencias abiertas y revisará si alguna incidencia tiene más de 7 días para despues modificarla. 
La clase que se ocupa de ello es: `ServicioIncidencias.cs` .

---

## Comprobación de conexión

Puedes realizar la consulta `GET /api/estatus` para validar que está conectado a la base de datos y está funcionando correctamente.

---



