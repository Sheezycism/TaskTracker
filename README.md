# Task Tracker UI

## Environment Versions
- **.NET SDK:** 8.0+
- **Node.js:** v20.x+
- **Frontend Framework:** Angular 17+ (Standalone Components)

## Running the API (Backend)
1. Navigate to `/TaskTracker`.
2. Run `dotnet restore`.
3. Run `dotnet run`.
4. The API is available at: `https://localhost:7110` (or your local specific port).

## Running the SPA (Frontend)
1. Navigate to `/TaskTrackerUI`.
2. Run `npm install`.
3. Run `npm start` (or `ng serve`).
4. The UI is available at: `http://localhost:4200`.

## Configuration
The SPA connects to the API via `src/environments/environment.ts`. 
Ensure the `apiUrl` matches the backend port.
