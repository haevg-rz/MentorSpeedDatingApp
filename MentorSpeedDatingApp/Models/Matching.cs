using System.Collections.Generic;

namespace MentorSpeedDatingApp.Models
{
    public class Matching
    {
        public Mentor Mentor { get; set; }
        public List<IDate> Dates { get; set; }
    }
}