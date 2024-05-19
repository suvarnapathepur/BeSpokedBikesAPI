# BeSpoked Bikes Sales Tracking Application - Backend

This is the backend part of the BeSpoked Bikes sales tracking application, built using ASP.NET Core Web API.

## Setup Instructions

1. **Clone the Repository**:   git clone <repository_url>

2. **Database Setup**:
- Ensure you have SQL Server installed and running.
- Update the database connection string in `appsettings.json` with your SQL Server credentials.

3. **Run Migrations**:
Navigate to the project directory and run:
dotnet ef database update

4. **Run the Application**:
Run the application using the following command:dotnet run

5. **API Documentation**:
Swagger UI is available for API documentation and testing at `https://localhost:<port>/swagger/index.html`.

## Features

- - **Entities**: Define entities for Products, Salespersons, Customers, Sales, and Discounts.
- **Data Layer**: Implement a data layer with Entity Framework Core and Dapper for data access.
- Middle tier allowing client access to the data layer
- RESTful API endpoints for CRUD operations on entities
- **CRUD Operations**: Implement CRUD operations for managing Products, Salespersons, Customers, Sales, and Discounts.
- Data seeding with sample data for testing

## Technologies Used

- ASP.NET Core 6.0
- Entity Framework Core: ORM for database interactions.
- Dapper: Micro-ORM for additional data access scenarios.
- SQL Server: Database management system for storing application data.
- Swagger UI: API documentation and testing tool.

## API Endpoints

- **Products**: `/api/products`
- **Salespersons**: `/api/salespersons`
- **Customers**: `/api/customers`
- **Sales**: `/api/sales`
- **Discounts**: `/api/discounts`

## Additional Notes

- Ensure the frontend server is running and accessible for consuming the backend APIs.
- Update CORS settings in `Startup.cs` if frontend is hosted on a different domain.
- Customize the database connection string, CORS policy, and other configurations in `appsettings.json` and `Startup.cs` as needed.
- Add authentication and authorization mechanisms if required for securing API endpoints.
- Implement logging and error handling to track and manage application errors.
- Ensure adequate testing of API endpoints using unit tests, integration tests, and API contract testing.
- Document API usage and deployment processes for future reference.

## Additional Notes

- Ensure the frontend server is running and accessible for consuming the backend APIs.
- Update CORS settings in `Startup.cs` if frontend is hosted on a different domain.
