using System;

namespace MentorSpeedDatingApp.Models
{
    public interface IDate
    {
        object Mentee { get; set; }
        TimeSlot TimeSlot { get; set; }
    }

    public class Date : IDate
    {
        public TimeSlot TimeSlot { get; set; }
        public object Mentee { get; set; }
    }

    public class BreakDate : IDate
    {
        public object Mentee { get; set; }
        public TimeSlot TimeSlot { get; set; }
    }

    public class TimeSlot
    {
        public bool IsBreak { get; set; }
        public DateTime Time { get; set; }
    }

    public class DateSpan
    {
        public string Time { get; set; }
    }
}