using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace MentorSpeedDatingApp.Models
{
    public interface IPerson
    {
        string Titel { get; set; }
        string Name { get; set; }
        string Vorname { get; set; }
        DateTime Date { get; set; }
        string ToString();
    }

    public class Mentee : IPerson
    {
        public string Titel { get; set; }
        public string Name { get; set; }
        public string Vorname { get; set; }
        public DateTime Date { get; set; }
        public List<Mentor> MatchedMentors { get; set; }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append(this.Titel);
            sb.Append(" ");
            sb.Append(this.Name);
            sb.Append(", ");
            sb.Append(this.Vorname);
            return sb.ToString();
        }
    }

    public class Mentor : IPerson
    {
        public string Titel { get; set; }
        public string Name { get; set; }
        public string Vorname { get; set; }
        public DateTime Date { get; set; }
        public List<Mentee> MatchedMentees { get; set; }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append(this.Titel);
            sb.Append(" ");
            sb.Append(this.Name);
            sb.Append(", ");
            sb.Append(this.Vorname);
            return sb.ToString();
        }
    }
}