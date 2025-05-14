# ASP.NET Core Maintenance Mode Example

This is a simple example of an **ASP.NET Core** application that allows toggling between **production mode** and **maintenance mode** using a **Console application** for easy enabling and disabling of maintenance.

## What does this solution solve?

- Enables toggling between **production mode** and **maintenance mode** for your application.
- When in **maintenance mode**, the app will respond with **status 503** and show a maintenance notice for all visitors to the API (JSON responses).
- It allows restricting access to the app and API during maintenance for updates or fixes.
- Provides an easy-to-use **command-line tool** for managing the maintenance mode status.

## How to set up and run the application

### 1. Clone the repository

```bash
git clone https://github.com/your-username/aspnetcore-maintenance-mode-example.git
cd aspnetcore-maintenance-mode-example
```

### 2. Restore dependencies

Make sure you have the **.NET SDK** (version 8.0 or higher) installed. To restore the dependencies, run:

```bash
dotnet restore
```

### 3. Run the application

Run the application and check that it works without errors:

```bash
dotnet run --project WebApp
```

This will start the web application, which can be accessed in your browser at `http://localhost:5000`.

### 4. Toggle the application into maintenance mode

To enable maintenance mode, use the console tool `MaintenanceManager`:

```bash
dotnet run --project MaintenanceManager down --message "The application is currently under maintenance."
```

### 5. Toggle the application back to production mode

To disable maintenance mode:

```bash
dotnet run --project MaintenanceManager up
```

### 6. Check the status

Once the application is in maintenance mode, accessing the website or API will return a 503 response with the following JSON:

```json
{
    "message": "The application is currently under maintenance."
}
```

## Project Structure

- **WebApp**: The core ASP.NET Core web application that includes middleware to check for maintenance mode.
- **MaintenanceManager**: A console application that makes it easy to toggle between maintenance mode and production mode.
- **MaintenanceDemo.sln**: Visual Studio Solution that contains both projects.

## Recommendations

- It is recommended to use a **CI/CD pipeline** to automate maintenance mode toggling during updates or deployments.
- Use **environment variables** for more dynamic handling of application modes (e.g., `ASPNETCORE_ENVIRONMENT`).

---

You now have an easy **maintenance mode** mechanism for your ASP.NET Core application! 🚀
