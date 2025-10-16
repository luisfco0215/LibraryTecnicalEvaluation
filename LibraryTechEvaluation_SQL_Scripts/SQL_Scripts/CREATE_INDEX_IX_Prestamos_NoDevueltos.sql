CREATE INDEX IX_Prestamos_NoDevueltos
ON Prestamos(Fecha_Devolucion)
WHERE Fecha_Devolucion IS NULL;