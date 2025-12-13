Task Management System
https://tmsapi.danielsplaygrounds.com/swagger/index.html

Create and run Inventory and Sales Report Tasks

1. InventoryTasksBuilderController - Supervisor Roles only

- [ ] `/api/v1/tasks/procurement` - Create a new procurement task : Create a request for new items to be created at a specific location
- [ ] `/api/v1/tasks/transfer` - Create a new procurement task : Create a request for existing items to be moved to a new location

2. ReportsTasksBuilderController - Supervisor Roles only

- [ ] `/api/v1/reports/inventory` - Create a new inventory type report task. reportMode 1 - List , reportMode 2 - Summary
- [ ] `/api/v1/reports/sales` - Create a new sales type report task. reportMode 1 - List , reportMode 2 - Summary


3. StoresOperationsController - Clerk and Supervisor Roles

- [ ] `/api/v1/stores/create_register` - Create a new cash register and assigne it to a existing location
- [ ] `/api/v1/stores/open_session` - Open a new session for an existing register and assigne a Clerk 
- [ ] `/api/v1/stores/create_cart` - Create a new cart for a existing client
- [ ] `/api/v1/stores/addto_cart/{cartId}` - Add items to a existing cart
- [ ] `/api/v1/stores/pay_cart/{cartId}` - Pay for existing cart


4. TasksOperationsController

- [ ] `/api/v1/operations/procurements/{taskID}` - Supply a list of items requested by a fulfilment task
- [ ] `/api/v1/operations/transfers/{taskID}` - Supply a list of items requested by a transfer task
- [ ] `/api/v1/operations/reports/{reportResultsTaskId}` - Get the results of a report as CSV
- [ ] `/api/v1/operations/reports/{reportTaskId}` - Run an existing Report Task
