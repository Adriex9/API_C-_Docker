# 🎓 UCLA Student API

This project provides a RESTful API to manage a PostgreSQL database of students.  
Built with **.NET Core** and **Dapper ORM**, it supports full CRUD operations and is containerized using **Docker** with persistent storage.

---

## 🚀 Features

- 📋 **Retrieve all students**  
  Get a list of all students stored in the database.

- ➕ **Add a new student**  
  Insert a new student record into the database.

- ✏️ **Update student details**  
  Modify an existing student’s information.

- 🗑️ **Delete a student**  
  Remove a student from the database.

- 💾 **Data persistence**  
  A Docker volume is built-in to ensure **data is not lost** even if the container crashes.

---

## 🛠️ Prerequisites

Make sure the following are installed on your machine:

- 🐳 Docker
- 🧱 .NET 6.0 SDK or higher
- 🐘 PostgreSQL
- 🧪 Dapper ORM

---

## 📦 Running the Project with Docker

Make sure you're in the project directory, then run:

```bash
docker-compose up --build -d
