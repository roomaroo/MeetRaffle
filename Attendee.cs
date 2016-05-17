using CsvHelper.Configuration;

namespace MeetRaffle
{
    class Attendee
    {
        public string Name { get; set; }
        public string Title { get; set; }
        public string Guests { get; set; }
    }

    class AttendeeMap : CsvClassMap<Attendee>
    {
        public AttendeeMap()
        {
            Map(m => m.Name).Name("Name");
            Map(m => m.Title).Name("Title");
            Map(m => m.Guests).Name("Guests");
        }
    }
}
