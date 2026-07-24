# ADR-006: Plantillas de configuración reutilizables

**Status**: Accepted

**Context**

El sistema requiere catálogos que comparten la misma estructura base: Id autonumérico, Name, Description, IsActive. Cada nuevo catálogo implica crear una entidad, una configuración Fluent, un controlador y cinco vistas. Este proceso es repetitivo y propenso a errores si se realiza manualmente cada vez.

**Decision**

Se utilizarán plantillas reutilizables para acelerar la creación de nuevos catálogos. Los catálogos existentes (Department, Brand, ProductType, Size) servirán como referencia canónica. La estructura común incluye:

- Entidad con `Id`, `Name`, `Description`, `IsActive`
- Configuración Fluent con `HasMaxLength`, `HasDefaultValue(true)` para IsActive
- Controlador con CRUD completo, búsqueda, filtro por estado y soft-delete
- Vistas con Bootstrap 5 siguiendo el mismo layout

**Consequences**

- La creación de nuevos catálogos es predecible y rápida.
- La consistencia entre catálogos se mantiene por diseño.
- Cualquier mejora en la plantilla base beneficia a todos los catálogos existentes.
- Se reduce la probabilidad de errores en la implementación de nuevos catálogos.
