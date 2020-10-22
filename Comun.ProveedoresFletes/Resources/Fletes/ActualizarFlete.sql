UPDATE dbo.Flete
SET
	[Negocio] = '{Negocio}',
	[CostoTotal] = {CostoTotal},
	[ProveedorId] = {ProveedorId}
WHERE
	[Id] = {Id}