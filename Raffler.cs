using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CsvHelper;
using CsvHelper.Configuration;

namespace Raffle
{
    class Raffler : INotifyPropertyChanged
    {
        private Attendee selectedAttendee;

        public event PropertyChangedEventHandler PropertyChanged;

        public IList<Attendee> Attendees { get; private set; }

        public void LoadNames(FileInfo attendeeList)
        {
            using (var reader = attendeeList.OpenText())
            {
                var csv = new CsvReader(reader, new CsvConfiguration {
                    HasHeaderRecord = true,
                    Delimiter ="\t",
                });
                csv.Configuration.RegisterClassMap<AttendeeMap>();

                this.Attendees = csv.GetRecords<Attendee>()
                    .Where(a => string.IsNullOrEmpty(a.Title))
                    .ToList();
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
                this.selectedAttendee = value;
                this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SelectedAttendee)));
            }
        }

        public async Task StartDraw(TimeSpan duration, TimeSpan drawInterval)
        {
            var stopwatch = new Stopwatch();
            var random = new Random();

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

        private bool drawInProgree;

        public bool DrawInProgress
        {
            get { return drawInProgree; }
            set
            {
                drawInProgree = value;
                this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(DrawInProgress)));
            }
        }

    }
}
