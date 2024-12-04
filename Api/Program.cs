using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Collections.Generic;
using System.Data;
using UCLAIREPO;
using UCLA.repo;
using UCLA;
using Npgsql;
using Dapper;
using System.Diagnostics.Tracing;


namespace UCLAservices
{


    public class StudentRepostring
    {
        private readonly string _connectionString;

        public StudentRepostring(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void AddStudent(Student student)
        {
            using IDbConnection db = new NpgsqlConnection(_connectionString);
            db.Execute("INSERT INTO Students (@Id,Firstname, Lastname, Email) VALUES (@Id,@Firstname, @Lastname, @Email)", student);
        }

        public void Update(Student student)
        {
            using IDbConnection db = new NpgsqlConnection(_connectionString);
            db.Execute("UPDATE Students SET Firstname = @Firstname, Lastname = @Lastname, Email = @Email WHERE Id = @Id", student);
        }

        public List<Student> GetAll()
        {
            using IDbConnection db = new NpgsqlConnection(_connectionString);
            return db.Query<Student>("SELECT * FROM Students").AsList();
        }

    }

    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            string connectionString = "Host=localhost;Port=5433;Username=defaultuser;Password=root;Database=database;";
            StudentRepo repo = new StudentRepo(connectionString);

            var app = builder.Build();


            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
                app.UseStaticFiles();
            }

      

            app.MapGet("/students", () =>
            {
                var students = repo.GetAll();
                return Results.Ok(students);
            })
            .WithName("GetStudents")
            .WithOpenApi();

            app.MapPost("/students", (Student student) =>
            {
                repo.AddStudent(student);
                return Results.Created($"/students/{student.Id}", student);
            })
            .WithName("AddStudent")
            .WithOpenApi();

            app.MapPut("/students", (Student student) =>
            {
                repo.Update(student);
                return Results.Ok(student);
            })
            .WithName("UpdateStudent")
            .WithOpenApi();

            app.MapDelete("/students/{id}", (int Id) =>
            {
              try
                    {
                        
                        repo.DeleteById(Id);
                        return Results.Ok($"Student with ID {Id} deleted.");
                    }
                        catch (Exception ex)
                    {
                        return Results.BadRequest($"Error: {ex.Message}");
                    }
            })
            .WithName("DeleteStudent")
            .WithOpenApi();

            app.MapGet("/", () =>
            {
                return Results.Content(@"<!DOCTYPE html>
<html lang=""en"">
<head>
    <meta charset=""UTF-8"">
    <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
    <title>Welcome</title>
</head>
<body>
    <h1>This is the only thing available...</h1>
    <p>If you want to use SQL, click here:</p>
    <form action=""/submitsql.html"" method=""get"">
        <button type=""submit"">Enter</button>
    </form>
</body>
</html>", "text/html");
            })
            .WithName("default page")
            .WithOpenApi();


            app.Run();
        }
    }
}
