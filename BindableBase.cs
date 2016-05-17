using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MeetRaffle
{
    abstract class BindableBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        virtual protected void SetProperty<T>(ref T field, T value, [CallerMemberName]string propertyName = null)
        {
            if (field?.Equals(value) != true)
            {
                field = value;
                this.OnPropertyChanged(propertyName);
            }
        }

        private void OnPropertyChanged(string propertyName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
