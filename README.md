# Car Rental Management

Car Rental Management is a backend REST API built with ASP.NET Core and designed using Clean Architecture principles.

The system is designed to manage the complete lifecycle of a car rental business, including vehicles, customers, reservations, rental agreements, billing, payments, maintenance, inspections, and authentication.

## Technologies

- ASP.NET Core
- C#
- Entity Framework Core
- SQL Server
- MediatR
- FluentValidation
- Clean Architecture
- CQRS
- RESTful APIs
- JWT Authentication
- Git

## Project Structure

- `Api` - API endpoints, requests, responses, filters, middleware, and HTTP concerns
- `Application` - Use cases, commands, queries, validation, abstractions, and application logic
- `Domain` - Domain entities, enums, and business rules
- `Infrastructure` - Database access, Entity Framework Core configurations, and external infrastructure implementations

## Features

### Authentication & Authorization

- User Authentication
- User Management
- Roles
- User Roles
- JWT Authentication
- Refresh Tokens
- Role-Based Authorization

### Branch Management

- Branches
- Branch Employees
- Vehicle Assignment by Branch

### Customer Management

- Customers
- Customer Information
- Customer Accounts
- Customer Rental History

### Employee Management

- Employees
- Employee Branch Assignment
- Rental Checkout Employees
- Rental Check-in Employees

### Vehicle Management

- Vehicle Makes
- Vehicle Models
- Vehicle Body Types
- Vehicle Categories
- Vehicles
- Vehicle Features
- Vehicle Images
- Vehicle Status Management
- Vehicle Availability
- Vehicle Branch Assignment

### Reservation Management

- Reservations
- Reservation Status Management
- Vehicle Availability Checking
- Vehicle Category Selection
- Vehicle Assignment
- Pickup and Return Scheduling
- Reservation Cancellation
- Reservation Conversion to Rental

### Rental Extras

- Extras
- Extra Pricing
- Per-Day Pricing
- Per-Rental Pricing
- Reservation Extras
- Extra Quantity Management
- Extra Price Snapshots

### Rental Agreement Management

- Rental Agreements
- Rental Number Generation
- Rental Checkout
- Rental Check-in
- Pickup Information
- Return Information
- Odometer Tracking
- Fuel Level Tracking
- Daily Rate Snapshots
- Deposit Management
- Rental Status Management

### Vehicle Inspection Management

- Vehicle Inspections
- Checkout Inspections
- Check-in Inspections
- Vehicle Condition Tracking

### Damage Management

- Damage Reports
- Damage Images
- Damage Status Tracking
- Repair Tracking
- Damage Waiver Management

### Maintenance Management

- Vehicle Maintenance
- Maintenance Records
- Scheduled Maintenance
- Maintenance Start
- Maintenance Completion
- Vehicle Maintenance Status

### Invoice Management

- Invoices
- Invoice Items
- Invoice Status Management
- Draft Invoices
- Issued Invoices
- Partially Paid Invoices
- Paid Invoices
- Voided Invoices

### Payment Management

- Payments
- Deposits
- Rental Charges
- Additional Charges
- Refunds
- Payment Status Tracking
- Payment Methods
- Transaction References
- Payment Failure Tracking

### Audit & Logging

- Audit Logs
- Create Operations Tracking
- Update Operations Tracking
- Delete Operations Tracking
- Login Tracking
- Logout Tracking
- Status Change Tracking
- Old and New Value Tracking
- IP Address Tracking
- User Agent Tracking

### Validation & Error Handling

- FluentValidation
- MediatR Validation Pipeline
- Result Pattern
- Feature-Specific Errors
- Global Exception Handling
- Problem Details Responses
- HTTP Status Code Mapping

## Architecture

The project follows Clean Architecture with separation between:

- Domain
- Application
- Infrastructure
- API

The Application layer uses CQRS with MediatR to separate commands and queries.

Example:

```text
Application
└── Features
    └── VehicleMakes
        ├── Commands
        │   └── CreateVehicleMake
        │       ├── CreateVehicleMakeCommand.cs
        │       ├── CreateVehicleMakeCommandHandler.cs
        │       └── CreateVehicleMakeCommandValidator.cs
        ├── Queries
        ├── DTOs
        └── Errors
