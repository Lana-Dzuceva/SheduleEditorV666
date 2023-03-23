using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ScheduleEditorClassLibrary
{
    public class Schedule
    {
        public List<SGroup> Groups { get; set; }

        public Schedule()
        {
            Groups = new List<SGroup>();
        }

        [JsonConstructor]
        public Schedule(List<SGroup> groups)
        {
            Groups = groups;
        }

        public SGroup this[string groupTitle]
        {
            get { return Groups.Where(group => group.Title == groupTitle).Single(); }
        }
    }
}
