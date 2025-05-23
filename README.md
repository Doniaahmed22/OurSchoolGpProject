# School Management System

A comprehensive school management platform that facilitates communication and academic tracking between students, parents, teachers, and school administrators.

## ✨ Features

### 🛡️ Administrator Features
- **User Management**
  - Login with secure authentication
  - Add and manage students, parents, and teachers
  - Edit user information and profiles
  - Search and view all system users
- **Academic Management**
  - Assign students to classes
  - Add subjects and manage curriculum
  - Create classes and assign teachers to subjects
  - Assign final grades for all students
- **Monitoring & Communication**
  - View attendance records for all students
  - Send absence warnings to parents
  - Request parent meetings
  - Send announcements to all users

### 👨‍🏫 Teacher Features
- **Class Management**
  - Login with role-based access
  - View all assigned classes
  - Upload and share educational materials
- **Academic Tracking**
  - Take class attendance
  - Assign grades for each class
  - Send progress reports to parents
- **Communication**
  - Send class announcements
  - Chat with parents and students in real-time

### 🎓 Student Features
- **Academic Access**
  - Login to personal dashboard
  - View all subjects and learning materials
  - Access teacher information
  - View personal grades and progress
- **Attendance & Communication**
  - View attendance records and absence warnings
  - Read school and class announcements
  - Chat with teachers

### 👨‍👩‍👧‍👦 Parent Features
- **Child Monitoring**
  - Login to family dashboard
  - View children's progress reports across subjects
  - Access teacher contact information
  - Monitor grades for each child
- **Communication**
  - Read school announcements
  - Chat with children's teachers
  - Receive meeting requests and notifications

## 🧰 Technologies

### 🧱 Architecture

* **N-tier Architecture**: Separation of concerns across UI, business logic, and data layers
* **Repository Pattern**: Abstraction for cleaner data access
* **Unit of Work**: Consistent data operations within a transaction

### ⚙️ Backend Stack

* **ASP.NET Core API**: RESTful services
* **Entity Framework Core**: ORM for efficient DB interaction
* **SQL Server**: Relational database engine

### 🔐 Authentication & Authorization

* **JWT**: Token-based user authentication
* **Role-based Authorization**: Secure and tailored access by user type

### 📡 Real-Time Communication

* **SignalR**: Enables real-time chat and notifications

### 🧩 Design Principles

* **Dependency Injection**: Enhances modularity and testability
* **SOLID Principles**: Ensures clean, maintainable, and scalable code

