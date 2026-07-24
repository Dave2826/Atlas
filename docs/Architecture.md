# Atlas Architecture Principles

## 1. Una única fuente de verdad por dato

Cada pieza de información del dominio debe almacenarse en un solo lugar. No deben existir copias redundantes que puedan derivar en inconsistencia. Si un dato puede calcularse a partir de otros, debe calcularse, no almacenarse.

## 2. El administrador mantiene el control

Atlas automatiza tareas repetitivas y agiliza la operación diaria, pero las decisiones críticas —como la creación de productos, la definición de catálogos y la configuración del sistema— son responsabilidad exclusiva del administrador. El sistema nunca tomará decisiones unilaterales que afecten la integridad del negocio.

## 3. Automatizar tareas repetitivas sin perder flexibilidad

Las operaciones rutinarias (cálculo de inventario, generación de reportes, validaciones de consistencia) deben automatizarse para reducir errores humanos y liberar tiempo operativo. Sin embargo, la automatización debe implementarse de forma que el administrador pueda intervenir cuando las condiciones del negocio lo requieran.

## 4. La lógica de negocio pertenece al backend

Las vistas contienen únicamente lógica de presentación. Las validaciones críticas, reglas de negocio y transformaciones de datos se ejecutan exclusivamente en el servidor. El frontend puede ofrecer validaciones de conveniencia (UX), pero nunca debe ser la única barrera de protección.

## 5. Los ViewModels desacoplan la UI de las entidades

Las vistas nunca trabajan directamente con entidades del dominio. Los ViewModels actúan como contratos explícitos entre el controlador y la vista, exponiendo únicamente los datos necesarios para la presentación y evitando exponer propiedades internas del modelo de datos.

## 6. No duplicar datos que puedan calcularse

Si un valor puede derivarse de otros datos existentes, no se almacena. Por ejemplo, el total de una venta se calcula a partir de sus líneas, y el stock disponible se calcula desde ProductSizeStock. Esta regla elimina la posibilidad de inconsistencias por desincronización.

## 7. Preferir simplicidad antes que complejidad

Las soluciones simples, legibles y directas son preferibles a abstracciones prematuras. El código se escribe primero para que lo entienda un ser humano. La optimización y la arquitectura en capas se introducen cuando el proyecto lo demanda, no por adelantado.

## 8. Los catálogos gobiernan el comportamiento del sistema

Los catálogos (Department, ProductType, Brand, Size) definen la estructura del negocio. El sistema se comporta en función de ellos. No existen valores mágicos ni configuraciones ocultas. Cualquier variación en el comportamiento debe reflejarse en los catálogos correspondientes.

## 9. Diseñar primero para el negocio y después para el código

Cada decisión técnica se evalúa primero contra las necesidades del negocio de Joben's Moda. El diseño de la base de datos, las relaciones entre entidades y el flujo de la interfaz se modelan a partir de la realidad operativa de la tienda, no de patrones académicos.
