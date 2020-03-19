using System.Collections;

namespace MentorSpeedDatingApp.Models
{
    public interface IPerson
    {
        string Titel { get; set; }
        string Name { get; set; }
        string Vorname { get; set; }
        void Save();
        void Delete();
    }

    public class Mentee : IPerson
    {
        public string Titel { get; set; }
        public string Name { get; set; }
        public string Vorname { get; set; }

        public void Save()
        {
        }

        public void Delete()
        {
        }
    }

    public class Mentor : IPerson
    {
        public string Titel { get; set; }
        public string Name { get; set; }
        public string Vorname { get; set; }

        public void Save()
        {
        }

        public void Delete()
        {
        }
    }
}