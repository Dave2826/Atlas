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

---

## Layaway

Represents a customer reservation agreement for one or more products.

A layaway allows customers to reserve merchandise through partial payments until the remaining balance is completed.

Responsibilities:

* Reserve products.
* Track customer payments.
* Track remaining balance.
* Prevent reserved products from being sold.
* Manage expiration dates.

Important Business Rules:

* A layaway belongs to one customer.
* A layaway may contain one or many products.
* Multiple payments may be registered.
* Products remain unavailable for general sale while reserved.
* Expired layaways may generate vouchers according to business policy.
* The administrator may override expiration rules when necessary.
* Additional products may be added to an existing customer account when authorized.

---

## Voucher

Represents store credit assigned to a customer.

Vouchers are used instead of cash refunds whenever possible and may be consumed partially or completely in future transactions.

Responsibilities:

* Store customer credit.
* Track voucher balance.
* Support sales and layaway payments.
* Preserve transaction history.

Possible Origins:

* Product exchanges.
* Expired layaways.
* Customer service situations.
* Manual administrative adjustments.

Important Business Rules:

* A voucher belongs to one customer.
* Multiple vouchers may exist for the same customer.
* Multiple vouchers may be combined in a transaction.
* Vouchers may be used partially.
* Voucher balance must always be traceable.
* Expiration policies are controlled by the business.

---

## Special Order

Represents a product request made by a customer when the requested item is not currently available.

Responsibilities:

* Store requested product information.
* Track search attempts.
* Track order status.
* Link arriving products to waiting customers.

Statuses:

* Pending
* Searching
* Found
* Arrived
* Completed
* Cancelled

Important Business Rules:

* Customer phone number is optional but recommended.
* Products found for a customer must be identified before entering general inventory.
* Search attempts should be tracked.
* Unclaimed merchandise may return to general inventory according to store policy.
* Special orders may generate customer notifications in future versions.

---

## Cash Session

Represents the opening and closing cycle of a cash register.

Responsibilities:

* Track opening balance.
* Track cash movements.
* Track closing balance.
* Support daily reconciliation.

Important Business Rules:

* Every session has an opening amount.
* Every session has a closing amount.
* Cash and card transactions must be separated.
* Voucher usage must be recorded separately.
* Expenses registered during the session must remain traceable.
* Historical cash sessions cannot be modified after closure without authorization.

---

## Expense

Represents an operational business expense.

Examples:

* Rent
* Fuel
* Food
* Supplies
* Maintenance
* Utilities

Responsibilities:

* Record operational costs.
* Support profitability analysis.
* Maintain expense history.

Important Business Rules:

* Expenses do not affect inventory.
* Expenses are linked to cash sessions when applicable.
* Sensitive financial information should be restricted to authorized users.

---

## User

Represents a person authorized to access Atlas.

Responsibilities:

* Authenticate into the system.
* Perform permitted operations.
* Generate activity history.

Important Business Rules:

* Every user must have a role.
* User actions should be traceable.
* Sensitive actions may require additional authorization.
* Disabled users cannot access the system.

---

## Role

Represents a permission profile assigned to users.

Initial Roles:

* Administrator
* Employee

Administrator Capabilities:

* Full access.
* Financial visibility.
* User management.
* Configuration management.
* Discount authorization.
* Exception handling.

Employee Capabilities:

* Register sales.
* Register layaways.
* Search customers.
* Register product arrivals.
* View operational information.

Restrictions:

* No access to profits.
* No access to sensitive financial reports.
* No access to user administration.
* No access to advanced system configuration.

---

## Future Domain Considerations

The following domains are planned but not part of the initial implementation:

* Notifications
* WhatsApp Integration
* Artificial Intelligence Assistant
* Multi-Store Management
* Customer Loyalty Programs
* Advanced Analytics
* Automated Purchasing Suggestions
* Cloud Synchronization

