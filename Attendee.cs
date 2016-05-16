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
        public string UserId { get; set; }
        public string Title { get; set; }
    }

    class AttendeeMap : CsvClassMap<Attendee>
    {
        public AttendeeMap()
        {
            Map(m => m.Name).Name("Name");
            Map(m => m.UserId).Name("User ID");
            Map(m => m.Title).Name("Title");
        }
    }
}
