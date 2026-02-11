# Project Setup Guide

This guide will help you get the project running locally using **.NET 10**, **Docker**, and **Aspire CLI**.

---

## Prerequisites

Before you begin, ensure the following tools are installed:

1. **.NET 10 SDK**  
   Download and install from: [https://dotnet.microsoft.com/en-us/download/dotnet/10.0](https://dotnet.microsoft.com/en-us/download/dotnet/10.0)

2. **Docker Desktop**  
   Install Docker for your operating system following the official guide: [https://docs.docker.com/get-docker/](https://docs.docker.com/get-docker/)

   > **Note:** Ensure Docker is open and running in the background before proceeding.

3. **Aspire CLI**  
   Install Aspire CLI using your terminal:

   **macOS / Linux:**
   ```bash
   curl -sSL https://aspire.dev/install.sh | bash
   ```

   **Windows (PowerShell):**
   ```powershell
   irm https://aspire.dev/install.ps1 | iex
   ```

4. **Entity Framework Core Tools**  
   Ensure your EF Core tools are up to date:
   ```bash
   dotnet tool update --global dotnet-ef
   ```

---

## Verify Installation

Confirm that Aspire CLI is installed correctly:
```bash
aspire --version
```

---

## App Configuration

The current `appsettings.json` configuration for database operations is as follows:

```json
"Database": {
  "Drop": true,
  "Migrate": true,
  "Seed": true
}
```

> **Note:** This will drop and seed the database on the first run.

---

## Running the Application

1. Navigate to the root of the project and run:
   ```bash
   aspire run
   ```
2. After running, a **Dashboard link** will be provided in the terminal. Click the link to open the application in your browser.
3. To run the frontend, click the link on the linegit  **Web Resource** in the terminal.

---

## Post-Setup Configuration

Once the application is running for the first time, update your `appsettings.json` to prevent dropping and reseeding the database on every start:

```json
"Database": {
"Drop": false,
"Migrate": false,
"Seed": false
}
```
