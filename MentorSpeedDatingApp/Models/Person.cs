using System.Collections;
using System.Runtime.CompilerServices;
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
            sb.Append(this.Name);
            return sb.ToString();
        }
    }

    public class Mentor : IPerson
    {
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
            sb.Append(this.Name);
            return sb.ToString();
        }
    }
}