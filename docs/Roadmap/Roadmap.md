# Roadmap General de Atlas

---

## Fase 0 — Base (Completada)

**Objetivo**: Establecer la infraestructura base del proyecto.

- [x] Creación del proyecto ASP.NET Core MVC (.NET 10.0)
- [x] Configuración de PostgreSQL + Entity Framework Core
- [x] Migración inicial con todas las tablas del dominio
- [x] Autenticación mediante cookies + BCrypt
- [x] Seed de roles y usuario administrador
- [x] Estructura de documentación

---

## Fase 1 — Catálogos Maestros (Completada)

**Objetivo**: Implementar los catálogos base que sustentan los módulos transaccionales.

- [x] Login y autenticación
- [x] Dashboard con resumen de indicadores
- [x] CRUD de Productos
- [x] CRUD de Departamentos (plantilla de catálogo)
- [x] CRUD de Tipos de Producto

### Pendientes de Fase 1
- [ ] CRUD de Brands
- [ ] CRUD de Sizes
- [ ] CRUD de Suppliers

---

## Fase 2 — Clientes y Operación

**Objetivo**: Implementar los módulos transaccionales esenciales.

- [ ] CRUD de Clientes
- [ ] Módulo de Caja (apertura y cierre de sesiones)
- [ ] Módulo de Gastos
- [ ] Módulo de Ventas
- [ ] Módulo de Apartados
- [ ] Módulo de Vales

---

## Fase 3 — Reportes y Consultas

**Objetivo**: Proveer visibilidad operativa y toma de decisiones.

- [ ] Reportes de ventas
- [ ] Historial de clientes
- [ ] Resumen de apartados
- [ ] Resumen de vales
- [ ] Alertas de inventario bajo
- [ ] Reportes imprimibles

---

## Fase 4 — Comunicación

**Objetivo**: Mejorar la comunicación con los clientes.

- [ ] Integración con WhatsApp
- [ ] Recordatorios de apartados
- [ ] Notificaciones de pedidos especiales
- [ ] Notificaciones de vencimiento de vales

---

## Fase 5 — Inteligencia de Negocio

**Objetivo**: Proveer información para la toma de decisiones estratégicas.

- [ ] Dashboards de ventas
- [ ] Análisis de rentabilidad por producto
- [ ] Reportes de actividad de clientes
- [ ] Desempeño por departamento
- [ ] Proyecciones y tendencias

---

## Fase 6 — Multi-Tienda

**Objetivo**: Soportar operaciones desde múltiples ubicaciones.

- [ ] Gestión de tiendas
- [ ] Inventario compartido
- [ ] Transferencias entre tiendas
- [ ] Reportes multi-tienda
- [ ] Administración centralizada

---

## Fase 7 — Producto Comercial

**Objetivo**: Preparar Atlas para su uso por otras empresas.

- [ ] Asistente de instalación
- [ ] Herramientas de configuración por negocio
- [ ] Personalización de marca
- [ ] Gestión de respaldos
- [ ] Onboarding de usuarios

---

## Arquitectura

- [ ] Implementar capa de Services
- [ ] Implementar capa de Repositories
- [ ] Implementar DTOs
- [ ] Agregar paginación en listados
- [ ] Incorporar ILogger en lugar de Console.WriteLine
- [ ] Implementar ViewModels para vistas
- [ ] Middleware de errores global con logging estructurado

---

## Calidad

- [ ] Proyecto de tests unitarios
- [ ] Proyecto de tests de integración
- [ ] Análisis estático de código
- [ ] CI/CD pipeline

---

## Escalabilidad

- [ ] Caché distribuida
- [ ] Optimización de consultas
- [ ] Índices adicionales en BD
- [ ] Auditoría de datos (CreatedBy/UpdatedBy)

---

## Futuro

- [ ] Asistente con inteligencia artificial
- [ ] Recomendaciones de compra
- [ ] Pronóstico de demanda
- [ ] Sugerencias automáticas de inventario
- [ ] Aplicación móvil
- [ ] Sincronización en la nube
- [ ] Soporte de códigos de barras
- [ ] Impresión de etiquetas
