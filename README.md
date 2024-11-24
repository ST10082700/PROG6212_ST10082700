# Contract Monthly Claim System (CMCS)

## Project Overview
The Contract Monthly Claim System (CMCS) is a web-based application designed to streamline the process of submitting and managing monthly claims for independent contractor lecturers. This system provides an efficient way for lecturers to submit claims, and for coordinators and managers to review and approve them.

## Login Credentials
All users share the same password for this demo/assignment: `8UXPOXwtDm1x1TrQ`

Available users:
- Lecturer: lecturer@iievarsitycollege.com
- Admin: admin@iievarsitycollege.com  
- HR: hr@iievarsitycollege.com
- Coordinator: coordinator@iievarsitycollege.com

## Features
- User authentication with role-based access
- Lecturer Dashboard
  - Submit new claims
  - View claim history and status
  - Upload supporting documents
- Admin/HR/Coordinator Dashboard 
  - Review pending claims
  - Approve/reject claims with comments
  - View claim analytics and reports
- File Management
  - Secure file upload for supporting documents
  - File type restrictions (.pdf, .doc, .docx, .xls, .xlsx)
  - 5MB file size limit
- Claim Processing
  - Automated invoice number generation
  - Claim status tracking (Pending, Approved, Rejected)
  - Validation of claim details
  - Transaction support for approval/rejection

## Technology Stack
- ASP.NET Core MVC
- C#
- Entity Framework Core with Supabase
- HTML/CSS/JavaScript

## Project Structure
- `Controllers/`: MVC controllers
- `Models/`: Data models and view models
- `Views/`: Razor views
- `Services/`: Business logic and data access
- `wwwroot/`: Static assets
- `Data/`: Database context and migrations

## Recent Updates
- Added file upload validation and security
- Implemented proper error handling
- Added claim amount calculation
- Enhanced data validation
- Added transaction support for claim processing
- Improved performance with async operations
- Added secure file storage and cleanup
- Enhanced security features

## Setup Instructions
1. Clone the repository
2. Configure connection string for Supabase in appsettings.json
3. Restore NuGet packages
4. Run database migrations
5. Build and run the application

## Security Features
- Secure file handling
- Input validation
- Role-based authorization
- Transaction support
- Secure password storage

## Future Improvements
- Enhanced reporting features
- Email notifications
- Mobile responsiveness improvements
- Additional file format support
- Claim templates
- Batch processing capabilities

## Contributing
This is a school assignment project. Contributions should follow the assignment guidelines.

## License
For educational purposes only.