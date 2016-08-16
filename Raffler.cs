﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Media;
using System.Threading.Tasks;
using CsvHelper;
using CsvHelper.Configuration;

namespace MeetRaffle
{
    class Raffler : BindableBase
    {
        private Attendee selectedAttendee;

        public IList<Attendee> Attendees { get; private set; }

        public void LoadNames(FileInfo attendeeList)
        {
            using (var reader = attendeeList.OpenText())
            {
                var csv = new CsvReader(reader, new CsvConfiguration {
                    HasHeaderRecord = true,
                    Delimiter =","
                });
                csv.Configuration.RegisterClassMap<AttendeeMap>();

                this.Attendees = csv.GetRecords<Attendee>()
                    .Where(a => string.IsNullOrEmpty(a.Title))
                    .ToList();
                this.AddGuests();
            }
        }

        private void AddGuests()
        {
            var attendeesWithGuests = this.Attendees.Where(a => !a.Guests.NullOrEmpty()).ToList();
            foreach(var attendee in attendeesWithGuests)
            {
                var numberOfGuests = int.Parse(attendee.Guests.TrimStart('+'));
                foreach (var i in Enumerable.Range(1, numberOfGuests))
                {
                    if (numberOfGuests > 1)
                    {
                        this.Attendees.Add(new Attendee { Name = $"{attendee.Name}'s guest {i}" });
                    }
                    else
                    {
                        this.Attendees.Add(new Attendee { Name = $"{attendee.Name}'s guest" });
                    }
                }
            }
        }

        public Attendee SelectedAttendee
        {
            get
            {
                return this.selectedAttendee;
            }
            set
            {
                this.SetProperty(ref selectedAttendee, value);
            }
        }

        public async Task StartDraw(TimeSpan duration, TimeSpan drawInterval, Stream audioStream)
        {
            var stopwatch = new Stopwatch();
            var random = new Random();
            new SoundPlayer(audioStream).Play();

            this.DrawInProgress = true;
            stopwatch.Start();
            while(stopwatch.Elapsed < duration)
            {
                var index = random.Next(this.Attendees.Count);
                this.SelectedAttendee = this.Attendees[index];
                await Task.Delay(drawInterval);
            }

            this.DrawInProgress = false;
        }

        private bool drawInProgress;

        public bool DrawInProgress
        {
            get { return drawInProgress; }
            set
            {
                this.SetProperty(ref drawInProgress, value);
            }
        }

    }
}
