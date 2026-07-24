# ADR-001: Inventario centralizado en ProductSizeStock

**Status**: Accepted

**Context**

El sistema necesita gestionar el inventario de productos que pueden tener múltiples tallas. Inicialmente se consideró almacenar el stock directamente en la entidad Product, pero esto impediría controlar el inventario por talla de forma granular. También se consideró una tabla separada por cada tipo de variante, pero eso multiplicaría las entidades sin un beneficio claro.

**Decision**

Todo el stock vive únicamente en la entidad `ProductSizeStock`. La entidad `Product` nunca tendrá una propiedad `Stock`. Cada registro de `ProductSizeStock` representa la existencia de un producto en una talla específica (representada como un string, no como una FK a Size, para permitir tallas no catalogadas en contextos legacy).

**Consequences**

- El stock total de un producto se calcula sumando las cantidades de sus registros en `ProductSizeStock`.
- No existe riesgo de inconsistencia entre un campo `Stock` en `Product` y los registros detallados en `ProductSizeStock`.
- La consulta de inventario requiere una suma, pero esta operación es eficiente con índices adecuados.
- ProductSizeStock queda como la única fuente de verdad para existencias.
