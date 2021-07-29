using AsyncProject.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace AsyncProject.Models.Services
{
    // We are shifting the responsibility from the controller itself to the service.
    // The controller then calls the service.
    public class StudentService : IStudent
    {

        private AsyncInnDbContext _context;

        public StudentService(AsyncInnDbContext context)
        {
            _context = context;
        }


        public async Task<Student> Create(Student student)
        {
            // student is an instance of student.
            // We will create a student with parameters and return it.
            // Current state of student object: raw
            _context.Entry(student).State = Microsoft.EntityFrameworkCore.EntityState.Added;
            // The current state of the student object: added

            await _context.SaveChangesAsync();
            return student;
        }

        public async Task<ActionResult<IEnumerable<Student>>> GetStudents()
        {
            var students = await _student.GetStudents();
            return NoContent();
        }

        // An action to return a single student to the controller.
        public async Task<Student> GetStudent(int id)
        {
            Student student = await _context.Students.FindAsync(id);
            return student;
        }

        public async Task<Student> UpdateStudent(int id, Student student)
        {
            _context.Entry(student).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return student;
        }

        Task<List<Student>> IStudent.GetStudents()
        {
            throw new NotImplementedException();
        }

        public async Task Delete(int id)
        {
            // Pulling a student by its specific id, given the ID.
            Student student = await GetStudent(id);

            _context.Entry(student).State = Microsoft.EntityFrameworkCore.EntityState.Deleted;
            await _context.SaveChangesAsync();
        }
    }
}
