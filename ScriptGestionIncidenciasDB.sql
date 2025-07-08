SET XACT_ABORT ON

BEGIN TRANSACTION SCRIPTDB

CREATE TABLE [Empleados] (
    [EmpleadoID] int IDENTITY(1,1) NOT NULL ,
    [Nombre] nvarchar(100)  NOT NULL ,
    [Email] nvarchar(100)  NOT NULL ,
    CONSTRAINT [PK_Empleados] PRIMARY KEY CLUSTERED (
        [EmpleadoID] ASC
    )
)

CREATE TABLE [Incidencias] (
    [IncidenciaID] int IDENTITY(1,1) NOT NULL ,
    [Titulo] nvarchar(100)  NOT NULL ,
    [Descripcion] nvarchar(300)  NOT NULL ,
    [FechaCreacion] date  NOT NULL ,
    [EstadoID] int  NOT NULL ,
    [EmpleadoID] int  NOT NULL ,
    CONSTRAINT [PK_Incidencias] PRIMARY KEY CLUSTERED (
        [IncidenciaID] ASC
    )
)

CREATE TABLE [Estados] (
    [EstadoID] int  NOT NULL ,
    [Nombre] nvarchar(100)  NOT NULL ,
    CONSTRAINT [PK_Estados] PRIMARY KEY CLUSTERED (
        [EstadoID] ASC
    )
)

ALTER TABLE [Incidencias] WITH CHECK ADD CONSTRAINT [FK_Incidencias_EstadoID] FOREIGN KEY([EstadoID])
REFERENCES [Estados] ([EstadoID])

ALTER TABLE [Incidencias] CHECK CONSTRAINT [FK_Incidencias_EstadoID]

ALTER TABLE [Incidencias] WITH CHECK ADD CONSTRAINT [FK_Incidencias_EmpleadoID] FOREIGN KEY([EmpleadoID])
REFERENCES [Empleados] ([EmpleadoID])

ALTER TABLE [Incidencias] CHECK CONSTRAINT [FK_Incidencias_EmpleadoID]

COMMIT TRANSACTION SCRIPTDB