﻿using System.Collections.Generic;
using VirtualClassroom.Domain;

namespace VirtualClassroom.Persistence
{
    public interface IActivitiesRepository : IRepository<Activity>
    {
        Activity GetByName(string name);
        List<Activity> GetByType(ActivityType type);

        ActivityType GetActivityType(string activityTypeName);
        List<ActivityType> GetActivityTypes();

        void RemoveActivityInfo(ActivityInfo activityInfo);
        void RemoveStudentActivity(StudentActivity studentActivity);
    }
}
