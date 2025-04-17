# Vehicle Rental System

A web-based application for managing vehicle rentals, built with ASP.NET Core MVC, Entity Framework Core (Code‑First) and a clean N‑Tier architecture (DAL → BLL → Web).

## 📝 Overview

This system allows customers to:
- Browse and filter available vehicles
- Make, view and cancel reservations
- Submit reviews and ratings
- Track payment and rental history

Administrators can:
- Manage vehicles, locations, customers, reservations and payments
- View reports & analytics
- Control user roles and access (Admin, UserTypeA, UserTypeB)

## ⚙️ Features

1. **Customer Management**  
   - Registration, login/logout, profile  
   - Rental history & payment records  

2. **Vehicle Management**  
   - Add, update, delete listings  
   - Categorize by type, price, availability  

3. **Reservation System**  
   - Book by pickup/drop‑off date & location  
   - Prevent double‑booking  

4. **Payment Processing**  
   - Secure online payments  
   - Invoice & payment history  

5. **Rental History**  
   - Tracks past rentals  
   - Total cost calculations  

6. **Reviews & Ratings**  
   - Customers submit feedback  
   - Vehicles display average rating  

7. **Location Management**  
   - Manage multiple branch locations  

8. **Reporting & Analytics**  
   - Revenue, utilization, top‑rented reports  

9. **Role‑Based Access**  
   - Admin vs. UserTypeA vs. UserTypeB  

## 🔧 Tech Stack

- **Backend**: .NET Core MVC  
- **Data Access**: Entity Framework Core (Code‑First)  
- **Architecture**:  
  - **DAL**: `DbContext`, Repositories  
  - **BLL**: Services  
  - **Web**: Controllers & Razor Views  
- **Authentication**: ASP.NET Identity  
- **Frontend**: Bootstrap 5, jQuery Unobtrusive Validation  

## 🚀 Getting Started

### Prerequisites

- [.NET 6 SDK](https://dotnet.microsoft.com/download) or later  
- SQL Server (Express/LocalDB) or any EF‑Core provider  
- Visual Studio 2022 / VS Code  

### Installation

1. **Clone the repo**
   ```bash
   git clone https://github.com/ladeyekun/vehicle‑rental‑system.git
   cd vehicle‑rental‑system
