namespace UCLAIREPO;

public interface IRepository<T>
{
    T GetById(int id);
    List<T> GetAll  ();
    void AddStudent(T s);
    void Update(T s);
    void DeleteById(int id);
    
    
}