using System.ComponentModel;
using System.Runtime.CompilerServices;
using DML_MobgameClient.UI.Annotations;

namespace DML_MobgameClient.UI.MVVM.Utils
{
    public class ObservableObject : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}