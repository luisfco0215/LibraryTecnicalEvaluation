# API Librería

## Descripción
Esta API permite gestionar autores, libros y préstamos. Los endpoints incluyen autenticación JWT y control de roles (Admin y Usuario). 
La documentación interactiva se encuentra disponible mediante **Swagger**.

---

## Endpoints principales

| Método | Ruta | Descripción | Roles | Código HTTP |
|--------|-----|------------|-------|------------|
| GET    | /libros/antes-de-2000 | Devuelve libros publicados antes del 2000 | Todos | 200, 500 |
| POST   | /libros | Crea un libro nuevo | Admin | 201, 400, 500 |
| GET    | /prestamos/no-devueltos | Lista préstamos no devueltos | Todos | 200, 500 |
| PUT    | /prestamos/{id} | Actualiza fecha de devolución | Admin/Usuario | 204, 404, 403 |
| DELETE | /prestamos/{id} | Elimina un préstamo | Admin/Usuario | 204, 404, 403 |

---

## Diseño Arquitectónico

- **Arquitectura:** API RESTful con capas separadas: Controladores, Repositorios y Contexto de EF Core.
- **Autenticación:** JWT con roles (Admin, Usuario).
- **Persistencia:** SQL Server usando Entity Framework Core 8.
- **Optimización:** Índices filtrados en `Prestamos` para mejorar consultas de préstamos no devueltos.

---

## Decisiones Técnicas

- Uso de **DTOs y Select en LINQ** para no traer datos innecesarios.
- Manejo de **excepciones** y retorno de códigos HTTP adecuados.
- **Swagger** para documentación interactiva.
- **Moq + xUnit** para pruebas unitarias, con cobertura de casos de éxito y fallo.
- JWT protegido en endpoints sensibles (POST, PUT, DELETE).
- Índices implementados en `Prestamos`:
  - `IX_Prestamos_NoDevueltos`: índice filtrado en `Fecha_Devolucion IS NULL`.
  - `IX_Prestamos_LibroId`: índice para optimizar JOINs con la tabla `Libros`.

---

## Requisitos

- .NET 8 SDK
- SQL Server
- Visual Studio 2022 o VS Code
- Postman (opcional, para pruebas de endpoints)

---

## Ejecución en Visual Studio 2022

1. Abrir la solución en Visual Studio 2022.
2. Seleccionar `LibraryTecnicalEvaluation` como proyecto de inicio.
3. Presionar **F5** para ejecutar la API con Swagger.
4. Abrir **Test Explorer** para ejecutar las pruebas unitarias.
5. Configurar la cadena de conexión y ejecutar migraciones desde **Package Manager Console**.
6. Usar Swagger o Postman para generar JWT y probar endpoints protegidos.

**Configurar la cadena de conexion en appsettings.json**
 "ConnectionStrings": {
   "DefaultConnection": "Server=TU_SERVIDOR;Database=LibreriaDB;Trusted_Connection=True;"
 }

**En Nuget Console, ejecutar el siguiente comando**
	Update-Database (luego de habilitar las migraciones)


## Para acceder a los EndPoint de login

1. POST /auth/login
	
{
    "username": "admin",
    "password": "12345"
}

## Respuesta

{
    "token": "eyJhbGciOiJIUzI1NiIsInR..."
}

 