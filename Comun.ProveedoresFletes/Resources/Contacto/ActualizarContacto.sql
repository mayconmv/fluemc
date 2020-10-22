UPDATE dbo.Contacto
SET
	[Nombre] = '{Nombre}',
	[Departamento] = '{Departamento}',
	[EMail] = '{EMail}',
	[Telefono] = '{Telefono}',
	[ProveedorId] = {ProveedorId}
WHERE
	[Id] = {Id}