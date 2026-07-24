# ADR-003: Productos sin variantes utilizarán la talla Unitalla

**Status**: Accepted

**Context**

No todos los productos tienen variantes por talla. Productos como accesorios, cinturones o gorras se venden en una sola presentación. El sistema necesita un mecanismo consistente para manejar ambos casos sin bifurcar la lógica de inventario.

**Decision**

Los productos sin variantes utilizarán la talla especial "Unitalla" en el catálogo Size. Nunca existirá stock directamente en Product. Todo el inventario, incluyendo productos sin variantes, se gestiona a través de `ProductSizeStock`. La talla "Unitalla" será una talla predecible en el sistema, con `DisplayOrder` inicial y `IsActive = true`.

**Consequences**

- El flujo de inventario es idéntico para todos los productos, con o sin variantes.
- No se requieren condicionales en la lógica de negocio para distinguir "con talla" vs "sin talla".
- El reporte de inventario y las consultas de stock utilizan el mismo mecanismo en todos los casos.
- La talla "Unitalla" debe existir en el catálogo Size antes de crear productos sin variantes.
