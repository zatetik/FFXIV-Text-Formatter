using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Models
{
    public class TextLog : INotifyPropertyChanged
    {
        private string _filteredText;
        public string FilePath { get; set; }
        public string RawText { get; set; }
        public string FilteredText { 
            get { return _filteredText; } 
            set 
            { 
                _filteredText = value;
                OnPropertyChanged(nameof(FilteredText));
            } 
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
