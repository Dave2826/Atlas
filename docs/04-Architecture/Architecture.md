# Atlas Architecture

## Overview

Atlas is being built as a business-focused retail management platform.

The objective is to create a system that remains simple for daily use while being capable of growing into a complete commercial solution in the future.

The architecture prioritizes maintainability, scalability, and clear separation of responsibilities.

---

## Architecture Style

Atlas uses the MVC (Model-View-Controller) pattern.

Responsibilities are divided into separate layers to keep business logic independent from the user interface.

Benefits:

* Easier maintenance.
* Cleaner code organization.
* Better scalability.
* Faster future development.

---

## Project Structure

Current structure:

```text
Controllers
Services
Repositories
DTOs
Data
Models
Views
wwwroot
```

Each layer has a specific responsibility and should avoid performing work that belongs to another layer.

---

## Controllers

Controllers handle incoming requests and coordinate application flow.

Responsibilities:

* Receive user actions.
* Validate requests.
* Call services.
* Return views or responses.

Controllers should remain lightweight and should not contain business rules.

---

## Services

Services contain business logic.

Examples:

* Sales processing.
* Layaway management.
* Voucher validation.
* Special order tracking.
* Cash session calculations.

Business decisions belong here.

---

## Repositories

Repositories handle data access.

Responsibilities:

* Read data.
* Store data.
* Update records.
* Remove records.

Repositories should not contain business logic.

---

## DTOs

Data Transfer Objects are used to move information between layers safely.

Benefits:

* Reduce coupling.
* Improve maintainability.
* Simplify validation.

---

## Models

Models represent the business domain.

Examples:

* Product
* Customer
* Sale
* Voucher
* Layaway

Models should represent real business concepts.

---

## Data Layer

The data layer manages database access and persistence configuration.

Future responsibilities:

* PostgreSQL configuration.
* Database migrations.
* Connection management.

---

## Database Strategy

Atlas will use PostgreSQL as its primary database engine.

Reasons:

* Free and open source.
* Reliable.
* Scalable.
* Well supported by .NET.
* Suitable for future multi-store deployments.

Automatic backups will be implemented as the project evolves.

---

## Security Principles

The system must protect sensitive business information.

Examples:

* Profit margins.
* Costs.
* Financial reports.
* Administrative functions.

Access to sensitive information should be controlled through roles and permissions.

---

## Scalability Strategy

Atlas starts with a single retail store.

Future growth may include:

* Multiple stores.
* Multiple users.
* Notifications.
* WhatsApp integration.
* Reporting modules.
* Artificial intelligence features.

The architecture should support these expansions without requiring a complete redesign.

---

## Development Philosophy

Business requirements come before technology decisions.

The project should always solve real operational problems before adding technical complexity.

Every feature must provide measurable value to daily store operations.

Technology is a tool.

The business problem is the priority.
