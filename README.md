# restapi-Transport-HA

## Overview
<p>This backend application provides a REST API for managing a taxi company’s vehicle fleet and generating vehicle assignment suggestions for trips. </p>

### Features

- **Add a New Vehicle**
Allows administrators to add vehicles to the fleet, specifying passenger capacity, range, and fuel type (Gasoline, Hybrid, Electric).
- **Suggest Vehicles for a Trip**
Given a trip’s passenger count and distance, the API suggests suitable vehicles, calculating the expected profit for each option based on travel fees and refueling costs.
<p> The following endpoints were not part of the original assignment but serve to aid development and inspection during testing:</p>

-  **List All Vehicles**
Returns a read-only collection of all vehicles currently in the fleet.
- **Get Vehicle by ID**
Retrieves the details of a specific vehicle by its unique identifier.

### Technology Stack
- Language: C# 12
- Framework: .NET 8 (ASP.NET Core)
- Data Storage: In-memory database (Entity Framework Core)
- API Style: RESTful

## Project Structure
The solution is organized according to best practices for maintainability, clarity, and scalability. Below is an overview of the main components:

	Transport_HA/
	│
	├── Controllers/
	│   ├── VehicleController.cs         // Exposes REST API endpoints for vehicle management (add, list, retrieve by ID)
	│   └── SuggestionController.cs      // Exposes REST API endpoint for generating vehicle assignment suggestions
	│
	├── DTOs/
	│   ├── Vehicle.cs                   // Defines Vehicle, VehicleAdd records, and FuelType enum
	│   ├── Suggestion.cs                // Defines the Suggestion record (rank, profit, vehicle)
	│   └── Trip.cs                      // Defines the Trip record (passenger count, distance)
	│
	├── Services/
	│   ├── IVehicleService.cs           // Interface for vehicle-related operations
	│   ├── VehicleService.cs            // Implementation of vehicle management logic
	│   ├── ISuggestionService.cs        // Interface for suggestion-related operations
	│   └── SuggestionService.cs         // Implementation of business logic for trip suggestions
	│
	├── DAL/
	│   ├── Entities/
	│   │   └── DBVehicle.cs             // Entity model for vehicle persistence
	│   └── VehicleDbContext.cs          // Entity Framework Core database context
	│
	├── Program.cs                       // Application entry point, dependency injection, and database seeding
	└── ...                              // Additional configuration and support files as required

### Key Points:
- **Controllers:** Handle HTTP requests and route them to the appropriate services.
- **DTOs:** Define data contracts for API communication, ensuring clear and consistent data exchange.
- **Services:** Encapsulate business logic and data access, promoting separation of concerns and testability.
- **DAL (Data Access Layer):** Manages database entities and context, abstracting persistence details from business logic.
- **Program.cs:** Configures application startup, dependency injection, and initial data seeding.

## Business Logic
**Vehicle Addition**
-	Vehicles are added via the API and stored in the in-memory database.
-	Each vehicle has a defined capacity, range, and fuel type and a unique identifier handled by the database.

**Suggestion Generation**
-	Input: Number of passengers and distance.
-	Process:
	-	Filters vehicles that can accommodate the passenger count and complete the trip without refueling.
- Calculates profit for each vehicle:
-	Travel Fee:
	-	€2 × distance
	-	€2 × number of started half-hours
		-	**Note:** Distance is split into two segments based on the description: 
			-	**city:** 2 min/km for first 50 km, **highway:** 1 min/km after 50 km
-	Refueling Cost:
	-	Gasoline: €2/km
	-	Electric: €1/km
	-	Hybrid: €1/km (city), €2/km (highway)
		-	**Note:** Hybrid fuel consumption logic was not clearly defined in the original task, so the following interpretation was applied.
-	Ranks suggestions by profit (highest first).
-	Output: List of suggestions with rank, profit, and vehicle details.

### Rest call examples
####  POST `/vehicle` | Add a New Vehicle 
#### Request
```
POST /api/vehicle HTTP/1.1
Host: localhost:5000
Content-Type: application/json
```
#### Body
```
{
  "passengerCapacity": 6,
  "range": 500.0,
  "fuelType": "Electric"
}
```

#### POST `/suggestion` | Suggest Vehicles for a Trip
#### Request
```
POST /api/suggestion HTTP/1.1
Host: localhost:5000
Content-Type: application/json
```
#### Body
```
{
  "passengerCount": 3,
  "distance": 350.0
}
```

