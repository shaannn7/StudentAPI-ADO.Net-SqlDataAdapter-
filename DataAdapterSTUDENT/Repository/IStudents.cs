
using DataAdapterSTUDENT.Model;

namespace DataAdapterSTUDENT.Repository
{
    public interface IStudents
    {
        public List<Students> GetAllStudents();
        public Students GetStudents(int id);
        public void UpdateStudents(int id,Students students);
        public void AddStudents(Students students);
        public void DeleteStudents(int id);
    
    }
}
