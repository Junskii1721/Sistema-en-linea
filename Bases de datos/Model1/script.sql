CREATE DATABASE DBFacturacion
go

USE DBFacturacion
GO
/****** Object:  Table [dbo].[Almacen]    Script Date: 22/7/2022 10:37:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Almacen](
	[AlmacenID] [numeric](8, 0) IDENTITY(1,1) NOT NULL,
	[Nombre_De_La_Distribuidora] [nvarchar](255) NOT NULL,
	[Direccion] [nvarchar](255) NOT NULL,
	[CiudadID] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[AlmacenID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Ciudad]    Script Date: 22/7/2022 10:37:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Ciudad](
	[CiudadID] [int] IDENTITY(1,1) NOT NULL,
	[Nombre_Ciudad] [nvarchar](255) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[CiudadID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DetalleFactura]    Script Date: 22/7/2022 10:37:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DetalleFactura](
	[ItemID] [int] IDENTITY(1,1) NOT NULL,
	[FacturaID] [int] NOT NULL,
	[ProductoID] [int] NOT NULL,
	[Cantidad] [numeric](4, 0) NOT NULL,
	[Precio_Unitario] [numeric](4, 2) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ItemID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Empleados]    Script Date: 22/7/2022 10:37:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Empleados](
	[EmpleadosID] [int] IDENTITY(1,1) NOT NULL,
	[Nombre_Del_Empleado] [varchar](255) NOT NULL,
	[Apellido_Del_Empleado] [varchar](255) NOT NULL,
	[FecNac] [date] NOT NULL,
	[Edad]  AS (datepart(year,getdate())-datepart(year,[FecNac])),
	[Numero_Telefonico] [numeric](8, 0) NOT NULL,
	[CargoID] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[EmpleadosID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Factura]    Script Date: 22/7/2022 10:37:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Factura](
	[FacturaID] [int] IDENTITY(1,1) NOT NULL,
	[VendedorID] [int] NOT NULL,
	[FechaFactura] [datetime] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[FacturaID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Info_Cargo]    Script Date: 22/7/2022 10:37:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Info_Cargo](
	[CargoID] [int] IDENTITY(1,1) NOT NULL,
	[Descripcion_Del_Cargo] [nvarchar](20) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[CargoID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Productos]    Script Date: 22/7/2022 10:37:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Productos](
	[ProductoID] [int] IDENTITY(1,1) NOT NULL,
	[Nombre_Del_Producto] [nvarchar](255) NOT NULL,
	[Cantidad_En_Inventario] [numeric](8, 0) NOT NULL,
	[Precio_Estandar] [numeric](8, 2) NOT NULL,
	[AlmacenID] [numeric](8, 0)NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ProductoID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Almacen] ON 

INSERT [dbo].[Almacen] ([AlmacenID], [Nombre_De_La_Distribuidora], [Direccion], [CiudadID]) VALUES (CAST(1 AS Numeric(8, 0)), N'Almacen 1', N'De donde fue el Lacmiel, 2 cuadras arriba, 1/2 cuadra al sur', 1)
INSERT [dbo].[Almacen] ([AlmacenID], [Nombre_De_La_Distribuidora], [Direccion], [CiudadID]) VALUES (CAST(2 AS Numeric(8, 0)), N'Almacen 2', N'Clínica Santa María, 1 cuadra al Sur, 20 varas abajor', 12)
INSERT [dbo].[Almacen] ([AlmacenID], [Nombre_De_La_Distribuidora], [Direccion], [CiudadID]) VALUES (CAST(3 AS Numeric(8, 0)), N'Almacen 3', N'De la Parroquia, 3 1/2 cuadras al Sur', 12)
SET IDENTITY_INSERT [dbo].[Almacen] OFF
GO
SET IDENTITY_INSERT [dbo].[Ciudad] ON 

INSERT [dbo].[Ciudad] ([CiudadID], [Nombre_Ciudad]) VALUES (1, N'Atlántico Norte')
INSERT [dbo].[Ciudad] ([CiudadID], [Nombre_Ciudad]) VALUES (2, N'Atlántico Sur')
INSERT [dbo].[Ciudad] ([CiudadID], [Nombre_Ciudad]) VALUES (3, N'Boaco')
INSERT [dbo].[Ciudad] ([CiudadID], [Nombre_Ciudad]) VALUES (4, N'Carazo')
INSERT [dbo].[Ciudad] ([CiudadID], [Nombre_Ciudad]) VALUES (5, N'Chinandega')
INSERT [dbo].[Ciudad] ([CiudadID], [Nombre_Ciudad]) VALUES (6, N'Chontales')
INSERT [dbo].[Ciudad] ([CiudadID], [Nombre_Ciudad]) VALUES (7, N'Esteli')
INSERT [dbo].[Ciudad] ([CiudadID], [Nombre_Ciudad]) VALUES (8, N'Granada')
INSERT [dbo].[Ciudad] ([CiudadID], [Nombre_Ciudad]) VALUES (9, N'Jinotega')
INSERT [dbo].[Ciudad] ([CiudadID], [Nombre_Ciudad]) VALUES (10, N'León')
INSERT [dbo].[Ciudad] ([CiudadID], [Nombre_Ciudad]) VALUES (11, N'Madriz')
INSERT [dbo].[Ciudad] ([CiudadID], [Nombre_Ciudad]) VALUES (12, N'Managua')
INSERT [dbo].[Ciudad] ([CiudadID], [Nombre_Ciudad]) VALUES (13, N'Masaya')
INSERT [dbo].[Ciudad] ([CiudadID], [Nombre_Ciudad]) VALUES (14, N'Matagalpa')
INSERT [dbo].[Ciudad] ([CiudadID], [Nombre_Ciudad]) VALUES (15, N'Nueva Segovia')
INSERT [dbo].[Ciudad] ([CiudadID], [Nombre_Ciudad]) VALUES (16, N'Río San Juan')
INSERT [dbo].[Ciudad] ([CiudadID], [Nombre_Ciudad]) VALUES (17, N'Rivas')
SET IDENTITY_INSERT [dbo].[Ciudad] OFF
GO
SET IDENTITY_INSERT [dbo].[Empleados] ON 

INSERT [dbo].[Empleados] ([EmpleadosID], [Nombre_Del_Empleado], [Apellido_Del_Empleado], [FecNac], [Numero_Telefonico], [CargoID]) VALUES (1, N'Martín', N'Cazeres', CAST(N'2000-07-26' AS Date), CAST(84283698 AS Numeric(8, 0)), 1)
SET IDENTITY_INSERT [dbo].[Empleados] OFF
GO
SET IDENTITY_INSERT [dbo].[Info_Cargo] ON 

INSERT [dbo].[Info_Cargo] ([CargoID], [Descripcion_Del_Cargo]) VALUES (1, N'Vendedor')
SET IDENTITY_INSERT [dbo].[Info_Cargo] OFF
GO
SET IDENTITY_INSERT [dbo].[Productos] ON 

INSERT [dbo].[Productos] ([ProductoID], [Nombre_Del_Producto], [Cantidad_En_Inventario], [Precio_Estandar], [AlmacenID]) VALUES (2, N'cable generico laptop', CAST(10 AS Numeric(8, 0)), CAST(0.33 AS Numeric(8, 2)), CAST(2 AS Numeric(8, 0)))
INSERT [dbo].[Productos] ([ProductoID], [Nombre_Del_Producto], [Cantidad_En_Inventario], [Precio_Estandar], [AlmacenID]) VALUES (3, N'CABLE DE AUDIO Y VIDEO', CAST(30 AS Numeric(8, 0)), CAST(15.00 AS Numeric(8, 2)), CAST(3 AS Numeric(8, 0)))
INSERT [dbo].[Productos] ([ProductoID], [Nombre_Del_Producto], [Cantidad_En_Inventario], [Precio_Estandar], [AlmacenID]) VALUES (4, N'CABLE DE AUDIO 3.5', CAST(15 AS Numeric(8, 0)), CAST(0.83 AS Numeric(8, 2)), CAST(1 AS Numeric(8, 0)))
INSERT [dbo].[Productos] ([ProductoID], [Nombre_Del_Producto], [Cantidad_En_Inventario], [Precio_Estandar], [AlmacenID]) VALUES (5, N'CABLE ARGOM USB Impresora', CAST(60 AS Numeric(8, 0)), CAST(2.83 AS Numeric(8, 2)), CAST(2 AS Numeric(8, 0)))
INSERT [dbo].[Productos] ([ProductoID], [Nombre_Del_Producto], [Cantidad_En_Inventario], [Precio_Estandar], [AlmacenID]) VALUES (6, N'CABLE ARGOM DURA SPRING TYPE', CAST(2 AS Numeric(8, 0)), CAST(4.03 AS Numeric(8, 2)), CAST(1 AS Numeric(8, 0)))
INSERT [dbo].[Productos] ([ProductoID], [Nombre_Del_Producto], [Cantidad_En_Inventario], [Precio_Estandar], [AlmacenID]) VALUES (7, N'CABLE ARGOM VGA 25FT', CAST(3 AS Numeric(8, 0)), CAST(4.03 AS Numeric(8, 2)), CAST(1 AS Numeric(8, 0)))
INSERT [dbo].[Productos] ([ProductoID], [Nombre_Del_Producto], [Cantidad_En_Inventario], [Precio_Estandar], [AlmacenID]) VALUES (8, N'CABLE ARGOM HDMI 50FT MT', CAST(9 AS Numeric(8, 0)), CAST(13.48 AS Numeric(8, 2)), CAST(1 AS Numeric(8, 0)))
INSERT [dbo].[Productos] ([ProductoID], [Nombre_Del_Producto], [Cantidad_En_Inventario], [Precio_Estandar], [AlmacenID]) VALUES (9, N'ADAPTADOR ONE EAC-108 HDMI', CAST(1 AS Numeric(8, 0)), CAST(1.51 AS Numeric(8, 2)), CAST(2 AS Numeric(8, 0)))
INSERT [dbo].[Productos] ([ProductoID], [Nombre_Del_Producto], [Cantidad_En_Inventario], [Precio_Estandar], [AlmacenID]) VALUES (26, N'PRUEBA', CAST(5 AS Numeric(8, 0)), CAST(100.00 AS Numeric(8, 2)), CAST(3 AS Numeric(8, 0)))
SET IDENTITY_INSERT [dbo].[Productos] OFF
GO
ALTER TABLE [dbo].[Almacen]  WITH CHECK ADD FOREIGN KEY([CiudadID])
REFERENCES [dbo].[Ciudad] ([CiudadID])
GO
ALTER TABLE [dbo].[DetalleFactura]  WITH CHECK ADD FOREIGN KEY([FacturaID])
REFERENCES [dbo].[Factura] ([FacturaID])
GO
ALTER TABLE [dbo].[DetalleFactura]  WITH CHECK ADD FOREIGN KEY([ProductoID])
REFERENCES [dbo].[Productos] ([ProductoID])
GO
ALTER TABLE [dbo].[Empleados]  WITH CHECK ADD FOREIGN KEY([CargoID])
REFERENCES [dbo].[Info_Cargo] ([CargoID])
GO
ALTER TABLE [dbo].[Factura]  WITH CHECK ADD FOREIGN KEY([VendedorID])
REFERENCES [dbo].[Empleados] ([EmpleadosID])
GO
ALTER TABLE [dbo].[Productos]  WITH CHECK ADD FOREIGN KEY([AlmacenID])
REFERENCES [dbo].[Almacen] ([AlmacenID])
GO

select*from [dbo].[Productos]