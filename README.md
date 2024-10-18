# Contract Monthly Claim System (CMCS)

## Project Overview
The Contract Monthly Claim System (CMCS) is a web-based application designed to streamline the process of submitting and managing monthly claims for independent contractor lecturers. This system provides an efficient way for lecturers to submit claims, and for coordinators and managers to review and approve them.

## Features
- User authentication (login functionality)
- Lecturer Dashboard
- Claim submission form
- View submitted claims
- Admin Dashboard for coordinators and managers
- Claim approval/rejection functionality

## Technology Stack
- ASP.NET Core MVC
- C#
- HTML/CSS/JavaScript
- Entity Framework Core (for future database implementation)

## Project Structure
- `Controllers/`: Contains the controller classes that handle HTTP requests
- `Models/`: Defines the data models used in the application
- `Views/`: Contains the CSHTML files that define the UI of the application
- `Services/`: Contains service classes for business logic
- `wwwroot/`: Stores static files like CSS, JavaScript, and images

## Setup and Running the Application
1. Clone the repository
2. Open the solution in Visual Studio
3. Restore NuGet packages
4. Build the solution
5. Run the application

## Key Components

### Models
- `ClaimModel`: Represents a claim in the system
- `ClaimSubmissionModel`: Used for the claim submission form
- `AdminDashboardModel`: Represents data for the admin dashboard

### Controllers
- `LecturerController`: Handles lecturer-related actions
- `AdminController`: Handles admin-related actions
- `AccountController`: Manages user authentication

### Services
- `ClaimService`: Manages claim-related operations

### Views
- `Lecturer/Dashboard.cshtml`: Lecturer's main dashboard
- `Lecturer/EnterClaimDetails.cshtml`: Claim submission form
- `Admin/Dashboard.cshtml`: Admin's main dashboard
- `Shared/SubmittedClaims.cshtml`: Displays list of submitted claims

## Current Limitations and Future Improvements
- Data is currently stored in-memory. Future versions will implement database storage.
- User authentication is simulated. Implement proper authentication and authorization.
- File upload functionality for supporting documents needs to be fully implemented.
- Improve error handling and user feedback.
- Implement more robust form validation.
- Add unit tests for controllers and services.