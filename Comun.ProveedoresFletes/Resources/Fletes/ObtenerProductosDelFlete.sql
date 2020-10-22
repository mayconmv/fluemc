SELECT
	P.Id,
	P.Descripcion,
	FP.Costo AS CostoDelFlete
FROM
	dbo.FleteProducto FP
	INNER JOIN dbo.Producto P ON P.Id = FP.ProductoId
WHERE FP.FleteId = {FleteId}