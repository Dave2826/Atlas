# Domain Definitions

## Category

Represents a product classification used to organize inventory.

Categories are fully configurable by administrators because product trends and merchandise types may change over time.

Examples:

* Men's T-Shirts
* Women's Blouses
* Children's Clothing
* Dresses
* School Uniforms
* Accessories
* Footwear

Responsibilities:

* Organize products.
* Facilitate inventory searches.
* Simplify stock reports.

---

## Product

Represents an item available for sale, layaway, exchange, special order, or inventory tracking.

A product belongs to one category and contains operational information used by the store.

Examples:

* Naruto T-Shirt
* Women's Floral Dress
* Children's Polo Shirt

Responsibilities:

* Store purchase cost.
* Store suggested sale price.
* Store final sale price selected by the administrator.
* Track stock quantity.
* Identify merchandise batches.
* Support sales, layaways, exchanges and special orders.

Important Business Rules:

* The system may calculate a suggested sale price automatically.
* The administrator always decides the final sale price.
* Products can be moved to offers or clearance sales.
* Products may belong to inventory batches.

---

## Customer

Represents a person who purchases products, creates layaways, receives vouchers, or places special orders.

Customer information should remain simple to avoid slowing down daily operations.

Required:

* First Name
* Last Name

Optional:

* Phone Number
* Notes

Responsibilities:

* Own layaways.
* Receive vouchers.
* Place special orders.
* Build purchase history.

Important Business Rules:

* Sales may be registered without a customer.
* Multiple customers may have similar names.
* Phone numbers are optional.
* Frequent customer status is managed manually by the administrator.

---

## Sale

Represents a completed commercial transaction.

A sale may contain one or many products.

Responsibilities:

* Register sold products.
* Register payment methods.
* Update inventory.
* Generate business metrics.

Supported Payment Methods:

* Cash
* Card
* Voucher
* Mixed Payment

Examples:

* $500 cash + $50 card
* $50 voucher + $100 cash

Important Business Rules:

* Multiple payment methods can be combined.
* Payment details must be recorded separately.
* Inventory must be updated immediately after completion.

---

## Sale Detail

Represents each product included within a sale.

Responsibilities:

* Link products to sales.
* Store quantity sold.
* Store sale price at the moment of the transaction.
* Preserve historical information.
