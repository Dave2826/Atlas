# ADR-002: ProductTypeSize como tabla puente

**Status**: Accepted

**Context**

Cada tipo de producto (ProductType) puede estar disponible en un subconjunto de tallas (Size). Por ejemplo, "Playeras" puede ofrecer tallas XS a XL, mientras que "Cinturones" solo ofrece tallas únicas. Se necesita una forma de definir qué tallas pertenecen a cada tipo de producto, sin modificar la estructura de los catálogos existentes.

**Decision**

Se implementa `ProductTypeSize` como una tabla puente (join table) con llave primaria compuesta `(ProductTypeId, SizeId)`. La tabla contiene únicamente estos dos campos, sin identificador autonumérico, sin metadata y sin propiedades adicionales. La relación se configura mediante Fluent API con `DeleteBehavior.Restrict` en ambos extremos.

**Consequences**

- La relación N:M entre ProductType y Size queda correctamente modelada.
- No se introducen columnas superfluas en la tabla puente.
- Las propiedades de navegación permiten recorrer la relación en ambos sentidos.
- El DeleteBehavior.Restrict protege la integridad de las relaciones existentes.
- La administración de las relaciones se realiza mediante el controlador de ProductType (ManageSizes).
