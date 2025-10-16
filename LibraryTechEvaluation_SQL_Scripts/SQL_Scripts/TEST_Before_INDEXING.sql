SET STATISTICS IO ON;
SET STATISTICS TIME ON;

SELECT p.Prestamo_Id, l.Titulo, a.Nombre
FROM Prestamos p
JOIN Libros l ON p.Libro_Id = l.Libro_Id
JOIN Autores a ON l.Autor_Id = a.Autor_Id
WHERE p.Fecha_Devolucion IS NULL;
