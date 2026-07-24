# ADR-007: Department derivado desde ProductType

**Status**: Accepted

**Context**

La entidad Product necesita conocer el departamento al que pertenece cada producto. Inicialmente se consideró almacenar `DepartmentId` directamente en `Product`, lo que habría creado dos rutas para obtener el departamento: una directa (Product → Department) y otra indirecta (Product → ProductType → Department). Esto viola el principio de una única fuente de verdad.

**Decision**

`Product` NO almacenará `DepartmentId`. El departamento se obtiene mediante la cadena de navegación:

```
Product → ProductType → Department
```

`ProductType` ya contiene `DepartmentId` como FK. Esta es la única fuente de verdad para la relación producto-departamento. Cualquier consulta que necesite el departamento de un producto debe recorrer esta cadena.

**Consequences**

- Una única fuente de verdad para la relación producto-departamento.
- No existe riesgo de inconsistencia entre dos campos que deberían coincidir.
- Las consultas que requieren departamento necesitan un `Include` adicional (`ProductType.Department`), pero esto es aceptable y puede optimizarse con índices.
- La estructura refleja correctamente la realidad del negocio: el tipo de producto determina el departamento, no al revés.
