using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ScheduleEditorClassLibrary
{
    public enum ClassTypes
    {
        Lecture,
        Practice
    }

    public enum SubGroups
    {
        First,
        Second
    }
    public enum RowTypes
    {
        Simple,
        TwoGroups,
        TwoWeeks,
        TwoGroupsAndTwoWeeks
    }
    public enum Results
    {
        [Description("Available")]
        Available,
        [Description("Преподаватель занят")]
        TeacherIsBusy,
        [Description("TypeMismatch")]
        TypeMismatch,
        [Description("Аудитория занята другой группой")]
        AudienceIsOccupied,
        [Description("Желание преподавателя не учтено")]
        InconsistencyWithDesire
    }
}
