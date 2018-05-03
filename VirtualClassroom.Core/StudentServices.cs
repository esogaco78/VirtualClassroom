using System.Collections.Generic;
using System.Linq;
using VirtualClassroom.Core.Shared;
using VirtualClassroom.Domain;
using VirtualClassroom.Persistence;

namespace VirtualClassroom.Core
{
    public class StudentServices : IStudentServices
    {
        private readonly IPersistanceContext persistanceContext;

        public StudentServices(IPersistanceContext persistanceContext)
        {
            this.persistanceContext = persistanceContext;
        }

        public Student GetStudent(int studentIdentifier)
        {
            IStudentRepository studentRepository = persistanceContext.GetStudentRepository();

            return studentRepository.GetById(studentIdentifier);
        }

        public IEnumerable<Student> GetAllStudents()
        {
            IStudentRepository studentRepository = persistanceContext.GetStudentRepository();

            return studentRepository.GetAll();
        }

        public void AddStudent(Student student)
        {
            IStudentRepository studentRepository = persistanceContext.GetStudentRepository();

            studentRepository.Add(student);
        }

        public void DeleteStudent(Student student)
        {
            IStudentRepository studentRepository = persistanceContext.GetStudentRepository();

            studentRepository.Delete(student);
        }

        public IEnumerable<Activity> GetActivities(int studentIdentifier)
        {
            IStudentRepository studentRepository = persistanceContext.GetStudentRepository();

            return studentRepository.GetActivities(studentIdentifier);
        }

        public Activity GetActivity(int studentIdentifier, int activityIdentifier)
        {
            IStudentRepository studentRepository = persistanceContext.GetStudentRepository();
            Activity studentActivity = studentRepository.GetActivities(studentIdentifier).Find(act => act.Id == activityIdentifier);

            return studentActivity;
        }

        public ActivityInfo GetActivityInfo(int studentIdentifier, int activityIdentifier)
        {
            IStudentRepository studentRepository = persistanceContext.GetStudentRepository();
            Student student = studentRepository.GetById(studentIdentifier);
            Activity studentActivity = studentRepository.GetActivities(studentIdentifier).Find(act => act.Id == activityIdentifier);
            ActivityInfo activityInfo = student.ActivityInfos.ToList().Find(act => act.ActivityId == studentActivity.Id);

            return activityInfo;
        }

        public IEnumerable<bool> GetActivityAttendance(int studentIdentifier, int activityIdentifier)
        {
            IStudentRepository studentRepository = persistanceContext.GetStudentRepository();
            Student student = studentRepository.GetById(studentIdentifier);

            List<bool> activityAttendance = new List<bool>();
            foreach(var studentActivityInfo in student.ActivityInfos)
            {
                if (studentActivityInfo.Presence == true && studentActivityInfo.ActivityId == activityIdentifier)
                    activityAttendance.Add(true);
            }

            return activityAttendance;
        }

        public IEnumerable<int> GetActivityGrades(int studentIdentifier, int activityIdentifier)
        {
            IStudentRepository studentRepository = persistanceContext.GetStudentRepository();
            Student student = studentRepository.GetById(studentIdentifier);

            List<int> activityGrades = new List<int>();
            foreach (var studentActivityInfo in student.ActivityInfos)
            {
                if (studentActivityInfo.ActivityId == activityIdentifier)
                    activityGrades.Add(studentActivityInfo.Grade);
            }

            return activityGrades;
        }

        public bool EditActivity(int studentIdentifier, ActivityInfo activityInfo)
        {
            IStudentRepository studentRepository = persistanceContext.GetStudentRepository();
            Student student = studentRepository.GetById(studentIdentifier);

            var activityInfoToEdit = student.ActivityInfos.ToList().Where(act => act.Id == activityInfo.Id).FirstOrDefault();

            if (activityInfoToEdit == null)
            {
                return false;
            }

            activityInfoToEdit.Presence = activityInfo.Presence;
            activityInfoToEdit.Grade = activityInfo.Grade;

            studentRepository.Save();

            return true;
        }
    }
}
