using UCLA;
using UCLAIREPO;
using Npgsql;
using System.Data;
using System.Linq.Expressions;
using Microsoft.VisualBasic;
using Dapper;
namespace UCLA.repo;

public class StudentRepo: IRepository<Student>
{
    private readonly string _connectionString;  
    private List<Student> _students= new List<Student>();


    public StudentRepo(string connectionString)
    {
         _connectionString = connectionString;

    }
    


        private IDbConnection CreateConnection()
        {
            return new NpgsqlConnection(_connectionString);
        }
    
    public Student GetById(int id)
    {
        using ( var connection =  CreateConnection())
        {
            string query = "SELECT * FROM students WHERE Id = @Id";
            return connection.QuerySingleOrDefault<Student>(query, new { Id = id });
        }
        
    }
    public List<Student> GetAll()
        {
            using (var connection = CreateConnection())
            {
                string query = "SELECT * FROM students";
                return connection.Query<Student>(query).AsList();
            }
        }
    public void AddStudent(Student s)
    {
        using (var connection = CreateConnection())
            {
                string query = "INSERT INTO students (Firstname,Lastname, Email) VALUES (@Firstname,@Lastname, @Email)";
                connection.Execute(query, s);
            }
    }
    public void Update(Student s)
    {
        using (var connection = CreateConnection())
            {
                string query = "UPDATE students SET Firstname = @Firstname, Lastname = @Lastname, Email = @Email WHERE Id = @Id";
                connection.Execute(query, s);
            }
    }
    public void DeleteById(int id)
    {
        using (var connection = CreateConnection())
            {
                string query = "DELETE FROM students WHERE Id = @Id";
                connection.Execute(query, new { Id = id });
            }
    }
    public void DeleteAll(int n)
    {
        using (var connection = CreateConnection())
        {
            string query = "DELETE FROM students Where Id=@Id";
            for(int i=0; i<n; i++)
            {
                connection.Execute(query, new { Id = i });
            }

        }
    }

}
