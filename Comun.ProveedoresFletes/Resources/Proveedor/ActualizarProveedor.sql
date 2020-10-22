UPDATE dbo.Proveedor
SET
	[Nombre] = '{Nombre}',
	[Direccion] = '{Direccion}',
	[RefProveedor] = {RefProveedor}
WHERE
	[Id] = {Id}