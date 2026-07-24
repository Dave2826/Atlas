# ADR-004: DeleteBehavior.Restrict en catálogos

**Status**: Accepted

**Context**

EF Core permite configurar el comportamiento de eliminación en las relaciones entre entidades. Por omisión, algunas relaciones podrían configurarse como Cascade, eliminando automáticamente registros hijos cuando se elimina un registro padre. Esta conducta es peligrosa en un sistema transaccional donde los datos históricos deben preservarse.

**Decision**

Todos los catálogos y relaciones del sistema utilizan `DeleteBehavior.Restrict`. Esto significa que no se puede eliminar un registro padre si existen registros hijos que dependan de él. El sistema utiliza soft-delete (campo `IsActive`) en todos los catálogos, por lo que la eliminación física está deshabilitada. El Restrict actúa como una capa adicional de protección.

**Consequences**

- Se protege la integridad referencial de todos los datos históricos.
- No existe riesgo de perder información por eliminaciones en cascada no intencionadas.
- Para "eliminar" un registro, se marca como inactivo (`IsActive = false`).
- Las migraciones futuras deben mantener esta regla explícitamente en cada configuración.
