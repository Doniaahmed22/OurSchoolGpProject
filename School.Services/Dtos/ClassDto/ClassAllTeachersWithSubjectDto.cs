using School.Data.Entities;
using School.Services.Dtos.SharedDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Services.Dtos.ClassDto
{
    public class ClassAllTeachersWithSubjectDto:ClassDtoWithId
    {
        public List<TeachersSubjectDto> classAllTeachersWithSubject { get; set; } = new List<TeachersSubjectDto> ();
        public ClassAllTeachersWithSubjectDto(int id, int number, int NumOfStudent, NameIdDto Department, NameIdDto Level, NameIdDto Term)
        :base(id, number, NumOfStudent, Department, Level, Term)
        {


        }

        public void PutTolist(IEnumerable<Subject> Subjects, Class c)
        {

            foreach (Subject Subject in Subjects)
            {
                TeachersSubjectDto teachersSubjectDto = new TeachersSubjectDto ();
                teachersSubjectDto.Subject.Name = Subject.Name;
                teachersSubjectDto.Subject.Id = Subject.Id;
                TeacherSubjectClass tsc = c.TeacherSubjectClasses.FirstOrDefault(tsc => tsc.SubjectId == Subject.Id);
                if(tsc != null)
                {
                    Teacher t = tsc.Teacher;
                    if (t != null)
                    {
                        teachersSubjectDto.ChosenTeacher.Name = t.Name;
                        teachersSubjectDto.ChosenTeacher.Id = t.Id;
                    }

                }

                foreach (var teacherSubject in Subject.TeachersSubject)
                {

                    teachersSubjectDto.Teachers.Add(new NameIdDto()
                    {
                        Name = teacherSubject.Teacher.Name,
                        Id = teacherSubject.Teacher.Id

                    });
                }

                classAllTeachersWithSubject.Add(teachersSubjectDto);

            }
        }
    }
}
