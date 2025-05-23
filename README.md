# School Management System

A comprehensive school management platform that facilitates communication and academic tracking between students, parents, teachers, and school administrators.

## 🎯 Overview

The School Management System is designed to streamline educational administration and enhance communication between all stakeholders in the educational process. The system provides role-based access control and tailored functionality for different user types.

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

## 🏗️ Technical Architecture

### Architecture Pattern
- **N-tier Architecture**: Clean separation across presentation, business, and data layers
- **Repository Pattern**: Abstraction layer for data access operations in `School.Repository`
- **Unit of Work Pattern**: Manages transactions and ensures data consistency

### Technology Stack

**Backend Framework**
- **ASP.NET Core API**: RESTful web API framework
- **Entity Framework Core**: ORM for database operations
- **SQL Server**: Primary database system

**Authentication & Security**
- **JWT Authentication**: Secure token-based authentication
- **Role-based Authorization**: Access control per user type

**Real-time Communication**
- **SignalR**: Real-time chat functionality between users

**Design Patterns & Principles**
- **Dependency Injection**: Loose coupling and testability
- **SOLID Principles**: Maintainable and scalable code structure
