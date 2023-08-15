using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ToDo_Web_App.Models
{
    public class Filters
    {
        public Filters(string filterString)
        {
            filterString = filterString ?? "all-all-all";
            string[] filters = filterString.Split('-');

            categoryID = filters[0];
            due = filters[1];
            statusID = filters[2]; 
        }

        public string filterString { get; }
        public string categoryID { get; }
        public string due { get; }
        public string statusID { get; }

        public bool HasCategory => categoryID.ToLower() != "all";
        public bool HasDue => due.ToLower() != "all";
        public bool HasStatus => statusID.ToLower() != "all";

        public static Dictionary<string, string> dueFilterValues =>
            new Dictionary<string, string>
            {
                {"future","Future"},
                {"past","Past" },
                {"today","Today"}
            };

        public bool isPast => due.ToLower() == "past";
        public bool isFuture => due.ToLower() == "future";
        public bool isToday => due.ToLower() == "today";


    }
}
