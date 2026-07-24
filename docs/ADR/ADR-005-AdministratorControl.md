# ADR-005: El administrador mantiene el control del sistema

**Status**: Accepted

**Context**

Los sistemas automatizados pueden tomar decisiones que no se alinean con la realidad operativa de un negocio. En una tienda de ropa, las condiciones cambian constantemente: promociones de último minuto, descuentos por cliente frecuente, productos dañados que deben ajustarse manualmente. Un sistema que automatiza sin permitir intervención humana puede volverse un obstáculo.

**Decision**

Atlas automatiza tareas repetitivas —cálculo de inventario, generación de reportes, validaciones de consistencia— pero las decisiones finales sobre operaciones críticas siempre pertenecen al administrador. El sistema nunca modificará precios, ajustará inventario, cancelará transacciones o alterará catálogos sin intervención explícita de un usuario autorizado.

**Consequences**

- El administrador tiene la última palabra en todas las operaciones que afectan la integridad del negocio.
- El sistema sugiere, valida y automatiza, pero no impone.
- Se requiere una interfaz clara para que el administrador pueda revisar y aprobar acciones automatizadas.
- Las validaciones automáticas (duplicados, inconsistencias) son obligatorias, pero nunca bloquean la operación sin posibilidad de revisión humana.
