# Community Issue Reporting Application

## Overview
This application allows community members to report issues in their local area. It features a responsive and user-friendly interface with three main functions (only "Report Issues" is currently implemented). Users can submit detailed reports including location information, issue categories, descriptions, and relevant media attachments.

## Features
- **Main Menu**
  - Three navigation options (only "Report Issues" is currently active)
  - Clean, modern interface with consistent color scheme
  - Responsive design that adapts to different screen sizes

- **Report Issues Form**
  - Location input field
  - Category selection dropdown (Sanitation, Roads, Utilities, Public Safety, Other)
  - Description text area with rich text capabilities
  - File attachment functionality for images and documents
  - Visual progress indicator with motivational feedback
  - Data validation to ensure complete submissions
  - Navigation with unsaved changes warning

- **User Engagement Features**
  - Dynamic progress tracking
  - Contextual feedback messages
  - Color-coded visual cues
  - Confirmation messages after successful submissions

## Application Screenshots
Here you can find screenshots of the application to help users understand the interface and functionality:

![Main Menu](./assets/Screenshot%202025-04-07%20214758.png)
![Main Menu](./assets/Screenshot%25202025-04-07%2520214758.png)

## Design Considerations
The application implements several key design principles:

- **Consistency**
  - Uniform color scheme throughout the application
  - Consistent button styles and layouts
  - Standardized fonts and text styles

- **Clarity**
  - Clear, descriptive labels and instructions
  - Logical form organization
  - Intuitive navigation

- **User Feedback**
  - Progress indicators
  - Validation messages
  - Success confirmations
  - Warning dialogs for potential data loss

- **Responsiveness**
  - Adaptable layouts for different screen sizes
  - Minimum window sizes to ensure content visibility
  - Proportional element sizing

## Requirements
- Windows operating system
- .NET Framework 4.5 or higher
- Visual Studio 2015 or newer (for development/compilation)
- Minimum 4GB RAM
- 100MB free disk space

## How to Compile the Application
1. Open Visual Studio
2. Create a new Windows Forms Application project (.NET Framework)
3. Copy the provided C# code into the appropriate files in your project
4. Ensure all required namespaces are included (System, System.Collections.Generic, System.Drawing, System.IO, System.Windows.Forms)
5. Build the solution by pressing F6 or selecting Build > Build Solution from the menu
6. The compiled executable will be available in the bin/Debug or bin/Release folder depending on your build configuration

## How to Run the Application
### From Visual Studio:
1. Open the solution in Visual Studio
2. Press F5 or click the Start button to run the application in debug mode

### As a Standalone Application:
1. Navigate to the bin/Debug or bin/Release folder in your project directory
2. Double-click the CommunityIssueReporter.exe file

## Using the Application

### Main Menu
When you start the application, you'll see the main menu with three options:
1. **Report Issues** - Active and ready to use
2. **Local Events and Announcements** - Disabled (coming soon)
3. **Service Request Status** - Disabled (coming soon)

Click on the "Report Issues" button to proceed to the reporting form.

### Reporting an Issue
1. **Enter Location**
   - Type the specific location where the issue is occurring
   - Be as precise as possible (e.g., "Corner of Main St. and Oak Ave." rather than just "Downtown")

2. **Select Category**
   - Choose the appropriate category from the dropdown menu
   - Options include: Sanitation, Roads, Utilities, Public Safety, and Other

3. **Provide Description**
   - Enter a detailed description of the issue
   - Include relevant details such as when the issue started, severity, impact, etc.
   - More detailed descriptions improve the progress indicator

4. **Attach Files (Optional)**
   - Click the "Attach Image/Document" button
   - Select relevant files such as photos of the issue or supporting documents
   - Supported file types include images (jpg, jpeg, png, gif, bmp) and documents (pdf, doc, docx)

5. **Track Your Progress**
   - Watch the progress bar to see how complete your report is
   - Read the feedback messages for guidance
   - A full progress bar indicates you've provided all necessary information

6. **Submit Your Report**
   - Click the "Submit Report" button when you've completed all necessary fields
   - Review the confirmation message
   - You'll be returned to the main menu after successful submission

7. **Return to Main Menu**
   - Click "Back to Main Menu" if you need to cancel your report
   - You'll receive a warning if you have unsaved changes

## Data Structure
The application uses two main data structures:

1. **Issue Class**
   - Properties: Location, Category, Description, AttachmentPath, ReportedTime
   - Captures all relevant information about a reported issue

2. **IssueRepository Class**
   - Static collection to store Issue objects
   - Methods to add and retrieve issues

Currently, the application stores reported issues in memory during runtime. When the application is closed, this data is lost. Future versions will implement persistent storage.

## Future Enhancements
- Implementation of "Local Events and Announcements" functionality
- Implementation of "Service Request Status" tracking
- Database integration for persistent data storage
- User accounts and authentication
- Mobile version of the application
- Administrative interface for issue management
- Issue status tracking and notifications
- Geographic mapping of reported issues
- Statistical reporting and analytics

## Troubleshooting
- **Application won't start:** Ensure you have the correct .NET Framework version installed
- **File attachments not working:** Check that you have the necessary permissions to access the selected files
- **Form elements appearing incorrectly:** Ensure your display resolution meets the minimum requirements
- **Button actions not responding:** Restart the application and try again

For any other issues, please contact support at [ST10463227@rcconnect.edu.za]
