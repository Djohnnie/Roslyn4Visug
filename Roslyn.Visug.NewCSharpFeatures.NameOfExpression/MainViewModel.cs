using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Roslyn.Visug.NewCSharpFeatures.NameOfExpression
{
    public class MainViewModel : INotifyPropertyChanged
    {
        #region [ PropertyChanged ]

        public event PropertyChangedEventHandler PropertyChanged;
        public void RaisePropertyChanged(String propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion

        #region [ Properties ]

        private String _message;

        public String Message
        {
            get { return _message; }
            set
            {
                _message = value;
                RaisePropertyChanged(nameof(Message));
            }
        }

        #endregion

        #region [ Commands ]

        public ICommand SampleCommand { get; }

        #endregion

        #region [ Construction ]

        public MainViewModel()
        {
            SampleCommand = new ActionCommand(OnSample);
        }

        #endregion

        #region [ Command Handlers ]

        private void OnSample()
        {
            this.Message = new Random().Next(1000000).ToString();
        }

        #endregion

    }
}