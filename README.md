# 🔧 AutoFix - Advanced Auto Repair Service Platform

<div align="center">

![AutoFix Logo](https://img.shields.io/badge/AutoFix-Auto%20Repair%20Platform-blue?style=for-the-badge&logo=car&logoColor=white)

[![.NET](https://img.shields.io/badge/.NET-9.0-512BD4?style=for-the-badge&logo=dotnet)](https://dotnet.microsoft.com/)
[![MongoDB](https://img.shields.io/badge/MongoDB-Atlas-47A248?style=for-the-badge&logo=mongodb&logoColor=white)](https://www.mongodb.com/atlas)
[![ASP.NET Core](https://img.shields.io/badge/ASP.NET%20Core-MVC-512BD4?style=for-the-badge&logo=dotnet)](https://docs.microsoft.com/en-us/aspnet/core/)
[![Bootstrap](https://img.shields.io/badge/Bootstrap-5.3-7952B3?style=for-the-badge&logo=bootstrap&logoColor=white)](https://getbootstrap.com/)

*A modern, scalable auto repair service platform connecting vehicle owners with skilled mechanics*

---

</div>

## 📋 Table of Contents

- [🎯 Project Overview](#-project-overview)
- [🏗️ Architecture](#️-architecture)
- [🛠️ Technology Stack](#️-technology-stack)
- [📁 Project Structure](#-project-structure)
- [🗄️ Database Design](#️-database-design)
- [🔐 Authentication & Authorization](#-authentication--authorization)
- [🎨 User Interface](#-user-interface)
- [🚀 Key Features](#-key-features)
- [⚙️ Configuration](#️-configuration)
- [🔧 Installation](#-installation)
- [📝 API Documentation](#-api-documentation)
- [🧪 Testing](#-testing)
- [🚀 Deployment](#-deployment)
- [🤝 Contributing](#-contributing)

---

## 🎯 Project Overview

**AutoFix** is a comprehensive auto repair service platform built with **.NET 9.0** and **MongoDB Atlas**. The platform facilitates seamless connections between vehicle owners seeking repair services and qualified mechanics offering their expertise.

### 🌟 Core Objectives

```mermaid
graph TD
    A[Vehicle Owner] --> B[Service Request]
    B --> C[Platform Matching]
    C --> D[Qualified Mechanic]
    D --> E[Service Delivery]
    E --> F[Completion & Rating]
```

- **🎯 Service Matching**: Intelligent matching between service requests and mechanic capabilities
- **⚡ Real-time Updates**: Live order status tracking and notifications
- **🔒 Secure Transactions**: Encrypted data handling and secure user authentication
- **📱 Responsive Design**: Mobile-first responsive interface
- **📊 Analytics**: Comprehensive order tracking and performance metrics

---

## 🏗️ Architecture

### 🏛️ High-Level Architecture

```
┌─────────────────────────────────────────────────────────────┐
│                    Presentation Layer                       │
│  ┌─────────────┐ ┌─────────────┐ ┌─────────────────────┐   │
│  │   Razor     │ │  Bootstrap  │ │     JavaScript      │   │
│  │    Views    │ │   Styling   │ │    Interactions     │   │
│  └─────────────┘ └─────────────┘ └─────────────────────┘   │
└─────────────────────────────────────────────────────────────┘
                               │
┌─────────────────────────────────────────────────────────────┐
│                   Business Logic Layer                      │
│  ┌─────────────┐ ┌─────────────┐ ┌─────────────────────┐   │
│  │ Controllers │ │  Services   │ │    Middleware       │   │
│  │   (MVC)     │ │  (Business) │ │  (Authentication)   │   │
│  └─────────────┘ └─────────────┘ └─────────────────────┘   │
└─────────────────────────────────────────────────────────────┘
                               │
┌─────────────────────────────────────────────────────────────┐
│                    Data Access Layer                        │
│  ┌─────────────┐ ┌─────────────┐ ┌─────────────────────┐   │
│  │   Models    │ │  MongoDB    │ │     Repositories    │   │
│  │  (Entities) │ │   Driver    │ │   (Data Access)     │   │
│  └─────────────┘ └─────────────┘ └─────────────────────┘   │
└─────────────────────────────────────────────────────────────┘
                               │
┌─────────────────────────────────────────────────────────────┐
│                     Database Layer                          │
│              MongoDB Atlas Cloud Database                   │
└─────────────────────────────────────────────────────────────┘
```

### 🔄 Request Flow

```mermaid
sequenceDiagram
    participant User
    participant Controller
    participant Service
    participant MongoDB
    participant View

    User->>Controller: HTTP Request
    Controller->>Service: Business Logic
    Service->>MongoDB: Database Operation
    MongoDB-->>Service: Data Response
    Service-->>Controller: Processed Data
    Controller->>View: Model Data
    View-->>User: Rendered HTML
```

---

## 🛠️ Technology Stack

<table>
<tr>
<td width="50%">

### 🖥️ Backend Technologies
```
🔹 .NET 9.0 Framework
🔹 ASP.NET Core MVC
🔹 MongoDB.Driver 2.28.0
🔹 C# 12.0 Language Features
🔹 Cookie-based Authentication
🔹 Dependency Injection
🔹 Model Validation
🔹 Logging & Error Handling
```

</td>
<td width="50%">

### 🎨 Frontend Technologies
```
🔹 Razor View Engine
🔹 Bootstrap 5.3
🔹 Bootstrap Icons
🔹 jQuery 3.6
🔹 JavaScript ES6+
🔹 CSS3 & SCSS
🔹 Responsive Design
🔹 Progressive Enhancement
```

</td>
</tr>
</table>

### 🗄️ Database & Cloud
- **MongoDB Atlas** - Cloud-hosted NoSQL database
- **MongoDB.Driver** - Official .NET driver
- **BSON Serialization** - Document mapping
- **GridFS** - File storage (planned)

---

## 📁 Project Structure

```
AutoFix/
├── 📁 Controllers/              # MVC Controllers
│   ├── 🎮 AccountController.cs      # User authentication & profiles
│   ├── 🎮 HomeController.cs         # Main landing page
│   └── 🎮 OrdersController.cs       # Order management
├── 📁 Data/                    # Data Access Layer
│   └── 🗄️ MongoDbContext.cs         # MongoDB connection & configuration
├── 📁 Models/                  # Data Models
│   ├── 👤 ApplicationUser.cs        # Base user model
│   ├── 👤 Client.cs                 # Client-specific model
│   ├── 👤 Mechanic.cs               # Mechanic-specific model
│   ├── 📋 ClientOrder.cs            # Service request model
│   ├── 📋 MechanicOrder.cs          # Accepted order model
│   ├── ⚙️ MongoDbSettings.cs        # Database configuration
│   └── 🔄 OrderStatus.cs            # Order status enumeration
├── 📁 Services/                # Business Logic
│   ├── 🔐 IAuthService.cs           # Authentication interface
│   ├── 🔐 AuthService.cs            # Authentication implementation
│   ├── 📋 IOrderService.cs          # Order management interface
│   └── 📋 OrderService.cs           # Order management implementation
├── 📁 Views/                   # Razor Views
│   ├── 📁 Account/                  # Authentication views
│   │   ├── 🔐 Login.cshtml              # User login form
│   │   ├── 🔐 Register.cshtml           # User registration form
│   │   ├── 👤 ClientProfile.cshtml      # Client profile management
│   │   └── 👤 MechanicProfile.cshtml    # Mechanic profile management
│   ├── 📁 Home/                     # Landing pages
│   │   ├── 🏠 Index.cshtml              # Homepage
│   │   └── ❌ Error.cshtml              # Error handling
│   ├── 📁 Orders/                   # Order management views
│   │   ├── ➕ CreateOrder.cshtml        # Service request form
│   │   ├── 📋 MyOrders.cshtml           # Client order list
│   │   ├── 📋 PendingOrders.cshtml      # Available orders for mechanics
│   │   ├── 📋 MyAcceptedOrders.cshtml   # Mechanic's accepted orders
│   │   ├── 📄 Details.cshtml            # Order details view
│   │   └── 📁 Partials/
│   │       └── 📋 _OrdersList.cshtml        # Reusable order list component
│   └── 📁 Shared/                   # Shared layouts
│       ├── 🖼️ _Layout.cshtml             # Main layout template
│       ├── 🔗 _LoginPartial.cshtml       # Login status partial
│       └── ❌ Error.cshtml               # Global error page
├── 📁 wwwroot/                 # Static Files
│   ├── 🎨 css/                      # Stylesheets
│   ├── 📜 js/                       # JavaScript files
│   └── 🖼️ images/                   # Static images
├── ⚙️ Program.cs                # Application entry point
├── ⚙️ appsettings.json          # Configuration settings
└── 📦 AutoFix.csproj            # Project file
```

---

## 🗄️ Database Design

### 📊 MongoDB Collections

#### 👥 Users Collection (`Clients` & `Mechanics`)

```json
{
  "_id": "ObjectId",
  "FullName": "string",
  "UserName": "string",
  "Email": "string",
  "PasswordHash": "string",
  "PhoneNumber": "string?",
  "Role": "Client|Mechanic",
  "CreatedDate": "DateTime",
  "LastLoginDate": "DateTime?",
  // Mechanic-specific fields
  "Skills": ["string[]"],
  "Services": ["string[]"],
  "Bio": "string",
  "Rating": "double",
  "CompletedOrders": "int"
}
```

#### 📋 ClientOrders Collection

```json
{
  "_id": "ObjectId",
  "ClientId": "string",
  "ClientName": "string",
  "MechanicId": "string?",
  "MechanicName": "string?",
  "ServiceType": "string",
  "Description": "string",
  "Location": "string",
  "ScheduledTime": "DateTime?",
  "EstimatedPrice": "decimal",
  "Notes": "string",
  "Status": "Pending|Accepted|Completed|Declined|Cancelled",
  "OrderDate": "DateTime"
}
```

#### ✅ MechanicOrders Collection

```json
{
  "_id": "ObjectId",
  "OrderId": "string",
  "MechanicId": "string",
  "ClientId": "string",
  "ServiceDetails": "string",
  "AcceptedDate": "DateTime",
  "CompletedDate": "DateTime?",
  "Notes": "string",
  "Status": "Accepted|Completed"
}
```

### 🔗 Data Relationships

```mermaid
erDiagram
    CLIENT ||--o{ CLIENT_ORDER : creates
    MECHANIC ||--o{ MECHANIC_ORDER : accepts
    CLIENT_ORDER ||--o| MECHANIC_ORDER : "accepted as"
    
    CLIENT {
        ObjectId _id
        string FullName
        string Email
        string Role
    }
    
    MECHANIC {
        ObjectId _id
        string FullName
        string Email
        string Role
        array Skills
        array Services
    }
    
    CLIENT_ORDER {
        ObjectId _id
        string ClientId
        string MechanicId
        string ServiceType
        enum Status
    }
    
    MECHANIC_ORDER {
        ObjectId _id
        string OrderId
        string MechanicId
        DateTime AcceptedDate
    }
```

---

## 🔐 Authentication & Authorization

### 🛡️ Security Architecture

```csharp
// Cookie-based Authentication Flow
services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Account/Login";
        options.LogoutPath = "/Account/Logout";
        options.ExpireTimeSpan = TimeSpan.FromDays(30);
        options.SlidingExpiration = true;
    });
```

### 🔑 User Roles & Permissions

<table>
<tr>
<th width="20%">Role</th>
<th width="40%">Permissions</th>
<th width="40%">Restrictions</th>
</tr>
<tr>
<td><strong>🙋‍♂️ Client</strong></td>
<td>
• Create service requests<br>
• View own orders<br>
• Cancel pending orders<br>
• Update profile<br>
• Contact assigned mechanic
</td>
<td>
• Cannot view other clients' orders<br>
• Cannot accept/decline orders<br>
• Cannot access mechanic features
</td>
</tr>
<tr>
<td><strong>🔧 Mechanic</strong></td>
<td>
• View pending orders<br>
• Accept/decline orders<br>
• Mark orders complete<br>
• Update skills & services<br>
• View accepted orders
</td>
<td>
• Cannot create service requests<br>
• Cannot access client-specific features<br>
• Cannot modify other mechanics' orders
</td>
</tr>
</table>

### 🔒 Security Features

```csharp
// Password Hashing
string hashedPassword = BCrypt.Net.BCrypt.HashPassword(password);

// Role-based Authorization
[Authorize(Roles = "Mechanic")]
public async Task<IActionResult> PendingOrders() { }

// User Identity Claims
var claims = new List<Claim>
{
    new Claim(ClaimTypes.NameIdentifier, user.Id),
    new Claim(ClaimTypes.Name, user.UserName),
    new Claim(ClaimTypes.Role, user.Role)
};
```

---

## 🎨 User Interface

### 🎭 Design System

#### 🎨 Color Palette
```scss
// Primary Brand Colors
$primary: #0d6efd;      // Bootstrap Primary Blue
$success: #198754;      // Success Green
$warning: #ffc107;      // Warning Amber
$danger: #dc3545;       // Danger Red
$info: #0dcaf0;         // Info Cyan

// Status Colors
$pending: #ffc107;      // Yellow for pending orders
$accepted: #198754;     // Green for accepted orders
$completed: #0d6efd;    // Blue for completed orders
$cancelled: #6c757d;    // Gray for cancelled orders
```

#### 📱 Responsive Breakpoints
```scss
// Mobile First Design
$mobile: 576px;         // Small devices
$tablet: 768px;         // Medium devices  
$desktop: 992px;        // Large devices
$widescreen: 1200px;    // Extra large devices
```

### 🧩 UI Components

#### 📋 Order Status Badges
```html
<!-- Dynamic status badges with color coding -->
@switch (Model.Status)
{
    case OrderStatus.Pending:
        <span class="badge bg-warning">Pending</span>
        break;
    case OrderStatus.Accepted:
        <span class="badge bg-success">Accepted</span>
        break;
    case OrderStatus.Completed:
        <span class="badge bg-primary">Completed</span>
        break;
}
```

#### 📈 Timeline Component
```css
.timeline {
    position: relative;
    padding-left: 30px;
}

.timeline::before {
    content: '';
    position: absolute;
    left: 10px;
    top: 0;
    bottom: 0;
    width: 2px;
    background: #e9ecef;
}

.timeline-marker {
    position: absolute;
    left: -30px;
    width: 20px;
    height: 20px;
    border-radius: 50%;
}
```

---

## 🚀 Key Features

### 1. 👤 User Management

<details>
<summary><strong>🔍 Click to expand User Management features</strong></summary>

#### Registration System
- **Dual Role Registration**: Separate flows for clients and mechanics
- **Skills & Services Selection**: Mechanics can specify their expertise
- **Email Validation**: Unique email requirement
- **Username Validation**: Unique username requirement

#### Profile Management
- **Client Profiles**: Contact information and order history
- **Mechanic Profiles**: Skills, services, bio, and ratings
- **Profile Updates**: Real-time profile modification
- **Avatar Support**: (Planned feature)

```csharp
// Mechanic Registration with Skills
public async Task<ApplicationUser?> RegisterUserAsync(
    string fullName, string userName, string email, 
    string password, string role, 
    List<string>? skills = null, List<string>? services = null)
{
    if (role == "Mechanic")
    {
        var mechanic = new Mechanic
        {
            // Base properties...
            Skills = skills ?? new List<string>(),
            Services = services ?? new List<string>()
        };
        await _context.Mechanics.InsertOneAsync(mechanic);
        return mechanic;
    }
    // Client registration logic...
}
```

</details>

### 2. 📋 Order Management

<details>
<summary><strong>🔍 Click to expand Order Management features</strong></summary>

#### Service Request Lifecycle
```mermaid
stateDiagram-v2
    [*] --> Pending: Client creates request
    Pending --> Accepted: Mechanic accepts
    Pending --> Declined: Mechanic declines
    Pending --> Cancelled: Client cancels
    Accepted --> Completed: Mechanic completes
    Declined --> [*]
    Cancelled --> [*]
    Completed --> [*]
```

#### Advanced Features
- **Real-time Status Updates**: Live order tracking
- **Filtering & Search**: Orders by status, date, service type
- **Bulk Operations**: Multiple order management (planned)
- **Order Analytics**: Performance metrics (planned)

```csharp
// Order Status Management
public async Task<bool> AcceptOrderAsync(string orderId, string mechanicId, string? notes = null)
{
    var filter = Builders<ClientOrder>.Filter.Eq(o => o.Id, orderId);
    var update = Builders<ClientOrder>.Update
        .Set(o => o.Status, OrderStatus.Accepted)
        .Set(o => o.MechanicId, mechanicId)
        .Set(o => o.Notes, notes ?? string.Empty);
    
    var result = await _context.ClientOrders.UpdateOneAsync(filter, update);
    return result.ModifiedCount > 0;
}
```

</details>

### 3. 🔍 Search & Discovery

<details>
<summary><strong>🔍 Click to expand Search & Discovery features</strong></summary>

#### Smart Matching System
- **Skill-based Filtering**: Match mechanics to service requirements
- **Location-based Search**: Geographic proximity matching (planned)
- **Availability Scheduling**: Time-based matching (planned)
- **Rating-based Sorting**: Quality-driven recommendations (planned)

#### Search Capabilities
```csharp
// Advanced Order Filtering
public async Task<List<ClientOrder>> GetOrdersByFilterAsync(
    string? status = null, 
    string? serviceType = null, 
    DateTime? fromDate = null)
{
    var filterBuilder = Builders<ClientOrder>.Filter;
    var filters = new List<FilterDefinition<ClientOrder>>();
    
    if (!string.IsNullOrEmpty(status))
        filters.Add(filterBuilder.Eq(o => o.Status, Enum.Parse<OrderStatus>(status)));
    
    if (!string.IsNullOrEmpty(serviceType))
        filters.Add(filterBuilder.Eq(o => o.ServiceType, serviceType));
    
    var combinedFilter = filters.Any() 
        ? filterBuilder.And(filters) 
        : filterBuilder.Empty;
    
    return await _context.ClientOrders.Find(combinedFilter).ToListAsync();
}
```

</details>

### 4. 📊 Analytics & Reporting

<details>
<summary><strong>🔍 Click to expand Analytics features</strong></summary>

#### Performance Metrics
- **Order Completion Rates**: Success metrics
- **Response Times**: Service efficiency tracking
- **User Satisfaction**: Rating and review system (planned)
- **Revenue Tracking**: Financial analytics (planned)

#### Dashboard Components
```csharp
// Analytics Service (Planned)
public class AnalyticsService
{
    public async Task<OrderStatistics> GetOrderStatisticsAsync(string userId, string role)
    {
        return new OrderStatistics
        {
            TotalOrders = await GetTotalOrdersAsync(userId, role),
            CompletedOrders = await GetCompletedOrdersAsync(userId, role),
            AverageRating = await GetAverageRatingAsync(userId),
            ResponseTime = await GetAverageResponseTimeAsync(userId)
        };
    }
}
```

</details>

---

## ⚙️ Configuration

### 🔧 Application Settings

```json
// appsettings.json
{
  "MongoDbSettings": {
    "ConnectionString": "mongodb+srv://username:password@cluster.mongodb.net/",
    "DatabaseName": "AutoFixDB",
    "ClientsCollectionName": "Clients",
    "MechanicsCollectionName": "Mechanics", 
    "ClientOrdersCollectionName": "ClientOrders",
    "MechanicOrdersCollectionName": "MechanicOrders"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  }
}
```

### 🌐 Environment Configuration

```csharp
// Program.cs - Dependency Injection Configuration
builder.Services.Configure<MongoDbSettings>(
    builder.Configuration.GetSection("MongoDbSettings"));

builder.Services.AddSingleton<MongoDbContext>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IOrderService, OrderService>();

// Authentication Configuration
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Account/Login";
        options.AccessDeniedPath = "/Account/AccessDenied";
        options.ExpireTimeSpan = TimeSpan.FromDays(30);
        options.SlidingExpiration = true;
    });
```

---

## 🔧 Installation

### 📋 Prerequisites

```bash
# Required Software
✅ .NET 9.0 SDK or later
✅ Visual Studio 2022 / VS Code
✅ MongoDB Atlas Account
✅ Git (for version control)
```

### 🚀 Quick Start

```bash
# 1. Clone the repository
git clone https://github.com/yourusername/autofix.git
cd autofix

# 2. Restore NuGet packages
dotnet restore

# 3. Update connection string in appsettings.json
# Replace with your MongoDB Atlas connection string

# 4. Build the application
dotnet build

# 5. Run the application
dotnet run

# 6. Open browser to
# https://localhost:5001 or http://localhost:5000
```

### 🔧 Development Setup

```bash
# Install development tools
dotnet tool install --global dotnet-ef
dotnet tool install --global dotnet-aspnet-codegenerator

# Enable hot reload for development
dotnet watch run

# Run with specific environment
dotnet run --environment Development
```

### 🏗️ MongoDB Atlas Setup

1. **Create MongoDB Atlas Account**
   - Visit [MongoDB Atlas](https://www.mongodb.com/atlas)
   - Create free tier cluster

2. **Configure Database Access**
   ```bash
   # Whitelist IP addresses
   # Create database user with read/write permissions
   # Get connection string
   ```

3. **Update Configuration**
   ```json
   {
     "MongoDbSettings": {
       "ConnectionString": "your-mongodb-atlas-connection-string",
       "DatabaseName": "AutoFixDB"
     }
   }
   ```

---

## 📝 API Documentation

### 🔐 Authentication Endpoints

<table>
<tr><th>Endpoint</th><th>Method</th><th>Description</th><th>Parameters</th></tr>
<tr>
<td><code>/Account/Register</code></td>
<td>POST</td>
<td>Register new user</td>
<td>fullName, userName, email, password, confirmPassword, role, skills[], services[]</td>
</tr>
<tr>
<td><code>/Account/Login</code></td>
<td>POST</td>
<td>User authentication</td>
<td>usernameOrEmail, password</td>
</tr>
<tr>
<td><code>/Account/Logout</code></td>
<td>POST</td>
<td>User logout</td>
<td>-</td>
</tr>
<tr>
<td><code>/Account/Profile</code></td>
<td>GET</td>
<td>Get user profile</td>
<td>-</td>
</tr>
</table>

### 📋 Order Management Endpoints

<table>
<tr><th>Endpoint</th><th>Method</th><th>Description</th><th>Authorization</th></tr>
<tr>
<td><code>/Orders/CreateOrder</code></td>
<td>POST</td>
<td>Create service request</td>
<td>Client Role</td>
</tr>
<tr>
<td><code>/Orders/MyOrders</code></td>
<td>GET</td>
<td>Get user's orders</td>
<td>Authenticated</td>
</tr>
<tr>
<td><code>/Orders/PendingOrders</code></td>
<td>GET</td>
<td>Get available orders</td>
<td>Mechanic Role</td>
</tr>
<tr>
<td><code>/Orders/AcceptOrder</code></td>
<td>POST</td>
<td>Accept service request</td>
<td>Mechanic Role</td>
</tr>
<tr>
<td><code>/Orders/CompleteOrder</code></td>
<td>POST</td>
<td>Mark order complete</td>
<td>Mechanic Role</td>
</tr>
<tr>
<td><code>/Orders/CancelOrder</code></td>
<td>POST</td>
<td>Cancel pending order</td>
<td>Client Role</td>
</tr>
</table>

### 📤 Request/Response Examples

#### Register User Request
```json
POST /Account/Register
Content-Type: application/x-www-form-urlencoded

{
  "fullName": "John Doe",
  "userName": "johndoe",
  "email": "john@example.com",
  "password": "SecurePassword123!",
  "confirmPassword": "SecurePassword123!",
  "role": "Mechanic",
  "skills": ["Engine Repair", "Brake Service"],
  "services": ["Oil Change", "Diagnostics"]
}
```

#### Create Order Request
```json
POST /Orders/CreateOrder
Content-Type: application/x-www-form-urlencoded

{
  "serviceType": "Oil Change",
  "description": "Regular oil change for 2018 Honda Civic",
  "location": "123 Main St, City, State",
  "scheduledTime": "2024-01-15T10:00:00",
  "estimatedPrice": 45.00,
  "notes": "Synthetic oil preferred"
}
```

---

## 🧪 Testing

### 🔬 Test Structure

```
Tests/
├── 🧪 UnitTests/
│   ├── Services/
│   │   ├── AuthServiceTests.cs
│   │   └── OrderServiceTests.cs
│   └── Controllers/
│       ├── AccountControllerTests.cs
│       └── OrdersControllerTests.cs
├── 🔄 IntegrationTests/
│   ├── AuthenticationFlowTests.cs
│   └── OrderManagementTests.cs
└── 🌐 EndToEndTests/
    ├── UserRegistrationTests.cs
    └── OrderProcessingTests.cs
```

### 🧪 Unit Testing Examples

```csharp
// AuthService Unit Test
[Test]
public async Task RegisterUserAsync_WithValidData_ShouldCreateUser()
{
    // Arrange
    var authService = new AuthService(_mockContext.Object, _mockLogger.Object);
    var userData = new UserRegistrationData
    {
        FullName = "Test User",
        Email = "test@example.com",
        Password = "TestPassword123!"
    };

    // Act
    var result = await authService.RegisterUserAsync(
        userData.FullName, 
        userData.Email, 
        userData.Email, 
        userData.Password, 
        "Client"
    );

    // Assert
    Assert.That(result, Is.Not.Null);
    Assert.That(result.Email, Is.EqualTo(userData.Email));
}
```

### 🔄 Integration Testing

```csharp
// Order Management Integration Test
[Test]
public async Task CreateOrder_EndToEndFlow_ShouldCompleteSuccessfully()
{
    // Arrange
    var client = _factory.CreateClient();
    await AuthenticateAsClient(client);

    // Act
    var response = await client.PostAsync("/Orders/CreateOrder", orderData);

    // Assert
    Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Redirect));
    
    // Verify order was created in database
    var orders = await GetOrdersFromDatabase();
    Assert.That(orders.Count, Is.GreaterThan(0));
}
```

---

## 🚀 Deployment

### ☁️ Cloud Deployment Options

#### 🌐 Azure App Service
```yaml
# azure-pipelines.yml
trigger:
- main

pool:
  vmImage: 'ubuntu-latest'

steps:
- task: DotNetCoreCLI@2
  displayName: 'Restore packages'
  inputs:
    command: 'restore'
    projects: '**/*.csproj'

- task: DotNetCoreCLI@2
  displayName: 'Build application'
  inputs:
    command: 'build'
    projects: '**/*.csproj'
    arguments: '--configuration Release'

- task: DotNetCoreCLI@2
  displayName: 'Publish application'
  inputs:
    command: 'publish'
    projects: '**/*.csproj'
    arguments: '--configuration Release --output $(Build.ArtifactStagingDirectory)'

- task: AzureWebApp@1
  displayName: 'Deploy to Azure Web App'
  inputs:
    azureSubscription: 'your-subscription'
    appName: 'autofix-app'
    package: '$(Build.ArtifactStagingDirectory)/**/*.zip'
```

#### 🐳 Docker Deployment
```dockerfile
# Dockerfile
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src
COPY ["AutoFix.csproj", "."]
RUN dotnet restore "./AutoFix.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "AutoFix.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AutoFix.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AutoFix.dll"]
```

```yaml
# docker-compose.yml
version: '3.8'
services:
  autofix:
    build: .
    ports:
      - "8080:80"
      - "8443:443"
    environment:
      - ASPNETCORE_ENVIRONMENT=Production
      - MongoDbSettings__ConnectionString=${MONGODB_CONNECTION_STRING}
    volumes:
      - ./logs:/app/logs
```

### 🔧 Production Configuration

```json
// appsettings.Production.json
{
  "MongoDbSettings": {
    "ConnectionString": "${MONGODB_CONNECTION_STRING}",
    "DatabaseName": "AutoFixProd"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Warning",
      "AutoFix": "Information"
    }
  },
  "AllowedHosts": "autofix.yourdomain.com"
}
```

---

## 🤝 Contributing

### 🛠️ Development Workflow

```mermaid
gitgraph
    commit id: "Initial"
    branch feature/user-management
    checkout feature/user-management
    commit id: "Add user models"
    commit id: "Implement auth service"
    checkout main
    merge feature/user-management
    branch feature/order-system
    checkout feature/order-system
    commit id: "Add order models"
    commit id: "Implement order service"
    checkout main
    merge feature/order-system
```

### 📋 Contribution Guidelines

1. **🔍 Issue First**: Create an issue before working on features
2. **🌿 Branch Naming**: Use descriptive branch names (`feature/order-cancellation`)
3. **💬 Commit Messages**: Follow conventional commit format
4. **🧪 Testing**: Include tests for new features
5. **📚 Documentation**: Update README and code comments

### 🔄 Pull Request Process

```bash
# 1. Fork the repository
# 2. Create feature branch
git checkout -b feature/amazing-feature

# 3. Make changes and commit
git commit -m "feat: add amazing feature"

# 4. Push to branch
git push origin feature/amazing-feature

# 5. Open Pull Request with description
```

### 📝 Code Style Guidelines

```csharp
// C# Coding Standards
namespace AutoFix.Services
{
    /// <summary>
    /// Service for managing user authentication and authorization
    /// </summary>
    public class AuthService : IAuthService
    {
        private readonly MongoDbContext _context;
        private readonly ILogger<AuthService> _logger;

        public AuthService(MongoDbContext context, ILogger<AuthService> logger)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Registers a new user in the system
        /// </summary>
        /// <param name="fullName">User's full name</param>
        /// <param name="email">User's email address</param>
        /// <returns>Created user or null if registration failed</returns>
        public async Task<ApplicationUser?> RegisterUserAsync(string fullName, string email)
        {
            try
            {
                // Implementation here
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to register user with email {Email}", email);
                return null;
            }
        }
    }
}
```

---

## 📄 License

```
MIT License

Copyright (c) 2024 AutoFix Platform

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
```

---

## 📞 Support & Contact

<div align="center">

### 🚀 Ready to get started?

[![GitHub Issues](https://img.shields.io/badge/Issues-GitHub-red?style=for-the-badge&logo=github)](https://github.com/yourusername/autofix/issues)
[![Documentation](https://img.shields.io/badge/Docs-Wiki-blue?style=for-the-badge&logo=wikipedia)](https://github.com/yourusername/autofix/wiki)
[![Discussions](https://img.shields.io/badge/Discussions-GitHub-purple?style=for-the-badge&logo=github)](https://github.com/yourusername/autofix/discussions)

### 💖 Show your support

Give a ⭐️ if this project helped you!

---

**Built with ❤️ by the AutoFix Team**

</div>