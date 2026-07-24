# ADR-000: Arquitectura inicial del proyecto

**Estado**: Aceptado

**Fecha**: 2026-06-12

## Contexto

Se necesita definir la arquitectura base para el sistema de gestión retail de Joben's Moda. El sistema debe ser funcional rápidamente, mantenible a largo plazo y capaz de evolucionar sin requerir reescrituras completas.

## Decisión

Se adopta la siguiente arquitectura inicial:

### MVC (Model-View-Controller)

**Razón**: Es el patrón predeterminado de ASP.NET Core para aplicaciones web con interfaz de usuario. Proporciona separación clara entre la lógica de presentación (Vistas), la lógica de control (Controllers) y los datos (Modelos). La curva de aprendizaje es baja y la comunidad de ASP.NET Core es extensa.

### Entity Framework Core + Fluent API

**Razón**: EF Core es el ORM oficial de Microsoft para .NET. Se elige Fluent API sobre Data Annotations para mantener las entidades del dominio limpias, sin acoplamiento a la infraestructura de persistencia. La configuración se organiza en clases separadas (`IEntityTypeConfiguration<T>`) que se registran automáticamente, facilitando el mantenimiento.

### PostgreSQL

**Razón**: Motor de base de datos relacional open source, maduro, con excelente soporte en .NET a través de Npgsql. Es gratuito, escalable y adecuado para despliegues futuros multi-tienda.

### Cookie Authentication

**Razón**: Esquema de autenticación estándar para aplicaciones web MVC. No requiere infraestructura externa (como OAuth providers) y es suficiente para un sistema de uso interno en una tienda.

### Bootstrap 5

**Razón**: Framework CSS ampliamente adoptado que proporciona componentes consistentes y responsive design sin requerir desarrollo frontend especializado. Acelera el desarrollo de la interfaz de usuario.

## Consecuencias

- Los controladores inyectan `DbContext` directamente durante la fase inicial
- Las carpetas `Services/`, `Repositories/` y `DTOs/` existen en el proyecto pero permanecen vacías
- La migración hacia una arquitectura en capas completa (Services + Repositories + DTOs) se realizará cuando el proyecto lo requiera
- Esta decisión permite avanzar rápidamente en la implementación de funcionalidades sin sobredimensionar la arquitectura desde el inicio

## Opciones Consideradas

- **Web API + SPA**: Se descartó por la complejidad adicional y porque no se requiere una interfaz rica en el cliente
- **Data Annotations**: Se descartó en favor de Fluent API para mantener las entidades del dominio desacopladas de la persistencia
- **JWT Authentication**: Se descartó por no ser necesaria para una aplicación web tradicional con sesiones de oficina
- **SQL Server**: Se descartó por el costo de licencias y porque PostgreSQL cumple todos los requisitos
