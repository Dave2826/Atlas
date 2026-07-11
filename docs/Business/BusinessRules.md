# Reglas del Negocio

## Propósito

Este documento registra las reglas del negocio confirmadas para el sistema Atlas. Solo debe contener información validada con el cliente o el equipo del proyecto.

---

## Reglas Confirmadas

### Generales

- El sistema representa la operación diaria de **Joben's Moda**
- El sistema es de uso interno del personal de la tienda
- El acceso requiere autenticación mediante usuario y contraseña

### Usuarios y Roles

- Existen dos roles: **Administrator** y **Employee**
- El rol **Administrator** tiene acceso completo al sistema
- El rol **Employee** tiene acceso a las operaciones diarias
- Un usuario inactivo no puede iniciar sesión

### Catálogos Maestros

- Los catálogos maestros (Departamentos, Tipos de Producto) utilizan desactivación lógica mediante `IsActive`
- No existe eliminación física de registros en catálogos maestros
- El administrador puede crear, editar, activar y desactivar departamentos
- El administrador puede crear, editar, activar y desactivar tipos de producto
- Un catálogo desactivado conserva sus relaciones con productos existentes
- Los nombres de los catálogos deben ser únicos (comparación insensible a mayúsculas/minúsculas)

### Productos

- Un producto pertenece a un departamento y a un tipo de producto
- Un producto puede tener múltiples tallas con stock individual
- El SKU de un producto debe ser único

---

## Reglas Pendientes de Confirmación

- Política de precios (márgenes, descuentos)
- Manejo de devoluciones
- Límites de crédito para apartados
- Política de vencimiento de vales
- Horarios de corte de caja
- Permisos específicos por rol

---

## Notas

- Toda nueva regla del negocio debe documentarse aquí antes de ser implementada
- Las reglas pueden modificarse conforme evolucione el negocio
- Ninguna regla aquí listada debe asumirse sin confirmación explícita
