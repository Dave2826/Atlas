# Atlas — Coding Standards

## Fluent API

Toda la configuración de entidades se realiza mediante Fluent API en clases separadas que implementan `IEntityTypeConfiguration<T>`. No se utilizan Data Annotations en las entidades del dominio.

```
Data/Configurations/
├── BrandConfiguration.cs
├── DepartmentConfiguration.cs
├── ProductTypeConfiguration.cs
├── ProductTypeSizeConfiguration.cs
├── SizeConfiguration.cs
└── ...
```

## Entity Configuration por entidad

Cada entidad tiene su propia clase de configuración. Las configuraciones se registran automáticamente mediante:

```csharp
modelBuilder.ApplyConfigurationsFromAssembly(typeof(AtlasDbContext).Assembly);
```

## ViewModels para vistas

Las vistas nunca reciben entidades del dominio directamente. Los ViewModels son contratos explícitos entre el controlador y la vista. Se ubican en `Models/ViewModels/`.

## Restrict en catálogos

Todas las relaciones hacia catálogos utilizan `DeleteBehavior.Restrict`. No se permite la eliminación en cascada. Esta regla protege la información histórica y evita la pérdida accidental de datos.

## JavaScript nativo cuando sea suficiente

Cuando la funcionalidad lo permite, se utiliza JavaScript nativo (vanilla JS) en lugar de librerías externas. Ejemplos: seleccionar / limpiar checkboxes, confirmaciones simples, manipulación básica del DOM. jQuery está disponible globalmente, pero no debe ser un requisito para funcionalidades nuevas.

## Conventional Commits

Los mensajes de commit siguen el formato:

```
<tipo>(<alcance>): <descripción>
```

Tipos utilizados:
- `feat`: Nueva funcionalidad
- `fix`: Corrección de bug
- `refactor`: Cambio de código sin cambio funcional
- `docs`: Documentación
- `chore`: Mantenimiento del proyecto

## No lógica de negocio en Views

Las vistas contienen exclusivamente lógica de presentación (bucles, condicionales de UI, formato). Cualquier regla de negocio, validación crítica o transformación de datos pertenece al controlador o a una capa de servicio.

## Nombres descriptivos

- Las variables, métodos y clases se nombran en inglés.
- El contenido de la UI (etiquetas, mensajes, botones) está en español.
- Los nombres reflejan el propósito, no la implementación.
- Las tablas en base de datos usan plural en inglés (Products, Sizes, Departments).

## Evitar duplicación de datos

Si un valor puede calcularse a partir de otros datos existentes, se calcula. No se almacenan totales que puedan derivarse de líneas de detalle, ni existencias que puedan calcularse desde movimientos de inventario.

## Convenciones generales

- **Archivos por clase**: Una clase por archivo.
- **File-scoped namespaces**: Usar `namespace X.Y;` en lugar de bloques con llaves.
- **Usings**: Dentro del archivo, después del namespace.
- **Nullable**: Habilitado a nivel de proyecto. Las propiedades de navegación requeridas usan `= null!;`.
- **Async**: Todos los métodos que acceden a base de datos son asíncronos.
- **Rutas**: Las rutas siguen el convenio RESTful de ASP.NET Core MVC.
