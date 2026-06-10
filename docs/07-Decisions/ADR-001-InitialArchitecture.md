# ADR-001 - Initial Architecture Decisions

## Status

Accepted

---

## Context

Atlas is being developed as a retail management platform based on real operational processes observed in small family-owned businesses.

Several architectural decisions were required before development could begin.

This document records those decisions and the reasons behind them.

---

## Decision 1 - MVC Architecture

Atlas will use the ASP.NET MVC pattern.

Reason:

* Clear separation of responsibilities.
* Easier maintenance.
* Familiar development model.
* Suitable for future growth.

---

## Decision 2 - PostgreSQL Database

PostgreSQL was selected as the primary database engine.

Reason:

* Open source.
* Reliable.
* Scalable.
* Excellent support in .NET.

---

## Decision 3 - Service Layer

Business rules will be implemented in Services.

Reason:

* Keeps Controllers lightweight.
* Centralizes business logic.
* Improves maintainability.

---

## Decision 4 - Repository Pattern

Database access will be isolated through Repositories.

Reason:

* Better separation of concerns.
* Easier testing.
* Easier future database changes.

---

## Decision 5 - Customer-Centered Operations

Customers are the center of business operations.

Reason:

* Layaways belong to customers.
* Vouchers belong to customers.
* Special orders belong to customers.

This reflects real-world store workflows.

---

## Decision 6 - Inventory Reservation

Products are reserved immediately when added to a layaway.

Reason:

* Matches store operations.
* Prevents accidental sales.
* Maintains inventory accuracy.

---

## Decision 7 - Voucher Independence

Vouchers belong to customers, not to layaways.

Reason:

* Customers may use vouchers in sales, layaways, or future purchases.
* Matches current business behavior.

---

## Decision 8 - Historical Traceability

Atlas will never delete important business transactions.

Reason:

* Lost products.
* Voucher history.
* Layaway history.
* Sales history.

All operational history must remain traceable.

---

## Consequences

The architecture becomes slightly more complex but significantly more flexible and aligned with real business operations.

Future development should follow these decisions unless a new ADR explicitly replaces them.
