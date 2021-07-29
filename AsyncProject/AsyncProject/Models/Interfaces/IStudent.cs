using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AsyncProject.Models
{
    interface IStudent
    {
        // CREATE
        Task<Student> Create(Student student);

        // GET ALL

        Task<List<Student>> GetStudents();

        //UPDATE
        Task<Student> UpdateStudent(int id, Student student);

        // DELETE
        Task Delete(int id);
    }
}
