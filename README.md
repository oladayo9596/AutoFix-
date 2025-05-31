# 🚗 AutoFix - Professional Auto Repair Platform

<div align="center">

![AutoFix Logo](https://img.shields.io/badge/AutoFix-Professional%20Auto%20Repair-blue?style=for-the-badge&logo=car&logoColor=white)

[![.NET](https://img.shields.io/badge/.NET-9.0-purple?style=flat-square&logo=dotnet)](https://dotnet.microsoft.com/)
[![MongoDB](https://img.shields.io/badge/MongoDB-Database-green?style=flat-square&logo=mongodb)](https://www.mongodb.com/)
[![Bootstrap](https://img.shields.io/badge/Bootstrap-5.3-purple?style=flat-square&logo=bootstrap)](https://getbootstrap.com/)
[![License](https://img.shields.io/badge/License-MIT-yellow?style=flat-square)](LICENSE)
[![Status](https://img.shields.io/badge/Status-Active%20Development-brightgreen?style=flat-square)]()

*Connecting vehicle owners with certified mechanics through a modern, intuitive platform*

[🚀 Quick Start](#-quick-start) • [📋 Features](#-features) • [🏗️ Architecture](#%EF%B8%8F-architecture) • [📖 Documentation](#-documentation) • [🤝 Contributing](#-contributing)


</div>

---

## 📊 Project Overview

**AutoFix** is a comprehensive automotive service platform built with **ASP.NET Core 9.0** and **MongoDB**, designed to revolutionize how vehicle owners connect with professional mechanics. The platform provides a seamless experience for service requests, mechanic matching, and service management.

### 🎯 Core Mission
> *To bridge the gap between vehicle owners seeking reliable automotive services and certified mechanics looking to grow their business through a technology-driven, user-centric platform.*

---

## ✨ Features

<table>
<tr>
<td width="50%">

### 👥 **User Management**
- 🔐 **Dual Authentication System**
  - Client registration and login
  - Mechanic registration and verification
  - Role-based authorization
  - Secure session management

### 🛠️ **Service Requests**
- 📝 **Smart Order Creation**
  - Detailed service descriptions
  - Location-based matching
  - Photo uploads for diagnostics
  - Estimated pricing input

</td>
<td width="50%">

### 🔧 **Mechanic Portal**
- 📋 **Order Management Dashboard**
  - View pending requests
  - Accept/decline orders
  - Track service progress
  - Customer communication

### 📱 **Modern UI/UX**
- 🎨 **Responsive Design**
  - Mobile-first approach
  - Bootstrap 5.3 framework
  - Custom CSS animations
  - AOS (Animate On Scroll) effects

</td>
</tr>
</table>

---

## 🏗️ Architecture

### 📐 **Technical Stack**

```mermaid
graph TB
    A[ASP.NET Core 9.0] --> B[MongoDB Database]
    A --> C[Bootstrap 5.3 UI]
    A --> D[Entity Framework Core]
    B --> E[MongoDbContext]
    C --> F[Custom CSS/JS]
    D --> G[Identity Framework]
    A --> H[Service Layer]
    H --> I[AuthService]
    H --> J[OrderService]
```

### 🗂️ **Project Structure**

<details>
<summary><b>📁 Click to expand detailed structure</b></summary>

```
AutoFix/
├── 🎮 Controllers/           # MVC Controllers
│   ├── AccountController.cs  # User authentication & profiles
│   ├── HomeController.cs     # Landing page & main navigation
│   ├── OrdersController.cs   # Service request management
│   └── DiagnosticController.cs # System diagnostics
│
├── 💾 Data/                  # Database Context
│   └── MongoDbContext.cs     # MongoDB configuration
│
├── 📊 Models/                # Data Models
│   ├── ApplicationUser.cs    # Base user model
│   ├── Client.cs            # Client-specific properties
│   ├── Mechanic.cs          # Mechanic-specific properties
│   ├── ClientOrder.cs       # Service order model
│   └── MechanicOrder.cs     # Mechanic order view model
│
├── 🔧 Services/              # Business Logic Layer
│   ├── AuthService.cs       # Authentication services
│   └── OrderService.cs      # Order management services
│
├── 🎨 Views/                 # Razor Views
│   ├── Home/                # Landing page views
│   ├── Account/             # User account views
│   ├── Orders/              # Order management views
│   └── Shared/              # Shared components
│
├── 🌐 wwwroot/               # Static Assets
│   ├── css/                 # Stylesheets
│   ├── js/                  # JavaScript files
│   └── lib/                 # Third-party libraries
│
└── ⚙️ Configuration Files
    ├── Program.cs           # Application entry point
    ├── appsettings.json     # Configuration settings
    └── AutoFix.csproj       # Project dependencies
```

</details>

---

## 🚀 Quick Start

### 📋 **Prerequisites**

<table>
<tr>
<td align="center">
<img src="https://img.shields.io/badge/.NET-9.0-purple?style=for-the-badge&logo=dotnet" alt=".NET 9.0"/>
<br><strong>.NET 9.0 SDK</strong>
</td>
<td align="center">
<img src="https://img.shields.io/badge/MongoDB-6.0+-green?style=for-the-badge&logo=mongodb" alt="MongoDB"/>
<br><strong>MongoDB 6.0+</strong>
</td>
<td align="center">
<img src="https://img.shields.io/badge/Visual_Studio-2022-purple?style=for-the-badge&logo=visual-studio" alt="Visual Studio"/>
<br><strong>Visual Studio 2022</strong>
</td>
</tr>
</table>

### ⚡ **Installation Steps**

```bash
# 1. Clone the repository
git clone https://github.com/yourusername/autofix.git
cd autofix

# 2. Restore NuGet packages
dotnet restore

# 3. Update database connection string in appsettings.json
# MongoDB connection string should point to your instance

# 4. Build the application
dotnet build

# 5. Run the application
dotnet run
```

### 🌐 **Access Points**

| **Service** | **URL** | **Description** |
|-------------|---------|-----------------|
| 🏠 **Main Application** | `https://localhost:7139` | Primary web interface |
| 🔒 **Admin Panel** | `https://localhost:7139/Account` | User management |
| 📊 **Orders Dashboard** | `https://localhost:7139/Orders` | Service requests |

---

## 🏛️ Detailed Architecture

### 🎯 **Core Components**

#### 🔐 **Authentication System**
```csharp
// Multi-role authentication with ASP.NET Identity
services.AddDefaultIdentity<ApplicationUser>(options => {
    options.SignIn.RequireConfirmedAccount = false;
    options.Password.RequireDigit = true;
    options.Password.RequiredLength = 6;
})
.AddRoles<IdentityRole>()
.AddEntityFrameworkStores<ApplicationDbContext>();
```

#### 💾 **Database Layer**
```javascript
// MongoDB Configuration
{
  "ConnectionStrings": {
    "MongoDb": "mongodb://localhost:27017/AutoFixDB"
  },
  "DatabaseSettings": {
    "UsersCollectionName": "Users",
    "OrdersCollectionName": "Orders"
  }
}
```

#### 🛠️ **Service Layer Architecture**

<details>
<summary><b>🔧 AuthService Implementation</b></summary>

**Key Responsibilities:**
- ✅ User registration and authentication
- 🔄 Profile management for clients and mechanics
- 🛡️ Role-based authorization
- 📝 User data validation and sanitization

**Core Methods:**
```csharp
Task<IdentityResult> RegisterUserAsync(RegisterViewModel model)
Task<SignInResult> LoginUserAsync(LoginViewModel model)
Task UpdateClientProfileAsync(string userId, ClientProfileViewModel model)
Task UpdateMechanicProfileAsync(string userId, MechanicProfileViewModel model)
```

</details>

<details>
<summary><b>📋 OrderService Implementation</b></summary>

**Key Responsibilities:**
- 🆕 Service request creation and management
- 🔍 Order filtering and search functionality
- 📊 Status tracking and updates
- 📱 Real-time notifications

**Core Methods:**
```csharp
Task<ClientOrder> CreateOrderAsync(ClientOrder order)
Task<List<ClientOrder>> GetOrdersByUserAsync(string userId)
Task<bool> AcceptOrderAsync(string orderId, string mechanicId)
Task<bool> UpdateOrderStatusAsync(string orderId, OrderStatus status)
```

</details>

---

## 🎨 Frontend Architecture

### 📱 **Responsive Design System**

The frontend utilizes a mobile-first approach with Bootstrap 5.3 and custom CSS:

```scss
// Design System Variables
:root {
  --primary-gradient: linear-gradient(135deg, #667eea 0%, #764ba2 100%);
  --success-color: #28a745;
  --warning-color: #ffc107;
  --danger-color: #dc3545;
  --border-radius: 1rem;
  --box-shadow: 0 0.5rem 1rem rgba(0, 0, 0, 0.15);
}
```

### 🎭 **Component Library**

<table>
<tr>
<td width="33%">

#### 🏠 **Landing Page**
- Hero section with gradient background
- Feature showcase cards
- Service portfolio display
- Customer testimonials
- Call-to-action sections

</td>
<td width="33%">

#### 📋 **Order Management**
- Advanced filtering system
- Status-based organization
- Real-time search functionality
- Interactive order cards
- Progress tracking

</td>
<td width="33%">

#### 👤 **User Profiles**
- Tabbed interface design
- Form validation
- Image upload capability
- Activity tracking
- Settings management

</td>
</tr>
</table>

---

## 🗄️ Database Schema

### 📊 **Data Models Overview**

```mermaid
erDiagram
    ApplicationUser ||--o{ Client : extends
    ApplicationUser ||--o{ Mechanic : extends
    Client ||--o{ ClientOrder : creates
    Mechanic ||--o{ ClientOrder : accepts
    
    ApplicationUser {
        string Id PK
        string UserName
        string Email
        string PhoneNumber
        DateTime CreatedAt
        string Role
    }
    
    Client {
        string UserId PK
        string Address
        Dictionary VehicleInformation
        DateTime LastServiceDate
    }
    
    Mechanic {
        string UserId PK
        string Address
        List Skills
        List Services
        string Bio
        decimal Rating
        int CompletedJobs
    }
    
    ClientOrder {
        string Id PK
        string ClientId FK
        string MechanicId FK
        string ServiceType
        string Description
        DateTime ScheduledTime
        string Location
        decimal EstimatedPrice
        OrderStatus Status
        DateTime CreatedAt
    }
```

### 🔗 **Relationships**

| **Entity** | **Relationship** | **Description** |
|------------|------------------|-----------------|
| `ApplicationUser` → `Client` | One-to-One | User profile extension for clients |
| `ApplicationUser` → `Mechanic` | One-to-One | User profile extension for mechanics |
| `Client` → `ClientOrder` | One-to-Many | Client can create multiple service requests |
| `Mechanic` → `ClientOrder` | One-to-Many | Mechanic can accept multiple orders |

---

## 🔧 API Reference

### 🏠 **HomeController**

<details>
<summary><b>📍 Endpoints</b></summary>

| **Method** | **Route** | **Description** | **Auth Required** |
|------------|-----------|-----------------|-------------------|
| `GET` | `/` | Landing page | ❌ |
| `GET` | `/Home/Privacy` | Privacy policy | ❌ |
| `GET` | `/Home/Error` | Error page | ❌ |

</details>

### 👤 **AccountController**

<details>
<summary><b>📍 Endpoints</b></summary>

| **Method** | **Route** | **Description** | **Auth Required** |
|------------|-----------|-----------------|-------------------|
| `GET` | `/Account/Register` | Registration form | ❌ |
| `POST` | `/Account/Register` | Process registration | ❌ |
| `GET` | `/Account/Login` | Login form | ❌ |
| `POST` | `/Account/Login` | Process login | ❌ |
| `GET` | `/Account/Profile` | User profile dashboard | ✅ |
| `POST` | `/Account/UpdateProfile` | Update profile information | ✅ |
| `POST` | `/Account/Logout` | User logout | ✅ |

</details>

### 📋 **OrdersController**

<details>
<summary><b>📍 Endpoints</b></summary>

| **Method** | **Route** | **Description** | **Auth Required** | **Role** |
|------------|-----------|-----------------|-------------------|----------|
| `GET` | `/Orders/CreateOrder` | New service request form | ✅ | Client |
| `POST` | `/Orders/CreateOrder` | Submit service request | ✅ | Client |
| `GET` | `/Orders/MyOrders` | Client's orders list | ✅ | Client |
| `GET` | `/Orders/PendingOrders` | Available requests | ✅ | Mechanic |
| `GET` | `/Orders/MyAcceptedOrders` | Mechanic's accepted orders | ✅ | Mechanic |
| `POST` | `/Orders/AcceptOrder/{id}` | Accept service request | ✅ | Mechanic |
| `POST` | `/Orders/CompleteOrder/{id}` | Mark order complete | ✅ | Mechanic |
| `GET` | `/Orders/Details/{id}` | Order details view | ✅ | Both |

</details>

---

## 🎯 Business Logic

### 🔄 **Order Lifecycle**

```mermaid
stateDiagram-v2
    [*] --> Pending : Client creates order
    Pending --> Accepted : Mechanic accepts
    Pending --> Declined : Mechanic declines
    Pending --> Cancelled : Client cancels
    Accepted --> InProgress : Work begins
    InProgress --> Completed : Service finished
    Completed --> Rated : Client provides rating
    Declined --> [*]
    Cancelled --> [*]
    Rated --> [*]
```

### 🏆 **User Roles & Permissions**

<table>
<tr>
<th>🎭 Role</th>
<th>✅ Permissions</th>
<th>❌ Restrictions</th>
</tr>
<tr>
<td><strong>Client</strong></td>
<td>
• Create service requests<br>
• View own orders<br>
• Update profile<br>
• Rate mechanics<br>
• Cancel pending orders
</td>
<td>
• Cannot view other clients' orders<br>
• Cannot accept service requests<br>
• Cannot access mechanic dashboard
</td>
</tr>
<tr>
<td><strong>Mechanic</strong></td>
<td>
• View pending orders<br>
• Accept/decline requests<br>
• Update order status<br>
• Manage profile & skills<br>
• View accepted orders
</td>
<td>
• Cannot create service requests<br>
• Cannot view client-specific data<br>
• Cannot modify other mechanics' orders
</td>
</tr>
</table>

---

## 🛡️ Security Features

### 🔒 **Authentication & Authorization**

```csharp
// Role-based authorization implementation
[Authorize(Roles = "Client")]
public class ClientOrdersController : Controller { }

[Authorize(Roles = "Mechanic")]
public class MechanicDashboardController : Controller { }

// Custom authorization policies
services.AddAuthorization(options =>
{
    options.AddPolicy("ClientOnly", policy => 
        policy.RequireRole("Client"));
    options.AddPolicy("MechanicOnly", policy => 
        policy.RequireRole("Mechanic"));
});
```

### 🛡️ **Data Protection**

- 🔐 **Password Hashing**: BCrypt with salt
- 🍪 **Secure Cookies**: HttpOnly, Secure, SameSite
- 🔒 **HTTPS Enforcement**: Redirect HTTP to HTTPS
- 🛡️ **CSRF Protection**: Anti-forgery tokens
- 📝 **Input Validation**: Server-side validation for all forms

---

## 📊 Performance Optimization

### ⚡ **Frontend Optimizations**

<table>
<tr>
<td width="50%">

#### 🎨 **CSS Optimizations**
- Minified stylesheets
- Critical CSS inlining
- Font optimization
- Image compression
- Lazy loading implementation

</td>
<td width="50%">

#### 📜 **JavaScript Optimizations**
- Bundle minification
- Async script loading
- Event delegation
- Debounced search inputs
- Efficient DOM manipulation

</td>
</tr>
</table>

### 🗄️ **Backend Optimizations**

```csharp
// Database query optimization examples
public async Task<List<ClientOrder>> GetOrdersAsync(string userId, int page = 1)
{
    return await _context.Orders
        .Where(o => o.ClientId == userId)
        .OrderByDescending(o => o.CreatedAt)
        .Skip((page - 1) * 10)
        .Take(10)
        .ToListAsync();
}

// Caching implementation
[ResponseCache(Duration = 300, Location = ResponseCacheLocation.Any)]
public IActionResult GetMechanics()
{
    // Cached for 5 minutes
}
```

---

## 🧪 Testing Strategy

### 🔬 **Testing Pyramid**

```
    🔺 E2E Tests (Selenium)
   🔺🔺 Integration Tests (ASP.NET Core)
  🔺🔺🔺 Unit Tests (xUnit, Moq)
```

### 📋 **Test Coverage Areas**

- ✅ **Unit Tests**: Service layer methods, validation logic
- ✅ **Integration Tests**: Controller actions, database operations
- ✅ **UI Tests**: Form submissions, navigation flows
- ✅ **Performance Tests**: Load testing, stress testing

---

## 🚀 Deployment

### 🐳 **Docker Configuration**

<details>
<summary><b>📦 Dockerfile</b></summary>

```dockerfile
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src
COPY ["AutoFix.csproj", "."]
RUN dotnet restore "AutoFix.csproj"
COPY . .
WORKDIR "/src"
RUN dotnet build "AutoFix.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AutoFix.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AutoFix.dll"]
```

</details>

### ☁️ **Cloud Deployment Options**

<table>
<tr>
<td align="center">
<img src="https://img.shields.io/badge/Azure-App%20Service-blue?style=for-the-badge&logo=microsoft-azure" alt="Azure"/>
<br><strong>Azure App Service</strong>
<br>Recommended for production
</td>
<td align="center">
<img src="https://img.shields.io/badge/AWS-Elastic%20Beanstalk-orange?style=for-the-badge&logo=amazon-aws" alt="AWS"/>
<br><strong>AWS Elastic Beanstalk</strong>
<br>Auto-scaling capabilities
</td>
<td align="center">
<img src="https://img.shields.io/badge/Docker-Container-blue?style=for-the-badge&logo=docker" alt="Docker"/>
<br><strong>Docker Containers</strong>
<br>Platform independent
</td>
</tr>
</table>

---

## 📈 Monitoring & Analytics

### 📊 **Application Insights**

```csharp
// Application monitoring setup
services.AddApplicationInsightsTelemetry();

// Custom telemetry tracking
public void TrackOrderCreation(string orderId, string serviceType)
{
    _telemetryClient.TrackEvent("OrderCreated", new Dictionary<string, string>
    {
        {"OrderId", orderId},
        {"ServiceType", serviceType},
        {"Timestamp", DateTime.UtcNow.ToString()}
    });
}
```

### 📋 **Key Metrics**

- 👥 **User Engagement**: Registration rates, login frequency
- 📊 **Order Metrics**: Creation rates, completion times
- 🔧 **Mechanic Performance**: Response times, acceptance rates
- 🚀 **System Performance**: Page load times, error rates

---

## 🛠️ Development Guidelines

### 📝 **Code Standards**

<table>
<tr>
<td width="50%">

#### 🎯 **C# Guidelines**
- Follow Microsoft C# conventions
- Use async/await for I/O operations
- Implement proper exception handling
- Use dependency injection
- Write comprehensive XML documentation

</td>
<td width="50%">

#### 🎨 **Frontend Guidelines**
- Mobile-first responsive design
- Semantic HTML structure
- Progressive enhancement
- Accessibility compliance (WCAG 2.1)
- Cross-browser compatibility

</td>
</tr>
</table>

### 🔄 **Git Workflow**

```mermaid
gitgraph
    commit id: "Initial"
    branch feature/new-feature
    checkout feature/new-feature
    commit id: "Feature work"
    commit id: "Feature complete"
    checkout main
    merge feature/new-feature
    commit id: "Merge feature"
    branch hotfix/critical-fix
    checkout hotfix/critical-fix
    commit id: "Critical fix"
    checkout main
    merge hotfix/critical-fix
```

---

## 🤝 Contributing

### 🚀 **Getting Started**

1. **Fork** the repository
2. **Create** a feature branch: `git checkout -b feature/amazing-feature`
3. **Commit** your changes: `git commit -m 'Add amazing feature'`
4. **Push** to the branch: `git push origin feature/amazing-feature`
5. **Open** a Pull Request

### 📋 **Pull Request Guidelines**

- ✅ **Code Quality**: Ensure all tests pass
- 📝 **Documentation**: Update relevant documentation
- 🎯 **Focus**: Keep PRs focused on a single feature/fix
- 🔍 **Review**: Request review from team members
- 🧪 **Testing**: Include appropriate test coverage

---

## 📚 Additional Resources

### 📖 **Documentation**

<table>
<tr>
<td align="center">
<a href="#api-documentation">
<img src="https://img.shields.io/badge/API-Documentation-blue?style=for-the-badge&logo=swagger"/>
<br><strong>API Docs</strong>
</a>
</td>
<td align="center">
<a href="#database-schema">
<img src="https://img.shields.io/badge/Database-Schema-green?style=for-the-badge&logo=mongodb"/>
<br><strong>DB Schema</strong>
</a>
</td>
<td align="center">
<a href="#deployment-guide">
<img src="https://img.shields.io/badge/Deployment-Guide-orange?style=for-the-badge&logo=docker"/>
<br><strong>Deploy Guide</strong>
</a>
</td>
</tr>
</table>

### 🎓 **Learning Resources**

- 📘 [ASP.NET Core Documentation](https://docs.microsoft.com/en-us/aspnet/core/)
- 📗 [MongoDB .NET Driver](https://docs.mongodb.com/drivers/csharp/)
- 📙 [Bootstrap 5 Documentation](https://getbootstrap.com/docs/5.3/)
- 📕 [jQuery Documentation](https://api.jquery.com/)

---

## 📞 Support & Contact

<div align="center">

### 🤝 **Need Help?**

<table>
<tr>
<td align="center">
<img src="https://img.shields.io/badge/GitHub-Issues-black?style=for-the-badge&logo=github"/>
<br><strong>GitHub Issues</strong>
<br>Bug reports & feature requests
</td>
<td align="center">
<img src="https://img.shields.io/badge/Email-Support-blue?style=for-the-badge&logo=gmail"/>
<br><strong>Email Support</strong>
<br>support@autofix.com
</td>
<td align="center">
<img src="https://img.shields.io/badge/Discord-Community-purple?style=for-the-badge&logo=discord"/>
<br><strong>Discord Community</strong>
<br>Join our developer community
</td>
</tr>
</table>

</div>

---

## 📄 License

This project is licensed under the **MIT License** - see the [LICENSE](LICENSE) file for details.

---

<div align="center">

### 🌟 **Star this repository if you found it helpful!**

[![GitHub stars](https://img.shields.io/github/stars/yourusername/autofix?style=social)](https://github.com/yourusername/autofix/stargazers)
[![GitHub forks](https://img.shields.io/github/forks/yourusername/autofix?style=social)](https://github.com/yourusername/autofix/network/members)
[![GitHub watchers](https://img.shields.io/github/watchers/yourusername/autofix?style=social)](https://github.com/yourusername/autofix/watchers)

---

**Built with ❤️ by the AutoFix Team**

*Empowering the automotive service industry through technology*
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
│   ├── 🎮 DiagnosticController.cs   # System diagnostics & health checks
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
│   │   ├── 🚫 AccessDenied.cshtml       # Access denied page
│   │   ├── 👤 ClientProfile.cshtml      # Client profile management
│   │   ├── 👤 MechanicProfile.cshtml    # Mechanic profile management
│   │   └── 👤 Profile.cshtml            # General profile page
│   ├── 📁 Home/                     # Landing pages
│   │   ├── 🏠 Index.cshtml              # Homepage
│   │   └── 🔒 Privacy.cshtml            # Privacy policy
│   ├── 📁 Orders/                   # Order management views
│   │   ├── ➕ CreateOrder.cshtml        # Service request form
│   │   ├── 📊 Dashboard.cshtml          # Orders dashboard
│   │   ├── 📋 MyOrders.cshtml           # Client order list
│   │   ├── 📋 PendingOrders.cshtml      # Available orders for mechanics
│   │   ├── 📋 MyAcceptedOrders.cshtml   # Mechanic's accepted orders
│   │   └── 📄 Details.cshtml            # Order details view
│   └── 📁 Shared/                   # Shared layouts
│       ├── 🖼️ _Layout.cshtml             # Main layout template
│       ├── 🔗 _LoginPartial.cshtml       # Login status partial
│       ├── 📋 _OrdersList.cshtml         # Reusable order list component
│       ├── 📋 _MechanicOrdersList.cshtml # Mechanic orders component
│       ├── ✅ _ValidationScriptsPartial.cshtml # Client validation scripts
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
       "DatabaseName": "AutoFixMech",
       "ClientsCollectionName": "Clients",
       "MechanicsCollectionName": "Mechanics",
       "ClientOrdersCollectionName": "ClientOrders",
       "MechanicOrdersCollectionName": "MechanicOrders"
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
<td><code>/Orders/MyAcceptedOrders</code></td>
<td>GET</td>
<td>Get mechanic's accepted orders</td>
<td>Mechanic Role</td>
</tr>
<tr>
<td><code>/Orders/Dashboard</code></td>
<td>GET</td>
<td>Order dashboard view</td>
<td>Authenticated</td>
</tr>
<tr>
<td><code>/Orders/Details/{id}</code></td>
<td>GET</td>
<td>Get order details</td>
<td>Authenticated</td>
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

### 🔧 Diagnostic Endpoints

<table>
<tr><th>Endpoint</th><th>Method</th><th>Description</th><th>Authorization</th></tr>
<tr>
<td><code>/api/Diagnostic/test-connection</code></td>
<td>GET</td>
<td>Test MongoDB connection</td>
<td>Public</td>
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

#### Diagnostic API Response
```json
GET /api/Diagnostic/test-connection

{
  "message": "Connection successful",
  "settings": {
    "connectionString": "mongodb+srv://[user]...[redacted]",
    "databaseName": "AutoFixMech"
  },
  "collections": [
    "Clients",
    "Mechanics", 
    "ClientOrders",
    "MechanicOrders"
  ]
}
```

---

## 🔧 Troubleshooting

### 🚨 Common Issues

#### **MongoDB Connection Issues**

**Problem**: `Failed to connect to MongoDB` error on startup
```bash
# Solution 1: Test connection using diagnostic endpoint
curl https://localhost:7139/api/Diagnostic/test-connection

# Solution 2: Check connection string in appsettings.json
# Ensure IP whitelist includes your current IP in MongoDB Atlas

# Solution 3: Verify network connectivity
ping cluster0.mongodb.net
```

**Problem**: `Authentication failed` MongoDB error
```bash
# Solution: Verify credentials in connection string
# Format: mongodb+srv://username:password@cluster.mongodb.net/
# Ensure special characters in password are URL encoded
```

#### **Build and Runtime Issues**

**Problem**: Package restore fails
```cmd
# Clear NuGet cache and restore
dotnet nuget locals all --clear
dotnet restore --force
```

**Problem**: Application won't start
```cmd
# Check for port conflicts
netstat -an | findstr :7139
netstat -an | findstr :5000

# Run with detailed logging
set ASPNETCORE_ENVIRONMENT=Development
dotnet run --verbosity detailed
```

#### **Profile and Order Issues**

**Problem**: Profile updates not saving
- Verify MongoDB connection is active
- Check browser console for JavaScript errors
- Ensure form fields match model properties

**Problem**: Orders not displaying
- Check user authentication status
- Verify role-based authorization
- Test with diagnostic endpoint first

### 📊 Health Checks

Use the diagnostic endpoint to verify system health:

```bash
# Test MongoDB connectivity
curl -X GET https://localhost:7139/api/Diagnostic/test-connection

# Expected response indicates:
# ✅ Database connection successful
# ✅ Collections are accessible
# ✅ Network connectivity is working
```

### 🔍 Debugging Tips

1. **Enable Detailed Logging**
   ```json
   // appsettings.Development.json
   {
     "Logging": {
       "LogLevel": {
         "Default": "Debug",
         "AutoFix": "Trace",
         "Microsoft.AspNetCore": "Information"
       }
     }
   }
   ```

2. **MongoDB Query Debugging**
   ```csharp
   // Add to MongoDbContext for query logging
   clientSettings.ClusterConfigurator = cb => {
       cb.Subscribe<CommandStartedEvent>(e => {
           Console.WriteLine($"MongoDB Query: {e.Command}");
       });
   };
   ```

3. **Browser Developer Tools**
   - Check Network tab for failed API calls
   - Monitor Console for JavaScript errors
   - Inspect form submission data

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
    "DatabaseName": "AutoFixProd",
    "ClientsCollectionName": "Clients",
    "MechanicsCollectionName": "Mechanics", 
    "ClientOrdersCollectionName": "ClientOrders",
    "MechanicOrdersCollectionName": "MechanicOrders"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Warning",
      "AutoFix": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "autofix.yourdomain.com"
}
```

### ⚡ Performance Optimization

#### **MongoDB Optimization**
```javascript
// Recommended MongoDB indexes for production
db.Clients.createIndex({ "Email": 1 }, { unique: true })
db.Mechanics.createIndex({ "Email": 1 }, { unique: true })
db.ClientOrders.createIndex({ "ClientId": 1, "Status": 1 })
db.ClientOrders.createIndex({ "Location": "2dsphere" }) // For location-based queries
db.MechanicOrders.createIndex({ "MechanicId": 1, "Status": 1 })
```

#### **Application Performance**
```csharp
// Connection pooling settings in Program.cs
clientSettings.MaxConnectionPoolSize = 100;
clientSettings.MinConnectionPoolSize = 10;
clientSettings.MaxConnectionIdleTime = TimeSpan.FromMinutes(10);
clientSettings.ServerSelectionTimeout = TimeSpan.FromSeconds(30);
```

#### **Caching Strategy**
```csharp
// Add response caching for static content
builder.Services.AddResponseCaching();
builder.Services.AddMemoryCache();

// Configure caching middleware
app.UseResponseCaching();
app.Use(async (context, next) => {
    context.Response.GetTypedHeaders().CacheControl = 
        new Microsoft.Net.Http.Headers.CacheControlHeaderValue()
        {
            Public = true,
            MaxAge = TimeSpan.FromDays(1)
        };
    await next();
});
```

---

## 🔒 Security Features

### 🛡️ Authentication & Authorization

- **Cookie-based Authentication**: Secure session management
- **Role-based Authorization**: Client and Mechanic roles
- **Password Requirements**: Minimum 6 characters with digits
- **Account Lockout**: Protection against brute force attacks
- **Secure Headers**: HTTPS enforcement and security headers

### 🔐 Data Protection

```csharp
// Password hashing and security configuration
builder.Services.AddDefaultIdentity<ApplicationUser>(options => {
    options.Password.RequireDigit = true;
    options.Password.RequiredLength = 6;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireLowercase = false;
    options.SignIn.RequireConfirmedAccount = false;
    options.Lockout.MaxFailedAccessAttempts = 5;
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(15);
});
```

### 🌐 Network Security

- **HTTPS Redirection**: All traffic encrypted
- **CORS Configuration**: Controlled cross-origin requests  
- **Input Validation**: Server-side model validation
- **XSS Protection**: Razor view encoding
- **CSRF Protection**: Anti-forgery tokens

---

## 🗄️ Database Schema

### 📊 MongoDB Collections

#### **Clients Collection**
```javascript
{
  "_id": ObjectId("..."),
  "Id": "string",
  "FullName": "string",
  "UserName": "string", 
  "Email": "string",
  "PhoneNumber": "string",
  "Address": "string",
  "VehicleInformation": {
    "Make": "string",
    "Model": "string", 
    "Year": "number",
    "LicensePlate": "string"
  },
  "ProfileImageUrl": "string",
  "CreatedAt": ISODate("..."),
  "LastLogin": ISODate("...")
}
```

#### **Mechanics Collection**
```javascript
{
  "_id": ObjectId("..."),
  "Id": "string",
  "FullName": "string",
  "UserName": "string",
  "Email": "string", 
  "PhoneNumber": "string",
  "Address": "string",
  "Skills": ["string"],
  "Services": ["string"],
  "Bio": "string",
  "ProfileImageUrl": "string",
  "Rating": "number",
  "CompletedJobs": "number",
  "IsVerified": "boolean",
  "CreatedAt": ISODate("..."),
  "LastLogin": ISODate("...")
}
```

#### **ClientOrders Collection**
```javascript
{
  "_id": ObjectId("..."),
  "Id": "string",
  "ClientId": "string",
  "ServiceType": "string",
  "Description": "string",
  "Location": "string",
  "ScheduledTime": ISODate("..."),
  "EstimatedPrice": "decimal",
  "Status": "enum", // Pending, Accepted, InProgress, Completed, Cancelled
  "Notes": "string",
  "CreatedAt": ISODate("..."),
  "UpdatedAt": ISODate("...")
}
```

#### **MechanicOrders Collection**
```javascript
{
  "_id": ObjectId("..."),
  "Id": "string",
  "MechanicId": "string",
  "ClientOrderId": "string",
  "AcceptedAt": ISODate("..."),
  "CompletedAt": ISODate("..."),
  "ActualPrice": "decimal",
  "WorkDescription": "string",
  "Status": "enum", // Accepted, InProgress, Completed  "ClientRating": "number",
  "ClientFeedback": "string"
}
```

---

## 🚀 Recent Updates & Roadmap

### ✅ **Recently Completed (May 2025)**

- **Enhanced Profile Management**: 
  - Added address and vehicle information fields
  - Fixed profile editing functionality for both clients and mechanics
  - Improved data validation and error handling

- **Advanced UI/UX Improvements**:
  - Redesigned MyOrders page with modern card layouts
  - Enhanced CreateOrder form with floating labels and validation
  - Added responsive design improvements

- **System Diagnostics**:
  - Added MongoDB connection testing endpoint
  - Improved error logging and debugging capabilities
  - Enhanced performance monitoring

- **Database Optimization**:
  - Updated MongoDB collections structure
  - Added proper indexing for better performance
  - Implemented connection pooling

### 🔮 **Upcoming Features (Q3 2025)**

#### **Priority 1 - Core Functionality**
- [ ] **Real-time Notifications**: WebSocket implementation for live updates
- [ ] **Payment Integration**: Stripe/PayPal payment processing
- [ ] **Rating System**: Enhanced mechanic rating and review system
- [ ] **Location Services**: GPS-based mechanic matching

#### **Priority 2 - Advanced Features**
- [ ] **Mobile App**: React Native mobile application
- [ ] **AI Diagnostics**: ML-powered issue detection from photos
- [ ] **Inventory Management**: Parts and tools tracking for mechanics
- [ ] **Analytics Dashboard**: Business intelligence for mechanics

#### **Priority 3 - Platform Enhancement**
- [ ] **Multi-language Support**: Internationalization (i18n)
- [ ] **Admin Panel**: Advanced administrative controls
- [ ] **API Documentation**: Swagger/OpenAPI integration
- [ ] **Automated Testing**: Comprehensive test suite

### 📈 **Performance Targets**

- **Response Time**: < 200ms for critical operations
- **Uptime**: 99.9% availability
- **Concurrent Users**: Support for 10,000+ simultaneous users
- **Database Performance**: < 50ms query response time

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