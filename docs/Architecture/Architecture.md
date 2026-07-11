# Arquitectura de Atlas

## Resumen

Atlas es un sistema de gestión retail construido sobre ASP.NET Core MVC con Entity Framework Core y PostgreSQL. Sigue una arquitectura MVC tradicional con inyección de dependencias y autenticación basada en cookies.

---

## Estilo Arquitectónico

Atlas utiliza el patrón **MVC (Model-View-Controller)**.

### Capas actuales

| Capa | Responsabilidad |
|---|---|
| `Controllers/` | Recibir peticiones HTTP, orquestar el flujo, retornar vistas |
| `Models/Entities/` | Representar las entidades del dominio del negocio |
| `Models/Enums/` | Definir enumeraciones del dominio |
| `Models/ViewModels/` | Modelos específicos para las vistas |
| `Views/` | Presentación de la interfaz de usuario (Razor + Bootstrap) |
| `Data/` | Configuración del DbContext y Entity Framework |
| `Data/Configurations/` | Configuración Fluent API de cada entidad |

### Patrón actual: Controller → DbContext

Actualmente los controladores inyectan `AtlasDbContext` directamente y realizan operaciones de base de datos sin capa intermedia.

Ejemplo representativo:

```csharp
[Authorize]
public class ProductController : Controller
{
    private readonly AtlasDbContext _context;

    public ProductController(AtlasDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        return View(await _context.Products
            .Include(p => p.ProductType)
            .ToListAsync());
    }
}
```

Las carpetas `Services/`, `Repositories/` y `DTOs/` existen en la estructura del proyecto pero están vacías. Su implementación está planificada para fases posteriores.

---

## Entity Framework Core

- **ORM**: Entity Framework Core 10.0.9
- **Base de datos**: PostgreSQL vía Npgsql
- **Migraciones**: Habilitadas con `AtlasDesignTimeDbContextFactory`
- **Configuración**: Fluent API mediante `IEntityTypeConfiguration<T>`

Todas las configuraciones se registran automáticamente mediante:

```csharp
modelBuilder.ApplyConfigurationsFromAssembly(typeof(AtlasDbContext).Assembly);
```

---

## Autenticación y Autorización

- **Esquema**: Cookie Authentication
- **Hash de contraseñas**: BCrypt.Net-Next
- **Control de acceso**: Atributo `[Authorize]` en todos los controllers excepto login
- **Anti-Forgery**: `[ValidateAntiForgeryToken]` en todos los endpoints POST

Configuración en `Program.cs`:

```csharp
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options => {
        options.LoginPath = "/Auth/Login";
        options.ExpireTimeSpan = TimeSpan.FromHours(8);
        options.SlidingExpiration = true;
    });
```

---

## Estructura de Carpetas

```
Atlas/
├── Atlas.csproj                     → Proyecto .NET 10.0
├── Migrations/                      → Migraciones de EF Core
├── Atlas/                           → Proyecto Web MVC
│   ├── Controllers/                 → AuthController, HomeController, ProductController,
│   │                                  DepartmentController, ProductTypeController
│   ├── Data/
│   │   ├── AtlasDbContext.cs        → DbContext principal
│   │   ├── AtlasDesignTimeDbContextFactory.cs
│   │   └── Configurations/          → Configuración Fluent API por entidad
│   ├── DTOs/                        → (vacío, uso futuro)
│   ├── Models/
│   │   ├── Entities/                → 18 entidades del dominio
│   │   ├── Enums/                   → 9 enumeraciones
│   │   └── ViewModels/              → LoginViewModel, ErrorViewModel
│   ├── Repositories/                → (vacío, uso futuro)
│   ├── Services/                    → (vacío, uso futuro)
│   ├── Views/                       → Vistas Razor organizadas por controller
│   └── Program.cs                   → Punto de entrada
└── docs/                            → Documentación del proyecto
```

---

## Convenciones del Proyecto

- **Namespaces**: `Atlas.Controllers`, `Atlas.Models.Entities`, `Atlas.Data.Configurations`, etc.
- **Idioma UI**: Español
- **Framework UI**: Bootstrap 5
- **Frontend JS**: jQuery + Bootstrap JS (sin frameworks JS adicionales)
- **Nombres de tablas en BD**: Plural en inglés (Products, Departments, Users)
- **DeleteBehavior**: `Restrict` como predeterminado para evitar eliminaciones en cascada no deseadas
- **Soft-delete en catálogos**: `IsActive` con `defaultValue(true)` para desactivación lógica

---

## Seguridad

- Autenticación mediante cookies seguras
- Contraseñas hasheadas con BCrypt (12 rounds)
- Tokens anti-forgery en todos los formularios
- SQL Injection prevenido por EF Core (parametrización automática)
- XSS prevenido por Razor (HTML encoding automático)
- Rutas protegidas con `[Authorize]`
