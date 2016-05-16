using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsvHelper.Configuration;

namespace Raffle
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
