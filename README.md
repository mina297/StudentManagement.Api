# StudentManagement.Api

An ASP.NET Core Web API built as part of my Software Developer internship at **MCV** (IT Department, Obour branch), covering full-stack fundamentals with a focus on clean backend architecture.

## About

This project simulates a simple student management system, built incrementally over several internship training days:

- **Day 1 — Web API Basics:** Controllers, CRUD endpoints (GET, POST, PUT, DELETE), route parameters, query strings, request bodies, and proper HTTP responses (`Ok`, `NotFound`, `BadRequest`).
- **Day 2 — Models, DTOs, Services & Dependency Injection:** Refactored the project into a clean architecture with separate `Models`, `Dtos`, and `Services` layers. Controllers now delegate business logic to services (`IStudentService`, `IDepartmentService`) via constructor-injected dependency injection, instead of holding logic directly.

## Tech Stack

- **ASP.NET Core Web API** (.NET 9)
- **Swagger / Swashbuckle** for interactive API documentation and testing
- **C#** with a layered architecture: Controllers → Services → Models/DTOs

## Project Structure

```
StudentManagement.Api/
├── Controllers/
│   ├── StudentsController.cs
│   └── DepartmentsController.cs
├── Models/
│   ├── Student.cs
│   └── Department.cs
├── Dtos/
│   ├── CreateStudentDto.cs
│   ├── UpdateStudentDto.cs
│   └── StudentDetailsDto.cs
├── Services/
│   ├── IStudentService.cs
│   └── StudentService.cs
└── Program.cs
```

## Features

**Students**
- Get all students / get by id
- Search students by name
- Filter students by age range (18–22), sorted
- Create, update, delete students
- Department existence validation before create/update

**Departments**
- Full CRUD (GET, POST, PUT, DELETE)
- Department statistics (student count, average/oldest/youngest age)
- Highest and lowest department by student count

## Running Locally

1. Clone the repo
2. Open `StudentManagement.Api.sln` in Visual Studio
3. Run the project (`F5`)
4. Swagger UI will be available at `https://localhost:{port}/swagger`

## About Me

Mina Elkomos — Computer Science student at Misr International University (MIU), aspiring software engineer.
- GitHub: [mina297](https://github.com/mina297)
- LinkedIn: [mina-samaan-zakaria](https://linkedin.com/in/mina-samaan-zakaria-75a3a6321)
