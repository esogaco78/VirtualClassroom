
using VirtualClassroom.Core.Shared;
using VirtualClassroom.Domain;
using VirtualClassroom.Persistence;

namespace VirtualClassroom.Core
{
    class ProfessorServices : IProfessorLogic
    {
        private readonly IPersistanceContext persistanceContext;

        public ProfessorServices(IPersistanceContext persistanceContext)
        {
            this.persistanceContext = persistanceContext;
        }

        public bool CreateActivity(int professorIdentifier, Activity activity)
        {
            IProfessorRepository professorRepository = persistanceContext.GetProfessorRepository();
            Professor professor = professorRepository.GetById(professorIdentifier);

            professor.Activities.Add(activity);
            professorRepository.Save();

            return true;
        }

        public bool EditActivity(int professorIdentifier, Activity activity)
        {
            IProfessorRepository professorRepository = persistanceContext.GetProfessorRepository();
            Professor professor = professorRepository.GetById(professorIdentifier);

            Activity activityToEdit = null;
            foreach(var professorActivity in professor.Activities)
            {
                if(professorActivity.Id == activity.Id)
                {
                    activityToEdit = professorActivity;
                    break;
                }
            }
            
            if (activityToEdit == null)
                return false;

            activityToEdit = activity;
            professorRepository.Save();

            return true;
        }

        public bool DeleteActivity(int professorIdentifier, int activityIdentifier)
        {
            IProfessorRepository professorRepository = persistanceContext.GetProfessorRepository();
            Professor professor = professorRepository.GetById(professorIdentifier);

            Activity activityToDelete = null;
            foreach (var professorActivity in professor.Activities)
            {
                if (professorActivity.Id == activityIdentifier)
                {
                    activityToDelete = professorActivity;
                    break;
                }
            }

            if (activityToDelete == null)
                return false;

            professor.Activities.Remove(activityToDelete);
            professorRepository.Save();

            return true;
        }

        public Activity GetActivity(int professorIdentifier, int activityIdentifier)
        {
            IProfessorRepository professorRepository = persistanceContext.GetProfessorRepository();
            Professor professor = professorRepository.GetById(professorIdentifier);

            foreach(var professorActivity in professor.Activities)
            {
                if (professorActivity.Id == activityIdentifier)
                    return professorActivity;
            }

            return null;
        }
    }
}
