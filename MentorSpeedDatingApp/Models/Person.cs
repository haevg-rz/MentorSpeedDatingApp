using System;
using System.Text;

namespace MentorSpeedDatingApp.Models
{
    public interface IPerson
    {
        string Titel { get; set; }
        string Name { get; set; }
        string Vorname { get; set; }
        string ToString();
    }

    public class Mentee : IPerson
    {
        public Mentee()
        {
            this.Titel = "";
            this.Name = "";
            this.Vorname = "";
        }

        public string Titel { get; set; }
        public string Name { get; set; }
        public string Vorname { get; set; }
        public bool IsFiller { get; set; }

        public override string ToString()
        {
            var sb = new StringBuilder();
            if (!String.IsNullOrWhiteSpace(this.Titel))
            {
                sb.Append(this.Titel);
                sb.Append(" ");
            }

            sb.Append(this.Name);
            sb.Append(", ");
            sb.Append(this.Vorname);
            return sb.ToString();
        }
    }

    public class Mentor : IPerson
    {
        public Mentor()
        {
            this.Titel = "";
            this.Name = "";
            this.Vorname = "";
        }

        public string Titel { get; set; }
        public string Name { get; set; }
        public string Vorname { get; set; }

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