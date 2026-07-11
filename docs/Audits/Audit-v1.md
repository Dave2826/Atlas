# Auditoría Arquitectónica v1

**Fecha**: 2026-07-11

**Alcance**: Evaluación general del proyecto Atlas después de la implementación de los módulos Dashboard, Login, Productos, Departamentos y Tipos de Producto.

---

## Arquitectura

### Estilo
MVC tradicional con inyección de dependencias. Los controladores acceden directamente al `DbContext` sin capa de servicios ni repositorios.

### Fortalezas detectadas

| Aspecto | Detalle |
|---|---|
| Configuraciones Fluent API separadas | 9 archivos en `Data/Configurations/` |
| Auto-descubrimiento de configuraciones | `ApplyConfigurationsFromAssembly` en `DbContext` |
| BCrypt para contraseñas | Implementado en `AuthController` y `UserConfiguration` |
| Cookie Authentication estándar | Configurado en `Program.cs` |
| `[Authorize]` en todos los controllers | Excepto login |
| `[ValidateAntiForgeryToken]` en todos los POST | Implementado consistentemente |
| Namespaces consistentes | `Atlas.Controllers`, `Atlas.Models.Entities`, `Atlas.Data.*` |
| Patrón de catálogo reutilizable | Department + ProductType con IsActive, búsqueda, filtro |
| Design-time factory para migraciones | `AtlasDesignTimeDbContextFactory.cs` |

### Deuda técnica

| Hallazgo | Archivo | Impacto |
|---|---|---|
| Connection string con password en texto plano | `AtlasDesignTimeDbContextFactory.cs:12` | Crítico — exposición de credenciales |
| Cascade Delete inconsistente en User | Migración `InitialCreate` | Importante — borrar un User eliminaría CashSessions y Sales |
| Entidades sin Configuration | Sale, SaleDetail, SalePayment, Expense, CashSession, SpecialOrder, LayawayPayment | Importante — dependen de convenciones EF Core |
| Configuraciones comentadas sin implementar | `CustomerConfiguration.cs:24`, `RoleConfiguration.cs:32` | Menor — código muerto |
| Debug `Console.WriteLine` en controllers | `ProductController.cs:46-52` (eliminado), `AuthController.cs:40-49` (eliminado) | Menor — ya corregido |
| Mezcla de idiomas en UI | Dashboard, Layout (corregido) | Menor — ya corregido |
| Directorios DTOs/Repositories/Services vacíos | Solo `.gitkeep` | Menor — estructura confusa |
| Sin paginación en listados | `ProductController`, `DepartmentController`, `ProductTypeController` | Menor — aceptable para etapa actual |
| Sin ILogger | Todos los controllers | Menor — usar `Console.WriteLine` |

### Pendientes para etapas posteriores

- Implementar Services/Repositories/DTOs
- Agregar paginación en listados
- Incorporar ILogger en lugar de Console.WriteLine
- Implementar ViewModels para vistas de catálogo
- Agregar middleware de errores global con logging estructurado
- Implementar caché
- Crear proyecto de tests unitarios y de integración
- Implementar auditoría (CreatedBy/UpdatedBy)
- Internacionalización/localización

---

## Seguridad

| Aspecto | Estado |
|---|---|
| Autenticación | Cookie auth con BCrypt |
| Autorización | `[Authorize]` global |
| Anti-Forgery | `[ValidateAntiForgeryToken]` |
| Password hashing | BCrypt.Net-Next 4.2.0 |
| Connection string expuesta | En factory y appsettings |
| SQL Injection | Prevenido por EF Core |
| XSS | Prevenido por Razor |

---

## Conclusiones

El proyecto tiene una base sólida con deuda técnica mínima. Los riesgos principales identificados son:

1. **Créditos de BD expuestos** en `AtlasDesignTimeDbContextFactory.cs`
2. **Cascades inconsistentes** en la relación User → CashSession/Sale

Ambos son abordables sin cambiar la arquitectura. Se recomienda resolverlos antes de continuar con módulos transaccionales (Ventas, Caja).

El patrón de catálogo (Department/ProductType) está correctamente implementado y es 100% reutilizable para los próximos módulos master (Brands, Sizes, Suppliers).
