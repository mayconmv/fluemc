UPDATE dbo.Cliente
SET
	[Nombre] = '{Nombre}',
	[DiasDePago] = {DiasDePago},
	[Temperatura] = {Temperatura},
	[Flete] = {Flete},
	[Tipo] = '{Tipo}',
	[Porcentaje] = {Porcentaje},
	[Absoluto] = '{Absoluto}',
	[ProveedorId] = {ProveedorId}
WHERE
	[Id] = {Id}