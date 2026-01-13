# Task Manager Application

A full-stack Task Manager application built with ASP.NET Core Web API backend and Angular frontend.

## Project Structure

```
ToDoApp/
├── ToDoApp/                    # ASP.NET Core Web API Backend
│   ├── Models/                 # Entity models (TaskItem, ToDoItem, TaskStatus)
│   ├── Data/                   # DbContext configuration
│   ├── Repositories/           # Repository pattern implementation
│   ├── Services/               # Business logic layer
│   ├── Controllers/            # API endpoints
│   └── DTOs/                   # Data transfer objects
│
└── ToDoUi/ToDoClient/          # Angular Frontend
    ├── src/app/
    │   ├── models/             # TypeScript models
    │   ├── services/           # HTTP services
    │   └── components/         # Angular components
    │       ├── task-list/      # Task list with filtering
    │       ├── task-detail/    # Task details with subtasks
    │       └── task-form/      # Create/Edit task form
    └── src/styles.css          # Global styles with custom palette
```

## Features

### Backend (ASP.NET Core Web API)
- **Clean Architecture**: Repository and Service patterns
- **Entity Framework Core**: Code-first approach with in-memory database
- **RESTful API**: Full CRUD operations for Tasks and ToDo items
- **CORS Support**: Configured for Angular frontend

### Frontend (Angular)
- **Modern Angular**: Standalone components with TypeScript
- **Routing**: Navigation between views
- **HTTP Client**: API integration
- **Responsive Design**: Mobile and desktop support
- **Color-Coded Status**: Visual task status indicators

### Task Features
- Create, read, update, and delete tasks
- Add multiple subtasks (ToDo items) per task
- Filter tasks by status (New, InProgress, Completed)
- Track completion percentage
- Set due dates for tasks and subtasks
- Visual progress indicators

## Color Palette

- **Primary (#4A628A)**: Dark blue for headings and primary buttons
- **Secondary (#7AB2D3)**: Medium blue for accents and progress
- **Tertiary (#B9E5E8)**: Light blue for borders and secondary elements
- **Background (#DFF2EB)**: Very light blue-green for page background

## Typography

- **Font Family**: BBH Hegarty from Google Fonts
- **Weights**: 400 (Regular), 500 (Medium), 600 (Semi-bold), 700 (Bold)

## Getting Started

### Prerequisites

- .NET 8.0 SDK
- Node.js (v18 or later)
- npm or yarn

### Backend Setup

1. Navigate to the backend directory:
   ```powershell
   cd ToDoApp
   ```

2. Restore NuGet packages:
   ```powershell
   dotnet restore
   ```

3. Run the backend:
   ```powershell
   dotnet run
   ```

   The API will be available at `http://localhost:5000` (or `https://localhost:5001`)

### Frontend Setup

1. Navigate to the frontend directory:
   ```powershell
   cd ToDoUi\ToDoClient
   ```

2. Install dependencies:
   ```powershell
   npm install
   ```

3. Update API URL if needed:
   - Open `src/app/services/task.service.ts`
   - Update `apiUrl` to match your backend URL (default: `http://localhost:5000/api/tasks`)

4. Start the development server:
   ```powershell
   npm start
   ```

   The application will be available at `http://localhost:4200`

## API Endpoints

### Tasks
- `GET /api/tasks` - Get all tasks
- `GET /api/tasks?status={status}` - Filter tasks by status
- `GET /api/tasks/{id}` - Get task by ID
- `POST /api/tasks` - Create new task
- `PUT /api/tasks/{id}` - Update task
- `DELETE /api/tasks/{id}` - Delete task

### ToDos (Subtasks)
- `GET /api/todos` - Get all todos
- `GET /api/todos?taskId={taskId}` - Get todos for specific task
- `GET /api/todos/{id}` - Get todo by ID
- `POST /api/todos` - Create new todo
- `PUT /api/todos/{id}` - Update todo
- `DELETE /api/todos/{id}` - Delete todo

## Data Models

### TaskItem
- Id (int)
- Title (string, required)
- Description (string)
- DueDate (DateTime?)
- Status (enum: New, InProgress, Completed)
- ToDoItems (collection)

### ToDoItem
- Id (int)
- TaskId (int, foreign key)
- Description (string, required)
- DueDate (DateTime?)
- Status (enum: New, InProgress, Completed)

## Development Notes

### Backend
- Uses in-memory database (data is lost on restart)
- To use SQL Server, update `Program.cs`:
  ```csharp
  options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
  ```

### Frontend
- Built with standalone components (Angular 19+)
- Uses HttpClient for API communication
- Implements reactive forms with two-way data binding

## Future Enhancements

- User authentication and authorization
- Persistent database (SQL Server, PostgreSQL)
- Task priority levels
- Task categories/tags
- Search functionality
- Sorting options
- Export tasks to PDF/CSV
- Email notifications for due dates
- Drag-and-drop task reordering

## License

This project is for educational purposes.
