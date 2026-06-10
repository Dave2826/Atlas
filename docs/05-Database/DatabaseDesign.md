# Atlas Database Design

## Overview

The Atlas database is designed around real retail operations rather than generic point-of-sale workflows.

The primary objective is to accurately represent how stores like Joben's Moda manage inventory, layaways, vouchers, special orders, customer relationships, and cash operations.

The database must prioritize:

* Operational simplicity.
* Data traceability.
* Historical preservation.
* Scalability.
* Multi-store readiness for future versions.

---

# Core Domains

## Category

Stores product classifications.

Examples:

* Men's Clothing
* Women's Clothing
* Children's Clothing
* Footwear
* Accessories

Relationship:

* One Category can contain many Products.

---

## Product

Represents merchandise available for sale.

Main Attributes:

* Internal Code
* Name
* Category
* Cost Price
* Suggested Price
* Final Sale Price
* Size
* Color
* Stock Quantity
* Status

Product Status:

* Available
* Reserved
* Sold
* Lost
* Inactive

Important Rules:

* Final price is always controlled by the administrator.
* Inventory quantity is tracked per product.
* Products may belong to inventory batches.
* Products reserved by layaways are removed from available inventory.

---

## Customer

Represents a store customer.

Main Attributes:

* First Name
* Last Name
* Phone Number (Optional)
* Notes
* Customer Status

Relationship:

* One Customer can have many Layaways.
* One Customer can have many Vouchers.
* One Customer can have many Special Orders.
* One Customer can have many Sales.

---

# Sales Domain

## Sale

Represents a completed transaction.

Main Attributes:

* Sale Date
* Total Amount
* User
* Notes

Relationship:

* One Sale contains many Sale Details.

---

## Sale Detail

Stores products sold during a transaction.

Main Attributes:

* Product
* Quantity
* Unit Price
* Subtotal

Relationship:

* Belongs to one Sale.
* References one Product.

---

# Layaway Domain

## Layaway

Represents a customer layaway account.

Main Attributes:

* Customer
* Creation Date
* Status
* Total Amount
* Remaining Balance

Layaway Status:

* Active
* Completed
* Cancelled

Important Rules:

* One customer may have multiple layaways.
* Products may be delivered individually.
* Products may be cancelled individually.

---

## Layaway Item

Represents a single product inside a layaway.

Main Attributes:

* Product
* Original Price
* Paid Amount
* Remaining Amount
* Status

Item Status:

* Pending
* Partial
* Paid
* Delivered
* Cancelled
* Lost

Important Rules:

* Payments may be applied directly to an item.
* Items may be paid individually.
* Items may be delivered individually.
* Cancelled items return to available inventory.
* Lost items remain in historical records.

---

## Layaway Payment

Represents money applied to a layaway.

Main Attributes:

* Date
* Amount
* Payment Method
* Notes
* User

Payment Types:

* General Payment
* Item Payment

Important Rules:

* Payments may be applied to the full layaway.
* Payments may be applied to a specific item.
* Mixed payment methods are supported.

---

# Voucher Domain

## Voucher

Represents customer store credit.

Main Attributes:

* Customer
* Original Amount
* Remaining Amount
* Creation Date
* Status

Voucher Status:

* Active
* Partially Used
* Consumed
* Expired
* Cancelled

Important Rules:

* Vouchers belong to customers.
* Vouchers do not belong to layaways.
* Partial usage is allowed.
* Multiple vouchers may be combined.

---

## Voucher Transaction

Represents voucher usage history.

Main Attributes:

* Voucher
* Date
* Amount Used
* Reference Type
* Reference Id

Reference Types:

* Sale
* Layaway
* Manual Adjustment

Important Rules:

* Every voucher movement must be traceable.
* Voucher history is never deleted.

---

# Special Order Domain

## Special Order

Represents customer merchandise requests.

Main Attributes:

* Customer
* Requested Product
* Request Date
* Notes
* Status

Status:

* Pending
* Searching
* Found
* Waiting For Customer
* Completed
* Cancelled

Important Rules:

* Special Orders are independent from Layaways.
* Found products may be physically reserved.
* Unclaimed products may return to inventory.

---

# Cash Management Domain

## Cash Session

Represents a work shift.

Main Attributes:

* Opening Amount
* Closing Amount
* Open Date
* Close Date
* User

Important Rules:

* Every session must start with an opening balance.
* Every session must be closed.
* Historical sessions cannot be modified without authorization.

---

## Expense

Represents operational business expenses.

Examples:

* Rent
* Fuel
* Utilities
* Maintenance
* Supplies

Relationship:

* May belong to a Cash Session.

---

# Security Domain

## User

Represents a system user.

Relationship:

* One User has one Role.

---

## Role

Represents permission levels.

Initial Roles:

* Administrator
* Employee

Future Roles may be added as the business grows.
