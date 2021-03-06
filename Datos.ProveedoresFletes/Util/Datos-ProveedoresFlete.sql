USE [master]
GO
/****** Object:  Database [ProveedoresFletes]    Script Date: 22/10/2020 17:32:24 ******/
CREATE DATABASE [ProveedoresFletes]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'ProveedoresFletes', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.MSSQLSERVER\MSSQL\DATA\ProveedoresFletes.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'ProveedoresFletes_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.MSSQLSERVER\MSSQL\DATA\ProveedoresFletes_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [ProveedoresFletes] SET COMPATIBILITY_LEVEL = 140
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [ProveedoresFletes].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [ProveedoresFletes] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [ProveedoresFletes] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [ProveedoresFletes] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [ProveedoresFletes] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [ProveedoresFletes] SET ARITHABORT OFF 
GO
ALTER DATABASE [ProveedoresFletes] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [ProveedoresFletes] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [ProveedoresFletes] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [ProveedoresFletes] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [ProveedoresFletes] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [ProveedoresFletes] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [ProveedoresFletes] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [ProveedoresFletes] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [ProveedoresFletes] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [ProveedoresFletes] SET  DISABLE_BROKER 
GO
ALTER DATABASE [ProveedoresFletes] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [ProveedoresFletes] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [ProveedoresFletes] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [ProveedoresFletes] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [ProveedoresFletes] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [ProveedoresFletes] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [ProveedoresFletes] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [ProveedoresFletes] SET RECOVERY FULL 
GO
ALTER DATABASE [ProveedoresFletes] SET  MULTI_USER 
GO
ALTER DATABASE [ProveedoresFletes] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [ProveedoresFletes] SET DB_CHAINING OFF 
GO
ALTER DATABASE [ProveedoresFletes] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [ProveedoresFletes] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [ProveedoresFletes] SET DELAYED_DURABILITY = DISABLED 
GO
EXEC sys.sp_db_vardecimal_storage_format N'ProveedoresFletes', N'ON'
GO
ALTER DATABASE [ProveedoresFletes] SET QUERY_STORE = OFF
GO
USE [ProveedoresFletes]
GO
/****** Object:  Table [dbo].[Cliente]    Script Date: 22/10/2020 17:32:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Cliente](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Nombre] [nvarchar](60) NULL,
	[DiasDePago] [int] NULL,
	[Temperatura] [bit] NULL,
	[Flete] [bit] NULL,
	[Tipo] [nvarchar](30) NULL,
	[Porcentaje] [float] NULL,
	[Absoluto] [nvarchar](30) NULL,
	[ProveedorId] [bigint] NOT NULL,
 CONSTRAINT [PK_Cliente] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Contacto]    Script Date: 22/10/2020 17:32:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Contacto](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Nombre] [nvarchar](60) NULL,
	[Departamento] [nvarchar](30) NULL,
	[EMail] [nvarchar](60) NULL,
	[Telefono] [nvarchar](15) NULL,
	[ProveedorId] [bigint] NOT NULL,
 CONSTRAINT [PK_Contacto] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Flete]    Script Date: 22/10/2020 17:32:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Flete](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Negocio] [nvarchar](60) NULL,
	[CostoTotal] [float] NULL,
	[ProveedorId] [bigint] NOT NULL,
 CONSTRAINT [PK_Flete] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[FleteProducto]    Script Date: 22/10/2020 17:32:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FleteProducto](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[FleteId] [bigint] NOT NULL,
	[ProductoId] [bigint] NOT NULL,
	[Costo] [float] NULL,
 CONSTRAINT [PK_FleteProducto] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Producto]    Script Date: 22/10/2020 17:32:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Producto](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Descripcion] [nvarchar](80) NULL,
	[CostoDelFlete] [float] NOT NULL,
 CONSTRAINT [PK_Producto] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Proveedor]    Script Date: 22/10/2020 17:32:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Proveedor](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Nombre] [nvarchar](60) NULL,
	[Direccion] [nvarchar](30) NULL,
	[RefProveedor] [int] NULL,
 CONSTRAINT [PK_Proveedor] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Cliente]  WITH CHECK ADD  CONSTRAINT [FK_Cliente_Proveedor] FOREIGN KEY([ProveedorId])
REFERENCES [dbo].[Proveedor] ([Id])
GO
ALTER TABLE [dbo].[Cliente] CHECK CONSTRAINT [FK_Cliente_Proveedor]
GO
ALTER TABLE [dbo].[Contacto]  WITH CHECK ADD  CONSTRAINT [FK_Contacto_Proveedor] FOREIGN KEY([ProveedorId])
REFERENCES [dbo].[Proveedor] ([Id])
GO
ALTER TABLE [dbo].[Contacto] CHECK CONSTRAINT [FK_Contacto_Proveedor]
GO
ALTER TABLE [dbo].[Flete]  WITH CHECK ADD  CONSTRAINT [FK_Flete_Proveedor] FOREIGN KEY([ProveedorId])
REFERENCES [dbo].[Proveedor] ([Id])
GO
ALTER TABLE [dbo].[Flete] CHECK CONSTRAINT [FK_Flete_Proveedor]
GO
ALTER TABLE [dbo].[FleteProducto]  WITH CHECK ADD  CONSTRAINT [FK_FleteProducto_Flete] FOREIGN KEY([FleteId])
REFERENCES [dbo].[Flete] ([Id])
GO
ALTER TABLE [dbo].[FleteProducto] CHECK CONSTRAINT [FK_FleteProducto_Flete]
GO
ALTER TABLE [dbo].[FleteProducto]  WITH CHECK ADD  CONSTRAINT [FK_FleteProducto_Producto] FOREIGN KEY([ProductoId])
REFERENCES [dbo].[Producto] ([Id])
GO
ALTER TABLE [dbo].[FleteProducto] CHECK CONSTRAINT [FK_FleteProducto_Producto]
GO
/****** Object:  StoredProcedure [dbo].[CrearCliente]    Script Date: 22/10/2020 17:32:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[CrearCliente]
	@Nombre NVARCHAR(60),
	@DiasDePago INT,
	@Temperatura INT,
	@Flete BIT,
	@Tipo NVARCHAR(30),
	@Porcentaje FLOAT, 
	@Absoluto NVARCHAR(30), 
	@ProveedorId BIGINT
AS
	INSERT INTO dbo.Cliente
		([Nombre],[DiasDePago],[Temperatura],[Flete],[Tipo],[Porcentaje],[Absoluto],[ProveedorId])
	VALUES
		(@Nombre,@DiasDePago,@Temperatura,@Flete,@Tipo,@Porcentaje,@Absoluto,@ProveedorId)
GO
USE [master]
GO
ALTER DATABASE [ProveedoresFletes] SET  READ_WRITE 
GO
