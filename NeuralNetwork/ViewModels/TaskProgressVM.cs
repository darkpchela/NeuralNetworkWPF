using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace NeuralNetwork.ViewModels
{
    public class TaskProgressVM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName]string property = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }

        private float _startValue;
        public float StartValue
        {
            get
            {
                return _startValue;
            }
            set
            {
                _startValue = value;
                OnPropertyChanged(nameof(StartValue));
            }
        }

        private float _endValue = 100;
        public float EndValue
        {
            get
            {
                return _endValue;
            }
            set
            {
                _endValue = value;
                OnPropertyChanged(nameof(EndValue));
            }
        }

        private float _value;
        public float Value
        {
            get
            {
                return _value;
            }
            set
            {
                _value = value;
                OnPropertyChanged(nameof(Value));
            }
        }

        private string _taskName;
        public string TaskName
        {
            get
            {
                return _taskName;
            }
            set
            {
                _taskName = value;
                OnPropertyChanged(nameof(TaskName));
            }
        }
    }
}
