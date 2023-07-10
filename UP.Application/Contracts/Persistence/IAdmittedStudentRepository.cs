using System.Linq.Expressions;
using UP.Domain;

namespace UP.Application.Contracts.Persistence
{
    public interface IAdmittedStudentRepository : IGenericRepository<AdmittedStudent>
    {
        // Task<IEnumerable<AdmittedStudent?>> GetAllAdmittedStudents();
        // Task AddAdmittedStudentAsync(AdmittedStudent admittedStudent);
        // Task<AdmittedStudent?> GetOneAdmittedStudent(Expression<Func<AdmittedStudent, bool>> filter);
        // Task RemoveAdmittedStudent(AdmittedStudent student);
        // Task UpdateAdmittedStudent(AdmittedStudent admittedStudent);
    }
}
