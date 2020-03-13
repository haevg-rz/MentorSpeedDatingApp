﻿namespace MentorSpeedDatingApp.Models
{
    public interface IPerson
    {
        string Titel { get; set; }
        string Name { get; set; }
        string Vorname { get; set; }
    }

    public class Mentee : IPerson
    {
        public string Titel { get; set; }
        public string Name { get; set; }
        public string Vorname { get; set; }
    }

    public class Mentor : IPerson
    {
        public string Titel { get; set; }
        public string Name { get; set; }
        public string Vorname { get; set; }
    }
}